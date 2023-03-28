using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;
using System.Collections.Generic;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 2020-05-18 AM 12:07:17
    /// <para/> 스트링 - 사용 가능한 레이어 드롭다운 표시
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerDropDownAttribute), true)]
    public class LayerDropDownAttributeDrawer : PropertyDrawer
    {
        LayerDropDownAttribute Atr => attribute as LayerDropDownAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        int selected = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (EditorHelper.CheckDuplicatedAttribute<DropDownAttributeBase>(fieldInfo))
            {
                EditorGUI.HelpBox(position, "DropDownAttribute는 중복 사용될 수 없습니다",
                     MessageType.Error);
                return;
            }

            if (!property.propertyType.Equals(SerializedPropertyType.Integer) &&
                !property.propertyType.Equals(SerializedPropertyType.String))
            {
                EditorGUI.PropertyField(position, property, label, true);
                EditorGUILayout.HelpBox($"[LayerDropDownAttribute] int 또는 string 타입에만 적용할 수 있습니다.", MessageType.Error);
                Debug.LogError("[LayerDropDownAttribute] int 또는 string 타입에만 적용할 수 있습니다.");
                return;
            }

            float widthLeft = position.width * 0.403f;
            float widthRight = position.width * 0.597f;

            // 유니티엔진 내 모든 레이어 가져오기
            // 0번 : Default, 8 ~ 31번 : 커스텀 레이어
            List<string> layerList = new List<string>() { "Default" };
            for (int i = 8; i < 32; i++)
            {
                string layer = UnityEngine.LayerMask.LayerToName(i);
                if (string.IsNullOrEmpty(layer) == false)
                    layerList.Add(layer);
            }

            // 1. 좌측 레이블 그리기
            EditorGUI.LabelField(new Rect(position.x, position.y, widthLeft, position.height),
                label);

            // 2. 우측 팝업 그리기
            selected = EditorGUI.Popup(new Rect(position.x + widthLeft, position.y, widthRight, position.height),
                selected, layerList.ToArray());

            // 3. 값 할당
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = selected == 0 ? 0 : selected + 7;
                    break;

                case SerializedPropertyType.String:
                    property.stringValue = layerList[selected];
                    break;
            }
        }
    }
}