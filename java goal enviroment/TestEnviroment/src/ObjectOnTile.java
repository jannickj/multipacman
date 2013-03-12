
public enum ObjectOnTile {
	
	Nothing("nothing"),
	Package("package"),
	PackageGoal("packagegoal");
	
	private String value;
	
	ObjectOnTile(String value) {
        this.value = value;
    }
	
	public String toString()
	{
		return value;
	}
}
