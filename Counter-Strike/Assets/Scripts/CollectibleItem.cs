using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
	[SerializeField] private string ItemName;

	
	void OnTriggerEnter(Collider other){
		Managers.Inventory.AddItem(ItemName);
		Destroy(this.gameObject);
	}
}
