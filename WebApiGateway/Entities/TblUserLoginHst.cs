using System;
using System.Collections.Generic;

namespace WebApiGateway.Entities
{
    public partial class TblUserLoginHst
    {
        public DateTime LoginDateTime { get; set; }
        public DateTime LogoutDateTime { get; set; }
        public int UserName { get; set; }
        public int UserLoginHstid { get; set; }
    }
}
