using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.Network;

public class ButtonHandler : MonoBehaviour
{
    public GameObject stopButton, startButton;
    public void StartSkenario()
    {
        Debug.Log("Start skenario");
        _ = WargamingAPI.GetSkenarioAktif();
        _ = WargamingAPI.GetAllCB();
        _ = TimeController.instance.Init();
        stopButton.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void StopSkenario()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        if (TimeController.instance.isPlaying)
        {
            TimeController.instance.isPlaying = false;
            ColyseusController.instance.SetMedia(new Dictionary<string, object> {
                ["media"] = "pause"
            });
        }
        else
        {
            TimeController.instance.isPlaying = true;
            ColyseusController.instance.SetMedia(new Dictionary<string, object>
            {
                ["media"] = "play"
            });
        }
    }
}
