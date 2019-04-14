using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status{get; private set;}

	public int health{get; private set;}
	public int maxHealth{get; private set;}
	NetworkService _network = new NetworkService();

	public void Startup(NetworkService service){
		Debug.Log("Player manager starting...");
		
		_network = service;

		health = 50;
		maxHealth = 100;

		status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth){
		this.health = health;
		this.maxHealth = maxHealth;
	}

	// 其他脚本可以调用这个方法调整血量
	public void ChangeHealth(int value){
		health += value;
		if(health > maxHealth){
			health = maxHealth;
		}
		else if(health < 0){
			health = 0;
		}

		if(health == 0){
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}
		Messenger.Broadcast(GameEvent.HEALTH_UPDATE);
	}
	
	// 重置玩家为初始化状态
	public void Respawn(){
		UpdateData(50, 100);
	}
}
