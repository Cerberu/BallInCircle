using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	void Awake(){
	
	}

	// Use this for initialization
	void Start () {

		//从玩家偏好中读取历史最佳成绩
		MyClass.bestScore = PlayerPrefs.GetInt ("bestScore", 0);

		//从玩家偏好中读取玩家玩过的次数
		MyClass.playedCount = PlayerPrefs.GetInt ("playedCount", 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//当该组件不可用时（程序退出等等）
	void OnDisable() {

		//将当前的分数写入playerprefs
		PlayerPrefs.SetInt ("bestScore", MyClass.bestScore);

		//将当前的玩过次数写入playerprefs
		PlayerPrefs.SetInt ("playedCount", MyClass.playedCount);
	}
}
