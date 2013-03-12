package wumpusenv;

import java.awt.*;
import java.io.*;
import java.util.Properties;

import EnvironmentInterface.WumpusEnvironment;

/**
 * WumpusApp is a combination of editor and runner for the agent in the Wumpus World.
 * It is called from the WumpusWorld class.
 * It contains TWO PANELS: the Runner and the WorldEditor.
*/

public class WumpusApp extends Frame {
	
	private static final long serialVersionUID = 8714859195624414371L;
	
	// World editor
    private WorldEditor worldEditor;
    private Menu worldEditorMenu;
    public static String WORLDED = "World Editor";
    
    // Runner
    private Runner runner;
    private Menu runnerMenu;
    public static String RUNNER = "Wumpus Runner";
    
    // Graphics
    private static String fPath = "wumpusenv/images";
    private CardLayout cardLayout;
    private Panel mainPanel;
    private Properties preferences;
    private Dialog errorDialog = new Dialog(this, "Error!", true);
    private Label errorLabel = new Label("");
    private CheckboxMenuItem toggleDebug, toggleGC, toggleSIM;
    
    /**
     * DOC
     */
    public WumpusApp() {
        super("Wumpus environment editor and simulator");
        InitWumpusApp();
    }
    
	/**
	 *  DOC
	 */
    public void InitWumpusApp() {
        setLayout(new BorderLayout());

        try {
        	setIconImage(getToolkit().getImage(fPath + "/wumpus.gif"));
        } catch (Exception e) { 
        	e.printStackTrace();
        }
        cardLayout = new CardLayout();
        
        worldEditor = new WorldEditor(this);
        runner = new Runner(this);
        preferences = loadPrefs();
        
        mainPanel = new Panel();
        mainPanel.setLayout(cardLayout);
        mainPanel.add(WORLDED, worldEditor);
        mainPanel.add(RUNNER, runner);
        Panel buttonPanel = new Panel();
        buttonPanel.setLayout(new GridLayout(1, 3));
        buttonPanel.add(new Button(WORLDED));
        buttonPanel.add(new Button(RUNNER));
        add("Center", mainPanel);
        add("South", buttonPanel);
        setMenuBar(setupMenuBar());

        setSize(640,480);
        setLocation(450, 0);
        setVisible(true);
    }
    
    /**
     * DOC
     * 
     * @return
     */
    public Runner getRunner() {
    	return runner;
    }
    
    /**
     * DOC
     * 
     * @return
     */
    private MenuBar setupMenuBar() {
        MenuBar menuBar = new MenuBar();
        //Menu fileMenu = new Menu("File");
        //fileMenu.add("Quit"); // disabled
        
        worldEditorMenu = new Menu("World Editor");
        worldEditorMenu.add("Load world");
        worldEditorMenu.add("Save world");
        
        //menuBar.add(fileMenu);
        menuBar.add(worldEditorMenu);
        
        return menuBar;
    }

    /**
     * DOC
     * 
     * @return
     */
	private Properties loadPrefs() {
		return new Properties();
	}

	/**
	 * DOC
	 * 
	 * @param pName
	 * @return
	 */
	public Image getImage(String pName) {
		try {
			java.net.URL u = getClass().getClassLoader().getResource(
					fPath + "/" + pName);
			return getToolkit().getImage(u);
		} catch (Exception ex) {
			ex.printStackTrace();
			return null;
		}
	}
    
	/**
	 * Closes down Wumpus environment. First unregisters entity with EIS, if needed.
	 * 
	 * @param evt
	 */
    public boolean handleEvent(Event evt) {
        switch (evt.id) {
            case Event.WINDOW_DESTROY:
            	// clean up first
            	WumpusWorld.getInstance().unregisterEntity();
            	// close
                WumpusWorld.getInstance().close();
                return true;
            default:
                return super.handleEvent(evt);
        }
    }

    /**
     * DOC
     * 
     * @param event
     * @param obj
     */
    public boolean action(Event event, Object obj) {
        if (obj == WORLDED) {
            cardLayout.show(mainPanel, WORLDED);
            return true;
        }
 		if (obj == RUNNER) {
            cardLayout.show(mainPanel, RUNNER);
            runner.setRealModel(worldEditor.getModel());
            return true;
        }
		if ("Load world".equals(obj)) {
            FileDialog fd = new FileDialog(this, "Load a world", FileDialog.LOAD);
            FilenameFilter fnf = new ExtensionFilter(".wld");
            fd.setFilenameFilter(fnf);
            fd.setDirectory(preferences.getProperty("homedir"));
            fd.setFile("*.wld");
            fd.show();
            if (fd.getFile() != null) {
                worldEditor.loadFrom(new File(fd.getDirectory() + fd.getFile()));
            }
			cardLayout.show(mainPanel, WORLDED);
			System.out.println("New world loaded");
			runner.reset();
			System.out.println("Runner reset");
            return true;
        }
 		if ("Save world".equals(obj)) {
            FileDialog fd = new FileDialog(this, "Save a world", FileDialog.SAVE);
            FilenameFilter fnf = new ExtensionFilter(".wld");
            fd.setFilenameFilter(fnf);
            fd.setDirectory(preferences.getProperty("homedir"));
            fd.setFile("*.wld");
            fd.show();
            if (fd.getFile() != null) {
                worldEditor.saveTo(new File(fd.getDirectory() + fd.getFile()));
            }
            return true;
        } 
	
		
		return false;
    }
    
    /**
     * DOC
     * 
     * @param msg
     */
	public void reportError(String msg) {
		errorLabel.setText(msg);
		errorDialog.pack();
		errorDialog
				.setLocation(
						getLocation().x
								+ (getSize().width - errorDialog.getSize().width)
								/ 2,
						getLocation().y
								+ (getSize().height - errorDialog.getSize().height)
								/ 2);
		errorDialog.show();
	}
	
	public WorldEditor getEditor() {
		return worldEditor;
	}
}

/**
 * DOC
 */
class ExtensionFilter implements FilenameFilter {
    private String extension;
   
    /**
     * DOC
     * 
     * @param extension
     */
    public ExtensionFilter(String extension) {
        this.extension = extension;
    }
    
    /**
     * DOC
     */
    public boolean accept(File dir, String name) {
        return (name.indexOf(extension) != -1);
    }
    
}

/**
 * DOC
 */
class PrefDialog extends Dialog {
	
	private static final long serialVersionUID = 4093273691750133525L;
	
	protected TextField homeDir = new TextField();
    
    /**
     * DOC
     * 
     * @param owner
     */
    public PrefDialog(Frame owner) {
        super(owner, "Preferences", true);
        setLayout(new BorderLayout());
        Panel prefs = new Panel();
        prefs.setLayout(new FlowLayout());
        prefs.add(new Label("Home directory"));
        prefs.add(homeDir);
    }
}
