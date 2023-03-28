
//using UnityEngine;
//using UnityEditor;
//using Rito.ut23.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Rito.ut23.Attributes
//{
//    /// <summary>
//    /// <para/> 2020-05-18 PM 9:15:30
//    /// <para/> Component 상속 타입 변수들에 자동으로 초기화해주는 기능
//    /// </summary>
//    [CustomPropertyDrawer(typeof(AutoInjectDropDownAttribute), true)]
//    public class AutoInjectDropDownAttributeDrawer : PropertyDrawer
//    {
//        AutoInjectDropDownAttribute Atr => attribute as AutoInjectDropDownAttribute;

//        private float Height { get; set; } = 20f;

//        private int SelectedIndex = 0;

//        private bool OptionChanged { get; set; } = false;

//        /// <summary> 애트리뷰트에서 전달받음 </summary>
//        private EInspectorInjection InjectOption { get; set; } = EInspectorInjection.None;

//        /// <summary> 인스펙터에서 선택함 </summary>
//        private EInspectorInjection prevInjectOption { get; set; } = EInspectorInjection.None;

//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            //return EditorGUI.GetPropertyHeight(property, label, true);
//            return Atr.Option == EInjection.SelectInInspector ? Height * 4f : Height * 3f;
//        }

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            Rect infoRect = new Rect(position.x, position.y, position.width, Height);
//            Rect optionRect = default;
//            Rect propRect = new Rect(position.x, position.y + Height, position.width, Height);

//            Rect ErrorRect = new Rect(position.x, position.y, position.width, Height * 2f);

//            Type fieldType = fieldInfo.FieldType;

//            // 인스펙터에서 선택을 해야 하는 경우
//            if (Atr.Option == EInjection.SelectInInspector)
//            {
//                optionRect = propRect;
//                propRect = new Rect(position.x, position.y + Height * 2f, position.width, Height);
//                ErrorRect = new Rect(position.x, position.y, position.width, Height * 3f);
//            }

//            // 컴포넌트가 아닌 타입 - 에러박스
//            if (fieldType.IsSubclassOf(typeof(Component)) == false)
//            {
//                // 배열, 리스트면 크기를 0으로 고정하고 콘솔 에러 메시지
//                if (fieldInfo.FieldType.Ex_IsArrayOrListType())
//                {
//                    fieldInfo.SetValue(property.serializedObject.targetObject, null);
//                    Debug.LogError("[AutoInject] 배열 또는 리스트에는 사용하실 수 없습니다.");
//                }

//                // 그 외의 경우, 인스펙터에 큼지막한 에러 박스
//                EditorHelper.ColorErrorBox(ErrorRect, "[AutoInject] Component를 상속하는 타입에만 사용할 수 있습니다");
//                return;
//            }


//            // 컴포넌트 타입 - 올바르게 동작 ===========================================================


//            // 인스펙터 선택 드롭박스 넣어주기
//            if (Atr.Option == EInjection.SelectInInspector)
//            {
//                List<string> inspectorOptionLst = new List<string>();
//                var allEnums = Enum.GetValues(typeof(EInspectorInjection)).Cast<EInspectorInjection>();

//                foreach (var e in allEnums)
//                    inspectorOptionLst.Add(e.ToString());


//                SelectedIndex = EditorGUI.Popup(optionRect, SelectedIndex, inspectorOptionLst.ToArray());

//                InjectOption = (EInspectorInjection)SelectedIndex;


//                if (prevInjectOption != InjectOption)
//                {
//                    OptionChanged = true;
//                    //property.objectReferenceValue = null;
//                }

