using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;
using WebApiGateway.Entities;
using Paseto;
using Paseto.Builder;
using Paseto.Protocol;
using System.Threading.Tasks;
using WebApiGateway;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using WebApiGateway.Helper;
using System.Net;
using System.Security.Cryptography;
using System.IO;

namespace WebApi.Services
{
    public interface IUserService<T>
    {
        //  User Authenticate(string username, string password, string role);
        Task<string> Authenticate_Paseto(string username, string password);
        List<TblBelougaRegistrationForm> GetBelougaRegistrationTeacher(int ID);
        List<StudentRegistration> GetBelougaRegistrationStudent(int ID);
        List<Tblclassmap> GetBelougaRegistrationClass(int ID);
        Task<string> BelougaToken();
        Task<string> Registration_Teacher_Paseto(int ID,List<TblBelougaRegistrationForm> tblBelouga);
        Task<string> schoolcheck(string tokenschool, int schoolstatecode, string schoolpostalcode, string schoolname);
        Task<string> Registration_Class_Paseto(int ID, string schoolid, string classname, string desc, string teacharid);
        Task<string> Registration_Student_Paseto(int ID, string firstname, string lastname, string username, string dob, string classid);
        // Task<TblSchool> schoolinfo(string username);
        Task<string> LoginCheck(string username, string password);
        Task<TblRegistration> Registration(string username);
        Task<TblStudentRegistration> StudentRegistration(string username);
        Task<T> userinfo(string username);
        //Task<TblSchool> school(string user);
     
    }

    public class UserService : IUserService<User>
    {
        private readonly AppSettings _appSettings;
        private readonly PrincetonhiveContext _context;
        private readonly HttpClient _httpClient;
        public List<loginUser> Register { get; set; } = new List<loginUser>();
        public UserService(PrincetonhiveContext context)
        {
          //  _appSettings = appSettings.Value;
            _context = context;
            _httpClient = new HttpClient();
        }

        private List<TblBelougaRegistrationForm> TEACHER;
       

        Dictionary<string, object> teacherdictonary1 = new Dictionary<string, object>();
        public IDictionary<string, object> teacherdictonary()
        {

            teacherdictonary1.Add("type", TEACHER[0].Type);
            teacherdictonary1.Add("new-user-type", 10);
            teacherdictonary1.Add("new-user-state", TEACHER[0].Newuserstate);
            teacherdictonary1.Add("new-user-country", TEACHER[0].Newusercountry);
            teacherdictonary1.Add("teacher-is-district", true);
            if (TEACHER[0].SchoolIdBelouga == "" || TEACHER[0].SchoolIdBelouga == null)
            {
                teacherdictonary1.Add("new-user-school-name", TEACHER[0].Newuserschoolname);
                teacherdictonary1.Add("teacher-is-existing-school", false);
                teacherdictonary1.Add("new-user-address", TEACHER[0].Newuseraddress);
                teacherdictonary1.Add("new-user-city", TEACHER[0].Newusercity);
                teacherdictonary1.Add("new-user-postal-code", TEACHER[0].Newuserpostalcode);
            }
            else
            {
                teacherdictonary1.Add("new-user-school-name", TEACHER[0].SchoolIdBelouga);
                teacherdictonary1.Add("teacher-is-existing-school", true);
            }
            
            teacherdictonary1.Add("teacher-first-name", TEACHER[0].Teacherfirstname);
            teacherdictonary1.Add("teacher-last-name", TEACHER[0].Teacherlastname);
            teacherdictonary1.Add("teacher-displayname", TEACHER[0].Teacherdisplayname);
            teacherdictonary1.Add("teacher-username", TEACHER[0].Teacherusername);
            teacherdictonary1.Add("teacher-email", TEACHER[0].Teacheremail);
            teacherdictonary1.Add("teacher-password", TEACHER[0].Teacherpassword);
            teacherdictonary1.Add("teacher-confirm-password", TEACHER[0].Teacherconfirmpassword);
            teacherdictonary1.Add("teacher-accept-tos", true);
            return teacherdictonary1;

        }

        private List<ClassRegistration> Class;

        Dictionary<string, object> Classdictonary1 = new Dictionary<string, object>();
        public IDictionary<string, object> Classdictonary(string schoolid, string classname, string desc, string teacharid)
        {

            Classdictonary1.Add("school_id", schoolid);
            Classdictonary1.Add("name", classname);
            Classdictonary1.Add("description", desc);
            Classdictonary1.Add("classroom_verification_image_one", 1);
            Classdictonary1.Add("classroom_verification_image_two", 1);
            Classdictonary1.Add("teacher_id", teacharid);
          
            return Classdictonary1;

        }

        private List<StudentRegistration> Student;

