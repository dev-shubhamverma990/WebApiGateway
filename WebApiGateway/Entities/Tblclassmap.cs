using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class Tblclassmap
    {
        public int ClassmapId { get; set; }
        public string TeacheId { get; set; }
        public string ClassName { get; set; }
        public string ClassIdBelouga { get; set; }
        public string SchoolIdBelouga { get; set; }
    }
}
