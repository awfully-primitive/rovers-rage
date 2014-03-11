using UnityEngine;
using System.Collections;
using System;

public class EnginePanel : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		if (engine != null) {
			_useMotor = engine.useMotor;
			_targetVelocity = engine.motor.targetVelocity;
			_targetVelocityText = _targetVelocity.ToString();
			_force = engine.motor.force;
			_forceText = _force.ToString();
			_freeSpin = engine.motor.freeSpin;
		}
	}
    
	// Update is called once per frame
	void Update()
	{
		if (engine != null) {
			engine.useMotor = _useMotor;
			engine.motor = new JointMotor() { 
                targetVelocity = _targetVelocity,
                force = _force,
                freeSpin = _freeSpin
            };
		}
	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect(5, 5, 300, 150));
		GUI.Box(new Rect(0, 0, 300, 150), GUIContent.none);
		GUI.Label(new Rect(5, 5, 290, 20), "Motor");

		GUI.Label(new Rect(5, 40, 170, 35), "Use Motor");
		_useMotor = GUI.Toggle(new Rect(185, 40, 20, 20), _useMotor, GUIContent.none);

		try {
			GUI.Label(new Rect(5, 70, 170, 35), "Target Velocity");
			_targetVelocityText = GUI.TextField(new Rect(185, 70, 100, 20), _targetVelocityText);
			_targetVelocity = float.Parse(_targetVelocityText);
		} catch (FormatException) {
		}

		try {
			GUI.Label(new Rect(5, 100, 170, 35), "Force");
			_forceText = GUI.TextField(new Rect(185, 100, 100, 20), _forceText);
			_force = float.Parse(_forceText);
		} catch (FormatException) {
		}

		GUI.Label(new Rect(5, 130, 170, 35), "Free spin");
		_freeSpin = GUI.Toggle(new Rect(185, 130, 20, 20), _freeSpin, GUIContent.none);

		GUI.EndGroup();
	}

	public HingeJoint engine = null;
	private bool _useMotor = false;
	private string _targetVelocityText = "";
	private float _targetVelocity = 0.0f;
	private string _forceText = "";
	private float _force = 0.0f;
	private bool _freeSpin = false;
}
