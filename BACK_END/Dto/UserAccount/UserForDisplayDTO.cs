using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Dto.UserAccount
{
    public class UserForDisplayDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
        public string Role { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public double? Calories_Goal { get; set; }
    }
}
