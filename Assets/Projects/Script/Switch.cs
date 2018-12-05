using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

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
        if (other.CompareTag("Player") || other.CompareTag("Player_half"))
        {
            IsOn = !IsOn;
            anim.SetBool("on", IsOn);
        }
    }
}
