
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 16.
    /// <para/> 무지갯빛 헤더 (자동으로 색상 변화)
    /// </summary>
    public class RainbowHeaderAttribute : HeaderAttributeBase
    {
        public string HeaderText { get; private set; } = "";

        public RainbowHeaderAttribute(string headerText)
        {
            HeaderText = headerText;
        }
    }
}
