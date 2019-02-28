using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseBoss : MonoBehaviour
{

	protected float nextShotTime;
	protected float msBetweenShots;
	public float startingHealth;
	public float health;
	protected Animator animatorBoss;
	public Transform targetPlayer;

	[SerializeField]
	public SpriteRenderer[] spritesBoss;

	public Transform[] Muzzles;

	float count = 0;

	public float colourChangeDelay = 0.5f;
	float currentDelay = 0f;
	bool colourChangeCollision = false;

	public managerOfScene manager;

	public bool isInvulnerable;


	public GameObject OuterHealthBar;
	public Image healthBarImage;

	private void Start()
	{
		manager = FindObjectOfType<managerOfScene>();
		
	}

	private void Awake()
	{
		isInvulnerable = true;
		OuterHealthBar.SetActive(false);
		healthBarImage.fillAmount = 0;

	}

	public void Initialization(float _hp, float _speedOfShip, float _MsBetweenShots, Animator _animatorOfShip)
	{
		this.startingHealth = _hp;
		this.health = _hp;	
		this.msBetweenShots = _MsBetweenShots;
		this.animatorBoss = _animatorOfShip;
	}

	public void TakeDamage(float damageTaken)
	{
		if(isInvulnerable == false)
		{
			colourChangeCollision = true;
			currentDelay = Time.time + colourChangeDelay;
			health -= damageTaken;

		}

	}

	public void ChangePhase(string NameOfPhase)
	{

	}

	public void die()
	{
		manager.changeStateOfCombat(false);
		healthBarImage.fillAmount = 0;
		OuterHealthBar.SetActive(false);
		TrashMan.despawn(gameObject);
	}

	public abstract void Shoot();

	public void Update()
	{
		if (health <= 0)
		{
			die();
		}

		Shoot();
		changeColor();

		HpManager();

		
		
	}

	public void HpManager()
	{
		if (isInvulnerable == true && manager.stateOfCombat == true)
		{
			//cÓDIGO PRA IR ENCHENDO
			OuterHealthBar.SetActive(true);
			healthBarImage.fillAmount += 0.3f * Time.deltaTime;
			Debug.Log("incrementando");

			OuterHealthBar.SetActive(true);

			if (healthBarImage.fillAmount == 1)
			{
				isInvulnerable = false;
			}
			/*
			if(currentBossHealth <= initialBossHealth)
			{
				currentBossHealth += 100f;
			}
			if(currentBossHealth >= initialBossHealth)
			{
				isInvulnerable = false;
			}
			*/
		}
		if (isInvulnerable == false)
		{
			animatorBoss.SetTrigger("FIGHT_START");
			healthBarImage.fillAmount = HealthBarModifier(health, startingHealth);
		}

	}

	private float HealthBarModifier(float currentHealth, float maxHealth)
	{
		return (currentHealth * 1) / maxHealth;
	}

	public void changeColor()
	{
		float duration = 0.08f;
		float lerp = Mathf.PingPong(Time.time, duration) / duration;

		if (colourChangeCollision)
		{
			foreach (SpriteRenderer spriteToBeModified in spritesBoss)
			{
				spriteToBeModified.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(100, 100, 100, 255), lerp);
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
