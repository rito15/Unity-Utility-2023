using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;
using System;
using System.Reflection;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 2020-05-18 AM 1:12:28
    /// <para/> 인스펙터에 메소드 버튼 표시
    /// </summary>
    [CustomPropertyDrawer(typeof(MethodButtonAttribute), true)]
    public class MethodButtonAttributeDrawer : PropertyDrawer
    {
        MethodButtonAttribute Atr => attribute as MethodButtonAttribute;

        float ControlHeight => (Atr.Height < 18f ? 18f : Atr.Height);

        int TextSize => (Atr.TextSize < 12 ? 12 : Atr.TextSize);

        /// <summary> 필드, 버튼 모두 포함한 컨트롤 개수 </summary>
        int ControlCount => (Atr.ShowField ? 1 : 0) + Atr.MethodNames.Length;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //return EditorGUI.GetPropertyHeight(property, label, true) * ControlCount;
            return ControlHeight * ControlCount;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //float ControlHeight = EditorGUI.GetPropertyHeight(property, label, true);
            int controlIndex = 0;

            Rect[] rects = new Rect[ControlCount];

            for (int i = 0; i < ControlCount; i++)
            {
                rects[i] = new Rect(position.x, position.y + ControlHeight * i, position.width, ControlHeight);
            }


            var target = property.serializedObject.targetObject;

            // 텍스트 색상 지정
            var buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.normal.textColor = EColorConverter.Convert(Atr.TextColor);
            buttonStyle.hover.textColor = EColorConverter.Convert(Atr.TextColor);
            buttonStyle.fontSize = TextSize;

            foreach (var methodName in Atr.MethodNames)
            {
                var method = fieldInfo.DeclaringType.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                    null,
                    new Type[] { },
                    null
                    );

                if (method == null)
                {
                    EditorGUI.HelpBox(rects[controlIndex], $"Error : Not Existed Method Name [{methodName}]", MessageType.Error);
                    //Debug.LogError($"[MethodButtonAttribute] 메소드 이름({methodName})이 잘못되었습니다 \nGameObject : {target.name}\nComponent : {fieldInfo.DeclaringType}\nField : {property.name}\n\n");
                }
                else
                {
                    // 배경 색상 지정
                    if (!Atr.ButtonColor.Equals(EColor.None))
                    {
                        using (new BackgroundColorScope(EColorConverter.Convert(Atr.ButtonColor)))
                        {
                            //if (GUILayout.Button(methodName, buttonStyle))
                            if (GUI.Button(rects[controlIndex], methodName, buttonStyle))
                                method.Invoke(target, null);
                        }
                    }
                    // 무색
                    else
                    {
                        //if (GUILayout.Button(methodName, buttonStyle))
                        if (GUI.Button(rects[controlIndex], methodName, buttonStyle))
                            method.Invoke(target, null);
                    }
                }
                controlIndex++;
            }

            // 원래 필드도 표시
            if (Atr.ShowField)
                EditorGUI.PropertyField(rects[controlIndex], property, label, true);


            //TODO: 버튼 위치가 필드의 위일 수도, 아래일 수도 있게 선택권 제공
        }
    }
}