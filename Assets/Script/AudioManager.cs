using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public sound[] sounds;

	// Use this for initialization
	void Start () {
		foreach (sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}

		PlaySound ("BackSound");
		PlaySound ("SoundCar");
	}
	public void PlaySound(string name)
	{
		foreach (sound s in sounds) {
			if (s.name == name)
				s.source.Play ();
		}
	}
}
