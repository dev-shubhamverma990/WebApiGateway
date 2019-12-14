using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using WebApiGateway.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Cors;
using System.Net;
using WebApiGateway.Helper;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace WebApi.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService<User> _userService;
        private object userModel;
        private readonly HttpClient _httpClient;
        private  ILogger<UsersController> _logger;
        private readonly PrincetonhiveContext _context;
        public UsersController(IUserService<User> userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Registration_Teacher")]
        public IActionResult Registration_Teacher(int ID)
        {
            var princetonhive = new PrincetonhiveContext();
            var TblTeacher = _userService.GetBelougaRegistrationTeacher(ID);
            var teacheridbelouga = TblTeacher[0].TeacherIdBelouga;
            var schoolidbelouga = TblTeacher[0].SchoolIdBelouga;
            var token = _userService.BelougaToken();
            if ((teacheridbelouga == null || teacheridbelouga == "") && (schoolidbelouga == null || schoolidbelouga == ""))
            {

                var checkemail = emailcheck(token.Result, TblTeacher[0].Teacheremail);

                if (checkemail.Result != "false")
                {
                    var checkusername = usernamecheck(token.Result, TblTeacher[0].Teacherusername);
                    if (checkusername.Result != "false")
                    {

                        var userpass = _userService.Registration_Teacher_Paseto(ID, TblTeacher);
                        var teacherregister = userpass.Result;
                        if (teacherregister == "ID is not found")
                            return BadRequest(new { message = "ID is incorrect" });

                        var teacherresult = GetAsyncTeacherRegistration(teacherregister);

                        var teacherid = teacherresult.Result.Replace("\"", string.Empty).Trim();

                        if (teacherid != null || teacherid != "")
                        {
                            var teacherregistrationupdate = princetonhive.TblRegistration.Where(x => x.RegistrationId == ID).SingleOrDefault();
                            if (teacherregistrationupdate != null)
                            {
                                teacherregistrationupdate.TeacherIdBelouga = teacherid;
                                princetonhive.SaveChanges();
                            }
                            return CommonModel.GetResponse("Registration data is valid and submitted successfully!", teacherresult.Result, HttpStatusCode.OK);
                        }
                        else
                        {
                            return CommonModel.GetResponse("Teacher Not Registered", teacherresult.Result, HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return CommonModel.GetResponse("Teacher UserName Already Exists", checkusername.Result, HttpStatusCode.OK);
                    }
                }
                else
                {
                    return CommonModel.GetResponse("Teacher Eamil Already Exists", checkemail.Result, HttpStatusCode.OK);
                }


            }

            else if ((teacheridbelouga == null || teacheridbelouga == "") && (schoolidbelouga != null))
            {
                var checkemail = emailcheck(token.Result, TblTeacher[0].Teacheremail);

                if (checkemail.Result != "false")
                {
                    var checkusername = usernamecheck(token.Result, TblTeacher[0].Teacherusername);
                    if (checkusername.Result != "false")
                    {

                        var userpass = _userService.Registration_Teacher_Paseto(ID, TblTeacher);
                        var teacherregister = userpass.Result;
                        if (teacherregister == "ID is not found")
                            return BadRequest(new { message = "ID is incorrect" });

                        var teacherresult = GetAsyncTeacherRegistration(teacherregister);

                        var teacherid = teacherresult.Result.Replace("\"", string.Empty).Trim();

                        if (teacherid != null || teacherid != "")
                        {
                            var teacherregistrationupdate = princetonhive.TblRegistration.Where(x => x.RegistrationId == ID).SingleOrDefault();
                            if (teacherregistrationupdate != null)
                            {
                                teacherregistrationupdate.TeacherIdBelouga = teacherid;
                                princetonhive.SaveChanges();
                            }


                            if (schoolidbelouga != null && teacherid != null)
                            {
                                var classtoken = _userService.Registration_Class_Paseto(ID, schoolidbelouga, TblTeacher[0].Class, TblTeacher[0].Class, teacheridbelouga);
                                var classidresult = GetAsyncClassRegistration(classtoken.Result);
                                var classid = classidresult.Result.Replace("\"", string.Empty).Trim();

                                var classupdate = princetonhive.TblRegistration.Where(x => x.RegistrationId == ID).SingleOrDefault();
                                if (classupdate != null)
                                {

                                    classupdate.ClassIdBelouga = classid;
                                    princetonhive.SaveChanges();
                                }

                                if (TblTeacher[0].Teacherusername != null)
                                {
                                    var std = new UserLogin()
                                    {
                                        UserName = TblTeacher[0].Teacherusername,
                                        Password = Encrypt(TblTeacher[0].Teacherpassword),
                                        UserRole = TblTeacher[0].Type

                                    };
                                    princetonhive.UserLogin.Add(std);
                                    princetonhive.SaveChanges();
                                }


                            }
                            return CommonModel.GetResponse("Registration data is valid and submitted successfully!", teacherresult.Result, HttpStatusCode.OK);
                        }
                        else
                        {
                            return CommonModel.GetResponse("Teacher Not Registered", teacherresult.Result, HttpStatusCode.BadRequest);
                        }
                    }
                    else
                    {
                        return CommonModel.GetResponse("Teacher UserName Already Exists", checkusername.Result, HttpStatusCode.OK);
                    }
                }
                else
                {
                    return CommonModel.GetResponse("Teacher Eamil Already Exists", checkemail.Result, HttpStatusCode.OK);
                }
            }
            else
            {
                var schoolid = _userService.schoolcheck(token.Result, TblTeacher[0].Newuserstate, TblTeacher[0].Newuserpostalcode, TblTeacher[0].Newuserschoolname);
                if (schoolid != null && teacheridbelouga != null)
                {
                    var classtoken = _userService.Registration_Class_Paseto(ID,schoolid.Result, TblTeacher[0].Class, TblTeacher[0].Class, teacheridbelouga);
                    var classidresult = GetAsyncClassRegistration(classtoken.Result);
                    var classid = classidresult.Result.Replace("\"", string.Empty).Trim();

                    var teacherregistrationupdate = princetonhive.TblRegistration.Where(x => x.RegistrationId == ID).SingleOrDefault();
                    if (teacherregistrationupdate != null)
                    {
                        teacherregistrationupdate.SchoolIdBelouga = schoolid.Result;
                        teacherregistrationupdate.ClassIdBelouga = classid;
                        princetonhive.SaveChanges();
                    }

                    if (TblTeacher[0].Teacherusername != null)
                    {
                        var std = new UserLogin()
                        {
                            UserName = TblTeacher[0].Teacherusername,
                            Password = Encrypt(TblTeacher[0].Teacherpassword),
                            UserRole = TblTeacher[0].Type

                        };
                        princetonhive.UserLogin.Add(std);
                        princetonhive.SaveChanges();
                    }
                    
                }

                return CommonModel.GetResponse("Registration data is valid and submitted successfully!", teacheridbelouga, HttpStatusCode.OK);

            }
            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Registration_Class")]
        public IActionResult Registration_Class(int Id)
        {
            var TblClass = _userService.GetBelougaRegistrationClass(Id);
            var classtoken = _userService.BelougaToken();

              var userpass = _userService.Registration_Class_Paseto(Id, TblClass[0].SchoolIdBelouga, TblClass[0].ClassName, TblClass[0].ClassName, TblClass[0].TeacheId);
                if (userpass.Result == "ID is not found")
                    return BadRequest(new { message = "ID is incorrect" });

                var classidresult = GetAsyncClassRegistration(userpass.Result);
                var classid = classidresult.Result.Replace("\"", string.Empty).Trim();

                using (var db = new PrincetonhiveContext())
                {
                    var resultclass = db.Tblclassmap.Where(y => y.ClassmapId == Id).SingleOrDefault();
                    if (resultclass != null)
                    {
                        resultclass.ClassIdBelouga = classid;
                        db.SaveChanges();
                    }

                }

                if (classidresult.Status.Equals(HttpStatusCode.OK))
                {
                    return CommonModel.GetResponse("Registration data is valid and submitted successfully!", classid, HttpStatusCode.OK);
                }
                else
                {
                    return CommonModel.GetResponse("Class Not Registered", classtoken.Result, HttpStatusCode.OK);
                }
            


        }


        [AllowAnonymous]
        [HttpPost]
        [Route("emailcheck")]
        public async Task<string> emailcheck(string tokenemail,string email)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenemail);
            var response = await _httpClient.GetAsync("https://belouga.org/api/checkEmail/" + email);
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responseData1 = responseData.Split(":");
            var success1 = responseData1[0];
            var success2 = responseData1[1];
            var success3 = responseData1[2];
            var success4 = responseData1[3];
            var success5 = success4.Split("}");
            var success6 = success5[0];
            return await Task.FromResult(success6);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("usernamecheck")]
        public async Task<string> usernamecheck(string tokenusername, string username)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenusername);
            var response = await _httpClient.GetAsync("https://belouga.org/api/checkUsername/" + username);
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responseData1 = responseData.Split(":");
            var success1 = responseData1[0];
            var success2 = responseData1[1];
            var success3 = responseData1[2];
            var success4 = responseData1[3];
            var success5 = success4.Split("}");
            var success6 = success5[0];
            return await Task.FromResult(success6);
        }
        public int ID;

        [AllowAnonymous]
        [HttpPost]
        [Route("Registration_Student")]
        public IActionResult Registration_Student(User model)
        {
            var TblStudent = _userService.GetBelougaRegistrationStudent(model.Id);
            var studenttoken = _userService.BelougaToken();
           
                var usernameresult = usernamecheck(studenttoken.Result, TblStudent[0].Username);
              
                if (usernameresult.Result != "false")
                {
                    var userpass = _userService.Registration_Student_Paseto(model.Id,TblStudent[0].FirstName,TblStudent[0].LastName, TblStudent[0].Username, TblStudent[0].Dob.ToString(), TblStudent[0].Class.ToString());
                    if (userpass.Result == "ID is not found")
                        return BadRequest(new { message = "ID is incorrect" });

                    var studentidresult = GetAsyncStudentRegistration(userpass.Result);
                    var studentid = studentidresult.Result.Replace("\"", string.Empty).Trim();
                    
                    using (var db = new PrincetonhiveContext())
                    {
                        var resultstudent = db.TblStudentRegistration.Where(y => y.StudentRegistrationId == model.Id).SingleOrDefault();
                        if (resultstudent != null)
                        {
                        resultstudent.StudentIdBelouga = studentid;

                      //  resultstudent.ClassIdBelouga = classid;
                        db.SaveChanges();
                        }

                        if (TblStudent[0].Username != null)
                        {
                            var std = new UserLogin()
                            {
                                UserName = TblStudent[0].Username,
                                Password = Encrypt(TblStudent[0].password),
                                UserRole = TblStudent[0].Type

                            };
                            db.UserLogin.Add(std);
                            db.SaveChanges();
                        }

                    }

                    if (studentidresult.Status.Equals(HttpStatusCode.OK))
                    {
                        return CommonModel.GetResponse("Registration data is valid and submitted successfully!", studentid, HttpStatusCode.OK);
                    }
                    else
                    {
                        return CommonModel.GetResponse("Student Not Registered", studentid, HttpStatusCode.OK);
                    }
                }
                else
                {
                    return CommonModel.GetResponse("Student Username Already Exists", TblStudent[0].Username, HttpStatusCode.OK);
                }

           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword([FromBody]User userParam)
        {
            var role = _userService.userinfo(userParam.Username);
            if (role.Result != null)
            {
                var roleresult = role.Result.Role;
                if (roleresult == "Teacher")
                {
                    var emailcheck = _userService.Registration(userParam.Username);
                    var emailresult = emailcheck.Result.Email;
                   
                    return CommonModel.GetResponse("Your Password Send Successfully on " + emailresult, (role.Result.Password,emailcheck.Result.FirstName, emailresult), HttpStatusCode.OK);
                     
                }
                else
                {
                    var emailcheck = _userService.StudentRegistration(userParam.Username);
                    string emailresult;
                    if (emailcheck.Result.FatherEmail != null)
                    {
                        emailresult = emailcheck.Result.FatherEmail;
                    }
                    else
                    {
                        //  emailresult = emailcheck.Result.SchoolEmail;
                        emailresult = "";
                    }
                    
                    return CommonModel.GetResponse("Your Password Send Successfully on "+emailresult, (role.Result.Password, emailcheck.Result.DisplayName,emailresult), HttpStatusCode.OK);
                    
                }
            }
            else
            {
                return CommonModel.GetResponse("Username does not Exist!", null, HttpStatusCode.OK);
            }
               
        }

        private async Task<string> GetAsyncTeacherRegistration(string result)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);
            var response = await _httpClient.GetAsync("https://belouga.org/api/registerTeacher");
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responseData1 = responseData.Split(":");
            var success1 = responseData1[0];
            var success2 = responseData1[1];
            var success3 = responseData1[2];
            var success4 = responseData1[3];
            var success5 = responseData1[4];
            //var success6 = responseData1[3];
            var success7 = success5.Split("}");
            var success8 = success7[0];
            return await Task.FromResult(success8);
           
        }

        private async Task<string> GetAsyncClassRegistration(string result)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);
            var response = await _httpClient.GetAsync("https://belouga.org/api/registerClassroom");
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responseData1 = responseData.Split(":");
            var success1 = responseData1[0];
            var success2 = responseData1[1];
            var success3 = responseData1[2];
            var success4 = responseData1[3];
            var success5 = responseData1[4];
            //var success6 = responseData1[3];
            var success7 = success5.Split("}");
            var success8 = success7[0];
            return await Task.FromResult(success8);
        }

        private async Task<string> GetAsyncStudentRegistration(string result)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);
            var response = await _httpClient.GetAsync("https://belouga.org/api/registerStudent");
            var responseData = response.Content.ReadAsStringAsync().Result;
            var responseData1 = responseData.Split(":");
            var success1 = responseData1[0];
            var success2 = responseData1[1];
            var success3 = responseData1[2];
            var success4 = responseData1[3];
            var success5 = responseData1[4];
            //var success6 = responseData1[3];
            var success7 = success5.Split("}");
            var success8 = success7[0];
            return await Task.FromResult(success8);
        }

        // [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            _logger.LogInformation(LoggingEvents.Login, "Username ({0}), Password({1}) have been passed on datetime({2})", userParam.Username, userParam.Password,DateTime.UtcNow.AddHours(5.30));
           // using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
            // var sessionList = await _userServic
            {
                // var sessionList = await _userServic
                var userpass = _userService.Authenticate_Paseto(userParam.Username, userParam.Password);
            if (userpass.Result == "Username is not found")
                    return CommonModel.GetResponse("Username is incorrect", userParam.Username, HttpStatusCode.BadRequest);
              //  return BadRequest(new { message = "Username is incorrect" });
            if (userpass.Result == "Password is invalid")
                return CommonModel.GetResponse("Password is invalid", userParam.Password, HttpStatusCode.BadRequest);
              //  return BadRequest(new { message = "Password is invalid" });
                //   var user = _userService.userinfo(userParam.Username);
                //if (user.IsCompletedSuccessfully)
                //{

                var roleresult = userpass.Result.Split(";");
                var token1 = roleresult[0].ToString();
                var role = roleresult[1].ToString();
                string email, mobile, name, Class, school, logo;
                // var registration;
               
            //   var sturegistration = _userService.StudentRegistration(userParam.Username);
                if (role == "Teacher")
                {
                  var  registration = _userService.Registration(userParam.Username);
                    email = registration.Result.Email;
                    name = registration.Result.DisplayName.ToString();
                    
                    
                   Class= registration.Result.Class.ToString();
                    school = registration.Result.SchoolName.ToString();
                  //  logo = registration.Result.SchoolLogo.ToString();
                }

                else
                {
                    var registration = _userService.StudentRegistration(userParam.Username);
                    email = registration.Result.Email;
                    name = registration.Result.DisplayName.ToString();
                 //   mobile = registration.Result.FatherMobile.ToString();
                 //   Class = registration.Result.Class.ToString();
                    //if (registration.Result.School == null)
                    //{
                    //    school = "";
                    //    logo = "";
                    //}
                    //else
                    //{
                    //    school = registration.Result.School.ToString();
                    //    logo = "";
                    //}
                    
                    //   Class = _context.TblRegistration.ToList().Where(a => a.ClassIdBelouga == registration.Result.ClassIdBelouga).Select(n => n.Class).SingleOrDefault();
                    //   school=_context.TblRegistration.ToList().Where(a => a.ClassIdBelouga == registration.Result.ClassIdBelouga).Select(n => n.SchoolName).SingleOrDefault();
                    //   logo = _context.TblRegistration.ToList().Where(a => a.ClassIdBelouga == registration.Result.ClassIdBelouga).Select(n => n.SchoolLogo).SingleOrDefault();
                }
                userParam.Token = token1;
                userParam.Email = email;
                userParam.FirstName = name;
            //    userParam.FatherMobile =mobile ;
             //   userParam.Class =Class ;
                userParam.Role = role;
             //   userParam.SchoolName = school;
            //    userParam.Logo = logo;
                //  var schoolprofile = _userService.school(registration.Result.SchoolCode.ToString());
                // userParam.Role = registration.Result.Type;
                //user.Email = registration[0].FatherEmail.ToString();

                //  }
                _logger.LogInformation(LoggingEvents.ValidToken, "Token({0}) generated successfully", userpass.Result);
            }
            return CommonModel.GetResponse("Login succesfully", userParam, HttpStatusCode.OK);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Encrypt")]
        public string Encrypt(string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswords([FromBody]User userParam)
        {
            // var sessionList = await _userServic
            var userpass = await _userService.LoginCheck(userParam.Username, userParam.Password);
            if (userpass == "Username is not found")
                return BadRequest(new { message = "Username is incorrect" });
            if (userpass == "Password is invalid")
                return BadRequest(new { message = "Password is invalid" });

            using (var db = new PrincetonhiveContext())
            {
                var changepass = db.UserLogin.Where(y => y.UserName == userParam.Username).SingleOrDefault();
                if (changepass != null)
                {
                    changepass.Password = Encrypt(userParam.NewPassword);
                    db.SaveChanges();
                }
                
            }
            return CommonModel.GetResponse("Password Changed succesfully", userParam, HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginCheck")]
        public async Task<IActionResult> LoginCheck([FromBody]User userParam)
        {
            // var sessionList = await _userServic
            var userpass = await _userService.LoginCheck(userParam.Username, userParam.Password);
            if (userpass == "Username is not found")
                return BadRequest(new { message = "Username is incorrect" });
            if (userpass == "Password is invalid")
                return BadRequest(new { message = "Password is invalid" });
            return CommonModel.GetResponse("Login succesfully", userParam, HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string username)
        {
            _logger.LogInformation(LoggingEvents.Logout, "Username ({0}) have been logout on {1}", username, DateTime.UtcNow.AddHours(5.30));
            // var sessionList = await _userServic
            //var userpass = await _userService.Logout(userParam.Username);
            //if (userpass == "Username is not found")
            //    return BadRequest(new { message = "Username is incorrect" });
            //if (userpass == "Password is invalid")
            //    return BadRequest(new { message = "Password is invalid" });
            return await Task.FromResult(CommonModel.GetResponse("Logout succesfully", username, HttpStatusCode.OK));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UserName")]
        public async Task<string> UserName([FromBody]User userParam)
        {
            // var sessionList = await _userServic
            var userpass = await _userService.userinfo(userParam.Username);
            if (userpass == null)
                return "Username is incorrect";
            else
            {
                return userpass.Username;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetRegistrationInfo")]
        public async Task<IActionResult> GetRegistrationInfo(string Username)
        {
            var registrationinfo = await _userService.Registration(Username);

            if (registrationinfo == null)
                return BadRequest(new { message = "Username  is incorrect" });

            return CommonModel.GetResponse("registration", registrationinfo, HttpStatusCode.OK);


        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("Getschoolinfo")]
        //public async Task<IActionResult> Getschoolinfo(string Username)
        //{
        //    var schoolinfo = await _userService.school(Username);

        //    if (schoolinfo == null)
        //        return BadRequest(new { message = "schoolCode  is incorrect" });


        //    return CommonModel.GetResponse("schoolinfo", schoolinfo, HttpStatusCode.OK);
        //}

    }
}
