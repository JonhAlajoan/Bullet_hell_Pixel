using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

	float speed = 30f;
	float damage = 1f;
	public bool firstHit = true;
	float lifetime = 3;
	float count;
	


	void OnEnabled()
	{
		count = 0;

	}
	private void Awake()
	{
		count = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.gameObject.GetComponentInChildren<BaseEnemyProjectile>() != null)
		{
		
			if(collision.gameObject.tag == "Invulnerable")
			{
				count = 0;
			}

			else
			{
				count = 0;
				TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
				UbhObjectPool.Instance.ReleaseGameObject(collision.transform.parent.gameObject);
			}
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

		if (collision.gameObject.GetComponent<BaseEnemy>() != null)
		{
			BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
			enemy.TakeDamage(damage);
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

		if (count >= 2)
		{
			count = 0;
			transform.rotation = new Quaternion(0, 0, 0, 0);
			TrashMan.despawn(gameObject);
		}
		float moveDistance = speed * Time.deltaTime;
		transform.Translate(Vector2.up* moveDistance);
	}

}

