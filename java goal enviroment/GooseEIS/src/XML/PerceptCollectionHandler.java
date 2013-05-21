package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import GooseEIS.FinishedParsingException;

import eis.iilang.Parameter;
import eis.iilang.Percept;

public class PerceptCollectionHandler extends ParameterHandler {

	public PerceptCollectionHandler(XMLReader reader) {
		super(null, reader, null);
		reader.setContentHandler(this);
		
	}
	
	@Override
	public void startElement (String uri, String name, String qname, Attributes attributes)
	{
		if ("percept".equals(name))
		{
			new PerceptHandler(this,this.getReader(),attributes);
		}
		
	}
	
	@Override
	public void endElement(String uri, String name, String qname)
	{
		LinkedList<Percept> percepts = new LinkedList<>();
		
		for(Object o : this.getParams())
		{
			Percept p = (Percept)o;
			percepts.add(p);
		}
		this.setElement(percepts);
		
		super.endElement(uri, name, qname);
		
<<<<<<< HEAD
		throw new RuntimeException("HEJHEJ");
=======
		throw new FinishedParsingException();
		
>>>>>>> 3dea9fff32315f093aeeaef4b91bd8da698822ec
	}

}
