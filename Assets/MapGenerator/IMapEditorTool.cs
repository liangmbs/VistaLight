using UnityEngine;
using System.Collections;

public interface IMapEditorTool {

	void RespondMouseLeftClick();
	void RespondMouseLeftUp();
	void RespondMouseMove(float x, float y);

	void Destory();

}
