using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleStar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        transform.Rotate(Vector3.up, 40 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {

        gameObject.SetActive(false);


    }
}
