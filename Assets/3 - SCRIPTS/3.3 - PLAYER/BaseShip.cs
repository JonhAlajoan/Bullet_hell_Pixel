using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine;
using System;
using UnityEngine.UI;
public abstract class BaseShip : MonoBehaviour
{

	/*-----------------------------------------Base code for future ships of the game--------------------------------
	 * - The purpose of this code is to serve as the base for the ships, with all the main functions (death, shoot)
	 */

	//The main camera of the scene
	protected Camera m_mainCamera;

	//Animator of the ship that will be used to set the bool isOnCombat to change between combat mode or exploring mode
	protected Animator m_animatorShip;

	//The player rigidBody, that'll be used for Physics.
	protected Rigidbody2D playerRigidBody;

	//The player controller that'll be used for controlling
	CharacterController controllerShip;
	
	//This variable will be used to determine in conjuction with the managerOfScene if the player is on combat
	protected bool m_isOnCombat; 

	//The position of the muzzles for using as the initial position of the bullet
	public Transform[] Muzzles;


	protected float m_nextShotTime;  //The time of when the next shot will be available
	protected float m_msBetweenShots; // the milliseconds between shots
	protected float m_health; //Health of the ship
	protected float m_speed; //Movespeed of the ship
	protected float m_xAxis; //Horizontal Axis
	protected float m_yAxis; //Vertical Axis							
	protected float m_count; //Countdown to set the radar on/off
	protected float m_colourChangeDelay = 0.5f; //delay to change the color and get back
	protected float m_currentDelay = 0f; //Actual time + colourChangeDelay to serve as a parameter for the if

	protected bool m_colourChangeCollision = false; //Controller used as parameter on the function takeDamage permite change of color
	protected bool m_isOnRadar; //Bool that controls if the radar is on or off (If true, the player can't move)

	[SerializeField]
	protected SpriteRenderer[] m_spritesShip;

	
	protected string m_TypeOfController; //Type of controller that'll be used ("Controller") or ("Keyboard")
	string m_radarIsReady; //String that is changed in order for the UI.text to show "Ready!" Or "Not Ready!"

	//Direction where the mouse is pointed so it can turn in the animator
    Vector2 directionMouse;

	//Manager of the scene
	protected managerOfScene m_manager;

	//Constructor of the cameraController Class
	protected CameraController m_cameraController;

	//Particles of the radar
	protected ParticleSystem m_particleRadar;
	
	//Text components
	public Text m_radarText;
	public Text m_healthText;

    [SerializeField]
    protected ShipRadar m_shipRadar;

    public GameObject canvasRestart;

    //This Initialization function, sets the variables for the parameters, normally this will be used on the start function

    private void OnEnable()
    {
        canvasRestart.SetActive(false);
    }

    public void Initialization(float _hp, float _speedOfShip, float _MsBetweenShots, Animator _animatorOfShip, Rigidbody2D _rigidBody)
	{
		m_radarIsReady = "Not ready";
		m_health = _hp;
		m_speed = _speedOfShip;
		m_msBetweenShots = _MsBetweenShots;
		m_animatorShip = _animatorOfShip;
		playerRigidBody = _rigidBody;

		//Getting the main camera of the scene
		m_mainCamera = Camera.main;

		//Finding the objects that have a cameraController and the ManagerOfScene
		m_manager = FindObjectOfType<managerOfScene>();
		m_cameraController = m_manager.GetComponent<CameraController>();
        m_radarText = m_manager.GetComponent<UIManager>().radarText;
        m_healthText = m_manager.GetComponent<UIManager>().hpText;
        m_particleRadar = transform.FindInChildren("RADAR").GetComponent<ParticleSystem>();
		m_isOnRadar = false;
	}

	/*This function will get the mouse position vertically and horizontally then normalize it so the variable directionMouse 
	  can be used as a float for the animatorShip as a parameter for rotating the ship*/
    public void faceMouse()
    {
		//Gets the mouse position as a point
        Vector3 _mousePosition = Input.mousePosition;
         _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

		//The direction that the ship is, based on the mouse and object position
        Vector2 _direction = new Vector2(
            _mousePosition.x - transform.position.x,
            _mousePosition.y - transform.position.y
            );
		
		//The direction being normalized (values being thrown as 1)
        directionMouse = _direction.normalized;
    }

	//This function makes the animator of ship assume stance of exploration or combat based on the isCombat variable
	private void AnimatorStateOfCombat(bool isCombat)
	{
		m_animatorShip.SetBool("isOnCombat", m_isOnCombat);
	}

	//This method makes the player shoot based if the current time passed is higher than the nextShotTime
	public virtual void Shoot()
	{
		//If actual time is higher than the nextShotTime
		if (Time.time > m_nextShotTime)
		{
			//Spawn the bullets from the muzzles position
			for (int i = 0; i < Muzzles.Length; i++)
			{
				TrashMan.spawn("BulletMainShip", Muzzles[i].position, Muzzles[i].rotation);
			}

			//Next shot ready will be actual time + milisseconds between shots divided by 1000
			m_nextShotTime = Time.time + m_msBetweenShots / 1000;

			//This is the camera shaker being used
			m_cameraController.Shaker(1f,1f,0.3f);
          
		}
	}

