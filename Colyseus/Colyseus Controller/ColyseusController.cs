using Colyseus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ColyseusController : MonoBehaviour
{
	ColyseusRoom<MyRoomState> room;
	ColyseusClient client;
	public static ColyseusController instance;

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
		client = new ColyseusClient("ws://localhost:2567");
		room = await client.JoinOrCreate<MyRoomState>("wargaming");
		Debug.Log("Create room");

		room.OnJoin += OnJoinRoom;
		room.OnLeave += OnLeaveRoom;
		room.OnStateChange += OnStateChange;
	}

	private void OnStateChange(MyRoomState state, bool isFirstState)
	{
		Debug.Log("State change!");
	}

	private void OnLeaveRoom(int code)
	{
		Debug.Log("Someone has leaved room!");
	}

	private void OnJoinRoom()
	{
		Debug.Log("Someone has left room!");
	}

	public async Task CreateSatuan(Dictionary<string, object> dataEntity)
	{
		await room.Send("tambah_entity", dataEntity);
	}

	public async void SendPosition(string id_satuan, Vector2 position, double heading)
	{
		await room.Send("movement", new Dictionary<string, object>
		{
			["id_object"] = id_satuan,
			["lat"] = position.y / 1000,
			["lng"] = position.x / 1000,
			["heading"] = heading,
			["altitude"] = 0
		});
	}

	public async void SetTime(string waktu)
	{
		await room.Send("media", new Dictionary<string, string>
		{
			["waktuGame"] = waktu
		});
	}
}