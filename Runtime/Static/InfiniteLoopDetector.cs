using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-05 AM 4:11:57
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
*/

namespace Rito.ut23
{
    /// <summary> 무한 루프 검사 및 방지(에디터 전용) </summary>
    public static class InfiniteLoopDetector
    {
        private static string prevPoint = "";
        private static int detectionCount = 0;
        private const int DetectionThreshold = 100000;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Run(
            [System.Runtime.CompilerServices.CallerMemberName] string mn = "",
            [System.Runtime.CompilerServices.CallerFilePath] string fp = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int ln = 0
        )
        {
            string currentPoint = $"{fp}:{ln}, {mn}()";

            if (prevPoint == currentPoint)
                detectionCount++;
            else
                detectionCount = 0;

            if (detectionCount > DetectionThreshold)
                throw new Exception($"Infinite Loop Detected: \n{currentPoint}\n\n");

            prevPoint = currentPoint;
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void Init()
        {
            UnityEditor.EditorApplication.update += () =>
            {
                detectionCount = 0;
            };
        }
#endif
    }
}