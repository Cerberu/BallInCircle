using UnityEngine;
using System.Collections;

public class DefeatImage : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//获得一个1到100的随机数
		int tempNum = Random.Range (1, 101);

		//如果随机数小于31
		if (tempNum < 31) {

			//显示插屏广告
			SdkToU3d.ShowChartboostInterstitial ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
