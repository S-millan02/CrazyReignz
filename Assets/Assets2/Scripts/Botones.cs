﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
	public void BotonPlay()
	{
		SceneManager.LoadScene("Roulette");
	}
}
