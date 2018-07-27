using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Projectile1 : BaseEnemyProjectile {

	// Use this for initialization
	void OnEnable ()
	{
		base.Initialize(3, 1, 3, 0, 10);
		transform.up = homingTarget.position - transform.position;
	}

	public override void Update()
	{
			
			count += 1 * Time.deltaTime;


			homingTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

			if(count > 1)
			{
				speed += accelSpeed * Time.deltaTime;
			}

			if (count >= 5)
			{
				transform.rotation = new Quaternion(0, 0, 0, 0);
				TrashMan.despawn(gameObject);
				count = 0;
				accelSpeed = 0;
			}

			float moveDistance = speed * Time.deltaTime;
			transform.Translate(Vector2.up * moveDistance);
		
	}


}
