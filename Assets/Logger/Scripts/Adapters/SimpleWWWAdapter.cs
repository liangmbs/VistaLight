using SimpleJSON;
using UnityEngine;

// Adapter for logging through HTTP POST using Unity's WWW API.
public class SimpleWWWAdapter : BaseAdapter
{
	public string url;

	public
	override
	void
	Init()
	{
	}
	
	public
	override
	void
	Fini()
	{
	}
	
	public
	override
	void
	Handle(JSONClass node)
	{
		string node_output = node.ToString().Trim().Replace("\n", " ") + "\n";

		WWWForm form = new WWWForm();
		form.AddField("data", node_output);

		new WWW(url, form);
	}

	// If there has been any error, return a description of that error, otherwise, null.
	public
	override
	string
	Error()
	{
		return null;
	}
}
