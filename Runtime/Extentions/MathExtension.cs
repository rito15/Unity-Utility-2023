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
     - 이름 규칙 변경: 항상 Ex_ 접두어 사용
     - Clamp() 메소드 종류 추가
*/

namespace Rito.ut23.Extensions
{
    public static class MathExtension
    {
        /***********************************************************************
        *                           int - Range, Clamp
        ***********************************************************************/
        #region .
        /// <summary> (min &lt;= value &lt;= max) </summary>
        [TestCompleted(2023, 03, 28)]
        public static bool Ex_InRange(in this int value, in int min, in int max)
            => min <= value && value <= max;

        /// <summary> (min &lt; value &lt; max) </summary>
        [TestCompleted(2023, 03, 28)]
        public static bool Ex_ExRange(in this int value, in int min, in int max)
            => min < value && value < max;

        /// <summary> (min &lt;= value &lt; max) </summary>
        [TestCompleted(2023, 03, 26)]
        public static bool Ex_InExRange(in this int value, in int min, in int max)
            => min <= value && value < max;

        /// <summary> (min &lt; value &lt;= max) </summary>
        [TestCompleted(2023, 03, 26)]
        public static bool Ex_ExInRange(in this int value, in int min, in int max)
            => min < value && value <= max;


        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2023, 03, 28)]
        public static int Ex_Clamp(in this int value, in int min, in int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        /// <summary> 값의 최소 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static int Ex_ClampMin(in this int value, in int min)
        {
            if (value < min) return min;
            return value;
        }
        /// <summary> 값의 최대 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static int Ex_ClampMax(in this int value, in int max)
        {
            if (value > max) return max;
            return value;
        }


        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2023, 03, 28)]
        public static int Ex_ClampRef(ref this int value, in int min, in int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        /// <summary> 값의 최소 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static int Ex_ClampMinRef(ref this int value, in int min)
        {
            if (value < min) value = min;
            return value;
        }
        /// <summary> 값의 최대 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static int Ex_ClampMaxRef(ref this int value, in int max)
        {
            if (value > max) value = max;
            return value;
        }
        #endregion

        /***********************************************************************
        *                           float - Range, Clamp
        ***********************************************************************/
        #region .
        /// <summary> (min &lt;= value &lt;= max) </summary>
        [TestCompleted(2021, 06, 17)]
        public static bool Ex_InRange(in this float value, in float min, in float max)
            => min <= value && value <= max;

        /// <summary> (min &lt; value &lt; max) </summary>
        [TestCompleted(2021, 06, 17)]
        public static bool Ex_ExRange(in this float value, in float min, in float max)
            => min < value && value < max;

        /// <summary> (min &lt;= value &lt; max) </summary>
        [TestCompleted(2023, 03, 26)]
        public static bool Ex_InExRange(in this float value, in float min, in float max)
            => min <= value && value < max;

        /// <summary> (min &lt; value &lt;= max) </summary>
        [TestCompleted(2023, 03, 26)]
        public static bool Ex_ExInRange(in this float value, in float min, in float max)
            => min < value && value <= max;


        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static float Ex_Clamp(in this float value, in float min, in float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        /// <summary> 값의 최소 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static float Ex_ClampMin(in this float value, in float min)
        {
            if (value < min) return min;
            return value;
        }
        /// <summary> 값의 최대 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static float Ex_ClampMax(in this float value, in float max)
        {
            if (value > max) return max;
            return value;
        }


        /// <summary> 값의 범위 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static float Ex_ClampRef(ref this float value, in float min, in float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        /// <summary> 값의 최소 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static float Ex_ClampMinRef(ref this float value, in float min)
        {
            if (value < min) value = min;
            return value;
        }
        /// <summary> 값의 최대 범위 제한 </summary>
        [TestCompleted(2023, 03, 26)]
        public static float Ex_ClampMaxRef(ref this float value, in float max)
        {
            if (value > max) value = max;
            return value;
        }


        /// <summary> 0 ~ 1 범위로 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static float Ex_Saturate(in this float value)
        {
            if (value < 0f) return 0f;
            if (value > 1f) return 1f;
            return value;
        }

        /// <summary> 0 ~ 1 범위로 제한 </summary>
        [TestCompleted(2021, 06, 17)]
        public static void Ex_SaturateRef(ref this float value)
        {
            if (value < 0f) value = 0f;
            if (value > 1f) value = 1f;
        }

        #endregion
    }
}