	//FixedUpdate focused on the movement of the player
    private void FixedUpdate()
	{

		//getting the inputs
		m_xAxis = Input.GetAxis("Horizontal");
		m_yAxis = Input.GetAxis("Vertical");

		//Setting the animator parameters
		m_animatorShip.SetFloat("yAxis", m_yAxis);
		m_animatorShip.SetFloat("xAxis", m_xAxis);

		//if the radar isn't on, ship can't move
		if (m_isOnRadar == false)
		{
			Vector2 _movement2d = new Vector2(m_xAxis, m_yAxis);
			transform.Translate(_movement2d * (m_speed * Time.deltaTime), Space.World);
		}
	}
    public void playRadar()
    {

    }
    
    public void stopRadar()
    {

    }

	void Update()
	{

		m_count += 1 * Time.deltaTime;

		//Getting the state of the combat from the manager
		m_isOnCombat = m_manager.stateOfCombat;

		//Getting the type of controller from the manager
		m_TypeOfController = m_manager.typeOfController;

		//Calling the AnimatorStateOfCombat Method
		AnimatorStateOfCombat(m_isOnCombat);
		
		//if count > 8, the player can press a button to call the radar
        if(Input.GetKey(KeyCode.LeftControl) && m_count < 8)
        {
            m_count = 0;
        }

		if (m_count >= 8)
		{
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!m_particleRadar.isPlaying)
                    m_particleRadar.Play();
                m_isOnRadar = true;
                m_shipRadar.SpawnRadarDots();
                m_radarIsReady = "Not ready!";
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                m_isOnRadar = false;
                m_particleRadar.Stop();
                m_count = 0;
                m_radarIsReady = "Not ready!";
            }
            else
                m_radarIsReady = "Ready!";
			
		}
        else
        {
            m_radarIsReady = "Not Ready";
        }

		//Control that'll be called if the typeOfController is a joystick
		if (m_isOnCombat == true && m_TypeOfController == "Controller")
		{
			//Getting the Axis from the right stick of the controller that'll be used as que guide for shooting
			float _xAxisRightStick;
			float _yAxisRightStick;
			_xAxisRightStick = Input.GetAxis("JRightHorizontal");
			_yAxisRightStick = Input.GetAxis("JRightVertical");

			//Setting the animator for the right stick
			m_animatorShip.SetFloat("xAxisRightStick", _xAxisRightStick);
			m_animatorShip.SetFloat("yAxisRightStick", _yAxisRightStick);
			
			if (_xAxisRightStick == 1 || _xAxisRightStick == -1 || _yAxisRightStick == 1 || _yAxisRightStick == -1)
			{
				Shoot();
			}
		}

		//Control that'll be called if the TypeOfController is a Keyboard
        if(m_isOnCombat == true && m_TypeOfController == "Keyboard")
        {
		
            faceMouse();

			m_animatorShip.SetFloat("xAxisRightStick", directionMouse.x);
			m_animatorShip.SetFloat("yAxisRightStick", directionMouse.y);
          
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }

			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				m_speed = m_speed / 4;
			}

			if (Input.GetKeyUp(KeyCode.LeftShift))
			{
				m_speed = m_speed * 4;
			}
		}

		if (m_health <= 0)
		{
			die();
		}
		changeColor();

		

		//Changing radar and health disponibility
		m_healthText.text = "Health: " + m_health.ToString();
		m_radarText.text = "Radar: " + m_radarIsReady;
		
	}

	public void changeColor()
	{
		//Duration of the lerp
		float _duration = 0.08f;
		float _lerp = Mathf.PingPong(Time.time, _duration) / _duration;

		if (m_colourChangeCollision)
		{
			foreach (SpriteRenderer spriteToBeModified in m_spritesShip)
			{
				spriteToBeModified.color = Color32.Lerp(new Color32(255, 255, 255, 255), new Color32(100, 100, 100, 255), _lerp);
			}
			if (Time.time > m_currentDelay)
			{
				foreach (SpriteRenderer spriteToBeModified in m_spritesShip)
				{
					spriteToBeModified.color = new Color32(255, 255, 255, 255);
				}
				m_colourChangeCollision = false;
			}
		}

	}

	//Function makes the ship flash between white and black and take damage
	public void TakeDamage(float _damageTaken)
	{
			
			m_colourChangeCollision = true;
			m_currentDelay = Time.time + m_colourChangeDelay;
			m_health -= _damageTaken;

	}

	public void die()
	{
		m_manager.changeStateOfCombat(false);
        canvasRestart.SetActive(true);
		TrashMan.despawn(gameObject);
	}


}
