using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongSwitch : MonoBehaviour {

    public GameObject bridges;
    private Animator anim;
    public bool IsOn;

    void Start()
    {
        anim = bridges.GetComponent<Animator>();
        anim.SetBool("on", IsOn);
    }

    private void OnTriggerEnter(Collider other)
    {
        //only triggers when cuboid is standing on it
        if (other.CompareTag("TopBottomCollider"))
        {
            IsOn = !IsOn;
            anim.SetBool("on", IsOn);
        }

    }
}
