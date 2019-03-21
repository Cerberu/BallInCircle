using UnityEngine;
using System.Collections;

public class ADSButton : MonoBehaviour {

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

	//方法，执行按下ADS按钮之后的操作
	public void ADSButtonClick(){
		
		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);

		//调用付费去广告的相关功能
		SdkToU3d.Buy ();
	}
}
