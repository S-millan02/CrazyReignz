using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    //Joystick Detection
    protected Joystick joystick;
    protected Joybutton joybutton;

    //Movement Speed
    public float Speed =5;

    void Start()
    {

        //Find Joysticks
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }

    void Update()
    {
        //Movement with the joystick
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * Speed + Input.GetAxis("Horizontal") * Speed,
                                         rigidbody.velocity.y,
                                         joystick.Vertical * Speed + Input.GetAxis("Vertical") * Speed);
    }
}
