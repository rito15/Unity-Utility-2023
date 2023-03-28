using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;
using System.Collections.Generic;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 2020-05-18 PM 3:29:49
    /// <para/> 
    /// </summary>
    [CustomPropertyDrawer(typeof(TagDropDownAttribute), true)]
    public class TagDropDownAttributeDrawer : PropertyDrawer
    {
        TagDropDownAttribute Atr => attribute as TagDropDownAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private int selected = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (EditorHelper.CheckDuplicatedAttribute<DropDownAttributeBase>(fieldInfo))
            {
                EditorGUI.HelpBox(position, "DropDownAttribute는 중복 사용될 수 없습니다",
                     MessageType.Error);
                return;
            }

            if (!property.propertyType.Equals(SerializedPropertyType.String))
            {
                EditorGUI.PropertyField(position, property, label, true);
                EditorGUILayout.HelpBox($"[TagDropDownAttribute] string 타입에만 적용할 수 있습니다.", MessageType.Error);
                Debug.LogError("[TagDropDownAttribute] string 타입에만 적용할 수 있습니다.");
                return;
            }

            float widthLeft = position.width * 0.403f;
            float widthRight = position.width * 0.597f;

            // 유니티엔진 내 모든 태그 가져오기
            List<string> tagList = new List<string>();
            tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

            // 1. 좌측 레이블 그리기
            EditorGUI.LabelField(new Rect(position.x, position.y, widthLeft, position.height),
                label);

            // 2. 우측 팝업 그리기
            selected = EditorGUI.Popup(new Rect(position.x + widthLeft, position.y, widthRight, position.height),
                selected, tagList.ToArray());

            // 3. 값 할당
            property.stringValue = tagList[selected];
        }
    }
}