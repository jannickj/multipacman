package GooseEIS;
import java.awt.Point;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ContentHandlerFactory;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Arrays;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Random;

import javax.management.RuntimeErrorException;

import org.xml.sax.InputSource;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.XMLReaderFactory;

import XML.PerceptCollectionHandler;
import XML.PerceptHandler;

import eis.EIDefaultImpl;
import eis.exceptions.ActException;
import eis.exceptions.EntityException;
import eis.exceptions.ManagementException;
import eis.exceptions.NoEnvironmentException;
import eis.exceptions.PerceiveException;
import eis.iilang.Action;
import eis.iilang.EnvironmentState;
import eis.iilang.Identifier;
import eis.iilang.Parameter;
import eis.iilang.Percept;

public class Environment extends EIDefaultImpl 
{
	private static final long serialVersionUID = 1L;
	private int port = 444444;
	private Socket socket;
	private InputStream sockreader;
	private PrintWriter sockwriter;
	XMLReader xmlreader;
	
	public Environment()
	{
	}
	
	@Override
	public void init(Map<String, Parameter> parameters)
			throws ManagementException {
		System.out.println("init");
		super.init(parameters);
		
		Identifier Name;
		if (parameters.containsKey("name") && parameters.get("name") instanceof Identifier)
			Name = ((Identifier) parameters.get("name"));
		else
			throw new RuntimeErrorException(
					new Error("No mapping from 'name' to Identifier could be found in parameters")
					);
		
		try {
			socket = new Socket("localhost", port);
			sockreader = socket.getInputStream();
			sockwriter = new PrintWriter (socket.getOutputStream(), true);
			xmlreader = XMLReaderFactory.createXMLReader();
		} catch (IOException | SAXException e) {
			e.printStackTrace();
		}
		
		sockwriter.print(Name.toXML());
		//TODO: Look into making it an actual handshake (ie. receive a confirmation)
		
		try {
			this.addEntity("agent");
		} catch (EntityException e) {
			e.printStackTrace();
		}
		
		setState(EnvironmentState.PAUSED);
	}

	@Override
	public void start() throws ManagementException {
		setState(EnvironmentState.RUNNING);
	}
	
	public static void main(String[] args) {
		System.out.println("main");
		new Environment();
	}
	
	@Override
	public String requiredVersion() {
		return "0.3";
	}

	@Override
	protected LinkedList<Percept> getAllPerceptsFromEntity(String arg0)
			throws PerceiveException, NoEnvironmentException 
	{
		Action action = new Action ("getAllPercepts");
		sockwriter.print (action.toXML());
		
		PerceptCollectionHandler handler = new PerceptCollectionHandler(xmlreader);
		xmlreader.setContentHandler (handler);
		
		try {
			xmlreader.parse(new InputSource(sockreader));
		} catch (IOException | SAXException e) {
			try {
				throw new Exception ("Could not parse XML in agent " + arg0, e);
			} catch (Exception e1) {
				e1.printStackTrace();
			}
		}
		 
		return handler.<LinkedList<Percept>>getElementAs();
	}
	
	@Override
	protected Percept performEntityAction(String arg0, Action action)
			throws ActException 
	{
		sockwriter.print (action.toXML());
		return null;
	}

	@Override
	protected boolean isSupportedByEntity(Action arg0, String arg1) {
		// TODO Auto-generated method stub
		return true;
	}

	@Override
	protected boolean isSupportedByEnvironment(Action arg0) {
		// TODO Auto-generated method stub
		return true;
	}

	@Override
	protected boolean isSupportedByType(Action arg0, String arg1) {
		// TODO Auto-generated method stub
		return true;
	}
}
