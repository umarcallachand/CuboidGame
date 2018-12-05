using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePlayer : MonoBehaviour {

    private Cube_rotation _playerRollCube1;
    private Cube_rotation _playerRollCube2;
    private GameObject _cube1Glow;
    private GameObject _cube2Glow;

    //Awake is used to initialize any variables or game state before the game starts
    private void Awake()
    {
        _playerRollCube1 = GameObject.Find("Cube1").GetComponent<Cube_rotation>();
        _playerRollCube2 = GameObject.Find("Cube2").GetComponent<Cube_rotation>();
        _cube1Glow = GameObject.Find("Cube1Glow");
        _cube2Glow = GameObject.Find("Cube2Glow");
    }

    // Use this for initialization
    void Start ()
    {
        _playerRollCube1.enabled = true;
        _cube1Glow.SetActive(true);
        _playerRollCube2.enabled = false;
        _cube2Glow.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _playerRollCube1.enabled = !_playerRollCube1.enabled;
            _cube1Glow.SetActive(!_cube1Glow.activeSelf);
            _playerRollCube2.enabled = !_playerRollCube2.enabled;
            _cube2Glow.SetActive(!_cube2Glow.activeSelf);
        }
    }
}
