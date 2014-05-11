using UnityEngine;
using System.Collections;

public class SnapPoint : MonoBehaviour {

	public Tuple<int, int> position = Tuple<int, int>.Of(0, 0);

	public bool isBase = false;

	public BridgeSetup bridgeSetupParent = null;

	private Vector3 originalScale;

	void Start() {
		originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	void OnMouseOver() {
		if (isBase) {
			transform.localScale = originalScale*2;
		}
	}

	void OnMouseExit() {
		if (isBase) {
			transform.localScale = originalScale;
		}
	}
}