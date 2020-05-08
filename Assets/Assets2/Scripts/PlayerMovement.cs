using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    //Joystick Detection
    protected Joybutton joybutton;

    //Movement
    public CharacterController ControllerPlayer;
    public float Speed = 5;
    public Animator anim;

    void Start()
    {
        //Find Joysticks
        joybutton = FindObjectOfType<Joybutton>();

        //Get Components
        ControllerPlayer = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Movement with the joystick
        Vector3 mov = new Vector3(SimpleInput.GetAxis("Horizontal") * Speed, 0, SimpleInput.GetAxis("Vertical") * Speed);
        ControllerPlayer.Move(mov * Time.deltaTime);

        float moveVertical = SimpleInput.GetAxis("Vertical");
        float moveHorizontal = SimpleInput.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * Speed * Time.deltaTime, Space.World);

        anim.SetInteger("Condition", 1);
    }
}
