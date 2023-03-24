using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Badan Entity"))
        {
            DataSatuan entityMusuh = other.GetComponentInParent<DataSatuan>();
            DataObstacle obstacle = this.GetComponent<DataObstacle>();

            if (!obstacle.isDestroy && entityMusuh.armor > 0)
            {
                Debug.Log("Ada kena 2");
                if (obstacle.infoObstacle.font == "J")
                {
                    Debug.Log("Ada kena 4");
                    if (entityMusuh.jenis == DataSatuan.JenisSatuan.VEHICLE)
                    {
                        Debug.Log("Ada kena 5");
                        if (!entityMusuh.kebalRanjau)
                        {
                            Debug.Log("Ada kena 6");
                            // Kurangi armor entity yang terkena ranjau 150
                            entityMusuh.armor -= 150;

                            // Send Colyseus
                            ColyseusController.instance.SetArmor(entityMusuh.id_entity, entityMusuh.armor);
                        }
                        else
                        {
                            Debug.Log(entityMusuh.id_entity + " kebal ranjau");
                        }

                        // Hancurkan obstacle setelah terinjak
                        ColyseusController.instance.DestroyRanjau(obstacle.nama);
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Debug.Log("Ada kena 7");
                    if (entityMusuh.jenis == DataSatuan.JenisSatuan.SHIP)
                    {
                        Debug.Log("Ada kena 8");
                        if (!entityMusuh.kebalRanjau)
                        {
                            Debug.Log("Ada kena 9");
                            // Kurangi armor entity yang terkena ranjau 100
                            entityMusuh.armor -= 100;

                            // Send Colyseus
                            ColyseusController.instance.SetArmor(entityMusuh.id_entity, entityMusuh.armor);
                        }
                        else
                        {
                            Debug.Log(entityMusuh.id_entity + " kebal ranjau");
                        }

                        // Hancurkan obstacle setelah terinjak
                        ColyseusController.instance.DestroyRanjau(obstacle.nama);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
