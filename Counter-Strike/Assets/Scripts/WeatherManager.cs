using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IWeatherManager {

	public ManagerStatus status {get; private set;}

	private NetworkService _network;

	public float cloudValue {get; private set;}

	public void Startup(NetworkService service){
		Debug.Log("Weather manager starting...");

		_network = service;

		StartCoroutine(_network.GetWeatherJSON(OnJSONDataLoaded));

		status = ManagerStatus.Initializing;
	}

	public void OnJSONDataLoaded(string data){
		Dictionary<string, object> dict;
		dict = Json.Deserialize(data) as Dictionary<string, object>;

		Dictionary<string, object> clouds = (Dictionary<string, object>)dict["clouds"];
		cloudValue = (long)clouds["all"]/ 100f;
		Debug.Log("Value: " + cloudValue);

		Messenger.Broadcast(GameEvent.WEATHER_UPDATA);

		status = ManagerStatus.Started;
	}
	
	// 解析XML的方法
	// public void OnXMLDataLoaded(string data){
	// 	// 获取解析XML结构
	// 	XmlDocument doc = new XmlDocument();
	// 	doc.LoadXml(data);
	// 	XmlNode root = doc.DocumentElement;

	// 	XmlNode node = root.SelectSingleNode("clouds");
	// 	string value = node.Attributes["value"].Value;
	// 	cloudValue = XmlConvert.ToInt32(value)/100f;
	// 	Debug.Log("Value: " + cloudValue);

	// 	// 广播消息，通知其他脚本
	// 	Messenger.Broadcast(GameEvent.WEATHER_UPDATA);

	// 	status = ManagerStatus.Started;
	// }

}
