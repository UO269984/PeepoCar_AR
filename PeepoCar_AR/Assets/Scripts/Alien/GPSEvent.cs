using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GPSEvent : MonoBehaviour {
	public abstract void Run(bool reached);
}