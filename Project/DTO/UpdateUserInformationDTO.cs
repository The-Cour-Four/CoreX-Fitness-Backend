
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DTO
{
    public class UpdateUserInformationDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public float weight { get; set; }
        public float height { get; set; }
        public int age { get; set; }
        //public string gender { get; set; }
    }
}
