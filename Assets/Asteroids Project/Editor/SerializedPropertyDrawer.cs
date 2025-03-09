using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AsteroidProject
{
    [CustomPropertyDrawer(typeof(SerializedInterfaceAttribute))]
    public class SerializedPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Type requiredType = (attribute as SerializedInterfaceAttribute).MainType;

            UpdatePropertyValue(property, requiredType);

            UpdateDropIcon(position, requiredType);

            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);
        }


        private bool IsFieldValid()
        {
            return fieldInfo.FieldType == typeof(GameObject) || typeof(IEnumerable<GameObject>).IsAssignableFrom(fieldInfo.FieldType);
        }

        private bool IsInvalidObject(Object CheckingObject, Type type)
        {
            if (CheckingObject is GameObject gameObject)
                return gameObject.GetComponent(type) == null;

            return true;
        }

        private void UpdatePropertyValue(SerializedProperty property, Type type)
        {
            if (property.objectReferenceValue == null)
                return;

            if (IsInvalidObject(property.objectReferenceValue, type))
                property.objectReferenceValue = null;
        }

        private void UpdateDropIcon(Rect position, Type type)
        {
            if (position.Contains(Event.current.mousePosition) == false)
                return;

            foreach (Object reference in DragAndDrop.objectReferences)
                if (IsInvalidObject(reference, type))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                    return;
                }
        }
    }
}
