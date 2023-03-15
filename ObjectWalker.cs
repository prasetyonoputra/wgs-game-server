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
    Vector2 posisiSekarang, posisiTujuan;
    double heading;

    void Update()
    {
        entityData = this.GetComponent<DataSatuan>();
        float step = entityData.speed * Time.deltaTime * TimeController.instance.percepatan;

        try
        {
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
                            posisiSekarang = this.transform.position;
                            posisiTujuan = entityData.jalurMisi[waypointIndex].transform.position;

                            heading = FindAngle(posisiSekarang.y, posisiSekarang.x, posisiTujuan.y, posisiTujuan.x);

                            transform.position = Vector2.MoveTowards(transform.position, entityData.jalurMisi[waypointIndex].transform.position, step);
                            ColyseusController.instance.SendPosition(entityData.id_entity, transform.position, heading);
                        }
                        else
                        {
                            waypointIndex++;
                        }
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, transform.position, step);
                    }
                }
            }
        }        
        catch (Exception err)
        {
            Debug.LogWarning(err.ToString());
        }
    }

    static double FindAngle(double x1, double y1, double x2, double y2)
    {
        double dx = x2 - x1;
        double dy = y2 - y1;
        return Math.Atan2(dy, dx) * (180 / Math.PI);
    }
}
