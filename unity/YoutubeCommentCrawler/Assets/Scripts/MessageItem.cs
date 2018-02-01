using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageItem : MonoBehaviour {
	[SerializeField]
	Text textDisplayName;
	[SerializeField]
	Text textDisplayMessage;
	public void Set (string name, string message) {
		textDisplayName.text = name;
		textDisplayMessage.text = message;
	}
}