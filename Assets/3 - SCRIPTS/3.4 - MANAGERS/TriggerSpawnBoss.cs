using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerSpawnBoss : MonoBehaviour {

	managerOfScene manager;
	bool isManagerFound;

	private void Start()
	{
		try
		{
			manager = GameObject.FindGameObjectWithTag("ManagerScene").GetComponent<managerOfScene>();
			isManagerFound = true;
		}
		catch(NullReferenceException)
		{
			isManagerFound = false;
		}
		
	}

	private void Update()
	{
		if(isManagerFound == false)
		{
			manager = GameObject.FindGameObjectWithTag("ManagerScene").GetComponent<managerOfScene>();
			isManagerFound = true;
		}
	
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<BaseShip>() != null)
		{
			manager.changeStateOfCombat(true);
			Debug.Log("State of combat: " + manager.stateOfCombat);
		}

		if (collision.GetComponent<MainShipScript>() != null)
		{
			manager.changeStateOfCombat(true);
			Debug.Log("State of combat: " + manager.stateOfCombat);
		}
	}
}
