using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterWindow : MonoBehaviour
{
    [SerializeField] InputField _namefield;

    public void OnClickCreateButton()
    {
        DataManager.GetIntstance().dataClass._userName = _namefield.text;

        FirebaseDatabase.DefaultInstance.GetReference("UUID_Instance").
            GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    var newUUID = snapshot.Child("PublishID").Value;
                    long longUUID = long.Parse(newUUID.ToString());

                    DataManager.GetIntstance().dataClass._uuid = longUUID;
                    DataManager.GetIntstance().FileSave();

                    FirebaseManager.GetIntstance()
                        .CreateUser(DataManager.GetIntstance().dataClass, longUUID + 1);
                }
            });

        Destroy(gameObject);
    }
}
