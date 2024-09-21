using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;


public class DataSender : MonoBehaviour
{
    [SerializeField] private Text loginText;
    [SerializeField] private Text passwordLoginText;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private Text loginErrorText;
    [SerializeField] private Text registerText;
    [SerializeField] private Text passwordRegisterText;
    [SerializeField] private GameObject registerPage;
    [SerializeField] private Text registerErrorText;
    [SerializeField] private Text welcomeUserText;
    [SerializeField] private GameObject changeUserButton;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private Text userPlace;
    
    void Start()
    {
        if (GameData.currentUser != "")
        {
            loginPage.SetActive(false);
            welcomeUserText.text = "Привет, " + GameData.currentUser + "!";
            changeUserButton.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AttemptLogin()
    {
        if (loginText.text != "" && passwordLoginText.text != "")
        {
            string requestJson = "{\"login\": \"" + loginText.text + "\", \"password\": \"" + passwordLoginText.text + "\"}";
            StartCoroutine(PostData("login", requestJson));
        }
    }

    public void AttemptRegister()
    {
        if (registerText.text != "" && passwordRegisterText.text != "")
        {
            string requestJson = "{\"login\": \"" + registerText.text + "\", \"password\": \"" + passwordRegisterText.text + "\"}";
            StartCoroutine(PostData("register", requestJson));
        }
    }

    public void ChangeUser()
    {
        GameData.currentUser = "";
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetLeaderboard(string url)
    {
        StopCoroutine("GetData");
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        userPlace.text = "Ваше место: -";
        StartCoroutine(GetData(url));
    }

    private IEnumerator PostData(string url, string json)
    {
        var request = new UnityWebRequest("http://192.168.56.10:8000/" + url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            ResponseClass dataDictionary = JsonUtility.FromJson<ResponseClass>(request.downloadHandler.text);
            switch (url)
            {
                case "login":
                    if (dataDictionary.status == "success")
                    {
                        GameData.currentUser = JsonUtility.FromJson<UserData>(json).login;
                        loginPage.SetActive(false);
                        welcomeUserText.text = "Привет, " + GameData.currentUser + "!";
                        changeUserButton.SetActive(true);
                    }
                    else
                    {
                        StartCoroutine(ShowError(loginErrorText));
                    }
                    break;
                case "register":
                    if (dataDictionary.status == "success")
                    {
                        GameData.currentUser = JsonUtility.FromJson<UserData>(json).login;
                        registerPage.SetActive(false);
                        welcomeUserText.text = "Привет, " + GameData.currentUser + "!";
                        changeUserButton.SetActive(true);
                    }
                    else
                    {
                        StartCoroutine(ShowError(registerErrorText));
                    }
                    break;
            }
        }
    }

    private IEnumerator ShowError(Text text)
    {
        float k = 1f;
        while (k > 0f)
        {
            text.color = Color.red * k;
            k -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator GetData(string url)
    {
        var request = new UnityWebRequest("http://192.168.56.10:8000/leaderboard/" + url, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            ResponseClass dataDictionary = JsonUtility.FromJson<ResponseClass>(request.downloadHandler.text);
            if (dataDictionary.status == "success")
            {
                GameObject newText;
                Text textComponent;
                for (int i = 0; i < dataDictionary.keys.Count; i++)
                {
                    newText = Instantiate(textPrefab, contentPanel);
                    textComponent = newText.GetComponent<Text>();
                    textComponent.text = (i + 1) + ". " + dataDictionary.keys[i];
                    newText = Instantiate(textPrefab, contentPanel);
                    textComponent = newText.GetComponent<Text>();
                    textComponent.text = dataDictionary.values[i];
                    if (dataDictionary.keys[i] == GameData.currentUser)
                    {
                        userPlace.text = "Ваше место: " + (i + 1);
                    }
                    if (i % 10 == 0)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
            }
        }
    }
}
