using UnityEngine;
using System.Collections;

public class IAPButton : MonoBehaviour {

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

	//方法，执行按下IAP按钮之后的操作
	public void IAPButtonClick(){
		
		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);

		//调用恢复购买相关的网络功能
		SdkToU3d.Restore ();
	}
}
