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

	void AddComment (CommentChunk comment) {
		var x = Random.Range (-1200.0f, 500.0f);
		var y = Random.Range (-630.0f, 270.0f);
		var z = 900.0f;
		var item = GameObject.Instantiate (messagePrefab);
		item.transform.SetParent (canvas.transform);
		item.transform.localPosition = new Vector3 (x, y, z);
		var mi = item.GetComponent<MessageItem> ();
		mi.Set (comment.DisplayName, comment.Message);
		messageList.Add (mi);
	}
}