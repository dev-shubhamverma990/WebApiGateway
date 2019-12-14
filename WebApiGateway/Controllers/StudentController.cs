using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreConsumingWebApi.Factory;
using AspCoreConsumingWebApi.Models;
using AspCoreConsumingWebApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/Student
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Student/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Student
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Student/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        private readonly IOptions<MySettingsModel> appSettings;

        public StudentController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        //public async Task<IActionResult> GetUser()
        //{
        //    var data = await ApiClientFactory.Instance.GetUsers();
        //    //var response = await SaveUser();
        //   // return View();
        //}

    }
}