        Dictionary<string, object> Studentdictonary1 = new Dictionary<string, object>();
        public IDictionary<string, object> Studentdictonary(string firstname, string lastname, string username, string dob, string classid)
        {

            Studentdictonary1.Add("type", "Student");
            Studentdictonary1.Add("student-first-name", firstname);
            Studentdictonary1.Add("student-last-name", lastname);
            Studentdictonary1.Add("student-username", username);
            Studentdictonary1.Add("student-password", "Princetonhive@123");
            Studentdictonary1.Add("student-confirm-password", "Princetonhive@123");
            //Studentdictonary1.Add("student-birthday", dob);
            //if (classid != "" || classid != null)
            //{
            //    Studentdictonary1.Add("classroomId", classid);
            //}
            
            return Studentdictonary1;

        }
        
        public async Task<string> schoolcheck(string tokenschool, int schoolstatecode,string schoolpostalcode,string schoolname)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenschool);
            var response = await _httpClient.GetAsync("https://belouga.org/api/getSchoolsByStateID/" + schoolstatecode);
            var responseData = response.Content.ReadAsStringAsync().Result;
            var result2 = JsonConvert.DeserializeObject<SchoolDetailsBelougaViewModal>(responseData);
            //  SchoolDetails = result2.data.ToList();
            var resultdetails = result2.data.ToList();
           
                   var newList = resultdetails.Where(a => a.postal_code == schoolpostalcode).Select(n => n.id).SingleOrDefault();
         
