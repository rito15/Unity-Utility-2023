
using UnityEngine;
using UnityEditor;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 무지갯빛 반짝반짝 헤더박스(상시 색상 변경)
    /// </summary>
    [CustomPropertyDrawer(typeof(RainbowBoxHeaderAttribute))]
    public class RainbowBoxHeaderAttributeDrawer : DecoratorDrawer
    {
        RainbowBoxHeaderAttribute AttributeInfo => attribute as RainbowBoxHeaderAttribute;

        public override float GetHeight() => 35f; // 5f : 헤더 <-> 첫번째 컨트롤 사이 간격

        private float currentHue = 0.00f;

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


            // Header Color, Box Color
            Color hCol = Color.HSVToRGB(currentHue, 1, 1);
            Color bCol = new Color(hCol.r, hCol.g, hCol.b, 0.2f);

            // RainbowHeader Small Box Color
            EditorGUI.DrawRect(headerRect, new Color(bCol.r, bCol.g, bCol.b, 0.4f));

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

            // Rainbow Plus !
            currentHue += 0.01f;
            if (currentHue > 1f)
                currentHue = 0f;
        }
    }
}
