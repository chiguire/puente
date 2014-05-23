using UnityEngine;
using System.Collections;

public class SnapPoint : MonoBehaviour {

	public Tuple<int, int> position = Tuple<int, int>.Of(0, 0);

	public bool isBase = false;
	public bool isBaseTerrain = false;

	public BridgeSetup bridgeSetupParent = null;
	public BridgeBeam bridgeBeamParent = null;

	private Vector3 originalScale;

	private Color highlightColor;
	private Color originalColor;

	void Start() {
		originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		originalColor = GetComponent<Renderer> ().material.color;
		highlightColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	void OnMouseOver() {
		if (isBase && (bridgeSetupParent != null && BridgeSetup.eLevelStage.SetupStage == bridgeSetupParent.LevelStage)
		     || (bridgeBeamParent != null && bridgeBeamParent.BeamState == BridgeBeam.eBeamState.BuiltMode)) {
			transform.localScale = originalScale*1.5f;
			GetComponent<Renderer>().material.color = highlightColor;
		}
	}

	void OnMouseExit() {
		if (isBase) {
			transform.localScale = originalScale;
			GetComponent<Renderer>().material.color = originalColor;
		}
	}

	void OnJointBreak(float breakForce) {
		if (bridgeBeamParent != null) {
			bridgeBeamParent.Break ();
		}
	}
}