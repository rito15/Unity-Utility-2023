#if RITO_UT23_ALLOW_RUNTIME_DEBUG

#define RITO_UT23_EDITOR_ONLY_SWITCH

#else

#if UNITY_EDITOR
#define RITO_UT23_EDITOR_ONLY_SWITCH
#endif

#endif

using System;
using UnityEngine;
using System.Diagnostics;

using Object = UnityEngine.Object;

// 날짜 : 2021-01-25 PM 2:45:53
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
*/
namespace Rito.ut23.Debugs
{
    using static CommonDefinitions;

    /// <summary> 디버그 래퍼 클래스(플래그에 따라 에디터 전용/런타임 허용) </summary>
    public static class Debug
    {
        /***********************************************************************
        *                               Properties
        ***********************************************************************/
        #region .
        public static ILogger logger => UnityEngine.Debug.unityLogger;
        public static ILogger unityLogger => UnityEngine.Debug.unityLogger;
        public static bool developerConsoleVisible
        {
            get => UnityEngine.Debug.developerConsoleVisible;
            set => UnityEngine.Debug.developerConsoleVisible = value;
        }
        public static bool isDebugBuild => UnityEngine.Debug.isDebugBuild;

        #endregion
        /***********************************************************************
        *                               Mark
        ***********************************************************************/
        #region .
        /// <summary> 메소드 호출 전파 추적용 메소드 </summary>
        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Mark(
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
        )
        {
            int begin = sourceFilePath.LastIndexOf(@"\");
            int end = sourceFilePath.LastIndexOf(@".cs");
            string className = sourceFilePath.Substring(begin + 1, end - begin - 1);

            UnityEngine.Debug.Log($"[Mark] {className}.{memberName}, {sourceLineNumber}");
        }

        #endregion
        /***********************************************************************
        *                               Assert
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition, string message, Object context)
            => UnityEngine.Debug.Assert(condition, message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition)
            => UnityEngine.Debug.Assert(condition);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition, object message, Object context)
            => UnityEngine.Debug.Assert(condition, message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition, string message)
            => UnityEngine.Debug.Assert(condition, message);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition, object message)
            => UnityEngine.Debug.Assert(condition, message);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Assert(bool condition, Object context)
            => UnityEngine.Debug.Assert(condition, context);


        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void AssertFormat(bool condition, Object context, string format, params object[] args)
            => UnityEngine.Debug.AssertFormat(condition, context, format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void AssertFormat(bool condition, string format, params object[] args)
            => UnityEngine.Debug.AssertFormat(condition, format, args);

        #endregion
        /***********************************************************************
        *                               Log
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Log(object message)
            => UnityEngine.Debug.Log(message);

        /// <summary> 메시지들을 ", "로 구분하여 콘솔 창에 출력 </summary>
        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Log(params object[] messages)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for(int i = 0; i < messages.Length; i++)
            {
                sb.Append(messages[i].ToString());

                if(i < messages.Length - 1)
                    sb.Append(", ");
            }

            UnityEngine.Debug.Log(sb);
        }

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Log(object message, Object context)
            => UnityEngine.Debug.Log(message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogFormat(string format, params object[] args)
            => UnityEngine.Debug.LogFormat(format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogFormat(Object context, string format, params object[] args)
            => UnityEngine.Debug.LogFormat(context, format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
            => UnityEngine.Debug.LogFormat(logType, logOptions, context, format, args);


        /// <summary> 콘솔 로그 타입을 지정하여 출력
        /// <para/> - 로그 타입을 Exception으로 지정할 경우, 바로 예외 호출
        /// </summary>
        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Log(object message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Log:     UnityEngine.Debug.Log(message); break;
                case LogType.Warning: UnityEngine.Debug.LogWarning(message); break;

                case LogType.Error:
                case LogType.Assert:
                    UnityEngine.Debug.LogError(message);
                    break;

                case LogType.Exception:
                    throw new Exception(message.ToString());
            }
        }

        #endregion
        /***********************************************************************
        *                               LogAssertion
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogAssertion(object message, Object context)
            => UnityEngine.Debug.LogAssertion(message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogAssertion(object message)
            => UnityEngine.Debug.LogAssertion(message);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogAssertionFormat(Object context, string format, params object[] args)
            => UnityEngine.Debug.LogAssertionFormat(context, format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogAssertionFormat(string format, params object[] args)
            => UnityEngine.Debug.LogAssertionFormat(format, args);

        #endregion
        /***********************************************************************
        *                               LogWarning
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogWarning(object message, Object context)
            => UnityEngine.Debug.LogWarning(message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogWarning(object message)
            => UnityEngine.Debug.LogWarning(message);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogWarningFormat(Object context, string format, params object[] args)
            => UnityEngine.Debug.LogWarningFormat(context, format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogWarningFormat(string format, params object[] args)
            => UnityEngine.Debug.LogWarningFormat(format, args);

        #endregion
        /***********************************************************************
        *                               LogError
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogError(object message, Object context)
            => UnityEngine.Debug.LogError(message, context);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogError(object message)
            => UnityEngine.Debug.LogError(message);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogErrorFormat(Object context, string format, params object[] args)
            => UnityEngine.Debug.LogErrorFormat(context, format, args);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogErrorFormat(string format, params object[] args)
            => UnityEngine.Debug.LogErrorFormat(format, args);

        #endregion
        /***********************************************************************
        *                               LogException
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogException(Exception exception)
            => UnityEngine.Debug.LogException(exception);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void LogException(Exception exception, Object context)
            => UnityEngine.Debug.LogException(exception, context);

        #endregion
        /***********************************************************************
        *                               DrawLine
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawLine(Vector3 start, Vector3 end)
            => UnityEngine.Debug.DrawLine(start, end);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
            => UnityEngine.Debug.DrawLine(start, end, color);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
            => UnityEngine.Debug.DrawLine(start, end, color, duration);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest)
            => UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);

        #endregion
        /***********************************************************************
        *                               DrawRay
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest)
            => UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
            => UnityEngine.Debug.DrawRay(start, dir, color, duration);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color)
            => UnityEngine.Debug.DrawRay(start, dir, color);

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DrawRay(Vector3 start, Vector3 dir)
            => UnityEngine.Debug.DrawRay(start, dir);

        #endregion
        /***********************************************************************
        *                               Etc
        ***********************************************************************/
        #region .

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void Break()
            => UnityEngine.Debug.Break();

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void DebugBreak()
            => UnityEngine.Debug.DebugBreak();

        [Conditional(RITO_UT23_EDITOR_ONLY_SWITCH)]
        public static void ClearDeveloperConsole()
            => UnityEngine.Debug.ClearDeveloperConsole();

        #endregion
    }
}