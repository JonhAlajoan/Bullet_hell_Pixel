using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyProjectile : MonoBehaviour {

    //--------------This is the class of the enemy base projectile to be used on all enemies--------

    /// <Attributes>
    /// 
    /// -m_damage: Damage that the bullet will cause to the player ship
    /// -m_vfxName: Name of the vfx that'll be spawned when the OnTriggerEnter2D runs
    ///
    /// </Attributes>
	
	protected float m_damage;
    protected string m_vfxName;
    /*
    //Initialization function that starts the damage of the bullet and the VFX that'll be used
	public void Initialize(float _damage, string _vfxName)
	{
        m_damage = _damage;
      //  m_vfxName = _vfxName;
	}*/


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<BaseShip>() != null)
		{
			BaseShip player = collision.gameObject.GetComponent<BaseShip>();
			player.TakeDamage(1);
		//	TrashMan.spawn(m_vfxName, transform.position, transform.rotation);
			transform.rotation = new Quaternion(0, 0, 0, 0);
            
            //Trashman.despawn but for the UBH bullets
			UbhObjectPool.Instance.ReleaseGameObject(transform.parent.gameObject);
		}
	}
	public abstract void Update();
}
