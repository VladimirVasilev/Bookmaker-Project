namespace BookmakerProject
{
    using System;

    public class Football : Game
    {
        // Constructors
        public Football(decimal coef, Countries country, DateTime dateAndTime, Match match)
            : base(coef, country, dateAndTime, match)
        { }
    }
}
