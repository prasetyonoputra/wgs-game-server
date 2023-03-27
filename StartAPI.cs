using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.Network;

public class StartAPI : MonoBehaviour
{
    public GameObject stopButton;
    public void StartSkenario()
    {
        _ = WargamingAPI.GetSkenarioAktif();
        _ = WargamingAPI.GetAllCB();
        _ = TimeController.instance.Init();
        stopButton.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
