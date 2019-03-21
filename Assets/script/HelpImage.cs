using UnityEngine;
using System.Collections;

public class HelpImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//方法，执行帮助图片被点击之后的操作
	public void HelpImageClick(){

		//自身禁用
		this.gameObject.SetActive (false);

		//游戏推出暂停
		Time.timeScale = 1;
	}
}
