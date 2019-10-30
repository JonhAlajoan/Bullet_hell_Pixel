﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ManagerMenu : MonoBehaviour
{
    public int indexDefault = 0;
    public int indexCanvas = 0;
    public Canvas[] canvas;

    public GameObject defaultBtn;

    private EventSystem myEventSystem;

    private void Awake()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        Cursor.visible = false;
    }

    private void Deselect()
    {
        myEventSystem.SetSelectedGameObject(null);
    }

    private void Update()  
    {
        if(myEventSystem.currentSelectedGameObject != null)
            defaultBtn = myEventSystem.currentSelectedGameObject.gameObject;

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetMouseButtonDown(0))
           && defaultBtn != null
           && myEventSystem.currentSelectedGameObject == null)
        {
            myEventSystem.SetSelectedGameObject(defaultBtn);
            indexDefault = 0;
        }
    }
    
    

}
