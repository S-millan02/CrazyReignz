using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    private string gameId = "3615069";
#elif UNITY_ANDROID
    private string gameId = "3615068";
#endif

    public Button myButton;
    public string myPlacementId = "rewardedVideo";
    public GiroController gController;

    private void Awake()
    {
        myButton.gameObject.SetActive(false);
    }

    public void CargarBoton()
    {
        myButton.interactable = Advertisement.IsReady(myPlacementId);
        myButton.gameObject.SetActive(true);
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    // Implement a function for showing a rewarded video ad:
    public void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)           
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("Ha ganado la recompensa");
            gController.vioVideo = true;
            gController.Girar();
            gController.bPrincipal.interactable = false;
            myButton.gameObject.SetActive(false);
        }

        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("Omitió el video, no hay recompensa.");
        }

        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}