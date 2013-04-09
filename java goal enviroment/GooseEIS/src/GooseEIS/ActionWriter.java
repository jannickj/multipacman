package GooseEIS;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

import eis.iilang.Action;

public class ActionWriter implements Runnable {
	Socket sock;
	BufferedReader reader;
	PrintWriter writer;
	
	public ActionWriter (Socket sock) throws IOException
	{
		this.sock = sock;
		reader = new BufferedReader (new InputStreamReader (sock.getInputStream()));
		writer = new PrintWriter(sock.getOutputStream(), true);
	}

	@Override
	public void run() 
	{
		
		
	}

	public void writeAction(Action a) 
	{
		writer.write (a.toXML());	
	}
}
