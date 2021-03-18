 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRot : MonoBehaviour
{
    public Transform scale_rotation_point;
    public Transform body_rotation_point;

    public GameObject FPC;
    public GameObject Scylla;

    public float angle = 10.0f;
    public float initial_offset = 0;
    public float scale_tilt = -70;
    private float detect_dist = 25;
    private float curr_offset;

    public float speed = 1.0f;

    RaycastHit hit;

    Vector3 target_direction;

    // Start is called before the first frame update
    void Start()
    {
        //transform.RotateAround(scale_rotation_point.position, transform.right, scale_tilt);
        curr_offset = 0;
        transform.RotateAround(body_rotation_point.position, body_rotation_point.up, initial_offset);
    }

    // Update is called once per frame
    void Update()
    {
        target_direction = FPC.transform.position - body_rotation_point.position;
        transform.RotateAround(body_rotation_point.position, body_rotation_point.up, angle * Time.deltaTime);
        int layermask = 1 << 9;
        layermask = ~layermask;
        if (Physics.Raycast(body_rotation_point.position, target_direction, out hit, detect_dist, layermask))
        {
            //Debug.DrawRay(body_rotation_point.position, target_direction, Color.green);
            //Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "FPCBody")
            {
                //Debug.Log("Hit Detected");
                GameObject.Find("Telepath").GetComponent<telearc>().linerenderer.enabled = false;
                GameObject.Find("Telepath").GetComponent<telearc>().teletarg.Stop();
                GameObject.Find("Telepath").GetComponent<telearc>().teletarg.Clear();
                GameObject.Find("Telepath").GetComponent<telearc>().enabled = false;
                //Debug.Log(Scylla.GetComponent<ScyllaOp>().attacking);
                Scylla.GetComponent<ScyllaOp>().attacking = true;
                //Debug.Log(Scylla.GetComponent<ScyllaOp>().attacking);
                try 
                {
                    Scylla.GetComponent<PatrolLine>().enabled = false;
                }
                catch 
                {
                    Scylla.GetComponent<PatrolLoop>().enabled = false;
                }
                var targetRotation = Quaternion.LookRotation(FPC.transform.position - Scylla.transform.position);
                Scylla.transform.rotation = Quaternion.Slerp(Scylla.transform.rotation, targetRotation, speed * Time.deltaTime);
                if (curr_offset < 0)
                {
                    transform.RotateAround(scale_rotation_point.position, transform.right, 10 * Time.deltaTime);
                    curr_offset += 10 * Time.deltaTime;
                }
            }
            else
            {
                //Debug.Log(curr_offset);
                GameObject.Find("Telepath").GetComponent<telearc>().enabled = true;
                try 
                {
                    Scylla.GetComponent<PatrolLine>().enabled = true;
                }
                catch 
                {
                    Scylla.GetComponent<PatrolLoop>().enabled = true;
                }
                if (curr_offset > scale_tilt)
                {
                    transform.RotateAround(scale_rotation_point.position, transform.right, -10 * Time.deltaTime);
                    curr_offset -= 10 * Time.deltaTime;
                }
            }
        }
        else
        {
            //Debug.Log(curr_offset);
            GameObject.Find("Telepath").GetComponent<telearc>().enabled = true;
            try 
            {
                Scylla.GetComponent<PatrolLine>().enabled = true;
            }
            catch 
            {
                Scylla.GetComponent<PatrolLoop>().enabled = true;
            }  
            if (curr_offset > scale_tilt)
            {
                transform.RotateAround(scale_rotation_point.position, transform.right, -10 * Time.deltaTime);
                curr_offset -= 10 * Time.deltaTime;
            }
        }
    }
}
