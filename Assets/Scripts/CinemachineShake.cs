using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance;
    [SerializeField] CinemachineVirtualCamera vc;
    CinemachineBasicMultiChannelPerlin cbmcp;

    float shakeTimer;
    float shakeTimerTotal;
    float startingIntensity;
    float priority;

    void Awake ()
    {
        instance = this;
        cbmcp = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0;
    }

    public void ShakeCamera (float intensity, float time, float priorityRequest)
    {   
        if (priorityRequest >= priority)
        {
            cbmcp.m_AmplitudeGain = intensity;
            startingIntensity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;
            priority = priorityRequest;
        }
    }

    void Update ()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            cbmcp.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
        }

        else 
        {
            priority = 0f;
        }
    }
}