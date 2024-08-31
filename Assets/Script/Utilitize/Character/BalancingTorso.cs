using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingTorso : MonoBehaviour
{
    public Rigidbody2D _RB2D;
    public float TorsoRotation;
    public float TorsoSensitivity;
    public float WeightSensitivity;
    public float ControlSensitivity;
    public Transform TorsoEmber;
    public Transform TorsoVisualCharacter;
    public Transform TorsoEmberAnchor;
    public float KecepatanBucket;

    public float MaxLean;
    public float MaxLeanCharacter;

    public float CurrentTorsoRotation;

    private void Start()
    {
        _RB2D = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    { 
        var b = Input.GetAxis("Mouse X");
        TorsoRotation -= b * ControlSensitivity;

        if (TorsoRotation >= MaxLean)
        {
            TorsoRotation = MaxLean;
        }
        else if (TorsoRotation <= -MaxLean)
        {
            TorsoRotation = -MaxLean;
        }
    }

    public void Update()
    {
        CurrentTorsoRotation = TorsoEmber.localRotation.z;
        if (TorsoRotation <= MaxLean && TorsoRotation >= -MaxLean)
        {
            TorsoEmberAnchor.transform.rotation = Quaternion.Euler(0, 0, TorsoRotation);
            TorsoEmber.transform.rotation = Quaternion.RotateTowards(TorsoEmber.transform.rotation, TorsoEmberAnchor.transform.rotation, KecepatanBucket * Time.deltaTime);
        }


        if(TorsoRotation <= MaxLeanCharacter && TorsoRotation >= -MaxLeanCharacter)
        {
            TorsoVisualCharacter.transform.rotation = Quaternion.Euler(0, 0, TorsoRotation);
        }
        else if (TorsoRotation <= -MaxLeanCharacter)
        {
            TorsoVisualCharacter.transform.rotation = Quaternion.Euler(0, 0, -MaxLeanCharacter);
        }
        else if (TorsoRotation >= MaxLeanCharacter)
        {
            TorsoVisualCharacter.transform.rotation = Quaternion.Euler(0, 0, MaxLeanCharacter);
        }

        if (TorsoRotation <= MaxLean && TorsoRotation >= -MaxLean)
        {
            if (TorsoRotation >= 0)
            {
                TorsoRotation += 1 * WeightSensitivity;
            }
            else
            {

                TorsoRotation -= 1 * WeightSensitivity;
            }

            TorsoRotation += _RB2D.velocity.x * TorsoSensitivity;
        }
    }


}
