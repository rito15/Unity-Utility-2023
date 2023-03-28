
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary> 
    /// <para/> 날짜 : 2020-05-15 PM 11:06:36
    /// <para/> 설명 : 한줄 쫙 그어주기
    /// </summary>
    
    public class LineAttribute : PropertyAttribute
    {
        public Color lineColor = Color.white;

        public LineAttribute() { }
        public LineAttribute(EColor color)
        {
            lineColor = EColorConverter.Convert(color);
        }
        public LineAttribute(float r, float g, float b) => lineColor = new Color(r, g, b);
        public LineAttribute(int r, int g, int b) => lineColor = new Color(r / 256f, g / 256f, b / 256f);
    }
}
