using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingTorso : MonoBehaviour
{
    public float TorsoRotation;
    public float TorsoSensitifity;
    public Transform TorsoVisual;

    public float MaxLean;

    private float RandomTorsoToward;
    private float TorsoControl;

    private void FixedUpdate()
    {
        TorsoRotation = TorsoControl + RandomTorsoToward;
        if (TorsoRotation <= MaxLean && TorsoRotation >= -MaxLean)
        {
            TorsoVisual.transform.rotation = Quaternion.Euler(0, 0, TorsoRotation);
            TorsoRotation = 0;
        }
        else
        {
            TorsoVisual.transform.rotation = new Quaternion(0, 0, 0, 0);
            TorsoRotation = 0;
        }
    }

    public void Update()
    {
        var b = Input.GetAxisRaw("Mouse X");
        TorsoControl -= b * TorsoSensitifity;

    }


}
