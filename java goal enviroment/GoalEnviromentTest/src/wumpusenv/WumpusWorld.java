/**
 Made separate environment package W.Pasman 28oct08.
 compile with 
	javac -classpath ../../bin wumpusenv/*.java
 
 Make jar with...
 
 jar cf wumpusenv.jar wumpusenv/*.class wumpusenv/images/*
 
 and move it to the appropriate position with eg

 mv wumpusenv.jar AI4010/
 
 to debug, 
 add the path to env to the build path in the project properties
 and then modify the path that you give in the MAS file.
 */
package wumpusenv;

import java.awt.Color;
import java.awt.Event;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Panel;

import EnvironmentInterface.WumpusEnvironment;
import eis.iilang.EnvironmentState;

/**
 * WumpusWorld is the main Applet that starts the Wumpus simulator and world
 * editor. </p>
 * <p>
 * On macintosh, this application works stable on Java 1.4.2 and higher. Minimal
 * version details: "1.4.2_09". Java(TM) 2 Runtime Environment, Standard Edition
 * (build 1.4.2_09-233) Java HotSpot(TM) Client VM (build 1.4.2-56, mixed mode)
 * Under 1.4.1 it crashes very frequenty (Usually Bus errors) while loading the
 * world.
 * </p>
 * <p>
 * Remember to set the classpath right, e.g. setenv CLASSPATH
 * $CLASSPATH\:/wumpus
 * </p>
 * 
 * @see {@link http://www-cse.uta.edu/~holder/courses/cse5361/wumpus.html}.
 *      Extensive information is available on {@link http 
 *      ://www.kr.tuwien.ac.at/students/prak_wumpusjava/simulator/Welcome.html}
 *      and {@link http://cl3512.inf.tu-dresden.de:8180/TomcatFlux/wumpus/}.
 */
@SuppressWarnings("serial")
public class WumpusWorld extends Panel {

	private WumpusEnvironment wumpusInterface;
	private WumpusApp wumpusApplication = null;

	/**
	 * Main method to start Wumpus environment stand alone.
	 * 
	 * @param args
	 */
	public static void main(String[] args) {
		getInstance().setUp();
	}

	/**
	 * Creates a new Wumpus world object and creates a Wumpus application.
	 */
	private WumpusWorld() {
		System.out.println("Initializing the Wumpus World.");
	}

	/**
	 * WumpusWorldHolder is loaded on the first execution of
	 * WumpusWorld.getInstance() or the first access to
	 * WumpusWorldHolder.INSTANCE, not before.
	 */
	private static class WumpusWorldHolder {
		private static final WumpusWorld INSTANCE = new WumpusWorld();
	}

	/**
	 * Create or get reference to the unique singleton Wumpus world.
	 * 
	 * @return singleton Wumpus world object.
	 * @see Threadsafe, see: {@link http
	 *      ://en.wikipedia.org/wiki/Singleton_pattern
	 *      #The_solution_of_Bill_Pugh}.
	 */
	public static WumpusWorld getInstance() {
		return WumpusWorldHolder.INSTANCE;
	}

	/**
	 * Sets EIS interface object.
	 */
	public void setInterface(WumpusEnvironment wumpusInterface) {
		this.wumpusInterface = wumpusInterface;
	}

	/*
	 * Point of making this class singleton is that we now make various EIS
	 * methods available for other classes, e.g. Runner and WumpusApp class.
	 */
	/**
	 * Registers entity with EIS interface if such an interface is available.
	 */
	public void registerEntity() {
		if (wumpusInterface != null) {
			wumpusInterface.registerEntity();
		}
	}

	/**
	 * Unregister entity with EIS interface if such an interface is available.
	 */
	public void unregisterEntity() {
		if (wumpusInterface != null) {
			wumpusInterface.unregisterEntity();
		}
	}

	/**
	 * See {@link WumpusEnvironment#notifyStateChange(EnvironmentEvent)}. This
	 * is using a hard observer pattern.
	 * 
	 * @param state
	 *            is the new {@link EnvironmentState}.
	 * 
	 */
	public void notifyStateChange(EnvironmentState state) {
		if (wumpusInterface != null) {
			wumpusInterface.notifyStateChange(state);
		}
	}

	/**
	 * Returns Wumpus application.
	 * 
	 * @return wumpus application.
	 */
	public WumpusApp getApplication() {
		return wumpusApplication;
	}

	/**
	 * Sets up a new Wumpus world.
	 */
	public void setUp() {
		System.out.println("Setting up WUMPUS WORLD");
		if (wumpusApplication != null) {
			wumpusApplication.dispose();
			wumpusApplication.setVisible(false);
		}
		wumpusApplication = new WumpusApp();
	}

	/**
	 * DOC
	 * 
	 * @param g
	 */
	public void paint(Graphics g) {
		g.setColor(Color.green.darker().darker());
		setBackground(Color.white);
		g.drawImage(wumpusApplication.getImage("wumpus.gif"), 0, 0, this);
		int midden = getSize().width / 2;
		g.setFont(new Font("Serif", Font.BOLD, 14));
		FontMetrics fm = g.getFontMetrics();
		g.drawString("Wumpus", midden - fm.stringWidth("Wumpus") / 2, 15);
		g.drawString("Applet", midden - fm.stringWidth("Applet") / 2, 30);
		g.drawString("Stub", midden - fm.stringWidth("Stub") / 2, 45);
		g.drawImage(wumpusApplication.getImage("agent.gif"),
				getSize().width - 50, 0, this);
	}

	/**
	 * handle edit event
	 * 
	 * @param event
	 */
	public void mouseClicked(Event event) {
		wumpusApplication.show();
	}

	/**
	 * close the wumpus world. Sets the environment state to KILLED
	 */
	public void close() {
		wumpusApplication.dispose();
		wumpusApplication.setVisible(false);
		wumpusApplication = null;
		notifyStateChange(EnvironmentState.KILLED);
	}
}
