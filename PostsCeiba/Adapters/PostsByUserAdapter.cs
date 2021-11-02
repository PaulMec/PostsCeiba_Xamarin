using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ServicesCeiba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostsCeiba.Adapters
{
    public class PostsByUserAdapter : BaseAdapter
    {
        #region variables
        Context context;
        List<Posts> postsByUser;
        #endregion
        public PostsByUserAdapter(Context context, List<Posts> PostsByUser)
        {
            this.context = context;
            this.postsByUser = PostsByUser;
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
            PostsByUserAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as PostsByUserAdapterViewHolder;

            var postsByUserData = postsByUser[position];
            if (holder == null)
            {
                holder = new PostsByUserAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.item_posts_user, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.tvTitle);
                holder.Body = view.FindViewById<TextView>(Resource.Id.tvBody);
                holder.btnBack = view.FindViewById<Button>(Resource.Id.btnBackPopUp);
                view.Tag = holder;


                holder.Title.Text = postsByUserData.Title.Trim();
                holder.Body.Text = postsByUserData.Body.Trim();
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return postsByUser.Count;
            }
        }
    }

    class PostsByUserAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Title { get; set; }
        public TextView Body { get; set; }
        public Button btnBack { get; set; }
    }
}