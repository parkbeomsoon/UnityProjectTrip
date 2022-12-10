using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] GameObject _createWindow;
    #region 인스턴스 생성
    static DataManager _instance;
    public static DataManager GetIntstance()
    {
        Init();
        return _instance;
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = FindObjectOfType<DataManager>().gameObject;
            if (go == null)
            {
                go = new GameObject { name = "DataManager" };
                go.AddComponent<DataManager>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<DataManager>();
        }
    }
    #endregion

    void Awake()
    {
        Init();
    }

    #region 파일데이터

    public string path;
    public string dirPath;

    public Data dataClass;

    [System.Serializable]
    public class Data
    {
        public string _userName = string.Empty;
        public long _uuid = 0;
        public float _bestTime = 0f;
        public int _coin = 0;
        public List<int> _purchasedCharacters = new List<int>();
        public int _nowCharacter = 0;
    }
    public void FileSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.OpenOrCreate);

        bf.Serialize(fs, dataClass);
        fs.Close();
    }
    public void FileLoad()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(path, FileMode.Open);
            Data data = (Data)bf.Deserialize(fs);
            fs.Close();

            dataClass = data;
        }
        else
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            Data data = new Data();
            data._userName = string.Empty;
            data._uuid = 0;
            data._bestTime = 0f;
            data._coin = 0;
            data._nowCharacter = 0;
            data._purchasedCharacters = new List<int>();
            data._purchasedCharacters.Add(0);
            dataClass = data;

            GameObject go = Instantiate(_createWindow);
            go.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
    #endregion
}
