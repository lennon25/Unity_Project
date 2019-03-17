using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
	public int HP;


	// Use this for initialization
	void Start () {
		HP = 5;
	}
	
	public void Hurt(int damage){
		HP -= damage;
		Debug.Log("Health points: " + HP);
	}
}
