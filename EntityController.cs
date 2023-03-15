using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;
using Wargaming.Core.GlobalParam.HelperPlotting;
using Wargaming.Core.Network;
using static DataSatuan;

public class EntityController : MonoBehaviour
{
    public static EntityController instance;
    public static List<string> listSatuan = new List<string>();

    public GameObject prefabSatuan;
    public GameObject prefabMisi;
    public GameObject prefabRadar;
    public GameObject satuanContainer;
    public GameObject misiContainer;
    public GameObject radarContainer;

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
        Debug.Log("Mulai load entity");

        var plotting = await WargamingAPI.loadDataCB(id_user, id_kogas, id_scenario, nama_document);

        await LoadEntitySatuan(JArrayExtended.setJArrayResult(plotting, 0));
        LoadMisiSatuan(JArrayExtended.setJArrayResult(plotting, 13));
        await LoadEntityRadar(JArrayExtended.setJArrayResult(plotting, 7));

        Debug.Log("Selesai load entity");
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
        GameObject entitySatuan = Instantiate(prefabSatuan);
        entitySatuan.name = satuan.id_satuan;
        entitySatuan.transform.position = new Vector2(satuan.lng * 1000, satuan.lat * 1000);
        entitySatuan.transform.parent = satuanContainer.transform;

        if (satuan.data_style.grup == "10")
        {
            satuan.jenis = JenisSatuan.INFANTRY;
        }
        else
        {
            if (satuan.tipe_tni == null)
            {
                await SetDetailSatuan(satuan);
            }
        }

        satuan.detector = new Detector(satuan.radar, satuan.sonar);

        DataSatuan entityData = entitySatuan.AddComponent<DataSatuan>();
        entityData.speed = (float)satuan.data_info.kecepatan_satuan / 100;
        entityData.id_entity = satuan.id_satuan;
        entityData.detector = satuan.detector;
        entityData.armor = satuan.data_info.armor;
        entityData.opacity = 1;
        entityData.kebalRanjau = false;
        entityData.infoSatuan = satuan.data_info;

        GameObject radarChild = entitySatuan.transform.GetChild(1).gameObject;
        radarChild.GetComponent<CircleCollider2D>().radius = getRadiusRadarEntity("utama", satuan.tipe_tni, satuan.height) / 100;

        GameObject jarakPandangChild = entitySatuan.transform.GetChild(2).gameObject;
        jarakPandangChild.GetComponent<CircleCollider2D>().radius = getRadiusRadarEntity("jarakPandang", satuan.tipe_tni, satuan.height) / 100;

        listSatuan.Add(satuan.id_satuan);

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

    public int getRadiusRadarEntity(string status, string jenis, float height)
    {
        int radius = 0;
        switch (status)
        {
            case "utama":
                if (jenis == "laut")
                {
                    radius = 100000;
                }
                else if (jenis == "udara")
                {
                    radius = 50000;
                }
                else
                {
                    radius = 10000;
                }

                break;
            case "jarakPandang":
                float d = 0;
                float h;
                float r = 6371000;

                try
                {
                    if (jenis == "laut")
                    {
                        h = height;
                    }
                    else if (jenis == "udara")
                    {
                        h = height;
                    }
                    else if (jenis == "darat")
                    {
                        h = height;
                    }
                    else if (jenis == "submarine")
                    {
                        h = 0;
                    }
                    else
                    {
                        h = 2;
                    }
                    float tahap1 = (float)Math.Sqrt(h / (2 * (r + h)));
                    d = (2 * r) * tahap1;
                    radius = (int)d;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    return 0;
                }

                break;
            default:
                break;
        }

        return radius;
    }

