using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblStudent
    {
        public int StudentId { get; set; }
        public string TeacherId { get; set; }
        public string InsId { get; set; }
        public string Class { get; set; }
        public string EnrollNo { get; set; }
        public string StudentName { get; set; }
        public string Section { get; set; }
        public string Stream { get; set; }
        public string Subjects { get; set; }
        public string GaurdName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string VStatus { get; set; }
        public string StudentImg { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? UserName { get; set; }

        public UserLogin UserNameNavigation { get; set; }
    }
}
