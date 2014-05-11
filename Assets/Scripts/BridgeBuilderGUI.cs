using UnityEngine;
using System.Collections;

public class BridgeBuilderGUI : MonoBehaviour {

	public BridgeSetup bridgeSetup;

	public static bool ClickedOnGUI() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.y = Screen.height - mousePos.y;
		return Input.GetMouseButtonDown(0) && windowRect.Contains(mousePos);
	}

	private static Rect windowRect = new Rect(10, 10, 600, 66);

	void OnGUI () {
		//Debug.Log ("Clicked GUI "+clickedGUI+" Mouse pos: "+mousePos);
		GUI.Box(windowRect, "");

		GUI.Label(new Rect(125, 18, 400, 20), "Shift-Click - Add. Ctrl-Click - Delete. Click - Move.");
		GUI.Label(new Rect(125, 36, 400, 20), "Cost: "+bridgeSetup.GetBridgeCost()+"$");

		if (BridgeSetup.eLevelStage.PlayStage == bridgeSetup.LevelStage) {
			if (GUI.Button (new Rect (18,18,100,50), "Back to Draw")) {
				bridgeSetup.LevelStage = BridgeSetup.eLevelStage.SetupStage;
			}
		} else if (BridgeSetup.eLevelStage.SetupStage == bridgeSetup.LevelStage) {
			if (GUI.Button (new Rect (18,18,100,50), "Test Bridge")) {
				bridgeSetup.LevelStage = BridgeSetup.eLevelStage.PlayStage;
			}
		}


	}
}
