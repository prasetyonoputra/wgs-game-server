using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;

public class RadarSatuanScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Badan Entity"))
        {
            DataSatuan entityMusuh = other.GetComponentInParent<DataSatuan>();
            DataSatuan entity = this.GetComponentInParent<DataSatuan>();

            if (entity.detector != null)
            {
                if (entityMusuh.armor > 0 && entityMusuh.infoSatuan.warna != entity.infoSatuan.warna && entity.detector?.dataRadar != null)
                {
                    if (entityMusuh?.ecm_activated != null)
                    {
                        if (entityMusuh.ecm_activated.Count != 0)
                        {
                            foreach (string id in entityMusuh.ecm_activated)
                            {
                                if (id == entity.id_entity)
                                {
                                    Debug.Log("Kesini 1");
                                    RadarFunction(entityMusuh, entity, "ecm");
                                }
                                else
                                {
                                    Debug.Log("Kesini 2");
                                    RadarFunction(entityMusuh, entity, "show");
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Langsung kesini");
                            RadarFunction(entityMusuh, entity, "show");
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Badan Entity"))
        {
            DataSatuan entityMusuh = other.GetComponentInParent<DataSatuan>();
            DataSatuan entity = this.GetComponentInParent<DataSatuan>();

            if (entityMusuh.infoSatuan.warna != entity.infoSatuan.warna)
            {
                if (entity.listDetectRadar.Find(x => (string)x["id"] == entityMusuh.id_entity) != null)
                {
                    if (entityMusuh.listActvRadar.Find(x => (string)x["id"] == entity.id_entity) != null)
                    {
                        int index = entityMusuh.listActvRadar.FindIndex(x => (string)x["id"] == entity.id_entity);

                        if (index != -1)
                        {
                            entityMusuh.listActvRadar.RemoveAt(index);
                            ColyseusController.instance.SetValueActvRadar(entityMusuh.id_entity, JsonConvert.SerializeObject(entityMusuh.listActvRadar));
                        }
                    }

                    if (entity.listDetectRadar.Find(x => (string)x["id"] == entityMusuh.id_entity) != null)
                    {
                        int index = entity.listDetectRadar.FindIndex(x => (string)x["id"] == entityMusuh.id_entity);

                        if (index != -1)
                        {
                            entity.listDetectRadar.RemoveAt(index);
                        }
                    }

                    ColyseusController.instance.SetValueDetectRadar(entity.id_entity, JsonConvert.SerializeObject(entity.listDetectRadar));
                }
            }
        }
    }

    private void RadarFunction(DataSatuan entityMusuh, DataSatuan entity, string status)
    {
        if (status == "show")
        {
            if (entity.listDetectRadar.Find(x => (string)x["id"] == entityMusuh.id_entity) == null)
            {
                if (entityMusuh.listActvRadar.Find(x => (string)x["id"] == entity.id_entity) == null)
                {
                    entityMusuh.listActvRadar.Add(new Dictionary<string, object>
                    {
                        ["id"] = entity.id_entity,
                        ["detector"] = "radar",
                        ["ecm"] = "deactive"
                    });

                    ColyseusController.instance.SetValueActvRadar(entityMusuh.id_entity, JsonConvert.SerializeObject(entityMusuh.listActvRadar));
                }
                else
                {
                    int index = entityMusuh.listActvRadar.FindIndex(x => (string)x["id"] == entity.id_entity);
                    if (index != -1)
                    {
                        if (entityMusuh.listActvRadar[index]["ecm"].ToString() == "active")
                        {
                            entityMusuh.listActvRadar[index]["ecm"] = "deactive";
                        }

                        ColyseusController.instance.SetValueActvRadar(entityMusuh.id_entity, JsonConvert.SerializeObject(entityMusuh.listActvRadar));
                    }
                }

                entity.listDetectRadar.Add(new Dictionary<string, object>
                {
                    ["id"] = entityMusuh.id_entity,
                    ["detector"] = "radar"
                });

                ColyseusController.instance.SetValueDetectRadar(entity.id_entity, JsonConvert.SerializeObject(entity.listDetectRadar));
            }

            if (ColyseusController.instance.typeWGS == 1)
            {
                ColyseusController.instance.SetTembakAuto(entity.id_entity, entity.id_user);
            }
        }
        else if (status == "ecm")
        {
            if (entity.listDetectRadar.Find(x => (string)x["id"] == entityMusuh.id_entity) != null)
            {
                if (entityMusuh.listActvRadar.Find(x => (string)x["id"] == entity.id_entity) != null)
                {
                    int indexx = entityMusuh.listActvRadar.FindIndex(x => (string)x["id"] == entity.id_entity);

                    if (indexx != -1)
                    {
                        entityMusuh.listActvRadar[indexx]["ecm"] = "active";
                        ColyseusController.instance.SetValueActvRadar(entityMusuh.id_entity, JsonConvert.SerializeObject(entityMusuh.listActvRadar));
                    }
                }

                int index = entity.listDetectRadar.FindIndex(x => (string)x["id"] == entityMusuh.id_entity);
                if (index != -1)
                {
                    entity.listDetectRadar.RemoveAt(index);
                }

                ColyseusController.instance.SetValueDetectRadar(entity.id_entity, JsonConvert.SerializeObject(entity.listDetectRadar));
            }
        }
    }
}
