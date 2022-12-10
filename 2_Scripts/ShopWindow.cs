using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : MonoBehaviour
{
    [SerializeField] GameObject[] _priceTextObjs;
    [SerializeField] GameObject[] _purchasedTextObjs;
    [SerializeField] GameObject _shortCoinMeesage;

    int[] prices = { 100, 500, 1000 };

    void Awake()
    {
        SetPrice();
        DetectItemData();
        _shortCoinMeesage.SetActive(false);
    }

    void SetPrice()
    {
        for(int i = 0; i < prices.Length; i++)
        {
            _priceTextObjs[i].GetComponent<Text>().text = prices[i].ToString();
        }
    }

    public void OnClickCharacter1()
    {
        int CharacNo = 1;
        PurchaseItem(CharacNo);
    }
    public void OnClickCharacter2()
    {
        int CharacNo = 2;
        PurchaseItem(CharacNo);
    }
    public void OnClickCharacter3()
    {
        int CharacNo = 3;
        PurchaseItem(CharacNo);
    }

    public void OnClickExit()
    {
        Destroy(gameObject);
    }

    void PurchaseItem(int CharacNo)
    {
        DataManager dm = DataManager.GetIntstance();
        if (dm.dataClass._coin >= prices[CharacNo - 1])
        {
            dm.dataClass._coin -= prices[CharacNo - 1];

            dm.dataClass._purchasedCharacters.Add(CharacNo);
            dm.dataClass._nowCharacter = CharacNo;

            GameManager.GetIntstance().ChangeCharacterSprite(CharacNo);
            dm.FileSave();
            _priceTextObjs[CharacNo - 1].SetActive(false);
            _purchasedTextObjs[CharacNo - 1].SetActive(true);
        }
        else
        {
            StartCoroutine(OpenShortCoinMsg());
        }
    }
    void DetectItemData()
    {
        for(int i = 0; i < _priceTextObjs.Length; i++)
        {
            _priceTextObjs[i].SetActive(true);
            _purchasedTextObjs[i].SetActive(false);
        }

        foreach (int no in DataManager.GetIntstance().dataClass._purchasedCharacters)
        {
            if (no == 0) continue;

            int idxItem = no - 1;
            _priceTextObjs[idxItem].SetActive(false);
            _purchasedTextObjs[idxItem].SetActive(true);

            _priceTextObjs[idxItem].transform.parent.GetComponent<Button>().interactable = false;
        }
    }
    IEnumerator OpenShortCoinMsg()
    {
        if (_shortCoinMeesage.activeSelf) yield return null;
        else
        {
            _shortCoinMeesage.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _shortCoinMeesage.SetActive(false);
        }
    }
}
