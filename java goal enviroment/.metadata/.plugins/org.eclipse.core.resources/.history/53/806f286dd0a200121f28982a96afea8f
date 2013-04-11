package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Parameter;
import eis.iilang.Percept;

public class ParameterCollectionHandler extends ParameterHandler {

	public ParameterCollectionHandler(ParameterHandler parent,
			XMLReader reader, Attributes attributes) {
		super(parent, reader, attributes);
		
	}

	@Override
	public void startElement (String uri, String name, String qname, Attributes attributes)
	{
		ParameterHandler handler = null;
		if (name.equals ("identifier"))
			handler = new IdentifierHandler(this,this.getReader(),attributes);
		
		if (name.equals ("number"))
			handler = new NumeralHandler(this,this.getReader(),attributes);
		
	
		if (name.equals ("parameterList")) 
			handler = new ParameterListHandler(this,this.getReader(), attributes);
			
		if (name.equals ("function")) 
			handler = new FunctionHandler(this,this.getReader(),attributes);
		
		if (name.equals("perceptParameter"))
			return;
		
		if(handler == null)
		{
			try {
				throw new Exception("Unknown XML node named: "+name);
			} catch (Exception e) { e.printStackTrace();}
		}
		
		
	}
	
	@Override
	protected void endInternElement(String uri, String name, String qname)
	{
		this.setElement(this.getParams());
	}

}
