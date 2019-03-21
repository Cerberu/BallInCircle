using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	//动画组件
	Animator animComponent;

	//声音播放器
	AudioSource mainAudioSource;

	// Use this for initialization
	void Start () {

		//获得动画组件
		animComponent = this.transform.root.GetComponent<Animator>();

		//获得声音播放器
		mainAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//方法，执行点击该按钮之后的相关操作
	public void EnterGame(){

		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);

		//启动渐入动画
		animComponent.SetTrigger ("FadeIn");

		//0.5秒之后加载场景
		Invoke ("LoadGameLevel", 0.5F);
	}

	//方法，加载游戏场景
	public void LoadGameLevel(){

		//加载游戏场景
		Application.LoadLevel ("InCircle");
	}
}
