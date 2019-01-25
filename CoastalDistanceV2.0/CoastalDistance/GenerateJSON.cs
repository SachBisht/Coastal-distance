using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CoastalDistance
{
    class GenerateJSON
    {
        double latCoordinate = 0;
        double longCoordinate = 0;
        public GenerateJSON()
        {
            Model coordinate = new Model();

            List<string> subCoordinates = new List<string>();
            var subSubCoordinates = new List<List<string>>();


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

                        double elevation = GetElevation(latCoordinate, longCoordinate);
                        if (elevation < 0 || elevation == 0)
                        {
                            Console.WriteLine("Elevation added=" + elevation);
                            subCoordinates.Add(latCoordinate.ToString());
                            subCoordinates.Add(longCoordinate.ToString());
                        }

                    }
                }
                subSubCoordinates.Add(subCoordinates);
                coordinate.Coordinates = subSubCoordinates;
                BinaryFormatter formatter = new BinaryFormatter();
                StreamWriter sw = new StreamWriter(@"D:\path" +j+ ".json");
                JsonSerializer jsonSerializer = JsonSerializer.Create();
                jsonSerializer.Serialize(sw, coordinate.Coordinates);
                sw.Close();
            }
                

            }

            //Getting the elevation
            public double GetElevation(double latitude, double longitude)
            {
                string responsedata = "";
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = "https://maps.googleapis.com/maps/api/elevation/json?locations=" + latitude + "," + longitude + "&key=AIzaSyCx6Ib86qZr-xoyCaeVqoQoII_GYiy7pI0";

                    responsedata = wc.UploadString(url, "");
                }
                dynamic json = JObject.Parse(responsedata.ToString());
                return json.results[0].elevation;

            }

        }
    }
