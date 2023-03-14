using Colyseus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainController : MonoBehaviour
{
	ColyseusRoom<MyRoomState> room;
	ColyseusClient client;
	public static MainController instance;

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
		room = await client.JoinOrCreate<MyRoomState>("my_room");
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

	public async Task syncPosition(Vector2 position, string idPlayer)
	{
		await room.Send("position", new Dictionary<string, object>() {
			["position"] = position,
			["id"] = idPlayer
		});
	}
}