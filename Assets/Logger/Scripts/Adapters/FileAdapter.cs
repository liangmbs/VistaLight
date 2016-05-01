using SimpleJSON;
using System.IO;
using UnityEngine;

// Adapter for logging to a file.
public class FileAdapter : BaseAdapter
{
	public string logfilePrefix;

	private StreamWriter m_out = null;
	private bool m_inited = false; // has init been called?
	private string m_error = null; // a string describing any error

	// Try to initialize logging to the given file.  If logging has already been initialized, do nothing.
	public
	override
	void
	Init()
	{
		if (m_inited) {
			Debug.Log("Re-initializing logger");
			return;
		}

		m_inited = true;

		try {
			m_out = new StreamWriter(logfilePrefix + "-" + System.Guid.NewGuid().ToString() + ".json");
			
			m_out.Write("# version:1\n");
			m_out.Flush();
		} catch (System.Exception ex) {
			m_error = ex.Message;
			Debug.Log(m_error);
		}
	}
	
	// Finish file logging.
	public
	override
	void
	Fini()
	{
		if (m_error != null) {
			return;
		}
		if (!m_inited) {
			Debug.Log("Finalizing uninitialized logger");
			return;
		}

		try {
			m_out.Flush();
			m_out.Close();
			m_out = null;
		} catch (System.Exception ex) {
			m_error = ex.Message;
			Debug.Log(m_error);
		}
	}
	
	public
	override
	void
	Handle(JSONClass node)
	{
		if (m_error != null) {
			return;
		}
		if (!m_inited) {
			Debug.Log("Logger not initialized");
			return;
		}
		
		try {
			string node_output = node.ToString().Trim().Replace("\n", " ") + "\n";
			
			m_out.Write(node_output);
			m_out.Flush();
		} catch (System.Exception ex) {
			m_error = ex.Message;
			Debug.Log(m_error);
		}
	}
	
	// If there has been any error in file handling, return a description of that error, otherwise, null.
	public
	override
	string
	Error()
	{
		return m_error;
	}
}
