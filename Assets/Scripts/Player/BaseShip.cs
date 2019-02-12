using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine;
public abstract class BaseShip : MonoBehaviour {

	protected Camera mainCamera;
	protected PostProcessingProfile postProcessingProfile;

	protected Animator animatorShip;
	protected Animator animatorMainCamera;

	protected Rigidbody2D playerRigidBody;
	CharacterController controllerShip;
	
	protected bool isOnCombat;
	public Transform[] Muzzles;

	protected float nextShotTime;
	protected float msBetweenShots;
	protected float health;
	protected float speed;
	protected float xAxis;
	protected float yAxis;

    Transform ObjectLookAtMouse;
    
    CinemachineVirtualCamera vCam;

    protected string TypeOfController;

    float heading;
    Vector2 directionMouse;

	Transform playerPos;
	


	public void Initialization(float hp, float speedOfShip, float MsBetweenShots, Animator animatorOfShip, Rigidbody2D rigidBody)
	{
		this.health = hp;
		this.speed = speedOfShip;
		this.msBetweenShots = MsBetweenShots;
		this.animatorShip = animatorOfShip;
		this.playerRigidBody = rigidBody;


		mainCamera = Camera.main;
		vCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
		animatorMainCamera = mainCamera.GetComponent<Animator>();
        ObjectLookAtMouse = GameObject.Find("ObjectLookAtMouse").GetComponent<Transform>();
        
	}

    public void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );
        directionMouse = direction.normalized;
    }

	public void CameraClamping()
	{
		/*Vector3 cameraEdges = Camera.main.WorldToViewportPoint(transform.position);

		Vector3 posInitial =  new Vector3(transform.position.x,transform.position.y,0);

		Vector3 posFinal = new Vector3(cameraEdges.x, cameraEdges.y, 0);

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, posFinal.x, posInitial.x), Mathf.Clamp(transform.position.y, posInitial.y, posFinal.y), 0);
		*/
		for (int i = 0; i < 20; i++)
		{
			if (Input.GetKeyDown("joystick 1 button " + i))
			{
				print("joystick 1 button " + i);
			}
		}
	}

	private void ChangeStateOfCombat(bool isCombat, float actualTime)
	{
		if (isCombat == true)
		{
			isOnCombat = false;

            vCam.m_Lens.OrthographicSize = 8f;
            vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.113f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.075f;
		}

		if (isCombat == false)
		{
			isOnCombat = true;
            vCam.m_Lens.OrthographicSize = 13f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 1f;
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 1f;
		}


		animatorShip.SetBool("isOnCombat", isOnCombat);
		animatorMainCamera.SetBool("isOnCombat", isOnCombat);
	}

	public void Shoot()
	{
		if (Time.time > nextShotTime)
		{
			
			for (int i = 0; i < Muzzles.Length; i++)
			{
				TrashMan.spawn("BulletMainShip", Muzzles[i].position, Muzzles[i].rotation);
			}
			nextShotTime = Time.time + msBetweenShots / 1000;
			Shaker(1f,1f,0.1f);

		}
	}

	public void Shaker(float amplitude, float frequency, float duration)
	{
		StopAllCoroutines();
		StartCoroutine(CameraShaker(amplitude, frequency, 0.5f));
	}

	public IEnumerator CameraShaker(float amplitude, float frequency, float duration)
	{
		vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
		vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
		yield return new WaitForSeconds(duration);
		vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
		vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;

	}

    private void FixedUpdate()
	{
		xAxis = Input.GetAxis("Horizontal");
		yAxis = Input.GetAxis("Vertical");

		animatorShip.SetFloat("yAxis", yAxis);
		animatorShip.SetFloat("xAxis", xAxis);
		
		Vector2 movement2d = new Vector2(xAxis, yAxis);

		playerRigidBody.AddForce(movement2d * speed);

		if(isOnCombat == true)
		{
			CameraClamping();
		}
			
		
	}

	void Update()
	{
		float actualLookAheadTime = vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime;

        if (Input.GetButtonDown("Jump"))
        {
            TypeOfController = "Keyboard";
            ChangeStateOfCombat(isOnCombat, actualLookAheadTime);
        }

		if (Input.GetButtonDown("Y button"))
		{
			Shoot();
		}

		if (isOnCombat == true && TypeOfController == "Controller")
		{
			float xAxisRightStick;
			float yAxisRightStick;

			xAxisRightStick = Input.GetAxis("JRightHorizontal");
			yAxisRightStick = Input.GetAxis("JRightVertical");

			animatorShip.SetFloat("xAxisRightStick", xAxisRightStick);
			animatorShip.SetFloat("yAxisRightStick", yAxisRightStick);
			
			if (xAxisRightStick == 1 || xAxisRightStick == -1 || yAxisRightStick == 1 || yAxisRightStick == -1)
			{
				Shoot();
			}
		}

        if(isOnCombat == true && TypeOfController == "Keyboard")
        {
            faceMouse();
          
            animatorShip.SetFloat("xAxisRightStick", directionMouse.x);
            animatorShip.SetFloat("yAxisRightStick", directionMouse.y);
          
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }


    }
}
