using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 날짜 : 2020-05-20 AM 1:18:21
    /// <para/> 대상을 BoxHeader 배경색과 같은 색상으로 하이라이트
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class BoxHighlightAttribute : PropertyAttribute
    {
        public EColor BoxColor { get; private set; }

        public BoxHighlightAttribute(EColor color) => BoxColor = color;
    }
}