using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipScript : MonoBehaviour {

    Animator animatorShip;
    Rigidbody2D playerRigidBody;
    public float speed;
    public bool isOnCombat;
	public Transform[] Muzzles;
	protected float nextShotTime;
	public float msBetweenShots;

	void Start ()
    {
        animatorShip = gameObject.GetComponent<Animator>();
        playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        speed = 10f;
        isOnCombat = false;
	}

    // Update is called once per frame

    private void FixedUpdate()
    {
        float xAxis;
        float yAxis;

        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

		animatorShip.SetFloat("yAxis", yAxis);
		animatorShip.SetFloat("xAxis", xAxis);

		Vector2 movement = new Vector2(xAxis, yAxis);
        playerRigidBody.AddForce(movement * speed);

        
    }

	public void ChangeStateOfCombat(bool isCombat)
	{
		if(isCombat == true)
		{
			isOnCombat = false;
		}

		if(isCombat == false)
		{
			isOnCombat = true;
		}


		animatorShip.SetBool("isOnCombat", isOnCombat);
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
		}
	}

	void Update()
    {

		/*		for (int i = 0; i < 20; i++)
				{
					if (Input.GetKeyDown("joystick 1 button " + i.ToString()))
					{
						Debug.Log("joystick 1 button " + i.ToString());
					}
				}*/

		if (Input.GetButtonDown("Y button"))
		{
			ChangeStateOfCombat(isOnCombat);

		}

		if (isOnCombat == true)
		{
			float xAxisRightStick;
			float yAxisRightStick;

			xAxisRightStick = Input.GetAxis("JRightHorizontal");
			yAxisRightStick = Input.GetAxis("JRightVertical");

			animatorShip.SetFloat("xAxisRightStick", xAxisRightStick);
			animatorShip.SetFloat("yAxisRightStick", yAxisRightStick);

			if(Input.GetButton("Fire1"))
			{
				Shoot();
				
			}

			
		}


	}
}
