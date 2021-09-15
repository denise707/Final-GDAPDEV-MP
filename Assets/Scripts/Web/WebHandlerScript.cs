using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;


public class WebHandlerScript : MonoBehaviour
{
    public static WebHandlerScript Instance;
    public List<Dictionary<string, string>> leaderboardsList;
    public List<Dictionary<string, string>> userList;

    void Awake()
    {
        if (Instance == null)
        {
            //Assign this instance of Gesture Manager
            Instance = this;
        }
        else
        {
            //Destroy other duplicates in scene
            Destroy(gameObject);
        }
    }

    public string BaseURL
    {
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    public void DeletePlayer()
    {
        StartCoroutine(SampleDeletePlayerRoutine());
    }

    //Delete player
    IEnumerator SampleDeletePlayerRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/25", "DELETE");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");
        //Check if there are no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    public void GetPlayers()
    {
        StartCoroutine(SampleGetPlayersRoutine());
    }

    //Getting player from web
    IEnumerator SampleGetPlayersRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/8", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");
        //Check if there are no errors
        if(string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a list of Dictionaries
            List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject<
                                                    List<Dictionary<string, string>>
                                                    >(request.downloadHandler.text);
            //Iterate for testing
            foreach (Dictionary<string, string> player in playerList)
            {
                Debug.Log($"Got player: {player["user_name"]}");
            }

            leaderboardsList = playerList;
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    public void CreatePlayer()
    {
        StartCoroutine(SamplePostRoutine());
    }

    //Creating new player
    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("group_num", "8");
        PlayerParams.Add("user_name", PlayerSystem.player_name);
        PlayerParams.Add("score", (PlayerSystem.score).ToString());

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        //Turn JSON to binary
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-----USER PROFILE
    public void GetUsers()
    {
        StartCoroutine(SampleGetUsersRoutine());
    }

    //Getting player from web
    IEnumerator SampleGetUsersRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");
        //Check if there are no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a list of Dictionaries
            List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject<
                                                    List<Dictionary<string, string>>
                                                    >(request.downloadHandler.text);
            //Iterate for testing
            foreach (Dictionary<string, string> player in playerList)
            {
                //Debug.Log($"Got player: {player["user_name"]}");
            }

            userList = playerList;
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    public void CreateUser()
    {
        StartCoroutine(SampleCreateUser());
    }

    //Creating new player
    IEnumerator SampleCreateUser()
    {
        Dictionary<string, string> PlayerParams = new Dictionary<string, string>();

        PlayerParams.Add("nickname", UIManager.create_nickname);
        PlayerParams.Add("name", UIManager.create_name);
        PlayerParams.Add("email", UIManager.create_email);
        PlayerParams.Add("contact", UIManager.create_contact);

        string requestString = JsonConvert.SerializeObject(PlayerParams);
        //Turn JSON to binary
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    public void EditPlayer()
    {
        StartCoroutine(SampleEditPlayerRoutine());
    }

    //Edit player
    IEnumerator SampleEditPlayerRoutine()
    {
        Dictionary<string, string> PlayerParameters =
            new Dictionary<string, string>();

        //Update player 
        PlayerParameters.Add("nickname", UIManager.create_nickname);
        PlayerParameters.Add("name", UIManager.create_name);
        PlayerParameters.Add("email", UIManager.create_email);
        PlayerParameters.Add("contact", UIManager.create_contact);

        string requestString = JsonConvert.SerializeObject(PlayerParameters);

        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/" + UIManager.retrieved_id, "PUT");

        request.SetRequestHeader("Content-Type", "application/json");

        request.uploadHandler = new UploadHandlerRaw(requestData);

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");
        //Check if there are no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a list of Dictionaries
            Dictionary<string, string> player = JsonConvert.DeserializeObject<
                                                    Dictionary<string, string>
                                                    >(request.downloadHandler.text);

            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.LogError($"Error");
        }
    }

    public void GetOne()
    {
        StartCoroutine(SampleGetOneRoutine());
    }

    //Get one player
    IEnumerator SampleGetOneRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/22", "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response Code: {request.responseCode}");
        //Check if there are no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a list of Dictionaries
            Dictionary<string, string> player = JsonConvert.DeserializeObject<
                                                    Dictionary<string, string>
                                                    >(request.downloadHandler.text);
            //Iterate for testing
            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.LogError($"Error");
        }
    }
}
