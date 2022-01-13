using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAlienScene : MonoBehaviour {
	
	public void OnMouseDown() {
		SceneManager.LoadScene("AlienScene");
	}
}