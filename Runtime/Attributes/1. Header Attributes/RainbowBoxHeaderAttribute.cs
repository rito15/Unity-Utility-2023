
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 무지갯빛 헤더박스 
    /// </summary>
    public class RainbowBoxHeaderAttribute : HeaderAttributeBase
    {
        public string HeaderText { get; private set; } = "";

        // 몇 줄의 필드를 묶을 것인지 결정
        public int FieldCount { get; private set; } = 1;

        // 추가 하단 높이
        public float BottomHeight { get; private set; } = 0f;

        public RainbowBoxHeaderAttribute(string headerText, int fieldCount,
            float bottomHeight = 0f)
        {
            HeaderText = headerText;
            FieldCount = fieldCount;
            BottomHeight = bottomHeight;

            if (FieldCount < 1)
                FieldCount = 1;

            if (BottomHeight < 0f)
                BottomHeight = 0f;
        }
    }
}
