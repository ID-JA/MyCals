using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Dto
{
    public class UserRegisterDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string Gender { get; set; }


        [Required]
        public DateTime Date_Of_Birth { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }


        public string Role { get; set; }


    }
}
