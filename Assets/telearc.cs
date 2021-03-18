using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class telearc : MonoBehaviour
{
    public CharacterController controller;
    public SteamVR_Input_Sources handleft;
    public SteamVR_Action_Boolean telebtn;
    public SteamVR_Behaviour_Pose VRcontrollerPose;
    public int segments = 12;
    public float maxdist = 10.0f;
    public LineRenderer linerenderer;

    public ParticleSystem teletarg;

    bool btn_pressed = false;
    bool target_locked = false;
    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        bool telpressed = telebtn.GetState(handleft);
        if (!telpressed && !btn_pressed)
        {
            linerenderer.enabled = false;
            return;
        }
        if (!telpressed && btn_pressed)
        {
            btn_pressed = false;
            if (target_locked)
            {
                Vector3 diff = target - controller.transform.position;
                controller.Move(diff);
            }
            target_locked = false;
            linerenderer.enabled = false;
            return;
        }
        btn_pressed = true;
        var pointlist = new List<Vector3>();
        
        Quaternion rot = VRcontrollerPose.transform.rotation;
        Matrix4x4 m = Matrix4x4.Rotate(rot);
        Vector3 pointdirection = m.MultiplyPoint3x4(new Vector3(0, 0, 1));
        float angle = Vector3.Dot(new Vector3(0, 1, 0), pointdirection);
        float dist = Mathf.Sin(angle*Mathf.PI) * maxdist;
        Vector3 dir = pointdirection;
        dir.y = 0;
        dir = Vector3.Normalize(dir) * dist;
        
        Vector3 A, P, B;
        A = VRcontrollerPose.transform.position;
        B = A + dir;
        RaycastHit hit;
        RaycastHit hit_check;
        if (Physics.Raycast(B, new Vector3(0, -1, 0), out hit, 10))
        {
            if (Physics.Raycast(A, dir, out hit_check, dist))
            {
                target_locked = false;
                linerenderer.enabled = false;
                teletarg.Stop();
                teletarg.Clear();
            }
            else
            {
                B.y = B.y - hit.distance;
                target = B;
                target_locked = true;
                linerenderer.enabled = true;
                teletarg.transform.position = B;
                teletarg.Play();
            }
        }
        else {
            target_locked = false;
            linerenderer.enabled = false; // change color instead somehow. Maybe add target on grouind of where teleporting to
            teletarg.Stop();
            teletarg.Clear();
        }

        P = (A+B)/2;
        P.y += 3.0f; // change to change arc height/size

        for (float t = 0; t <= 1; t += 1.0f / (float)segments)
        {
            Vector3 X = A * (1-t) + P*t; 
            Vector3 Y = Vector3.Lerp(P, B, t);
            Vector3 R = Vector3.Lerp(X, Y, t);
            pointlist.Add(R);
        } 
        linerenderer.positionCount = pointlist.Count;
        linerenderer.SetPositions(pointlist.ToArray());
        
    }
}
