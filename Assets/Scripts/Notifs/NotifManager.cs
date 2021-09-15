using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Unity.Notifications.Android;
using UnityEngine.UI;

public class NotifManager : MonoBehaviour
{
    //public Text DataText;

    private void SetupDefaultChannel()
    {
        string channel_id = "default";

        string channel_title = "Default Channel"; //Ex Push Notifications

        Importance importance = Importance.Default;

        string channel_description = "Default Channel for the game";

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, channel_title, channel_description, importance);

        //Register your channel
        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    private void SetupRepeatingChannel()
    {
        string channel_id = "repeat";

        string channel_title = "Repeating Channel"; //Ex Push Notifications

        Importance importance = Importance.Default;

        string channel_description = "Repeating Channel for the game";

        AndroidNotificationChannel repeatChannel = new AndroidNotificationChannel(channel_id, channel_title, channel_description, importance);

        //Register your channel
        AndroidNotificationCenter.RegisterNotificationChannel(repeatChannel);
    }

    private void Awake(){
        SetupDefaultChannel();
        SetupRepeatingChannel();

        //If app is open
        AndroidNotificationCenter.CancelAllNotifications();
    }

    public void SendSimpleNotif()
    {
        string notif_Title = "Simple Notif";

        string notif_Message = "The monsters are attacking! Prepare your weapons and survive.";

        DateTime fireTime = DateTime.Now.AddSeconds(10); //10 seconds delay

        AndroidNotification notif = new AndroidNotification(notif_Title, notif_Message, fireTime);

        notif.SmallIcon = "icon";

        AndroidNotificationCenter.SendNotification(notif, "default");
    }

    public void SendRepeatingNotif()
    {
        string notif_Title = "Repeating Notif";

        string notif_Message = "The monsters are attacking! Prepare your weapons and survive.";

        DateTime fireTime = DateTime.Now.AddSeconds(10); //10 seconds delay after pressed

        TimeSpan interval = new TimeSpan(12, 0, 0);

        AndroidNotification notif = new AndroidNotification(notif_Title, notif_Message, fireTime, interval);

        notif.SmallIcon = "icon";

        AndroidNotificationCenter.SendNotification(notif, "repeat");
    }

    //Add icons, must be square -> 128 128, or if big at least 192 x 192
    //Enable read/write for sprite
    //Project Settings -> Mobile Notifications -> Check Reschedule on Start, Add to list -> small

    //public void SendDataNotif()
    //{
    //    string notif_Title = "Data Notif";

    //    string notif_Message = "This is a data notification";

    //    DateTime fireTime = DateTime.Now.AddSeconds(10); //10 seconds delay

    //    AndroidNotification notif = new AndroidNotification(notif_Title, notif_Message, fireTime);

    //    //notif.SmallIcon = "";
    //    //notif.LargeIcon = "";

    //    notif.IntentData = "Data Notif";

    //    AndroidNotificationCenter.SendNotification(notif, "default");
    //}

    //private void CheckIntentData()
    //{
    //    //Pressed notif
    //    AndroidNotificationIntentData data = AndroidNotificationCenter.GetLastNotificationIntent();
    //    //Open in normal way
    //    if(data == null)
    //    {
    //        DataText.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        DataText.gameObject.SetActive(true);
    //        DataText.text = data.Notification.IntentData;
    //    }
    //}
}
