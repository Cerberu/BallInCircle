using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpButton : MonoBehaviour {

	//帮助的图片
	public GameObject helpImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//方法，执行帮助按钮按下之后的相关操作
	public void HelpButtonClick(){

		//帮助图片激活
		helpImage.SetActive (true);

		//游戏暂停
		Time.timeScale = 0;
	}
}
