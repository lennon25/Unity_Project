using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingPopup settingPopup;

	public int _score;

	void Awake(){
		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void Start(){

		_score = 0;
		scoreLabel.text = _score.ToString();

		settingPopup.Close();
	}
	
	public void OnEnemyHit(){
		_score += 1;
		scoreLabel.text = _score.ToString();
	}

	public void OnOpenSettings(){
		settingPopup.Open();
	}
}
