using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScyllaOp : MonoBehaviour
{
    public float health;
    public float prev_health;
    private float attack_timer;
    private float respawn_timer;
    private Vector3 initial_pos; 

    public bool attacking;

    public CharacterController FPC;
    
    public GameObject death_timer;

    // Start is called before the first frame update
    void Start()
    {
        health = 4.0f;
        attack_timer = 9.0f;
        respawn_timer = 90.0f;
        initial_pos = transform.position;
        prev_health = health;
        attacking = false;
        death_timer.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attack_timer);
        //Debug.Log(attacking);
        if (attacking == true)
        {
            death_timer.GetComponent<Renderer>().enabled = true;
            if (prev_health != health)
            {
                attack_timer += 3.0f;
                prev_health = health;
            }
            if (attack_timer > 0)
            {
                attack_timer -= Time.deltaTime;
                death_timer.GetComponent<TextMesh>().text = string.Format("Scylla's Gaze {0:#00}", attack_timer);
            }
            else
            {
                death_timer.GetComponent<Renderer>().enabled = false;
                FPC.GetComponent<FPCOp>().Dead();
                attacking = false;
                attack_timer = 12.0f;
            }
        }
        else
        {
            //death_timer.GetComponent<Renderer>().enabled = false;
        }
        if (health == 0)
        {
            death_timer.GetComponent<Renderer>().enabled = false;
            attacking = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            if (respawn_timer > 0)
            {
                //Debug.Log(respawn_timer);
                respawn_timer -= Time.deltaTime;
            }
            else
            {
                Restart();
            }
        }
    }

    void Restart()
    {
        health = 4.0f;
        respawn_timer = 120.0f;
        transform.position = initial_pos;
        attacking = false;
        gameObject.SetActive(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            try
            {
                transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = true; 
                for (int j = 0; j <  transform.GetChild(i).gameObject.transform.childCount; j++)
                {
                    transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.SetActive(true);
                }
            }
            catch
            {
            }
        }
        Debug.Log("Respawn");
    }
}
