using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour {
    public Transform Center;
    public float Speed;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //Speed = 50 * Time.deltaTime;
	    //Debug.Log(Speed);
        transform.RotateAround(Center.position,Vector3.up,Speed);
    }
}
