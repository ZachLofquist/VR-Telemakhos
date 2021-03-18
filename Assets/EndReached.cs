using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndReached : MonoBehaviour
{
    public CharacterController FPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision) 
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "FPCBody")
        {
            FPC.GetComponent<FPCOp>().Dead();
        }
    }
}
