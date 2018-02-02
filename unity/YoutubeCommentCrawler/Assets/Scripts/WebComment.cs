namespace WebComment {
	public class CommentChunk {
		public bool IsOwnerMessage;
		public string DisplayName;
		public string Message;
		public CommentChunk (bool IsOwner, string name, string message) {
			IsOwnerMessage = IsOwner;
			DisplayName = name;
			Message = message;
		}
		public int commentIdHash;
	}
}