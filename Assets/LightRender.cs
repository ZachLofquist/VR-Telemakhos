using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRender : MonoBehaviour
{
    Transform mainCamTransform; // Stores the FPS camera transform
    private bool visible = true;
    public float distanceToAppear = 75;
    //Renderer objRenderer;
    public Light pointLight;

    private void Start()
    {
        //objRenderer = gameObject.GetComponent<Renderer>(); //Get render reference
    }
    private void Update()
    {
        disappearChecker();
    }
    private void disappearChecker()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);

        // We have reached the distance to Enable Object
        if (distance < distanceToAppear)
        {
            if (!visible)
            {
                //objRenderer.enabled = true; // Show Object
                pointLight.enabled = true;
                visible = true;
                //Debug.Log("Visible");
            }
        }
        else if (visible)
        {
            //objRenderer.enabled = false; // Hide Object
            pointLight.enabled = false;
            visible = false;
            //Debug.Log("InVisible");
        }
    }
}