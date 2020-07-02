using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ManageSoccer{
    public class Season{
        public List<SoccerTeam> Teams{get;set;}
        public void ReadSeasonFromFile(){
            string patron = ("\\.csv$");
            Regex  nuevo = new Regex(patron);
            MatchCollection encontro = nuevo.Matches("es.1.csv");                      
            if(encontro.Count>0){            
                try{
                    string [] lines = File.ReadAllLines("es.1.csv");
                    Teams = new List<SoccerTeam>();
                    foreach (string line in lines.Skip(1)){
                        fillSoccerTeamList(line);                        
                    }
                    clasificar();
                    printResults();
                                        
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
            SoccerTeam team = Teams.Find(team => team.Team.Contains(nameOfTeam));
            if(team!=null){
                return team;
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

    }
}