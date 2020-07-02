using System;
namespace ManageSoccer{
    public class SoccerTeam :IComparable<SoccerTeam>{
        public string Team{get;set;}
        public int Points{get;set;}
        public int Ranking{get;set;}

         public int CompareTo(SoccerTeam other)
        {
            return Points.CompareTo(other.Points);
        }

       

        public override string ToString(){
            return $"Team: {Team} Points: {Points} Ranking {Ranking}";
        }
    }

}