using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

///--------------------------------
/// Author: Victor Alvarez, Ph.D.
/// University of Oviedo, Spain
///
/// Author: Daniel Pérez Prádanos
///--------------------------------

public class GPS : MonoBehaviour {
	
	private float timeElapsed = 0f;
	private bool gpsEnabled = false;
	public float refreshTime = 10f;
	
	private Location curLocation;
	private double goalDist;
	private List<Goal> goalsReached = new List<Goal>();
	
	public Text gpsStatusText, coordsText;
	
	public Goal[] goals;
	private IEnumerator<Goal> goalsIter;
	
	/**
	 * This method first launch the permissions to obtain the location.
	 */
	private void Start() {
		this.goalsIter = ((IEnumerable<Goal>) this.goals).GetEnumerator();
		this.goalsIter.MoveNext();
		
		DontDestroyOnLoad(gameObject);
		StartCoroutine(StartLocationService());
	}
	
	private void Update() {
		this.timeElapsed += Time.deltaTime;
		
		if (this.gpsEnabled && this.timeElapsed >= this.refreshTime) {
			UpdateGPS();
			this.timeElapsed = 0f;
		}
	}
	
	/**
	 * It waits up to 20 seconds to obtain the location. 
	 */
	private IEnumerator StartLocationService() {
		if (! Input.location.isEnabledByUser) {
			gpsStatusText.text = "GPS is not enabled";
			yield break;
		}
		
		Input.location.Start(10, 0.1f);
		int maxWait = 20;
		
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		if (maxWait <= 0) {
			gpsStatusText.text = "GPS initialisation time out";
			yield break;
		}
		
		if (Input.location.status == LocationServiceStatus.Failed) {
			gpsStatusText.text = "Unable to determine device location";
			yield break;
		}
		
		this.gpsEnabled = true; // All good, GPS is enabled
		gpsStatusText.text = "GPS " + Input.location.status.ToString();
			
		yield break;
	}
	
	private void UpdateGPS() {
		this.curLocation = new Location{latitude = Input.location.lastData.latitude, longitude = Input.location.lastData.longitude};
		this.goalDist = DistanceBetweenCoordinates.DistanceTo(this.goalsIter.Current.location, this.curLocation);
		
		CheckGoal();
		UpdateText();
	}
	
	private void CheckGoal() {
		if (! this.goalsReached.Contains(this.goalsIter.Current) && this.goalDist <= this.goalsIter.Current.radius) {
			this.goalsIter.Current.gpsEvent.Run(true);
			this.goalsReached.Add(this.goalsIter.Current);
			
			if (! this.goalsIter.MoveNext()) { // next point of interest
				this.goalsIter.Reset();
				this.goalsIter.MoveNext();
			}
		}
		
		for (int i = 0; i < this.goalsReached.Count;) {
			Goal goal = this.goalsReached[i];
			
			if (DistanceBetweenCoordinates.DistanceTo(goal.location, this.curLocation) > goal.radius) {
				goal.gpsEvent.Run(false);
				this.goalsReached.RemoveAt(i);
			}
			else
				i++;
		}
	}
	
	private void UpdateText() {
		Location goalLocation = this.goalsIter.Current.location;
		
		this.coordsText.text = goalLocation.latitude.ToString("n6") + "\n"
			+ goalLocation.longitude.ToString("n6")
			+ "\nDistance to goal: " + this.goalDist.ToString("n0") + " metres";
	}
}

[System.Serializable]
public struct Location {
	public double latitude;
	public double longitude;
}

[System.Serializable]
public struct Goal {
	public Location location;
	public double radius;
	public GPSEvent gpsEvent;
}

public static class DistanceBetweenCoordinates {
	public static double DistanceTo(this Location baseCoordinates, Location targetCoordinates) {
		return DistanceTo(baseCoordinates, targetCoordinates, UnitOfLength.Kilometre);
	}
	
	public static double DistanceTo(this Location baseCoordinates, Location targetCoordinates, UnitOfLength unitOfLength) {
		var baseRad = Math.PI * baseCoordinates.latitude / 180;
		var targetRad = Math.PI * targetCoordinates.latitude / 180;
		var theta = baseCoordinates.longitude - targetCoordinates.longitude;
		var thetaRad = Math.PI * theta / 180;
		
		double dist =
			Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
			Math.Cos(targetRad) * Math.Cos(thetaRad);
		
		dist = Math.Acos(dist);
		
		dist = dist * 180 / Math.PI;
		dist = dist * 60 * 1.1515;
		
		return unitOfLength.ConvertFromMiles(dist) * 1000; //To meters
	}
}

public class UnitOfLength {
	public static UnitOfLength Kilometre = new UnitOfLength(1.609344);
	public static UnitOfLength NauticalMile = new UnitOfLength(0.8684);
	public static UnitOfLength Mile = new UnitOfLength(1);
	
	private readonly double _fromMilesFactor;
	
	private UnitOfLength(double fromMilesFactor) {
		_fromMilesFactor = fromMilesFactor;
	}
	
	public double ConvertFromMiles(double input) {
		return input * _fromMilesFactor;
	}
}