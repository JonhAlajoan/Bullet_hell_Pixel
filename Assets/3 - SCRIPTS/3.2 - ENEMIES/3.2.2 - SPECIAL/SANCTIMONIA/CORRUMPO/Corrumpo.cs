using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrumpo : BaseEnemy {

	// Use this for initialization
	public override void Start () {
		base.Start();
		base.Initialization(20, 30, 100, gameObject.GetComponent<Animator>());
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
	}

	public override void Shoot(States _actualState)
	{
	}
}
