using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Content/BoolAsset")]
public class BoolAsset : ScriptableObject
{
    public bool value;

    public void SetValue(bool _value)
    {
        value = _value;
    }

    public bool GetValue() => value;
}
