using System;
using System.Net.Http;
using WebApi.Helpers;
using WebApiGateway.Entities;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Entities;

namespace RegistrationBelougaConsole
{
    class Program
    {
        private readonly AppSettings _appSettings;
       static private readonly PrincetonhiveContext _context;
       static HttpClient _client;
        //public Program(PrincetonhiveContext context)
        //{
        //    //  _appSettings = appSettings.Value;
        //    _context = context;
        //    _client = new HttpClient();
        //}

        public static void Main(string[] args)
        {
            try
            {
                OnPostAsync(new PrincetonhiveContext()).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
           
        }

       
        public class ResponseViewModel
        {
            public int statuscode { get; set; }
            public string message { get; set; }
            public dynamic data { get; set; }
            public string accessToken { get; set; }
        }
        public enum StatusCodesStatus
        {
            SuccessCode = 200,
            InternaServerError = 500,
            DataNotFound = 204,
            DataCreated = 201,
            BadRequest = 400,
            NotFound = 404,
            UnAuthorised = 401
        }

        static public async Task<IActionResult> OnPostAsync(PrincetonhiveContext _context)
        {
            _client = new HttpClient();
            var studentid = (from a in _context.TblStudentRegistration
                               where a.StudentIdBelouga == null
                               select a.StudentRegistrationId).ToList();

            foreach(var id in studentid)
            {
                User UserModel = new User();

                UserModel.Id = id;

                var response = await _client.PostAsJsonAsync("http://localhost:52778/Users/Registration_Student", UserModel);
                var loginResult = response.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
                if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                {
                  //  Console.WriteLine(content.data.DisplayName,"has been registered successfully.");
                }

            }
            return null;
        }
    }
}
