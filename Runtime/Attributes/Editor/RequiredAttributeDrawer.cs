using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 날짜 : 2020-05-19 PM 8:21:20
    /// <para/> 해당 필드가 반드시 초기화되어야 한다고 경고박스로 알려주기
    /// <para/> * 컴포넌트 타입에만 사용 가능
    /// </summary>
    [CustomPropertyDrawer(typeof(RequiredAttribute), true)]
    public class RequiredAttributeDrawer : PropertyDrawer
    {
        RequiredAttribute Atr => attribute as RequiredAttribute;

        System.Type FieldType => fieldInfo.FieldType;

        float Height => 18f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (Atr.ShowMessageBox ? Height * 2.5f : Height);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect firstThird = new Rect(position.x, position.y + Height * 0.5f, position.width, Height);
            Rect midThird = new Rect(position.x, position.y + Height * 1.5f, position.width, Height);

            Rect errorRect = new Rect(position.x, position.y + Height * 0.5f, position.width, Height * 2f);

            // 타입 제한 : 컴포넌트, 게임오브젝트
            if (FieldType.Ex_IsChildOrEqualsTo(typeof(Component)) == false && 
                FieldType.IsEquivalentTo(typeof(GameObject)) == false)
            {
                // 배열, 리스트면 크기를 0으로 고정하고 콘솔 에러 메시지
                if (FieldType.Ex_IsArrayOrListType())
                {
                    fieldInfo.SetValue(property.serializedObject.targetObject, null);
                    Debug.LogError("[Required] 배열 또는 리스트에는 사용할 수 없습니다.");
                }

                if (!Atr.ShowMessageBox)
                {
                    EditorHelper.ColorErrorBox(position, $"[Required - Error] Component 타입이 아닙니다. - 대상 타입 : {FieldType}");
                }
                else
                {
                    EditorHelper.ColorErrorBox(errorRect, $"[Required - Error] Component 타입이 아닙니다.\n대상 타입 : {FieldType}");
                }

                return;
            }


            // 1-1. 경고 - null이거나 MisMatch
            // Type Mismatch까지 검사(참조가 들어있는 상태에서 스크립트 내의 타입을 바꿔버린 경우)
            if (property.objectReferenceValue == null ||
            property.objectReferenceValue.GetType().Ex_IsChildOrEqualsTo(FieldType) == false)
            {
                using (new TextColorScope(Color.red))
                {
                    if (Atr.ShowMessageBox)
                    {
                        // ★ Missing Reference 잡아주기
                        property.objectReferenceValue = null;

                        EditorHelper.ColorErrorBox(firstThird, "[Required Component]");
                        EditorGUI.PropertyField(midThird, property, label, true);
                    }
                    else
                    {
                        EditorGUI.PropertyField(position, property, label, true);
                    }
                }
            }

            // 1-2. 평소 상태
            else
            {
                using (new TextColorScope(Color.blue))
                {
                    if (Atr.ShowMessageBox)
                    {
                        EditorHelper.ColorInfoBox(firstThird, Color.green, "[Required Component]");

                        EditorGUI.PropertyField(midThird, property, label, true);
                    }
                    else
                    {
                        EditorGUI.PropertyField(position, property, label, true);
                    }
                }
            }

        }

        public class TextColorScope : GUI.Scope
        {
            // Origins
            private readonly Color objectField_normal;
            private readonly Color objectField_focused;
            private readonly Color label_normal;
            private readonly Color label_hover;
            private readonly Color label_focused;

            public TextColorScope(Color color)
            {
                objectField_normal = EditorStyles.objectField.normal.textColor;
                objectField_focused = EditorStyles.objectField.focused.textColor;
                label_normal = EditorStyles.label.normal.textColor;
                label_hover = EditorStyles.label.hover.textColor;
                label_focused = EditorStyles.label.focused.textColor;

                EditorStyles.objectField.normal.textColor = color;
                EditorStyles.objectField.focused.textColor = color;
                EditorStyles.label.normal.textColor = color;
                EditorStyles.label.hover.textColor = color;
                EditorStyles.label.focused.textColor = color;
            }

            protected override void CloseScope()
            {
                //EditorStyles.objectField.normal.textColor = objectField_normal;
                //EditorStyles.objectField.focused.textColor = objectField_focused;
                //EditorStyles.label.normal.textColor = label_normal;
                //EditorStyles.label.hover.textColor = label_hover;
                //EditorStyles.label.focused.textColor = label_focused;
                EditorStyles.objectField.normal.textColor = Color.black;
                EditorStyles.objectField.focused.textColor = Color.black;
                EditorStyles.label.normal.textColor = Color.black;
                EditorStyles.label.hover.textColor = Color.black;
                EditorStyles.label.focused.textColor = Color.black;
            }
        }
    }
}