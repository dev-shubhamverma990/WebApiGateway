using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGateway.Entities
{
    public class ClassRegistration
    {
        public int Classid { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SchoolId { get; set; }
        public string Description { get; set; }
        public string TeachetId { get; set; }
        public int classroom_verification_image_one { get; set; }
        public int classroom_verification_image_two { get; set; }

    }
}
