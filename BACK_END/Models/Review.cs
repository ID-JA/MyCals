using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string Title { get; set; }

        public string Text { get; set; }

        public int Nbr_Stars { get; set; }

        public int Id_User { get; set; }

        [ForeignKey("Id_User")]
        public UserAccount userAccount { get; set; }
    }
}
