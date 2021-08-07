using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Dto.Review
{
    public class ReviewForAddDTO
    {
        public int Id_User { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int Nbr_Stars { get; set; }

    }
}
