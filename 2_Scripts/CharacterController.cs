using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float _rotSpeed = 50f;
    public bool isDead = false;
    public float _jumpPow = 5f;

    Vector2 firstPos;
    Rigidbody2D _rigid;
    GameManager _gm;

    void Awake()
    {
        firstPos = GetComponent<RectTransform>().anchoredPosition;
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.isKinematic = true;
        _gm = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        if (_gm._platform == GameManager.Platform.PC)
        {
            #region PC
            if (isDead)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_gm._nowWindow != null)
                    {
                        NewRecordWindow nw;
                        if ((nw = _gm._nowWindow.GetComponent<NewRecordWindow>()) != null)
                        {
                            if (!nw._closeMessageActive)
                            {
                                nw.OnClickRegistButton();
                            }
                            else
                                Destroy(_gm._nowWindow);
                        }
                        else
                            Destroy(_gm._nowWindow);
                    }

                    _gm.ResetGame();
                    if (_gm._nowWindow != null) Destroy(_gm._nowWindow);
                }
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!_gm._isStart)
                {
                    _rigid.isKinematic = false;
                    _gm.GameStart();
                }
                else _rigid.AddForce(Vector3.up * _jumpPow, ForceMode2D.Impulse);

                if (_gm._nowWindow != null) Destroy(_gm._nowWindow);
            }
        #endregion
        }
    }

    #region Mobile
    public void OnClickJump()
    {
        if (_gm._nowWindow != null) 
        {
            NewRecordWindow nw;
            if ((nw = _gm._nowWindow.GetComponent<NewRecordWindow>()) != null)
            {
                if (!nw._closeMessageActive)
                {
                    nw.OnClickRegistButton();
                }
                else
                    Destroy(_gm._nowWindow);
            }
            else
                Destroy(_gm._nowWindow); 
        }
        if (isDead)
        {
            _gm.ResetGame();
        }
        else
        {
            if (!_gm._isStart)
            {
                _rigid.isKinematic = false;
                _gm.GameStart();
            }
            else _rigid.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        }
    }
    #endregion

    public void Reset()
    {
        GetComponent<RectTransform>().anchoredPosition = firstPos;
        isDead = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0;
        isDead = true;
    }
}