            return await Task.FromResult(newList);
        }

        public List<TblBelougaRegistrationForm> GetBelougaRegistrationTeacher(int ID)
        {  
            var tblbelougaregistration = (from a in _context.TblRegistration

                                          where a.RegistrationId == ID
                                          select new TblBelougaRegistrationForm
                                          {
                                              Type = "Teacher",
                                              Newusertype = 10,
                                              Newusercity=a.City,
                                              Newuserstate = a.State,
                                              Newusercountry = a.Country,
                                              Teacherisexistingschool = false,
                                              Teacherisdistrict = false,
                                              Newuserschoolname = a.SchoolName,
                                              Newuseraddress = a.Address, 
                                              Newuserpostalcode = a.PostalCode,
                                              Teacherfirstname = a.FirstName,
                                              Teacherlastname = a.LastName,
                                              Teacherdisplayname = a.DisplayName,
                                              Teacherusername = a.Username,
                                              Teacheremail = a.Email,
                                              Teacherpassword = "Princetonhive@123",
                                              Teacherconfirmpassword = "Princetonhive@123",
                                              SchoolIdBelouga=a.SchoolIdBelouga,
                                              Teacheraccepttos = true,
                                              Class=a.Class,
                                              TeacherIdBelouga=a.TeacherIdBelouga,
                                              
                                              
                                          }).ToList();
          //  TEACHER = tblbelougaregistration; 
            return tblbelougaregistration;

        }
        
        public List<StudentRegistration> GetBelougaRegistrationStudent(int ID)
        {
            var tblbelougaregistrationStudent = (from a in _context.TblStudentRegistration
                                               where a.StudentRegistrationId == ID
                                               select new StudentRegistration
                                               {
                                                  
                                                   Type = a.Type,
                                                   FirstName = a.FirstName,
                                                   LastName = a.LastName,
                                                   Username = a.Username,
                                                   password = "Princetonhive@123",
                                                   confirmpassword= "Princetonhive@123",
                                                //   Dob="",
                                                   Class=""


                                               }).ToList();
           
            return tblbelougaregistrationStudent;

        }

        public List<Tblclassmap> GetBelougaRegistrationClass(int ID)
        {
            var tblbelougaregistrationClass = (from a in _context.Tblclassmap
                                               join b in _context.TblRegistration on a.TeacheId equals b.TeacherIdBelouga
                                                 where a.ClassmapId == ID
                                                 select new Tblclassmap
                                                 {

                                                     TeacheId = a.TeacheId,
                                                     ClassName = a.ClassName,
                                                     SchoolIdBelouga = b.SchoolIdBelouga,
                                                    
                                                 }).ToList();

            return tblbelougaregistrationClass;

        }

        Dictionary<string, object> userlogin1 = new Dictionary<string, object>();
        public IDictionary<string, object> userlogin(string username, string password)
        {

            userlogin1.Add("username", username);
            if(username=="yogesh123")
            {
                userlogin1.Add("password", "password");
            }
            else if(username=="parassingh")
            {
                userlogin1.Add("password", "password");
            }
            else
            {
                userlogin1.Add("password", "Princetonhive@123");
            }
            

            return userlogin1;

        }

        public async Task<string> BelougaToken()
        {
            var token = new PasetoBuilder<Version2>()
            .WithKey(Encoding.UTF8.GetBytes("RI1wBFCma8HqmSzsF8sftk26PlLBLtdG"))
           .AddClaim("aud", "production")
            .AddClaim("iss", "vidya")
            .AddClaim("jti", "belouga_api")
            .AddClaim("user", "")

            .Expiration(DateTime.UtcNow.AddHours(24))
            .AsLocal()

            .Build();

            return await Task.FromResult(token);
        }
        
        public async Task<string> Registration_Teacher_Paseto(int ID,List<TblBelougaRegistrationForm> tblBelouga)
        {
            TEACHER = tblBelouga;
            teacherdictonary();

            var token = new PasetoBuilder<Version2>()

            .WithKey(Encoding.UTF8.GetBytes("RI1wBFCma8HqmSzsF8sftk26PlLBLtdG"))
           .AddClaim("aud", "production")
            .AddClaim("iss", "vidya")
            .AddClaim("jti", "belouga_api")
            .AddClaim("user", teacherdictonary1)

            .Expiration(DateTime.UtcNow.AddHours(24))
            .AsLocal()
           
            .Build();

            return await Task.FromResult(token);
        }

        public async Task<string> Registration_Class_Paseto(int id,string schoolid, string classname, string desc, string teacharid)
        {
            Classdictonary( schoolid,  classname,  desc,  teacharid);

            var token = new PasetoBuilder<Version2>()

            .WithKey(Encoding.UTF8.GetBytes("RI1wBFCma8HqmSzsF8sftk26PlLBLtdG"))
           .AddClaim("aud", "production")
            .AddClaim("iss", "vidya")
            .AddClaim("jti", "belouga_api")
          
            .AddClaim("classroom", Classdictonary1)

            .Expiration(DateTime.UtcNow.AddHours(24))
            .AsLocal()
            
            .Build();

            return await Task.FromResult(token);
        }

       
        public async Task<string> Registration_Student_Paseto(int ID, string firstname, string lastname, string username, string dob, string classid)
        {
            Studentdictonary(firstname, lastname, username, dob, classid);
            var token = new PasetoBuilder<Version2>()

            .WithKey(Encoding.UTF8.GetBytes("RI1wBFCma8HqmSzsF8sftk26PlLBLtdG"))
           .AddClaim("aud", "production")
            .AddClaim("iss", "vidya")
            .AddClaim("jti", "belouga_api")
          
            .AddClaim("user", Studentdictonary1)

            .Expiration(DateTime.UtcNow.AddHours(24))
            .AsLocal()
           
            .Build();

            return await Task.FromResult(token);
        }
        
        public string Encrypt(string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Decrypt(string value)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public async Task<string> Authenticate_Paseto(string username, string password)
        {
            var user = (from a in _context.UserLogin
                        where a.UserName == username
                        select a.UserName.ToString());
            if (user.ToList().Count == 0)
                return "Username is not found";
            var pass = (from a in _context.UserLogin
                        where a.UserName == username && a.Password == Encrypt(password)
                        select a.Password.ToString());
          
            if (pass.ToList().Count == 0)
                return "Password is invalid";
        
            var role = (from a in _context.UserLogin
                        where a.UserName == username && a.Password == Encrypt(password)
                        select a.UserRole.ToString());
            userlogin(username, password);
           
            var token = new PasetoBuilder<Version2>()

            .WithKey(Encoding.UTF8.GetBytes("RI1wBFCma8HqmSzsF8sftk26PlLBLtdG"))
           .AddClaim("aud", "production")
            .AddClaim("iss", "vidya")
            .AddClaim("jti", "belouga_api")
           
            .AddClaim("user", userlogin1)

            .Expiration(DateTime.UtcNow.AddHours(24))
            .AsLocal()
            
            .Build();

            return await Task.FromResult(token + ";" + role.SingleOrDefault());
        }


        public async Task<string> LoginCheck(string username, string password)
        {
            var user = (from a in _context.UserLogin
                        where a.UserName == username
                        select a.UserName).FirstOrDefault();
            if (user == null)
                return "Username is not found";
            var pass = (from a in _context.UserLogin
                        where a.UserName == username && a.Password == Encrypt(password)
                        select a.Password).FirstOrDefault();

            if (pass == null)
                return "Password is invalid";
            userlogin(username, password);
            
            return await Task.FromResult(user);
        }
        
       
        public async Task<TblRegistration> Registration(string username)
        {
            var DisplayName = (from a in _context.TblRegistration
                               where a.Username == username
                               select new TblRegistration {FirstName=a.FirstName, DisplayName = a.DisplayName, Class = a.Class,Type=a.Type,Email=a.Email,SchoolName=a.SchoolName,SchoolLogo=a.SchoolLogo}).SingleOrDefault();

            return await Task.FromResult(DisplayName);
        }

        public async Task<TblStudentRegistration> StudentRegistration(string username)
        {
            var DisplayName1 = (from a in _context.TblStudentRegistration
                               
                                where a.Username == username
                                select new TblStudentRegistration { DisplayName = a.DisplayName, FatherEmail = a.FatherEmail, FatherMobile = a.FatherMobile, SchoolCode = a.SchoolCode, Type = a.Type, Email = a.Email }).SingleOrDefault();

            return await Task.FromResult(DisplayName1);
        }

        public async Task<User> userinfo(string username)
        {
            var _user = (from a in _context.UserLogin
                         where a.UserName == username
                         select new User { Username = a.UserName, Role = a.UserRole, FirstName = a.Name, Token = a.Captcha,Password= Decrypt(a.Password) }).SingleOrDefault();

            return await Task.FromResult(_user);
        }

    }

    public class loginUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset logindate { get; set; }
        public DateTimeOffset logoutdate { get; set; }
    }
}