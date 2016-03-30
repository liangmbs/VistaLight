using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Texture2D pointer;

	// Use this for initialization
	void Start () {
		// pointer = Resources.Load ("image/hand_mouse", typeof(Texture2D)) as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter(PointerEventData eventData) {
		Cursor.SetCursor (pointer, Vector2.zero, CursorMode.Auto);
	}

	public void OnPointerExit(PointerEventData eventData) {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);	
	}

	/*
	void OnMouseEnter() {
		Cursor.SetCursor (pointer, Vector2.zero, CursorMode.Auto);
	}

	void OnMouseExit() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}
	*/
}
