using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnAudio : GPSEvent {
	
	public override void Run(bool reached) {
		if (reached)
			GetComponent<AudioSource>().Play();
	}
}