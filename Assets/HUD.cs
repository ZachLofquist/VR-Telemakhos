using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static float scale = 0.1f;

    Vector3 startscale = new Vector3(scale, scale, scale);
    public Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = startscale;
        this.transform.rotation = Camera.main.transform.rotation;
        this.transform.position = startpos + Camera.main.transform.position + Camera.main.transform.forward * 1f - Camera.main.transform.up * .4f;
    }
}
