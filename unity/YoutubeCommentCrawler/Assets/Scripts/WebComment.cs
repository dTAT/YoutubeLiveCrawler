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
		///付与情報
		public int commentIdHash;
		public int autherHash;
		public string autherString;
	}
}