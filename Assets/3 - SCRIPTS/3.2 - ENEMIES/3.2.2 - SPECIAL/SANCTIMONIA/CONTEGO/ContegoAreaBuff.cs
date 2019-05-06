using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContegoAreaBuff : MonoBehaviour {

	float count;
	protected Collider2D m_colliderThis;
	private void Start()
	{
		if (!m_colliderThis)
			m_colliderThis = GetComponent<Collider2D>();
		count = 0;
	}


	private void Update()
	{
		
		count += 1 * Time.deltaTime;

		if(count >= 1 && count < 3)
		{
			m_colliderThis.enabled = true;
		}
		
		if (count >= 4)
		{
			m_colliderThis.enabled = false;
			count = 0;
		}
	}


	public void OnTriggerEnter2D(Collider2D other)
	{
		BaseEnemy enemy = other.GetComponent<BaseEnemy>();
		ParticleSystem _particle = other.GetComponent<ParticleSystem>();

		if (enemy != null)
		{

			if (!other.transform.Find("CONTEGO_BUFF") && other.name != "CORRUMPO_SHIELD")
			{
				Debug.Log("Partícula Spawnada em: " + other.gameObject.name);
				GameObject _buffVFX = TrashMan.spawn("CONTEGO_BUFF", enemy.transform.position, enemy.transform.rotation);
				_buffVFX.transform.parent = enemy.transform;
				_buffVFX.transform.localPosition = new Vector3(0, 0, 0);
			}

			enemy.ContegoHeal(20);
		
		}
	}
}



