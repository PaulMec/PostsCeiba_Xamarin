using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PostsCeiba.Activity;
using PostsCeiba.Fragments;
using ServicesCeiba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostsCeiba.Adapters
{
    public class UsersAdapter : BaseAdapter
    {
        #region Variables and Controls
        Context context;
        List<Users> users;
        Users userSelect;
        #endregion

        public UsersAdapter(Context context, List<Users> users)
        {
            this.context = context;
            this.users = users;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            PostAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as PostAdapterViewHolder;

            var usersPosts = users[position];
            if (holder == null)
            {
                holder = new PostAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_writer, parent, false);
                holder.WriterName = view.FindViewById<TextView>(Resource.Id.tvWriterName);
                holder.Mobile = view.FindViewById<TextView>(Resource.Id.tvPhone);
                holder.Email = view.FindViewById<TextView>(Resource.Id.tvEmail);
                holder.MoreInfo = view.FindViewById<ImageView>(Resource.Id.btnMore);
                holder.MoreInfo.Click += MoreInfo_Click;
                holder.MoreInfo.Tag = usersPosts.Id;
                view.Tag = holder;

            }

            //fill in your items
            holder.WriterName.Text = usersPosts.Name?.ToString();
            holder.Mobile.Text = string.Format("Teléfono: {0}", usersPosts.Phone.ToString());
            holder.Email.Text = string.Format("Email: {0}", usersPosts.Email.ToString());

            return view;
        }

        private void MoreInfo_Click(object sender, EventArgs e)
        {
            try
            {
                var position = ((ImageView)sender);
                foreach (var user in users)
                    if (user.Id.ToString() == position.Tag.ToString())
                    {
                        userSelect = user;
                    }
                var ParentActivity = (UsersActivity)context;
                var transaction = ParentActivity.FragmentManager.BeginTransaction();
                var postsByUserFragment = new PostsByUserFragment(context, userSelect);
                postsByUserFragment.Cancelable = false;
                postsByUserFragment.Show(transaction, null);
                Toast.MakeText(context, "Lista de escritores", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast toast = Toast.MakeText(context, string.Format("Error: {0}", ex.Message), ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }

        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return users.Count;
            }
        }
    }

    public class PostAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView WriterName { get; set; }
        public TextView Mobile { get; set; }
        public TextView Email { get; set; }
        public ImageView MoreInfo { get; set; }

    }
}