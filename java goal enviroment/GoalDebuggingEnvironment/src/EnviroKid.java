import java.io.IOException;
import java.io.PrintStream;
import java.net.Socket;
import java.util.LinkedList;
import java.util.Map;

import javax.management.RuntimeErrorException;

import org.xml.sax.SAXException;
import org.xml.sax.helpers.XMLReaderFactory;
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


public class EnviroKid extends EIDefaultImpl
{
	private long last = 0;
	
	@Override
	public void init(Map<String, Parameter> parameters)
			throws ManagementException {
		super.init(parameters);
		
		
		
		try {
			this.addEntity("debugger");
		} catch (EntityException e) {
			e.printStackTrace();
		}
		
		setState(EnvironmentState.PAUSED);
	}


	@Override
	protected LinkedList<Percept> getAllPerceptsFromEntity(String arg0)
			throws PerceiveException, NoEnvironmentException {
		// TODO Auto-generated method stub
		return new LinkedList<Percept>();
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
	public void start() throws ManagementException {
		setState(EnvironmentState.RUNNING);
	}
	
	public static void main(String[] args) {
		new EnviroKid();
	}
	
	@Override
	public String requiredVersion() {
		return "0.3";
	}

	@Override
	protected boolean isSupportedByType(Action arg0, String arg1) {
		// TODO Auto-generated method stub
		return true;
	}

	@Override
	protected Percept performEntityAction(String arg0, Action arg1)
			throws ActException {
		long now = System.nanoTime();
		long current = (now - last)/1000000L;
		System.out.println("Timed: "+current);
		last = System.nanoTime();
		// TODO Auto-generated method stub
		return null;
	}

}
