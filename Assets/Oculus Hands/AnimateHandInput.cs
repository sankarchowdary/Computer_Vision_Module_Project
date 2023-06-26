using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandInput : MonoBehaviour
{
    public InputActionProperty PinchAnim;
    public InputActionProperty gripAnim;
    public Animator handAnim;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = PinchAnim.action.ReadValue<float>();
        handAnim.SetFloat("Trigger", triggerValue);

        float gripvalue = gripAnim.action.ReadValue<float>();
        handAnim.SetFloat("Grip", gripvalue);
    }
}