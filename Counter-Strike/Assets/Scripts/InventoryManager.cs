using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status	{get; private set;}
	
	private Dictionary<string, int> _items;  // 使用字典数据结构存储物品
	
	public string equippendItem{get; private set;}

	public void Startup(NetworkService service){
		Debug.Log("Inventory manager starting...");
		// 初始化空的物品结构
		UpdateData(new Dictionary<string, int>());

		status = ManagerStatus.Started;
	}
	public void UpdateData(Dictionary<string, int > items){
		_items = items;
	}
	public Dictionary<string,int> GetData(){
		return _items;
	}


	private void DisplayItems(){
		string itemDisplay = "Items: ";
		foreach(KeyValuePair<string, int> item in _items){
			itemDisplay += item.Key + " " + item.Value + " "; 
		
		}
		Debug.Log(itemDisplay);
	}

	public void AddItem(string name){
		if(_items.ContainsKey(name)){
			_items[name] += 1;
		}else{
			_items[name] = 1;
		}
		
		DisplayItems();
	}

	public List<string> GetItemList(){
		List<string> list = new List<string>(_items.Keys);
		return list;
	}

	public int GetItemCount(string name){
		if(_items.ContainsKey(name))
			return _items[name];
		return 0;
	}

	public bool EquipItem(string name){
		if(_items.ContainsKey(name) && equippendItem != name){
			equippendItem = name;
			Debug.Log("Equipped " + name);
			return true;
		}

		equippendItem = null;
		Debug.Log("Unequipped");
		return false;
	}

	public bool ConsumeItem(string name){
		if(_items.ContainsKey(name)){
			_items[name]--;
			if(_items[name] == 0){
				_items.Remove(name);
			}
		}else{
			Debug.Log("Connot consume " + name);
			return false;
		}

		DisplayItems();
		return true;
	}
}
