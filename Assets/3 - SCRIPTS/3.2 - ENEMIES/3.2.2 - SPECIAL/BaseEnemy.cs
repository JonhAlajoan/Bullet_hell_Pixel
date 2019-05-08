using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    //-------------This class serve as the base for the all enemies classes------------

	protected float m_nextShotTime; //this will be subtracted from the time to decide the time of the next shot
	protected float m_msBetweenShots; //The milisseconds between shots
	protected float m_startingHealth; //Starting health of the enemy

	[SerializeField]
	protected float m_health; //Actual health of the enemy

    protected float m_count = 0; //If any enemy need a counter on a update override
    protected float m_colourChangeDelay = 0.5f; //delay to change the color and get back
    protected float m_currentDelay = 0f; //Actual time + colourChangeDelay to serve as a parameter for the if

    protected Animator m_animator; //Animator of the enemy

	[SerializeField]
	protected SpriteRenderer[] m_sprites; //Sprites of the enemies that'll be used to change color

    protected bool m_colourChangeCollision = false; //Controller used as parameter on the function takeDamage permite change of color

    protected managerOfScene m_manager; //Manager of the scene

	[SerializeField]
	protected TextMesh m_text; //Text mesh for debug purposes

	public virtual void Start()
	{
		if(m_manager == null)
			m_manager = FindObjectOfType<managerOfScene>();
	}

    //This Initialization function, sets the variables for the parameters, normally this will be used on the start function 
    public void Initialization(float _hp, float _MsBetweenShots, Animator _animatorOfEnemy)
	{
		m_startingHealth = _hp;
		m_health = _hp;
		m_msBetweenShots = _MsBetweenShots;
		m_animator = _animatorOfEnemy;
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

    //Function that makes the enemy be healed by the contego
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

    //Reset the HP and add it to the starting health whenever called (normally when aggro is lost)
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
