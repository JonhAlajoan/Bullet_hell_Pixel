using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class managerOfScene : MonoBehaviour {

	public bool stateOfCombat;
	public string typeOfController;

	Camera mainCamera;

	protected Animator animatorShip;
	protected Animator animatorMainCamera;

	CinemachineVirtualCamera vCam;

	private void Start()
	{
		vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
		mainCamera = Camera.main;
		animatorMainCamera = mainCamera.GetComponent<Animator>();
		animatorShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
	}


	public void changeStateOfCombat(bool _stateOfCombat)
	{
		stateOfCombat = _stateOfCombat;

		if (stateOfCombat == true)
		{
			//stateOfCombat = false;

			vCam.m_Lens.OrthographicSize = 13f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 1f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 1f;

			
		}

		if (stateOfCombat == false)
		{
			vCam.m_Lens.OrthographicSize = 8f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.113f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.075f;

		}

		animatorShip.SetBool("isOnCombat", stateOfCombat);
		animatorMainCamera.SetBool("isOnCombat", stateOfCombat);
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
