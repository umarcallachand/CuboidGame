using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation_joining : MonoBehaviour {

    GameObject _player;
    GameObject _playerSplitted, _cube1, _cube2 ;
    private float _cuboidX, _cuboidZ;
    private bool _canJoin;
    private Vector3 rotationDir;
    private Direction dir;


    void Awake()
    {
        _player = GameObject.Find("Player Final");
        //contains both cubes
        _playerSplitted = GameObject.Find("Cubes");
        _cube1 = GameObject.Find("Cube1");
        _cube2 = GameObject.Find("Cube2");
        _canJoin = false;
    }

    private void LateUpdate()
    {
        var offset = _cube1.transform.position - _cube2.transform.position;
        var modX = Mathf.Round(Mathf.Abs(offset.x));
        var modZ = Mathf.Round(Mathf.Abs(offset.z));
        //Debug.Log(modX + " " + modZ);

        if(modX == 1 && modZ == 0 && !_canJoin)
        {
            FindXZCuboid();
            dir = Direction.West;
            rotationDir = new Vector3(-180, 90, 0);
            _canJoin = true;
        }
        else if (modZ == 1 && modX == 0 && !_canJoin)
        {
            FindXZCuboid();
            dir = Direction.South;
            rotationDir = new Vector3(0,0,0);
            _canJoin = true;
        }

        else _canJoin = false;
    }

    private void FindXZCuboid()
    {
        _cuboidX = (Mathf.Round(_cube2.transform.position.x) + Mathf.Round(_cube1.transform.position.x)) / 2;
        _cuboidZ = (Mathf.Round(_cube2.transform.position.z) + Mathf.Round(_cube1.transform.position.z)) / 2;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_half"))
        {

            //sets player to active again so that Instantiate will create an active gameObject and set it inactivve just after
            _player.SetActive(true);
            
            _player.transform.localScale = new Vector3(0.5f,0.5f,1f);
            var newPlayer = Instantiate(_player, (new Vector3(_cuboidX, 2.75f, _cuboidZ)), Quaternion.Euler(rotationDir));
            newPlayer.GetComponent<PlayerRoll>().TopDirection = dir;
            
            _player.SetActive(false);

            //set both cubes to inactive
            _playerSplitted.SetActive(false);
        }

    }
}
