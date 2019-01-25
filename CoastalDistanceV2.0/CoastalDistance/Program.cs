using System;

//31.0984679,-81.385627  ============Provided Ones

//Points:
//32.5603881,-80.1679822
//31.0659465,-81.4059198
//31.2007044,-81.3285527
//30.6536353,-81.4366219
//30.6224202,-81.4534372


namespace CoastalDistance
{
    class Program : GeoCoordinates
    {
        static void Main(string[] args)
        {
            //Starts the program's initiating method
            Start();
        }
        static void Start()
        {
            //Assigning input values from where the nearest Coastline value is to be found
            String inputCoordinates = ("32.5603881,-80.1679822");
            string[] arr = inputCoordinates.Split(',');
            Coordinates coordinates = new Coordinates
            {
                Latitude = Double.Parse(arr[0]),
                Longitude = Double.Parse(arr[1])
            };

            //Setting up the UI
            Console.WriteLine("Checking for current Coordinates:(" + coordinates.Latitude + "," + coordinates.Longitude + ")\n\n");

            //Getting the distance and Coordinates
            Coordinates newCoordinates = GetCoordinates(coordinates);
            ///GenerateJSON generateJSON = new GenerateJSON();

            //Displaying the values in the UI
            Console.WriteLine("Nearest Coastline at - (" + newCoordinates.Latitude + "," + newCoordinates.Longitude + ") \n\n");
            Console.WriteLine("Distance: " + Math.Round(newCoordinates.Distance,3) + "(mitres) OR "+ Math.Round(newCoordinates.Distance * 3.281, 3) + "(feet)");
            
            //GetElevation(newCoordinates.Latitude,newCoordinates.Longitude);
            Console.ReadKey();
        }
    }
}
