using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorController : MonoBehaviour
{
    [Header("Valores")]
    public float fuerzaGolpe = 4;
    public float vida = 100;
    public float valorGolpe = 5;
    public float poderDash;
    public float duracionPowerUp = 5;
    [Header("UI")]
    public Slider sVida;
    public Gradient sGradient;
    public Image sImage;
    public GameController gController;
    public float velocidadDashSlider;
    public Slider dashSlider;
    [Space]
    private Rigidbody rb;
    private Animator anim;
    public float velocidad = 1;
    private Vector3 move;
    public Animator anim2;

    public bool tocando;
    private bool undidoBtn;
    public List<Collider> choques;

    public static int poderInicial;
    private float multVelocidad = 1;
    private float multGolpe = 1;
    private float multFuerza = 1;
    private float multVida = 1;

    private float val = 10;

    private void Awake()
    {
        dashSlider.value = 0;

        switch (poderInicial)
        {
            case 1:
                multVelocidad = 2;
                Debug.Log("Poder inicial: x2 Velocidad");
                break;
            case 2:
                multGolpe = 2;
                Debug.Log("Poder inicial: x2 Daño");
                break;
            case 3:
                multFuerza = 2;
                Debug.Log("Poder inicial: x2 Empuje");
                break;
            case 4:
                multVida = 2;
                Debug.Log("Poder inicial: x2 Vida");
                break;
            default:
                Debug.Log("Ningun poder inicial");
                break;
        }

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        vida *= multVida;
        sVida.maxValue = vida;
        sVida.value = vida;
        sImage.color = sGradient.Evaluate(1);
    }

    public void Golpe(bool on)
    {
        anim.SetBool("golpeando", on);
        undidoBtn = on;
        anim2.SetBool("Punch", on);
    }

    public void Dash()
    {
        if (dashSlider.value == 1)
        {
            dashSlider.value = 0;
            rb.AddForce(transform.forward * poderDash, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (enabled)
        {
            #region bug :v
            if (val >= 0)
            {
                val -= 1;
            }
            if (val > 0)
            {
                Golpe(false);
            }
            #endregion

            if (vida <= 0)
            {
                Debug.LogWarning("Perdiste");
                gController.ApagarJoystick();
                enabled = false;
            }

            if (dashSlider.value < 1)
            {
                dashSlider.value += velocidadDashSlider * Time.unscaledDeltaTime;
            }

            move = new Vector3(SimpleInput.GetAxis("Horizontal") * (velocidad * multVelocidad) * Time.unscaledDeltaTime, 0, SimpleInput.GetAxis("Vertical") * (velocidad * multVelocidad) * Time.unscaledDeltaTime);

            rb.AddForce(move, ForceMode.VelocityChange);
            if (SimpleInput.GetAxis("Horizontal") != 0 || SimpleInput.GetAxis("Vertical") != 0)
            {
                transform.forward = new Vector3(SimpleInput.GetAxis("Horizontal"), 0, SimpleInput.GetAxis("Vertical"));
            }

            if (SimpleInput.GetAxis("Horizontal") != 0 || SimpleInput.GetAxis("Vertical") != 0)
            {
                anim2.SetInteger("Condition", 1);
            }
            else
            {
                anim2.SetInteger("Condition", 0);
            }

            if (undidoBtn && tocando)
            {
                for (int i = 0; i < choques.Count; i++)
                {
                    choques[i].GetComponentInParent<EnemigoController>().CambiarVida(-(valorGolpe * multGolpe));
                    choques[i].GetComponent<Rigidbody>().AddForce(transform.forward * (fuerzaGolpe * multFuerza), ForceMode.Impulse);
                }
            }
        }
    }

    public void CambiarVida(float val)
    {
        if (enabled)
        {
            vida += val;
            sVida.value = vida;
            sImage.color = sGradient.Evaluate(vida / sVida.maxValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            ActivarPowerup aP = other.GetComponent<ActivarPowerup>();
            switch (aP.powerUp)
            {
                case ActivarPowerup.power.velocidad:
                    StartCoroutine(PowerUpTiempo(1));
                    break;
                case ActivarPowerup.power.fuerza:
                    StartCoroutine(PowerUpTiempo(2));
                    break;
                case ActivarPowerup.power.vida:
                    StartCoroutine(PowerUpTiempo(3));
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerUpTiempo(int val)
    {
        switch (val)
        {
            case 1:
                float valAnterior1 = multVelocidad;
                multVelocidad *= 2;
                yield return new WaitForSecondsRealtime(duracionPowerUp);
                multVelocidad = valAnterior1;
                break;
            case 2:
                float valAnterior2 = multFuerza;
                multFuerza *= 4;
                yield return new WaitForSecondsRealtime(duracionPowerUp);
                multFuerza = valAnterior2;
                break;
            case 3:
                CambiarVida(Mathf.Clamp(vida / 4, 0, vida));
                yield return null;
                break;
            default:
                break;
        }
    }
}