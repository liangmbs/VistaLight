using System;
using UnityEngine;

public class RoadTool : IMapEditorTool
{
	public MapController mapController;
	public float segmentLength = 1000;

	private GameObject currentNode = null;
	private bool isBiDirection = true;

	public RoadTool (MapController mapController)
	{
		this.mapController = mapController;
	}

	public bool BiDirection {
		get { return isBiDirection; }
		set { isBiDirection = value;  }
	}


	private void CreateNewRoad(Vector3 startPosition, Vector3 targetPosition) {
		mapController.DeselectNode(currentNode);
		float remainingDistance = Vector3.Distance(startPosition, targetPosition);

		// Get the direction from the start point to the destination
		Vector3 vector = (targetPosition - startPosition).normalized;

		// Create a series of nodes towards the destination
		Vector3 previousPosition = startPosition;
		GameObject nextNode;
		while (remainingDistance > 2 * segmentLength) {
			// Create next node
			Vector3 nextPosition = previousPosition + vector * segmentLength;
			nextNode = mapController.AddNode(nextPosition);

			// Create connection
			GameObject connection = mapController.AddConnection(currentNode, nextNode, isBiDirection);

			// Update for the next iteration
			currentNode = nextNode;
			previousPosition = nextPosition;
			remainingDistance = (targetPosition - previousPosition).magnitude;
		}

		// At the end, create a node at the target position
		nextNode = mapController.AddNode(targetPosition);
		mapController.AddConnection(currentNode, nextNode, isBiDirection);
		previousPosition = nextNode.gameObject.transform.position;

		
        currentNode = nextNode;
		mapController.SelectNode(nextNode);
	}

	private void CreateNewRoad(Vector3 startPosition, GameObject targetNode) {
		mapController.DeselectNode(currentNode);
		float remainingDistance = Vector3.Distance(startPosition, targetNode.transform.position);

		// Get the direction from the start point to the destination
		Vector3 vector = (targetNode.transform.position - startPosition).normalized;

		// Create a series of nodes towards the destination
		Vector3 previousPosition = startPosition;
		GameObject nextNode;
		while (remainingDistance > 2 * segmentLength) {
			// Create next node
			Vector3 nextPosition = previousPosition + vector * segmentLength;
			nextNode = mapController.AddNode(nextPosition);

			// Create connection
			GameObject connection = mapController.AddConnection(currentNode, nextNode, isBiDirection);

			// Update for the next iteration
			currentNode = nextNode;
			previousPosition = nextPosition;
			remainingDistance = (targetNode.transform.position - previousPosition).magnitude;
		}

		// At the end, do not create node, but use the target node
		// nextNode = map.AddNode(targetNode.Position);
		mapController.AddConnection(currentNode, targetNode, isBiDirection);
		previousPosition = targetNode.gameObject.transform.position;


		currentNode = targetNode;
		mapController.SelectNode(targetNode);
	}

	public void RespondMouseLeftClick() {
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		Debug.Log(ray.collider);

		if (currentNode == null) {
			if (ray.collider != null) {
				if (ray.collider.tag == "Background") {
					currentNode = mapController.AddNode(ray.point);
					mapController.SelectNode(currentNode);
				} else if (ray.collider.tag == "Node") {
					currentNode = ray.collider.gameObject;
					mapController.SelectNode(currentNode);
				}
			}
		} else {
			if (ray.collider != null && ray.collider.tag == "Background") {
				Vector3 targetPosition = ray.point;
				Vector3 startPosition = currentNode.transform.position;
				this.CreateNewRoad(startPosition, targetPosition);
			} else if (ray.collider != null && ray.collider.tag == "Node") {
				if (ray.collider.gameObject != currentNode) {
					Vector3 startPosition = currentNode.transform.position;
					this.CreateNewRoad(startPosition, ray.collider.gameObject);
				}
			}
		}

	}

    public void RespondMouseMove(float x, float y) {
    }

	public void Destory() {
		mapController.DeselectNode(currentNode);
	}

	public void RespondMouseLeftUp() {
	}

	public void RespondMouseRightClick() {
		// Right click on a node, remove it
		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (ray.collider != null && ray.collider.tag == "Node") {
			if (ray.collider.gameObject == currentNode) {
				currentNode = null;
			}
			mapController.RemoveNode(ray.collider.gameObject);
		} else {
			mapController.DeselectNode(currentNode);
			currentNode = null;
		}
	}
}


