using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : MonoBehaviour {
	public Material lightsMat;
	public Color onColor = new Color(0.8f, 0.71791f, 0.1152582f);
	public Color offColor = Color.black;
	
	private bool lightsOn = false;
	
	public void Start() {
		UpdateColor();
	}
	
	public void OnMouseDown() {
		this.lightsOn = ! this.lightsOn;
		UpdateColor();
	}
	
	private void UpdateColor() {
		this.lightsMat.SetColor("_EmissionColor", this.lightsOn ? this.onColor : this.offColor);
	}
}