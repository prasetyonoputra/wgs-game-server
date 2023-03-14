using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.Network;

public class StartAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _ = WargamingAPI.GetSkenarioAktif();
        _ = WargamingAPI.GetAllCB();
        _ = TimeController.instance.Init();
    }
}
