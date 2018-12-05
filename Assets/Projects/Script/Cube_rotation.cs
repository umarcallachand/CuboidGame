using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_rotation : MonoBehaviour {

    public float rotationSpeed = 200;

    private ScoreDB _score;
    public Direction TopDirection { get; set; }
    private bool _moving;
    private Direction _rotationDirection;
    private Vector3 _pivot;
    private float _totalRotation;
    private Vector3 _axis;
    public GameObject CuboidGameObject;
    private bool _grounded;
    public float MaxRange;
    public Transform[] RayPosition;

    private bool canUpdate = true;

    void Start()
    {
        _moving = false;
        TopDirection = Direction.Up;
        _grounded = false;
        _score = GameObject.Find("GameScore").GetComponent<ScoreDB>();

    }

    void Update()
    {
        if (canUpdate)
        {
            if (_moving)
            {
                float deltaRotation = rotationSpeed * Time.deltaTime;
                if (_totalRotation + deltaRotation >= 90)
                {
                    deltaRotation = 90 - _totalRotation;
                    _moving = false;
                }
                if ((_rotationDirection == Direction.West) || (_rotationDirection == Direction.North))
                    transform.RotateAround(_pivot, _axis, deltaRotation);
                else transform.RotateAround(_pivot, _axis, -deltaRotation);

                _totalRotation += deltaRotation;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) Rotate(Direction.North);
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) Rotate(Direction.West);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) Rotate(Direction.South);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) Rotate(Direction.East);

        }
        
    }

    private void LateUpdate()
    {
        Ray ray1 = new Ray(transform.position ,Vector3.down);

        Debug.DrawRay(transform.position, Vector3.down, Color.blue);

        if (!_grounded && !_moving)
        {
            RaycastHit hit1;
            if (Physics.Raycast(ray1, out hit1))
            {
                if (hit1.transform.CompareTag("Deadend"))
                {
                    Debug.Log("hit");

                    _grounded = true;

                    GameObject.Find("Deadend").GetComponent<RestartLevel>().Restart();
                    //make player fall
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    GameObject.Find("cube").GetComponent<BoxCollider>().isTrigger = true;
                    canUpdate = false;
                }
            }
        }
    }

    void Rotate(Direction direction)
    {
        _rotationDirection = direction;
        _moving = true;
        _totalRotation = 0;
        //CuboidGameObject.transform.localPosition = Vector3.zero;
        _grounded = false;
        if (_rotationDirection == Direction.East)
        {
            if ((TopDirection == Direction.East) || (TopDirection == Direction.West))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(.5f, -.5f, 0);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.East;
                _pivot = transform.position + new Vector3(.5f, -.5f, 0);
            }
            else _pivot = transform.position + new Vector3(.5f, -.5f, 0);

            _axis = Vector3.forward;
        }
        else if (_rotationDirection == Direction.West)
        {
            if ((TopDirection == Direction.East) || (TopDirection == Direction.West))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(-.5f, -.5f, 0);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.West;
                _pivot = transform.position + new Vector3(-.5f, -.5f, 0);
            }
            else _pivot = transform.position + new Vector3(-.5f, -.5f, 0);

            _axis = Vector3.forward;
        }
        else if (_rotationDirection == Direction.North)
        {
            if ((TopDirection == Direction.North) || (TopDirection == Direction.South))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(0, -.5f, .5f);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.North;
                _pivot = transform.position + new Vector3(0, -.5f, .5f);
            }
            else _pivot = transform.position + new Vector3(0, -.5f, .5f);

            _axis = Vector3.right;
        }
        else if (_rotationDirection == Direction.South)
        {
            if ((TopDirection == Direction.North) || (TopDirection == Direction.South))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(0, -.5f, -.5f);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.South;
                _pivot = transform.position + new Vector3(0, -.5f, -.5f);
            }
            else _pivot = transform.position + new Vector3(0, -.5f, -.5f);

            _axis = Vector3.right;
        }

        _score.UpdateScore();
    }
}
