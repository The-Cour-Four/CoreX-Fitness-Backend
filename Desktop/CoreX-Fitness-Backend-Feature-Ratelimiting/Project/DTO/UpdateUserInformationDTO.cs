
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTO
{
    public class UpdateUserInformationDTO
    {
        public string CurrentEmail { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty; //new password
        public string email { get; set; }   = string.Empty; //new email
        public float weight { get; set; }
        public float height { get; set; }
        public int age { get; set; }
        //public string gender { get; set; }
    }
}
