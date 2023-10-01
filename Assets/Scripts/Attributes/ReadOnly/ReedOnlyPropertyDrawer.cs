using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReedOnlyPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
