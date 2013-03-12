package EnvironmentInterface;

import java.util.LinkedList;
import java.util.Map;

import wumpusenv.WumpusApp;
import wumpusenv.WumpusWorld;
import wumpusenv.WumpusWorldPercept;
import eis.EIDefaultImpl;
import eis.exceptions.ActException;
import eis.exceptions.EntityException;
import eis.exceptions.ManagementException;
import eis.exceptions.NoEnvironmentException;
import eis.exceptions.PerceiveException;
import eis.iilang.Action;
import eis.iilang.EnvironmentState;
import eis.iilang.Identifier;
import eis.iilang.Numeral;
import eis.iilang.Parameter;
import eis.iilang.Percept;

/**
 * <p>
 * Provides an EIS environment interface for connecting the Wumpus environment
 * with GOAL.
 * </p>
 * <p>
 * Interface is said to be connected to Wumpus world if a Wumpus game is
 * running. Wumpus world need to be launched again if no WumpusApp is available.
 * </p>
 * 
 * @author KH
 */
public class WumpusEnvironment extends EIDefaultImpl {

	private static final long serialVersionUID = 773327222037240183L;

	private WumpusWorld world;
	/**
	 * Single CONTROLLABLE entity living in the Wumpus world.
	 */
	private static final String ENTITY = "agent";

	enum InitKey {
		FILE, UNKNOWN;

		static InitKey toKey(String key) {
			try {
				return valueOf(key.toUpperCase());
			} catch (Exception ex) {
				return UNKNOWN;
			}
		}
	}

	enum WumpusAction {
		FORWARD, GRAB, SHOOT, CLIMB, TURN, UNKNOWN;

		static WumpusAction toKey(String act) {
			try {
				return valueOf(act.toUpperCase());
			} catch (Exception ex) {
				return UNKNOWN;
			}
		}

	}

	/**
	 * Main method to start Wumpus environment stand alone.
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		new WumpusEnvironment();
	}

	/**
	 * Creates a Wumpus world.
	 */
	public WumpusEnvironment() {
		world = WumpusWorld.getInstance();
		// do not change order!
		world.setInterface(this);
		// set up needs the interface to register entity
		world.setUp();
	}

	/**************************************************************/
	/******************** Suppport functions **********************/
	/**************************************************************/

	/**
	 * Creates a Wumpus world.
	 */
	public WumpusEnvironment() {
		world = WumpusWorld.getInstance();
		// do not change order!
		world.setInterface(this);
		// set up needs the interface to register entity
		world.setUp();
	}

	/**
	 * Each call to executeAction will increment the current time.
	 * 
	 * @throws NoEnvironmentException
	 */
	private void executeAction(String pAgent, String pAct)
			throws NoEnvironmentException {
		if (getApplication().getRunner().gameRunning()) {
			getApplication().getRunner().nextStep(pAct);
		} else {
			throw new NoEnvironmentException("Game is not running");
		}
	}

	/**
	 * Returns Wumpus application associated with Wumpus world interface.
	 * 
	 * @return Wumpus application object, may be null.
	 */
	private WumpusApp getApplication() {
		return world.getApplication();
	}

