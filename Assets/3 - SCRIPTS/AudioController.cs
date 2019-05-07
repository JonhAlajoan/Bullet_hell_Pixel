using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	#region BACKGROUND_MUSIC
	public AudioClip[] backgroundMusicClipsArray;
	public AudioSource backgroundMusicSource;
	#endregion

	#region SFX_SOUNDS
	[SerializeField]
	public AudioClip[] SFXSounds;
	[SerializeField]
	public AudioSource SFXAudioSource;
	#endregion

	#region PUBLIC METHODS
	void Start()
	{
		foreach (AudioClip clip in SFXSounds)
		{
			clip.LoadAudioData();
		}
	}

	public void PlayRandomMusic()
	{
		backgroundMusicSource.clip = backgroundMusicClipsArray[Random.Range(0, backgroundMusicClipsArray.Length)];
		backgroundMusicSource.Play();
		Invoke("PlayRandomMusic", backgroundMusicSource.clip.length);
	}

	public void PlaySFXSounds(string clipToPlay)
	{
		foreach (AudioClip clip in SFXSounds)
		{
			if (clip.name == clipToPlay)
			{
				SFXAudioSource.PlayOneShot(clip);
			}
		}

	}
	#endregion
}
