using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject cUI;
    public GameObject cControls;
    public GameObject cPausa;

    private bool end;

    private void Awake()
    {
        cUI.SetActive(true);
        cControls.SetActive(true);
        cPausa.SetActive(false);
    }

    public void Pausa()
    {
        cUI.SetActive(false);
        cControls.SetActive(false);
        cPausa.SetActive(true);
        Time.timeScale = 0;
    }
    public void ApagarJoystick()
    {
        cControls.SetActive(false);
        end = true;
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        cUI.SetActive(true);
        if (!end)
        {
            cControls.SetActive(true);
        }
        cPausa.SetActive(false);
    }
    public void VolverMenu()
    {
        Time.timeScale = 1;
        Initiate.Fade("Menu", Color.black, 1);
    }
}