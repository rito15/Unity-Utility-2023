using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ 기록]
   2020. 01. 22. IsNull, NotNull 메소드 추가
   2020. 05. 01. object -> UnityEngine.Object 타입으로 변경
   2023. 03. 26.
   - Ut23 라이브러리에 편입
*/

namespace Rito.ut23
{

    /// <summary> 오브젝트 다중 검사 </summary>
    public static class Checker
    {
        #region NullCheck

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// </summary>
        public static bool IsNullAny<T>(params T[] targets) where T : UnityEngine.Object
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null) return true;
            }
            return false;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// </summary>
        public static bool IsNullAny(params UnityEngine.Object[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null) return true;
            }
            return false;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 false 리턴
        /// <para/> 파라미터들이 모두 Null이 아니면 true 리턴
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// </summary>
        public static bool NotNullAll<T>(params T[] targets) where T : UnityEngine.Object
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 파라미터들이 하나라도 Null이면 true 리턴
        /// <para/> 파라미터들이 모두 Null이 아니면 true 리턴
        /// <para/> * 왼쪽 파라미터부터 순차적 검사
        /// </summary>
        public static bool NotNullAll(params UnityEngine.Object[] targets)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    return false;
            }
            return true;
        }

        #endregion // ==========================================================

    }
}