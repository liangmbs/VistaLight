using SimpleJSON;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Basic logger implementation.
public class Logger : ILogger
{
	private IAdapter[] m_adapters = null;

	private string m_player_id = null;
	private string m_session_id = null;
	private string m_run_id = null;
	private uint m_run_count = 0;
	private uint m_action_count = 0;



	// Check if session started.
	public
	bool
	IsSessionBegun()
	{
		return m_session_id != null;
	}

	// Begin the session.
	public
	void
	BeginSession(IAdapter[] adapters, string game_id, string player_id, string build_id, string version, string condition, JSONClass details)
	{
		BeginSessionImpl(adapters, game_id, player_id, build_id, version, condition, details, false);
	}

	private
	void
	BeginSessionImpl(IAdapter[] adapters, string game_id, string player_id, string build_id, string version, string condition, JSONClass details, bool auto)
	{
		if (m_session_id != null) {
			Debug.Log("Previous session not ended");
			EndSessionImpl(null, true);
		}

		if (adapters == null || adapters.Length == 0) {
			adapters = new IAdapter[] { new NullAdapter() };
		}
		m_adapters = adapters;

		m_player_id = player_id;
		m_session_id = System.Guid.NewGuid().ToString();
		m_run_id = null;
		m_run_count = 0;
		m_action_count = 0;


		foreach (IAdapter adapter in m_adapters) {
			adapter.Init();
		}



		JSONClass data = new JSONClass();
		data["game_id"] = StringOrEmpty(game_id);
		data["player_id"] = StringOrEmpty(m_player_id);
		data["session_id"] = StringOrEmpty(m_session_id);
		data["build_id"] = StringOrEmpty(build_id);
		data["version"] = StringOrEmpty(version);
		data["condition"] = StringOrEmpty(condition);
		data["client_time"] = GetTimestampNow().ToString();
		data["details"] = ClassOrEmpty(details);

		JSONClass node = new JSONClass();
		node["type"] = "session_begin";
		if (auto) {
			node["auto"] = "true";
		}
		node["data"] = data;

		foreach (IAdapter adapter in m_adapters) {
			adapter.Handle(node);
		}
	}

	// End the current session.
	public
	void
	EndSession(JSONClass details)
	{
		EndSessionImpl(details, false);
	}
	
	public
	void
	EndSessionImpl(JSONClass details, bool auto)
	{
		if (m_run_id != null) {
			Debug.Log("Run not ended");
			EndRunImpl(null, true);
		}
		if (m_session_id == null) {
			Debug.Log("No session to end");
		}



		JSONClass data = new JSONClass();
		data["session_id"] = StringOrEmpty(m_session_id);
		data["run_count"] = m_run_count.ToString();
		data["client_time"] = GetTimestampNow().ToString();
		data["details"] = ClassOrEmpty(details);

		JSONClass node = new JSONClass();
		node["type"] = "session_end";
		if (auto) {
			node["auto"] = "true";
		}
		node["data"] = data;

		foreach (IAdapter adapter in m_adapters) {
			adapter.Handle(node);
		}



		foreach (IAdapter adapter in m_adapters) {
			adapter.Fini();
		}


		m_player_id = null;
		m_session_id = null;
		m_run_id = null;
		m_run_count = 0;
		m_action_count = 0;
	}

	// Begin a run.
	public
	void
	BeginRun(string definition, JSONClass details)
	{
		BeginRunImpl(definition, details, false);
	}

	private
	void
	BeginRunImpl(string definition, JSONClass details, bool auto)
	{
		if (m_session_id == null) {
			Debug.Log("Run not in session");
			BeginSessionImpl(null, null, null, null, null, null, null, true);
		}
		if (m_run_id != null) {
			Debug.Log("Previous run not ended");
			EndRunImpl(null, true);
		}



		m_run_id = System.Guid.NewGuid().ToString();
		m_action_count = 0;

		
		
		JSONClass data = new JSONClass();
		data["session_id"] = StringOrEmpty(m_session_id);
		data["run_id"] = StringOrEmpty(m_run_id);
		data["run_seqno"] = m_run_count.ToString();
		data["client_time"] = GetTimestampNow().ToString();
		data["details"] = ClassOrEmpty(details);

		JSONClass node = new JSONClass();
		node["type"] = "run_begin";
		if (auto) {
			node["auto"] = "true";
		}
		node["data"] = data;

		foreach (IAdapter adapter in m_adapters) {
			adapter.Handle(node);
		}
	}

	// End the current run.
	public
	void
	EndRun(JSONClass details)
	{
		EndRunImpl(details, false);
	}

	private
	void
	EndRunImpl(JSONClass details, bool auto)
	{
		if (m_run_id == null) {
			Debug.Log("No run to end");
		}



		JSONClass data = new JSONClass();
		data["run_id"] = StringOrEmpty(m_run_id);
		data["action_count"] = m_action_count.ToString();
		data["client_time"] = GetTimestampNow().ToString();
		data["details"] = ClassOrEmpty(details);

		JSONClass node = new JSONClass();
		node["type"] = "run_end";
		if (auto) {
			node["auto"] = "true";
		}
		node["data"] = data;

		foreach (IAdapter adapter in m_adapters) {
			adapter.Handle(node);
		}



		m_run_id = null;
		++ m_run_count;
		m_action_count = 0;
	}

	// Log an action.
	public
	void
	TakeAction(int type, JSONClass details)
	{
		if (m_run_id == null) {
			Debug.Log("Action not in run");
			BeginRunImpl(null, null, true);
		}



		JSONClass data = new JSONClass();
		data["run_id"] = StringOrEmpty(m_run_id);
		data["action_seqno"] = m_action_count.ToString();
		data["type"] = type.ToString();
		data["client_time"] = GetTimestampNow().ToString();
		data["details"] = ClassOrEmpty(details);

		JSONClass node = new JSONClass();
		node["type"] = "action";
		node["data"] = data;

		foreach (IAdapter adapter in m_adapters) {
			adapter.Handle(node);
		}



		++ m_action_count;
	}

	public
	string
	Error()
	{
		string ret = null;

		foreach (IAdapter adapter in m_adapters) {
			string adapter_error = adapter.Error();
			if (adapter_error != null) {
				if (ret == null) {
					ret = adapter_error;
				} else {
					ret = ret + "; " + adapter_error;
				}
			}
		}

		return ret;
	}



	private
	string
	StringOrEmpty(string str)
	{
		if (str == null) {
			return "";
		} else {
			return str;
		}
	}
	
	private
	JSONClass
	ClassOrEmpty(JSONClass cls)
	{
		if (cls == null) {
			return new JSONClass();
		} else {
			return cls;
		}
	}

	private
	double
	GetTimestampNow()
	{
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
		return timestamp;
	}
}
