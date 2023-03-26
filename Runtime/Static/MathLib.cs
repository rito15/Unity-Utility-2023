using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ ���]
   2020. 01. 15. �ۼ�
   2020. 04. 05. ���̹� ���� : MathLib
   2020. 04. 08. GetRandom() ����, Lotto() �ۼ�
   2023. 03. 26.
     - Ut23 ���̺귯���� ����
     - Clamp() ��� ���� => �ٰ�: Ȯ�� �޼ҵ�� ����ϴ°� �ξ� ����
*/

namespace Rito
{
    /// <summary>
    /// ���� ��� ���� �޼ҵ�
    /// <para/> ----------------------------------------
    /// <para/> [�޼ҵ� ���]
    /// <para/> Digitalize : ���� 0 �Ǵ� 1�� ������ȭ�Ͽ� ����
    /// <para/> SignedDigitalize : ���� -1, 0, 1�� ������ȭ�Ͽ� ����
    /// <para/> InRange : ���� ���� ���� ���� �ִ��� �˻�
    /// <para/> Clamp : ���� ���� ���� ���� ����
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

        // ���� -> Random.Range ����, �Ǽ� ��� 

        /// <summary> �Է��� Ȯ����� �̱� ��� ����(probability : 0 ~ 100) </summary>
        public static bool Lotto(float zeroToHundredProbability)
        {
            if (zeroToHundredProbability < 0f) zeroToHundredProbability = 0f;
            if (zeroToHundredProbability > 100f) zeroToHundredProbability = 100f;

            return Random.Range(0f, 100f) <= zeroToHundredProbability;
        }

        #endregion // ================================================================

        #region Digitalize

        /// <summary>
        /// ������ 0, 1 ������ ������ȭ�Ͽ� ����
        /// </summary>
        public static int Digitalize(in int intValue)
        {
            return intValue == 0 ?
                0 :
                1;
        }

        /// <summary>
        /// �Ǽ��� 0, 1 ������ ������ȭ�Ͽ� ����
        /// </summary>
        public static double Digitalize(in double doubleValue)
        {
            return doubleValue == 0.0 ?
                0.0 :
                1.0;
        }

        /// <summary>
        /// ������ -1, 0, 1 ������ ������ȭ�Ͽ� ����
        /// </summary>
        public static int SignedDigitalize(in int intValue)
        {
            if (intValue < 0) return -1;
            if (intValue > 0) return 1;
            return 0;
        }

        /// <summary>
        /// �Ǽ��� -1, 0, 1 ������ ������ȭ�Ͽ� ����
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
        /// ���� ���� ���� �ִ��� �˻�(���� ����)
        /// <para/> �Ķ���� : (��� ����, �ּڰ�, �ִ�)
        /// </summary>
        public static bool InRange(in float variable, in float min, in float max)
        {
            return (variable >= min && variable <= max);
        }
        /// <summary>
        /// ���� ���� ���� �ִ��� �˻�(���� ����)
        /// <para/> �Ķ���� : (�ּڰ�, ref ��� ����, �ִ�)
        /// </summary>
        public static bool InRange(float min, ref float variable, float max)
        {
            return (variable >= min && variable <= max);
        }

        #endregion // ================================================================
    }
}