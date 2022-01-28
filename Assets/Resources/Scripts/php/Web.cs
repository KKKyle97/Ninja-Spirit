using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Web : MonoBehaviour
{
    string url;

    private void Start()
    {
        url = "http://childoflightpms.herokuapp.com/api/ninjaspirits/";
        //url = "http://localhost:8086/api/ninjaspirits/";
    }

    public IEnumerator Register(string username, string name, int avatarId, System.Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        //form.AddField(name of field, data of field)
        form.AddField("username", username);
        form.AddField("name", name);
        form.AddField("avatarId", avatarId);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"firstLogin", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);

                if (Main.instance.response.message.Contains("success"))
                {
                    Main.instance.userInfo = JsonUtility.FromJson<UserInfo>(Main.instance.response.data);
                    Debug.Log(Main.instance.userInfo.id);
                    Debug.Log("registered");
                    callback(true);
                }
            }
        }
    }

    public IEnumerator Login(string username, string password, System.Action<int> callback)
    {
        WWWForm form = new WWWForm();
        //form.AddField(name of field, data of field)
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
               
                if (Main.instance.response.message.Contains("logged"))
                {
                    Main.instance.userInfo = JsonUtility.FromJson<UserInfo>(Main.instance.response.data);
                    callback(1);
                }
                else if (www.downloadHandler.text.Contains("first"))
                    callback(2);
                else
                    callback(3);
            }
        }
    }

    public IEnumerator LoadProfile(int userId, Action<bool> callback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + userId + "/loadProfile"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                if (Main.instance.response.message.Contains("success"))
                {
                    Debug.Log(Main.instance.response.data);
                    Main.instance.userInfo = JsonUtility.FromJson<UserInfo>(Main.instance.response.data);
                    if (callback != null)
                        callback(true);
                }
            }
        }
    }

    public IEnumerator ChangeAvatar(int userId, int avatarId, Action<bool> callback)
    {
        string param = "{\"avatarId\":"+ avatarId + ",\"userId\":"+userId+"}";

        byte[] bytes = Encoding.UTF8.GetBytes(param);

        UnityWebRequest www = UnityWebRequest.Put(url + "changeAvatar", param);
        www.uploadHandler.contentType = "application/json";

        using (www)
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("success")) callback(true);
            }
        }
    }

    public IEnumerator UnlockAvatar(int userId,int avatarId,int accId,int coin, System.Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId",userId);
        form.AddField("avatarId",avatarId);
        form.AddField("accId",accId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "unlockAvatar",form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);
                if (Main.instance.response.message.Contains("success"))
                {
                    Debug.Log(coin);
                    Main.instance.userInfo.coin = coin;
                    callback(true);
                }
            }
        }
    }

    public IEnumerator GetAvatars(int userId, System.Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + userId + "/getAvatars"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);
                if (Main.instance.response.message.Contains("success"))
                {
                    Debug.Log(Main.instance.response.data);
                    string jsonArray = JsonHelper.fixJson(Main.instance.response.data);

                    Debug.Log(jsonArray);
                    Main.instance.avatars = JsonHelper.FromJson<Avatar>(jsonArray);

                    foreach (Avatar avatar in Main.instance.avatars)
                    {
                        Debug.Log(avatar.avatar_id);
                    }
                    callback(true);
                }
            }
        }
    }

    public IEnumerator GetBadges(int userId, System.Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url + userId + "/getAllBadges"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);
                if (Main.instance.response.message.Contains("success"))
                {
                    Debug.Log(Main.instance.response.data);
                    string jsonArray = JsonHelper.fixJson(Main.instance.response.data);

                    Debug.Log(jsonArray);
                    Main.instance.badges = JsonHelper.FromJson<Badge>(jsonArray);

                    foreach (Badge badge in Main.instance.badges)
                    {
                        Debug.Log(badge.id);
                    }
                    callback(true);
                }
            }
        }
    }

    public IEnumerator UnlockReportBadge(int accId, Action<bool> callback) {
        WWWForm form = new WWWForm();
        form.AddField("accId",accId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "unlockReportBadge",form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);

                if (Main.instance.response.message.Contains("success"))
                {
                    Main.instance.hasNewUnlock = true;
                    Debug.Log("new report badge:" + Main.instance.hasNewUnlock);
                }
                callback(true);
            }
        }
    }

    public IEnumerator UnlockAvatarBadge(int accId, Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("accId", accId);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "unlockAvatarBadge", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);

                if (Main.instance.response.message.Contains("success")) {
                    Main.instance.hasNewUnlock = true;
                    Debug.Log("new avatar badge:" + Main.instance.hasNewUnlock);
                }
                callback(true);
            }
        }
    }

    public IEnumerator UnlockCoinBadge(int accId,int coin, Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("accId", accId);
        form.AddField("coin", coin);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "unlockCoinBadge", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);

                if (Main.instance.response.message.Contains("success"))
                {
                    Main.instance.hasNewUnlock = true;
                    Debug.Log("new coin badge:" + Main.instance.hasNewUnlock);
                    callback(true);
                }
            }
        }
    }

    public IEnumerator UpdateScore(int userId, int highScore, int coin, Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        //form.AddField(name of field, data of field)
        form.AddField("userId", userId);
        form.AddField("coin", coin);
        form.AddField("highScore", highScore);
        Debug.Log(coin + "coins");

        using (UnityWebRequest www = UnityWebRequest.Post(url + "updateScore", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Main.instance.response = JsonUtility.FromJson<HTTPResponse>(www.downloadHandler.text);
                Debug.Log(Main.instance.response.message);

                if (Main.instance.response.message.Contains("success"))
                {
                    callback(true);
                }
            }
        }
    }

    public IEnumerator SendMessage(int accId, int score, string message, Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        //form.AddField(name of field, data of field)
        form.AddField("accId", accId);
        form.AddField("score", score);
        form.AddField("message", message);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "sendMessage", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.responseCode.Equals(201))
                {
                    callback(true);
                }
                else
                {
                    callback(false);
                }
            }
        }
    }

    public IEnumerator SendReport(int accId, Report report, Action<bool> callback)
    {
        WWWForm form = new WWWForm();
        //form.AddField(name of field, data of field)
        form.AddField("accId", accId);
        form.AddField("bodyPart", report.body);
        form.AddField("level", report.level);
        form.AddField("description", report.description);
        form.AddField("duration", report.duration);
        form.AddField("mood", report.mood);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "sendReport", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                callback(true);
            }
        }
    }

    public IEnumerator ShowTutorial(int userId, bool isSkipped, Action<bool> callback)
    {
        string param = "";
        if(isSkipped)
            param = "{\"userId\":" + userId + ",\"isSkipped\":" + 1 + "}";
        else
            param = "{\"userId\":" + userId + ",\"isSkipped\":" + 0 + "}";

        byte[] bytes = Encoding.UTF8.GetBytes(param);

        Debug.Log(param);

        UnityWebRequest www = UnityWebRequest.Put(url + "showTutorial", param);
        www.uploadHandler.contentType = "application/json";

        using (www)
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Contains("success")) callback(true);
            }
        }
    }

}
