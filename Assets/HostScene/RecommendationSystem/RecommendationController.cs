using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecommendationController : MonoBehaviour {

	private Recommendation recommendation;

	public Text content;
	public Image AcceptImage;
	public Image DenyImage;
	public Button AccpetButton;
	public Button DenyButton;

	public bool isProcessed = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Accept() {
		DisableButtons ();
		PerformeRecommendation ();
		AcceptImage.gameObject.SetActive (true);
		isProcessed = true;
	}

	public void PerformeRecommendation() {
		
	}

	public void Deny() {
		DisableButtons ();
		DenyImage.gameObject.SetActive (true);
		isProcessed = true;
	}

	public void EnableButtons() {
		AccpetButton.gameObject.SetActive (true);
		DenyButton.gameObject.SetActive (true);
	}

	public void DisableButtons() {
		AccpetButton.gameObject.SetActive (false);
		DenyButton.gameObject.SetActive (false);
	}

	public void HideStamps() {
		AcceptImage.gameObject.SetActive (false);
		DenyImage.gameObject.SetActive (false);
	}

	public void SetRecommendation(Recommendation recommendation) {
		this.recommendation = recommendation;

		content.text = string.Format ("Consider prioritize ship {0} to priority {1}. {2}",
			recommendation.ship.Ship.Name, recommendation.desiredPriority,
			recommendation.justification);

		isProcessed = false;

		EnableButtons ();
		HideStamps ();
	}

	public Recommendation GetRecommendation() {
		return recommendation;
	}
}
