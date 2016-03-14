using SimpleJSON;

// Interface for the logger.
public interface ILogger
{
	// Check if session has begun.
	bool IsSessionBegun();
	
	// Begin the session.  Log to the given adapters.
	void BeginSession(IAdapter[] adapters, string game_id, string player_id, string build_id, string version, string condition, JSONClass details);

	// End the current session.
	void EndSession(JSONClass details);

	// Begin a run.
	void BeginRun(string definition, JSONClass details);

	// End the current run.
	void EndRun(JSONClass details);

	// Log an action.
	void TakeAction(int type, JSONClass details);

	// Return any error encountered, otherwise null.
	string Error();
}
