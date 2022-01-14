using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecsPanelRot : MonoBehaviour {
	
	public float rotSpeed = 2f;
	public Transform parentTransform;
	public Transform cameraTransform;
	
	public void Update() {
		Vector3 panelToCamera = this.parentTransform.InverseTransformVector(this.cameraTransform.position - transform.position);
		panelToCamera.y = 0;
		
		Quaternion targetRot = Quaternion.LookRotation(panelToCamera) * Quaternion.Euler(90, 0, 0);
		float angle = Quaternion.Angle(transform.localRotation, targetRot);
		transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, this.rotSpeed * angle * Time.deltaTime);
	}
}