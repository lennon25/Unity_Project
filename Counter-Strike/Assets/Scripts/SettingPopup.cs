using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingPopup : MonoBehaviour {

	[SerializeField] private Slider speedSlider;
	[SerializeField] private GameObject repository;

	[SerializeField] private  Image[] itemIcons;
	[SerializeField] private Text[] itemLabels;
	[SerializeField] private Text curItemLabel;
	[SerializeField] private Button equipButton;
	[SerializeField] private Button useButton;

	private string _curItem;

	void Start(){
		speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
	}

	public void Open(){
		gameObject.SetActive(true);
		repository.SetActive(true);

		Refresh();
	}

	public void Close(){
		gameObject.SetActive(false);
		repository.SetActive(false);
	}

	public void OnSubmitName(string name){
		Debug.Log(name);
	}

	public void OnSpeedValue(float speed){
		Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
	}

	public void Refresh(){
		List<string> itemList = Managers.Inventory.GetItemList();

		int  len = itemIcons.Length;
		for (int i=0; i < len; i++){
			if(i < itemList.Count){
				itemIcons[i].gameObject.SetActive(true);
				itemLabels[i].gameObject.SetActive(true);

				string item = itemList[i];

				// 从resource加载精灵
				Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
				itemIcons[i].sprite = sprite;
				itemIcons[i].SetNativeSize();

				int count = Managers.Inventory.GetItemCount(item);
				string message = "x" + count;
				if(item == Managers.Inventory.equippendItem){
					message = "Equipped\n" + message;
				}
				itemLabels[i].text = message;
				EventTrigger.Entry entry = new EventTrigger.Entry();
				// 允许单击图标
				entry.eventID = EventTriggerType.PointerClick;
				// 为每个物品触发不同的lamda函数
				entry.callback.AddListener((BaseEventData date) => {
					OnItem(item);
				});

				EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
				trigger.triggers.Clear();
				trigger.triggers.Add(entry);
			}
			else{
				itemIcons[i].gameObject.SetActive(false);
				itemLabels[i].gameObject.SetActive(false);	
			}
		}

		if(!itemList.Contains(_curItem)){
			_curItem = null;
		}
		if(_curItem == null){
			curItemLabel.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(false);
			useButton.gameObject.SetActive(false);
		}
		else{
			curItemLabel.gameObject.SetActive(true);
			equipButton.gameObject.SetActive(true);
			if(_curItem == "health"){
				useButton.gameObject.SetActive(true);
			}else{
				useButton.gameObject.SetActive(false);
			}

			curItemLabel.text = _curItem + ":";
		}
	}

	// 被鼠标单击监听器调用的函数
	public void OnItem(string item){
		_curItem = item;
		Refresh();
	}

	public void OnEquip(){
		Managers.Inventory.EquipItem(_curItem);
		Refresh();
	}

	public void OnUse(){
		Managers.Inventory.ConsumeItem(_curItem);
		if(_curItem == "health"){
			Managers.Player.ChangeHealth(25);
		}
		else{
			Debug.Log("Don't Use the device.");
		}
		Refresh();
	}
}
