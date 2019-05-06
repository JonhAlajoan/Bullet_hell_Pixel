using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LANCER : BaseEnemy {

	protected Transform m_playerPos;
	public bool isAttached;
	
	public override void Shoot(string _typeOfBullets)
	{
		throw new System.NotImplementedException();
	}



	// Use this for initialization
	public override void Start ()
	{
		base.Initialization(30, 30, 0, GetComponent<Animator>());
		m_playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		m_count = 0;
		isAttached = false;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update();
		
	}

	IEnumerator despawn()
	{
		yield return new WaitForSeconds(5f);
		isAttached = false;
		TrashMan.despawn(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<BaseShip>() != null)
		{
			BaseShip _enemy = collision.GetComponent<BaseShip>();
			_enemy.TakeDamage(2f);
			StartCoroutine(despawn());
			isAttached = true;
		}
		else
			return;
	}
}
