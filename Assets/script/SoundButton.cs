using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundButton : MonoBehaviour {
	
	//该图片对应的声音开光状态，false为关，true为开
	public bool audioEnable;

	//声音播放器
	AudioSource mainAudioSource;

	//自身的Image组件
	Image selfImage;

	//自身的button组件
	Button selfButton;
	
	// Use this for initialization
	void Start () {
		
		//获得声音播放器
		mainAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

		//获得自身的Image组件
		selfImage = this.GetComponent <Image>();

		//获得自身的Button组件
		selfButton = this.GetComponent <Button>();
	}
	
	// Update is called once per frame
	void Update () {

		if(audioEnable == MyClass.audioEnable){

			selfImage.enabled = true;
			selfButton.enabled = true;
		}

		else{

			selfImage.enabled = false;
			selfButton.enabled = false;
		}
	}

	//方法，执行按下排名按钮之后的操作
	public void SoundButtonClick(){
		
		//播放按钮音效
		MyClass.AudioPlay (mainAudioSource, MyClass.AudioResources[1], MyClass.audioEnable);

		//修改系统的声音开关
		MyClass.audioEnable = !MyClass.audioEnable;
	}
}
