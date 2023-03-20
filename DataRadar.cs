using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperPlotting;

public class DataRadar : MonoBehaviour
{
    public string id_entity;
    public string id_user;
    public EntityRadarInfo infoRadar;
    public EntityRadar entityRadar;
    public string type;
    public List<Dictionary<string, object>> listDetectRadar = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> listActvRadar = new List<Dictionary<string, object>>();
}
