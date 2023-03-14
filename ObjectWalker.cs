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

    public void Start()
    {
        entityData = this.GetComponent<DataSatuan>();
    }

    void Update()
    {
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
                    if (TimeController.instance.isPlay)
                    {
                        ColyseusController.instance.SendPosition(entityData.id_entity, transform.position, 0);
                        if (transform.position != entityData.jalurMisi[waypointIndex].transform.position)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, entityData.jalurMisi[waypointIndex].transform.position, step);
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

    static double DegreeBearing(
        double lat1, double lon1,
        double lat2, double lon2)
    {
        var dLon = ToRad(lon2 - lon1);
        var dPhi = Math.Log(
            Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
        if (Math.Abs(dLon) > Math.PI)
            dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
        return ToBearing(Math.Atan2(dLon, dPhi));
    }

    public static double ToRad(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    public static double ToDegrees(double radians)
    {
        return radians * 180 / Math.PI;
    }

    public static double ToBearing(double radians)
    {
        // convert radians to degrees (as bearing: 0...360)
        return (ToDegrees(radians) + 360) % 360;
    }
}
