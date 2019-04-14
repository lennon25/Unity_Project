using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingPopup settingPopup;

	[SerializeField] private Text healthLabel;

	[SerializeField] private Text levelEnding;
	public int _score;

	void Awake(){
		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
		Messenger.AddListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
		Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	void OnDestroy(){
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
		Messenger.RemoveListener(GameEvent.HEALTH_UPDATE, OnHealthUpdate);
		Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
		Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
		Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
	}

	void Start(){
		OnHealthUpdate();

		_score = 0;
		scoreLabel.text = _score.ToString();

		levelEnding.gameObject.SetActive(false);
		settingPopup.Close();
		
	}
	
	public void OnEnemyHit(){
		_score += 1;
		scoreLabel.text = _score.ToString();
	}

	public void OnOpenSettings(){
		settingPopup.Open();
	}

	private void OnHealthUpdate(){
		string message =  "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
		healthLabel.text = message;
	}

	private void OnLevelComplete(){
		StartCoroutine(CompleteLevel());
	}

	private IEnumerator CompleteLevel(){
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level Complete!";

		yield return new WaitForSeconds(2);

		Managers.Mission.GoToNext();
	}

	private void OnLevelFailed(){
		StartCoroutine(FailLevel());
	}

	private IEnumerator FailLevel(){
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "Level Failed!";

		yield return new WaitForSeconds(2);

		Managers.Player.Respawn();
		Managers.Mission.RestartCurrent();
	}

	public void SaveGame(){
		Managers.Data.SaveGameState();
	}

	public void LoadGame(){
		Managers.Data.LoadGameState();
	}

	private void OnGameComplete(){
		levelEnding.gameObject.SetActive(true);
		levelEnding.text = "You Finished the Game!";
	}
}
