using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Dto.Meals
{
    public class MealForDisplayDTO
    {
        public int Id_User { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Calories { get; set; }
    }
}
