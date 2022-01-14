using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAlien : GPSEvent {
	
	public GameObject alienImage;
	public GameObject alienModel;
	
	public AudioSource[] alienAudios;
	
	private bool inTarget = false;
	private bool markerDetected = false;
	
	public override void Run(bool reached) {
		this.inTarget = reached;
		
		Array.ForEach(this.alienAudios,
			this.inTarget ? (Action<AudioSource>) (audio => audio.Play()) : (Action<AudioSource>) (audio => audio.Stop()));
		
		UpdateAlien();
	}
	
	public void SetMarkerDetected(bool markerDetected) {
		this.markerDetected = markerDetected;
		UpdateAlien();
	}
	
	private void UpdateAlien() {
		this.alienImage.SetActive(this.inTarget && ! this.markerDetected);
		this.alienModel.SetActive(this.inTarget);
	}
}