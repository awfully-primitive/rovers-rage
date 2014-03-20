using UnityEngine;
using System.Collections;

// Better switch to a global script
// or global script to start dragging

public class StickyEntityBehavior : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {

	}

	void OnMouseDown() {
		var ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject == gameObject) {
				_isGrabbed = true;
				hint.rigidbody.MovePosition(new Vector3(0, gameObject.transform.position.y, 0));
				var q = new Quaternion();
				q.SetLookRotation(cam.transform.forward);
				hint.rigidbody.MoveRotation(q);
				_plane = new Plane(-cam.transform.forward, new Vector3(0, gameObject.transform.position.y, 0));
			}
		}
	}
	
	void OnMouseDrag() {
		if (_isGrabbed) {
			var ray = cam.ScreenPointToRay(Input.mousePosition);

			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			gameObject.collider.enabled = false;

			RaycastHit hit;
			float enter;
			if (Physics.Raycast(ray, out hit)) {
				gameObject.rigidbody.MovePosition(radialSnappoint.transform.localPosition.magnitude * hit.normal + hit.point);
				var q = new Quaternion();
				q.SetLookRotation(hit.normal);
				gameObject.rigidbody.MoveRotation(q);
			} 
			else if (_plane.Raycast(ray, out enter)) {
				var point = ray.origin + ray.direction * enter;
				gameObject.transform.position = point;

				gameObject.transform.rotation = Quaternion.identity;
			}

			gameObject.collider.enabled = true;
		}
	}

	public bool radialSnapping = true;
	public Camera cam;
	public GameObject hint;
	public GameObject radialSnappoint;

	private bool _isGrabbed = false;
	private Plane _plane;
}
