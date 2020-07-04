using System.Collections.Generic;
using System.IO;

namespace LibrarySoccer{
    public class TableOfResultsFile : ITableResults
    {
        public void showResults(List<SoccerTeam> teams)
        {
            List<string> valuesInTheTable = new List<string>();
            string values = "Equipo,Puntos,Clasificacion";
            valuesInTheTable.Add(values);

            foreach (SoccerTeam team in teams){
                
                string lineOfTeam = ($"{team.Team},{team.Points},{team.Ranking}");
                valuesInTheTable.Add(lineOfTeam);                
            }

            File.WriteAllLines("results.csv",valuesInTheTable);
        }
    }
}