using UnityEngine;

// �ۼ��� : Rito
// �ۼ��� : 2023-03-28 AM 0:26:02
/*
   [ ���]
   2023. 03. 28. ���� �ۼ�, ����ð�/�����ð� ��� �׽�Ʈ �Ϸ�
*/
namespace Rito.ut23
{
    /// <summary>
    /// <para/> ���ӽð�/��Ÿ�� üũ Ÿ�̸� 
    /// <para/> [����]
    /// <para/> 1. Scaled(��) : Ÿ�ӽ����� ���� �޴� Ÿ�̸� ����
    /// <para/> 2. Unscaled(��) : Ÿ�ӽ����� ���� ���� �ʴ� Ÿ�̸� ����
    /// <para/> 
    /// <para/> [API - ����]
    /// <para/> - IsEnded(): ���� ����
    /// <para/> 
    /// <para/> [API - ���ӽð�]
    /// <para/> - GetElapsedTime(): ����ð�(��)
    /// <para/> - GetElapsedTimeClamped(): ����ð�(��), �ִ�: ���ӽð�
    /// <para/> - GetElapsedRatio(): ����ð�(����: 0~1)
    /// <para/> - GetElapsedRatioClamped(): ����ð�(����: 0~1), �ִ�: 1
    /// <para/> 
    /// <para/> [API - ��Ÿ��]
    /// <para/> - GetRemainingTime(): �����ð�(��)
    /// <para/> - GetRemainingTimeClamped(): �����ð�(��), �ּڰ�: 0
    /// <para/> - GetRemainingRatio(): �����ð�(����: 0~1)
    /// <para/> - GetRemainingRatioClamped(): �����ð�(����: 0~1), �ּڰ�: 0
    /// </summary>
    public static class CoolTimer
    {
        /// <summary> Ÿ�ӽ����� ���� O </summary>
        public static ScaledTimer Scaled(float seconds) => new ScaledTimer(seconds);
        /// <summary> Ÿ�ӽ����� ���� O </summary>
        public static ScaledTimer GameTime(float seconds) => new ScaledTimer(seconds);


        /// <summary> Ÿ�ӽ����� ���� X </summary>
        public static UnscaledTimer Unscaled(float seconds) => new UnscaledTimer(seconds);
        /// <summary> Ÿ�ӽ����� ���� X </summary>
        public static UnscaledTimer Realtime(float seconds) => new UnscaledTimer(seconds);


        /// <summary> Ÿ�ӽ����� ���� O </summary>
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
            *                               ���
            ***********************************************************************/
            #region .

            /// <summary> ��� �ð� </summary>
            public float GetElapsedTime()
            {
                return Current - _begin;
            }

            /// <summary> ��� �ð�(�ִ�: Duration) </summary>
            public float GetElapsedTimeClamped()
            {
                float ret = Current - _begin;
                return ret > _duration ? _duration : ret;
            }

            /// <summary> ��� �ð� ����(0: ���� ~ 1: ����) </summary>
            public float GetElapsedRatio()
            {
                return (GetElapsedTime() / _duration);
            }

            /// <summary> ��� �ð� ����(���� ����: 0 ~ 1) </summary>
            public float GetElapsedRatioClamped()
            {
                float ret = GetElapsedRatio();
                return ret > 1f ? 1f : ret; ;
            }

            #endregion

            /***********************************************************************
            *                               �ܿ�
            ***********************************************************************/
            #region .

            /// <summary> ���� �ð� </summary>
            public float GetRemainingTime()
            {
                return _duration - GetElapsedTime();
            }

            /// <summary> ���� �ð�(�ּڰ�: 0) </summary>
            public float GetRemainingTimeClamped()
            {
                float ret = GetRemainingTime();
                return ret < 0f ? 0f : ret;
            }

            /// <summary> ���� �ð� ����(0: ���� ~ 1: ����) </summary>
            public float GetRemainingRatio()
            {
                return (GetRemainingTime() / _duration);
            }

            /// <summary> ���� �ð� ����(���� ����: 0 ~ 1) </summary>
            public float GetRemainingRatioClamped()
            {
                float ret = GetRemainingRatio();
                return ret < 0f ? 0f : ret;
            }

            #endregion

            /// <summary> ���� ���� </summary>
            public bool IsEnded()
            {
                return GetElapsedTime() > _duration;
            }
        }


        /// <summary> Ÿ�ӽ����� ���� X </summary>
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
            *                               ���
            ***********************************************************************/
            #region .

            /// <summary> ��� �ð� </summary>
            public float GetElapsedTime()
            {
                return Current - _begin;
            }

            /// <summary> ��� �ð�(�ִ�: Duration) </summary>
            public float GetElapsedTimeClamped()
            {
                float ret = Current - _begin;
                return ret > _duration ? _duration : ret;
            }

            /// <summary> ��� �ð� ����(0: ���� ~ 1: ����) </summary>
            public float GetElapsedRatio()
            {
                return (GetElapsedTime() / _duration);
            }

            /// <summary> ��� �ð� ����(���� ����: 0 ~ 1) </summary>
            public float GetElapsedRatioClamped()
            {
                float ret = GetElapsedRatio();
                return ret > 1f ? 1f : ret; ;
            }

            #endregion

            /***********************************************************************
            *                               �ܿ�
            ***********************************************************************/
            #region .

            /// <summary> ���� �ð� </summary>
            public float GetRemainingTime()
            {
                return _duration - GetElapsedTime();
            }

            /// <summary> ���� �ð�(�ּڰ�: 0) </summary>
            public float GetRemainingTimeClamped()
            {
                float ret = GetRemainingTime();
                return ret < 0f ? 0f : ret;
            }

            /// <summary> ���� �ð� ����(0: ���� ~ 1: ����) </summary>
            public float GetRemainingRatio()
            {
                return (GetRemainingTime() / _duration);
            }

            /// <summary> ���� �ð� ����(���� ����: 0 ~ 1) </summary>
            public float GetRemainingRatioClamped()
            {
                float ret = GetRemainingRatio();
                return ret < 0f ? 0f : ret;
            }

            #endregion

            /// <summary> ���� ���� </summary>
            public bool IsEnded()
            {
                return GetElapsedTime() > _duration;
            }
        }

    }
}