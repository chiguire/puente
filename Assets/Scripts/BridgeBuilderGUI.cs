using UnityEngine;
using System.Collections;

public class BridgeBuilderGUI : MonoBehaviour {

	public BridgeSetup bridgeSetup;

	public static bool ClickedOnGUI() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.y = Screen.height - mousePos.y;
		return Input.GetMouseButtonDown(0) && windowRect.Contains(mousePos);
	}

	private static Rect windowRect = new Rect(10, 10, Screen.width-20, 66);
	private static Rect winningRect = new Rect(10, Screen.height-200, Screen.width-20, 150);
	private static Rect winningLabelRect = new Rect(winningRect.center.x-50, winningRect.center.y-9, 100, 18);

	private bool timeToggle = false;
	//private bool showForce = false;

	void OnGUI () {
		if (!bridgeSetup.finishedLevel) {
			GUI.Box(windowRect, "");
	
			GUI.Label(new Rect(18, 18, 300, 20), bridgeSetup.GetLevelName());
			GUI.Label(new Rect(18, 36, 300, 20), "Click - Add. Ctrl-Click - Delete.");
			GUI.Label(new Rect(18, 54, 300, 20), "Cost: "+bridgeSetup.GetBridgeCost()+"$");
	
			if (BridgeSetup.eLevelStage.PlayStage == bridgeSetup.LevelStage) {
				if (GUI.Button (new Rect (318,18,100,50), "Back to Draw")) {
					bridgeSetup.LevelStage = BridgeSetup.eLevelStage.SetupStage;
				}
				GUI.enabled = !bridgeSetup.IsTrainStarted;
				if (GUI.Button (new Rect (425,18,100,50), "Run Train")) {
					bridgeSetup.StartTrain();
				}
				GUI.enabled = true;
		
				if (GUI.Toggle (new Rect (532,18,100,20), timeToggle, "Slow Time")) {
					Time.timeScale = 0.25f;
					timeToggle = true;
				} else {
					Time.timeScale = 1.0f;
					timeToggle = false;
				}
				/*
				if (GUI.Toggle (new Rect (532,40,100,20), showForce, "Show Force")) {
					showForce = true;
					bridgeSetup.SetBeamsToShowForce(showForce);
				} else {
					showForce = false;
					bridgeSetup.SetBeamsToShowForce(showForce);
				}
				*/
			} else if (BridgeSetup.eLevelStage.SetupStage == bridgeSetup.LevelStage) {
				if (GUI.Button (new Rect (318,18,100,50), "Test Bridge")) {
					bridgeSetup.LevelStage = BridgeSetup.eLevelStage.PlayStage;
				}
			}
		} else {
			GUI.Box(winningRect, "");
			
			GUI.Label(winningLabelRect, "You Win!");
			if (GUI.Button(new Rect(winningLabelRect.x-100, winningLabelRect.y+40, 300, 30), "Back to Title Screen")) {
				Application.LoadLevel("Title");
			}
		}


	}
}
