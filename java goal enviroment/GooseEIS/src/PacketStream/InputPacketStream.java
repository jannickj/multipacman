package PacketStream;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.ByteBuffer;

public class InputPacketStream
{
	InputStream stream;
	
	public InputPacketStream(InputStream stream)
	{
		this.stream = stream;
	}
	
	public ByteArrayInputStream getPacketStream() throws Exception
	{
		int packLen = PackageLength();
		byte[] bytes = new byte[packLen];
		int bytesRead = 0;
				
		do 
		{
			bytesRead += stream.read(bytes, bytesRead, packLen - bytesRead);
					
		} while (bytesRead < packLen);
		
		return new ByteArrayInputStream(bytes);
	}
	
	private int PackageLength() throws Exception
	{
		byte[] lengthBytes = new byte[4];
		
		for (int i = 0; i < 4; i++)
		{
			int nextByte = stream.read();
			
			if (nextByte == -1)
				throw new Exception();
			
			lengthBytes[i] = (byte) nextByte;
		}
		
		return ByteArrayToLong(lengthBytes);
	}
	
	private int ByteArrayToLong(byte[] bytes)
	{
		ByteBuffer buf = ByteBuffer.allocate(4);
		
		buf.put(bytes);
		buf.flip();
		return buf.getInt();
	}
}
