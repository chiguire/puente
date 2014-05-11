using UnityEngine;
using System.Collections;

public class BridgeSetup : MonoBehaviour {

	/** LevelStage determines which stage of the level the player is:
	 *  - SetupStage lets the player put beams around. Beams are kinetic.
	 *  - PlayStage spawns the train and makes it pass the bridge. Beams are dynamic.
	 */
	public enum eLevelStage {
		SetupStage,
		PlayStage,
	};

	private eLevelStage levelStage = eLevelStage.SetupStage;

	public eLevelStage LevelStage
	{
		get
		{
			return this.levelStage;
		}

		set
		{
			levelStage = value;
			if (eLevelStage.SetupStage == levelStage) {
				Time.timeScale = 0.0f;
				SetSnapPointsVisible(true);
				trainController.SetVisible(false);
				SetBeamsToSetup();
			} else if (eLevelStage.PlayStage == levelStage) {
				Time.timeScale = 1.0f;
				SetSnapPointsVisible(false);
				SetBeamsToPlay();
				trainController.SetVisible(true);
				trainController.ResetTrain();
				trainController.StartTrain();
			}
		}
	}
	
	/** Level information
	 *  - TerrainGen, in charge of generating the terrain and its collider.
	 *  - anchorPointLocations, where the bridge beams spawn
	 *  - roadLevel, the y coordinate (in Snap Point space) where all horizontal beams are roads. The rest support the bridge but are not roads.
	 */
	public TerrainGenerator terrainGenerator;
	public Tuple<int, int>[] anchorPointLocations;
	public int roadLevel;

 	/** Internal game information
 	 *  - snapPoints, contains all snap points when in Setup Stage.
 	 *  - bridgeBeams, contains all references to bridge beams.
 	 *  - trainControl, allows to move train and reset position when in Play Stage.
 	 */
	private GameObject snapPoints;
	private GameObject bridgeBeams;
	public TrainController trainController;

	private int bridgeCost = 0;

	private Tuple<int, int> snapPositionsDimensions = Tuple<int, int>.Of (40, 30);
	private Vector3 snapPointSeparations = new Vector3(1, 1, 0);
	private Vector3 snapPointOffset = new Vector3(-0.5f, -0.5f, 0);

	private Vector3 gridOrigin = Vector3.zero;

	void Start () {
		terrainGenerator.CreateTerrain();

		snapPoints = new GameObject();
		snapPoints.name = "SnapPointsContainer";
		bridgeBeams = new GameObject();
		bridgeBeams.name = "BridgeBeamsContainer";

		bridgeCost = 0;

		anchorPointLocations = new Tuple<int, int>[] {
			Tuple<int, int>.Of(12, 18),
			Tuple<int, int>.Of(28, 18)
		};
		roadLevel = 18;

		int w = snapPositionsDimensions._1;
		int h = snapPositionsDimensions._2;
		Vector3 dims = new Vector3(w*snapPointSeparations.x, h*snapPointSeparations.y, 0);
		
		gridOrigin.x = -dims.x/2.0f+snapPointOffset.x;
		gridOrigin.y = -dims.y/2.0f+snapPointOffset.y;
		gridOrigin.z = snapPointOffset.z;

		GameObject ap = Resources.Load("AnchorPoint") as GameObject;
		GameObject sp = Resources.Load("SnapPoint") as GameObject;

		for (int i = 0; i != anchorPointLocations.Length; i++)
		{
			GameObject go = Instantiate(ap, FromSnapToSpace(new Vector3(anchorPointLocations[i]._1, anchorPointLocations[i]._2, gridOrigin.z)), new Quaternion()) as GameObject;
			go.transform.parent = snapPoints.transform;
		}

		for (int j = 0; j != h; j++) {
			for (int i = 0; i != w; i++) {
				bool foundAnchor = FindAnchorPointIndex(i, j) != -1;
				Vector3 pos = FromSnapToSpace(new Vector3(i, j, gridOrigin.z));
				GameObject go = Instantiate((foundAnchor? ap: sp), pos, new Quaternion()) as GameObject;
				go.transform.parent = snapPoints.transform;
				go.layer = 8;
				go.GetComponent<SnapPoint>().position = Tuple<int, int>.Of(i, j);
				go.GetComponent<SnapPoint>().isBase = foundAnchor;
			}
		}

		LevelStage = eLevelStage.SetupStage;
	}

	void Update () {
		if (eLevelStage.SetupStage == levelStage) {
			if (Input.GetMouseButtonDown(0) && !BridgeBuilderGUI.ClickedOnGUI()) {
				GameObject objClicked = GetSnapPointClicked();
				if (null != objClicked) {
					CreateBeam(objClicked);
				}
			}
		}
	}

