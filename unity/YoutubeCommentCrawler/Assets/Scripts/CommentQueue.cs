using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using WebComment;

public class CommentQueue : MonoBehaviour {

	[SerializeField]
	float dequeueDelay = 1.0f;
	Queue<CommentChunk> commentQueue;
	void Awake () {
		commentQueue = new Queue<CommentChunk> ();
	}
	void Start () {
		StartCoroutine (ProcessDequeue ());
	}

	IEnumerator ProcessDequeue () {
		while (true) {
			if (commentQueue.Count > 0) {
				var c = commentQueue.Dequeue ();
				PassToDrawer (c);
			}
			yield return new WaitForSeconds (dequeueDelay);
		}
	}
	CommentDrawer drawer;
	public void SetDrawer (CommentDrawer d) {
		drawer = d;
	}
	public void AddCommentRange (IList<CommentChunk> chunks) {
		foreach (var c in chunks) {
			//PassToDrawer (c);
			commentQueue.Enqueue (c);
		}
	}
	void PassToDrawer (CommentChunk chunk) {
		//ßDebug.Log ("pass drawer");
		drawer.DrawComment (chunk);
	}
}