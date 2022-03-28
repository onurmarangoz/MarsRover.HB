using MarsRover.HB.App.Services.Interfaces;
using MarsRover.HB.Client.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace MarsRover.HB.Client.Services
{
    public class RoverClientService : IRoverClientService
    {
        private readonly IRoverService roverService;
        public RoverClientService(IRoverService roverService)
        {
            this.roverService = roverService;
        }

        public void RoverStartup()
        {
            List<string> result = new();

            try
            {
                if (Info(out result))
                {
                    var pleteauString = Console.ReadLine();    //first line
                    var roverA = Console.ReadLine();           //second  line
                    var roverAMapPath = Console.ReadLine();    //third  line
                    var roverB = Console.ReadLine();           //fourth  line
                    var roverBMapPath = Console.ReadLine();    //fifith  line

                    result = this.roverService.RoverProcess(pleteauString, roverA, roverAMapPath, roverB, roverBMapPath);
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }

            #region Results

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();

            #endregion
        }


        public bool Info(out List<string> result)
        {
            Console.WriteLine("Mars Keşif Programına Hoşgeldiniz!");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1 - Dataları kendim girmek istiyorum");
            Console.WriteLine("2 - Default değerlerle işlem yapmak istiyorum");
            result = new List<string>();

            if (Console.ReadLine() == "2")
                result =  this.roverService.RoverProcess("5 5", "1 2 N", "LMLMLMLMM", "3 3 E", "MMRMMRMRRM");
            else
                return true;

            return false;

        }

    }
}
