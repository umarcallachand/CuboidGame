using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    North,
    South,
    East,
    West,
    Up
}

public class PlayerRoll : MonoBehaviour {

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
    private GameObject _fallinGameObject;
    private SoundManager _audioManager;
    private bool _roll;
    private string _soundName;
    public bool IsEnable { get; set; }
    
    public bool HasWon { get; set; }

    private void Awake()
    {
        _audioManager = FindObjectOfType<SoundManager>();
        TopDirection = Direction.Up;
    }

    
    void Start()
    {
        _moving = false;
        
        _grounded = false;
        _fallinGameObject = GameObject.Find("cuboidFall");
        DontDestroyOnLoad(_fallinGameObject);
        _score = GameObject.Find("GameScore").GetComponent<ScoreDB>();
        _roll = false;
        HasWon = false;
        IsEnable = false;

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
                //play audio
                if (_roll)
                {
                    _audioManager.Play(_soundName);
                    _roll = false;
                }
                
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && IsEnable) Rotate(Direction.North);
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsEnable) Rotate(Direction.West);
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && IsEnable) Rotate(Direction.South);
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && IsEnable) Rotate(Direction.East);

        }
        //testing purposes


    }

    private void LateUpdate()
    {
        RaycastHit hit1, hit2;
        Ray ray1, ray2;
        ray1 = new Ray(RayPosition[0].position, Vector3.down);
        ray2 = new Ray(RayPosition[1].position, Vector3.down);

        Debug.DrawRay(RayPosition[0].position, Vector3.down, Color.blue);
        Debug.DrawRay(RayPosition[1].position, Vector3.down, Color.blue);

        if (!_grounded && !_moving && !HasWon)
        {
            if (Physics.Raycast(ray1, out hit1) && Physics.Raycast(ray2, out hit2))
            {
                //both ray cuts through deadend
                if (hit1.transform.CompareTag("Deadend") && hit2.transform.CompareTag("Deadend"))
                {
                    Debug.Log("hit");

                    _grounded = true;
                    //restart game after 1.5s
                    GameObject.Find("Deadend").GetComponent<RestartLevel>().Restart();

                    //make player fall
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    GameObject.Find("cuboid2").GetComponent<BoxCollider>().isTrigger = true;
                    canUpdate = false;
                }
                else if (hit1.transform.CompareTag("Deadend"))
                {
                    Debug.Log("hit");

                    _grounded = true;
                    //restart game after 1.5s
                    GameObject.Find("Deadend").GetComponent<RestartLevel>().Restart();

                    PlayerFallOneHit(RayPosition[0].position);
                }
                else if (hit2.transform.CompareTag("Deadend"))
                {
                    Debug.Log("hit");

                    _grounded = true;
                    //restart game after 1.5s
                    GameObject.Find("Deadend").GetComponent<RestartLevel>().Restart();

                    PlayerFallOneHit(RayPosition[1].position);
                }
            }
        }
    }

    private void PlayerFallOneHit(Vector3 positionOfForceVector3)
    {
        //make object fall
        var newGameObject = Instantiate(_fallinGameObject, transform.position, transform.rotation);
        newGameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * 100, positionOfForceVector3);

        Destroy(gameObject);
        canUpdate = false;
    }

    void Rotate(Direction direction)
    {
        _rotationDirection = direction;
        _moving = true;
        _totalRotation = 0;
        CuboidGameObject.transform.localPosition = Vector3.zero;
        _grounded = false;
        if (_rotationDirection == Direction.East)
        {
            if ((TopDirection == Direction.East) || (TopDirection == Direction.West))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(1, -.5f, 0);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.East;
                _pivot = transform.position + new Vector3(.5f, -1, 0);
            }
            else _pivot = transform.position + new Vector3(.5f, -.5f, 0);

            _axis = Vector3.forward;
        }
        else if (_rotationDirection == Direction.West)
        {
            if ((TopDirection == Direction.East) || (TopDirection == Direction.West))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(-1, -.5f, 0);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.West;
                _pivot = transform.position + new Vector3(-.5f, -1, 0);
            }
            else _pivot = transform.position + new Vector3(-.5f, -.5f, 0);

            _axis = Vector3.forward;
        }
        else if (_rotationDirection == Direction.North)
        {
            if ((TopDirection == Direction.North) || (TopDirection == Direction.South))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(0, -.5f, 1);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.North;
                _pivot = transform.position + new Vector3(0, -1, .5f);
            }
            else _pivot = transform.position + new Vector3(0, -.5f, .5f);

            _axis = Vector3.right;
        }
        else if (_rotationDirection == Direction.South)
        {
            if ((TopDirection == Direction.North) || (TopDirection == Direction.South))
            {
                TopDirection = Direction.Up;
                _pivot = transform.position + new Vector3(0, -.5f, -1);
            }
            else if (TopDirection == Direction.Up)
            {
                TopDirection = Direction.South;
                _pivot = transform.position + new Vector3(0, -1, -.5f);
            }
            else _pivot = transform.position + new Vector3(0, -.5f, -.5f);

            _axis = Vector3.right;
        }
        _score.UpdateScore();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            PlaySound("Wood Creak");
        }
        else if (other.CompareTag("Tiles"))
        {
            PlaySound("Roll Block");
        }

        else if (other.CompareTag("Teleport") && TopDirection == Direction.Up)
        {
            PlaySound("Teleport");
        }

        else if (other.CompareTag("Switch"))
        {
            PlaySound("Switch");
        }

        if (other.CompareTag("Tiles"))
        {
            IsEnable = true;
        }
    }

    private void PlaySound(string name)
    {
        _soundName = name;
        _roll = true;
    }
}
