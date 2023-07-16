using System;
using UnityEditor;
using UnityEngine;
using Util;

namespace CustomEditor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Length: PropertyAttribute
    {
        public readonly int min;
        public readonly int max;

        public Length(int min = 0, int max = int.MaxValue)
        {
            this.min = min;
            this.max = max;
        } 
    }

    public static class LengthExtender
    {
        public static void Get(this Length value)
        {
            
        }
    }

    [CustomPropertyDrawer(typeof(Length))]
    public class LengthDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.HelpBox(position, "" + property.GetType(), MessageType.Info);
            if (!property.isArray) return;

            if (attribute is Length length && (property.arraySize < length.min || property.arraySize > length.max))
            {
                EditorGUI.HelpBox(position, $"Array has to be within {length.min} and {length.max}", MessageType.Error);
            }
        }
    }
}