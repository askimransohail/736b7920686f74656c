using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private FloatingJoystick floatingJoystick;
	[SerializeField] private float speed;
	[SerializeField] private float rotationSpeed;

	[NonSerialized] private Camera cameraMain;
	[SerializeField] private Vector3 cameraOffset;

	private CharacterAnimator animController;
	

	void Start()
	{
		cameraMain = Camera.main;
		animController = GetComponent<CharacterAnimator>();
	}

	public void FixedUpdate()
	{
		if (floatingJoystick.Vertical != 0 || floatingJoystick.Horizontal != 0)
		{
			animController.SetWalkingTrue();

			Transform camTransform = cameraMain.transform;
			Vector3 camPosition = new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z);
			Vector3 direction = (transform.position - camPosition).normalized;
			Vector3 forwardMovement = direction * floatingJoystick.Vertical;
			Vector3 horizontalMovement = camTransform.right * floatingJoystick.Horizontal;
			Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);

			transform.Translate(movement * speed * Time.deltaTime, Space.World);

			Vector3 rotationDirection = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotationDirection), rotationSpeed * Time.fixedDeltaTime);

			cameraMain.transform.position = (cameraOffset + this.transform.position);
		}
		else
		{
			animController.SetWalkingFalse();
		}
	}

}
