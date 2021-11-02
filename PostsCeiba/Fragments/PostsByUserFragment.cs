using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicesCeiba.Models;
using ServicesCeiba;
using PostsCeiba.Adapters;

namespace PostsCeiba.Fragments
{
    public class PostsByUserFragment : DialogFragment
    {
        #region variables
        List<Posts> postsByUser;
        Users userSelect;
        Context context;
        TextView nameWriter;
        TextView cellphone;
        TextView email;
        TextView city;
        TextView website;
        ListView listPostsByUser;
        Button btnBack;
        #endregion
        public PostsByUserFragment(Context context, Users user)
        {
            this.context = context;
            this.userSelect = user;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetStyle(DialogFragmentStyle.NoFrame, Resource.Style.CustomDialogFragment);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ListPostsByUser, container, false);
            nameWriter = view.FindViewById<TextView>(Resource.Id.tvNameWriter);
            cellphone = view.FindViewById<TextView>(Resource.Id.tvCellphone);
            email = view.FindViewById<TextView>(Resource.Id.tvEmail);
            city = view.FindViewById<TextView>(Resource.Id.tvCity);
            website = view.FindViewById<TextView>(Resource.Id.tvWebsite);
            listPostsByUser = view.FindViewById<ListView>(Resource.Id.ListPostsByUser);
            btnBack = view.FindViewById<Button>(Resource.Id.btnBackPopUp);
            btnBack.Click += BtnCancel_Click;


            nameWriter.Text = userSelect.Name.Trim();
            cellphone.Text = string.Format("Teléfono: {0}", userSelect.Phone);
            email.Text = string.Format("Email: {0}", userSelect.Email);
            city.Text = string.Format("Ciudad: {0}", userSelect.Address.City);
            website.Text = string.Format("WebSite: {0}", userSelect.Website);
            GetPostsByUsers();
            return view;
        }

        private async void GetPostsByUsers()
        {
            try
            {
                Toast.MakeText(context, "Lista de publicaciones por escritor", ToastLength.Short).Show();
                var response = await WebServices.PublicationsManagementClient.GetPostsByUsers(userSelect.Id.ToString());
                if (response.Count != 0)
                {
                    postsByUser = response;
                    listPostsByUser.Adapter = new PostsByUserAdapter(context, postsByUser);
                }
                else
                {
                    Toast.MakeText(context, "Ooops: error al obtener las publicaciones", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast toast = Toast.MakeText(context, string.Format(Resources.GetString(Resource.String.txt_error), ex.Message), ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }
        }

        /// <summary>
        /// Acción dentro del popup botón de atras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }
}