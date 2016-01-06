using UnityEngine;
using System.Collections;

public class ConnectionVO : MonoBehaviour {

	public Connection connection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 startPosition = connection.StartNode.Position;
		Vector3 endPosition = connection.EndNode.Position;
		gameObject.transform.position = (startPosition + endPosition) / 2.0f;
		float distance = (Vector3.Distance(endPosition, startPosition));
		gameObject.transform.localScale = new Vector3(distance, distance / 10, 0.01f);
		gameObject.transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), startPosition - endPosition);

		if (connection.Bidirectional) {
			gameObject.transform.FindChild("Unidirectional").GetComponent<SpriteRenderer>().enabled = false;
			gameObject.transform.FindChild("Bidirectional").GetComponent<SpriteRenderer>().enabled = true;
		} else {
			gameObject.transform.FindChild("Unidirectional").GetComponent<SpriteRenderer>().enabled = true;
			gameObject.transform.FindChild("Bidirectional").GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
