using System.Collections.Generic;

namespace MarsRover.HB.App.Services.Interfaces
{
    public interface IRoverService
    {
        List<string> RoverProcess(string plateauString, string roverAString, string roverAMapPath, string roverBString, string roverBMapPath);
    }
}
