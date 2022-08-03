using System;
using Microsoft.Test.Xbox.XDRPC;
using XDevkit;

namespace ConsoleApp1
{
	public class RemoteConsole
	{

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

		private XboxConsole _myConsole;

		public RemoteConsole(XboxConsole myConsole)
        {
			_myConsole = myConsole;
		}

		public void SendCommand(string commandLine)
		{
			const uint addrCBuff_Add = 0x860992A0;
			const uint addrCBuff_Exec = 0x8609B670;

			// null check command being send for nulls or empty.
			if (string.IsNullOrEmpty(commandLine)) throw new ArgumentNullException(commandLine);

			// append line feed if one doesn't exist.
			if (commandLine[commandLine.Length - 1] != '\n') commandLine += '\n';

			// prepare rpc call
			var paramCmd = new XDRPCStringArgumentInfo(commandLine);

			// execute RPC call
			_myConsole.ExecuteRPC<uint>(XDRPCMode.Title, addrCBuff_Add, paramCmd);
			_myConsole.ExecuteRPC<uint>(XDRPCMode.Title, addrCBuff_Exec);
		}
	}
}
