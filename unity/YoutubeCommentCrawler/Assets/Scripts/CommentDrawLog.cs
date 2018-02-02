using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebComment;

public class CommentDrawLog : CommentDrawer {
	public override void DrawComment (CommentChunk comment) {
		var s = string.Format ("name:{0} msg:{1}", comment.DisplayName, comment.Message);
		if (!comment.IsOwnerMessage || IsDisplayOwnerMode) {
			Debug.Log (s);
		}
	}
	public override void DrawComments (IList<CommentChunk> comments, bool isDisplayOwnerMode) {
		foreach (var comment in comments) {
			var s = string.Format ("name:{0} msg:{1}", comment.DisplayName, comment.Message);
			if (!comment.IsOwnerMessage || isDisplayOwnerMode) {
				Debug.Log (s);
			}
		}
	}
}