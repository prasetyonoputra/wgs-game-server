using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;
using Wargaming.Core.GlobalParam.HelperPlotting;

public class DataSatuan : MonoBehaviour
{
    public enum JenisSatuan { VEHICLE, SHIP, AIRCRAFT, INFANTRY }
    public string id_entity;
    public string id_user;
    public JenisSatuan jenis;
    public Detector detector;
    public List<GameObject> jalurMisi = new List<GameObject>();
    public float speed;
    public List<string> ecm_activated = new List<string>();
    public List<string> listEmbarkasi = new List<string>();
    public string iconNow;
    public float armor;
    public int opacity;
    public bool kebalRanjau;
    public List<Dictionary<string, object>> listDetectRadar = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> listActvRadar = new List<Dictionary<string, object>>();
    public EntitySatuanInfo infoSatuan;
}
