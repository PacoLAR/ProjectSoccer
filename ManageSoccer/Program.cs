using System;
using System.Collections.Generic;
using LibrarySoccer;

namespace ManageSoccer
{
    class Program
    {
        static void Main(string[] args)
        {
            Season season = new Season();
            season.ReadSeasonFromFile();
            List<Game> games = season.GetGames(date:"8 Mar 2020");           
            foreach (Game game in games)
            {
                Console.WriteLine(game);
            }            
        }
    }
}
