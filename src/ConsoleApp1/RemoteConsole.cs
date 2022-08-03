using System;
using Microsoft.Test.Xbox.XDRPC;
using XDevkit;

namespace ConsoleApp1
{
	public class RemoteConsole
	{

		private XboxConsole _myConsole;//  --- Please get back to this and figure out how to sync with main class
		//
		// 1. create constructor taking XboxConsole as a parameter (store as private member) -
		// 2. Create 2 public methods -
		//		AddCommand -
		//		ExecuteCommands -
		// 3. in AddCommand (CBuf_AddText)
		//		create a call using the XboxConsole to Execute an RPC
		//			i.e. myConsole.ExecuteRPC(XDRPCMode.Title, [address we got in IDA], { });
		// 4. do the same in ExecuteCommands (CBuf_Execute)
		//
		// Part 2:
		// 1. Move OpenConnection and GetTitleID from main method into this class as private methods
		// 2. Change constructor to take in nothing
		// 3. change constructor to call OpenConnection
		//
		// Part 3:
		// 1. add new constructor taking a string called 'consoleName' which can be the IP or Name of the console
		// 2. add overload to OpenConnection which explicitly opens by name/IP
		// 3. implement IDisposable on this class (hint: if you do RemoteConsole : IDisposible
		//		visual studio will popup and ask if you want it to auto-populate interface members following IDispose pattern
		// 4. Dispose of XboxConsole object (CloseConnection and possibly dispose() if it implements IDisposable.

		public RemoteConsole(XboxConsole myConsole)
        {
			//XboxConsole cnsl;
			//cnsl = myConsole;

			// correct way
			//this._myConsole = myConsole;
        }

		public void AddCommand(/*String - use primitive types (blue ones)*/ string command, XboxConsole myConsole) ///Please solve issue with the myConsole and make sync with main class without arg
        {
			uint cbufAddr = 0x860992A0;
            try
            {
                myConsole.ExecuteRPC<uint>(XDRPCMode.Title, cbufAddr, new object[] { command });
            }
            catch
            {
				Console.WriteLine("\nFailed to insert command!\n");
            }
            finally
            {
				Console.WriteLine("Executing command!");
				ExecuteCommands();
			}
            // mode, address, arguments (array)
            //myConsole.ExecuteRPC<uint>(XDRPCMode.Title, , argInfo);

		}

		public void ExecuteCommands()
        {
			uint cbufAddr = 0x8609B670;
			_myConsole.ExecuteRPC<uint>(XDRPCMode.Title, cbufAddr, new object[] {});
		}
	}
}
