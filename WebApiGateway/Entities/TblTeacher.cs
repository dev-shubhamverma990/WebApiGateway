using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblTeacher
    {
        public int TeacherId { get; set; }
        public string InsId { get; set; }
        public string TeacherName { get; set; }
        public string Mobile { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string VStatus { get; set; }
        public string TeacherImg { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? UserName { get; set; }

        public UserLogin UserNameNavigation { get; set; }
    }
}
