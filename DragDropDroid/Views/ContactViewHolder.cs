using System;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace DragDropDroid.Views {
    public class ContactViewHolder : RecyclerView.ViewHolder {
        Action<int> _listener;

        public TextView VCName { get; set; }
        public TextView VCEmail { get; set; }

        // Initialize controls.
        public ContactViewHolder(View itemView, Action<int> listener) : base(itemView) {
            VCName = itemView.FindViewById<TextView>(Resource.Id.VCContactName);
            VCEmail = itemView.FindViewById<TextView>(Resource.Id.VCContactEmail);

            _listener = listener;
            itemView.Click += OnClick;
        }

        // Provides positional information to the on click event.
        void OnClick(object sender, EventArgs e) {
            int position = base.AdapterPosition;
            if(position != RecyclerView.NoPosition) {
                _listener(position);
            }
        }
    }
}