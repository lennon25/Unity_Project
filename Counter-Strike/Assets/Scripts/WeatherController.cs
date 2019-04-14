using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
	[SerializeField] private Material sky;
	[SerializeField] private Light sun;

	private float _fullIntensity;
	private float _cloudValue = 0f;

	// 添加/移除事件监听器
	void Awake(){
		
		Messenger.AddListener(GameEvent.WEATHER_UPDATA, OnWeatherUpdate);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.WEATHER_UPDATA, OnWeatherUpdate);
	}

	// Use this for initialization
	void Start () {
		_fullIntensity = sun.intensity;
	}
	

	private void OnWeatherUpdate(){
		// 使用WeatherManagers的多云值
		SetOvercast(WeatherManagers.Weather.cloudValue);
	}

	private void SetOvercast(float value){
		sky.SetFloat("_Blend", value);
		sun.intensity = _fullIntensity - (_fullIntensity * value);
	}
}
