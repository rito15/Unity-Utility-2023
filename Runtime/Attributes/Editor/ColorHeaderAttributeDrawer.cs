
using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 다양한 색상의 굵은 글씨 헤더 달아주기
    /// </summary>
    [CustomPropertyDrawer(typeof(ColorHeaderAttribute))]
    public class ColorHeaderAttributeDrawer : DecoratorDrawer
    {
        ColorHeaderAttribute AttributeInfo => attribute as ColorHeaderAttribute;

        public override float GetHeight() => base.GetHeight() * 2 + 5f;

        public override void OnGUI(Rect position)
        {
            Color col = AttributeInfo.MainColor.Ex_Convert();

            float textX = position.x - 5f;
            float textY = position.y + base.GetHeight();
            float textWidth = position.width;
            float textHeight = base.GetHeight();

            // Remember Olds
            Color oldStyleTextColor = EditorStyles.boldLabel.normal.textColor;
            int oldTextSize = EditorStyles.boldLabel.fontSize;

            // Draw Text
            EditorStyles.boldLabel.normal.textColor = col;
            EditorStyles.boldLabel.fontSize = 15;

            EditorGUI.LabelField(new Rect(textX, textY, textWidth, textHeight),
                AttributeInfo.HeaderText, EditorStyles.boldLabel);

            // Restore Olds
            EditorStyles.boldLabel.normal.textColor = oldStyleTextColor;
            EditorStyles.boldLabel.fontSize = oldTextSize;
        }
    }
}
