using SimpleJSON;

// Interface for a loggin adapter to allow different formats/etc of logging.
public interface IAdapter
{
	// Initalize logging adapter (called at beginning of session).
	void Init();

	// Finalize logging adapter (called at end of session).
	void Fini();

	// Handle logging event.
	void Handle(JSONClass node);

	// Get a description of any error; null if none.
	string Error();
}
