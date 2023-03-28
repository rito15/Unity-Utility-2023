using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-14 PM 7:54:51
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - 이름 규칙 변경: 항상 Ex_ 접두어 사용
*/

namespace Rito.ut23.Extensions
{
    public static class GameObjectExtension
    {
        [TestCompleted(2021, 06, 16)]
        public static T Ex_GetOrAddComponent<T>(this GameObject @this)
            where T : Component
        {
            if (!@this.TryGetComponent(out T component))
            {
                component = @this.AddComponent<T>();
            }

            return component;
        }
    }
}