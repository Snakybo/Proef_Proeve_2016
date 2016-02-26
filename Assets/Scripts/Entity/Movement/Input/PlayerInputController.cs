﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
	public float InputX
	{
		get
		{
			return Input.GetAxis("Horizontal");
		}
	}

	public float InputZ
	{
		get
		{
			return Input.GetAxis("Vertical");
		}
	}


	public override bool Jump
	{
		get
		{
			return Input.GetKey(KeyCode.Space);
		}
	}

	public override bool Crouch
	{
		get
		{
			return Input.GetKey(KeyCode.C);
		}
	}

	protected void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			GlobalEvents.Invoke(new StartWeaponFireEvent(Entity));
		}
		else if(Input.GetMouseButtonUp(0))
		{
			GlobalEvents.Invoke(new StopWeaponFireEvent(Entity));
		}

		if(Input.GetKeyDown(KeyCode.V))
		{
			GlobalEvents.Invoke(new SwitchFireModeEvent(Entity));
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			GlobalEvents.Invoke(new ReloadWeaponEvent(Entity));
		}
	}
}