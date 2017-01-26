using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class StatusWndManager : MonoBehaviour {
	public Color color = new Color (0.5f, 0.5f, 0.5f, 0.5f);
	// Use this for initialization
	void Start () {
		var mesh = GetComponent<MeshFilter> ().mesh;
		var colors = mesh.colors;
		for (int i = 0; i < colors.Length; ++i) {
			colors [i] = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
