package PacketStream;

import java.io.IOException;
import java.io.OutputStream;
import java.io.PrintStream;
import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;

public class OutputPacketStream {
	
	private OutputStream stream;
	
	public OutputPacketStream (PrintStream stream)
	{
		this.stream = stream;
	}
	
	public void SendPacket(String packet) throws IOException
	{
		byte[] bytes = packet.getBytes("UTF-8");
		
		ByteBuffer buf = ByteBuffer.allocate(4);
		
		buf.putInt(bytes.length);
		
		stream.write(buf.array());
		
		stream.write(bytes);
		stream.flush();
	}
}
