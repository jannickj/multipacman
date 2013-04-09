package GooseEIS;
import java.net.Socket;


import eis.iilang.Action;
import eis.iilang.Percept;


public class Agent {
	PerceptReader incoming;
	ActionWriter outgoing;
	
	public Agent (PerceptReader incoming, ActionWriter outgoing)
	{
		this.incoming = incoming;
		this.outgoing = outgoing;
	}
	
	public Percept performAction (Action a)
	{
		outgoing.writeAction(a);
		return incoming.readPercept();
	}

}
