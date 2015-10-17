using UnityEngine;
using System.Collections;

public interface IMapEditorTool {

	void RespondMouseClick();
	void RespondMouseMove(float x, float y);

	void Destory();

}
