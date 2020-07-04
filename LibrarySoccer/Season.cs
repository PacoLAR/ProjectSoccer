using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LibrarySoccer{
    public class Season{
        public List<SoccerTeam> Teams{get;set;}
        public List<Game> Games {get;set;}
        public void ReadSeasonFromFile(){
            string patron = ("\\.csv$");
            Regex  nuevo = new Regex(patron);
            MatchCollection encontro = nuevo.Matches("es.1.csv");                      
            if(encontro.Count>0){            
                try{
                    string [] lines = File.ReadAllLines("es.1.csv");
                    Teams = new List<SoccerTeam>();
                    Games = new List<Game>();
                    foreach (string line in lines.Skip(1)){
                        fillSoccerTeamList(line);
                        fillGameList(line);
                        
                    }
                    clasificar();
                    //printResults();
                                        
                }catch (DirectoryNotFoundException ){
                    Console.WriteLine("No encontré el directorio");
                }catch (FileNotFoundException ){
                    Console.WriteLine("No encontré el archivo");
                }
                catch (IOException ){
                    Console.WriteLine("Error al leer el archivo");
                }              
            }else{
                Console.WriteLine("Solo archivos con formato csv");

            }
        }
        public List<Game> GetGames(string localTeam =null,string visitantTeam=null,string date = null ){
            DateTime Date;
           
            
            if((!string.IsNullOrEmpty(localTeam)&&!string.IsNullOrEmpty(visitantTeam)&&!string.IsNullOrEmpty(date))){
                Console.WriteLine("estoy aqui");
                Date = Convert.ToDateTime(date);
                return Games.FindAll(game => game.Local.Team==localTeam&&game.Visitant.Team==visitantTeam&&game.Date==Date);
            }else if((!string.IsNullOrEmpty(localTeam)&&!string.IsNullOrEmpty(visitantTeam))){
                Console.WriteLine("estoy entrando aqui");
                return Games.FindAll(game => game.Local.Team==localTeam&&game.Visitant.Team==visitantTeam);
            }else if(!string.IsNullOrEmpty(localTeam)&&!string.IsNullOrEmpty(date)){
                Date = Convert.ToDateTime(date);
                return Games.FindAll(game => game.Local.Team==localTeam&&game.Date==Date);
            }else if(!string.IsNullOrEmpty(visitantTeam)&&!string.IsNullOrEmpty(date)){
                Date = Convert.ToDateTime(date);
                return Games.FindAll(game => game.Visitant.Team==visitantTeam&&game.Date==Date);
            }else if(!string.IsNullOrEmpty(localTeam)){
                return Games.FindAll(Game=>Game.Local.Team == localTeam);
            }else if(!string.IsNullOrEmpty(visitantTeam)){
                return Games.FindAll(Game=>Game.Visitant.Team==visitantTeam);
            }else if(!string.IsNullOrEmpty(date)){
                Date = Convert.ToDateTime(date);
                return Games.FindAll(game => game.Date==Date);                
            }else{
                return Games;
            }
            
        }
        private void fillGameList(string line){
            if(line!=string.Empty){
                SoccerTeam localTeam = new SoccerTeam();
                SoccerTeam visitantTeam = new SoccerTeam();
                string []sections = line.Split(',');
                localTeam.Team = getNameOfTeam(sections[2]);
                visitantTeam.Team = getNameOfTeam(sections[5]);
                DateTime date = formatTheDate(sections[1]);
                ResultOfMatch result = determinateResult(sections[3]);
                Game game = new Game();
                game.Local = localTeam;
                game.Visitant = visitantTeam;
                game.Date = date;
                game.fullTimeResult = result;

                Games.Add(game);

            }
        }
        private void fillSoccerTeamList(string line){
            if(line!=string.Empty){
                string[] sections = line.Split(',');
                string local = getNameOfTeam(sections[2]);
                string visitant = getNameOfTeam(sections[5]);
                SoccerTeam teamLocal = new SoccerTeam();
                teamLocal.Team=local;
                SoccerTeam teamVisitant = new SoccerTeam();
                teamVisitant.Team=visitant;
                determinatePoints(sections[3],teamLocal,teamVisitant);
                
            }else{
                Console.WriteLine("El argumento esta vacio");
            }
        }
        private string getNameOfTeam(string sectionNameTeam){
            string[] formatNameTeam = sectionNameTeam.Split('(');
            string nameOfTeam = formatNameTeam[0].TrimEnd();
            return nameOfTeam;
        }

        private void determinatePoints(string sectionResult,SoccerTeam local,SoccerTeam visitant){
            
            string [] marker = sectionResult.Split('-');
            
            int goalsLocal = int.Parse(marker[0]);
            int goalsVisitant = int.Parse(marker[1]);
            determinateAddToList(local);
            determinateAddToList(visitant);
            
            SoccerTeam teamLocal = findTeam(local.Team);
            SoccerTeam teamVisitant =findTeam(visitant.Team);
            if(goalsLocal>goalsVisitant){
                teamLocal.Points+=3;
            }else if(goalsLocal<goalsVisitant){
                teamVisitant.Points+=3;
            }else{
                teamLocal.Points+=1;
                teamVisitant.Points+=1;
            } 


        }
        private ResultOfMatch determinateResult(string sectionResult){
            
            string [] marker = sectionResult.Split('-');
            
            int goalsLocal = int.Parse(marker[0]);
            int goalsVisitant = int.Parse(marker[1]);
            
            if(goalsLocal>goalsVisitant){
                return ResultOfMatch.LocalWon;
            }else if(goalsLocal<goalsVisitant){
                return ResultOfMatch.VisitantWon;
            }else{
                return ResultOfMatch.Draw;
            } 


        }
        

        private void determinateAddToList(SoccerTeam team){
            
            if(searchTeam(team)){

                
            }else{
                Teams.Add(team);
            }
        }
        private Boolean searchTeam(SoccerTeam teamSearch){
          
            if(Teams.Exists(team => team.Team == teamSearch.Team)){
                
                return true;
            }else{
                return false;
            }
        }
        private SoccerTeam findTeam(string nameOfTeam){
            SoccerTeam teamFind = Teams.Find(team => team.Team.Contains(nameOfTeam));
            if(teamFind!=null){
                return teamFind;
            }else{
                return null;
            }           
        }

        private void clasificar(){
            Teams.Sort();
            int count = Teams.Count;
            foreach (SoccerTeam team in Teams){
                team.Ranking = count;
                count--;
            }
            Teams.Reverse();
        }
        private void printResults(){
            foreach (SoccerTeam team in Teams)
            {
                Console.WriteLine(team);
            }
        }
        public static DateTime formatTheDate(string sectionDate){
                string generalPattern= "\\s*\\([A-z0-9]+\\)\\s*";
                Regex  result = new Regex(generalPattern);
                string[]formatDate = result.Split(sectionDate);               
                return Convert.ToDateTime(formatDate[1]);
        }

    }
}