using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager{

	public ManagerStatus status {get; private set;}

	private string _filename;
	private NetworkService _network;

	public void Startup(NetworkService service){
		Debug.Log("Data manager starting...");

		_network = service;

		// 构建game.dat文件的完整路径，unity提供用于保存数据的位置
		_filename = Path.Combine(Application.persistentDataPath, "game.dat");

		status = ManagerStatus.Started;
	}

	public void SaveGameState(){
		// 将数据序列化为字典
		Dictionary<string, object> gameState = new Dictionary<string, object>();
		gameState.Add("inventory", Managers.Inventory.GetData());
		gameState.Add("health", Managers.Player.health);
		gameState.Add("maxHealth", Managers.Player.maxHealth);
		gameState.Add("curLevel", Managers.Mission.curLevel);
		gameState.Add("maxLevel", Managers.Mission.maxLevel);

		// 在文件路径创建一个二进制文件，创建文本文件使用File.CreateText()
		FileStream stream = File.Create(_filename);
		BinaryFormatter formatter = new BinaryFormatter();
		// 序列化字典作为创建文件的存储内容
		formatter.Serialize(stream, gameState);
		stream.Close();

	}
	
	public void LoadGameState(){
		if(!File.Exists(_filename)){
			Debug.Log("No saved game");
			return;
		}

		// 用于放置加载数据的字典
		Dictionary<string, object> gameState;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = File.Open(_filename, FileMode.Open);
		gameState = formatter.Deserialize(stream) as Dictionary<string, object>;
		stream.Close();

		// 使用反序列化的数据更新管理器
		Managers.Inventory.UpdateData((Dictionary<string, int>) gameState["inventory"]);
		Managers.Player.UpdateData((int)gameState["health"], (int)gameState["maxHealth"]);
		Managers.Mission.UpdateData((int)gameState["curLevel"], (int)gameState["maxLevel"]);
		Managers.Mission.RestartCurrent();

	} 
}

