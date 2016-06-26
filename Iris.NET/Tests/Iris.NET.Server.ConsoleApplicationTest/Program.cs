﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iris.NET.Server.ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).Namespace}");
            Console.Write("Press Enter to start");
            Console.ReadLine();
            Console.WriteLine("Started\n");

            IrisServer server = new IrisServer(new IrisPubSubRouter());
            server.Start(22000);

            string input = null;
            do
            {
                if (input != null)
                    Console.WriteLine($"-- Unrecognized command \"{input}\"\n");
                Console.Write("Command: ");
                input = Console.ReadLine();
            } while (input?.ToUpper() != "QUIT" && input?.ToUpper() != "Q");

            server.Stop();
            Console.Write("Terminate...");
            Console.ReadLine();
        }
    }
}
