using UnityEngine;
using Wargaming.Core.GlobalParam.HelperPlotting;

public class DataObstacle : MonoBehaviour
{
    public string dokumen { get; set; }
    public string id { get; set; }
    public string id_user { get; set; }
    public InfoObstacle infoObstacle { get; set; }
    public float lat { get; set; }
    public float lng { get; set; }
    public string nama { get; set; }
    public string symbol { get; set; }
    public bool isDestroy { get; set; }
}
