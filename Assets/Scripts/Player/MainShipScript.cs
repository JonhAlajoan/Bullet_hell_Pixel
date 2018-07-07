using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipScript : MonoBehaviour {

    Animator animatorShip;
    Rigidbody2D playerRigidBody;
    public float speed;
    public bool isOnCombat;

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
        float xAxisPhysic;
        float yAxisPhysic;

        xAxisPhysic = Input.GetAxis("Horizontal");
        yAxisPhysic = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(xAxisPhysic, yAxisPhysic);
        playerRigidBody.AddForce(movement * speed);

        
    }

    void Update()
    {
        float xAxis;
        float yAxis;

        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        animatorShip.SetFloat("yAxis", yAxis);
        animatorShip.SetFloat("xAxis", xAxis);


    }
}
