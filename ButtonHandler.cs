using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
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
