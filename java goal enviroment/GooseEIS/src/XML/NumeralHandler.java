package XML;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;

import eis.iilang.Identifier;
import eis.iilang.Numeral;

public class NumeralHandler extends ParameterHandler {

	public NumeralHandler(ParameterHandler parent, XMLReader reader,
			Attributes attributes) {
		super(parent, reader, attributes);
		// TODO Auto-generated constructor stub
	}

	
	@Override
	protected void endInternElement(String uri, String name, String qname)
	{
		String Attr = this.getAttributes().getValue(0);
		this.setElement(new Numeral(Double.parseDouble(Attr)));
	}
	

}
