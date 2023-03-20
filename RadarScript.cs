using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;

public class RadarScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Badan Entity"))
        {
            DataSatuan entityMusuh = other.GetComponentInParent<DataSatuan>();
            DataRadar entity = this.GetComponent<DataRadar>();

            if (entityMusuh.armor > 0 && entityMusuh.infoSatuan.warna != entity.infoRadar.warna)
            {
                RadarFunction(entityMusuh, entity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Badan Entity"))
        {
            DataSatuan entityMusuh = other.GetComponentInParent<DataSatuan>();
            DataRadar entity = this.GetComponent<DataRadar>();

            if (entityMusuh.infoSatuan.warna != entity.infoRadar.warna)
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

    private void RadarFunction(DataSatuan entityMusuh, DataRadar entity)
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
    }
}
