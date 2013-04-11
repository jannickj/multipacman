package XML;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Identifier;
import eis.iilang.Parameter;
import eis.iilang.Percept;

public class IdentifierHandler extends ParameterHandler {

	
	public IdentifierHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		// TODO Auto-generated constructor stub
	}
	
	@Override
	protected void endInternElement(String uri, String name, String qname)
	{
		String Attr = this.getAttributes().getValue(0);
		this.setElement(new Identifier (Attr));
	}


}
