using UnityEngine;
using System.Collections;

public class DragEntityBehavior : MonoBehaviour {

	Transform grabbed;
	public float grabDistance = 10.0f;
	
	public bool useToggleDrag = true; // Didn't know which style you prefer. 
	
	void Update () {
		if (useToggleDrag)
			UpdateToggleDrag();
		else
			UpdateHoldDrag();
	}

	// Toggles drag with mouse click
	void UpdateToggleDrag () {
		if (Input.GetMouseButtonDown(0)) 
			Grab();
		else if (grabbed)
			Drag();
	}
	
	// Drags when user holds down button
	void UpdateHoldDrag () {
		if (Input.GetMouseButton(0)) 
			if (grabbed)
				Drag();
			else 
				Grab();
		else
			grabbed = null;
	}
	
	void Grab() {
		if (grabbed) 
			grabbed = null;
		else {
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))          
				grabbed = hit.transform;
		}
	}
	
	void Drag() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var position = transform.position + transform.forward * grabDistance;
		var plane = new Plane(-transform.forward, position);
		float distance;
		if (plane.Raycast(ray, out distance)) {
			grabbed.position = ray.origin + ray.direction * distance;
			grabbed.rotation = transform.rotation;
		}
	}
}
