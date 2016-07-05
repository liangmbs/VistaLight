using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public ShipController shipController;
	public ShipListController shipListController; 

	public static GameObject itemBeginDragged;

	Vector3 startPosition;


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeginDragged = null;
		transform.position = startPosition;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeginDragged = gameObject;
		startPosition = transform.position;
	}

	#endregion


}


