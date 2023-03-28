
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 15.
    /// <para/> 헤더(굵은 글씨) + 라인 한 줄
    /// </summary>
    public class LineHeaderAttribute : HeaderAttributeBase
    {
        public string HeaderText { get; private set; } = "";

        public EColor MainColor { get; private set; }

        public int EngCount = 0;
        public int HanGeulCount = 0;
        public int SpaceCount = 0;

        public LineHeaderAttribute(string headerText, EColor color = EColor.Black)
        {
            HeaderText = headerText;
            MainColor = color;

            char[] txtArray = headerText.ToCharArray();
            foreach (var txt in txtArray)
            {
                if (txt == ' ')
                    SpaceCount++;
                else if (char.GetUnicodeCategory(txt) == System.Globalization.UnicodeCategory.OtherLetter)
                    HanGeulCount++;
                else
                    EngCount++;
            }
        }
    }
}
