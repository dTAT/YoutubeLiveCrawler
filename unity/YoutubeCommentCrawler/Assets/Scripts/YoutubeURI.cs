namespace Youtube {
	public class URIGenerator {
		private string apiKey;
		public URIGenerator (string key) {
			apiKey = key;
		}
		const string youtubeDomain = "https://www.googleapis.com/youtube/v3/";
		/// チャンネル内検索用のURIを取得する
		public string GetSearch (string channelId) {
			const string searchURIFormat = "{0}search?key={1}&part=snippet&channelId={2}&eventType=live&type=video";
			return string.Format (searchURIFormat, youtubeDomain, apiKey, channelId);
		}
		/// VideoのURIを取得する
		public string GetVideo (string videoId) {
			const string searchChannelFormat = "{0}videos?part=liveStreamingDetails&key={1}&id={2}";
			return string.Format (searchChannelFormat, youtubeDomain, apiKey, videoId);
		}
		///liveChatのURIを取得する
		public string GetLiveChat (string liveChatId, string nextPageToken = null) {
			const string getChatURIFormat = "{0}liveChat/messages?liveChatId={1}&part=snippet,authorDetails&key={2}{3}";
			var pageToken = (string.IsNullOrEmpty (nextPageToken)) ? string.Empty : pageTokenURI + nextPageToken;
			return string.Format (getChatURIFormat, youtubeDomain, liveChatId, apiKey, pageToken);
		}
		const string pageTokenURI = "&pageToken=";
	}
}