using System;

[Serializable]
public struct SerializedPair<TK, TV>
{
    public TK Key;
    public TV Value;

    public SerializedPair(TK key, TV value)
    {
        Key = key;
        Value = value;
    }
}