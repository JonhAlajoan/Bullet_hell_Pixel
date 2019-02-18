using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

	float speed = 30;
	float damage = 5f;
	public bool firstHit = true;
	float lifetime = 3;
	float skinWidth = .1f;
	float count;
	


	void OnEnabled()
	{
		count = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.GetComponent<BaseEnemyProjectile>() != null)
		{
			TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
			count = 0;
			TrashMan.despawn(collision.gameObject);
			TrashMan.despawn(gameObject);
		}

		if (collision.gameObject.GetComponent<BaseBoss>() != null)
		{
			BaseBoss boss = collision.gameObject.GetComponent<BaseBoss>();
			boss.TakeDamage(damage);
			TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
			count = 0;
			TrashMan.despawn(gameObject);
		}
		
		
	}

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	void Update()
	{
		count += 1 * Time.deltaTime;

		if (count >= 5)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
			TrashMan.despawn(gameObject);
			count = 0;
		}
		float moveDistance = speed * Time.deltaTime;
		transform.Translate(Vector2.up* moveDistance);
	}

}

