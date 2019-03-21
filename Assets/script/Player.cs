using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//主角的旋转方向，0为顺时针，1为逆时针
	public int rotateDirction;

	//主角的位置，0为圈内，1为圈外
	public int positionState;

	//主角围绕圆心旋转的角速度
	public float rotateSpeed;

	//主角旋转时围绕的轴向
	Vector3 rotateAxis;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){

		//如果游戏处于playing状态、玩家位于该圆环上，并且旋转方向和位置状态均符合
		if (MyClass.gameState == GameState.Playing &&
			this.transform.parent.GetComponent<Circle> ().playerStay &&
			GameController.Instance.playerRotateDirection == rotateDirction &&
			GameController.Instance.playerPositionState == positionState) {

			//根据旋转的方向来判断旋转的轴向
			if (rotateDirction == 0) {
		
				//顺时针围绕-Z轴
				rotateAxis = Vector3.back;
			}
	
			if (rotateDirction == 1) {
		
				//顺时针围绕-Z轴
				rotateAxis = Vector3.forward;
			}
	
			//绕着圆心旋转
			this.transform.RotateAround (this.transform.parent.position, rotateAxis, rotateSpeed * Time.fixedDeltaTime);
	
			//更新主角的坐标
			GameController.Instance.playerCurrentPositon = this.transform.position;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
		//如果游戏处于playing状态、玩家位于该圆环上，并且旋转方向和位置状态均符合
		if (MyClass.gameState == GameState.Playing &&
		    this.transform.parent.GetComponent<Circle> ().playerStay &&
		    GameController.Instance.playerRotateDirection == rotateDirction &&
		    GameController.Instance.playerPositionState == positionState) {

			//该图片显示
			this.renderer.enabled = true;
			
			//碰撞体激活
			this.collider2D.enabled = true;
		}
		else{

			//该图片不显示
			this.renderer.enabled = false;
			
			//碰撞体禁用
			this.collider2D.enabled = false;
		}
	}
}
