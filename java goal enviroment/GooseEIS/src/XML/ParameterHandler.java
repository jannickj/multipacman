package XML;

import java.util.LinkedList;

import org.xml.sax.Attributes;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.DefaultHandler;
import eis.iilang.IILElement;

public abstract class ParameterHandler extends DefaultHandler 
{
	
	private XMLReader reader;
	private Object element;
	private LinkedList<Object> params = new LinkedList<>();
	private Attributes attributes;
	
	ParameterHandler parent;
	
	public ParameterHandler(ParameterHandler parent, XMLReader reader, Attributes attributes)
	{
		this.attributes = attributes;
		this.reader = reader;
		this.parent = parent;
		this.getReader().setContentHandler(this);
	}
	
	@Override
	public void endElement (String uri, String name, String qname)
	{
		if(parent != null)
		{
			this.parent.params.add(this.element);
			reader.setContentHandler(parent);
		}
	}
	
	

	protected LinkedList<Object> getParams()
	{
		return this.params;
	}
	
	protected XMLReader getReader()
	{
		return reader;
	}
	
	protected void setElement(Object element)
	{
		this.element = element;
	}
	
	public Object getElement()
	{
		return this.element;
	}
	
	public <T> T getElementAs() {
	    return (T)getElement();
	}
	
	public Attributes getAttributes()
	{
		return this.attributes;
	}

}
