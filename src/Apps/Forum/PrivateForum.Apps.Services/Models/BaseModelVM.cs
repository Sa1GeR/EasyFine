using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.App.Web.Services.Models
{
    public class BaseModelVM
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
