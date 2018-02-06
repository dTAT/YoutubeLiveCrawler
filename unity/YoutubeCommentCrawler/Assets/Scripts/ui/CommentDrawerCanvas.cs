using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebComment;
public class CommentDrawerCanvas : CommentDrawer {

	[SerializeField]
	Canvas canvas;
	[SerializeField]
	MessageItem messagePrefab;
	List<MessageItem> messageList = new List<MessageItem> ();
	List<int> messageHashList = new List<int> ();
	const int messageMax = 20;

	void Awake () {
		IsDisplayOwnerMode = false;
	}

	public override void DrawComment (CommentChunk comment) {
		//Debug.Log (comment.Message);
		if (IsDisplayOwnerMode || !comment.IsOwnerMessage) {
			if (messageList.Count > messageMax) {
				var i = messageList[0];
				messageList.Remove (i);
				GameObject.Destroy (i.gameObject);
			}
			AddComment (comment);
		}
	}
	public override void DrawComments (IList<CommentChunk> comments, bool isDisplayOwnerMode) {
		foreach (var c in comments) {
			if (messageHashList.Contains (c.commentIdHash)) {
				continue;
			}
			messageHashList.Add (c.commentIdHash);
			if (isDisplayOwnerMode || !c.IsOwnerMessage) {
				if (messageList.Count > messageMax) {
					var i = messageList[0];
					messageList.Remove (i);
					GameObject.Destroy (i.gameObject);
				}
				AddComment (c);
			}
		}
	}

	[SerializeField]
	float RandomXRange = 500.0f;
	[SerializeField]
	float RandomYRange = 300.0f;
	[SerializeField]
	float ZPos = 900.0f;
	void AddComment (CommentChunk comment) {
		var x = Random.Range (-RandomXRange, RandomXRange);
		var y = Random.Range (-RandomYRange, RandomYRange);
		var item = GameObject.Instantiate (messagePrefab);
		item.transform.SetParent (canvas.transform);
		item.transform.localPosition = new Vector3 (x, y, ZPos);
		var mi = item.GetComponent<MessageItem> ();
		mi.Set (comment.DisplayName, comment.Message);
		messageList.Add (mi);
	}
}