using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Dto.Manager
{
    public class ManagerForDisplayDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
