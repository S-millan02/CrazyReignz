using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void B_iniciar()
    {
        Initiate.Fade("PreGame", Color.black, 1);
    }

    public void B_Salir()
    {
        Application.Quit();
    }
}