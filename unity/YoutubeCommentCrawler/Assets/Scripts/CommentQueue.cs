using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebComment;

public class CommentQueue : MonoBehaviour {
	CommentDrawer drawer;
	public void SetDrawer (CommentDrawer d) {
		drawer = d;
	}
	public void AddCommentRange (IList<CommentChunk> chunks) {
		foreach (var c in chunks) {
			PassToDrawer (c);
		}
	}
	void PassToDrawer (CommentChunk chunk) {
		drawer.DrawComment (chunk);
	}
}