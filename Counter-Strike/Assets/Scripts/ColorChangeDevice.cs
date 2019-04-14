using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : BaseDevice {

	public override void Operate(){
		Color random = new Color(Random.Range(0f,1f),
			Random.Range(0f,1f), Random.Range(0f,1f));
		// 设置对象上的材质的颜色
		GetComponent<Renderer>().material.color = random;
	}

}
