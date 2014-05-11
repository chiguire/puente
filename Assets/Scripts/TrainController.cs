using UnityEngine;
using System.Collections;

public class TrainController : MonoBehaviour {

	private Vector3 trainDirection = new Vector3(19.0f, 0.0f, 0.0f);

	private ResetPhysics[] trainWagon;

	private GameObject trainHead;
	private bool trainWorking = false;

	// Use this for initialization
	void Start () {
		trainWagon = GetComponentsInChildren<ResetPhysics>();
		trainHead = GameObject.FindGameObjectWithTag("TrainHead");
		trainWorking = false;
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}

	void FixedUpdate() {
		if (trainWorking) {
			trainHead.rigidbody.AddForce(trainDirection, ForceMode.Force);
		}
	}

	public void StartTrain() {
		trainWorking = true;
	}

	public void ResetTrain() {
		trainWorking = false;

		foreach (ResetPhysics rp in trainWagon) {
			rp.Reset();
		}
	}
}
