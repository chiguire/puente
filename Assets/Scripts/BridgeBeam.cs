using UnityEngine;
using System.Collections;

public class BridgeBeam : MonoBehaviour {

	public enum eBeamState {
		LayoutMode,
		BuiltMode,
		TestingMode,
	};

	public enum eBeamAppereanceState {
		NormalMode,
		ForceMode,
	};

	public BridgeSetup bridgeSetupParent = null;

	public bool IsRoadBeam = false;

	private eBeamState beamState = eBeamState.LayoutMode;


	public eBeamAppereanceState BeamAppereanceState = eBeamAppereanceState.NormalMode;

	private GameObject beam;
	private GameObject pointStart;
	private Rigidbody pointStartRigidbody;
	private GameObject pointEnd;
	private Rigidbody pointEndRigidbody;

	private GameObject pointClicked;

	private Color originalColor;

	private static Plane beamPlane = new Plane(Vector3.forward, new Vector3(0.0f, 0.0f, 0.0f));
	private const float MAX_BEAM_DISTANCE = 4.0f;

	// Update is called once per frame
	void Update () {
		if (eBeamState.LayoutMode == beamState && Input.GetMouseButtonUp(0)){
			beamState = eBeamState.BuiltMode;
			DestroyIfSamePosition();
			pointClicked = null;
			IsRoadBeam = bridgeSetupParent.IsInRoadLevel(pointStart.transform.position, pointEnd.transform.position);
			bridgeSetupParent.HandleEndPoint(pointEnd);
		}
		
		if (eBeamState.LayoutMode == beamState) {
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			float distance = 0;
			
			GameObject pointNotClicked = pointClicked == pointStart? pointEnd: pointStart;
			
			if (beamPlane.Raycast(r, out distance)) {
				Vector3 p = r.GetPoint(distance);
				pointClicked.transform.position = LimitPointDistance(p, pointNotClicked);
				pointClicked.GetComponent<ResetPhysics>().UpdatePosition();
				IsRoadBeam = bridgeSetupParent.IsInRoadLevel(pointStart.transform.position, pointEnd.transform.position);
			}
		}
		
		PositionBeam();
		ColorBeam();
	}

	public GameObject PointStart {
		get { return pointStart; }
	}

	public GameObject PointEnd {
		get { return pointEnd; }
	}

	public eBeamState BeamState {
		get { return beamState; }
		set {
			beamState = value; 
			
			if (eBeamState.TestingMode == beamState) {
				Vector3 beamVector = pointEnd.transform.position - pointStart.transform.position;
				
				SpringJoint sj = pointEnd.AddComponent<SpringJoint> ();
				sj.connectedBody = pointStart.rigidbody;
				sj.spring = 500.0f;
				sj.damper = 100.0f;
				sj.autoConfigureConnectedAnchor = false;
				sj.anchor = Vector3.zero;
				sj.connectedAnchor = Vector3.zero;
				sj.breakForce = 4.0f;
				sj.minDistance = beamVector.magnitude;
				sj.maxDistance = beamVector.magnitude;

				pointStartRigidbody.isKinematic = false;
				pointEndRigidbody.isKinematic = false;
				pointStartRigidbody.WakeUp();
				pointEndRigidbody.WakeUp();
			} else {
				pointStartRigidbody.isKinematic = true;
				pointEndRigidbody.isKinematic = true;
			}
		}
	}

	public void StartLayout(Vector3 clickPosition, GameObject hingePoint, BridgeSetup bs) {
		beam = transform.Find("Beam").gameObject;
		pointStart = transform.Find("PointStart").gameObject;
		pointStartRigidbody = pointStart.rigidbody;
		pointStart.GetComponent<SnapPoint>().bridgeSetupParent = bs;
		pointStart.GetComponent<SnapPoint>().bridgeBeamParent = this;
		pointEnd = transform.Find("PointEnd").gameObject;
		pointEndRigidbody = pointEnd.rigidbody;
		pointEnd.GetComponent<SnapPoint>().bridgeSetupParent = bs;
		pointEnd.GetComponent<SnapPoint>().bridgeBeamParent = this;

		pointStartRigidbody.isKinematic = true;
		pointEndRigidbody.isKinematic = true;

		beamState = eBeamState.LayoutMode;
		pointClicked = pointEnd;
		transform.position = clickPosition;

		originalColor = beam.GetComponent<Renderer> ().material.color;

		FixedJoint hj = pointStart.AddComponent("FixedJoint") as FixedJoint;
		hj.connectedBody = hingePoint.rigidbody;
	}

	public Vector3 LimitPointDistance(Vector3 mousePos, GameObject start) {
		return bridgeSetupParent.GetSnapPointFromPosition(mousePos, start.transform.position, MAX_BEAM_DISTANCE);
	}

	public void ResetState() {
		pointStart.GetComponent<ResetPhysics>().Reset();
		pointEnd.GetComponent<ResetPhysics>().Reset();
		BeamAppereanceState = eBeamAppereanceState.NormalMode;
		PositionBeam();
		BeamState = eBeamState.BuiltMode;
		beam.renderer.enabled = true;
		pointStart.renderer.enabled = true;
		pointEnd.renderer.enabled = true;
		Destroy(pointEnd.GetComponent<SpringJoint> ());
	}

	public void Break() {
		beam.renderer.enabled = false;
		pointStart.renderer.enabled = false;
		pointEnd.renderer.enabled = false;
	}

	private Color getForceColor() {
		Color b = Color.blue;
		Color r = Color.red;
		SpringJoint sj = pointEnd.GetComponent<SpringJoint> ();
		float t = sj.breakForce == Mathf.Infinity? 0.0f: sj.breakForce;

		return Color.Lerp (b, r, t);
	}

	private void DestroyIfSamePosition() {
		if ((pointStart.transform.position - pointEnd.transform.position).magnitude < Mathf.Epsilon) {
			Destroy(gameObject);
		}
	}

	private void PositionBeam() {
		Vector3 beamVector = pointEnd.transform.position - pointStart.transform.position;
		
		beam.transform.position = pointStart.transform.position + beamVector/2.0f;
		Vector3 beamScale = new Vector3(1.0f, beamVector.magnitude/2.0f, 1.0f);
		beam.transform.localScale = beamScale;
		
		Vector3 euAngles = Vector3.zero;
		Quaternion qt = Quaternion.identity;
		
		euAngles.z = Mathf.Atan2(beamVector.y, beamVector.x)*Mathf.Rad2Deg+90;
		qt.eulerAngles = euAngles;
		
		beam.transform.localRotation = qt;
	}
	
	private void ColorBeam() {
		Color c = originalColor;
		if (eBeamAppereanceState.NormalMode == BeamAppereanceState) {
			if (IsRoadBeam) {
				c.a = 1.0f;
				beam.layer = 9; //collides with train
			} else {
				c.a = 0.5f;
				beam.layer = 10; //does not collide with train
			}
		} else {
			c = getForceColor();
		}
		beam.GetComponent<Renderer> ().material.color = c;
	}

}
