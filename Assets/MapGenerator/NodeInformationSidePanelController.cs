using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NodeInformationSidePanelController : MonoBehaviour {

	public Node node;
	public InputField xPosInput;
	public InputField yPosInput;
	public Toggle anchorPointSelector;
	public Toggle exitPointSelector;


	public void UpdateDisplay() {
		if (node != null) {
			xPosInput.text = node.X.ToString();
			yPosInput.text = node.Y.ToString();
			anchorPointSelector.isOn = node.IsAnchor;
			exitPointSelector.isOn = node.IsExit;
		}
	}

	public void UpdateData() {

		if (node != null) {
			node.X = double.Parse(xPosInput.text);
			node.Y = double.Parse(yPosInput.text);
			node.IsAnchor = anchorPointSelector.isOn;
			node.IsExit = exitPointSelector.isOn;
		}

		UpdateDisplay();
	}
}
