using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeNav : MonoBehaviour
{
    private NavMeshPath path;
    public GameObject target;
    private float elapsed = 0.0f;

    public GameObject front;
    public GameObject back;

    public float speed = 1.0f;
    public float stepLength = 2.0f;
    public float Offset;

    private bool extended = false;

    private float y_start;

    private Vector3 actualTarget;

    private Vector3 direction;

    public GameObject monster;
    public GameObject body;

    float angle;

    void Start() 
    {
        Offset = (front.transform.position - back.transform.position).magnitude;
        y_start = front.transform.position.y;
        path = new NavMeshPath();
        elapsed = 0.0f;    
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 0.1f)
        {
            elapsed -= 0.1f;
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
        }
        if (path.corners != null)
        {
            Vector3 direction = path.corners[1] - path.corners[0];
            angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            if (angle != 0)
            {
                transform.RotateAround(body.transform.position, Vector3.up, angle * Time.deltaTime);
            }
        }
        if (angle < 10)
        {
            StartCoroutine(MoveFront(path));
            StartCoroutine(MoveBack(path));
        }
        //StartCoroutine(MoveFront(path));
        //StartCoroutine(MoveBack(path));
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
    }

    private IEnumerator MoveFront(NavMeshPath path)
    {
        float step = speed * Time.deltaTime;
        if (!extended) 
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                front.transform.position = Vector3.MoveTowards(front.transform.position, path.corners[i+1], step);
                if ((front.transform.position - back.transform.position).magnitude > stepLength)
                {
                    extended = true;
                    yield return null;
                }
            }   
        }
    }

    private IEnumerator MoveBack(NavMeshPath path)
    {
        float step = speed * Time.deltaTime;
        if (extended)
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                back.transform.position = Vector3.MoveTowards(back.transform.position, path.corners[i+1], step);
                if ((front.transform.position - back.transform.position).magnitude < Offset)
                {
                    extended = false;
                    yield return null;
                }
            }
        }
    }
}
