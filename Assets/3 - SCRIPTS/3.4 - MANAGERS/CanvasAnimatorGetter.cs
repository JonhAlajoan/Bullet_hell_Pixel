using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimatorGetter : MonoBehaviour {

    ManagerMenu m_menuManager;

	void Start ()
    {
        if(!m_menuManager)
            m_menuManager = GameObject.FindGameObjectWithTag("ManagerScene").GetComponent<ManagerMenu>();
	}


	
	public void changeCanvas(string nextMenu, string previousMenu)
    {
      // m_menuManager.ChangeCanvas(nextMenu,previousMenu);
    }
}
