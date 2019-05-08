using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrumpo : BaseEnemy {

	
	public override void Start ()
    {
		base.Start();
		Initialization(500, 100, gameObject.GetComponent<Animator>());
	}
	
	public override void Update ()
    {
        base.Update();
	}

	public override void Shoot(string _typeOfShot)
	{
		TrashMan.spawn(_typeOfShot, transform.position, transform.rotation);
		Debug.Log("Shooting corrumpo: " + _typeOfShot);
	}
}
