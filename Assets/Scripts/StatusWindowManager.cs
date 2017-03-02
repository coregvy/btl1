﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindowManager : MonoBehaviour {
	Text statusText;
	string text = "";
	// Use this for initialization
	void Start () {
		Debug.Log ("sw man start.");
		statusText = transform.FindChild ("Canvas").transform.FindChild ("Text").gameObject.GetComponent<Text>();
		//text  = statusText.text = "set ok!!";
	}
	
	// Update is called once per frame
	void Update () {
		//transform.FindChild ("Canvas").transform.FindChild ("Text").gameObject.GetComponent<Text>().text = text;
		statusText.text = text;
	}

	public void updateText(string newmsg) {
		text = newmsg;
		// 	transform.FindChild ("Canvas").transform.FindChild ("Text").gameObject.GetComponent<Text>().text = newmsg;
	}
	public void updateText(CharacterStatus status) {
		text = $@"{status.name}
HP: {status.hp}   攻撃力: {status.power}
射程: 1";
	}
}