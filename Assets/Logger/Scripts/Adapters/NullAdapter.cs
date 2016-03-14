using SimpleJSON;

// Null adapter that ignores logging.
public class NullAdapter : IAdapter
{
	public
	NullAdapter()
	{
	}
	
	public
	void
	Init()
	{
	}
	
	public
	void
	Fini()
	{
	}
	
	public
	void
	Handle(JSONClass node)
	{
	}

	public
	void
	DebugPrint(string msg)
	{
	}

	public
	string
	Error()
	{
		return null;
	}
}
