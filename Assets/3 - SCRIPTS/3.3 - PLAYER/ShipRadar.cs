using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRadar : MonoBehaviour {

	[SerializeField]
	protected Transform m_enemyFlockPosition;
	[SerializeField]
	protected Transform m_playerPosition;

	[SerializeField]
	protected Transform m_radarRotation;

	protected float m_count;

	float angle;

	private void Start()
	{
		m_count = 0;

		if (!m_enemyFlockPosition)
			m_enemyFlockPosition = FindObjectOfType<BaseEnemy>().transform;

		if (!m_playerPosition)
			m_playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	private void Update()
	{
		if (!m_enemyFlockPosition)
			m_enemyFlockPosition = FindObjectOfType<BaseEnemy>().transform;

		transform.right = m_enemyFlockPosition.position - transform.position;

		m_count += 1 * Time.deltaTime;
		if (m_count >= 3)
		{
			SpawnRadarDots();
			m_count = 0;
		}
			

	}

	public void SpawnRadarDots()
	{

		Vector2 _radarPos = new Vector2(transform.position.x, transform.position.y + Random.Range(2,5));

		//GameObject _radarDot = TrashMan.spawn("RADAR_DOTS",_radarPos, transform.rotation);
		//_radarDot.transform.parent = transform;
	}
}
