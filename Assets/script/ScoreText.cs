﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

	//自身的text组件
	Text selfText;

	// Use this for initialization
	void Start () {

		//获取自身的text组件
		selfText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		//实时显示玩家的得分
		selfText.text = GameController.Instance.score.ToString("00");
	}
}