    private async Task SetDetailSatuan(EntitySatuan satuan)
    {
        var data = await WargamingAPI.GetDetailSatuan(satuan.id_symbol, satuan.data_style.grup);
        if (data == null) return;

        switch (satuan.data_style.grup)
        {
            case "1":
                // Jika Jenis Satuan = Angkatan Darat
                var detailDarat = DetailSatuanDarat.FromJson(data);

                satuan.allSenjata = new Dictionary<string, object>
                {
                    ["gun"] = detailDarat.gun,
                    ["missile"] = detailDarat.missile,
                    ["bomb"] = detailDarat.bomb,
                    ["torpedo"] = detailDarat.torpedo
                };

                satuan.tipe_tni = detailDarat.tipe_tni;
                satuan.jenis = JenisSatuan.VEHICLE;
                satuan.path_object_3d = detailDarat.OBJ.model3D;
                satuan.radar = detailDarat.radar;
                satuan.alutsista = ObjectDarat.ToString(detailDarat.OBJ);
                satuan.height = detailDarat.OBJ.height;

                break;
            case "2":
                // Jika Jenis Satuan = Angkatan Laut
                var detailLaut = DetailSatuanLaut.FromJson(data);

                satuan.allSenjata = new Dictionary<string, object>
                {
                    ["gun"] = detailLaut.gun,
                    ["missile"] = detailLaut.missile,
                    ["bomb"] = detailLaut.bomb,
                    ["torpedo"] = detailLaut.torpedo
                };

                satuan.tipe_tni = detailLaut.tipe_tni;
                satuan.jenis = JenisSatuan.SHIP;
                satuan.path_object_3d = detailLaut.OBJ.model3D;
                satuan.radar = detailLaut.radar;
                satuan.sonar = detailLaut.sonar;
                satuan.alutsista = ObjectLaut.ToString(detailLaut.OBJ);
                satuan.height = (float)detailLaut.OBJ.height;

                break;
            case "3":
                // Jika Jenis Satuan = Angkatan Udara
                var detailUdara = DetailSatuanUdara.FromJson(data);

                satuan.allSenjata = new Dictionary<string, object>
                {
                    ["gun"] = detailUdara.gun,
                    ["missile"] = detailUdara.missile,
                    ["bomb"] = detailUdara.bomb,
                    ["torpedo"] = detailUdara.torpedo
                };

                satuan.tipe_tni = detailUdara.tipe_tni;
                satuan.jenis = JenisSatuan.AIRCRAFT;
                satuan.path_object_3d = detailUdara.OBJ.model3D;
                satuan.radar = detailUdara.radar;
                satuan.alutsista = ObjectUdara.ToString(detailUdara.OBJ);
                satuan.height = (float)detailUdara.OBJ.height;

                break;
        }
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
                objectjalur.name = mission.id_mission;

                DataMisi dataMisi = objectjalur.AddComponent<DataMisi>();
                dataMisi.id_mission = mission.id_mission;
                dataMisi.id_object = mission.id_object;
                dataMisi.tgl_mulai = mission.tgl_mulai;
                dataMisi.jenis = mission.jenis;
                dataMisi.data_properties = mission.data_properties;

                dataTarget.jalurMisi.Add(objectjalur);

                objectjalur.transform.parent = misiContainer.transform;
            }

            dataTarget.jalurMisi = dataTarget.jalurMisi.OrderBy(x => Convert.ToDateTime(x.GetComponent<DataMisi>().tgl_mulai)).ToList();
            dataTarget.speed = float.Parse(mission.data_properties.kecepatan, CultureInfo.InvariantCulture.NumberFormat) / 100;

            if (targetObject.GetComponent<ObjectWalker>())
            {
                
            }
            else
            {
                targetObject.AddComponent<ObjectWalker>();
            }

