using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit(){
		// 调用方法，设置角色alive为false
		WanderingAI behavior = GetComponent<WanderingAI>();
		if(behavior != null){
			behavior.SetAlive(false);
		}
		StartCoroutine(Die());
	}

	// 协程方法，响应被击中，并消失
	private IEnumerator Die(){
		this.transform.Rotate(-75, 0, 0);

		yield return new WaitForSeconds(1.5f);

		Destroy(this.gameObject);

	}
}
