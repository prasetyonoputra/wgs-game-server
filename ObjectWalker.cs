using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWalker : MonoBehaviour
{
    private DataSatuan entityData;
    private DataMisi misiData;
    private int waypointIndex = 0;
    private DateTime waktuMulaiMisi;
    private DateTimeOffset waktuMulai;
    private Vector2 posisiSekarang, posisiTujuan, posisiSebelum;
    private double heading, distance;
    public int statusMisi = 0, _statusMisi = 0;
    private string idMisi;

    void Update()
    {
        entityData = this.GetComponent<DataSatuan>();
        float step = entityData.speed * Time.deltaTime * TimeController.instance.percepatan;

        if (waypointIndex < entityData.jalurMisi.Count)
        {
            misiData = entityData.jalurMisi[waypointIndex].GetComponent<DataMisi>();
            waktuMulaiMisi = Convert.ToDateTime(misiData.tgl_mulai);
            waktuMulai = waktuMulaiMisi;

            if (waktuMulai.ToUnixTimeMilliseconds() <= TimeController.instance.getDateTimeOffset().ToUnixTimeMilliseconds())
            {
                if (TimeController.instance.isPlaying)
                {
                    if (transform.position != entityData.jalurMisi[waypointIndex].transform.position)
                    {
                        idMisi = misiData.id_mission;
                        posisiSekarang = transform.position;
                        posisiTujuan = misiData.transform.position;
                        statusMisi = 1;

                        heading = FindAngle(posisiSekarang.y, posisiSekarang.x, posisiTujuan.y, posisiTujuan.x);
                        distance = DistanceTo(posisiSebelum.y, posisiSebelum.x, posisiSekarang.y, posisiSekarang.x, 'K') / 1000;
                        posisiSebelum = posisiSekarang;

                        transform.position = Vector2.MoveTowards(posisiSekarang, entityData.jalurMisi[waypointIndex].transform.position, step);
                        ColyseusController.instance.SendPosition(entityData.id_entity, transform.position, heading, distance);
                    }
                    else
                    {
                        entityData.jalurMisi.RemoveAt(waypointIndex);

                        int index = entityData.jalurMisi.FindIndex(x => x.GetComponent<DataMisi>().id_mission == idMisi);
                        if (index == -1)
                        {
                            Debug.Log("Misi selesai");
                            if (misiData.jenis == "ranjauPergerakan")
                            {
                                ColyseusController.instance.SendMisiRanjau(entityData.id_entity, misiData.missionDefault);
                            }
                            else if (misiData.jenis == "embarkasi")
                            {
                                GameObject entityInduk = GameObject.Find(misiData.data_properties.id_tujuan);
                                DataSatuan entityIndukData = entityInduk.GetComponent<DataSatuan>();

                                GameObject entityEmbarkasi = GameObject.Find(misiData.id_object);
                                DataSatuan entityDataEmbarkasi = entityEmbarkasi.GetComponent<DataSatuan>();
                                entityDataEmbarkasi.opacity = 0;

                                if (entityIndukData.listEmbarkasi.Find(x => x == misiData.id_object) == null)
                                {
                                    entityIndukData.listEmbarkasi.Add(misiData.id_object);
                                }

                                ColyseusController.instance.SetOpacityObject(misiData.id_object, entityDataEmbarkasi.opacity);
                                ColyseusController.instance.SendListEmbarkasi(misiData.data_properties.id_tujuan, entityIndukData.listEmbarkasi);

                                Debug.Log(entityEmbarkasi.name + ": melakukan embarkasi ke " + entityInduk.name);
                            }
                            else if (misiData.jenis == "debarkasi")
                            {
                                GameObject entityDebarkasi = GameObject.Find(misiData.id_object);
                                DataSatuan entityData = entityDebarkasi.GetComponent<DataSatuan>();

                                if (misiData.data_properties.tools == "lifeboat")
                                {
                                    ColyseusController.instance.SetIconNow(misiData.id_object, "taktis");
                                }

                                ColyseusController.instance.SetOpacityObject(misiData.id_object, entityData.opacity);
                            }

                            Destroy(GameObject.Find(idMisi));
                            statusMisi = 2;
                        }

                        waypointIndex = 0;
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, transform.position, step);
                }
            }
        }

        if (statusMisi != _statusMisi && statusMisi != 0)
        {
            if (statusMisi == 1)
            {
                if (misiData.jenis == "debarkasi")
                {
                    GameObject entityInduk = GameObject.Find(misiData.data_properties.idDebarkasi);
                    DataSatuan entityIndukData = entityInduk.GetComponent<DataSatuan>();
                    entityIndukData.listEmbarkasi.Remove(misiData.id_object);

                    GameObject entityDebarkasi = GameObject.Find(misiData.id_object);
                    DataSatuan entityDataDebarkasi = entityDebarkasi.GetComponent<DataSatuan>();
                    entityDebarkasi.transform.position = entityInduk.transform.position;

                    if (misiData.data_properties.tools == "lifeboat")
                    {
                        ColyseusController.instance.SetIconNow(misiData.id_object, "lifeboat");
                    }
                    else
                    {
                        ColyseusController.instance.SetIconNow(misiData.id_object, "taktis");
                    }

                    entityDataDebarkasi.opacity = 1;

                    ColyseusController.instance.SetOpacityObject(misiData.id_object, entityDataDebarkasi.opacity);
                    ColyseusController.instance.SendListEmbarkasi(misiData.data_properties.idDebarkasi, entityIndukData.listEmbarkasi);

                    Debug.Log(entityDebarkasi.name + ": melakukan debarkasi dari " + entityInduk.name);
                }

                ColyseusController.instance.SetStatusMisi(idMisi, statusMisi, entityData.id_entity);
                Debug.Log(entityData.id_entity + ": bergerak!");
            }
            else if (statusMisi == 2)
            {
                ColyseusController.instance.SetStatusMisi(idMisi, statusMisi, entityData.id_entity);
                Debug.Log(entityData.id_entity + ": berhenti!");
                statusMisi = 0;
            }

            _statusMisi = statusMisi;
        }
    }

    static double FindAngle(double x1, double y1, double x2, double y2)
    {
        double dx = x2 - x1;
        double dy = y2 - y1;
        return Math.Atan2(dy, dx) * (180 / Math.PI);
    }

    static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        switch (unit)
        {
            case 'K': //Kilometers -> default
                return dist * 1.609344;
            case 'N': //Nautical Miles 
                return dist * 0.8684;
            case 'M': //Miles
                return dist;
        }

        return dist;
    }
}
