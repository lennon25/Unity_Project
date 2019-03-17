using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Scrip/FPS Input")]
public class FPSInput : MonoBehaviour {
	public float speed = 6.0f;
	private CharacterController _characterController;
	public float gravity = -9.8f;

	// Use this for initialization
	void Start () {
		// 使用附加到相同对象上的其他组件
		_characterController = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement,speed);
		movement.y = gravity;

		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		_characterController.Move(movement);

		
	}
}
