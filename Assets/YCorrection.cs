using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YCorrection : MonoBehaviour
{
    private float y_start;
    private Vector3 target;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
         y_start = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        target = new Vector3(transform.position.x, y_start, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, step);

    }
}
