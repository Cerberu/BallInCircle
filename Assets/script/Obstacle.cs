using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	//位置标志位，0表示该障碍物位于圈内，1表示该障碍物位于圈外
	public int positionState;

	//标志位，指示该障碍物是否可以旋转
	public bool rotateEnable;

	//旋转的角速度
	public float rotateSpeed;

	//旋转过的角度
	float totalAngle;
	
	// Use this for initialization
	void Start () {

		//初始化，旋转过的角度为0
		totalAngle = 0;
	}

	void FixedUpdate(){

		//如果允许旋转，并且游戏处于playing状态
		if(rotateEnable && MyClass.gameState == GameState.Playing){

			//沿着环心旋转
			this.transform.RotateAround(this.transform.parent.position, Vector3.forward, rotateSpeed * Time.fixedDeltaTime);

			//记录旋转的总角度
			totalAngle += Mathf.Abs(rotateSpeed) * Time.fixedDeltaTime;

			//如果总角度超过90度
			if(totalAngle >= 90){

				//反向
				rotateSpeed = - rotateSpeed;

				//角度清0
				totalAngle = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		//玩家位于该圈内，并且位置符合
		if(this.transform.parent.GetComponent<Circle> ().playerStay == true &&
		   GameController.Instance.playerPositionState == positionState){

			//碰撞体激活
			this.collider2D.enabled = true;
		}

		else{

			//碰撞体禁用
			this.collider2D.enabled = false;
		}
	}

	//方法，当碰到别的物体时
	public void OnTriggerEnter2D(Collider2D other){
		
		//如果碰到的是主角
		if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {

			//游戏失败
			GameController.Instance.GameDefeat();
		}
	}
}
