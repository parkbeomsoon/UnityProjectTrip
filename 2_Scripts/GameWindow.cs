using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    [SerializeField] GameObject _keyText;
    [SerializeField] GameObject _mobileButton;

    void Start()
    {
        if (GameManager.GetIntstance()._platform == GameManager.Platform.PC)
        {
            _keyText.gameObject.SetActive(true);
            _mobileButton.SetActive(false);
        }
        else
        {
            _keyText.gameObject.SetActive(false);
            _mobileButton.SetActive(true);
        }
    }

}
