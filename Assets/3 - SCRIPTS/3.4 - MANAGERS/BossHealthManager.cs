using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BossHealthManager : MonoBehaviour {

	public float initialBossHealth;
	public float currentBossHealth;
	public GameObject currentBossObject;

	Image healthBar;

	public float fillAmount;

	private void Start()
	{
		healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();

		try
		{
			currentBossObject = GameObject.FindGameObjectWithTag("Boss");
			initialBossHealth = currentBossObject.GetComponent<BaseBoss>().startingHealth;
		}
		catch (NullReferenceException)
		{
			Debug.Log("Boss não encontrado!");
			currentBossObject = GameObject.FindGameObjectWithTag("Boss");
			initialBossHealth = currentBossObject.GetComponent<BaseBoss>().startingHealth;
		}

		

	}

	private void Update()
	{


		if (!currentBossObject)
		{
			Debug.Log("Boss não encontrado!");
			currentBossObject = GameObject.FindGameObjectWithTag("Boss");
			initialBossHealth = currentBossObject.GetComponent<BaseBoss>().startingHealth; ;
		}

		initialBossHealth = currentBossObject.GetComponent<BaseBoss>().startingHealth;
		currentBossHealth = currentBossObject.GetComponent<BaseBoss>().health;

		healthBar.fillAmount = HealthBarModifier(currentBossHealth, initialBossHealth);



	}

	private float HealthBarModifier(float currentHealth, float maxHealth)
	{
		

		return (currentHealth * 1) / maxHealth;
	}
}
