using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    [CustomPropertyDrawer(typeof(LabelAttribute), true)]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        LabelAttribute AttributeInfo => attribute as LabelAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType.IsArray)
            {
                Debug.LogWarning("배열 타입에는 사용할 수 없습니다");
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            // 1. 예전 스타일 기억
            Color oldLabelNormalTextColor = EditorStyles.label.normal.textColor;

            // 2. 원하는 스타일 적용
            Color textColor = AttributeInfo.TextColor.Ex_Convert();
            EditorStyles.label.normal.textColor = textColor;
            EditorStyles.label.hover.textColor = textColor;
            EditorStyles.label.focused.textColor = textColor;

            // 3. 원하는 컨트롤에 적용
            EditorGUI.PropertyField(position, property, new GUIContent(AttributeInfo.Label), true);
            //EditorGUILayout.PropertyField(property, new GUIContent(AttributeInfo.Label), true);

            // 4. 다시 예전 스타일 복원
            EditorStyles.label.normal.textColor = oldLabelNormalTextColor;

        }
    }
}