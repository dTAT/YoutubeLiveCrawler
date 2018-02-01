using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Youtube;
public abstract class CommentDrawer : MonoBehaviour {
	public abstract void DrawComments (IList<JSON.CommentInfo> comments, bool isDisplayOwnerMode);

	// float _x = UnityEngine.Random.Range (-400f, 400f);
	// float _y = UnityEngine.Random.Range (-250f, 250f);
	// GameObject cvn = Instantiate (canvas);
	// cvn.transform.Find ("Description").gameObject.GetComponent<Text> ().text = message;
	// cvn.transform.Find ("Name").gameObject.GetComponent<Text> ().text = dispName;
	// cvn.transform.position = new Vector3 (_x, _y, cvn.transform.position.z);
}