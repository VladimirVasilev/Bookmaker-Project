using System;
using System.Collections.Generic;
using BookmakerProject;
using BookmakerProject.IO;


namespace Testing
{
    /// <summary>
    /// This client is used just for testing functionality, it will be deleted afterwards
    /// </summary>
    class Program
    {
        static void Main()
        {

            string filePath = @"..\..\TestFile.txt";

            
            List<Game> TestList = new List<Game>()
            {
                new Football(3, Countries.France, DateTime.Now, new Match("team", "other team")),
                new Volleyball(3, Countries.France, DateTime.Now, new Match("team", "other team")),
                new Tennis(3, Countries.France, DateTime.Now, new Match("team", "other team")),
                new Football(3, Countries.France, DateTime.Now, new Match("team", "other team")),

            };

            TestList.SaveFile(filePath);

            TestList = null;

            var newList = ReadWriteFile.ReadFile(filePath);

            foreach (var item in newList)
            {
                Console.WriteLine(item);
            }

            

 

            Console.ReadLine();

        }

        private static void DisplayProgress(object sender, ProgressReportEventArgs e)
        {

            Console.WriteLine("{0}%", e.Progress);
            Console.CursorTop -= 1;



        }


    }
}
