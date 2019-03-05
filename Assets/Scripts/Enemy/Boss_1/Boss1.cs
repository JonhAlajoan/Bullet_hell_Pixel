using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BaseBoss {

	[SerializeField]
	protected UbhShotCtrl[] m_shotControl;
	States m_state;
	public override void Start()
	{
		base.Start();
		Initialization(1000, 5, 5000, gameObject.GetComponent<Animator>());
		m_shotControl = GetComponentsInChildren<UbhShotCtrl>();
	}

	public override void Update()
	{
		base.Update();

		if(healthBarImage.fillAmount == 0.75f)
		{
			isInvulnerable = true;
			m_state = States.PHASE2;
			ChangePhase(m_state);
		}

		if (healthBarImage.fillAmount == 0.5f)
		{
			isInvulnerable = true;
			m_state = States.PHASE3;
			ChangePhase(m_state);
		}

	}

	public override void Shoot(States _actualState)
	{
		switch(_actualState)
		{
			case States.PHASE1:
			{
					if (Time.time > m_nextShotTime)
					{
						for (int i = 0; i < m_shotControl.Length; i++)
						{
							m_shotControl[i].StartShotRoutine();
						}
						m_nextShotTime = Time.time + m_msBetweenShots / 1000;
					}
				}
			break;
		}
		
	}

}
