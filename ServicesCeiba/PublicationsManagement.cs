using ServicesCeiba.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCeiba
{
    public class PublicationsManagement
    {
        #region Atributos
        private readonly HttpRequestHelper httpRequestHelper;
        public readonly string WebAPIUrl;
        #endregion
        public PublicationsManagement(string webApiUrl)
        {
            this.WebAPIUrl = webApiUrl;
            httpRequestHelper = new HttpRequestHelper(WebAPIUrl); //HttpRequestRestFulHelper
        }
        public async Task<List<Posts>> GetPosts()
        {
            string url = "/posts";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(url);
            return await httpRequestHelper.Get<List<Posts>>(sb.ToString());
        }
        public async Task<List<Users>> GetUsers()
        {
            string url = "/users";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(url);
            return await httpRequestHelper.Get<List<Users>>(sb.ToString());
        }
        public async Task<List<Posts>> GetPostsByUsers(string Id)
        {
            string url = $"/posts?userId={Id}";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(url);
            return await httpRequestHelper.Get<List<Posts>>(sb.ToString());
        }

    }
}
