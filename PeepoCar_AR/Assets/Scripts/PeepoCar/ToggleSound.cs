using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour {
	private AudioSource audioSource;
	private bool soundOn = false;
	
	public void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}
	
	public void OnMouseDown() {
		this.soundOn = ! this.soundOn;
		
		if (this.soundOn)
			this.audioSource.Play();
		
		else
			this.audioSource.Stop();
	}
}