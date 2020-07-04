using System;

namespace LibrarySoccer{
    public class Game{
        public DateTime Date{get;set;}
        public SoccerTeam Local{get;set;}
        public SoccerTeam Visitant{get;set;}
        public ResultOfMatch halfTimeResult{get;set;}
        public ResultOfMatch fullTimeResult{get;set;}

        public override string ToString(){
            return $"Local: {Local.Team}, Visitant: {Visitant.Team}, Date: {Date}, Result: {fullTimeResult}";
        }
    }
}