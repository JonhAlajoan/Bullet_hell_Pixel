using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(FixedTime))]
public class managerOfScene : MonoBehaviour{

	public bool stateOfCombat;
	public string typeOfController;

	Camera mainCamera;

	protected Animator animatorShip;
	protected Animator animatorMainCamera;

	public GameObject VirtualCamera;

	public int frameCount;


	private void Start()
	{
		
		mainCamera = Camera.main;
		animatorMainCamera = mainCamera.GetComponent<Animator>();
		animatorShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		frameCount = FixedTime.fixedFrameCount;

	}


	public void changeStateOfCombat(bool _stateOfCombat)
	{
		stateOfCombat = _stateOfCombat;

		if (stateOfCombat == true)
		{
			VirtualCamera.SetActive(false);
		}

		if (stateOfCombat == false)
		{
			VirtualCamera.SetActive(true);
		}

		animatorShip.SetBool("isOnCombat", stateOfCombat);
		//animatorMainCamera.SetBool("isOnCombat", stateOfCombat);
	}


	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
		{
			typeOfController = "Keyboard";
		}
		if(Input.GetAxis("JRightHorizontal") > 1)
		{
			typeOfController = "Joystick";
		}
		

		
		


		/*if (Input.GetButtonDown("Jump"))
		{
			TypeOfController = "Keyboard";
			ChangeStateOfCombat(isOnCombat, actualLookAheadTime);
		}

		if (Input.GetButtonDown("Y button"))
		{
			Shoot();
		}*/
	}
}
