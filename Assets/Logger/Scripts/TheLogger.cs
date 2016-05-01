// Utility class to hold persistent singleton instance of a logger.
public class TheLogger
{
	private static ILogger o_instance;

	// get the logger instance
	public static ILogger instance
	{
		get
		{
			// not threadsafe!?
			if(o_instance == null) {
				o_instance = new Logger();
			}
			return o_instance;
		}
	}
}
