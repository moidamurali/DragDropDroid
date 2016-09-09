using System;
using Android.Content;
using Android.Views;
using Android.Support.V7.Widget;
using DragDropDroid.ViewModels;
using static Android.Views.View;

namespace DragDropDroid.Views {
    public class ContactsAdapter : RecyclerView.Adapter, IOnLongClickListener {
        public event EventHandler<int> ItemClick;

        public MainViewModel MainViewModel;

        // Simple ctor initializes a new instance of our MainViewModel.
        public ContactsAdapter() {
            MainViewModel = new MainViewModel();
        }

        // Not used, but overridden as required.
        public override int ItemCount { get { return MainViewModel.Contacts.Count; } }

        // Set the layout and initialize the Card View's listener to this adapter's OnLoncClick implementation.
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            var layout = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContactListItem, parent, false);

            var vcContactsCardView = layout.FindViewById<CardView>(Resource.Id.VCContactCardView);
            vcContactsCardView.SetOnLongClickListener(this);

            return new ContactViewHolder(layout, OnItemClick);
        }

        // Bind the Name and Email fields to the Contacts list.
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            var viewHolder = (ContactViewHolder)holder;

            viewHolder.VCName.Text = MainViewModel.Contacts[position].Name;
            viewHolder.VCEmail.Text = MainViewModel.Contacts[position].Email;
        }
        
        // Invoke the OnItemClick event with positional data..
        void OnItemClick(int position) {
            ItemClick?.Invoke(this, position);
        }

        // Long click event to start dragging when CarView has a LongClick event.
        public bool OnLongClick(View view) {
            ClipData data = ClipData.NewPlainText("", "");
            DragShadowBuilder shadowBuilder = new View.DragShadowBuilder(view);
            view.StartDrag(data, shadowBuilder, view, 0);
            view.Visibility = ViewStates.Invisible;
            return true;
        }
    }
}