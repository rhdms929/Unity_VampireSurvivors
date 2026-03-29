using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[Header("# BGM")]
	public AudioClip bgmClip;
	public float bgmVolume;
	AudioSource bgmPlayer;
	AudioHighPassFilter bgmEffect;

	[Header("# SFX")]
	public AudioClip[] sfxClips;
	public float sfxVolume;
	public int channels;
	AudioSource[] sfxPlayers;
	int channelIndex;
	public enum SFX
	{
		Dead, Hit, LevelUp = 3,
		Lose, Melee, Range = 7,
		Select, Win, Exp
	}

	void Awake()
	{
		instance = this;
		Init();
	}

	void Init()
	{
		// 배경음 플레이어 초기화
		GameObject bgmObj = new GameObject("BGMPlayer");
		bgmObj.transform.parent = transform;
		bgmPlayer = bgmObj.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = bgmVolume;
		bgmPlayer.clip = bgmClip;
		bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

		// 효과음 플레이어 초기화
		GameObject sfxObj = new GameObject("sfxPlayer");
		sfxObj.transform.parent = transform;
		sfxPlayers = new AudioSource[channels];

		for(int i=0; i < sfxPlayers.Length; i++)
		{
			sfxPlayers[i] = sfxObj.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].bypassListenerEffects = true;
			sfxPlayers[i].volume = sfxVolume;
		}
	}
	public void PlayBgm(bool isPlay)
	{
		if (isPlay)
		{
			bgmPlayer.Play();
		}
		else
		{
			bgmPlayer.Stop();
		}
	}
	public void EffectBgm(bool isPlay)
	{
		bgmEffect.enabled = isPlay;
	}

	public void PlaySfx(SFX sfx)
	{
		for(int i=0; i < sfxPlayers.Length; i++) { 
			int loopIndex = (i + channelIndex) % sfxPlayers.Length;

			if (sfxPlayers[loopIndex].isPlaying)
				continue;

			int randomIndex = 0;
			if (sfx == SFX.Hit || sfx == SFX.Melee)
			{
				randomIndex = Random.Range(0, 2);
			}

			channelIndex = loopIndex;
			sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + randomIndex];
			sfxPlayers[loopIndex].Play();
			break;
		}
	}
}
