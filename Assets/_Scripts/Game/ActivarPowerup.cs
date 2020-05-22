using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPowerup : MonoBehaviour
{
    public AudioClip pow;

    public enum power { velocidad, fuerza, vida }
    public power powerUp;
}