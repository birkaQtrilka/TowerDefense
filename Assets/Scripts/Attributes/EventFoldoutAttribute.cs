using System;
using UnityEngine;


/// <summary>
///  Adds a foldout to unity events so your inspector doesn't get cluttered   
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class EventFoldoutAttribute : PropertyAttribute
{
    
}
