using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindowManager : MonoBehaviour {
	Text statusText;
	// Use this for initialization
	void Start () {
		statusText = transform.FindChild ("Canvas").transform.FindChild ("Text").gameObject.GetComponent<Text>();
		statusText.text = "set ok!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void updateText(string text) {
		statusText.text = text;
	}
}
