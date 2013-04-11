package XML;

import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.LinkedList;
import java.util.List;

import junit.framework.Assert;

import org.junit.Test;
import org.omg.Dynamic.Parameter;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.XMLReaderFactory;

import eis.iilang.Function;
import eis.iilang.Identifier;
import eis.iilang.Numeral;
import eis.iilang.ParameterList;
import eis.iilang.Percept;

public class PeceptHandlerTest {

	@Test
	public void XmlParsing_simplePerceptWithIdentifier_ReturnsAPerceptWithAnIdentifier() throws SAXException, IOException 
	{
		String xml ="<perceptCollection><percept name=\"percept_name\">" +
						"<perceptParameter>" +
							"<identifier value=\"identifer_value\"></identifier>" +
						"</perceptParameter>"+
					"</percept></perceptCollection>";
		XMLReader reader = XMLReaderFactory.createXMLReader();
		PerceptCollectionHandler handler = new PerceptCollectionHandler(reader);
		reader.setContentHandler(handler);
		InputStream is = new ByteArrayInputStream(xml.getBytes());
		InputSource sc = new InputSource(is);
		reader.parse(sc);
		
		Percept p = handler.<LinkedList<Percept>>getElementAs().getFirst();
		
		Assert.assertEquals("percept_name", p.getName());
		
		
		Assert.assertEquals("identifer_value", ((Identifier)p.getParameters().getFirst()).getValue());
	}
	
	@Test
	public void ComplexXmlParsing_AllTypesOfParametersInTwoPercepts_ReturnsTwoPerceptsWithAllTypesOfParameters() throws Exception
	{
		String xml = "<perceptCollection>" +
					"<percept name=\"percept1\">" +
						"<perceptParameter>" +
							"<identifier value=\"identifier1\"></identifier>" +
						"</perceptParameter>"+
						"<perceptParameter>" +
							"<parameterList>" +
								"<number value=\"2.0\" />" +
								"<function name=\"function_name\">" +
									"<identifier value=\"identifier_fun\" />" +
									"<number value=\"42\" />" +
								"</function>" +
							"</parameterList>" +
						"</perceptParameter>" +
					"</percept>" +
					"<percept name=\"percept2\">" +
						"<perceptParameter>" +
							"<identifier value=\"identifier2\"></identifier>" +
						"</perceptParameter>" +
					"</percept>" +
				"</perceptCollection>";
		
		XMLReader reader = XMLReaderFactory.createXMLReader();
		PerceptCollectionHandler handler = new PerceptCollectionHandler(reader);
		reader.setContentHandler(handler);
		InputStream is = new ByteArrayInputStream(xml.getBytes());
		InputSource sc = new InputSource(is);
		reader.parse(sc);
		
		LinkedList<Percept> pc = handler.<LinkedList<Percept>>getElementAs();
		Percept p1 = pc.getFirst();
		Percept p2 = pc.getLast();
		ParameterList pl = (ParameterList)p1.getParameters().getLast();
		LinkedList<eis.iilang.Parameter> pll = new LinkedList<eis.iilang.Parameter>(); 
		for (eis.iilang.Parameter p : pl)
		{
			pll.add(p);
		}
		
		Numeral pl_num = (Numeral) pll.getFirst();
		Function fun = (Function) pll.getLast();
		
		for (eis.iilang.Parameter p : pl)
		{
			if (p.getClass().isInstance(Numeral.class))
				pl_num = (Numeral) p;
			if (p.getClass().isInstance(Function.class))
				fun = (Function) p;
		}
		
		Identifier fun_id = (Identifier) fun.getParameters().getFirst();
		Numeral fun_num = (Numeral) fun.getParameters().getLast();
	
		Assert.assertEquals(2, pc.size());
		Assert.assertEquals("percept1", p1.getName());
		Assert.assertEquals("percept2", p2.getName());
		Assert.assertEquals(2.0, pl_num.getValue());
		Assert.assertEquals("function_name", fun.getName());
		Assert.assertEquals("identifier_fun", fun_id.getValue());
		Assert.assertEquals(42.0, fun_num.getValue());
		Assert.assertEquals("identifier1", ((Identifier)p1.getParameters().getFirst()).getValue());
		Assert.assertEquals("identifier2", ((Identifier)p2.getParameters().getFirst()).getValue());
	}

}
