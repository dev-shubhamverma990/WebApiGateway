using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblBelougaRegistrationForm
    {
       
        public int Newusertype { get; set; }
        public int Newusercountry { get; set; }
        public int Newuserstate { get; set; }
        public bool Teacherisexistingschool { get; set; }
        public bool Teacherisdistrict { get; set; }
        public string Type { get; set; }
        public string Newuserschoolname { get; set; }
        public string Newuserschoolpostalcode { get; set; }
        public string Newuseraddress { get; set; }
        public string Newusercity { get; set; }
        public string Newuserpostalcode { get; set; }
        public string Teacherfirstname { get; set; }
        public string Teacherlastname { get; set; }
        public string Teacherdisplayname { get; set; }
        public string Teacherusername { get; set; }
        public string Teacheremail { get; set; }
        public string Teacherpassword { get; set; }
        public string Teacherconfirmpassword { get; set; }
        public bool Teacheraccepttos { get; set; }
        public string SchoolIdBelouga { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLogo { get; set; }
        public string Class { get; set; }
        public string TeacherIdBelouga { get; set; }
    }
}
