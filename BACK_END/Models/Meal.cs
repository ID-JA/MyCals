using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Models
{
    public class Meal
    {
        [Key]
        public int Id_Meal { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "Varchar(1000)")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public float Calories { get; set; }

        public int Id_User { get; set; }

        [ForeignKey("Id_User")]
        public UserAccount userAccount { get; set; }

    }
}
