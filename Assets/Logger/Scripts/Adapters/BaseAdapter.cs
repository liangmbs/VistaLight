using SimpleJSON;
using UnityEngine;

public abstract class BaseAdapter : MonoBehaviour, IAdapter
{
	public abstract void Init();
	
	public abstract void Fini();
	
	public abstract void Handle(JSONClass node);

	public abstract string Error();
}
