using UnityEngine;

// 작성자 : Rito
// 작성일 : 2023-03-28 AM 0:26:02
/*
   [ 기록]
   2023. 03. 28. 최초 작성, 경과시간/남은시간 기능 테스트 완료
   2023. 04. 03. 객체 생성 직관성을 위해 중간 클래스 Create 추가, IsRunning() 메소드 추가
*/
namespace Rito.ut23
{
    /// <summary>
    /// <para/> 지속시간/쿨타임 체크 타이머 
    /// <para/> [생성]
    /// <para/> 1. Create.Scaled(초) : 타임스케일 영향 받는 타이머 생성
    /// <para/> 2. Create.Unscaled(초) : 타임스케일 영향 받지 않는 타이머 생성
    /// <para/> 
    /// <para/> [API - 공통]
    /// <para/> - IsEnded()  : 종료 여부
    /// <para/> - IsRunning(): 진행 여부
    /// <para/> 
    /// <para/> [API - 지속시간]
    /// <para/> - GetElapsedTime(): 경과시간(초)
    /// <para/> - GetElapsedTimeClamped(): 경과시간(초), 최댓값: 지속시간
    /// <para/> - GetElapsedRatio(): 경과시간(비율: 0~1)
    /// <para/> - GetElapsedRatioClamped(): 경과시간(비율: 0~1), 최댓값: 1
    /// <para/> 
    /// <para/> [API - 쿨타임]
    /// <para/> - GetRemainingTime(): 남은시간(초)
    /// <para/> - GetRemainingTimeClamped(): 남은시간(초), 최솟값: 0
    /// <para/> - GetRemainingRatio(): 남은시간(비율: 0~1)
    /// <para/> - GetRemainingRatioClamped(): 남은시간(비율: 0~1), 최솟값: 0
    /// </summary>
    public static class CoolTimer
    {
        public static class Create
        {
            /// <summary> 타임스케일 영향 O </summary>
            public static ScaledTimer Scaled(float seconds) => new ScaledTimer(seconds);
            /// <summary> 타임스케일 영향 O </summary>
            public static ScaledTimer GameTime(float seconds) => new ScaledTimer(seconds);

            /// <summary> 타임스케일 영향 X </summary>
            public static UnscaledTimer Unscaled(float seconds) => new UnscaledTimer(seconds);
            /// <summary> 타임스케일 영향 X </summary>
            public static UnscaledTimer Realtime(float seconds) => new UnscaledTimer(seconds);
        }

        /// <summary> 타임스케일 영향 O </summary>
        public readonly struct ScaledTimer
        {
            private readonly float _begin;
            private readonly float _duration;
            private static float Current => Time.timeSinceLevelLoad;

            public ScaledTimer(float duration)
            {
                _begin = Current;
                _duration = duration;
            }

            /***********************************************************************
            *                               경과
            ***********************************************************************/
            #region .

            /// <summary> 경과 시간 </summary>
            public float GetElapsedTime()
            {
                return Current - _begin;
            }

            /// <summary> 경과 시간(최댓값: Duration) </summary>
            public float GetElapsedTimeClamped()
            {
                float ret = Current - _begin;
                return ret > _duration ? _duration : ret;
            }

            /// <summary> 경과 시간 비율(0: 시작 ~ 1: 종료) </summary>
            public float GetElapsedRatio()
            {
                return (GetElapsedTime() / _duration);
            }

            /// <summary> 경과 시간 비율(범위 제한: 0 ~ 1) </summary>
            public float GetElapsedRatioClamped()
            {
                float ret = GetElapsedRatio();
                return ret > 1f ? 1f : ret; ;
            }

            #endregion

            /***********************************************************************
            *                               잔여
            ***********************************************************************/
            #region .

            /// <summary> 남은 시간 </summary>
            public float GetRemainingTime()
            {
                return _duration - GetElapsedTime();
            }

            /// <summary> 남은 시간(최솟값: 0) </summary>
            public float GetRemainingTimeClamped()
            {
                float ret = GetRemainingTime();
                return ret < 0f ? 0f : ret;
            }

            /// <summary> 남은 시간 비율(0: 시작 ~ 1: 종료) </summary>
            public float GetRemainingRatio()
            {
                return (GetRemainingTime() / _duration);
            }

            /// <summary> 남은 시간 비율(범위 제한: 0 ~ 1) </summary>
            public float GetRemainingRatioClamped()
            {
                float ret = GetRemainingRatio();
                return ret < 0f ? 0f : ret;
            }

            #endregion

            /// <summary> 종료 여부 </summary>
            public bool IsEnded()
            {
                return GetElapsedTime() > _duration;
            }

            /// <summary> 진행 여부 </summary>
            public bool IsRunning()
            {
                return GetElapsedTime() <= _duration;
            }
        }


        /// <summary> 타임스케일 영향 X </summary>
        public readonly struct UnscaledTimer
        {
            private readonly float _begin;
            private readonly float _duration;
            private static float Current => Time.realtimeSinceStartup;

            public UnscaledTimer (float duration)
            {
                _begin = Current;
                _duration = duration;
            }

            /***********************************************************************
            *                               경과
            ***********************************************************************/
            #region .

            /// <summary> 경과 시간 </summary>
            public float GetElapsedTime()
            {
                return Current - _begin;
            }

            /// <summary> 경과 시간(최댓값: Duration) </summary>
            public float GetElapsedTimeClamped()
            {
                float ret = Current - _begin;
                return ret > _duration ? _duration : ret;
            }

            /// <summary> 경과 시간 비율(0: 시작 ~ 1: 종료) </summary>
            public float GetElapsedRatio()
            {
                return (GetElapsedTime() / _duration);
            }

            /// <summary> 경과 시간 비율(범위 제한: 0 ~ 1) </summary>
            public float GetElapsedRatioClamped()
            {
                float ret = GetElapsedRatio();
                return ret > 1f ? 1f : ret; ;
            }

            #endregion

            /***********************************************************************
            *                               잔여
            ***********************************************************************/
            #region .

            /// <summary> 남은 시간 </summary>
            public float GetRemainingTime()
            {
                return _duration - GetElapsedTime();
            }

            /// <summary> 남은 시간(최솟값: 0) </summary>
            public float GetRemainingTimeClamped()
            {
                float ret = GetRemainingTime();
                return ret < 0f ? 0f : ret;
            }

            /// <summary> 남은 시간 비율(0: 시작 ~ 1: 종료) </summary>
            public float GetRemainingRatio()
            {
                return (GetRemainingTime() / _duration);
            }

            /// <summary> 남은 시간 비율(범위 제한: 0 ~ 1) </summary>
            public float GetRemainingRatioClamped()
            {
                float ret = GetRemainingRatio();
                return ret < 0f ? 0f : ret;
            }

            #endregion

            /// <summary> 종료 여부 </summary>
            public bool IsEnded()
            {
                return GetElapsedTime() > _duration;
            }

            /// <summary> 진행 여부 </summary>
            public bool IsRunning()
            {
                return GetElapsedTime() <= _duration;
            }
        }

    }
}