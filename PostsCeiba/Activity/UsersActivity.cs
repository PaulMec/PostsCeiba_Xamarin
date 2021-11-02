using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
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
    public class UsersActivity : AppCompatActivity
    {
        #region variables
        ListView ListUsers;
        Button BtnBack;
        TextView TextResults;
        EditText SearchBar;

        List<Users> Users;
        #endregion
        /// <summary>
        /// Initialize variables and view
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.list_users);
            ListUsers = FindViewById<ListView>(Resource.Id.lvList_Users);
            BtnBack = FindViewById<Button>(Resource.Id.btnBack);
            TextResults = FindViewById<TextView>(Resource.Id.tvTextResults);
            SearchBar = FindViewById<EditText>(Resource.Id.searchBar);
            SearchBar.TextChanged += SearchBar_TextChanged;
            BtnBack.Click += BtnBack_Click;

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            LoadUsers();
        }
        /// <summary>
        /// Search to name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string searchString = e.Text.ToString();
            var filteredList = Users?.Where(p => p.Name?.ToLower().Contains(searchString.ToLower()) ?? false).ToList();
            LoadUsersIntoView(filteredList);
        }
        /// <summary>
        /// Get the users in the services
        /// </summary>
        private async void LoadUsers()
        {
            try
            {
                Toast.MakeText(this, "Lista de escritores", ToastLength.Short).Show();
                Users = await WebServices.PublicationsManagementClient.GetUsers();
                if (Users.Count != 0)
                {
                    LoadUsersIntoView(Users);
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
        /// Charge users in the view
        /// </summary>
        /// <param name="response"></param>
        private void LoadUsersIntoView(List<Users> response)
        {
            if (response != null && response.Count != 0)
            {
                TextResults.Visibility = ViewStates.Gone;
                ListUsers.Visibility = ViewStates.Visible;
                ListUsers.Adapter = new UsersAdapter(this, response);
            }
            else
            {
                TextResults.Visibility = ViewStates.Visible;
                ListUsers.Visibility = ViewStates.Gone;
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