package GooseEIS;

import java.util.LinkedList;

import eis.EIDefaultImpl;
import eis.exceptions.ActException;
import eis.exceptions.NoEnvironmentException;
import eis.exceptions.PerceiveException;
import eis.iilang.Action;
import eis.iilang.Percept;

public class GooseEnvironment extends EIDefaultImpl
{

	
	public GooseEnvironment()
	{
		
	}
	
	@Override
	public String requiredVersion() {
		// TODO Auto-generated method stub
		return null;
	}

	@Override
	protected LinkedList<Percept> getAllPerceptsFromEntity(String arg0) throws PerceiveException, NoEnvironmentException 
	{
		
		return null;
	}

	@Override
	protected boolean isSupportedByEntity(Action arg0, String arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	protected boolean isSupportedByEnvironment(Action arg0) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	protected boolean isSupportedByType(Action arg0, String arg1) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	protected Percept performEntityAction(String arg0, Action arg1) throws ActException
	{
		
		
		
		return null;
	}

}
