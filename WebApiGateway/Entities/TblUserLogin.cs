using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblUserLogin
    {
        public TblUserLogin()
        {
           
            TblStudent = new HashSet<TblStudent>();
            TblTeacher = new HashSet<TblTeacher>();
            TblUserLoginHst = new HashSet<TblUserLoginHst>();
        }

        public int UserLoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Captcha { get; set; }
        public int? UserRole { get; set; }

      
        public ICollection<TblStudent> TblStudent { get; set; }
        public ICollection<TblTeacher> TblTeacher { get; set; }
        public ICollection<TblUserLoginHst> TblUserLoginHst { get; set; }
    }
}
