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
        }
        else
        {
            TimeController.instance.isPlaying = true;
        }
    }
}
