﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{

	public enum States { ATTACKING, PATROLLING, SPAWNING }

	protected float m_nextShotTime;
	protected float m_msBetweenShots;
	protected float m_startingHealth;
	protected float m_health;
	public float speedEnemy;
	protected Animator m_animator;
	protected Transform m_targetPlayer;

	[SerializeField]
	protected SpriteRenderer[] m_sprites;

	public Transform[] Muzzles;

	protected float m_count = 0;

	protected float m_colourChangeDelay = 0.5f;
	protected float m_currentDelay = 0f;
	protected bool m_colourChangeCollision = false;

	protected managerOfScene m_manager;

	protected States m_stateBaseClass;

	//[SerializeField]
	//protected CircleCollider2D m_playerRangeDetector;


	public virtual void Start()
	{
		m_stateBaseClass = States.SPAWNING;
		m_manager = FindObjectOfType<managerOfScene>();
		//m_playerRangeDetector.radius = 25f;
	}

	public void Initialization(float _hp, float _speedOfEnemy, float _MsBetweenShots, Animator _animatorOfEnemy)
	{
		m_startingHealth = _hp;
		m_health = _hp;
		m_msBetweenShots = _MsBetweenShots;
		m_animator = _animatorOfEnemy;
		speedEnemy = _speedOfEnemy;
	}

	public void TakeDamage(float _damageTaken)
	{
		m_colourChangeCollision = true;
		m_currentDelay = Time.time + m_colourChangeDelay;
		m_health -= _damageTaken;
	}


	/*public void die()
	{
		m_manager.changeStateOfCombat(false);
		TrashMan.despawn(gameObject);
	}*/

	public abstract void Shoot(States _actualState);

	public virtual void Update()
	{
		if (m_health <= 0)
		{
		//	die();
		}
		changeColor();

	}


	public void changeColor()
	{
		float _duration = 0.08f;
		float _lerp = Mathf.PingPong(Time.time, _duration) / _duration;

		if (m_colourChangeCollision)
		{
			foreach (SpriteRenderer spriteToBeModified in m_sprites)
			{
				spriteToBeModified.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(100, 100, 100, 255), _lerp);
			}
			if (Time.time > m_currentDelay)
			{
				foreach (SpriteRenderer spriteToBeModified in m_sprites)
				{
					spriteToBeModified.color = new Color32(255, 255, 255, 255);
				}
				m_colourChangeCollision = false;
			}
		}

	}
}
