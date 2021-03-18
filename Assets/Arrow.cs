using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Arrow : MonoBehaviour
{
    public GameObject arrow_back;
    private float m_Speed = 50.0f;
    public Transform m_Tip = null;

    private Rigidbody m_Rigidbody = null;
    private bool m_IsStopped = true;
    private Vector3 m_LastPosition;

    private GameObject Scylla;

    public GameObject Bow;

    //public Light arrowLight;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LastPosition = GameObject.Find("FPCBody").transform.position;
        //arrowLight.enabled = false;
    }

    private void FixedUpdate()
    {
        if (m_IsStopped)
            return;

        // Rotate
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));

        // Collision Check
        if (Physics.Linecast(m_LastPosition, m_Tip.position, out RaycastHit hit))
        {
            
            Stop();
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Scylla_Scale")
            {
                Scylla = hit.collider.transform.root.gameObject;
                Scylla.GetComponent<ScyllaOp>().health -= 1;
                //Debug.Log(Scylla.GetComponent<ScyllaOp>().health);
                //hit.collider.gameObject.SetActive(false);
                hit.collider.gameObject.GetComponent<Renderer>().enabled = false; 
                for (int i = 0; i <  hit.collider.gameObject.transform.childCount; i++)
                {
                    hit.collider.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
                Bow.GetComponent<Bow>().arrow_num--;
                Delete();
            }
            if (hit.collider.gameObject.tag == "Scylla_Head")
            {
                Scylla = hit.collider.transform.root.gameObject;
                if (Scylla.GetComponent<ScyllaOp>().health == 1)
                {
                    Scylla.GetComponent<ScyllaOp>().health -= 1;
                    hit.collider.gameObject.SetActive(false);
                    Bow.GetComponent<Bow>().arrow_num--;
                    Delete(); 
                }
                
            }
        }

        // Store the position
        m_LastPosition = m_Tip.position;
    }

    private void Stop()
    {
        m_IsStopped = true;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }

    public void Fire(float pullValue)
    {
        m_LastPosition = GameObject.Find("FPCBody").transform.position;
        m_IsStopped = false;
        transform.parent = null;

        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * m_Speed * pullValue);
        //arrowLight.enabled = true;

        //Destroy(gameObject, 5.0f); // Cleans up scene after 5 seconds. Can be removed
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
