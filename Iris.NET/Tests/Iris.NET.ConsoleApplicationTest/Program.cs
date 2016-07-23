﻿using Iris.NET.Collections;
using Iris.NET.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Iris.NET.ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataStructuresTest();
        }

        private static void DataStructuresTest()
        {
            IChannelsSubscriptionsDictionary<string> csd = new IrisChannelsSubscriptionsDictionary<string>();
            Console.WriteLine("- Empty"); Console.WriteLine(csd); Console.WriteLine("\n");

            string james = "James";
            csd.Add(james, "main");
            Console.WriteLine("- James in main"); Console.WriteLine(csd); Console.WriteLine("\n");

            csd.Add(james, new string[] { "main", "submain" });
            var adam = "Adam";
            csd.Add(adam, new string[] { "main", "submain" });
            Console.WriteLine("- James and Adam in submain"); Console.WriteLine(csd); Console.WriteLine("\n");

            var anna = "Anna";
            csd.Add(anna, "parallel");
            csd.Add(james, "parallel");
            csd.Add(adam, "parallel");
            Console.WriteLine("- James, Adam and Anna in parallel"); Console.WriteLine(csd); Console.WriteLine("\n");

            var people = csd["parallel"];
            Console.WriteLine("- People in \"parallel\""); Console.WriteLine(string.Join(",", people.ToArray())); Console.WriteLine("\n");

            var stuart = "Stuart";
            csd.Add(stuart, new string[] { "parallel", "subparallel" });
            Console.WriteLine("- Stuart in subparallel"); Console.WriteLine(csd); Console.WriteLine("\n");

            csd.Add(stuart, new string[] { "main", "submain", "extrasub" });
            csd.Add(adam, new string[] { "main", "submain", "extrasub" });
            Console.WriteLine("- Stuart and Adam in extrasub"); Console.WriteLine(csd); Console.WriteLine("\n");

            var mainSubs = csd.GetSubscriptions("main");
            Console.WriteLine("- Subscriptions to \"main\""); Console.WriteLine(string.Join(",", mainSubs.ToArray())); Console.WriteLine("\n");

            var submainFullSubs = csd.GetSubscriptions("main/submain", true);
            Console.WriteLine("- Full subscriptions to \"submain\" and its hierarchy"); Console.WriteLine(string.Join(",", submainFullSubs.ToArray())); Console.WriteLine("\n");

            var parallelFullSubs = csd.GetSubscriptions("parallel", true);
            Console.WriteLine("- Full subscriptions to \"parallel\" and its hierarchy"); Console.WriteLine(string.Join(",", parallelFullSubs.ToArray())); Console.WriteLine("\n");

            csd.RemoveChannel("main/submain");
            Console.WriteLine("- Removed main/submain"); Console.WriteLine(csd); Console.WriteLine("\n");

            csd.Add(stuart, new string[] { "main", "submain", "extrasub" });
            Console.WriteLine("- Stuart in extrasub"); Console.WriteLine(csd); Console.WriteLine("\n");

            var mainFullSubs = csd.GetSubscriptions("main", true);
            Console.WriteLine("- Full subscriptions to \"main\" and its hierarchy"); Console.WriteLine(string.Join(",", mainFullSubs.ToArray())); Console.WriteLine("\n");

            var heads = csd.GetChannelsHeads();
            Console.WriteLine("- Heads"); Console.WriteLine(string.Join(",", heads)); Console.WriteLine("\n");

            foreach (var head in heads)
            {
                var childs = csd.GetChannelsHierarchy(head);
                Console.WriteLine($"- Childs of {head}"); Console.WriteLine(string.Join(",", childs)); Console.WriteLine();
            }
            Console.WriteLine();

            Console.Write("Press ENTER to terminate...");
            Console.ReadLine();
        }
    }
}
