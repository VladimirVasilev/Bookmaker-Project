using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerProject
{
    public static class GameFactory
    {

        public static Game CreateGame(GameType type, decimal coef, Countries country, DateTime dateAndTime, Match match)
        {
            return CreateGame(type.ToString(), coef, country, dateAndTime, match);
        }

        public static Game CreateGame(string type, decimal coef, Countries country, DateTime dateAndTime, Match match)
        {
            Game game;
            switch (type.ToLower().Trim())
            {
                case "football":
                    game = new Football(coef, country, dateAndTime, match);
                    break;
                case "basketball":
                    game = new Basketball(coef, country, dateAndTime, match);
                    break;
                case "volleyball":
                    game = new Volleyball(coef, country, dateAndTime, match);
                    break;
                case "tennis":
                    game = new Tennis(coef, country, dateAndTime, match);
                    break;               
                case "handball":
                    game = new Handball(coef, country, dateAndTime, match);
                    break;    
                default:
                    throw new ArgumentException("Invalid sport defined in the file");
            }

            return game;
        }
    }
}
