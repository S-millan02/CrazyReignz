using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiningManager : MonoBehaviour {

	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;

	public Text winText;
	public int section;
	float totalAngle;
	public string[] PrizeName;

	// Use this for initialization
	private void Start () {
		isCoroutine = true;
		totalAngle = 360 / section;
	}

	// Update is called once per frame
	private void Update () {

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isCoroutine)
			StartCoroutine(Spin());
	}

	private IEnumerator Spin(){

		isCoroutine = false;
		randVal = Random.Range (200, 300);

		timeInterval = 0.0001f*Time.deltaTime*2;

		for (int i = 0; i < randVal; i++) {

			transform.Rotate (0, 0, (totalAngle/2)); //Start Rotate 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt (randVal * 0.2f))
				timeInterval = 0.1f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.5f))
				timeInterval = 0.2f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.7f))
				timeInterval = 0.3f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.8f))
				timeInterval = 0.4f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.9f))
				timeInterval = 0.5f*Time.deltaTime;

			yield return new WaitForSeconds (timeInterval);

		}

		switch (finalAngle)
		{
			case 0:
					Debug.Log("1");
				break;

			case 1:
				Debug.Log("2");
				break;

			case 2:
				Debug.Log("3");
				break;

			case 3:
				Debug.Log("4");
				break;

			case 4:
				Debug.Log("5");
				break;

			case 5:
				Debug.Log("6");
				break;

			case 6:
				Debug.Log("7");
				break;

			case 7:
				Debug.Log("8");
				break;
		}

		if (Mathf.RoundToInt (transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate (0, 0, totalAngle/2);
		
		finalAngle = Mathf.RoundToInt (transform.eulerAngles.z);//round off euler angle of wheel value

		//Prize check
		for (int i = 0; i < section; i++) {

			if (finalAngle == i * totalAngle)
				winText.text = PrizeName [i];
		}

	
		isCoroutine = true;
	}
}
