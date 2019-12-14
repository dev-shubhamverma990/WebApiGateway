using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblStateMaster
    {
        public string StateName { get; set; }
        public int Countryid { get; set; }
        public int Stateid { get; set; }
    }
}
