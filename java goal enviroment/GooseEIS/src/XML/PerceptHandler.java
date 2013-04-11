package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Function;
import eis.iilang.Identifier;
import eis.iilang.Numeral;
import eis.iilang.Parameter;
import eis.iilang.ParameterList;
import eis.iilang.Percept;

public class PerceptHandler extends ParameterHandler 
{
	


	public PerceptHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		// TODO Auto-generated constructor stub
		
	}

	@Override
	public void startElement (String uri, String name, String qname, Attributes attributes)
	{
		this.setElement(new Percept(attributes.getValue(0)));
		
		ParameterCollectionHandler handler = new ParameterCollectionHandler(this, this.getReader(), attributes);
		this.getReader().setContentHandler(handler);
		
	}
	
	@Override
	protected void endInternElement(String uri, String name, String qname)
	{
		@SuppressWarnings("unchecked")
		LinkedList<Object> l = (LinkedList<Object>) this.getParams().pop();
		
		for(Object o : l)
		{
			Parameter p = (Parameter)o;
			this.<Percept>getElementAs().addParameter(p);
		}
	}
	
	
	
}