using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DockVO : MonoBehaviour, MapSelectableVO {

	public Dock dock;

	public GameObject dockInfoPanel;
	public Text industryTypeText;
	public Text UtilizationText;

	public Dock Dock { 
		get { return dock; }
		set { 
			dock = value; 
			industryTypeText.text = string.Format("Industry Type: {0}", dock.type.ToString ());
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	public void Update() {
		gameObject.transform.position = new Vector3(
				(float)dock.node.X,
				(float)dock.node.Y,
				-2);
		/*
		gameObject.transform.localScale = new Vector3(
				(float)(Camera.main.orthographicSize / 10),
				(float)(Camera.main.orthographicSize / 10),
				(float)1);
		*/

		foreach (IndustryType type in Enum.GetValues(typeof(IndustryType))) {
			if (type == dock.type) {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = true;
			} else {
				gameObject.transform.FindChild(type.ToString()).GetComponent<SpriteRenderer>().enabled = false;
			}
		}

		UpdateUtilization ();

		CheckClick ();
	}

	private void UpdateUtilization() {
		GameObject timerGO = GameObject.Find ("Timer");
		if (timerGO == null) {
			return;
		}
		Timer timer = timerGO.GetComponent<Timer> ();

		double utilization = 0;
		TimeSpan totalTime = timer.VirtualTime - timer.gameStartTime;
		if (totalTime.TotalSeconds == 0) {
			utilization = 0;
		} else {
			utilization = dock.utilizedTime.TotalSeconds / totalTime.TotalSeconds;
		}

		UtilizationText.text = string.Format ("{0:P2}", utilization);
	}


	public void OnMouseDrag() {
		SceneSetting sceneSetting = GameObject.Find("SceneSetting").GetComponent<SceneSetting>();
		if (sceneSetting.AllowMapEditing) {
			RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			dock.node.X = ray.point.x;
			dock.node.Y = ray.point.y;
		}
	}

	public void Select() {
		gameObject.transform.FindChild("SelectCircle").gameObject.SetActive(true);
	}

	public void Deselect() {
		gameObject.transform.FindChild("SelectCircle").gameObject.SetActive(false);
	}

    public GameObject GetSidePanel()
    {
        return null;
    }

	private void CheckClick() {
		if (SceneSetting.Instance.AllowMapEditing) {
			return;
		}

		if (!Input.GetMouseButtonDown (0)) {
			return;
		}

		RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (ray.collider == null) return;

		if (ray.collider.gameObject == gameObject) {
			if (dockInfoPanel.activeSelf) {
				dockInfoPanel.SetActive (false);
			} else {
				dockInfoPanel.SetActive (true);
			}
		}

	}

}
