using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectWindow : MonoBehaviour
{
    [SerializeField] GameObject[] _items;
    [SerializeField] Image[] _checkImages;
    [SerializeField] GameObject _noItemMessage;

    int _nowSelectedCharacterNo = 0;

    void Awake()
    {
        InitWindow();
        _noItemMessage.SetActive(false);
    }

    void InitWindow()
    {
        DataManager dm = DataManager.GetIntstance();

        if (dm.dataClass._purchasedCharacters.Count != 0)
        {
            List<int> purchasedChar = dm.dataClass._purchasedCharacters;
            foreach(int characterNo in purchasedChar)
            {
                _items[characterNo].transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        foreach (Image img in _checkImages)
        {
            img.gameObject.SetActive(false);
        }

        //현재 선택된 캐릭터
        _nowSelectedCharacterNo = dm.dataClass._nowCharacter;
        _checkImages[_nowSelectedCharacterNo].gameObject.SetActive(true);
    }

    public void OnClickCharacter0()
    {
        int no = 0;
        Change(no);
    }
    public void OnClickCharacter1()
    {
        int no = 1;
        Change(no);
    }
    public void OnClickCharacter2()
    {
        int no = 2;
        Change(no);
    }
    public void OnClickCharacter3()
    {
        int no = 3;
        Change(no);
    }
    public void OnClickExit()
    {
        Destroy(gameObject);
    }

    void Change(int characNo)
    {
        if (DataManager.GetIntstance().dataClass._purchasedCharacters.Contains(characNo))
        {
            _nowSelectedCharacterNo = characNo;
            foreach (Image img in _checkImages)
            {
                img.gameObject.SetActive(false);
            }
            _checkImages[characNo].gameObject.SetActive(true);
            GameManager.GetIntstance().ChangeCharacterSprite(characNo);

            //캐릭터 변경 후 데이터 저장
            DataManager.GetIntstance().dataClass._nowCharacter = characNo;
            DataManager.GetIntstance().FileSave();
        }
        else
        {
            StartCoroutine(OpenNoItemMsg());
        }
    }

    IEnumerator OpenNoItemMsg()
    {
        if (_noItemMessage.activeSelf) yield return null;
        else
        {
            _noItemMessage.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _noItemMessage.SetActive(false);
        }
    }
}
