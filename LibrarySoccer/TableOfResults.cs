using System;
using System.Collections.Generic;

namespace LibrarySoccer{
    public class TableOfResults : ITableResults
    {
        public void showResults(List<SoccerTeam> teams)
        {
            foreach (SoccerTeam team in teams)
            {
                Console.WriteLine(team);
            }
        }
    }
}