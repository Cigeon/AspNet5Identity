using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet5Identity.WEB.Models
{
    public class UsersModel
    {
        public string Search { get; set; }
        public string Sort { get; set; }
        public List<DetailModel> Users { get; set; }
    }
}