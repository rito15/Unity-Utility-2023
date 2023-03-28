
using UnityEngine;
using UnityEditor;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 무지갯빛 헤더 (자동으로 색상 변화)
    /// </summary>
    [CustomPropertyDrawer(typeof(RainbowHeaderAttribute))]
    public class RainbowHeaderAttributeDrawer : DecoratorDrawer
    {
        RainbowHeaderAttribute AttributeInfo => attribute as RainbowHeaderAttribute;

        public override float GetHeight() => base.GetHeight() * 2 + 5f;

        private float currentHue = 0.00f;

        public override void OnGUI(Rect position)
        {
            float textX = position.x - 5f;
            float textY = position.y + base.GetHeight();
            float textWidth = position.width;
            float textHeight = base.GetHeight();

            // Remember Olds
            Color oldStyleTextColor = EditorStyles.boldLabel.normal.textColor;
            int oldTextSize = EditorStyles.boldLabel.fontSize;

            // Draw Text
            EditorStyles.boldLabel.normal.textColor = Color.HSVToRGB(currentHue, 1, 1);
            EditorStyles.boldLabel.fontSize = 15;

            EditorGUI.LabelField(new Rect(textX, textY, textWidth, textHeight),
                AttributeInfo.HeaderText, EditorStyles.boldLabel);

            // Restore Olds
            EditorStyles.boldLabel.normal.textColor = oldStyleTextColor;
            EditorStyles.boldLabel.fontSize = oldTextSize;

            currentHue += 0.01f;
            if (currentHue >= 1.00f)
                currentHue = 0f;
        }
    }
}
