using UnityEngine;
using UnityEditor;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 날짜 : 2020-05-20 AM 1:20:26
    /// <para/> 대상을 BoxHeader 배경색과 같은 색상으로 하이라이트
    /// </summary>
    [CustomPropertyDrawer(typeof(BoxHighlightAttribute), true)]
    public class BoxHighlightAttributeDrawer : DecoratorDrawer
    {
        BoxHighlightAttribute Atr => attribute as BoxHighlightAttribute;

        public override float GetHeight() => 0f;

        public override void OnGUI(Rect position)
        {
            float X = position.x - 5f;
            float Y = position.y;
            float Width = position.width + 5f;
            float Height = base.GetHeight(); // 또는 position.height

            //Color bCol =
            //    EColorConverter.Convert(Atr.BoxColor).Ex_SetAlpha(0.2f).Ex_PlusRGB(-0.1f);
            Color bCol = EColorConverter.Convert(Atr.BoxColor);
            bCol.a = 0.2f;
            bCol = new Color(bCol.r - 0.1f, bCol.g - 0.1f, bCol.b - 0.1f);

            EditorGUI.DrawRect(new Rect(X, Y, Width, Height), bCol);
        }
    }
}