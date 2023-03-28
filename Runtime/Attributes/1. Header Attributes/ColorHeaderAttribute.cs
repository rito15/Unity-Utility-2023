
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 다양한 색상의 굵은 글씨 헤더 달아주기
    /// </summary>
    public class ColorHeaderAttribute : HeaderAttributeBase
    {
        public string HeaderText { get; private set; } = "";

        public EColor MainColor { get; private set; }

        public ColorHeaderAttribute(string headerText, EColor color)
        {
            HeaderText = headerText;
            MainColor = color;
        }
    }
}
