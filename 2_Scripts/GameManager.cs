using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum Platform
    {
        PC          = 0,
        Mobile,
    }

    #region 인스턴스 생성

    static GameManager _instance;
    public static GameManager GetIntstance()
    {
        Init();
        return _instance;
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = FindObjectOfType<GameManager>().gameObject;
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GameManager>();
        }
    }

    #endregion

    #region 오브젝트 변수

    [SerializeField] GameObject StartObj;
    [SerializeField] GameObject TitleObj;
    [SerializeField] GameObject PauseWindow;
    [SerializeField] GameObject NewRecordWindow;
    [SerializeField] GameObject CharacterSelectWindow;
    [SerializeField] GameObject ShopWindow;
    [SerializeField] GameObject RankingWindow;
    [SerializeField] Sprite[] _characterSprite;
    [SerializeField] Text _bestTimeText;
    [SerializeField] Text _coinText;

    #endregion

    #region 컨트롤 변수

    PlanetGenerater generator;
    BackgroundMove bgMove;
    public CharacterController cc;
    Text _timeText;

    public Platform _platform;
    public bool _isStart = false;
    float _time = 0;

    public GameObject _nowWindow;

    #endregion


    void Awake()
    {
        _platform = Platform.PC;
        Init();
        InitGM();
    }
    void InitGM()
    {
        DataManager.GetIntstance().path = Application.persistentDataPath + "/TripAppData.dat";
        DataManager.GetIntstance().dirPath = Application.persistentDataPath;
        DataManager.GetIntstance().FileLoad();

        generator = FindObjectOfType<PlanetGenerater>();
        bgMove = FindObjectOfType<BackgroundMove>();
        cc = FindObjectOfType<CharacterController>();
        StartObj.SetActive(false);
        TitleObj.SetActive(true);

        int nowNo = DataManager.GetIntstance().dataClass._nowCharacter;
        ChangeCharacterSprite(nowNo);

        RefreshData();
    }

    public void GameStart()
    {
        StartObj.SetActive(true);
        TitleObj.SetActive(false);
        _timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<Text>();
        StartCoroutine(StartTime());
        _isStart = true;
        bgMove._isStart = true;
    }
    public void ResetGame()
    {
        DataManager.GetIntstance().FileLoad();
        TitleObj.SetActive(false);
        generator.Reset();
        cc.Reset();
        StartCoroutine(StartTime());
        Time.timeScale = 1;
    }
    IEnumerator StartTime()
    {
        _time = 0f;
        _timeText.text = string.Format($"{_time:N2}");

        while (!cc.isDead)
        {
            _time += Time.deltaTime;
            _timeText.text = string.Format($"{_time:N2}");
            yield return new WaitForEndOfFrame();
        }
        DataManager.GetIntstance().dataClass._coin += (int)_time / 10;
        if (_time > DataManager.GetIntstance().dataClass._bestTime)
        {
            DataManager.GetIntstance().dataClass._bestTime = _time;
            OpenNewRecordWindow(_time);
        }


        DataManager.GetIntstance().FileSave();
        RefreshData();
        TitleObj.SetActive(true);

    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        GameObject go = Instantiate(PauseWindow);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void OnClickShop()
    {
        if(_nowWindow != null)
        {
            Destroy(_nowWindow);
        }
        GameObject go = Instantiate(ShopWindow);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
        _nowWindow = go;
    }
    public void OnClickRanking()
    {
        if (_nowWindow != null)
        {
            Destroy(_nowWindow);
        }
        GameObject go = Instantiate(RankingWindow);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
        _nowWindow = go;
    }
    public void OnClickCharacter()
    {
        if (_nowWindow != null)
        {
            Destroy(_nowWindow);
        }
        GameObject go = Instantiate(CharacterSelectWindow);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
        _nowWindow = go;
    }

    void RefreshData()
    {
        _bestTimeText.text = string.Format($"{DataManager.GetIntstance().dataClass._bestTime:N2}");
        _coinText.text = string.Format($"{DataManager.GetIntstance().dataClass._coin}");
    }
    void OpenNewRecordWindow(float time)
    {
        if (_nowWindow != null)
        {
            Destroy(_nowWindow);
        }
        GameObject go = Instantiate(NewRecordWindow);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
        NewRecordWindow nrw = go.GetComponent<NewRecordWindow>();
        nrw.SetTime(time);
        _nowWindow = go;
    }

    public void ChangeCharacterSprite(int characNo)
    {
        SpriteRenderer _character = GameObject.FindGameObjectWithTag("Character").GetComponent<SpriteRenderer>();
        _character.sprite = _characterSprite[characNo];
        if (characNo != 0) _character.flipX = true;
        else _character.flipX = false;
    }
}
