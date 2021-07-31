using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Models
{
    public class UserAccount : IdentityUser<int>
    {
        public string? PictureUser { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Column(TypeName ="Date")]
        public DateTime Date_Of_Birth { get; set; }
        public double? Weight { get; set; } = 0;
        public double? Height { get; set; } = 0;
        public double? Calories_Goal { get; set; } = 0;

        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }


    }
}
