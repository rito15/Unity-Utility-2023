
using UnityEngine;
using UnityEditor;

namespace Rito.ut23.Attributes
{
    /// <summary> 
    /// <para/> 날짜 : 2020-05-15 PM 11:06:36
    /// <para/> 설명 : 한줄 쫙 그어주기
    /// </summary>
    [CustomPropertyDrawer(typeof(LineAttribute))]
    public class LineAttributeDrawer : DecoratorDrawer
    {
        LineAttribute LineInfo => attribute as LineAttribute;

        public override float GetHeight() => base.GetHeight();

        public override void OnGUI(Rect position)
        {
            float X = position.x - 5f;
            float Y = position.y + GetHeight() / 2f;
            float width = position.width + 5f;
            float height = 2f;

            EditorGUI.DrawRect(new Rect(X, Y, width, height), LineInfo.lineColor);
        }
    }
}
