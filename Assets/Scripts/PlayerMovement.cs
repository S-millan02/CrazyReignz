using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    protected Joystick joystick;
    protected Joybutton joybutton;

    public float Speed =5;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * Speed + Input.GetAxis("Horizontal") * Speed,
                                         rigidbody.velocity.y,
                                         joystick.Vertical * Speed + Input.GetAxis("Vertical") * Speed);
    }
}
