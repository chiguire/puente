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
		transform.localScale = originalScale*3;
	}

	void OnMouseExit() {
		transform.localScale = originalScale;
	}

	// Update is called once per frame
//	void Update () {
//	
//	}
}
