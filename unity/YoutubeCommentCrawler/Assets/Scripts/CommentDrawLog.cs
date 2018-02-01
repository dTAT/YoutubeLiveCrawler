using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentDrawLog : CommentDrawer {
	public override void DrawComments (IList<Youtube.JSON.CommentInfo> comments, bool isDisplayOwnerMode) {
		foreach (var comment in comments) {
			var s = string.Format ("name:{0} msg:{1}", comment.DisplayName, comment.Message);
			if (!comment.IsOwnerMessage || isDisplayOwnerMode) {
				Debug.Log (s);
			}
		}
	}
}