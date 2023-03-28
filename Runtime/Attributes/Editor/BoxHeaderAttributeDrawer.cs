
using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 15.
    /// <para/> 헤더 한 줄 작성 + 여러 필드를 하나의 컬러 박스로 묶어주기
    /// </summary>
    [CustomPropertyDrawer(typeof(BoxHeaderAttribute))]
    public class BoxHeaderAttributeDrawer : DecoratorDrawer
    {
        BoxHeaderAttribute AttributeInfo => attribute as BoxHeaderAttribute;

        public override float GetHeight() => 35f; // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격

        public override void OnGUI(Rect position)
        {
            float headerHeight = 20f;
            float oneControlHeight = 20f;
            float boxHeight =
                headerHeight +
                oneControlHeight * (AttributeInfo.FieldCount)
                + AttributeInfo.BottomHeight + 5f; // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격

            float X = position.x - 5f;
            float Y = position.y + (GetHeight() - headerHeight - 5f); // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격
            float width = position.width + 5f;


            Rect headerRect = new Rect(X, Y, width, headerHeight);
            Rect boxRect = new Rect(X, Y, width, boxHeight);


            Color hCol = EColorConverter.Convert(AttributeInfo.HeaderColor);
            Color bCol = AttributeInfo.BoxColor == EColor.None ?
                hCol : 
                EColorConverter.Convert(AttributeInfo.BoxColor);


            // 색상 보정
            hCol = hCol.Ex_PlusRGB(-0.6f);
            bCol = bCol.Ex_SetAlpha(0.2f).Ex_PlusRGB(-0.1f);

            // Color Picker(Option)
            if (AttributeInfo.UseColorPicker)
            {
                hCol = EditorGUI.ColorField(new Rect(X + width - 42f, Y, 20f, 16f), 
                    GUIContent.none, hCol, false, true, false);

                bCol = EditorGUI.ColorField(new Rect(X + width - 21f, Y, 20f, 16f), 
                    GUIContent.none, bCol, false, true, false);
            }

            // Header Small Box Color
            EditorGUI.DrawRect(headerRect, new Color(bCol.r, bCol.g, bCol.b, 0.5f));

            // Content Box Color
            // - 필드 카운트가 존재할 경우
            if (AttributeInfo.FieldCount > 0)
            {
                EditorGUI.DrawRect(boxRect, bCol);
            }

            // Remember Olds
            Color oldStyleTextColor = EditorStyles.boldLabel.normal.textColor;
            int oldTextSize = EditorStyles.boldLabel.fontSize;


            // Custom Text Color
            EditorStyles.boldLabel.normal.textColor = hCol;
            EditorStyles.boldLabel.fontSize = 15;

            // Header Label
            EditorGUI.LabelField(new Rect(X + 2f, Y, width, headerHeight), 
                AttributeInfo.HeaderText, EditorStyles.boldLabel);


            // Restore Olds
            EditorStyles.boldLabel.normal.textColor = oldStyleTextColor;
            EditorStyles.boldLabel.fontSize = oldTextSize;
        }
    }
}
