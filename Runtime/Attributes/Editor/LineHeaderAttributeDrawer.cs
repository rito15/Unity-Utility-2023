
using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 15.
    /// <para/> 헤더(굵은 글씨) + 라인 한 줄
    /// </summary>
    [CustomPropertyDrawer(typeof(LineHeaderAttribute))]
    public class LineHeaderAttributeDrawer : DecoratorDrawer
    {
        LineHeaderAttribute AttributeInfo => attribute as LineHeaderAttribute;

        public override float GetHeight() => base.GetHeight() * 2 + 5f;

        public override void OnGUI(Rect position)
        {
            Color col = AttributeInfo.MainColor.Ex_Convert();

            float textX = position.x - 5f;
            float textY = position.y + base.GetHeight();
            float textWidth  = 
                AttributeInfo.SpaceCount * 4f + 
                AttributeInfo.EngCount * 10f + 
                AttributeInfo.HanGeulCount * 16f;
            float textHeight = base.GetHeight();

            float lineX = textX + textWidth;
            float lineY = position.y + 27f;
            float lineWidth  = position.width + 3f - textWidth;
            float lineHeight = 2f;


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


            // Draw Line
            EditorGUI.DrawRect(new Rect(lineX, lineY, lineWidth, lineHeight), new Color(col.r, col.g, col.b, 0.5f));
        }
    }
}
