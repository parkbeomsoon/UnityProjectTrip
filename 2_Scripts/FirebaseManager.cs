using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

/*
 * 데이터베이스 구조
 * 테이블 - UserList
 * 하위 - 유저명
 * "Record": 기록(float)
 */

public class FirebaseManager : MonoBehaviour
{
    static FirebaseManager _instance;
    public static FirebaseManager GetIntstance() 
    {
        Init();
        return _instance; 
    }

    DatabaseReference _ref;

    void Awake()
    {
        _ref = FirebaseDatabase.DefaultInstance.RootReference;
        Init();
    }

    void Start()
    {
        //WriteData("aaaa",15.52f);
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = FindObjectOfType<FirebaseManager>().gameObject;
            if(go == null)
            {
                go = new GameObject { name = "FirebaseManager" };
                go.AddComponent<FirebaseManager>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<FirebaseManager>();
        }
    }
    public void WriteRecord(string record)
    {
        _ref.Child("UserList").Child(DataManager.GetIntstance().dataClass
            ._uuid.ToString()).Child("Record").SetValueAsync(record);
    }
    public void CreateUser(DataManager.Data characterData, long nextUUID)
    {
        _ref.Child("UserList").Child(characterData._uuid.ToString())
            .Child("Name").SetValueAsync(characterData._userName.ToString());
        _ref.Child("UserList").Child(characterData._uuid.ToString())
            .Child("Record").SetValueAsync(string.Format($"{characterData._bestTime:N2}"));

        _ref.Child("UUID_Instance").Child("PublishID").SetValueAsync(nextUUID.ToString());
    }
}
