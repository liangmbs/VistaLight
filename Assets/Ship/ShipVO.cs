using UnityEngine;
using System.Collections;
using System;

public class ShipVO : MonoBehaviour {

	public Ship ship;

	private GameObject thumbnail = null;
	public GameObject Thumbnail { 
		get {
			if (thumbnail == null) {
				thumbnail = transform.Find("thumbnail").gameObject;
            }
			return thumbnail;
		}
	}

	private GameObject background = null;
	public GameObject Background {
		get {
			if (background == null) {
				background = transform.Find("background").gameObject;
			}
			return background;
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	private void AdaptShapeByCameraHeight() {

		transform.localScale = new Vector3(
					(float)(Camera.main.orthographicSize / 50),
					(float)(Camera.main.orthographicSize / 50),
					(float)1);

		if (Camera.main.orthographicSize < 5000) {
			transform.Find("background").gameObject.SetActive(true);
			transform.Find("thumbnail").gameObject.SetActive(false);
		} else {
			transform.Find("background").gameObject.SetActive(false);
			Thumbnail.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
			transform.position = new Vector3 (
				(float)ship.X,
				(float)ship.Y,
				-2);

			UpdateShipHeading ();

			AdaptShapeByCameraHeight ();
			ChangeThumbnailColorByIndustry ();
	}

	private void UpdateShipHeading() {
		double heading = gameObject.GetComponent<ShipController>().heading;

		Thumbnail.transform.eulerAngles = new Vector3(0, 0, (float)heading);
		Background.transform.eulerAngles = new Vector3(0, 0, (float)heading);
	}

	private void ChangeThumbnailColorByIndustry() {
		Color color = IndustryColor.GetIndustryColor(ship.Industry);
        Thumbnail.GetComponent<SpriteRenderer>().color = color;	
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			ship.SerializeTo (stream);
		} else {
			ship.DeserializeFrom (stream);
		}
	}
}
