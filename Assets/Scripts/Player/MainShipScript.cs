using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipScript : BaseShip {

    

	void Start ()
    {
		base.Initialization(10,15,50,gameObject.GetComponent<Animator>(), gameObject.GetComponent<Rigidbody2D>());
	}

}
