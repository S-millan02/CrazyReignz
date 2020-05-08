using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBloom : MonoBehaviour
{
    public float velocidad = 1;
    public Color col;

    private Material mat;
    private float valor = 1;
    private bool volver = false;

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        if (valor <= 1)
        {
            volver = true;
        }
        if (valor >= 2.5f)
        {
            volver = false;
        }

        if (volver)
        {
            valor += velocidad;
        }
        else
        {
            valor -= velocidad;
        }
        mat.SetColor("_EmissionColor", col * valor);
    }
}