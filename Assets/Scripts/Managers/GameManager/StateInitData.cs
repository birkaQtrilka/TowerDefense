using System;
/// <summary>
/// Used so you can set data via inspector, since State<T> cannot be serialized
/// </summary>
[Serializable]
public class StateInitData
{
    //name of type
    [InspectorReadOnly]
    public string Name;

    public int Priority;
    
}