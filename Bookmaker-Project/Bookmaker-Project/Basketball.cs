using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerProject
{
    public class Basketball : Game
    {
        // Constructors
        public Basketball(decimal coef, Countries country, DateTime dateAndTime, Match match)
            : base(coef, country, dateAndTime, match)
        { }
    }
}
