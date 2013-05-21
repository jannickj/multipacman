package GooseEIS;
import java.awt.Point;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintStream;
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

public class XmasEnvironment extends EIDefaultImpl 
{
	private static final long serialVersionUID = 1L;
	private int port = 44444;
	private Socket socket;
	private InputStream sockreader;
	private PrintStream sockwriter;
	XMLReader xmlreader;
	private String Name;
	
	public XmasEnvironment()
	{
//		Map<String, Parameter> m = new HashMap<String, Parameter>();
//		
//		Identifier param = new Identifier("testname");
//		m.put("agentName", param);
//		try {
//			this.init(m);
//		} catch (ManagementException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
	}
	
	@Override
	public void init(Map<String, Parameter> parameters)
			throws ManagementException {
		System.out.println("init");
		super.init(parameters);
		
		Identifier nameId;
		if (parameters.containsKey("agentName") && parameters.get("agentName") instanceof Identifier)
		{
			nameId = ((Identifier) parameters.get("agentName"));
			Name = nameId.getValue();
		}
		else
			throw new RuntimeErrorException(
					new Error("No mapping from 'agentName' to Identifier could be found in parameters")
					);
		
		System.out.println("Agent name = " + Name);
		printDebugMsg("Connecting to socket on port " + port);
		
		try {
			socket = new Socket("localhost", port);
			sockreader = socket.getInputStream();
			sockwriter = new PrintStream (socket.getOutputStream(), true);
			
			xmlreader = XMLReaderFactory.createXMLReader();
		} catch (IOException | SAXException e) {
			e.printStackTrace();
		}
		
		printDebugMsg("Connected to socket, sending handshake");
		
		System.out.println("WRITING: "+nameId.toXML());
		sockwriter.print(nameId.toXML());
		//TODO: Look into making it an actual handshake (ie. receive a confirmation)
		
		try {
			this.addEntity(this.Name);
		} catch (EntityException e) {
			e.printStackTrace();
		}
		
		setState(EnvironmentState.PAUSED);
	}
	
	private void printDebugMsg(String str)
	{
		System.out.println(Name + ":: " + str);
	}

	@Override
	public void start() throws ManagementException {
		setState(EnvironmentState.RUNNING);
	}
	
	public static void main(String[] args) {
		System.out.println("main");
		new XmasEnvironment();
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
		System.out.println("WRITING: "+action.toXML());
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
		
		LinkedList<Percept> percepts = handler.<LinkedList<Percept>>getElementAs();
		for (Percept p : percepts) {
			System.out.println(p);
		}
		
		return handler.<LinkedList<Percept>>getElementAs();
	}
	
	@Override
	protected Percept performEntityAction(String arg0, Action action)
			throws ActException 
	{
		System.out.println("WRITING: "+action.toXML());
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
