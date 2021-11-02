using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using PostsCeiba.Adapters;
using ServicesCeiba;
using ServicesCeiba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostsCeiba.Activity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class PostsActivity : AppCompatActivity
    {
        #region variables
        ListView ListPosts;
        Button BtnBack;
        TextView TextResults;
        EditText SearchBar;

        List<Posts> Posts;
        #endregion
        /// <summary>
        /// Initialize variables and view
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.list_posts);
            ListPosts = FindViewById<ListView>(Resource.Id.lvList_Posts);
            BtnBack = FindViewById<Button>(Resource.Id.btnBack);
            TextResults = FindViewById<TextView>(Resource.Id.tvTextResults);
            SearchBar = FindViewById<EditText>(Resource.Id.searchBar);
            BtnBack.Click += BtnBack_Click;
            SearchBar.TextChanged += SearchBar_TextChanged;

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            LoadPosts();
        }

        /// <summary>
        /// Search to title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string searchString = e.Text.ToString();
            var filteredList = Posts?.Where(p => p.Title?.ToLower().Contains(searchString.ToLower()) ?? false).ToList();
            LoadPostsIntoView(filteredList);
        }

        private async void LoadPosts()
        {
            try
            {
                Toast.MakeText(this, "Lista de publicaciones", ToastLength.Short).Show();
                Posts = await WebServices.PublicationsManagementClient.GetPosts();
                if (Posts.Count != 0)
                {
                    LoadPostsIntoView(Posts);
                }

            }
            catch (Exception ex)
            {
                Toast toast = Toast.MakeText(this, string.Format(Resources.GetString(Resource.String.txt_error), ex.Message), ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }
        }

        /// <summary>
        /// Charge posts in the view
        /// </summary>
        /// <param name="response"></param>
        private void LoadPostsIntoView(List<Posts> response)
        {
            if(response != null && response.Count != 0)
            {
                TextResults.Visibility = ViewStates.Gone;
                ListPosts.Visibility = ViewStates.Visible;
                ListPosts.Adapter = new PostsAdapter(this, response);
            }
            else
            {
                TextResults.Visibility = ViewStates.Visible;
                ListPosts.Visibility = ViewStates.Gone;
            }
        }
        /// <summary>
        /// Back view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}