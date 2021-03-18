using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    
    private float intro_timer;
    public Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        intro_timer = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (intro_timer > 0)
        {
            intro_timer -= Time.deltaTime;
            this.transform.rotation = Camera.main.transform.rotation;
            this.transform.position = startpos + Camera.main.transform.position + Camera.main.transform.forward * 4f - Camera.main.transform.right * 1.5f;
        }
        if (intro_timer <= 0)
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
