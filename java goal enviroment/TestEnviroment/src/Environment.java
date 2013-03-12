import java.awt.Point;
import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Arrays;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;
import java.util.Random;

import eis.EIDefaultImpl;
import eis.exceptions.ActException;
import eis.exceptions.EntityException;
import eis.exceptions.ManagementException;
import eis.exceptions.NoEnvironmentException;
import eis.exceptions.PerceiveException;
import eis.iilang.Action;
import eis.iilang.EnvironmentState;
import eis.iilang.Parameter;
import eis.iilang.Percept;

public class Environment extends EIDefaultImpl {

	private static final int GRID_SIZE = 3;
	private static final long serialVersionUID = 1L;
	private ObjectOnTile[][] grid = new ObjectOnTile[GRID_SIZE][GRID_SIZE]; 
	private Point manPos;
	private boolean manLockedOnPackage = false;
	private HashMap<String,Direction> DirMap = new HashMap<String, Direction>();
	
	public Environment()
	{
		System.out.println("constructor");
		for(int i = 0; i < GRID_SIZE; i++)
		{
			Arrays.fill(grid[i], ObjectOnTile.Nothing);			
		}
		manPos = random(-1,-1);
		
		Point pack = random(manPos.x,manPos.y);
		System.out.println("pack pos: y = " + pack.y + " x = " + pack.x);
		grid[pack.y][pack.x] = ObjectOnTile.Package;
		
		
		Point packageGoal = random(pack.x,pack.y);
		grid[packageGoal.y][packageGoal.x] = ObjectOnTile.PackageGoal;
		System.out.println("goal pos: y = " + packageGoal.y + " x = " + packageGoal.x);		
		
		DirMap.put("up", Direction.up);
		DirMap.put("down", Direction.down);
		DirMap.put("left", Direction.left);
		DirMap.put("right", Direction.right);
		
	}
	
	private Point random(int x, int y)
	{
		Random rnd = new Random();
		Point p = new Point();
		while((p.x = Math.abs(rnd.nextInt() % GRID_SIZE)) == x && (p.y = Math.abs(rnd.nextInt() % GRID_SIZE)) == y);
		
		return p;
	}
	
	@Override
	public void init(Map<String, Parameter> parameters)
			throws ManagementException {
		System.out.println("init");
		super.init(parameters);
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
			throws PerceiveException, NoEnvironmentException {
		
		LinkedList<Percept> percepts = new LinkedList<Percept>();
		
		percepts.add(new Percept(grid[manPos.y][manPos.x].toString()));
		
		return percepts;
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

	@Override
	protected Percept performEntityAction(String arg0, Action action)
			throws ActException {
		Percept p = null;
		String an = action.getName();
		
		if(an.equals("move"))
		{
			System.out.println("Moving: "+action.getParameters().getFirst().toProlog());
			Direction  dir = StringToDirection(action.getParameters().getFirst().toProlog());
			
			if(!moveMan(dir))
			{
				p = new Percept("hitwall");	
				System.out.println("HitWall");
			}
			else
			{
				if(grid[manPos.y][manPos.x] == ObjectOnTile.PackageGoal && manLockedOnPackage)
				{
					p = new Percept("packageDelivered");
				}
				
			}
			
		}
		else if(an.equals("grab"))
		{
			System.out.println("Grabbing");
			if(grid[manPos.y][manPos.x] == ObjectOnTile.Package)
			{
				grid[manPos.y][manPos.x] = ObjectOnTile.Nothing;
				manLockedOnPackage = true;
				p = new Percept("grabbedpackage");
			}
		
			
		}
		
		printGrid();
		return p;
	}
	
	private Point getVector(Direction d)
	{
		switch(d)
		{
		case up:
			return new Point(0,-1);
		case down:
			return new Point(0,1);
		case left:
			return new Point(-1,0);
		case right:
			return new Point(1,0);
			
		}
		
		return null;		
	}
	
	private boolean isWithinGrid(Point p)
	{
		return p.x < GRID_SIZE && p.x >= 0 && p.y < GRID_SIZE && p.y >= 0;
		
	}
	
	private Direction StringToDirection(String s)
	{
		return DirMap.get(s);		
	}
	
	private boolean moveMan(Direction d)
	{
		
		Point vector = getVector(d);
		
		Point newPos = new Point(); 
		newPos.x = manPos.x + vector.x;
		newPos.y = manPos.y + vector.y;
		
		if( isWithinGrid(newPos) )
		{
			
			manPos.x = newPos.x;
			manPos.y = newPos.y;
			
			
			return true;
		}
		else
			return false;
			
	}
	
	private void printGrid()
	{
		for(int i = 0; i < GRID_SIZE; i++)
		{
			for(int j = 0; j < GRID_SIZE; j++)
			{
				String s = null;
				switch(grid[i][j])
				{
				case Nothing:
					s = "O";
					break;
				case Package:
					s = "P";
					break;
				case PackageGoal:
					s = "G";
					break;
				}
				if(manPos.x == j && manPos.y == i)
				{
					if(manLockedOnPackage)
						s = "M";
					else if(s.equals("P"))
						s = "�";
					else
						s = "m";
				}
				System.out.print(s);	
			}
			System.out.print("\n");
		}
	}

}
