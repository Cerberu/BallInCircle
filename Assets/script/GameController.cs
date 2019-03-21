using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	//单例
	public static GameController Instance;

	//集合，存储屏幕上的所有圆环
	public ArrayList circleArray = new ArrayList();

	//当前主角处于第几个环上
	public int playerCircleIndex;

	//主角的旋转方向，0为顺时针，1为逆时针
	public int playerRotateDirection;

	//主角的位置，0为圈内，1为圈外
	public int playerPositionState;

	//主角的实时坐标
	public Vector3 playerCurrentPositon;

	//屏幕之外，刷新圆环的刷新点数组
	public Vector3[] circleRefreshPoints;

	//bool值，指示是否需要刷新一个新环
	public bool circleCreatEnable = false;

	//玩家的当局得分
	public int score;

	//游戏失败的界面图片
	public GameObject defeatImage;

	//新纪录的标志图片
	public GameObject newScoreImage;

	//分数的文本框
	public Text currentScoreText;

	//帮助图片
	public GameObject helpImage;

	//bool值，指示摄像机是否转动
	bool cameraRotateEnable;

	//临时变量
	int playerCurrentIndex;
	GameObject tempCircle;
	Vector3 tempPosition;
	Vector2 startPos;
	Vector2 endPos;
	Vector2 endPos1;
	Vector2 endPos2;
	Vector2 relativePosition;
	Vector3 crossPoint;
	float r1;
	float r2;
	float rate;
	float rate1;
	float rate2;
	RaycastHit2D testRayCastHit2D_1;
	RaycastHit2D testRayCastHit2D_2;
	Vector2[] originalCirclePosition;

	//摄像机旋转时转过的角度
	float cameraTotalAngle;

	//摄像机旋转的角速度
	float cameraRotateSpeed = 12;

	//刷新点的索引
	int tempRefreshPointIndex = 0;
	
	//即将刷新的圆环的索引
	int tempCircleIndex = 0;

	//自身的声音播放器
	AudioSource selfAudioSource;

	//在Awake中获取单例
	void Awake(){

		//规定目标帧率
		Application.targetFrameRate = 100;

		//获得单例自身
		GameController.Instance = this;

		//获得自身的声音播放器
		selfAudioSource = this.GetComponent<AudioSource> ();

		//播放背景音乐
		MyClass.AudioPlay (selfAudioSource, MyClass.audioEnable);
	}

	// Use this for initialization
	void Start () {

		//如果玩家是第一次玩
		if (MyClass.playedCount == 0) {

			//显示帮助界面
			helpImage.SetActive(true);

			//游戏暂停
			Time.timeScale = 0;
		}

		//初始时，摄像机可以转动
		cameraRotateEnable = true;

		//初始时，转过20度
		cameraTotalAngle = 20;

		//游戏状态
		MyClass.gameState = GameState.Playing;

		//初始时，玩家得分为0
		score = 0;

		//玩家初始的旋转方向，随机为顺时针或者逆时针
		playerRotateDirection = Random.Range (0, 2);

		//玩家初始时位于圈内
		playerPositionState = 0;

		//玩家初始时处于第一个环上
		playerCircleIndex = 0;

		//实例化一个环200
		tempCircle = Instantiate (MyClass.CirclesResources[0],
		                          MyClass.circleStartPosition,
		                          MyClass.CirclesResources[0].transform.rotation) as GameObject;


		//该圆环注册入集合
		circleArray.Add (tempCircle);

		//计算下一个圆环的位置
		tempPosition  = (circleArray [circleArray.Count - 1] as GameObject).transform.position +
		                new Vector3 ((circleArray [circleArray.Count - 1] as GameObject).GetComponent<Circle>().radius, 0, 0) +
						new Vector3 (MyClass.CirclesResources [1].GetComponent<Circle>().radius, 0, 0);

		//实例化一个环300
		tempCircle = Instantiate (MyClass.CirclesResources[1],
		                          tempPosition,
		                          MyClass.CirclesResources[1].transform.rotation) as GameObject;

		//该圆环注册入集合
		circleArray.Add (tempCircle);

		//玩家位于第playerCircleIndex个环上
		(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().playerStay = true;

		//第一个环不可以得分
		(circleArray [0] as GameObject).GetComponent<Circle> ().scoreEnable = false;

		//初始化临时数组
		originalCirclePosition = new Vector2[1000];

		//使用itween的valueto函数
		iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1.0F, "time", 0.3F, "easetype", iTween.EaseType.linear, "onupdate", "TestUpdate", "oncomplete", "TestCompleted"));
	}

	void TestUpdate(float value){

//		testValue = value * 2;
	}

	void TestCompleted(){

//		testValue = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//如果允许摄像机旋转
		if(cameraRotateEnable){

			//摄像机旋转
			this.transform.Rotate (Vector3.forward, cameraRotateSpeed * Time.fixedDeltaTime);

			//计算总角度
			cameraTotalAngle += Mathf.Abs (cameraRotateSpeed) * Time.fixedDeltaTime;

			//如果总角度超过40度
			if (cameraTotalAngle >= 40) {

				//反向
				cameraRotateSpeed = - cameraRotateSpeed;

				//角度清0
				cameraTotalAngle = 0;
			}
		}

		//如果圆环移动

	}

	//当该组件不可用时（程序退出等等）
	void OnDisable() {
		
		//将当前的分数写入playerprefs
		PlayerPrefs.SetInt ("bestScore", MyClass.bestScore);
		
		//将当前的玩过次数写入playerprefs
		PlayerPrefs.SetInt ("playedCount", MyClass.playedCount);
	}

	//方法，执行屏幕点击之后的相关操作
	public void ScreenClick(){

		testRayCastHit2D_1 = new RaycastHit2D();
		testRayCastHit2D_2 = new RaycastHit2D();

		startPos = (circleArray [playerCircleIndex] as GameObject).transform.position;
		r1 = (circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().radius;

		//如果当前主角所在的圆环不是第一个环
		if (playerCircleIndex > 0) {
			
			//从当前圆环圆心，向上一个圆环圆心，发射射线线段
			endPos1 = (circleArray [playerCircleIndex - 1] as GameObject).transform.position;
			r2 = (circleArray [playerCircleIndex - 1] as GameObject).GetComponent<Circle> ().radius;
			testRayCastHit2D_1 = Physics2D.Linecast (startPos, endPos1, 1 << LayerMask.NameToLayer("Player"));

			//比例
			rate1 = (r2 - 0.03F) / (r1 + r2);
		}

		//如果当前主角所在的圆环不是最后一个环
		if(playerCircleIndex < (circleArray.Count - 1)){
		
			//从当前圆环圆心，向下一个圆环圆心，发射射线线段
			endPos2 = (circleArray [playerCircleIndex + 1] as GameObject).transform.position;
			r2 = (circleArray [playerCircleIndex + 1] as GameObject).GetComponent<Circle> ().radius;
			testRayCastHit2D_2 = Physics2D.Linecast (startPos, endPos2, 1 << LayerMask.NameToLayer("Player"));

			//比例
			rate2 = (r2 - 0.03F) / (r1 + r2);
		}

		//如果向前发的射线碰到主角
		if (testRayCastHit2D_1.collider != null) {
			
			//当前圆环与下一个圆环的交点
			crossPoint = startPos * rate1 + endPos1 * (1 - rate1);
			
			//当前圈的标志位改变
			(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().playerStay = false;
			
			//进入上一个圈
			playerCircleIndex --;
			
			//上一个圈的标志位改变
			(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().playerStay = true;
			
			//玩家进入圈内
			playerPositionState = 0;
			
			//玩家旋转方向与当前圈相反
			playerRotateDirection = 1 - playerRotateDirection;
			
			//计算新激活的球的索引
			playerCurrentIndex = playerRotateDirection * 2 + playerPositionState;
			
			//新激活的球的位置
			(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).position = crossPoint;
			
			//新激活的球的旋转
			if(playerCurrentIndex % 2 == 0){
				
				//调整球的旋转，使其Y轴正对圆心
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, (circleArray [playerCircleIndex] as GameObject).transform.position - crossPoint);
			}
			
			else{
				
				//调整球的旋转，使其Y轴正对圆心，逆时针旋转的球体需要做一个180度的调整
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, crossPoint - (circleArray [playerCircleIndex] as GameObject).transform.position);
			}

			//调整位置
			CirclesPositionAdjust();
		}
			
		//如果向后发射的射线碰到主角
		else if (testRayCastHit2D_2.collider != null) {
			
			//当前圆环与下一个圆环的交点
			crossPoint = startPos * rate2 + endPos2 * (1 - rate2);
			
			//当前圈的标志位改变
			(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().playerStay = false;
			
			//进入下一个圈
			playerCircleIndex ++;
			
			//下一个圈的标志位改变
			(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle> ().playerStay = true;
			
			//玩家进入圈内
			playerPositionState = 0;
			
			//玩家旋转方向与当前圈相反
			playerRotateDirection = 1 - playerRotateDirection;

			//计算新激活的球的索引
			playerCurrentIndex = playerRotateDirection * 2 + playerPositionState;
			
			//新激活的球的位置
			(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).position = crossPoint;
			
			//新激活的球的旋转
			if(playerCurrentIndex % 2 == 0){
				
				//调整球的旋转，使其Y轴正对圆心
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, (circleArray [playerCircleIndex] as GameObject).transform.position - crossPoint);
			}
			
			else{
				
				//调整球的旋转，使其Y轴正对圆心，逆时针旋转的球体需要做一个180度的调整
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, crossPoint - (circleArray [playerCircleIndex] as GameObject).transform.position);
			}

			//如果该环的得分使能位为true
			if((circleArray [playerCircleIndex] as GameObject).GetComponent<Circle>().scoreEnable){

				//播放得分音效
				MyClass.AudioPlay (selfAudioSource, MyClass.AudioResources[5], MyClass.audioEnable);

				//得分
				score ++;

				//实时更新分数
				currentScoreText.text = score.ToString("00");

				//标志位置为false，下次进入不再得分
				(circleArray [playerCircleIndex] as GameObject).GetComponent<Circle>().scoreEnable = false;

				//只要有得分，就需要刷新新环
				circleCreatEnable = true;
			}

			//调整位置
			CirclesPositionAdjust();
		}
		
		//如果没有碰到主角
		else{

			//播放音效
			MyClass.AudioPlay (selfAudioSource, MyClass.AudioResources[4], MyClass.audioEnable);
			
			//玩家从内圈转到外圈,或从外圈转到内圈
			playerPositionState = 1 - playerPositionState;

			//计算新激活的球的索引
			playerCurrentIndex = playerRotateDirection * 2 + playerPositionState;

			//新激活的球的位置
			(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).position = playerCurrentPositon;

			//新激活的球的旋转
			if(playerCurrentIndex % 2 == 0){
				
				//调整球的旋转，使其Y轴正对圆心
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, (circleArray [playerCircleIndex] as GameObject).transform.position - playerCurrentPositon);
			}
			
			else{

				//调整球的旋转，使其Y轴正对圆心，逆时针旋转的球体需要做一个180度的调整
				(circleArray [playerCircleIndex] as GameObject).transform.GetChild(playerCurrentIndex).rotation = Quaternion.FromToRotation (Vector3.up, playerCurrentPositon - (circleArray [playerCircleIndex] as GameObject).transform.position);
			}
		}
	}

	//方法，刷新圆环
	public void CreatNewCircle(){

		//临时的纵坐标
		Vector2 newCirclePosition;

		//播放新环音效
		MyClass.AudioPlay (selfAudioSource, MyClass.AudioResources[3], MyClass.audioEnable);

		//计算刷新的球索引和方向
		CalculateNewCircleIndex ();

		//屏幕上最右边环的位置
		startPos = (circleArray [circleArray.Count - 1] as GameObject).transform.position;

		//屏幕上最右边环的半径
		r1 = (circleArray [circleArray.Count - 1] as GameObject).GetComponent<Circle> ().radius;

		//新环的半径
		r2 = MyClass.CirclesResources[tempCircleIndex].GetComponent<Circle> ().radius; 

		//刷新点的位置
		endPos = circleRefreshPoints [tempRefreshPointIndex];

		//比例
		rate = (r1 + r2) / Vector2.Distance (startPos, endPos);

		//计算新环的位置
		newCirclePosition = startPos * (1 - rate) + endPos * rate;

		//先实例化一个圆环
		tempCircle = Instantiate (MyClass.CirclesResources[tempCircleIndex],
		                          circleRefreshPoints[tempRefreshPointIndex],
		                          MyClass.CirclesResources[tempCircleIndex].transform.rotation) as GameObject;

		//使用itween的相关函数，使圆环移到相应的位置
		iTween.MoveTo(tempCircle, iTween.Hash("position", (Vector3)newCirclePosition, "time", 0.2F, "easetype", iTween.EaseType.linear, "oncomplete", "NewCircleCreated"));
		
	}

	//方法，圆环整体移动，确保玩家所在环的上一个环位于circleStartPosition
	public void CirclesPositionAdjust(){

		//只有屏幕上超过1个环之后才执行调整
		if (playerCircleIndex > 0) {

			//获取移动之前所有环的位置
			for(int i = 0; i < circleArray.Count; i++){

				//存入临时数组中
				originalCirclePosition[i] = (circleArray[i] as GameObject).transform.position;
			}

			//计算玩家所在环的的位置与circleStartPosition之间的相对位置
			relativePosition = MyClass.circleStartPosition - (circleArray[playerCircleIndex] as GameObject).transform.position;

			//使用itween的valueto函数
			iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1.0F, "time", 0.3F, "easetype", iTween.EaseType.linear, "onupdate", "CircleMove", "oncomplete", "CircleMoveCompleted"));
		
			//圆环整体移动时，摄像机停止旋转
			cameraRotateEnable = false;
		}
	}

	//方法，适时调整屏幕中所有环的位置
	public void CircleMove(float value){

		//所有的环整体移动
		for (int i = 0; i < circleArray.Count; i++) {

			(circleArray[i] as GameObject).transform.position = originalCirclePosition[i] + value * relativePosition;
		}
	}

	//方法，执行圆环整体移动之后的操作
	public void CircleMoveCompleted(){

		//圆环整体移动结束，摄像机恢复旋转
		cameraRotateEnable = true;

		//如果需要刷新新环
		if (circleCreatEnable) {

			//刷新一个新环
			CreatNewCircle();

			//标志位改变
			circleCreatEnable = false;
		}
	}
		
	//方法，计算刷新的环的索引
	public void CalculateNewCircleIndex(){

		//临时索引
		int tempIndex = 0;

		//如果得到一分（此时主角位于第二个环上，准备刷新第三个环）
		if (score <= 1) {

			tempIndex = Random.Range(0, 3);

			if(tempIndex < 2){

				//随机刷新环200和环300
				tempCircleIndex = tempIndex;

				//随机从上方或者下方刷新
				tempRefreshPointIndex = Random.Range(0, 2);
			}

			if(tempIndex == 2){

				//刷新的环的索引
				tempCircleIndex = 3 - playerRotateDirection;

				//刷新的环的方向
				tempRefreshPointIndex = 1 - playerRotateDirection;
			}
		}

		//如果用户的得分为2,3,4分
		else if(score < 5){

			//如果当前玩家顺时针旋转
			if(playerRotateDirection == 0){

				//从下方刷新逆时针障碍物
				tempCircleIndex = Random.Range(8, 12);
				tempRefreshPointIndex = 1;
			}

			//玩家逆时针旋转
			else{

				//从上方刷新顺时针障碍物
				tempCircleIndex = Random.Range(4, 8);
				tempRefreshPointIndex = 0;
			}
		}

		//如果用户的得分为5 - 9分
		else if(score < 10){

			//如果当前玩家顺时针旋转
			if(playerRotateDirection == 0){
				
				tempCircleIndex = Random.Range(19, 26);
				
				tempRefreshPointIndex = 1;
			}
			
			//玩家逆时针旋转
			else{
				
				tempCircleIndex = Random.Range(12, 19);
				
				tempRefreshPointIndex = 0;
			}
		}

		//如果用户的分数超过10分
		else{

			//如果当前玩家顺时针旋转
			if(playerRotateDirection == 0){
				
				tempCircleIndex = Random.Range(31, 36);
				
				tempRefreshPointIndex = 1;
			}
			
			//玩家逆时针旋转
			else{
				
				tempCircleIndex = Random.Range(26, 31);
				
				tempRefreshPointIndex = 0;
			}
		}
	}

	//方法，执行游戏失败之后的相关操作
	public void GameDefeat(){

		//游戏状态改变
		MyClass.gameState = GameState.Defeat;

		//摄像机停止旋转
		cameraRotateEnable = false;

		//背景音乐停止播放
		MyClass.AudioStop (selfAudioSource);

		//播放死亡音效
		MyClass.AudioPlay (selfAudioSource, MyClass.AudioResources [2], MyClass.audioEnable);

		//截图
		SdkToU3d.ScreenShot ();

		//在玩家当前位置实例化一枚粒子特效
		Instantiate (MyClass.ParticleResources[0],
		             playerCurrentPositon,
		             MyClass.ParticleResources[0].transform.rotation);

		//如果本次得分超过历史最佳得分
		if (score > MyClass.bestScore) {

			//将本次成绩记录为历史最佳成绩
			MyClass.bestScore = score;

			//上传最高分
			SdkToU3d.ReportScore(MyClass.bestScore);

			//新纪录的标志图片激活
			newScoreImage.SetActive(true);
		}

		//玩家玩的次数加1
		MyClass.playedCount ++;

		//0.5秒钟之后显示失败界面
		Invoke ("DefeatInterfaceDisplay", 0.5F);
	}

	//方法，显示失败界面
	public void DefeatInterfaceDisplay(){

		//失败界面激活
		defeatImage.SetActive (true);
	}
}
