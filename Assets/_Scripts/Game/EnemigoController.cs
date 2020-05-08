using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemigoController : MonoBehaviour
{
    [Header("Valores")]
    public float fuerzaGolpe = 4;
    public float vida = 100;
    public float valorGolpe = 4;
    private Material mat;
    private Color col;
    private float colV;
    [Space]
    public Transform Jugador;
    private NavMeshAgent agent;
    private Animator anim;

    public bool tocando;
    private bool esperaGolpe;
    private float esperaF;
    public List<Collider> choques;

    private Rigidbody rb;
    private AimConstraint aim;
    private MeshRenderer mr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mat = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        aim = GetComponent<AimConstraint>();
        mr = GetComponent<MeshRenderer>();
        col = new Color(1, 1, 1, 1);
        colV = 1;
        esperaF = 1;
    }

    private void Update()
    {
        if (enabled)
        {
            if (vida <= 0)
            {
                Debug.Log("Muerte " + name);
                //gameObject.SetActive(false);
                agent.enabled = false;
                mat.SetColor("_BaseColor", Color.black);
                //rb.detectCollisions = false;
                //rb.useGravity = false;
                //rb.isKinematic = true;
                aim.enabled = false;
                //mr.enabled = false;
                gameObject.name = gameObject.name + "_Dead";
                enabled = false;
            }
            else
            {
                agent.SetDestination(Jugador.position);
            }


            if (tocando)
            {
                if (!esperaGolpe)
                {
                    esperaGolpe = true;
                }
            }
            else
            {
                if (esperaGolpe)
                {
                    esperaGolpe = false;
                    //anim.SetBool("golpeando", false);
                }
            }

            if (esperaGolpe)
            {
                esperaF -= Time.unscaledDeltaTime;
            }
            else
            {
                esperaF = 1;
            }
            if (esperaF < 0)
            {
                anim.SetBool("golpeando", true);
                for (int i = 0; i < choques.Count; i++)
                {
                    choques[i].GetComponentInParent<JugadorController>().CambiarVida(-valorGolpe);
                    choques[i].GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaGolpe, ForceMode.Impulse);
                }
                esperaF = 1;
            }
            else if (esperaF < 0.9f)
            {
                anim.SetBool("golpeando", false);
            }
        }
    }

    public void CambiarVida(float val)
    {
        if (enabled)
        {
            vida += val;
            colV += ((1 / vida) * val);
            colV = Mathf.Clamp(colV, 0, 1);
            col.g = colV;
            col.b = colV;
            mat.SetColor("_BaseColor", col);
        }
    }
}