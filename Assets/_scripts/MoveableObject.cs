using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

    public GameObject testCube;

    //cursor test
    public Texture2D cursorTexture;
    public static Vector2 cursorHotspot;
    private CursorMode cursorMode = CursorMode.Auto;

	void Start () {
        testCube = GameObject.Find("testCube");

        Cursor.SetCursor(cursorTexture, cursorHotspot, cursorMode);
	}

    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "testCube")
                {

                    Debug.Log("Object clicked");
                }
            }
        }
		
	}

    private void OnGUI()
    {
        //Lock the cursor
        if(GUI.Button(new Rect(0,0,100,50), "Lock Cursor"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            Debug.Log(cursorHotspot);
        }

        //Confine the cursor
        if(GUI.Button(new Rect(125,0,100,50), "Confine Cursor"))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
