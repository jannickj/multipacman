package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Function;
import eis.iilang.Parameter;
import eis.iilang.ParameterList;

public class ParameterListHandler extends ParameterHandler {


	
	public ParameterListHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		new ParameterCollectionHandler(this, reader, attributes);
	}
		
		
		
	@Override
	public void endElement(String uri, String name, String qname)
	{
		LinkedList<Parameter> params = new LinkedList<>();
		for(Object o : this.getParams())
		{
			params.add((Parameter)o);			
		}
		
		this.setElement(new ParameterList(params));
		
		super.endElement(uri, name, qname);
	}
	
	

}
