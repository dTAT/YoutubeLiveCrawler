///   based on https://github.com/platoronical/YoutubeLiveCommentAdd
///
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using WebComment;
using Youtube;
public class YoutubeCommentCrawler : MonoBehaviour {

    ///APIKey。GameObjectからセットする想定
    [SerializeField]
    private string apiKey = "!PleaseSetYourAPIKey!";
    ///ChannelID。GameObjectからセットする想定
    [SerializeField]
    private string focusCannelId = "!PleaseSetYourChannel!";
    ///オーナー自身のメッセージを表示するか
    [SerializeField]
    private bool isDisplayOwnerMesssageMode = false;
    ///コメント取得間隔(秒)
    [SerializeField]
    float commentFetchInterval = 5.0f;
    bool doFetchComment = true;
    Youtube.URIGenerator uriGenerator = null;
    //CommentDrawer drawer = null;
    CommentQueue queue = null;
    //コメントと投稿時間だけ出るやつ
    //private string chatURIbottom = "&part=snippet&hl=ja&maxResults=2000&fields=items/snippet/displayMessage,items/snippet/publishedAt,items/authorDetails/displayName&key="; //&part=snippet&hl=ja&maxResults=2000&fields=items/snippet/displayMessage,items/snippet/publishedAt&key=
    void Start () {

        var drawer = GetComponent<CommentDrawer> ();
        if (null != drawer) {
            drawer.IsDisplayOwnerMode = isDisplayOwnerMesssageMode;
        }
        queue = GetComponent<CommentQueue> ();
        if (null != drawer && null != queue) {
            queue.SetDrawer (drawer);
        }
        StartCoroutine (ProcessSearchVideoId ());
    }
    ///指定チャンネルのVIDEOIDをしぼりこむプロセス
    private IEnumerator ProcessSearchVideoId () {
        uriGenerator = new Youtube.URIGenerator (apiKey);
        var searchURI = uriGenerator.GetSearch (focusCannelId);
        Debug.Log ("Connect to: " + searchURI);
        UnityWebRequest liverequest = UnityWebRequest.Get (searchURI);
        ///取得をまつ
        yield return liverequest.SendWebRequest ();
        //エラー
        if (liverequest.isHttpError || liverequest.isNetworkError) {
            Debug.LogError ("Youtube APIの取得に失敗しました。");
            Debug.LogWarning ("Hint: ChannelIDやAPIKeyは設定しましたか？");
            Debug.Log (liverequest.error);
            yield break;
        } else { //成功
            var jsonText = liverequest.downloadHandler.text;
            var videoId = JSON.GetVideoId (jsonText);
            if (string.IsNullOrEmpty (videoId)) {
                Debug.LogError ("VideoIdの取得に失敗しました");
                yield break;
            }
            StartCoroutine (ProcessChatIdFromVideoId (videoId));
        }
    }
    ///ビデオIDからチャットIDをしぼりこむプロセス
    private IEnumerator ProcessChatIdFromVideoId (string videoId) {
        StopCoroutine (ProcessSearchVideoId ());
        var videoURI = uriGenerator.GetVideo (videoId);
        Debug.Log ("GetChat from : " + videoURI);
        UnityWebRequest channelrequest = UnityWebRequest.Get (videoURI);
        yield return channelrequest.SendWebRequest ();
        var jsonText = channelrequest.downloadHandler.text;
        var chatId = JSON.GetChatId (jsonText);
        if (string.IsNullOrEmpty (chatId)) {
            Debug.LogError ("チャットIDの取得に失敗しました");
            yield break;
        }
        StartCoroutine (ProcessComment (chatId));
    }
    ///コメントを取得しつづけるプロセス
    private IEnumerator ProcessComment (string chatId) {
        var nextPageTokenstr = string.Empty;
        while (doFetchComment) {
            yield return new WaitForSeconds (commentFetchInterval);
            var chatURI = uriGenerator.GetLiveChat (chatId, nextPageTokenstr);
            Debug.Log (chatURI);
            UnityWebRequest connectChatrequest = UnityWebRequest.Get (chatURI);
            yield return connectChatrequest.SendWebRequest ();
            var jsonText = connectChatrequest.downloadHandler.text;
            var token = nextPageTokenstr;
            var comments = JSON.GetComments (jsonText, token, out nextPageTokenstr);
            if (null != comments && null != queue) {
                queue.AddCommentRange (comments);
            }
        }
    }
}