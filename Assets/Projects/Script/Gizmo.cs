using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{

    public float GizmoSize = .75f;

    public Color GizmoColor = Color.yellow;
	// Use this for initialization
    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawSphere(transform.position,GizmoSize);
    }
}
