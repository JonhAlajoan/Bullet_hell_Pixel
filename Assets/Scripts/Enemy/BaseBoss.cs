using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBoss : MonoBehaviour
{

	protected float nextShotTime;
	protected float msBetweenShots;
	protected float health;
	protected Animator animatorBoss;
	public Transform targetPlayer;

	[SerializeField]
	public SpriteRenderer[] spritesBoss;

	public Transform[] Muzzles;

	float count = 0;

	public float colourChangeDelay = 0.5f;
	float currentDelay = 0f;
	bool colourChangeCollision = false;

	public void Initialization(float _hp, float _speedOfShip, float _MsBetweenShots, Animator _animatorOfShip)
	{
		this.health = _hp;		
		this.msBetweenShots = _MsBetweenShots;
		this.animatorBoss = _animatorOfShip;
	}

	public void TakeDamage(float damageTaken)
	{
		colourChangeCollision = true;
		currentDelay = Time.time + colourChangeDelay;
		health -= damageTaken;
		
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
		changeColor();
		
	}

	public void changeColor()
	{
		float duration = 0.05f;
		float lerp = Mathf.PingPong(Time.time, duration) / duration;

		if (colourChangeCollision)
		{
			foreach (SpriteRenderer spriteToBeModified in spritesBoss)
			{
				spriteToBeModified.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(0, 0, 0, 255), lerp);
			}
			if (Time.time > currentDelay)
			{
				foreach (SpriteRenderer spriteToBeModified in spritesBoss)
				{
					spriteToBeModified.color = new Color32(255, 255, 255, 255);
				}
				colourChangeCollision = false;
			}
		}
		

	

		
	}

}
