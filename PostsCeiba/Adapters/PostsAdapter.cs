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
    public class PostsAdapter : BaseAdapter
    {
        #region Variables and Controls
        Context context;
        List<Posts> posts;
        Posts userSelect;
        #endregion

        public PostsAdapter(Context context, List<Posts> Posts)
        {
            this.context = context;
            this.posts = Posts;
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
            PostsAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as PostsAdapterViewHolder;

            var postsData = posts[position];
            if (holder == null)
            {
                holder = new PostsAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.item_posts_user, parent, false);
                holder.Title = view.FindViewById<TextView>(Resource.Id.tvTitle);
                holder.Body = view.FindViewById<TextView>(Resource.Id.tvBody);
                holder.btnBack = view.FindViewById<Button>(Resource.Id.btnBackPopUp);
                view.Tag = holder;


                holder.Title.Text = postsData.Title.Trim();
                holder.Body.Text = postsData.Body.Trim();
            }
            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return posts.Count;
            }
        }
    }

    class PostsAdapterViewHolder : Java.Lang.Object
    {
        public TextView Title { get; set; }
        public TextView Body { get; set; }
        public Button btnBack { get; set; }
    }
}