	//public interface

	public Vector3 GetSnapPointFromPosition(Vector3 pos, Vector3 start, float maxDistance) {
		Vector3 diff = pos - start;
		if (diff.magnitude >= maxDistance) {
			//If distance larger than maximum put it in same vector
			Vector3 direction = diff.normalized;
			return SetPointToSnapPoint(start, start + maxDistance*direction);
		}
		
		return SetPointToSnapPoint(start, pos);
	}

	public bool IsInRoadLevel(Vector3 a, Vector3 b) {
		return Mathf.FloorToInt(FromSpaceToSnap(a).y) == roadLevel && Mathf.FloorToInt(FromSpaceToSnap(b).y) == roadLevel;
	}

	public void CreateHingeForSnapPoint(GameObject pointEnd) {
		Vector3 pointEndSnapPoint = FromSpaceToSnap(pointEnd.transform.position);
		int api = FindAnchorPointIndex(Mathf.FloorToInt(pointEndSnapPoint.x), Mathf.FloorToInt(pointEndSnapPoint.y));
		if (api < 0) return;

		SnapPoint sp = GetSnapPoint(anchorPointLocations[api]._1, anchorPointLocations[api]._2);

		HingeJoint hj = pointEnd.AddComponent("HingeJoint") as HingeJoint;
		hj.connectedBody = sp.GetComponent<Rigidbody>();
	}

	public int GetBridgeCost() {
		return bridgeCost;
	}

	//private

	private BridgeBeam CreateBeam(GameObject snapPoint) {
		GameObject go = Instantiate(Resources.Load ("BridgeBeam"), snapPoint.transform.position, new Quaternion()) as GameObject;
		BridgeBeam bb = go.GetComponent<BridgeBeam>();
		bb.bridgeSetupParent = this;
		Vector3 newPos = new Vector3(snapPoint.transform.position.x, snapPoint.transform.position.y, gridOrigin.z+1);
		bb.StartLayout(newPos, snapPoint);
		bb.transform.parent = bridgeBeams.transform;

		bridgeCost += 100;

		return bb;
	}

	private Vector3 SetPointToSnapPoint(Vector3 a, Vector3 b) {
		return FromSnapToSpace(FromSpaceToSnap(b));
	}

	private Vector3 FromSpaceToSnap(Vector3 a) {
		return new Vector3(Mathf.Round((a.x-gridOrigin.x)/snapPointSeparations.x),
				           Mathf.Round((a.y-gridOrigin.y)/snapPointSeparations.y),
		                   a.z);
	}

	private Vector3 FromSnapToSpace(Vector3 a) {
		return new Vector3(a.x*snapPointSeparations.x+gridOrigin.x, a.y*snapPointSeparations.y+gridOrigin.y, gridOrigin.z);
	}

	private void SetSnapPointsVisible(bool value) {
		Renderer[] rs = snapPoints.GetComponentsInChildren<Renderer>();

		foreach (Renderer r in rs) {
			r.enabled = value;
		}
	}

	private GameObject GetSnapPointClicked() {
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh = new RaycastHit();

		if (Physics.Raycast(r, out rh, Mathf.Infinity, 1 << 8)) {
			if (rh.collider.GetComponent<SnapPoint>().isBase) {
				return rh.collider.gameObject;
			}
		}

		return null;
	}

	private void SetBeamsToPlay() {
		BridgeBeam[] bb = bridgeBeams.GetComponentsInChildren<BridgeBeam>();
		
		foreach (BridgeBeam b in bb) {
			b.BeamState = BridgeBeam.eBeamState.TestingMode;
		}
	}

	private void SetBeamsToSetup() {
		BridgeBeam[] bb = bridgeBeams.GetComponentsInChildren<BridgeBeam>();
		
		foreach (BridgeBeam b in bb) {
			b.ResetState();
		}
	}

	private int FindAnchorPointIndex(int i, int j) {
		for (int k = 0; k != anchorPointLocations.Length; k++) {
			if (anchorPointLocations[k]._1 == i && anchorPointLocations[k]._2 == j) {
				return k;
			}
		}
		return -1;
	}

	private SnapPoint GetSnapPoint(int i, int j) {
		SnapPoint[] snps = snapPoints.GetComponentsInChildren<SnapPoint>();

		foreach (SnapPoint sp in snps) {
			if (sp.position._1 == i && sp.position._2 == j) {
				return sp;
			}
		}

		return null;
	}
}
