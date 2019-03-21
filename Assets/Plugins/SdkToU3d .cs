using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static  class SdkToU3d {

	// 展示插屏广告，在结算界面初始化时调用，几率30%
	[DllImport( "__Internal" )]
	private static extern void showChartboostInterstitial();

	// 评论按钮
	[DllImport( "__Internal" )]
	private static extern void commentBtn();

	// 打开排行版
	[DllImport( "__Internal" )]
	private static extern void openGameCenter();

	// 截图，在游戏结束时候，结算页面显示之前调用
	[DllImport( "__Internal" )]
	private static extern void screenShot();

	// 结算页面上分享按钮
	[DllImport( "__Internal" )]
	private static extern void share();

	// 付费去广告
	[DllImport( "__Internal" )]
	private static extern void buy();

	// 恢复购买
	[DllImport( "__Internal" )]
	private static extern void restore();

	// 上传最高分
	[DllImport( "__Internal" )]
	private static extern void reportScore(int A);

	
	// 展示插屏广告，在结算界面初始化时调用，几率30%
	public static void ShowChartboostInterstitial()
	{
		//showChartboostInterstitial();
	}

	// 评论
	public static void Comment()
	{
		//commentBtn();
	}

	// 排行版
	public static void OpenGameCenter()
	{
		//openGameCenter ();
	}

	// 截图，在游戏结束时候，结算页面显示之前调用
	public static void ScreenShot()
	{
		//screenShot();
	}

	// 分享按钮
	public static void Share()
	{
		//share();
	}

	// 付费去广告
	public static void Buy()
	{
		//buy();
	}

	// 恢复购买
	public static void Restore()
	{
		//restore();
	}

	//上传最高分
	public static void ReportScore(int A)
	{
		//reportScore(A);
	}
}
