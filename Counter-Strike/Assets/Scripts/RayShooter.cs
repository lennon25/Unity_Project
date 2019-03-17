using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour {
	private Camera _camera;



	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera>();	

		// 隐藏屏幕中心的光标
		// Cursor.lockState = CursorLockMode.Locked;
		// Cursor.visible = false;

	}

	void OnGUI() {
		int size = 80;
		float posX = _camera.pixelWidth/2 -size/4;
		float posY = _camera.pixelHeight/2 -size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
			// 获取屏幕中心的点，是屏幕宽高的一半
			Vector3 point = new Vector3(_camera.pixelWidth/2,
										_camera.pixelHeight/2, 0);
			
			Ray ray = _camera.ScreenPointToRay(point);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				// 获取射线击中的对象
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				// 判断击中对象上是否有ReactiveTarget对象
				if(target !=null){
					// 调用被击中方法
					target.ReactToHit();
					Messenger.Broadcast(GameEvent.ENEMY_HIT);
				}
				else{
					StartCoroutine(SphereIndicator(hit.point)); // 调用启动协程来响应击中
				}
				
			}
		}
	}

	// 创建协程方法来响应击中
	private IEnumerator SphereIndicator(Vector3 pos){
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = pos;

		yield return new WaitForSeconds(1);

		Destroy(sphere);
	}
}
