using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.
            // Some of the TODO's are done for you to get you started. 

            logger.LogInfo("Log initialized........");
            
            string[] lines = File.ReadAllLines(csvPath);
            
            if (lines.Length == 0)
            {
                logger.LogError("File has no input");
            }
            if (lines.Length == 1)  
            {
                logger.LogWarning("File has only one line of input");
            }
            
            logger.LogInfo($"Lines: {lines[0]}");
            var parser = new TacoParser();
            var locations = lines.Select(line => parser.Parse(line)).ToArray();
            
            ITrackable tacoBell = null;
            ITrackable tacoBell2 = null;
            double distance = 0;    

            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;
                    
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell = locA;
                        tacoBell2 = locB;
                    }   
                }
            }
            
            logger.LogInfo($"{tacoBell.Name} and {tacoBell2.Name} are the two Taco Bells that are the farthest apart from one another.");
            
        }
    }
}
