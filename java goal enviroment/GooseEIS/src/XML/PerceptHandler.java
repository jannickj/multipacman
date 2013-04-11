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

public class PerceptHandler extends ParameterCollectionHandler
{
	


	public PerceptHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		// TODO Auto-generated constructor stub
		this.setElement(new Percept(attributes.getValue(0)));
	}


	@Override
	public void endElement(String uri, String name, String qname)
	{

		if("perceptParameter".equals(name))
			return;
		
		for(Object o : this.getParams())
		{
			Parameter p = (Parameter)o;
			this.<Percept>getElementAs().addParameter(p);
		}
		
		super.endElement(uri, name, qname);
	}
	
	
	
}
