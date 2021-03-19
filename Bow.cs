﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject m_ArrowPrefab = null;

    [Header("Bow")]
    public float m_GrabThreshold = 0.15f;
    public Transform m_Start = null;
    public Transform m_End = null;
    public Transform m_Socket = null;

    private Transform m_PullingHand = null;
    private Arrow m_CurrentArrow = null;
    private Animator m_Animator = null;

    private List<Arrow> arrows;
    public int arrow_num;

    private float m_PullValue = 0.0f;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow(0.0f));
        arrow_num = 0;
        arrows = new List<Arrow>();
    }

    private void Update()
    {
        if (!m_PullingHand || !m_CurrentArrow)
            return;
        
        m_PullValue = CalculatePull(m_PullingHand);
        m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f);
        
        m_Animator.SetFloat("Blend", m_PullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = m_End.position - m_Start.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - m_Start.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        // Wait
        yield return new WaitForSeconds(waitTime);

        // Create, child
        GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket);

        // Orient
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;
        
        // Set
        m_CurrentArrow = arrowObject.GetComponent<Arrow>();
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, m_Start.position);

        if (distance > m_GrabThreshold)
            return;
        
        m_PullingHand = hand;
    }

    public void Release()
    {
        if (m_PullValue > 0.25f) // Change number to make mplayer have to pull arrow back further
            FireArrow();
        
        m_PullingHand = null;

        m_PullValue = 0.0f; 
        m_Animator.SetFloat("Blend", 0);

        if (!m_CurrentArrow)
            StartCoroutine(CreateArrow(0.25f)); //Change value to see what is best
    }

    private void FireArrow()
    {
        arrows.Add(m_CurrentArrow);
        arrow_num++;
        //if (arrow_num > 5)
        //{
        //    arrows[arrow_num - 6].Delete();
        //}
        m_CurrentArrow.Fire(m_PullValue);
        m_CurrentArrow = null;
    }
}
