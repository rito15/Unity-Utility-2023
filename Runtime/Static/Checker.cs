using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ ���]
   2020. 01. 22. IsNull, NotNull �޼ҵ� �߰�
   2020. 05. 01. object -> UnityEngine.Object Ÿ������ ����
   2023. 03. 26.
   - Ut23 ���̺귯���� ����
*/

namespace Rito.ut23
{

    /// <summary> ������Ʈ ���� �˻� </summary>
    public static class Checker
    {
        #region NullCheck

        /// <summary>
        /// �Ķ���͵��� �ϳ��� Null�̸� true ����
        /// <para/> * ���� �Ķ���ͺ��� ������ �˻�
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
        /// �Ķ���͵��� �ϳ��� Null�̸� true ����
        /// <para/> * ���� �Ķ���ͺ��� ������ �˻�
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
        /// �Ķ���͵��� �ϳ��� Null�̸� false ����
        /// <para/> �Ķ���͵��� ��� Null�� �ƴϸ� true ����
        /// <para/> * ���� �Ķ���ͺ��� ������ �˻�
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
        /// �Ķ���͵��� �ϳ��� Null�̸� true ����
        /// <para/> �Ķ���͵��� ��� Null�� �ƴϸ� true ����
        /// <para/> * ���� �Ķ���ͺ��� ������ �˻�
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