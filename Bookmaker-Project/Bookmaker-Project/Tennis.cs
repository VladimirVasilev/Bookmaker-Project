namespace BookmakerProject
{
    using System;

    public class Tennis : Game
    {
        // Constructors
        public Tennis(decimal coef, Countries country, DateTime dateAndTime, Match match)
            : base(coef, country, dateAndTime, match)
        { }
    }
}
