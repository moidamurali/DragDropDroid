using System;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using static Android.Views.View;
using Android.Graphics.Drawables;
using DragDropDroid.Models;

namespace DragDropDroid.Views {
    [Activity(Label = "DragDropDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainView : Activity {

        RecyclerView _recyclerView;
        ContactsAdapter _adapter;

        Contact _contactToMove;
        int _currentContactPosition = -1;
        int _newContactPosition = -1;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            // Initialize the adapter.
            _adapter = new ContactsAdapter();
            _adapter.ItemClick += OnListItemClick;

            // Initialize the recycler view.
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.VCContacts);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            _recyclerView.SetAdapter(_adapter);

            // Hook up our drag event hanlders to the Drop Area and Recycler View.
            _recyclerView.Drag += OnDragInsideRecyclerView;
            FindViewById<LinearLayout>(Resource.Id.VCDropArea).Drag += OnDragToBox;

            // Hook up the button click event.
            FindViewById<Button>(Resource.Id.VCAddContact).Click += OnAddContactClick;
        }

        // Adds a new contact to the contact list with some default values.
        private void OnAddContactClick(object sender, EventArgs e) {
            _adapter.MainViewModel.Contacts.Insert(0, new Contact() { Name = "Added Contact", Email = "AddedContact@fakeaddy.com" });
            _adapter.NotifyItemInserted(0);
            _recyclerView.ScrollToPosition(0);
        }

        // Displays an alert showing the selected contact's information.
        void OnListItemClick(object sender, int position) {
            var contact = _adapter.MainViewModel.Contacts[position];

            var builder = new AlertDialog.Builder(this);
            builder.SetTitle($"{contact.Name}");
            builder.SetMessage($"Email: {contact.Email}");
            builder.SetPositiveButton("OK", (send, args) => { });
            builder.SetCancelable(false);
            builder.Show();
        }

        // Rearranges items inside the recycler view based on drag actions.
        void OnDragInsideRecyclerView(object sender, DragEventArgs e) {
            var theEvent = e.Event;
            var view = (View)theEvent.LocalState;
            var containerView = (RecyclerView)sender;

            _currentContactPosition = containerView.GetChildAdapterPosition(view);

            // Ensure the position is valid.
            if(_currentContactPosition != -1) {
                _contactToMove = _adapter.MainViewModel.Contacts[_currentContactPosition];
            }


            switch(theEvent.Action) {
                case DragAction.Location:
                    View onTopOf = containerView.FindChildViewUnder(theEvent.GetX(), theEvent.GetY());
                    _newContactPosition = containerView.GetChildAdapterPosition(onTopOf);

                    // Ensure the new position is valid.
                    if(_newContactPosition != -1) {

                        var layoutManager = (LinearLayoutManager)_recyclerView.GetLayoutManager();
                        int firstVisiblePosition = layoutManager.FindFirstCompletelyVisibleItemPosition();
                        int lastVisiblePosition = layoutManager.FindLastVisibleItemPosition();

                        // Scroll up or down one if we are over the first or last visible list item.
                        if(_newContactPosition >= lastVisiblePosition)
                            layoutManager.ScrollToPositionWithOffset(firstVisiblePosition + 1, 0);
                        else if(_newContactPosition <= firstVisiblePosition)
                            layoutManager.ScrollToPositionWithOffset(firstVisiblePosition - 1, 0);

                        // Update the location of the Contact
                        _adapter.MainViewModel.Contacts.Remove(_contactToMove);
                        _adapter.MainViewModel.Contacts.Insert(_newContactPosition, _contactToMove);
                        _adapter.NotifyItemMoved(_currentContactPosition, _newContactPosition);
                    }
                    e.Handled = true;
                    break;

                case DragAction.Ended:
                    // Reset the visibility for the Contact item's view.
                    // This is done to reset the state in instances where the drag action didn't do anything.
                    view.Visibility = ViewStates.Visible;

                    // Boundary condition, scroll to top is moving list item to position 0.
                    if(_newContactPosition != -1) {
                        _recyclerView.ScrollToPosition(_newContactPosition);
                        _newContactPosition = -1;
                    }
                    else {
                        _recyclerView.ScrollToPosition(0);
                    }

                    e.Handled = true;
                    break;
            }
        }

        // Sets drag box style when contacts are dragged to the box.
        // Removes list items from recycler view when user drags list items to the box.
        void OnDragToBox(object sender, DragEventArgs e) {
            // Styling updates based on whether the drag item is in our out of the box area.
            Drawable defaultShape = GetDrawable(Resource.Drawable.shape);
            Drawable enterShape = GetDrawable(Resource.Drawable.shape_droptarget);

            var theEvent = e.Event;
            var view = (View)theEvent.LocalState;
            var containerView = (View)sender;

            switch(theEvent.Action) {
                case DragAction.Entered:
                    containerView.SetBackgroundResource(Resource.Drawable.shape_droptarget);
                    e.Handled = true;
                    break;

                case DragAction.Exited:
                    containerView.SetBackgroundResource(Resource.Drawable.shape);
                    e.Handled = true;
                    break;

                case DragAction.Drop:

                    _adapter.MainViewModel.Contacts.Remove(_contactToMove);
                    _adapter.NotifyItemRemoved(_currentContactPosition);

                    ViewGroup owner = (ViewGroup)(view.Parent);
                    if(owner != null)
                        owner.RemoveView(view);
                    e.Handled = true;
                    break;

                case DragAction.Ended:
                    containerView.SetBackgroundResource(Resource.Drawable.shape);
                    e.Handled = true;
                    break;
            }
        }
    }
}

