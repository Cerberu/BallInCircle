using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

	//圆环的半径
	public float radius;

	//bool值，指示主角是否处于该圆环之上,false表示玩家不在该环上，true表示在。
	public bool playerStay;

	//bool值，指示该环是否是可以得分的环，只有主角第一次进入该环时，才能得分
	public bool scoreEnable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//方法，当碰到别的物体时
	public void OnTriggerEnter2D(Collider2D other){

		//如果碰到的是主角
		if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {

			//如果玩家不在该环上，并且玩家位于圈外
			if(playerStay == false && GameController.Instance.playerPositionState == 1){

				//玩家在环外碰撞到其他环，则失败
				GameController.Instance.GameDefeat();
			}
		}
	}

	//方法，执行该环被创建之后的操作
	public void NewCircleCreated(){
		
		//在集合中注册
	  	GameController.Instance.circleArray.Add (this.gameObject);
	}
}
