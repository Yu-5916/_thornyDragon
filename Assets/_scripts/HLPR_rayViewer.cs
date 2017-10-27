using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HLPR_rayViewer : MonoBehaviour {

    public float rayRange = 50f;

    private Camera cam;
    
	void Start ()
    {
        cam = GetComponentInParent<Camera>();	
	}
	

	void Update ()
    {
        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
        Debug.DrawRay(lineOrigin, cam.transform.forward * rayRange, Color.red);
	}
}
