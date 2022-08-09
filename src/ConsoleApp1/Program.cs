using XDevkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Test.Xbox.XDRPC;

namespace ConsoleApp1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var manager = new XboxManager();
            var console = manager.OpenConsole(manager.DefaultConsole);
            var exitCmd = "allahahkbar";
            var lastCmd = string.Empty;
            var validGame = false;

            // open console connection with null handler
            try
            {
                console.OpenConnection(null);
            }
            catch
            {
                Console.WriteLine("Couldn't connect to console.");
            }

            // check to make sure this console supports RPC
            //if (!console.SupportsRPC()) throw new NotSupportedException("Console does not support XDRPC!");

            var rcon = new RemoteConsole(console);
            var titleID = console.ExecuteRPC<uint>(XDRPCMode.Title, "xam.xex", 463);
            var gameType = 0;

            // switch running title id
            switch (titleID)
            {
                case 0x4541080f:
                    Console.WriteLine("The Orange Box is running!");
                    validGame = true;
                    gameType = 1;
                    break;
                case 0x5841125A:
                    Console.WriteLine("CS:GO is running!");
                    validGame = true;
                    gameType = 2;
                    break;
                default:
                    break;
            }

            if (validGame)
            {
                Console.Write("Connected!\n");
                while (lastCmd != exitCmd)
                {
                    // do rpc if not exit constant
                    Console.Write("\nEnter Command: ");
                    lastCmd = Console.ReadLine();
                    rcon.SendCommand(lastCmd,gameType);
                }
            }

            Console.WriteLine("bye");
            
        }
        
    }
}
