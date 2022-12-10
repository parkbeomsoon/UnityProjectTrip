using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordClass
{
    string _userName;
    string _record;
    int _uuid;

    public RecordClass(string userName, string record)
    {
        _userName = userName;
        _record = record;
    }
    public string GetName()
    {
        return _userName;
    }
    public string GetRecord()
    {
        return _record;
    }
}

