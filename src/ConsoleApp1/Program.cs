using XDevkit;
using JRPC_Client;
using System;
using Microsoft.Test.Xbox.XDRPC;

namespace ConsoleApp1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var manager = new XboxManager();
            var console = manager.OpenConsole(manager.DefaultConsole);
            var exitCmd = "exitcmd";
            var lastCmd = string.Empty;
            var validGame = false;

            // open console connection with null handler
            try
            {
                console.OpenConnection(null);
                console.XNotify("Connected! GLHF");
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
                    Console.WriteLine("The Orange Box is running!\n");
                    validGame = true;
                    gameType = 1;
                    break;
                case 0x5841125A:
                    Console.WriteLine("CS:GO is running!\n");
                    validGame = true;
                    gameType = 2;
                    break;
                case 0x584109E9:
                    Console.WriteLine("Hybrid is running!\n");
                    validGame = true;
                    gameType = 3;
                    break;
                case 0x454108D4:
                    Console.WriteLine("Left 4 Dead 2 is running!\n");
                    validGame = true;
                    gameType = 4;
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
            console.XNotify("Connection Broke! Please open valid title!");

        }
        
    }
}
