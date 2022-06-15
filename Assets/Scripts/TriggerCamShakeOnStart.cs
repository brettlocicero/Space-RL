using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamShakeOnStart : MonoBehaviour
{
    [SerializeField] float camShakeAmt;
    [SerializeField] float camShakeTime;
    [SerializeField] float priority;

    void Start()
    {
        CinemachineShake.instance.ShakeCamera(camShakeAmt, camShakeTime, priority);
    }
}
