using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
	protected float m_nextShotTime;
	protected float m_msBetweenShots;
	protected float m_startingHealth;
	[SerializeField]
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

	[SerializeField]
	protected TextMesh m_text;

	public virtual void Start()
	{
		if(m_manager == null)
			m_manager = FindObjectOfType<managerOfScene>();
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


	public void die()
	{
		TrashMan.despawn(gameObject);
	}

	public abstract void Shoot(string _typeOfBullets);

	public virtual void Update()
	{
		if (m_health <= 0)
		{
			die();
		}
		changeColor();
		m_text.text = m_health.ToString();
	}

	public void ContegoHeal(float _hpRecovered)
	{
		if(gameObject.name == "CONTEGO_SHIELD")
		{
			m_health += _hpRecovered;
		}

		if (m_health + _hpRecovered > m_startingHealth)
			m_health = m_startingHealth;
		else
			m_health += _hpRecovered;
			
	}

	public void resetHp()
	{
		m_health += m_startingHealth;
		m_startingHealth = m_health;
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
