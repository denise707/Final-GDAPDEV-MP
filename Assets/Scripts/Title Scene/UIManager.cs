using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject TextCopy;
    public GameObject TextParent;

    bool retrieved = false;

    //USER PROFILE VARIABLES

    //Pop Up Windows
    public GameObject PlayerDetails;
    public GameObject NotFound;
    public GameObject UserProfile;

    //Search
    public InputField iF_search_nickname;
    public InputField iF_search_email;

    string search_nickname = "";
    string search_email = "";

    //Retrieved
    public static string retrieved_id = "";
    string retrieved_nickname = "";
    string retrieved_name = "";
    string retrieved_email = "";
    string retrieved_contact = "";

    //To create
    public InputField iF_create_nickname;
    public InputField iF_create_name;
    public InputField iF_create_email;
    public InputField iF_create_contact;

    public static string create_nickname = "";
    public static string create_name = "";
    public static string create_email = "";
    public static string create_contact = "";

    //Edit
    public InputField iF_edit_nickname;
    public InputField iF_edit_name;
    public InputField iF_edit_email;
    public InputField iF_edit_contact;

    // Start is called before the first frame update
    void Start()
    {
        WebHandlerScript.Instance.GetPlayers();
        WebHandlerScript.Instance.GetUsers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }
   
    public void onExit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void onCloseWindow(GameObject name)
    {
        name.SetActive(false);
    }

    public void onOpenWindow(GameObject name)
    {
        name.SetActive(true);
    }

    public void onLeaderboards(GameObject LeaderboardsMenu)
    {
        if (!retrieved)
        {
            foreach (Dictionary<string, string> player in WebHandlerScript.Instance.leaderboardsList)
            {
                GameObject newPlayer = GameObject.Instantiate(this.TextCopy, this.TextParent.transform); ;
                newPlayer.GetComponent<Text>().text = ("    " + player["user_name"]).ToString() + " - " + (player["score"]).ToString();
            }

            retrieved = true;
        }

        TextCopy.SetActive(false);
        StartCoroutine(OpenLeaderboards(LeaderboardsMenu));
    }

    public IEnumerator OpenLeaderboards(GameObject LeaderboardsMenu)
    {
        yield return new WaitForSeconds(5f);
        LeaderboardsMenu.SetActive(true);
    }

    //----USER PROFILE

    public void OpenUserProfile(GameObject window)
    {
        iF_search_nickname.text = "";
        iF_search_email.text = "";

        WebHandlerScript.Instance.GetUsers();
        window.SetActive(true);
    }

    public void onSearch()
    {
        bool found = false;
        
        search_nickname = iF_search_nickname.text;
        search_email = iF_search_email.text;

        foreach (Dictionary<string, string> user in WebHandlerScript.Instance.userList)
        {
            if(user["nickname"] == search_nickname && user["email"] == search_email)
            {
                retrieved_id = user["id"];
                retrieved_nickname = user["nickname"];
                retrieved_name = user["name"];
                retrieved_email = user["email"];
                retrieved_contact = user["contact"];

                found = true;
            }
        }

        if (found)
        {
            PlayerDetails.transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                $"Nickname: {retrieved_nickname} \n " +
                $"Name: {retrieved_name} \n " +
                $"Email: {retrieved_email} \n" +
                $"Contact: {retrieved_contact}";
            PlayerDetails.SetActive(true);
        }
        else
        {
            NotFound.SetActive(true);
        }
    }

    public void onCreate(GameObject window)
    {
        create_nickname = iF_create_nickname.text;
        create_name = iF_create_name.text;
        create_email = iF_create_email.text;
        create_contact = iF_create_contact.text;

        WebHandlerScript.Instance.CreateUser();

        UserProfile.SetActive(false);

        window.SetActive(false);
    }

    public void onEdit(GameObject window)
    {
        create_nickname = iF_edit_nickname.text;
        create_name = iF_edit_name.text;
        create_email = iF_edit_email.text;
        create_contact = iF_edit_contact.text;

        WebHandlerScript.Instance.EditPlayer();

        UserProfile.SetActive(false);
        PlayerDetails.SetActive(false);

        window.SetActive(false);
    }
}
