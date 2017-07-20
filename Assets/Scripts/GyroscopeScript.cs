using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeScript : MonoBehaviour {

	public Transform player;
	public Transform head;
	public Transform root;

	private Gyroscope gyro;
	private bool gyroSupported;
	private Quaternion rotationFix;

	//to fix rotation this gyro needs a parent transform with this rotation: Quaternion.Euler (90, 180, 0);

	// Use this for initialization
	void Start () {

		gyroSupported = SystemInfo.supportsGyroscope;

		if (gyroSupported) {
			gyro = Input.gyro;
			gyro.enabled = true;

			rotationFix = new Quaternion (0, 0, 1, 0);
		}

	}

	// Update is called once per frame
	void Update () {

	}

	void LateUpdate(){
		if (gyroSupported) {
			transform.localRotation = gyro.attitude * rotationFix;
		}

		root.transform.position = head.position;
		player.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
	}
}
