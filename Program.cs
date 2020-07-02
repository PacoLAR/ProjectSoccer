using System;

namespace ManageSoccer
{
    class Program
    {
        static void Main(string[] args)
        {
            Season season = new Season();
            season.ReadSeasonFromFile();
        }
    }
}
