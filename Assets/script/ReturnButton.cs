using UnityEngine;
using System.Collections;

public class ReturnButton : MonoBehaviour {

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

	//方法，执行返回按钮被按下之后的操作
	public void ReturnButtonClick(){

		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);
		
		//启动渐入动画
		animComponent.SetTrigger ("FadeIn");
		
		//0.5秒之后加载主界面场景
		Invoke ("ReturnToMainMenu", 0.5F);
	}

	//方法，返回主界面
	public void ReturnToMainMenu(){

		//加载主界面
		Application.LoadLevel ("Menu");
	}
}
