using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;

public class RankingWindow : MonoBehaviour
{
    [SerializeField] RectTransform _content;
    [SerializeField] Text _playerBestTimeText;
    [SerializeField] Text _myRankText;
    [SerializeField] Text _noDataText;
    [SerializeField] GameObject _recordLinePrefab;

    float myBestTime;

    void Awake()
    {
        ReadAllRecords();
        myBestTime = DataManager.GetIntstance().dataClass._bestTime;
        _playerBestTimeText.text = string.Format($"{myBestTime:N2}");

        if (myBestTime == 0f)
        {
            _noDataText.gameObject.SetActive(true);
            _myRankText.transform.parent.gameObject.SetActive(false);
        }
        else _noDataText.gameObject.SetActive(false);
    }

    public void OnClickCloseButton()
    {
        Destroy(gameObject);
    }

    void ReadAllRecords()
    {
        int myRank = 1;
        List<RecordClass> userList = new List<RecordClass>();
        string myName = DataManager.GetIntstance().dataClass._userName;

        FirebaseDatabase.DefaultInstance.GetReference("UserList").
            GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    //err
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    
                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        string name = snapshot.Child(data.Key).Child("Name").Value.ToString();
                        string rcd = snapshot.Child(data.Key).Child("Record").Value.ToString();
                        float time = float.Parse(rcd);
                        if (time > myBestTime && name != myName) myRank++;

                        RecordClass rc = new RecordClass(name, rcd);
                        userList.Add(rc);
                    }

                    if(myBestTime == 0f)
                    {
                        _noDataText.gameObject.SetActive(true);
                        _myRankText.transform.parent.gameObject.SetActive(false);
                    }
                    _myRankText.text = string.Format($"({myRank}위)");

                    //전체 랭킹
                    userList.Sort((x1, x2) => float.Parse(x2.GetRecord())
                                    .CompareTo(float.Parse(x1.GetRecord())));
                    int cnt = 0;
                    foreach (RecordClass rc in userList)
                    {
                        if (cnt < 10)
                        {
                            if (float.Parse(rc.GetRecord()) != 0)
                            {
                                GameObject go = Instantiate(_recordLinePrefab, _content);
                                RecordLine rl = go.GetComponent<RecordLine>();
                                rl.InitLine(rc.GetName(), rc.GetRecord());
                                cnt++;
                            }
                        }
                        else break;
                    }
                }


            });
    }

}
