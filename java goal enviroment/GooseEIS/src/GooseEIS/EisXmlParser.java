package GooseEIS;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.Queue;

import org.xml.sax.Attributes;
import org.xml.sax.helpers.DefaultHandler;

import eis.iilang.Function;
import eis.iilang.IILElement;
import eis.iilang.Identifier;
import eis.iilang.Numeral;
import eis.iilang.Parameter;
import eis.iilang.ParameterList;
import eis.iilang.Percept;

public class EisXmlParser extends DefaultHandler
{	
	LinkedList<LinkedList<Parameter>> parameters = new LinkedList<LinkedList<Parameter>>();
	private Percept percept;
	
	public EisXmlParser ()
	{
		
	}
	
	@Override
	public void startElement (String uri, String name, String qname, Attributes attributes)
	{
		String Attr = attributes.getLocalName(0);
		
		if (name.equals ("percept"))
			percept = new Percept(Attr);

		if (name.equals ("identifier"))
			parameters.getFirst().add (new Identifier (Attr));
		
		if (name.equals ("number"))
			parameters.getFirst().add (new Numeral(Double.parseDouble(Attr)));
		
		if (name.equals ("parameterList")) {
			parameters.getFirst().add(new ParameterList());
			parameters.push(new LinkedList<Parameter>());
		}
			
		if (name.equals ("function")) {
			parameters.getFirst().add(new Function(Attr));
			parameters.push(new LinkedList<Parameter>());
		}
	}
	
	@Override
	public void endElement (String uri, String name, String qname)
	{
		if (name.equals("parameterList")) {
			parameters.pop();
		}
			
	}
	
	private IILElement StringToParameter (String s, String Attr)
	{
		if (s.equals ("percept"))
			return new Percept(Attr);
		if (s.equals ("perceptParameter"))
		if (s.equals ("identifier"))
			return new Identifier(Attr);
		if (s.equals ("number"))
			return new Numeral(Double.parseDouble(Attr));
		
		return null;
	}
}
