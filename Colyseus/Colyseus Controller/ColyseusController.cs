using Colyseus;
using Colyseus.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using Wargaming.Core.GlobalParam.HelperDataAlutsista;

public class ColyseusController : MonoBehaviour
{
	public ColyseusRoom<State> room;
	public ColyseusClient client;
	public static ColyseusController instance;
	public static ColyseusState state;
	internal int typeWGS = 1;
	const string serviceColyseus = "ws://localhost";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		} else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		Debug.Log("Start");
		_ = prepareRoomAsync();
	}

    // Update is called once per frame
    void Update()
    {
		
	}

	public async Task prepareRoomAsync()
	{
		client = new ColyseusClient(serviceColyseus + ":2567");
		room = await client.JoinOrCreate<State>("wargaming");
		state = new ColyseusState();

		var _message = new ColyseusRoomMessage();
		_message.initMessage();

		room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);

		room.State.world.OnChange += state.OnStateWorldChange;

		room.State.players.OnAdd += state.OnStatePlayerJoin;
		room.State.players.OnRemove += state.OnStatePlayerLeave;

		room.State.entities.OnAdd += state.OnStateEntitiesAdd;

		room.State.missions.OnAdd += state.OnStateMissionAdd;
		room.State.missions.OnRemove += state.OnStateMissionRemove;
	}

	public async Task CreateSatuan(Dictionary<string, object> dataEntity)
	{
		await room.Send("tambah_entity", dataEntity);
	}

	public async Task CreateMisi(Dictionary<string, object> dataMission)
	{
		await room.Send("tambah_misi", dataMission);
	}

	public async Task SendListText(JArray dataListTeks)
	{
		await room.Send("setListTexts", dataListTeks.ToString());
	}

	public async void SendPosition(string id_satuan, Vector2 position, double heading, double distance)
	{
		await room.Send("movement", new Dictionary<string, object>
		{
			["id_object"] = id_satuan,
			["lat"] = position.y / 1000,
			["lng"] = position.x / 1000,
			["heading"] = heading,
			["altitude"] = 0,
			["jarakTempuh"] = distance
		});
	}

	public async void SetTime(string waktu)
	{
		await room.Send("media", new Dictionary<string, string>
		{
			["waktuGame"] = waktu
		});
	}

	public async Task CreateSetJalurMiniMap(Dictionary<string, object> dataMisi)
	{
		await room.Send("setJalurMinimap", dataMisi);
	}
	public async void SetValueActvRadar(string id_object, string dataDetector)
	{
		await room.Send("detek_radar", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["valueActv"] = dataDetector
		});
	}

	public async void SetValueDetectRadar(string id_object, string dataDetector)
	{
		await room.Send("detek_radar", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["valueDetect"] = dataDetector
		});
	}

	public async void SetTembakAuto(string id_object, string id_user)
	{
		await room.Send("setTembakAuto", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["id_user"] = id_user
		});
	}

	public async void SetStatusMisi(string idMisi, int statusMisi, string id)
	{
		await room.Send("setStatusMisi", new Dictionary<string, object>
		{
			["id_misi"] = idMisi,
			["status_misi"] = statusMisi
		});
	}

	public async void SetMedia(Dictionary<string, object> state)
	{
		await room.Send("media", state);
	}

	public async void SendMisiRanjau(string id_object, string value)
	{
		await room.Send("setMisiRanjau", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["missionDefault"] = value
		});
	}

	public async void SetOpacityObject(string id_object, int value)
	{
		await room.Send("setOpacityObject", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["value"] = value
		});
	}

	public async void SendListEmbarkasi(string id_object, List<string> listEmbarkasi)
	{
		await room.Send("embarkasi", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["listEmbarkasi"] = JsonConvert.SerializeObject(listEmbarkasi)
		});
	}

	public async void SetIconNow(string id_object, string value)
	{
		await room.Send("changeIcon", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["iconNow"] = value
		});
	}

	public async void SetArmor(string id_object, float armor)
	{
		await room.Send("setArmor", new Dictionary<string, object>
		{
			["id_object"] = id_object,
			["armor"] = armor
		});
	}

	public async void DestroyRanjau(string idObstacle)
	{
		await room.Send("destroy_ranjau", new Dictionary<string, object>
		{
			["id_object"] = idObstacle,
			["isDestroy"] = true
		});
	}
}

public class ColyseusState
{
	public void OnStateWorldChange(List<DataChange> changes)
	{
		for (int i = 0; i < changes.Count; i++)
		{
			switch (changes[i].Field)
			{
				case "media":
					Debug.Log(changes[i].Value.ToString());
					TimeController.instance.ChangeMedia(changes[i].Value.ToString());
					break;
				case "perbandinganGame":
					TimeController.instance.SetPercepatan(int.Parse(changes[i].Value.ToString()));
					break;
				case "typeWGS":
					ColyseusController.instance.typeWGS = int.Parse(changes[i].Value.ToString());
					break;
			}
		}
	}

	public void OnStatePlayerJoin(string key, players players)
	{
		Debug.Log(players.nama + " joined!");
	}

