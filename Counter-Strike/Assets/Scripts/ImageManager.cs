using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ImageManager : MonoBehaviour, IWeatherManager {

	public  ManagerStatus status {get; private set;}
	private NetworkService _network;
	private Texture2D _webImage;
	
	public void Startup(NetworkService service){
		Debug.Log("Images manager starting...");

		_network = service;

		status = ManagerStatus.Started;
	} 

	public void GetWebImage(Action<Texture2D> callback){
		if(_webImage == null){
			StartCoroutine(_network.DownloadImage((Texture2D image) => {
				_webImage = image;	// 存储已下载的图像
				callback(_webImage);	//回调函数在lambda中函数中使用
			}));  
		}
		else{
			callback(_webImage);

		}
	}
}
