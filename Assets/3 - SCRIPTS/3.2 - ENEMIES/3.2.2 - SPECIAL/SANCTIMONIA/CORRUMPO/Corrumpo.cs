using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrumpo : BaseEnemy {

	// Use this for initialization
	public override void Start () {
		base.Start();
		base.Initialization(500, 30, 100, gameObject.GetComponent<Animator>());
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
	}

	public override void Shoot(string _typeOfShot)
	{
		TrashMan.spawn(_typeOfShot, transform.position, transform.rotation);
		Debug.Log("Shooting corrumpo: " + _typeOfShot);
	}
}
