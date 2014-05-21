using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	public float[] heights;
	public Vector3 separation = new Vector3(1.0f, -10.0f, 2.0f);

	// Use this for initialization
	void Start () {
	}

	public void CreateTerrain(float[] h = null)
	{
		if (h == null) {
			if (heights.Length == 0) {
				heights = new float[100];
				
				for (int i = 0; i != heights.Length; i++) {
					//heights[i] = Mathf.PerlinNoise(((float)i)/heights.Length*5.0f, 11)*5.0f;
					if (i < 42) {
						heights[i] = 3.0f;
					} else if (i < 58) {
						heights[i] = -1.0f;
					} else {
						heights[i] = 3.0f;
					}
				}
			}
		} else {
			heights = h;
		}

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh m = mf.mesh;
		
		renderer.material = GetComponent<MeshRenderer>().materials[0];

		// * Front lines
		// * Lower ground lines //this and the previous form the front faces
		// * Front lines
		// * Lower back lines // top faces
		// * Lower back lines
		// * Lower ground back lines // back faces
		Vector3[] vertices = new Vector3[heights.Length*6];
		
		for (int i = 0; i != heights.Length; i++) {
			float x = (-heights.Length*0.5f+i)*separation.x;
			vertices[heights.Length*0+i] = new Vector3(x, heights[i], 0.0f);
			vertices[heights.Length*1+i] = new Vector3(x, heights[i]+separation.y, 0.0f);
			vertices[heights.Length*2+i] = new Vector3(x, heights[i], 0.0f);
			vertices[heights.Length*3+i] = new Vector3(x, heights[i], separation.z);
			vertices[heights.Length*4+i] = new Vector3(x, heights[i], separation.z);
			vertices[heights.Length*5+i] = new Vector3(x, heights[i]+separation.y, separation.z);
		}
		
		m.vertices = vertices;
		
		Vector3[] normals = new Vector3[heights.Length*6];
		for (int i = 0; i != heights.Length; i++) {
			normals[heights.Length*0+i] = new Vector3(0, 0, 1);
			normals[heights.Length*1+i] = new Vector3(0, 0, 1);
			normals[heights.Length*2+i] = new Vector3(0, 1, 0);
			normals[heights.Length*3+i] = new Vector3(0, 1, 0);
			normals[heights.Length*4+i] = new Vector3(0, 0, 1);
			normals[heights.Length*5+i] = new Vector3(0, 0, 1);
		}
		
		m.normals = normals;
		
		Vector2[] uv = new Vector2[heights.Length*6];
		
		for (int i = 0; i != heights.Length; i++) {
			uv[heights.Length*0+i] = new Vector2(i*0.5f, 0.96f);
			uv[heights.Length*1+i] = new Vector2(i*0.5f, 0);
			uv[heights.Length*2+i] = new Vector2(i*0.5f, 0.96f);
			uv[heights.Length*3+i] = new Vector2(i*0.5f, 0.98f);
			uv[heights.Length*4+i] = new Vector2(i*0.5f, 0.96f);
			uv[heights.Length*5+i] = new Vector2(i*0.5f, 0);
		}
		
		m.uv = uv;
		
		int [] triangles = new int[heights.Length*3*6];
		for (int i = 0; i != heights.Length-1; i++) {
			triangles[i*18+0] = heights.Length*0+i;
			triangles[i*18+1] = heights.Length*0+i+1;
			triangles[i*18+2] = heights.Length*1+i;
			
			triangles[i*18+3] = heights.Length*0+i+1;
			triangles[i*18+4] = heights.Length*1+i+1;
			triangles[i*18+5] = heights.Length*1+i;
			
			triangles[i*18+6] = heights.Length*2+i;
			triangles[i*18+7] = heights.Length*3+i;
			triangles[i*18+8] = heights.Length*2+i+1;
			
			triangles[i*18+9] = heights.Length*2+i+1;
			triangles[i*18+10] = heights.Length*3+i;
			triangles[i*18+11] = heights.Length*3+i+1;

			triangles[i*18+12] = heights.Length*4+i;
			triangles[i*18+13] = heights.Length*4+i+1;
			triangles[i*18+14] = heights.Length*5+i;

			triangles[i*18+15] = heights.Length*4+i+1;
			triangles[i*18+16] = heights.Length*5+i+1;
			triangles[i*18+17] = heights.Length*5+i;
		}
		
		m.triangles = triangles;
		
		MeshCollider mc = GetComponent<MeshCollider>();
		mc.sharedMesh = m;
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