	/**
	 * Register entity with EIS. There is a single controllable entity in this
	 * Wumpus world called 'agent'.
	 */
	public void registerEntity() {
		try {
			this.addEntity(ENTITY);
		} catch (EntityException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Unregister entity with EIS.
	 */
	public void unregisterEntity() {
		try {
			deleteEntity(ENTITY);
		} catch (EntityException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Notify environment listeners of environment event.
	 * 
	 * @param state
	 *            environment event.
	 * @see eis.EIDefaultImpl.
	 */
	public void notifyStateChange(EnvironmentState state) {
		try {
			if (state != getState()) {
				setState(state);
			}
		} catch (ManagementException e) {
			// should not happen. Throw stack trace to screen.
			e.printStackTrace();
		}
	}

	/**************************************************************/
	/********** Implements EnvironmentInterface *******************/
	/**************************************************************/

	/**
	 * {@inheritDoc}
	 */
	@Override
	protected LinkedList<Percept> getAllPerceptsFromEntity(String arg0)
			throws PerceiveException, NoEnvironmentException {
		// EIS percepts
		LinkedList<Percept> percepts = new LinkedList<Percept>();
		

		WumpusWorldPercept wumpusWorldPercept = getApplication().getRunner()
				.getCurrentPercept();

		if (wumpusWorldPercept == null) {
			return null;
		}

		// construct the EIS percepts from the Wumpus World percept
		if (wumpusWorldPercept.getBreeze()) {
			percepts.add(new Percept("breeze"));
		}
		if (wumpusWorldPercept.getStench()) {
			percepts.add(new Percept("stench"));
		}
		if (wumpusWorldPercept.getBump()) {
			percepts.add(new Percept("bump"));
		}
		if (wumpusWorldPercept.getScream()) {
			percepts.add(new Percept("scream"));
		}
		if (wumpusWorldPercept.getGlitter()) {
			percepts.add(new Percept("glitter"));
		}
		percepts.add(new Percept("time", new Numeral(getApplication()
				.getRunner().getTime())));

		return percepts;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public EnvironmentState getState() {
		if (getApplication() != null && getApplication().getRunner() != null) {
			if (getApplication().getRunner().gameRunning()) {
				return EnvironmentState.STARTED;
			}
			return EnvironmentState.PAUSED;
		}
		return EnvironmentState.INITIALIZED;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public void kill() throws ManagementException {
		if (getApplication() != null) { // Wumpus application not yet killed
			try {
				deleteEntity(ENTITY);
			} catch (EntityException e) {
				e.printStackTrace();
			}
			world.close();
		}
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public void pause() throws ManagementException {
		getApplication().getRunner().setPaused(true);
		setState(EnvironmentState.PAUSED);
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public void start() throws ManagementException {
		// need to launch Wumpus world?
		if (getApplication() == null) {
			world.setUp();
		} else { // start running
			// if game is running switch to runner view, if that is not
			// visible
			if (getApplication().getRunner().isVisible()) {
				getApplication().getRunner().setPaused(false);
			} else {
				// make runner visible
				getApplication().action(null, WumpusApp.RUNNER);
				getApplication().getRunner().setPaused(false);
			}
		}
		setState(EnvironmentState.STARTED);
	}

	/*
	 * @Override public void manageEnvironment(EnvironmentCommand command)
	 * throws ManagementException, NoEnvironmentException { switch
	 * (command.getType()) { case START: // need to launch Wumpus world? if
	 * (getApplication() == null) { world.setUp(); } else { // start running //
	 * if game is running switch to runner view, if that is not // visible if
	 * (getApplication().getRunner().isVisible()) {
	 * getApplication().getRunner().setPaused(false); } else { // make runner
	 * visible getApplication().action(null, WumpusApp.RUNNER);
	 * getApplication().getRunner().setPaused(false); } }
	 * notifyEnvironmentEvent(STARTED_EVT); break; case KILL: if
	 * (getApplication() != null) { // Wumpus application not yet killed try {
	 * deleteEntity(ENTITY); } catch (EntityException e) { e.printStackTrace();
	 * } world.close(); notifyEnvironmentEvent(KILLED_EVT);
	 * 
	 * } break; case PAUSE: getApplication().getRunner().setPaused(true);
	 * notifyEnvironmentEvent(PAUSED_EVT); break; case RESET:
	 * getApplication().getRunner().reset(); notifyEnvironmentEvent(PAUSED_EVT);
	 * break; case INIT: break; case MISC: throw new
	 * ManagementException("Command MISC is not supported."); default: throw new
	 * ManagementException("Unknown environment command " + command); } }
	 */

	/**
	 * {@inheritDoc}
	 */
	@Override
	public void init(Map<String, Parameter> parameters)
			throws ManagementException {

		for (String key : parameters.keySet()) {
			Parameter p = parameters.get(key);
			switch (InitKey.toKey(key)) {
			case FILE:
				if (!(p instanceof Identifier)) {
					throw new ManagementException(
							"String expected as value for key" + InitKey.FILE
									+ " but got " + p);
				}
				// load that map
				java.net.URL url;
				String filename = ((Identifier) p).getValue();
				try {
					world.getApplication()
							.getEditor()
							.loadFrom(
									getClass().getClassLoader().getResource(
											filename));
				} catch (Exception e) {
					System.out
							.println("Warning: wumpus environment can't open map "
									+ filename);
				}
				break;
			default:
				throw new ManagementException("Init key " + key + " unknown.");

			}
		}

		// HACK. nothing really supported yet. trac 959
		setState(EnvironmentState.PAUSED);
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public String requiredVersion() {
		return "0.3";
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	protected boolean isSupportedByEntity(Action action, String arg1) {
		return WumpusAction.toKey(action.getName()) != WumpusAction.UNKNOWN;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	protected boolean isSupportedByEnvironment(Action arg0) {
		return true;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	protected boolean isSupportedByType(Action arg0, String arg1) {
		return true;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	protected Percept performEntityAction(String entity, Action action)
			throws ActException {
		try {
			
			
			switch (WumpusAction.toKey(action.getName())) {
			case CLIMB:
				executeAction(entity, "climb");
				return null;
			case FORWARD:
				executeAction(entity, "forward");
				return null;
			case GRAB:
				executeAction(entity, "grab");
				return null;
			case SHOOT:
				executeAction(entity, "shoot");
				return null;
			case TURN:
				if (action.getParameters().size() != 1) {
					throw new ActException(
							"turn requires exactly 1 parameter, but received "
									+ action.getParameters());
				}
				Parameter param0 = action.getParameters().get(0);
				if (!(param0 instanceof Identifier)) {
					throw new ActException(
							"turn takes Identifier as parameter but received "
									+ param0);
				}
				String direction = ((Identifier) param0).getValue();
				if (direction.equals("left")) {
					executeAction(entity, "turn(left)");
					return null;
				} else if (direction.equals("right")) {
					executeAction(entity, "turn(right)");
					return null;
				} else {
					throw new ActException(
							"turn takes only 'left' and 'right' as parameter, but received "
									+ direction);
				}
			default: // UNKNOWN
				throw new ActException("unknown action: " + action);
			}
		} catch (NoEnvironmentException e) {
			throw new ActException("Environment is not available");
		}
	}
}