//                prevInjectOption = InjectOption;
//            }
//            else
//            {
//                // 인스펙터 선택이 아닌 경우
//                switch (Atr.Option)
//                {
//                    case EInjection.GetComponent:
//                        InjectOption = EInspectorInjection.GetComponent;
//                        break;
//                    case EInjection.GetComponentInChildren:
//                        InjectOption = EInspectorInjection.GetComponentInChildren;
//                        break;
//                    case EInjection.GetComponentInChildrenOnly:
//                        InjectOption = EInspectorInjection.GetComponentInChildrenOnly;
//                        break;
//                    case EInjection.GetComponentInparent:
//                        InjectOption = EInspectorInjection.GetComponentInparent;
//                        break;
//                    case EInjection.GetComponentInparentOnly:
//                        InjectOption = EInspectorInjection.GetComponentInparentOnly;
//                        break;
//                    case EInjection.FindObjectOfType:
//                        InjectOption = EInspectorInjection.FindObjectOfType;
//                        break;
//                }
//                Debug.Log("냐앙");
//            }

//            // 실행모드 옵션 제한을 설정한 경우
//            switch (Atr.ModeOption)
//            {
//                case EModeOption.EditModeOnly when EditorApplication.isPlaying:
//                    EditorHelper.ColorWarningBox(infoRect, "[AutoInject] 에디터 모드에서만 동작합니다");
//                    EditorGUI.PropertyField(propRect, property, label, true);
//                    return;

//                case EModeOption.PlayModeOnly when !EditorApplication.isPlaying:
//                    EditorHelper.ColorWarningBox(infoRect, "[AutoInject] 플레이 모드에서만 동작합니다");
//                    EditorGUI.PropertyField(propRect, property, label, true);
//                    return;
//            }

//            // 인스펙터 내에서 옵션 미선택 상태
//            if (InjectOption == EInspectorInjection.None)
//            {
//                //property.objectReferenceValue = null;
//                EditorHelper.ColorWarningBox(infoRect, "[AutoInject] 옵션을 선택하세요");
//                EditorGUI.PropertyField(propRect, property, label, true);
//                return;
//            }

//            // 인스펙터에서 지정한 옵션별로 동작 : null일 때만 실행하게 해서 에디터 성능 최적화
//            else if (property.objectReferenceValue == null)
//            {
//                Component target = property.serializedObject.targetObject as Component;

//                switch (InjectOption)
//                {
//                    case EInspectorInjection.GetComponent:
//                        property.objectReferenceValue = target.GetComponent(fieldType);
//                        break;

//                    case EInspectorInjection.GetComponentInChildren:
//                        property.objectReferenceValue = target.GetComponentInChildren(fieldType);
//                        break;

//                    case EInspectorInjection.GetComponentInChildrenOnly:
//                        property.objectReferenceValue = target.Ex_GetComponentInChildrenOnly(fieldType);
//                        break;

//                    case EInspectorInjection.GetComponentInparent:
//                        property.objectReferenceValue = target.GetComponentInParent(fieldType);
//                        break;

//                    case EInspectorInjection.GetComponentInparentOnly:
//                        property.objectReferenceValue = target.Ex_GetComponentInParentOnly(fieldType);
//                        break;

//                    case EInspectorInjection.FindObjectOfType:
//                        property.objectReferenceValue = GameObject.FindObjectOfType(fieldType);
//                        break;
//                }

//                Debug.Log($"U1 : {SelectedIndex}, InjectOption : {InjectOption}, Prev : {prevInjectOption}");
//                SelectedIndex = (int)InjectOption;
//                Debug.Log($"U2 : {SelectedIndex}, InjectOption : {InjectOption}, Prev : {prevInjectOption}");

//                OptionChanged = false;
//            }


//            // 실행 결과 - 성공
//            if (property.objectReferenceValue != null)
//            {
//                using (new BackgroundColorScope(Color.green))
//                {
//                    EditorGUI.HelpBox(infoRect,
//                    $"[AutoInject - Succeeded] {Atr.Option} : {fieldType.Ex_ToStringSimple()}",
//                    MessageType.Info);
//                }
//            }
//            // 실행 결과 - 실패(대상이 없음)
//            else
//            {
//                using (new BackgroundColorScope(Color.yellow))
//                {
//                    EditorGUI.HelpBox(infoRect,
//                    $"[AutoInject - Failed(Not Existed Target)] {Atr.Option} : {fieldType.Ex_ToStringSimple()}",
//                    MessageType.Warning);
//                }
//            }
//            EditorGUI.PropertyField(propRect, property, label, true);
//        }
//    }
//}