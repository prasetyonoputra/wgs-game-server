using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;

public class DataSatuan : MonoBehaviour
{
    public enum JenisSatuan { VEHICLE, SHIP, AIRCRAFT, INFANTRY }
    public enum TypeSatuan { DEFAULT, TRACKS, SURFACE, SUB, JET, PROPS, HELICOPTER }


    public string id_entity;

    [Header("Author")]
    public string id_user;

    [Header("Data")]
    public JenisSatuan jenis;
    public Detector detector;

    public string namaSatuan;
    public string noSatuan;

    public List<GameObject> jalurMisi = new List<GameObject>();

    public string descSatuan;
    public string fontTaktis;
    public string simbolTaktis;

    public int current;
    public float speed = 1f;
}
