using UnityEngine;
using System.Collections;

public class ClickPanel : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//如果游戏失败
		if (MyClass.gameState == GameState.Defeat) {

			//该panel不再起作用
			this.gameObject.SetActive(false);
		}
	}
}
