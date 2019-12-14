using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApiGateway.Helper
{
    public static class CommonModel
    {
        public static ContentResult GetResponse(string message, dynamic data, HttpStatusCode statusCode, long count = 0)
        {
            dynamic dyanamicObject = new ExpandoObject();
            dyanamicObject.message = message;
            dyanamicObject.data = data;
            dyanamicObject.statuscode = statusCode;
            dyanamicObject.totalCount = count;
            
            return new ContentResult
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(dyanamicObject),
                ContentType = "text/json",
                StatusCode = (int)statusCode
            };
        }
    }
}
