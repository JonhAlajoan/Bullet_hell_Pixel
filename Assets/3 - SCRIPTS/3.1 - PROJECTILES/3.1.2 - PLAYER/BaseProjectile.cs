using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    //------------------------Base projectile for using on ships bullets--------------------

    /// <summary>
    /// 
    /// - m_speed: Speed of the bullet (the default is 30f)
    /// - m_damage: Damage that the bullet will cause to the enemy
    /// - m_lifetime: lifetime of the bullet
    /// - m_count: control of the timeCount;
    /// 
    /// </summary>
	protected float m_speed = 30f;
	protected float m_damage = 1f;
	protected float m_lifetime = 2;
	protected float m_count;
	
    //No idea why, but onEnable and awake need to have m_count on zero so the invulnerable bullet doesn't crash
	void OnEnabled()
	{
		m_count = 0;

	}
    //No idea why, but onEnable and awake need to have m_count on zero so the invulnerable bullet doesn't crash
    private void Awake()
	{
		m_count = 0;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        //if the detected collider is an enemy projectile...
		if (collision.gameObject.GetComponentInChildren<BaseEnemyProjectile>() != null)
		{
		    
            //if the projectile detected is invulnerable..
			if(collision.gameObject.tag == "Invulnerable")
			{
				m_count = 0;
                //TrashMan.spawn("VFX_HIT_INVULNERABLE", transform.position, transform.rotation);
                //AudioPlays();
            }

			else
			{
				m_count = 0;
				TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
				UbhObjectPool.Instance.ReleaseGameObject(collision.transform.parent.gameObject);
			}

			TrashMan.despawn(gameObject);	
			
		}

        //if the detected collider is the boss
		if (collision.gameObject.GetComponent<BaseBoss>() != null)
		{
			BaseBoss boss = collision.gameObject.GetComponent<BaseBoss>();
			boss.TakeDamage(m_damage);
			TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
			m_count = 0;
			TrashMan.despawn(gameObject);
		}

        //if the detected collider is the enemy
        if (collision.gameObject.GetComponent<BaseEnemy>() != null)
		{
			BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
			enemy.TakeDamage(m_damage);
			TrashMan.spawn("VFX_HIT_PLAYER", transform.position, transform.rotation);
			m_count = 0;
			TrashMan.despawn(gameObject);
		}


	}

	public void SetSpeed(float newSpeed)
	{
		m_speed = newSpeed;
	}

	void Update()
	{
		m_count += 1 * Time.deltaTime;

		if (m_count >= m_lifetime)
		{
			m_count = 0;
			transform.rotation = new Quaternion(0, 0, 0, 0);
			TrashMan.despawn(gameObject);
		}

		float moveDistance = m_speed * Time.deltaTime;

		transform.Translate(Vector2.up* moveDistance);
	}

}

