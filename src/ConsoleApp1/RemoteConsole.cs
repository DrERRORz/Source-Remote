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

		private XboxConsole myConsole;

		public RemoteConsole(XboxConsole myConsole)
        {
			this.myConsole = myConsole;
		}

		public void SendCommand(string commandLine, int type)
		{
			///All values below are updated to match the current values of the TUs
			///Future me, use this ExecutingComma
			uint addrCBuff_Add = 0;
			uint addrExeComm = 0;
			if (type.Equals(1)) //TOB
            {
				addrCBuff_Add = 0x8609A848; 
				addrExeComm = 0x8609CE90; 
			}
			if (type.Equals(2)) //CS:GO
			{
				addrCBuff_Add = 0x86A1A330; 
				addrExeComm = 0x86A1AFB8;  
			}
			if (type.Equals(3)) //Hyrbid?
			{
				addrCBuff_Add = 0x8643C5E0;
				addrExeComm = 0x8643C8C0;
			}
			if (type.Equals(4)) //Left 4 Dead 2
            {
				addrCBuff_Add = 0x86BCDDB0;
				addrExeComm = 0x86BD0388;
			}

			// null check command being send for nulls or empty.
			if (string.IsNullOrEmpty(commandLine)) throw new ArgumentNullException(commandLine);

			// append line feed if one doesn't exist.
			if (commandLine[commandLine.Length - 1] != '\n') commandLine += '\n';

			// prepare rpc call
			var paramCmd = new XDRPCStringArgumentInfo(commandLine);

			// execute RPC call 
			myConsole.ExecuteRPC<uint>(XDRPCMode.Title, addrCBuff_Add, paramCmd);
			myConsole.ExecuteRPC<uint>(XDRPCMode.Title, addrExeComm);
		}
	}
}
