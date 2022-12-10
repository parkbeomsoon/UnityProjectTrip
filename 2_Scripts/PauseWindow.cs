using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    GameManager _gm;

    void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }
    public void OnClickResume()
    {
        if (_gm.cc.isDead)
        {
            Destroy(gameObject);
        }
        else
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
    public void OnClickRestart()
    {
        _gm.ResetGame();
        Destroy(gameObject);
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
