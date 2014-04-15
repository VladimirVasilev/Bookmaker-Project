namespace BookmakerProject
{
    using System;

    public class Match
    {

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        /// <summary>
        /// A description of the football event
        /// </summary>
        /// <param name="homeTeam">The name of the home team</param>
        /// <param name="awayTeam">The name of the home team</param>
        public Match(string homeTeam, string awayTeam)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
        }


        // TODO:
    }
}
