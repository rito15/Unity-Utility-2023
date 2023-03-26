using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-05 AM 3:22:50
// 작성자 : Rito

/*
    [목록]

    - InExclusiveRange(min, max) : 값이 열린 구간 범위에 있는지 검사
    - InInclusiveRange(min, max) : 값이 닫힌 구간 범위에 있는지 검사
    - Clamp(min, max) : 값의 범위 제한
    - Saturate() : 값을 0 ~ 1 사이로 제한
*/
/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - 이름 규칙 변경: 항상 EX_ 접두어 사용
*/

namespace Rito.ut23.Extensions
{
    public static class MathExtension
    {
        /***********************************************************************
        *                               Range, Clamp
        ***********************************************************************/
        #region .
        /// <summary> Inclusive Range (min &lt;= value &lt;= max) </summary>
        [TestCompleted(2021, 06, 17)]
        public static bool EX_InRange(in this float value, in float min, in float max)
            => min <= value && value <= max;

        /// <summary> Exclusive Range (min &lt; value &lt; max) </summary>
        [TestCompleted(2021, 06, 17)]
        public static bool EX_ExRange(in this float value, in float min, in float max)
            => min < value && value < max;

        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static float Clamp(in this float value, in float min, in float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static void EX_ClampRef(ref this float value, in float min, in float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
        }

        /// <summary> 0 ~ 1 범위로 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static float EX_Saturate(in this float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }

        /// <summary> 0 ~ 1 범위로 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static void EX_SaturateRef(ref this float value)
        {
            if (value < 0f) value = 0f;
            if (value > 1f) value = 1f;
        }

        #endregion
    }
}