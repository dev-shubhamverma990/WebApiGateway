using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;
using WebApiGateway.Entities;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using WebApiGateway;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
       
       // private readonly ILogger<UsersController> logger; // =new Logger<UnitTest1>();
       
        private IUserService<User> _userService= new UserService(  new PrincetonhiveContext());
       
      
        Dictionary<string, object> userlogin1 = new Dictionary<string, object>();

        [Fact]
        public void GetToken()
        {
            var serviceProvider = new ServiceCollection()
       .AddLogging()
       .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<UsersController>();
            var controller = new UsersController(  _userService, logger);

            // Act
            User user = new User();
            user.Username = "yogesh123";
            user.Password = "password";
            var result = controller.Authenticate(user);
            Assert.NotNull(result);
        }
        [Fact]
        public void LoginUser()
        {
            var controller = new UsersController(_userService, null);

            // Act
            User user = new User();
            user.Username = "yogesh123";
            var result = controller.UserName(user);
            Assert.NotNull(result);
        }

        [Fact]
        public void LoginCheck()
        {
            var controller = new UsersController(_userService, null);

            // Act
            User user = new User();
            user.Username = "yoges23";
            user.Password = "password";
            var result = controller.LoginCheck(user);

        }
        [Fact]
        public void Logout()
        {
            var controller = new UsersController(_userService, null);

    
           
            var result = controller.Logout("yogesh123");

        }
        [Fact]
        public void Registration()
        {

            var controller = new UsersController(_userService, null);

            // Act
            //   User user = new User();
            //   user.Username = "yogesh123";

            var result = controller.GetRegistrationInfo("yogesh123");
            Assert.NotNull(result);
        }

        [Fact]
        public void ChkEmailTeacher()
        {

            var controller = new UsersController(_userService, null);

            var result = controller.emailcheck("v2.local.ENG98mfmCWo7p8qEha5nuyv4lP5y8248CH52s2ties_VQW7BUKjlX8_k_FYLORU7zFpRLeFM-261tVD2gRYTIcIjfMBOMbELwVkF6iuWen0gJw08dokV14ndhBG1xe7gZNzGq9N_TAzZ83Lz140OcOxO2POJKL0N0k2imMWckZGLKcBykkE_I-IV7SVbGRUx-G-K-bKrWS3A9HuCoilsCjDkrde6JvUopjYDANfL0sjbCrsnZv8n4FQ", "amar@gmail.com");
            Assert.NotNull(result);

        }

        [Fact]
        public void ChkUsernameTeacher()
        {

            var controller = new UsersController(_userService, null);

            var result = controller.usernamecheck("v2.local.ENG98mfmCWo7p8qEha5nuyv4lP5y8248CH52s2ties_VQW7BUKjlX8_k_FYLORU7zFpRLeFM-261tVD2gRYTIcIjfMBOMbELwVkF6iuWen0gJw08dokV14ndhBG1xe7gZNzGq9N_TAzZ83Lz140OcOxO2POJKL0N0k2imMWckZGLKcBykkE_I-IV7SVbGRUx-G-K-bKrWS3A9HuCoilsCjDkrde6JvUopjYDANfL0sjbCrsnZv8n4FQ", "amar123");
            Assert.NotNull(result);

        }

        [Fact]
        public void ChkEmailStudent()
        {

            var controller = new UsersController(_userService, null);

            var result = controller.emailcheck("v2.local.ENG98mfmCWo7p8qEha5nuyv4lP5y8248CH52s2ties_VQW7BUKjlX8_k_FYLORU7zFpRLeFM-261tVD2gRYTIcIjfMBOMbELwVkF6iuWen0gJw08dokV14ndhBG1xe7gZNzGq9N_TAzZ83Lz140OcOxO2POJKL0N0k2imMWckZGLKcBykkE_I-IV7SVbGRUx-G-K-bKrWS3A9HuCoilsCjDkrde6JvUopjYDANfL0sjbCrsnZv8n4FQ", "paras4@gmail.com");
            Assert.NotNull(result);

        }

        [Fact]
        public void ChkUsernameStudent()
        {

            var controller = new UsersController(_userService, null);

            var result = controller.usernamecheck("v2.local.ENG98mfmCWo7p8qEha5nuyv4lP5y8248CH52s2ties_VQW7BUKjlX8_k_FYLORU7zFpRLeFM-261tVD2gRYTIcIjfMBOMbELwVkF6iuWen0gJw08dokV14ndhBG1xe7gZNzGq9N_TAzZ83Lz140OcOxO2POJKL0N0k2imMWckZGLKcBykkE_I-IV7SVbGRUx-G-K-bKrWS3A9HuCoilsCjDkrde6JvUopjYDANfL0sjbCrsnZv8n4FQ", "parassingh");
            Assert.NotNull(result);

        }

        //[Fact]
        //public void GetBelougaRegistrationTeacher()
        //{

        //    var controller = new UsersController(_userService, null);

        //    var result = controller.GetBelougaRegistrationTeacher(1);
        //    Assert.NotNull(result);

        //}

    }
}
