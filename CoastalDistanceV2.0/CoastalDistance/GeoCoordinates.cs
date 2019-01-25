using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Device.Location;

//Used Newtonsoft.JSON library for JSON file reading!!

namespace CoastalDistance
{
    class GeoCoordinates
    {

        public static Coordinates GetCoordinates(Coordinates coordinates)
        {
            //Initiating the default values
            double minDistance = 9999999;
            double latCoordinate = 0;
            double longCoordinate = 0;

            //Setting up an object for new Coordinates that are to be sent to the user
            Coordinates newCoordinates = new Coordinates();

            //Searching for Coastline coordinates in files
            for (int j = 1; j < 6; j++)
            {
                string filename = @"C:\Users\sbisht\source\repos\CoastalDistanceV2.0\Resources\SouthEast\Part" + j + ".json";
                using (StreamReader file = File.OpenText(filename))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject obj = JObject.Load(reader);
                    dynamic data = JObject.Parse(obj.ToString());
                    for (int i = 0; i < data.Coordinates.Count - 1; i = i + 2)
                    {
                        longCoordinate = (double)(data.Coordinates[i + 1]);
                        latCoordinate = (double)(data.Coordinates[i]);
                        latCoordinate = Math.Round(latCoordinate, 7);
                        longCoordinate = Math.Round(longCoordinate, 7);

                        //Getting the distance of the input Coordinates from the current coordinates
                        double currDistance = GetDistance(coordinates, latCoordinate, longCoordinate);

                        //Checking for the minimum distance
                        if (minDistance > currDistance)
                        {
                            minDistance = currDistance;
                            newCoordinates.Latitude = latCoordinate;
                            newCoordinates.Longitude = longCoordinate;
                        }
                    }
                }
            }
            newCoordinates.Distance = minDistance;
            return newCoordinates;
        }

        public static double GetDistance(Coordinates coordinates, double latCoordinate, double longCoordinate)
        {
            double currDistance;
            GeoCoordinate startCoordinate = new GeoCoordinate();
            GeoCoordinate endCoordinate = new GeoCoordinate();

            startCoordinate.Latitude = coordinates.Latitude;
            startCoordinate.Longitude = coordinates.Longitude;

            endCoordinate.Latitude = latCoordinate;
            endCoordinate.Longitude = longCoordinate;
            currDistance = startCoordinate.GetDistanceTo(endCoordinate);
            return currDistance;
        }

    }
}
