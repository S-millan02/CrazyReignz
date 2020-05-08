using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiroController : MonoBehaviour
{
    public RectTransform circulo;
    public float minVal;
    public float maxVal;
    public AnimationCurve curve;
    private float valorG;
    public float velocidadBajada;
    private bool iniciar;
    private float tiempo;
    private float a;
    public TextMeshProUGUI finalPower;
    [Space]
    public int aF;
    public float vl;

    private void Awake()
    {
        finalPower.text = "Gira la ruleta para comenzar";
    }

    public void Girar()
    {
        finalPower.text = "...";
        circulo.localEulerAngles = new Vector3(0, 0, 0);
        tiempo = 0.1f;
        valorG = Random.Range(minVal, maxVal);
        iniciar = true;
    }

    private void Update()
    {
        if (iniciar)
        {
            if (valorG >= 0)
            {
                tiempo -= velocidadBajada * Time.unscaledDeltaTime;
                a = Mathf.Lerp(0, valorG, curve.Evaluate(tiempo));
                circulo.Rotate(0, 0, a);
            }

            if (circulo.localEulerAngles.z >= 36 && circulo.localEulerAngles.z <= 108)
            {
                // 72
                aF = 2;
            }
            else if (circulo.localEulerAngles.z >= 108 && circulo.localEulerAngles.z <= 180)
            {
                // 144
                aF = 3;
            }
            else if (circulo.localEulerAngles.z >= 180 && circulo.localEulerAngles.z <= 252)
            {
                // 216
                aF = 4;
            }
            else if (circulo.localEulerAngles.z >= 252 && circulo.localEulerAngles.z <= 324)
            {
                // 288
                aF = 5;
            }
            else
            {
                // 0
                aF = 1;
            }

            if (a <= 0)
            {
                iniciar = false;
                StartCoroutine(Finalizar());
            }
        }
    }

    IEnumerator Finalizar()
    {
        switch (aF)
        {
            case 1:
                finalPower.text = "Poder inicial: x2 Velocidad";
                break;
            case 2:

                finalPower.text = "Poder inicial: x2 Daño";
                break;
            case 3:

                finalPower.text = "Poder inicial: x2 Empuje";
                break;
            case 4:
                finalPower.text = "Poder inicial: x2 Vida";
                break;
            default:
                finalPower.text = "Ningun poder inicial";
                break;
        }
        yield return new WaitForSecondsRealtime(3);
        JugadorController.poderInicial = aF;
        Initiate.Fade("Game", Color.black, 1);
    }
}