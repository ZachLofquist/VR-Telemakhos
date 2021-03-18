using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLine : MonoBehaviour
{
    public GameObject check_1;
    public GameObject check_2;
    public GameObject check_3;
    public GameObject check_4;
    public GameObject check_5;
    public GameObject check_6;
    public GameObject check_7;
    public GameObject check_8;

    private List<GameObject> waypoints;

    int i;
    private bool forward;

    public float speed = 1.0f;

    private Quaternion oldRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        waypoints = new List<GameObject>();
        waypoints.Add(check_1);
        waypoints.Add(check_2);
        waypoints.Add(check_3);
        waypoints.Add(check_4);
        waypoints.Add(check_5);
        waypoints.Add(check_6);
        waypoints.Add(check_7);
        waypoints.Add(check_8);
        i = 0;
        oldRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        oldRotation = transform.rotation;
        if (transform.position == waypoints[i].transform.position)
        {
            if (i == 0)
            {
                forward = true;
            }
            if (i == 7)
            {
                forward = false;
            }
            if (forward)
            {
                i++;
            }
            if (!forward)
            {
                i--;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[i].transform.position, step);
            var targetRotation = Quaternion.LookRotation(waypoints[i].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}
