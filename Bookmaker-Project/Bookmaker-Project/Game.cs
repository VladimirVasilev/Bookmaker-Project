namespace BookmakerProject
{
    using System;

    public abstract class Game
    {
        // Fields
        private decimal coef;
        private Countries country;
        private DateTime dateAndTime;
        private Match match;

        // Constructors
        public Game(decimal coef, Countries country, DateTime dateAndTime, Match match)
        {
            this.Coef = coef;
            this.Country = country;
            this.DateAndTime = dateAndTime;
            this.Match = match;
        }

        // Properties
        public decimal Coef
        {
            get { return this.coef; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Coefficient must have non-negative value!");
                }
                else
                {
                    this.coef = value;
                }
            }
        }

        public Countries Country
        {
            get { return this.country; }

            set
            {
                if (!Enum.IsDefined(typeof(Countries), value)) //This returns an indication whether a constant with a specified value exists in a specified enumeration
                {
                    throw new ArgumentException("The given value doesn't exist in a optional choices");
                }
                else
                {
                    this.country = value;
                }
            }
        }

        public DateTime DateAndTime
        {
            get { return this.dateAndTime; }
            set 
            {
                // If the match is more than 90 minutes before now assume it's already over
                if (value < DateTime.Now.AddMinutes(-90))
                {
                    throw new ArgumentException("The date of the game is before the current date and time");
                }
                this.dateAndTime = value;
            }
        }

        public string SportName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public Match Match
        {
            get { return this.match; }
            set { this.match = value; }
        }
    }
}
