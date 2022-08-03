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
        static XboxConsole myConsole;

        static uint GetTitleID()
        {
            // invoke RPC by ordinal (DLL or Module export Number)
            // Since xbox 360 does not export by name like on other platforms
            // we must instead invoke by ordinal number
            //
            // xam.xex (xbox aux methods) module contains a function at ordinal number 463
            // that returns the currently running game ID (number)
            //
            // our RPC is constructed as follows
            // XDRPCMode = Title (system should work too)
            // module = xam.xex
            // ordinal number = 463
            // arguments = empty object
            return myConsole.ExecuteRPC <uint>(XDRPCMode.Title, "xam.xex", 463);
        }

        static void OpenConnection()
        {
            while (true)
            {
                XboxManagerClass xbm = new XboxManagerClass();
                string dflt_cnsl = xbm.DefaultConsole;
                Console.Write($"{dflt_cnsl}\nIs this the correct address? Y/N: ");
                var userInput = Console.ReadLine();
                if (userInput.ToUpper() == "Y")
                {
                    try
                    {
                        Console.WriteLine("\nConnecting...\n");
                        XboxManager manager = (XDevkit.XboxManager)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                        myConsole = manager.OpenConsole(dflt_cnsl);
                    }
                    catch
                    {
                        Console.Write("Failed connection!\nRetrying...\n");
                    }
                    finally
                    {
                        Console.WriteLine($"Connected to {myConsole.Name}");
                        Console.WriteLine($"current process name: {myConsole.RunningProcessInfo.ProgramName}\n");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\nReissuing request...\n");
                    Thread.Sleep(5000);
                }
            }
        }

        static void Main(string[] args)
        {
            RemoteConsole RmtCnsl = new RemoteConsole(myConsole);
            OpenConnection();

            var titleID = GetTitleID();

            // switch running title id
            switch (titleID)
            {
                case 0x4541080f:
                    Console.WriteLine("The Orange Box is running!");
                    break;
                default:
                    break;
            }
            var exitCmd = "allahahkbar";
            Console.Write("Connected! \n\nEnter Command: ");
            var lastCmd = Console.ReadLine();
            RmtCnsl.AddCommand(lastCmd,myConsole);


            while (lastCmd != exitCmd)
            {
                // do rpc if not exit constant
            }
            Console.WriteLine("bye");
            
        }
        
    }
}
