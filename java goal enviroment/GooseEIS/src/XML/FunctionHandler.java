package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Function;
import eis.iilang.Numeral;
import eis.iilang.Parameter;

public class FunctionHandler  extends ParameterCollectionHandler{

	private String funcname;
	public FunctionHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		this.funcname = attributes.getValue(0);
	}
	
	@Override
	public void endElement(String uri, String name, String qname)
	{

		LinkedList<Parameter> params = new LinkedList<>();
		for(Object o : this.getParams())
		{
			params.add((Parameter)o);			
		}
		
		this.setElement(new Function(funcname, params));
		
		super.endElement(uri, name, qname);
	}



}
