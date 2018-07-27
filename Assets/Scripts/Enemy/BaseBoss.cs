using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : MonoBehaviour {
	protected float nextShotTime;
	protected float msBetweenShots;
	protected float health;
	protected Animator animatorBoss;
	protected Material materialBoss;
	public Transform targetPlayer;

	public Transform[] Muzzles;

	public void Initialization(float hp, float speedOfShip, float MsBetweenShots, Animator animatorOfShip, Material material)
	{
		this.health = hp;
		
		this.msBetweenShots = MsBetweenShots;
		this.animatorBoss = animatorOfShip;
		this.materialBoss = material;

	}

	public void TakeDamage(float damageTaken)
	{

	}

	public void ChangePhase(string NameOfPhase)
	{

	}

	public void die()
	{

	}

	public abstract void Shoot();

	public void Update()
	{
		Shoot();
	}

}
