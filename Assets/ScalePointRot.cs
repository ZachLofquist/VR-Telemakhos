using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePointRot : MonoBehaviour
{
    public Transform body_rotation_point;

    public float angle = 10.0f;
    public float initial_offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.RotateAround(body_rotation_point.position, body_rotation_point.up, initial_offset);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(body_rotation_point.position, body_rotation_point.up, angle * Time.deltaTime);
    }
}