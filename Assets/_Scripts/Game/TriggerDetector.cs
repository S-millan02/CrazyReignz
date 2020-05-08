using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public JugadorController jController;
    public EnemigoController eController;

    private void OnTriggerEnter(Collider other)
    {
        if (jController != null)
        {
            jController.choques.Add(other);
        }
        if (eController != null)
        {
            eController.choques.Add(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (jController != null)
        {
            jController.tocando = true;
        }
        if (eController != null)
        {
            eController.tocando = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (jController != null)
        {
            jController.tocando = false;
            jController.choques.Remove(other);
        }
        if (eController != null)
        {
            eController.tocando = false;
            eController.choques.Remove(other);
        }
    }
}