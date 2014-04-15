namespace BookmakerProject
{
    using System;

    public class Volleyball : Game
    {
        // Constructors
        public Volleyball(decimal coef, Countries country, DateTime dateAndTime, Match match)
            : base(coef, country, dateAndTime, match)
        { }
    }
}
