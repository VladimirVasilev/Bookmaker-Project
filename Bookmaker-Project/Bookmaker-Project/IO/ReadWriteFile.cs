using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace BookmakerProject.IO
{
    public static class ReadWriteFile
    {
        // TODO: Implement Read-Write File

        public static double CurentOperationProgress { get; private set; }
        public static string CurrentFile { get; private set; }

        public static List<Game> ReadFile(string filePath)
        {
            // TODO: Replace this with List<Game> when we're done editing Game class
            List<Game> result = new List<Game>();

            CurrentFile = filePath;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string nextLine = reader.ReadLine();

                    #region Report Progress

                    CurentOperationProgress = Math.Round(100d * reader.BaseStream.Position / reader.BaseStream.Length, 5);
                    if (CurentOperationProgress > 1 && CurentOperationProgress % 1 == 0)
                    {
                        OnRaiseCustomEvent(new ProgressReportEventArgs((byte)CurentOperationProgress));
                    }

                    #endregion

                    string[] arguments = nextLine.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                    Game nextGame = GameFactory.CreateGame
                        (
                            type: arguments[0],
                            coef: decimal.Parse(arguments[1]),
                            country: (Countries)Enum.Parse(typeof(Countries), arguments[2]),
                            dateAndTime: Convert.ToDateTime(arguments[3]),
                            match: new Match(arguments[4], arguments[5])
                        );

                    result.Add(nextGame);
                }
            }

            return result;

        }

        public static void SaveFile(this IEnumerable<Game> gameList, string filePath)
        {

            CurrentFile = filePath;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                StringBuilder text = new StringBuilder();
                foreach (var item in gameList)
                {
                    if (item.Match.HomeTeam == null || item.Match.HomeTeam.Length < 1)
                    {
                        throw new ArgumentException("The name of the teams is empty", "HomeTeam");
                    }
                    if (item.Match.AwayTeam == null || item.Match.AwayTeam.Length < 1)
                    {
                        throw new ArgumentException("The name of the teams is empty", "AwayTeam");
                    }
                    text.AppendFormat("{0}; {1}; {2}; {3}; {4}; {5} \r\n", item.GetType().Name, item.Coef, item.Country.ToString(), item.DateAndTime, item.Match.HomeTeam, item.Match.AwayTeam);
                }

                writer.Write(text);
            }
        }

        public static Task<List<Game>> ReadFileAsync(string filePath)
        {
            return Task.Run(() => ReadFile(filePath));
        }

        public static event EventHandler<ProgressReportEventArgs> DisplayProgressEvent;

        private static void OnRaiseCustomEvent(ProgressReportEventArgs e)
        {
            EventHandler<ProgressReportEventArgs> handler = DisplayProgressEvent;

            if (handler != null)
            {
                handler(null, e);
            }
        }
    }
}
