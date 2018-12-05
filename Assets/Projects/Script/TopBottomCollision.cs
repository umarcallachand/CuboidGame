using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBottomCollision : MonoBehaviour {

    public GameObject BrokenWood;
    public float Force;
    public float Radius;
    private GameObject _soundManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            _soundManager = GameObject.Find("Sound Manager");
            _soundManager.GetComponent<SoundManager>().Play("Wood break");
            var destroyed = Instantiate(BrokenWood, transform.position, transform.rotation);
            Destroy(other.gameObject);
            
            foreach (var d in destroyed.GetComponentsInChildren<Rigidbody>())
            {
                d.AddExplosionForce(Force, transform.position, Radius);
            }
        }

    }
}
