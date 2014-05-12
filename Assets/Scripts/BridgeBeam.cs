using UnityEngine;
using System.Collections;

public class BridgeBeam : MonoBehaviour {

	public enum eBeamState {
		LayoutMode,
		BuiltMode,
		TestingMode,
	};

	public BridgeSetup bridgeSetupParent = null;

	public bool isRoadBeam = false;

	private eBeamState beamState = eBeamState.LayoutMode;

	public eBeamState BeamState {
		get { return beamState; }
		set {
			beamState = value; 

			if (eBeamState.TestingMode == beamState) {
				pointStartRigidbody.isKinematic = false;
				pointEndRigidbody.isKinematic = false;

				pointStart.layer = isRoadBeam? 9: 10;
			} else {
				pointStartRigidbody.isKinematic = true;
				pointEndRigidbody.isKinematic = true;
			}
		}
	}

	private GameObject beam;
	private GameObject pointStart;
	private Rigidbody pointStartRigidbody;
	private GameObject pointEnd;
	private Rigidbody pointEndRigidbody;

	private GameObject pointClicked;

	private static Plane beamPlane = new Plane(-Vector3.forward, new Vector3(0.0f, 0.0f, 1.0f));
	private const float MAX_BEAM_DISTANCE = 4.0f;

	public GameObject PointStart {
		get { return pointStart; }
	}

	public GameObject PointEnd {
		get { return pointEnd; }
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (eBeamState.LayoutMode == beamState && Input.GetMouseButtonUp(0)){
			beamState = eBeamState.BuiltMode;
			DestroyIfSamePosition();
			pointClicked = null;
			isRoadBeam = bridgeSetupParent.IsInRoadLevel(pointStart.transform.position, pointEnd.transform.position);
			bridgeSetupParent.HandleEndPoint(pointEnd);
		}

		if (eBeamState.LayoutMode == beamState) {
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance = 0;
			
			GameObject pointNotClicked = pointClicked == pointStart? pointEnd: pointStart;
			
			if (beamPlane.Raycast(r, out distance)) {
				Vector3 p = r.GetPoint(distance);
				pointClicked.transform.position = LimitPointDistance(p, pointNotClicked) + Vector3.forward;
				pointClicked.GetComponent<ResetPhysics>().UpdatePosition();
				PositionBeam();
			}
		} else if (eBeamState.TestingMode == beamState) {
			PositionBeam();
		}

	}

	private void PositionBeam() {
		Vector3 beamVector = pointEnd.transform.position - pointStart.transform.position;
		
		beam.transform.position = pointStart.transform.position;
		Vector3 beamScale = new Vector3(beamVector.magnitude/2.0f, 1.0f, 1.0f);
		beam.transform.localScale = beamScale;
		
		Vector3 euAngles = Vector3.zero;
		Quaternion qt = Quaternion.identity;
		
		euAngles.z = Mathf.Atan2(beamVector.y, beamVector.x)*Mathf.Rad2Deg;
		qt.eulerAngles = euAngles;
		
		beam.transform.localRotation = qt;

		if (eBeamState.LayoutMode == beamState) {
			SpringJoint sj = pointEnd.GetComponent<SpringJoint>();
			sj.minDistance = 0;
			sj.maxDistance = 0;
			sj.spring = 120;
			sj.damper = 0.0f;
		}
	}

	public void StartLayout(Vector3 clickPosition, GameObject hingePoint, BridgeSetup bs) {
		beam = transform.Find("Beam").gameObject;
		pointStart = transform.Find("PointStart").gameObject;
		pointStartRigidbody = pointStart.rigidbody;
		pointStart.GetComponent<SnapPoint>().bridgeSetupParent = bs;
		pointEnd = transform.Find("PointEnd").gameObject;
		pointEndRigidbody = pointEnd.rigidbody;
		pointEnd.GetComponent<SnapPoint>().bridgeSetupParent = bs;

		pointStartRigidbody.isKinematic = true;
		pointEndRigidbody.isKinematic = true;

		beamState = eBeamState.LayoutMode;
		pointClicked = pointEnd;
		transform.position = clickPosition;

		FixedJoint hj = pointStart.AddComponent("FixedJoint") as FixedJoint;
		hj.connectedBody = hingePoint.rigidbody;
	}

	public void ResetState() {
		pointStart.GetComponent<ResetPhysics>().Reset();
		pointEnd.GetComponent<ResetPhysics>().Reset();
		PositionBeam();
		BeamState = eBeamState.BuiltMode;
	}

	public Vector3 LimitPointDistance(Vector3 mousePos, GameObject start) {
		return bridgeSetupParent.GetSnapPointFromPosition(mousePos, start.transform.position, MAX_BEAM_DISTANCE);
	}

	private void DestroyIfSamePosition() {
		if ((pointStart.transform.position - pointEnd.transform.position).magnitude < Mathf.Epsilon) {
			Destroy(gameObject);
		}
	}
}
