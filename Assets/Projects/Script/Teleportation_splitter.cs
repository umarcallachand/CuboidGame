using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation_splitter : MonoBehaviour {

    GameObject _cubes;
    public GameObject TeleportEffect;
    private bool _isTeleported;
    private GameObject _player;

    void Start()
    {
        //saves the gameobject cube in a variable so that we can activate it again
        _cubes = GameObject.Find("Cubes");
        //deactivates at start of game
        _cubes.SetActive(false);
        _isTeleported = false;
        _player = GameObject.Find("Player Final");
    }

    private void Update()
    {
        if (_isTeleported)
        {
            _player.transform.localScale = Vector3.Lerp(_player.transform.localScale, Vector3.zero, 2*Time.deltaTime);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //only triggers when cuboid is standing on it
        if (other.CompareTag("TopBottomCollider"))
        {
            StartCoroutine(Teleport());
        }

    }

    private IEnumerator Teleport()
    {
        _isTeleported = true;
        
        var teleportLocation = transform.position + new Vector3(0, 0.2f, 0);
        var teleport = Instantiate(TeleportEffect, teleportLocation , transform.rotation);
        yield return new WaitForSeconds(2f);
        _isTeleported = false;
        _player.SetActive(false);
        Destroy(teleport);
        _cubes.SetActive(true);
        //gameObject.SetActive(false);
    }
}
