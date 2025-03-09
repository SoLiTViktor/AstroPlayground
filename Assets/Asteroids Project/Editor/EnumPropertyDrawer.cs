using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public abstract class EnumPropertyDrawer<T> : PropertyDrawer where T : Enum
{
    private readonly GUIContent[] options = Enum.GetNames(typeof(T))
        .Select(x => new GUIContent(x.Replace("_", "/"))).ToArray();
        
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects)
            return base.GetPropertyHeight(property, label);
        
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.serializedObject.isEditingMultipleObjects)
        {
            MethodInfo defaultDraw = typeof(EditorGUI).GetMethod("DefaultPropertyField", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            defaultDraw.Invoke(null, new object[3] { position, property, label });
            return;
        }
            
        EditorGUI.BeginProperty(position, label, property);

        var current = property.enumValueIndex;
        var selected = EditorGUI.Popup(position, label, current, options);
        property.enumValueIndex = selected;
        
        EditorGUI.EndProperty();
    }
}
#endif