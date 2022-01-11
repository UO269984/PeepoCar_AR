using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRearView : MonoBehaviour {
	public Transform rightRearView;
	public Transform leftRearView;
	public float closeSpeed = 1;
	public float maxCloseAngle = 60;
	
	private Vector3 rightRearViewInitRot;
	private Vector3 leftRearViewInitRot;
	
	private float curRot = 0;
	private bool rearViewClosed = false;
	
	private AudioSource audioSource;
	
	public void Start() {
		this.rightRearViewInitRot = this.rightRearView.localEulerAngles;
		this.leftRearViewInitRot = this.rightRearView.localEulerAngles;
		
		this.audioSource = GetComponent<AudioSource>();
	}
	
	public void Update() {
		float speed = 0;
		if (this.rearViewClosed && this.curRot < this.maxCloseAngle)
			speed = this.closeSpeed;
		
		else if (! this.rearViewClosed && this.curRot > 0)
			speed = -this.closeSpeed;
		
		else if (this.audioSource.isPlaying)
			this.audioSource.Stop();
		
		curRot += this.maxCloseAngle * speed * Time.deltaTime;
		SetRearViewRotation(curRot);
	}
	
	private void SetRearViewRotation(float zRot) {
		this.rightRearView.localRotation = Quaternion.Euler(this.rightRearViewInitRot.x, this.rightRearViewInitRot.y, zRot);
		this.leftRearView.localRotation = Quaternion.Euler(this.leftRearViewInitRot.x, this.leftRearViewInitRot.y, -zRot);
	}
	
	public void OnMouseDown() {
		this.rearViewClosed = ! this.rearViewClosed;
		this.audioSource.Play();
	}
}