#pragma warning disable CS1591

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-02-26 AM 5:10:04
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - 이름 규칙 변경: 항상 Ex_ 접두어 사용
*/

namespace Rito.ut23.Extensions
{
    using static CommonDefinitions;
    using Conditional = System.Diagnostics.ConditionalAttribute;

    public static class DebugExtension
    {
        private static readonly StringBuilder sBuilder = new StringBuilder();

        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .
        private static string BuildString(params object[] objs)
        {
            sBuilder.Clear();
            foreach (var o in objs)
            {
                sBuilder.Append(o);
            }
            return sBuilder.ToString();
        }
        
        #endregion
        /***********************************************************************
        *                               Log
        ***********************************************************************/
        #region .
        [TestCompleted(2021, 06, 17)]
#if !RITO_UT23_ALLOW_RUNTIME_DEBUG
        [Conditional(UNITY_EDITOR)]
#endif
        public static void Ex_Log<T>(this T @this)
        {
            Debug.Log(@this);
        }

        [TestCompleted(2021, 06, 17)]
#if !RITO_UT23_ALLOW_RUNTIME_DEBUG
        [Conditional(UNITY_EDITOR)]
#endif
        public static void Ex_Log<T>(this T @this, params object[] postfix)
        {
            Debug.Log(@this + BuildString(postfix));
        }

        [TestCompleted(2021, 06, 17)]
#if !RITO_UT23_ALLOW_RUNTIME_DEBUG
        [Conditional(UNITY_EDITOR)]
#endif
        public static void Ex_LogError<T>(this T @this)
        {
            Debug.LogError(@this);
        }

        [TestCompleted(2021, 06, 17)]
#if !RITO_UT23_ALLOW_RUNTIME_DEBUG
        [Conditional(UNITY_EDITOR)]
#endif
        public static void Ex_LogError<T>(this T @this, params object[] postfix)
        {
            Debug.LogError(@this + BuildString(postfix));
        }
        #endregion
        /***********************************************************************
        *                               Assert
        ***********************************************************************/
        #region .
        /// <summary>
        /// 값을 평가하여, 틀릴 경우 콘솔 에러 출력
        /// </summary>
        [TestCompleted(2021, 06, 17)]
#if !RITO_UT23_ALLOW_RUNTIME_DEBUG
        [Conditional(UNITY_EDITOR)]
#endif
        public static void Ex_Assert<T>(this T @this, T other)
        {
            if (!@this.Equals(other))
                Debug.LogError($"Assertion Failed : [{@this}] must be [{other}]");
        }

        #endregion

    }
}