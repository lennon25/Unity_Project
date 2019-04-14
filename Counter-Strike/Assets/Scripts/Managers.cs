using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]
[RequireComponent(typeof(DataManager))]

public class Managers : MonoBehaviour {

	public static PlayerManager Player{
		get; private set;
	}
	public static InventoryManager Inventory{
		get; private set;
	}
	public static MissionManager Mission {get; private set;}

	public static DataManager Data {get; private set;}

	// 启动时要遍历的管理器列表
	private List<IGameManager> _startSequence;

	void Awake(){
		// Unity提供方法，用于在场景间持久化
		DontDestroyOnLoad(gameObject);

		Data = GetComponent<DataManager>();
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Mission = GetComponent<MissionManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
		_startSequence.Add(Mission);
		_startSequence.Add(Data);

		// 启动协程序列，异步执行
		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers(){

		NetworkService network = new NetworkService();

		foreach(IGameManager manager in _startSequence){
			manager.Startup(network);
		}
		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules){
			int lastReady = numReady;
			numReady = 0;

			foreach(IGameManager manager in _startSequence){
				if(manager.status == ManagerStatus.Started){
					numReady++;
				}
			}

			if(numReady > lastReady){
				Debug.Log("Progress: " + numReady + "/" + numModules);
				// startup事件根据事件广播数据
				Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
				yield return null;
			}

			
		}
		Debug.Log("All manager started up");
		Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);  // startup事件不使用参数广播
	}
}
