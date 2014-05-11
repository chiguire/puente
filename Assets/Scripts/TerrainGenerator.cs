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
					if (i < 40) {
						heights[i] = -Mathf.Sin((float)40/heights.Length*Mathf.PI*10-Mathf.PI*0.5f)*3.0f;;
					} else if (i < 60) {
						heights[i] = -Mathf.Sin((float)i/heights.Length*Mathf.PI*10-Mathf.PI*0.5f)*3.0f;
					} else {
						heights[i] = -Mathf.Sin((float)60/heights.Length*Mathf.PI*10-Mathf.PI*0.5f)*3.0f;
					}
				}
			}
		} else {
			heights = h;
		}

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh m = mf.mesh;
		
		renderer.material = GetComponent<MeshRenderer>().materials[0];
		
		//First front lines, then lower ground lines, then fron lines again, then back lines
		Vector3[] vertices = new Vector3[heights.Length*4];
		
		for (int i = 0; i != heights.Length; i++) {
			float x = (-heights.Length*0.5f+i)*separation.x;
			vertices[heights.Length*0+i] = new Vector3(x, heights[i], 0.0f);
			vertices[heights.Length*1+i] = new Vector3(x, heights[i]+separation.y, 0.0f);
			vertices[heights.Length*2+i] = new Vector3(x, heights[i], 0.0f);
			vertices[heights.Length*3+i] = new Vector3(x, heights[i], separation.z);
		}
		
		m.vertices = vertices;
		
		Vector3[] normals = new Vector3[heights.Length*4];
		for (int i = 0; i != heights.Length; i++) {
			normals[heights.Length*0+i] = new Vector3(0, 0, -1);
			normals[heights.Length*1+i] = new Vector3(0, 0, -1);
			normals[heights.Length*2+i] = new Vector3(0, 1, 0);
			normals[heights.Length*3+i] = new Vector3(0, 1, 0);
		}
		
		m.normals = normals;
		
		Vector2[] uv = new Vector2[heights.Length*4];
		
		for (int i = 0; i != heights.Length; i++) {
			uv[heights.Length*0+i] = new Vector2(i*0.5f, 0.96f);
			uv[heights.Length*1+i] = new Vector2(i*0.5f, 0);
			uv[heights.Length*2+i] = new Vector2(i*0.5f, 0.96f);
			uv[heights.Length*3+i] = new Vector2(i*0.5f, 0.98f);
		}
		
		m.uv = uv;
		
		int [] triangles = new int[heights.Length*3*4];
		for (int i = 0; i != heights.Length-1; i++) {
			triangles[i*12+0] = heights.Length*0+i;
			triangles[i*12+1] = heights.Length*0+i+1;
			triangles[i*12+2] = heights.Length*1+i;
			
			triangles[i*12+3] = heights.Length*0+i+1;
			triangles[i*12+4] = heights.Length*1+i+1;
			triangles[i*12+5] = heights.Length*1+i;
			
			triangles[i*12+6] = heights.Length*2+i;
			triangles[i*12+7] = heights.Length*3+i;
			triangles[i*12+8] = heights.Length*2+i+1;
			
			triangles[i*12+9] = heights.Length*2+i+1;
			triangles[i*12+10] = heights.Length*3+i;
			triangles[i*12+11] = heights.Length*3+i+1;
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
