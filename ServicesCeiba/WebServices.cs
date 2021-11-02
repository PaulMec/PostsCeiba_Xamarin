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
using System.Threading.Tasks;

namespace ServicesCeiba
{
	/// <summary>
	/// Web services implementation
	/// </summary>
	public class WebServices
	{
		#region Public Methods

		public static void InitAPIClients(string endPointURL)
		{
			PublicationsManagementClient = new PublicationsManagement(endPointURL);
		}

		public static PublicationsManagement PublicationsManagementClient;
		#endregion
	}
}