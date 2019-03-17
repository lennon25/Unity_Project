using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

	//编写基本的AI功能，AI是计算机控制的个体
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;
	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private bool _alive;



	void Start() {
		_alive = true;
	}	
	
	// Update is called once per frame
	void Update () {
		if(_alive){
			transform.Translate(0,0,speed * Time.deltaTime);

			Ray ray = new Ray(transform.position, transform.forward);
	
			RaycastHit hit;
			if(Physics.SphereCast(ray, 0.75f, out hit)){
				GameObject hitObject = hit.transform.gameObject;
				// 检测射线击中是否玩家对象
				if(hitObject.GetComponent<PlayerCharacter>()){
					if(_fireball == null){
						_fireball = Instantiate(fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;
					}
				}
				else if(hit.distance < obstacleRange){
					float angle = Random.Range(-100, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	public void SetAlive(bool alive){
		_alive = alive;
	}
}
