using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCOp : MonoBehaviour
{
    private Vector3 initial_pos; 
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        initial_pos = transform.position;
    }

    void Update() 
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, 1.1f))
        {
            if (hit.collider.gameObject.tag == "End")
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        transform.position = initial_pos;
    }
}
