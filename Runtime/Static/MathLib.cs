using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ 기록]
   2020. 01. 15. 작성
   2020. 04. 05. 네이밍 변경 : MathLib
   2020. 04. 08. GetRandom() 제거, Lotto() 작성
   2023. 03. 26.
     - Ut23 라이브러리에 편입
     - Clamp() 모두 제거 => 근거: 확장 메소드로 사용하는게 훨씬 편리함
*/

namespace Rito
{
    /// <summary>
    /// 수학 계산 정적 메소드
    /// <para/> ----------------------------------------
    /// <para/> [메소드 목록]
    /// <para/> Digitalize : 값을 0 또는 1로 디지털화하여 리턴
    /// <para/> SignedDigitalize : 값을 -1, 0, 1로 디지털화하여 리턴
    /// <para/> InRange : 변수 값이 범위 내에 있는지 검사
    /// <para/> Clamp : 변수 값을 범위 내로 보정
    /// <para/> 
    /// </summary>
    public static class MathLib
    {
        #region Random

        private static int GetSeed()
        {
            var rand = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] data = new byte[100];

            rand.GetBytes(data);

            return System.Math.Abs(System.BitConverter.ToInt32(data, 4));
        }

        // 난수 -> Random.Range 정수, 실수 모두 

        /// <summary> 입력한 확률대로 뽑기 결과 리턴(probability : 0 ~ 100) </summary>
        public static bool Lotto(float zeroToHundredProbability)
        {
            if (zeroToHundredProbability < 0f) zeroToHundredProbability = 0f;
            if (zeroToHundredProbability > 100f) zeroToHundredProbability = 100f;

            return Random.Range(0f, 100f) <= zeroToHundredProbability;
        }

        #endregion // ================================================================

        #region Digitalize

        /// <summary>
        /// 정수를 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int Digitalize(in int intValue)
        {
            return intValue == 0 ?
                0 :
                1;
        }

        /// <summary>
        /// 실수를 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double Digitalize(in double doubleValue)
        {
            return doubleValue == 0.0 ?
                0.0 :
                1.0;
        }

        /// <summary>
        /// 정수를 -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static int SignedDigitalize(in int intValue)
        {
            if (intValue < 0) return -1;
            if (intValue > 0) return 1;
            return 0;
        }

        /// <summary>
        /// 실수를 -1, 0, 1 값으로 디지털화하여 리턴
        /// </summary>
        public static double SignedDigitalize(in double doubleValue)
        {
            if (doubleValue < 0.0) return -1.0;
            if (doubleValue > 0.0) return 1.0;
            return 0.0;
        }

        #endregion // ================================================================

        #region Range

        /// <summary>
        /// 값이 범위 내에 있는지 검사(정점 포함)
        /// <para/> 파라미터 : (대상 변수, 최솟값, 최댓값)
        /// </summary>
        public static bool InRange(in float variable, in float min, in float max)
        {
            return (variable >= min && variable <= max);
        }
        /// <summary>
        /// 값이 범위 내에 있는지 검사(정점 포함)
        /// <para/> 파라미터 : (최솟값, ref 대상 변수, 최댓값)
        /// </summary>
        public static bool InRange(float min, ref float variable, float max)
        {
            return (variable >= min && variable <= max);
        }

        #endregion // ================================================================
    }
}