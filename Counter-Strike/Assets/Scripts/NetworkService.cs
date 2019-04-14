using UnityEngine;
using System.Collections;
using System;
using System.Xml;

public class NetworkService{
	// 发送请求的URL
	private const string jsonApi = 
		"http://api.openweathermap.org/data/2.5/weather?q=Chicage,us";
		
	private const string webImage = 
		"http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

	private const string localApi = 
		"http://localhost/ch9/api.php";


	private bool IsResponseValid(WWW www){
		// 响应中检查错误
		if(www.error != null){
			Debug.Log("bad connection");
			return false;
		}
		else if (string.IsNullOrEmpty(www.text)){
			Debug.Log("bad data");
			return false;
		}
		else{
			return true;
		}	
	}

	// private IEnumerator CallAPI(string url, Action<string> callback){
	// 	// 通过创建WWW对象，发送HTTP请求
	// 	WWW www = new WWW(url);
	// 	yield return www;
	// 	if (!IsResponseValid(www)){
	// 		yield break;
	// 	}
	// 	// 使用委托直接调用函数
	// 	callback(www.text);
	// }

	// 给CallAPI添加参数
	private IEnumerator CallAPI(string url, Hashtable args, Action<string> callback){
		WWW www;
		if (args == null){
			www = new WWW(url);
		}else{
			// 使用WWWForm发送参数
			WWWForm form = new WWWForm();
			foreach(DictionaryEntry arg in args){
				form.AddField(arg.Key.ToString(), arg.Value.ToString());
			}
			www = new WWW(url, form);
		}
		yield return www;

	}

	public IEnumerator DownloadImage(Action<Texture2D> callback){  // 这个回调使用Texture2D
		WWW www = new WWW(webImage);
		yield return www;
		callback(www.texture);
	}
	
	public IEnumerator GetWeatherJSON(Action<string> callback){
		// 使用一个协程方法 调用另一协程方法嵌套返回
		return CallAPI(jsonApi, null, callback);
	}


	public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback){
		Hashtable args = new Hashtable();
		args.Add("message", name);
		args.Add("cloud_value", cloudValue);
		args.Add("timestamp", DateTime.UtcNow.Ticks);

		return CallAPI(localApi, args, callback);
	}

}