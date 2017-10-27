using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

    private Material material;
    private Color normalColor;
    private Color highlightColor;
    private bool touching;
    private float interval;

    public AudioSource rayray;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;

        normalColor = material.color;
        highlightColor = new Color(
            normalColor.r * 1.5f,
            normalColor.g * 1.5f,
            normalColor.b * 1.5f
            );    
    }


    public void OnMouseOver()
    {
        touching = true;

        if (touching)
        {
            interval += Time.deltaTime;
            material.color = Color.Lerp(normalColor, highlightColor, interval);

            if (touching && Input.GetButtonDown("Fire1"))
            {
                rayray.Play();
            }
        }     
    }

    public void OnMouseExit()
    {
        touching = false;

        if (!touching)
        {
            interval += Time.deltaTime;
            material.color = Color.Lerp(highlightColor, normalColor, interval);
        }
    }

    void FindMouse()
    {
        Vector3 p = new Vector3();
        Camera c = Camera.main;
        Event e = Event.current;
        Vector2 mousePos = new Vector2()
        {
            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            x = e.mousePosition.x,
            y = c.pixelHeight - e.mousePosition.y
        };

        p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + p.ToString("F3"));
        GUILayout.EndArea();
    }
}
