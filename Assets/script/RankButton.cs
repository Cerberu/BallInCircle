﻿using UnityEngine;
using System.Collections;

public class RankButton : MonoBehaviour {

	//声音播放器
	AudioSource mainAudioSource;
	
	// Use this for initialization
	void Start () {
		
		//获得声音播放器
		mainAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//方法，执行按下排名按钮之后的操作
	public void RankButtonClick(){
		
		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);

		//调用排名相关的网络功能
		SdkToU3d.OpenGameCenter ();
	}
}
