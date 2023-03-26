using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 날짜 : 2023-03-26 PM 3:32:23
// 작성자 : Rito

namespace Rito.ut23
{
    /// <summary> 
    /// 공통으로 사용될 상수 정의
    /// </summary>
    public static class CommonDefinitions
    {
        public const string UNITY_EDITOR = "UNITY_EDITOR";
        public const string RITO_UT23_EDITOR_ONLY_SWITCH = "RITO_UT23_EDITOR_ONLY_SWITCH";

        // 에디터 정의 플래그 //
        // 에디터에서 전역으로 선언할 경우, 빌드 환경에서도 디버그 허용
        public const string RITO_UT23_ALLOW_RUNTIME_DEBUG = "RITO_UT23_ALLOW_RUNTIME_DEBUG";
    }
}