using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesMotion : MonoBehaviour
{

    public float moveRange = 2.5f;
    public float speed = 300f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPosition - Vector3.down * 5, startPosition, Time.deltaTime * speed);
        //Vector3 ypos = startPosition;
        //ypos.y += moveRange * Mathf.Sin(Time.time * speed);
        //transform.position = ypos;
    }
}
