﻿using UnityEngine;

/// <summary>
/// The first-person camera behaviour
/// </summary>
public class GameCamera : MonoBehaviour
{
	[SerializeField] private Vector2 yConstraint;
	[SerializeField] private Vector2 sensitivity;

	private Transform weapon;
	private Transform player;
	private Transform eyes;

	private float rotationY;

	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void Update()
	{
		if(player == null || weapon == null || eyes == null)
		{
			Entity p = EntityUtils.GetEntityWithTag("Player");

			if(p == null)
			{
				Debug.LogError("No player found");
				return;
			}

			player = p.transform;
			weapon = player.Find("Weapon");
			eyes = player.Find("Eyes");
		}

		if(Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		UpdateCamera();

		// Move the camera to the player's eyes
		Camera.main.transform.position = eyes.position;
		Camera.main.transform.rotation = eyes.rotation;
	}

	private void UpdateCamera()
	{
		float rotationX = player.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity.x;

		rotationY += Input.GetAxis("Mouse Y") * sensitivity.y;
		rotationY = Mathf.Clamp(rotationY, yConstraint.x, yConstraint.y);

		player.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
}