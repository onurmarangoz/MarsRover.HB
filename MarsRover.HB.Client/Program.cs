using MarsRover.HB.App.Services;
using MarsRover.HB.App.Services.Interfaces;
using MarsRover.HB.Client.Services;
using MarsRover.HB.Client.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover.HB
{
    public class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            serviceProvider = new ServiceCollection()
                                           .AddTransient<IRoverClientService, RoverClientService>()
                                           .AddTransient<IRoverService, RoverService>()
                                           .BuildServiceProvider();

           serviceProvider.GetService<IRoverClientService>().RoverStartup();
        }
    }
}


