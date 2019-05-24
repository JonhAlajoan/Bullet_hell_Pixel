using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ManagerMenu : MonoBehaviour
{
    public int indexDefault = 0;
    public int indexCanvas = 0;
    public Canvas[] canvas;

    public Button defaultBtn;

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
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetMouseButtonDown(0))
           && defaultBtn != null
           && myEventSystem.currentSelectedGameObject == null)
        {
            defaultBtn.Select();
            indexDefault = 0;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Deselect();
        }

    }
    
    public void ChangeCanvas(string _canvas)
    {
        for(int i = 0; i < canvas.Length; i++)
        {
            if(canvas[i].name == _canvas)
            {
                canvas[i].gameObject.SetActive(false);
                canvas[i + 1].gameObject.SetActive(true);
            }
        }
        indexDefault = 0;
    }

}
