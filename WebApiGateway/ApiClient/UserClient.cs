using AspCoreModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiGateway.Entities;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<List<TblUserLogin>> GetUsers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, 
                "api/Student/GetStudents"));
            return await GetAsync<List<TblUserLogin>>(requestUrl);
        }

        public async Task<Message<TblUserLogin>> SaveUser(TblUserLogin model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "User/SaveUser"));
            return await PostAsync<TblUserLogin>(requestUrl, model);
        }
    }
}
