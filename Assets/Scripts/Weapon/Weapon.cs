﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEntityInjector
{
	public Entity Entity { set; get; }

	public WeaponUpgrade Upgrade { protected set; get; }

	public GameObject Model { private set; get; }

	[SerializeField] protected WeaponUpgrade baseUpgrade;
	[SerializeField] protected WeaponUpgrade[] upgradeData;

	private List<WeaponModifier> modifiers = new List<WeaponModifier>();
	
	protected virtual void Awake()
	{
		SetUpgrade(baseUpgrade);
	}

	protected virtual void OnEnable()
	{
		Entity.Events.AddListener<StartWeaponFireEvent>(OnStartFireEvent);
		Entity.Events.AddListener<StopWeaponFireEvent>(OnStopFireEvent);
	}

	protected virtual void OnDisable()
	{
		Entity.Events.RemoveListener<StartWeaponFireEvent>(OnStartFireEvent);
		Entity.Events.RemoveListener<StopWeaponFireEvent>(OnStopFireEvent);
	}

	public virtual void StartFire()
	{
	}

	public virtual void StopFire()
	{
	}

	public bool CanFire()
	{
		foreach(WeaponModifier modifier in modifiers)
		{
			if(!modifier.CanFire())
			{
				return false;
			}
		}

		return true;
	}

	public void AddModifier(WeaponModifier modifier)
	{
		modifiers.Add(modifier);
	}

	public void RemoveModifier(WeaponModifier modifier)
	{
		modifiers.Remove(modifier);
	}

	protected abstract HitInfo ConstructHitInfo();

	protected bool Fire()
	{
		if(!CanFire())
		{
			return false;
		}

		HitInfo hitInfo = ConstructHitInfo();
		GlobalEvents.Invoke(new WeaponFireEvent(this, hitInfo));

		if(hitInfo.Hit)
		{
			float damage = Upgrade.BaseDamage;

			// Apply damage modifiers
			if(hitInfo.Tag == "Head")
			{
				damage *= Upgrade.DamageMultipliers.Head;
			}
			else if(hitInfo.Tag == "Body")
			{
				damage *= Upgrade.DamageMultipliers.Body;
			}
			else if(hitInfo.Tag == "Limb")
			{
				damage *= Upgrade.DamageMultipliers.Limbs;
			}

			DamageInfo damageInfo = new DamageInfo(hitInfo, damage);
			DamageEvent damageEvent = new DamageEvent(damageInfo);

			GlobalEvents.Invoke(damageEvent);
			Entity.Events.Invoke(damageEvent);
		}

		return true;
	}

	protected virtual void SetUpgrade(WeaponUpgrade upgrade)
	{
		Upgrade = upgrade;

		if(Model != null)
		{
			Destroy(Model);
		}

		Model = Instantiate(upgrade.Model);
		Model.transform.SetParent(transform, false);
	}

	private void OnStartFireEvent(StartWeaponFireEvent evt)
	{
		if(evt.Entity == Entity)
		{
			StartFire();
		};
	}

	private void OnStopFireEvent(StopWeaponFireEvent evt)
	{
		if(evt.Entity == Entity)
		{
			StopFire();
		}
	}
}