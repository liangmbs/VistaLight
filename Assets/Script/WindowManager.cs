using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Collections.Generic; 

public class WindowManager: MonoBehaviour {

	public Button windowscontroller;
	public GameObject infoWindow;
	public GameObject infoBar;
	public bool open = false;

	RectTransform rt;
	public float slideAmount =80;
	public float slideTime = .6f;
	public float slideTimer = 0;
	private float homeY = -80.0f;
	private float endY = 80.0f;


	SortedDictionary<int, GameObject> ships = 
		new SortedDictionary<int, GameObject> ();
	
	public List<GameObject> selectedUnits= new List<GameObject>();
	public GameObject newbar;
	public GameObject bar;
	public GameObject title;
	public Transform shipInformationwindow;

	void Start() 
	{
		rt = (RectTransform)infoWindow.transform;
		windowscontroller.onClick.AddListener (() => Controler());


		for (int i = 1; i <= 30; i++) {
			bar = Instantiate(newbar, new Vector3(290, title.transform.position.y - i * 33.0F, 0), Quaternion.identity)as GameObject;
			bar.transform.SetParent(shipInformationwindow);
		}


	}
	
	void Update()
	{
		if (slideTimer < 0)
		{
			slideTimer = 0;
		}
		if (slideTimer > 0)
		{
			slideTimer -= Time.deltaTime;
		}
	}
	
	void OnGUI(){

		/*controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());*/
		if (open == true) {
			windowscontroller.transform.rotation = Quaternion.Euler(0,0,180);
			rt.anchoredPosition = new Vector2
				(rt.anchoredPosition.x, Mathf.Lerp (homeY , endY, 1 - (slideTimer / slideTime)));
			//GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = false;
			
		}
		
		if (open == false) {
			windowscontroller.transform.rotation = Quaternion.Euler(0,0,0);
			rt.anchoredPosition = new Vector2
				( rt.anchoredPosition.x, Mathf.Lerp(endY,homeY, 1 - (slideTimer / slideTime)));
			//GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = true;
			
		}
	}
	
	
	void Controler(){
		open = !open;
		slideTimer = slideTime;
	}





	public void RefreshList(){
		
		Empty ();
		
		
	}
	
	
	public void Empty(){
		
		
	}


	



}


	/*
	private bool showproperty;
	public Collider2D hitObject;
	public GameObject propertywindow;
	private GameObject window;

	void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (Input.GetMouseButtonDown (0)) {
			if(hit.collider != null)
			{
				hitObject = hit.collider;
				GameObject shipObject = GameObject.Find (hitObject.name);
				ClickOnShip(shipObject);
			}
		}
	}

	void OpenWindow() {
		window = Instantiate (propertywindow, transform.position, transform.rotation) as GameObject;
		GameObject shipObject = GameObject.Find (hitObject.name);
		window.name = "window";
	}
	
	public void ClickOnShip(GameObject hit){
		GameObject detection = GameObject.Find ("window");
		if (detection == null) {
			OpenWindow ();	
		}
		GameObject replacedshipObjct = hit;
		SetWindowShip(replacedshipObjct);
	}

	void SetWindowShip(GameObject shipobject){
		window.GetComponentInChildren<Window> ().setShip (shipobject);
	}
*/
	/*
	void Drawpropertywindow(bool showproperty,RaycastHit2D hit){
		bool show = true;
		bool notdoinganything = true;
		if (hitObject == null) {
			show = true;
			notdoinganything = false;
		}
		else if(hitObject != hit.collider){
			show = false;
			notdoinganything = false;
		}
		else if (hitObject == hit.collider){
			show = false;
			notdoinganything = true;

		}

		if (show == true && notdoinganything == false) {
			window = Instantiate (propertywindow, transform.position, transform.rotation) as GameObject;
			hitObject = hit.collider;
			GameObject shipObject = GameObject.Find (hitObject.name);
			window.name = "window";
			window.GetComponentInChildren<Window> ().setShip (shipObject);
		}
		if (show == false && notdoinganything == false) {
			Destroy (window);
		} 
		if (notdoinganything) {
	
		}
	}
	*/
	
	