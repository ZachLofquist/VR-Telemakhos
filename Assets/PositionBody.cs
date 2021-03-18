using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBody : MonoBehaviour
{
    public Transform frontLeft;
    public Transform backLeft;
    public Transform frontRight;
    public Transform backRight;

    Vector3 target;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        target.y = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        target.x = (frontLeft.position.x + frontRight.position.x + backLeft.position.x + backRight.position.x)/4;
        target.z = (frontLeft.position.z + frontRight.position.z + backLeft.position.z + backRight.position.z)/4;

        float step = speed * Time.deltaTime;
        //Debug.Log(target.x);
        //Debug.Log(target.z);
        //Debug.Log(transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
