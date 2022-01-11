using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
	private AudioSource audioSource;
	
	public void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}
	
	public void OnMouseDown() {
		this.audioSource.Play();
	}
}