using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;
using Wargaming.Core.GlobalParam.HelperPlotting;
using Wargaming.Core.Network;

public class EntityController : MonoBehaviour
{
    public static EntityController instance;

    [Header("Raw Prefab For object")]
    public GameObject prefabEntity;
    public GameObject prefabMisi;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task LoadEntityFromCB(long? id_user, long? id_kogas, long? id_scenario, string nama_document, int playerIndex)
    {
        // Load Data CB dari Database
        var plotting = await WargamingAPI.loadDataCB(id_user, id_kogas, id_scenario, nama_document);

        // Spawn Entity Satuan
        await LoadEntitySatuan(JArrayExtended.setJArrayResult(plotting, 0));

        LoadMisiSatuan(JArrayExtended.setJArrayResult(plotting, 13));
    }

    public async Task<bool> LoadEntitySatuan(JArray data)
    {
        if (!JArrayExtended.checkingJArrayData(data)) return false;

        for (int i = 0; i < data.Count; i++)
        {
            //Debug.Log("Spawn Satuan " + i);
            //Debug.Log(data[i].ToString());

            var satuan = EntitySatuan.FromJson(data[i]);
            await SpawnEntitySatuan(satuan, data[i].ToString());
        }

        return true;
    }

    public bool LoadMisiSatuan(JArray data)
    {
        if (!JArrayExtended.checkingJArrayData(data)) return false;

        for (int i = 0; i < data.Count; i++)
        {
            //Debug.Log("Spawn Misi " + i);
            //Debug.Log(data[i].ToString());

            var mission = MisiSatuan.FromJson(data[i].ToString());
            SpawnMisiSatuan(mission);
        }

        return true;
    }

    public async Task SpawnEntitySatuan(EntitySatuan satuan, string satuanDefault)
    {
        GameObject entity = Instantiate(prefabEntity);
        entity.name = satuan.id_satuan;
        entity.transform.position = new Vector2(satuan.lng * 1000, satuan.lat * 1000);

        DataSatuan entityData = entity.AddComponent<DataSatuan>();
        entityData.speed = (float)satuan.data_info.kecepatan_satuan;
        entityData.id_entity = satuan.id_satuan;

        satuan.detector = new Detector(satuan.radar, satuan.sonar);
        entity.GetComponent<DataSatuan>().detector = satuan.detector;

        await ColyseusController.instance.CreateSatuan(new Dictionary<string, object>
        {
            ["id_object"] = satuan.id_satuan,
            ["lat"] = satuan.lat,
            ["lng"] = satuan.lng,
            ["speed"] = satuan.data_info.kecepatan_satuan.ToString() + "|" + satuan.data_info.jenis_kecepatan,
            ["heading"] = satuan.heading,
            ["armor"] = satuan.data_info.armor,
            ["weapon"] = satuan.data_info.weapon,
            ["jarakTempuh"] = 0, // Untuk sementara
            ["bensin"] = Int64.Parse(satuan.data_info.bahan_bakar),
            ["defaultData"] = satuanDefault,
            ["info"] = EntitySatuanInfo.ToString(satuan.data_info),
            ["type"] = "satuan",
            ["tipe_tni"] = satuan.tipe_tni,
            //dataEntity["senjataUtama"] = null;
            ["bahan_bakar"] = Int64.Parse(satuan.data_info.bahan_bakar),
            ["fuel_load"] = Int64.Parse(satuan.data_info.bahan_bakar_load),
            ["personil"] = satuan.data_info.personil,
            ["style"] = EntitySatuanStyle.ToString(satuan.data_style),
            ["nama"] = satuan.id_satuan,
            ["altitude"] = 0,
            ["alutsista"] = satuan.alutsista,
            ["allsenjata"] = JsonConvert.SerializeObject(satuan.allSenjata),
            //dataEntity["posisiJalur"] = 0;
            ["detector"] = JsonConvert.SerializeObject(satuan.detector),
            ["iconNow"] = "gambar",
            //dataEntity["listRealShape"] = 0;
        });
    }

    private void SpawnMisiSatuan(MisiSatuan mission)
    {
        List<JalurMisi> jalur = mission.data_properties.jalur;

        try
        {
            GameObject targetObject = GameObject.Find(mission.id_object);
            DataSatuan dataTarget = targetObject.GetComponent<DataSatuan>();

            foreach (JalurMisi setiapJalur in jalur)
            {
                Vector3 jalurPosition = new Vector2(setiapJalur.lng * 1000, setiapJalur.lat * 1000);

                GameObject objectjalur = Instantiate(prefabMisi, jalurPosition, Quaternion.identity);
                objectjalur.name = mission.id;

                DataMisi dataMisi = objectjalur.AddComponent<DataMisi>();
                dataMisi.id = mission.id;
                dataMisi.id_mission = mission.id_mission;
                dataMisi.id_object = mission.id_object;
                dataMisi.tgl_mulai = mission.tgl_mulai;
                dataMisi.jenis = mission.jenis;
                dataMisi.data_properties = mission.data_properties;

                dataTarget.jalurMisi.Add(objectjalur);
            }

            targetObject.AddComponent<ObjectWalker>();
        }
        catch (Exception err)
        {
            Debug.LogWarning(err.ToString());
        }
    }
}
