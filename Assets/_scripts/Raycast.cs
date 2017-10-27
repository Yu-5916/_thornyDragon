using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {

    public int rayDamage = 1;
    public float fireRate = .25f; //this is specifical for firing, may not need
    public float rayRange = 50f; //this will be necessary
    public float hitForce = 100f; //apply force to object...not sure if we need this
    public Transform rayEnd; //where the ray ends
    public AudioSource rayAudio;

    private Camera cam;
    private WaitForSeconds duration = new WaitForSeconds(.07f); //how long the lase stays in the game view 
    private LineRenderer laserLine; //array of two or more points and draws a straight line 
    private float nextFire; //hold the time at which the player will be able to fire again

	void Start ()
    {

        laserLine = GetComponent<LineRenderer>();
        //rayAudio = GetComponent<AudioSource>();
        cam = GetComponentInParent<Camera>();

    }

    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update ()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(PewEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, rayEnd.position); //start the line renderer at the end of the lazer eyes

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, rayRange))
            {
                laserLine.SetPosition(1, hit.point);

                
            } else
            {
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * rayRange));
            }
        }
		
	}

    private IEnumerator PewEffect()
    {
        //rayAudio.Play();

        laserLine.enabled = true;
        yield return duration;
        laserLine.enabled = false;
    }
}
