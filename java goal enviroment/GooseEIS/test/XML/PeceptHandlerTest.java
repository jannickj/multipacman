package XML;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;

import junit.framework.Assert;

import org.junit.Test;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.XMLReaderFactory;

import eis.iilang.Identifier;
import eis.iilang.Percept;

public class PeceptHandlerTest {

	@Test
	public void XmlParsing_simplePeceptWithIdentifier_ReturnsAPerceptWithAnIdentifier() throws SAXException, IOException 
	{
		String xml ="<percept name=\"percept_name\">" +
						"<perceptParameter>" +
							"<identifier value=\"identifer_value\"></identifier>" +
						"</perceptParameter>"+
					"</percept>";
		XMLReader reader = XMLReaderFactory.createXMLReader();
		PerceptHandler handler = new PerceptHandler(null, reader,null);
		reader.setContentHandler(handler);
		InputStream is = new ByteArrayInputStream(xml.getBytes());
		InputSource sc = new InputSource(is);
		reader.parse(sc);
		
		Percept p = handler.getElementAs();
		
		Assert.assertEquals("percept_name", p.getName());
		
		
		Assert.assertEquals("identifer_value", ((Identifier)p.getParameters().getFirst()).getValue());
		
		
	}

}