	public void OnStateEntitiesAdd(string key, Entity entity)
	{
		if (entity.type == "add_obstacle")
		{
			_ = EntityController.instance.SpawnObstacleOnAdd(entity);
		}

		if (entity.type == "obstacle")
		{
			_ = EntityController.instance.SpawnObstacleOnAdd(entity);
		}
	}
	public void OnStatePlayerLeave(string key, players players)
	{
		Debug.Log(players.nama + " left!!");
	}

	public async void OnStateMissionAdd(string key, Mission mission)
	{
		await EntityController.instance.AddMisi(mission);
	}

	public void OnStateMissionRemove(string key, Mission mission)
	{
		EntityController.instance.RemoveMisi(mission);
	}

	public void OnStateMissionChange(string key, Mission mission)
	{
		Debug.Log("onMissionChange");
	}
}

public class ColyseusRoomMessage
{
	protected ColyseusRoom<State> room;

	public void initMessage()
	{
		room = ColyseusController.instance.room;

		room.OnMessage<Mission>("ubah_misi_unity", (mission) =>
		{
			_ = EntityController.instance.EditMisi(mission);
		});

		room.OnMessage<DataECM>("ecmToUnity", (data) =>
		{
			try
			{
				GameObject entity = GameObject.Find(data.id_object);
				DataSatuan dataEntity = entity.GetComponent<DataSatuan>();
				dataEntity.ecm_activated = data.ecm_activated;

				Debug.Log(dataEntity + ": mengaktifkan ECM");
			}
			catch (Exception e)
			{
				Debug.LogWarning(e.ToString());
			}
		});

		room.OnMessage<DataArmor>("setArmorUnity", (data) =>
		{
			GameObject entity = GameObject.Find(data.id_object);
			GameObject radarChild = entity.transform.GetChild(1).gameObject;
			GameObject jarakPandangChild = entity.transform.GetChild(2).gameObject;
			DataSatuan dataEntity = entity.GetComponent<DataSatuan>();

			dataEntity.armor = float.Parse(data.armor);
			Debug.Log(dataEntity.id_entity + ": sisa armor " + dataEntity.armor);

			if (dataEntity.armor <= 0)
			{
				Debug.Log(dataEntity.id_entity + ": mati");
				EntityController.instance.RefreshRadar();
			}
		});

		room.OnMessage<DataDetector>("setDetectorUnity", (data) =>
		{
			try
			{
				GameObject entity = GameObject.Find(data.id_object);

				DataSatuan dataEntity = entity.GetComponent<DataSatuan>();

				dataEntity.detector = data.detector;

				foreach (RadarSatuan dataRadar in dataEntity.detector.dataRadar)
				{
					if (dataRadar.used == 1)
					{
						GameObject radarChild = entity.transform.GetChild(1).gameObject;
						GameObject jarakPandangChild = entity.transform.GetChild(2).gameObject;

						radarChild.GetComponent<CircleCollider2D>().radius = float.Parse(dataRadar.RADAR_DET_RANGE, CultureInfo.InvariantCulture.NumberFormat) * 10;

						jarakPandangChild.SetActive(false);
						radarChild.SetActive(true);

						Debug.Log(dataEntity.id_entity + ": active detector");

						break;
					}
					else
					{
						GameObject radarChild = entity.transform.GetChild(1).gameObject;
						GameObject jarakPandangChild = entity.transform.GetChild(2).gameObject;

						jarakPandangChild.SetActive(true);
						radarChild.SetActive(false);

						foreach (Dictionary<string, object> datae in dataEntity.listDetectRadar)
						{
							GameObject x = GameObject.Find((string)datae["id"]);
							DataSatuan dataX = x.GetComponent<DataSatuan>();

							if (dataX.listActvRadar.Find(x => (string)x["id"] == dataEntity.id_entity) != null)
							{
								int index = dataX.listActvRadar.FindIndex(x => (string)x["id"] == dataEntity.id_entity);

								if (index != -1)
								{
									dataX.listActvRadar.RemoveAt(index);
									ColyseusController.instance.SetValueActvRadar(dataX.id_entity, JsonConvert.SerializeObject(dataX.listActvRadar));
								}
							}
						}

						dataEntity.listDetectRadar.Clear();
						dataEntity.listActvRadar.Clear();

						ColyseusController.instance.SetValueActvRadar(dataEntity.id_entity, JsonConvert.SerializeObject(dataEntity.listActvRadar));
						ColyseusController.instance.SetValueDetectRadar(dataEntity.id_entity, JsonConvert.SerializeObject(dataEntity.listDetectRadar));

						Debug.Log(dataEntity.id_entity + ": delete detector");
					}
				}

			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		});
	}
}

class DataArmor
{
	public string id_object { get; set; }
	public string armor { get; set; }
}

class DataDetector
{
	public string id_object { get; set; }
	public Detector detector { get; set; }
}

class DataECM
{
	public string id_object { get; set; }
	public List<string> ecm_activated { get; set; }
}