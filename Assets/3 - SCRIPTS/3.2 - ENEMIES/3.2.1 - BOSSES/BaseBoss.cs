using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseBoss : MonoBehaviour
{

	public enum States{SPAWNING,PHASE1,PHASE2,PHASE3,PHASE4}

	protected float m_nextShotTime;
	protected float m_msBetweenShots;
	protected float m_startingHealth;
	protected float m_health;

	protected Animator m_animatorBoss;
	protected Transform m_targetPlayer;

	[SerializeField]
	protected SpriteRenderer[] m_spritesBoss;

	public Transform[] Muzzles;

	protected float m_count = 0;

	protected float m_colourChangeDelay = 0.5f;
	protected float m_currentDelay = 0f;
	protected bool m_colourChangeCollision = false;

	protected managerOfScene m_manager;

	public bool isInvulnerable;

	public GameObject OuterHealthBar;
	public Image healthBarImage;

	protected States m_stateBaseClass;

	public virtual void Start()
	{
		m_stateBaseClass = States.SPAWNING;
		m_manager = FindObjectOfType<managerOfScene>();
	}

	private void Awake()
	{
		isInvulnerable = true;
		OuterHealthBar.SetActive(false);
		healthBarImage.fillAmount = 0;
	}

	public void Initialization(float _hp, float _speedOfShip, float _MsBetweenShots, Animator _animatorOfShip)
	{
		m_startingHealth = _hp;
		m_health = _hp;	
		m_msBetweenShots = _MsBetweenShots;
		m_animatorBoss = _animatorOfShip;
	}

	public void TakeDamage(float _damageTaken)
	{
		if(isInvulnerable == false)
		{
			m_colourChangeCollision = true;
			m_currentDelay = Time.time + m_colourChangeDelay;
			m_health -= _damageTaken;
		}

	}
	//Lembrar de setar no animator os fuckoing parâmetros.
	public void ChangePhase(States _nameOfPhase)
	{
		m_stateBaseClass = _nameOfPhase;

		switch(m_stateBaseClass)
		{
			case States.PHASE1:
				m_animatorBoss.SetInteger("PHASE", 1);
			break;

			case States.PHASE2:
				m_animatorBoss.SetInteger("PHASE", 2);
			break;

			case States.PHASE3:
				m_animatorBoss.SetInteger("PHASE", 3);
			break;

			case States.PHASE4:
				m_animatorBoss.SetInteger("PHASE", 4);
			break;
		}
		
	}

	public void die()
	{
		m_manager.changeStateOfCombat(false);
		healthBarImage.fillAmount = 0;
		OuterHealthBar.SetActive(false);
		TrashMan.despawn(gameObject);
	}

	public abstract void Shoot(States _actualState);

	public virtual void Update()
	{
		if (m_health <= 0)
		{
			die();
		}
		changeColor();
		HpManager();

	}

	public void HpManager()
	{
		if (isInvulnerable == true && m_manager.stateOfCombat == true)
		{
			
			OuterHealthBar.SetActive(true);
			healthBarImage.fillAmount += 0.3f * Time.deltaTime;

			OuterHealthBar.SetActive(true);

			if (healthBarImage.fillAmount == 1)
			{
				isInvulnerable = false;
				m_animatorBoss.SetTrigger("FIGHT_START");
				m_stateBaseClass = States.PHASE1;
				ChangePhase(m_stateBaseClass);
			}
			
		}
		if (isInvulnerable == false)
		{			
			healthBarImage.fillAmount = HealthBarModifier(m_health, m_startingHealth);
		}

	}

	public float HealthBarModifier(float _currentHealth, float _maxHealth)
	{
		return (_currentHealth * 1) / _maxHealth;
	}

	public void changeColor()
	{
		float _duration = 0.08f;
		float _lerp = Mathf.PingPong(Time.time, _duration) / _duration;

		if (m_colourChangeCollision)
		{
			foreach (SpriteRenderer spriteToBeModified in m_spritesBoss)
			{
				spriteToBeModified.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(100, 100, 100, 255), _lerp);
			}
			if (Time.time > m_currentDelay)
			{
				foreach (SpriteRenderer spriteToBeModified in m_spritesBoss)
				{
					spriteToBeModified.color = new Color32(255, 255, 255, 255);
				}
				m_colourChangeCollision = false;
			}
		}
				
	}



}
