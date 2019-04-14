using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour {

	void Start(){
		Operate();
	}

	public void Operate(){
		// 调用ImageManager中的方法
		WeatherManagers.Image.GetWebImage(OnWebImage);
	}

	public void OnWebImage(Texture2D image){
		// 在回调中将已下载的图像应用到材质
		GetComponent<Renderer>().material.mainTexture = image;
	}

}
