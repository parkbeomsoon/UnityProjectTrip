using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordLine : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _time;

    public void InitLine(string name, string time)
    {
        _name.text = name;
        _time.text = time;
    }
}
