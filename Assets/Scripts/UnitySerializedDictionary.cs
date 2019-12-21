using UnityEngine;
using System;
using System.Collections.Generic;


public class SerializationCallbackScript : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<Building> _keys = new List<Building>();
    public List<float> _values = new List<float>();

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<Building, float> _myDictionary = new Dictionary<Building, float>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in _myDictionary)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _myDictionary = new Dictionary<Building, float>();

        for (var i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            _myDictionary.Add(_keys[i], _values[i]);
    }

    void OnGUI()
    {
        foreach (var kvp in _myDictionary)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }
}