            ColyseusController.instance.CreateMisi(new Dictionary<string, object>()
            {
                ["id"] = mission.id_mission,
                ["idPrimary"] = mission.id,
                ["jenis"] = mission.jenis,
                ["id_user"] = mission.id_user,
                ["tgl_mulai"] = mission.tgl_mulai,
                ["id_object"] = mission.tgl_mulai,
                ["properties"] = MisiSatuan.getString(mission.data_properties),
                ["status"] = 0,
                ["isSelected"] = false,
                ["used"] = false
            });
        }
        catch (Exception err)
        {
            Debug.LogWarning(err);
            Destroy(GameObject.Find(mission.id_mission));

        }
    }

    public async Task<bool> AddMisi(Mission misi)
    {
        if (misi.jenis == "pergerakan")
        {
            return await SetObjectPergerakan(misi);
        }
        //else if (misi.jenis == "embarkasi")
        //{
        //    return await SetObjectEmbarkasiAsync(misi);
        //}
        //else if (misi.jenis == "debarkasi")
        //{
        //    Debug.Log("Tambah Misi Debarkasi");
        //    return await SetObjectDebarkasiAsync(misi);
        //}
        //else if (misi.jenis == "ranjauPergerakan")
        //{
        //    Debug.Log("Tambah Misi Ranjau pergerakan");
        //    return await SetObjectRanjauPergerakanAsync(misi);
        //}
        //else if (misi.jenis == "penyapuanRanjau")
        //{
        //    Debug.Log("Tambah Misi Penyapuan Ranjau");
        //    return await SetObjectPenyapuanRanjauAsync(misi);
        //}

        return true;
    }

    public bool RemoveMisi(Mission misi)
    {
        GameObject misiObject = GameObject.Find(misi.id);
        GameObject satuan = GameObject.Find(misi.id_object);

        List<GameObject> listMisi = satuan.GetComponent<DataSatuan>().jalurMisi;
        List<GameObject> listMisiBaru = new List<GameObject>();

        foreach (GameObject misiSatuan in listMisi)
        {
            if (misiSatuan.GetComponent<DataMisi>().id_mission != misi.id)
            {
                listMisiBaru.Add(misiSatuan);
            }
        }

        satuan.GetComponent<DataSatuan>().jalurMisi = listMisiBaru;

        Debug.Log(misi.id_object + ": Menghapus misi " + misi.id);

        return true;
    }

    public async Task<bool> SetObjectPergerakan(Mission misi)
    {
        var mission = MisiSatuan.FromJson(JsonConvert.SerializeObject(misi));

        if (mission == null) return false;

        if (mission.data_properties == null) return false;

        if (mission.data_properties.jalur == null) return false;

        SpawnMisiSatuan(mission);

        Dictionary<string, object> propMinimap = new();
        propMinimap["id_point"] = mission.id_object;
        propMinimap["id_misi"] = mission.id;
        propMinimap["jalur"] = mission.data_properties.jalur;
        propMinimap["speed"] = mission.data_properties.kecepatan;
        propMinimap["removeOnEnd"] = true;
        await ColyseusController.instance.CreateSetJalurMiniMap(propMinimap);

        Debug.Log(misi.id_object + ": Membuat misi " + mission.id);

        return true;
    }

    public async Task<bool> LoadEntityRadar(JArray arrayRadar)
    {
        if (arrayRadar == null) return false;
        if (arrayRadar.Count == 0) return false;

        for (int i = 0; i < arrayRadar.Count; i++)
        {
            var radar = EntityRadar.FromJson(arrayRadar[i].ToString());
            await SpawnRadarSatuan(radar);
        }

        return true;
    }

    public async Task SpawnRadarSatuan(EntityRadar radar)
    {
        GameObject entityRadar = Instantiate(prefabRadar);
        entityRadar.name = radar.nama;
        entityRadar.transform.position = new Vector2(radar.lng * 1000, radar.lat * 1000);
        entityRadar.transform.parent = radarContainer.transform;
        //entityRadar.AddComponent<RadarScript>();

        DataRadar dataSatuanRadar = entityRadar.AddComponent<DataRadar>();
        dataSatuanRadar.id_entity = radar.nama;
        dataSatuanRadar.id_user = radar.id_user;
        dataSatuanRadar.infoRadar = EntityRadarInfo.FromJson(radar.info_radar);
        dataSatuanRadar.entityRadar = radar;
        dataSatuanRadar.type = "Radar";

        Dictionary<string, object> dataRadar = new Dictionary<string, object> { };
        dataRadar["id_object"] = radar.nama;
        dataRadar["lng"] = radar.lng;
        dataRadar["lat"] = radar.lat;
        dataRadar["type"] = "radar";
        dataRadar["armor"] = 500;
        dataRadar["defaultData"] = radar.info_radar;
        dataRadar["isActive"] = true;

        await ColyseusController.instance.CreateSatuan(dataRadar);
    }

    public void SetRadarScript()
    {
        foreach (string id_satuan in listSatuan)
        {
            GameObject satuan = GameObject.Find(id_satuan);
            GameObject radarChild = satuan.transform.GetChild(1).gameObject;
            GameObject jarakPandangChild = satuan.transform.GetChild(2).gameObject;

            if (satuan.GetComponent<RadarSatuanScript>())
            {
                // nothing
            }
            else
            {
                radarChild.AddComponent<RadarSatuanScript>();
                jarakPandangChild.AddComponent<RadarSatuanScript>();
            }
        }
    }

    public void RefreshRadar()
    {
        foreach (string id_satuan in listSatuan)
        {
            GameObject satuan = GameObject.Find(id_satuan);

            for (int i = 0; i < 2; i++)
            {
                satuan.transform.GetChild(1).gameObject.SetActive(!satuan.transform.GetChild(1).gameObject.activeSelf);
                satuan.transform.GetChild(2).gameObject.SetActive(!satuan.transform.GetChild(2).gameObject.activeSelf);
            }
        }
    }
}
