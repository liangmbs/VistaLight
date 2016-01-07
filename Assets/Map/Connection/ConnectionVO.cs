using UnityEngine;
using System.Collections;

public class ConnectionVO : MonoBehaviour {

	public Connection connection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Node startNode = connection.StartNode;
		Node endNode = connection.EndNode;

		Vector3 startPosition = new Vector3((float)startNode.X, (float)startNode.Y, (float)MapController.MapZIndex);
		Vector3 endPosition = new Vector3((float)endNode.X, (float)endNode.Y, (float)MapController.MapZIndex);

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
