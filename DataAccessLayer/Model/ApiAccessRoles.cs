using System;
using System.Collections.Generic;

namespace DataAccessLayer.Model
{
    public partial class ApiAccessRoles
    {
        public int Id { get; set; }
        public string ApiName { get; set; }
        public string ControllerName { get; set; }
        public string RolesAccess { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }
    }
}
