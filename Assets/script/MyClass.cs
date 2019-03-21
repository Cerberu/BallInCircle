using UnityEngine;
using System.Collections;

//游戏状态枚举
public enum GameState{Playing, Defeat};

public class MyClass {

	//数组，存储所有不同大小，不同player，不同障碍物的圆环资源
	public static GameObject[] CirclesResources = Resources.LoadAll<GameObject>("circle");

	//数组，存储所有的音效资源
	public static AudioClip[] AudioResources = Resources.LoadAll<AudioClip> ("audio");

	//数组，存储所有的粒子特效资源
	public static ParticleSystem[] ParticleResources = Resources.LoadAll<ParticleSystem>("particle");

	//起始时，第一个环所在位置，这个位置是一个相对固定的位置，在这个游戏中为屏幕中心
	public static Vector3 circleStartPosition = new Vector3 (6.67F, 3.75F, 0);

	//游戏状态
	public static GameState gameState = GameState.Playing;

	//玩家的历史最佳成绩
	public static int bestScore;

	//玩家一共玩了多少次
	public static int playedCount;

	//bool值，声音开关，默认打开
	public static bool audioEnable = true;


	//方法，通用的播放音频的方法
	public static void AudioPlay(AudioSource audio_source, bool enable){
		
		//如果音频激活，并且声音开关也激活
		if (audio_source != null && enable) {
			
			//播放对应的音频
			audio_source.Play();
		}
	}

	//重载方法
	public static void AudioPlay(AudioSource audio_source, AudioClip audio_clip, bool enabled){
		
		//如果音频激活
		if (audio_source != null && enabled) {
			
			//播放对应的音频
			audio_source.PlayOneShot(audio_clip);
		}
	}

	//通用的音频停止方法
	public static void AudioStop(AudioSource audio_source){
		
		//如果音频非空
		if (audio_source != null) {
			
			audio_source.Stop();
		}
	}
}
