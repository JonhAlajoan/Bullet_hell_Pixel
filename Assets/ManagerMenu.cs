using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerMenu : MonoBehaviour
{
    public Canvas[] canvas;
    public EventSystem eventSystem;
    public GameObject[] textObjects;
    protected GameObject m_selectedObject;


    private void OnEnable()
    {
        eventSystem = EventSystem.current;
        m_selectedObject = eventSystem.currentSelectedGameObject;
            
    }


    private void Update()
    {
        if (!eventSystem)
            eventSystem = EventSystem.current;

        
        
    }

    public void JumpNextObject(string _name)
    {
            for (int i = 0; i < textObjects.Length; i++)
            {
                if (textObjects[i].name == _name)
                    m_selectedObject = textObjects[i + 1];
            }
 
    }

}
