using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGateway.Entities
{
    public class SchoolDetailsBelouga
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state_id { get; set; }
        public string district_id { get; set; }
        public string postal_code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string registration_code { get; set; }
        public string status_id { get; set; }
        public string archived { get; set; }
        public string archive_reasons_id { get; set; }
        public string created { get; set; }
        public string created_by { get; set; }
        public string modified { get; set; }
        public string modified_by { get; set; }
    }

    public class SchoolDetailsBelougaViewModal
    {
        public List<SchoolDetailsBelouga> data { set; get; }
        public SchoolDetailsBelougaViewModal()
        {
            data = new List<SchoolDetailsBelouga>();
        }
    }
}
