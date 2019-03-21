using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayedCountText : MonoBehaviour {

	//自身的text组件
	Text selfText;
	
	// Use this for initialization
	void Start () {
		
		//获取自身的text组件
		selfText = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		//如果玩家失败次数小于100
		if (MyClass.playedCount < 100) {

			//显示2位
			selfText.text = MyClass.playedCount.ToString("00");
		}

		//如果玩家失败次数在100——999之间
		else if(MyClass.playedCount < 1000){

			//显示3位
			selfText.text = MyClass.playedCount.ToString("000");
		}

		//1000次以上显示4位
		else{
		
			//显示4位
			selfText.text = MyClass.playedCount.ToString("0000");
		}
	}
}
