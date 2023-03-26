using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
   [ ���]
   2020. 01. 15. ���� �Ϸ�, in ������ �߰�
   2020. 04. 05. ���̹� ���� : ColliderLib
   2023. 03. 26.
   - Ut23 ���̺귯���� ����
*/

namespace Rito.ut23
{
    using Debugger = Debugs.Debug;

    public static class ColliderLib
    {
        #region Collider - Check

        /// <summary>
        /// Ư�� ��ǥ �������� ���� ���� �ݶ��̴� �˻�
        /// <para/> ���� : (üũ ����, ���� �� �ݶ��̴� ����)
        /// </summary>
        public static (bool, int) CheckSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            var colliders = GetCollidersInSphere(center, radius, layerMask);
            int count = colliders.Length;

            return (count > 0, count);

        }

        /// <summary>
        /// Ʈ������ �������� ���� ���� �ݶ��̴� �˻�
        /// <para/> ���� : (üũ ����, ���� �� �ݶ��̴� ����)
        /// <para/> * Ʈ������ null üũ ����
        /// </summary>
        public static (bool, int) CheckSphere(in Transform mySelf, in float radius = 1, in int layerMask = -1)
        {
            if (mySelf == null)
            {
                Debugger.LogError("Transform Is Missing");
                return (false, 0);
            }

            var colliders = GetCollidersInSphere(mySelf, radius, layerMask);
            int count = colliders.Length;

            return (count > 0, count);
        }

        #endregion

        #region Collider - Get

        /// <summary> �� ������ �ݶ��̴��� �����Ͽ� �ش� ���� ���� layer ������Ʈ �˻�</summary>
        public static Collider GetColliderInSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            Collider[] targetCol = Physics.OverlapSphere(center, radius, layerMask);

            // ������ null ����
            int length = targetCol.Count();
            if (length == 0) return null;

            // ������ �Ÿ� �˻�
            (float minDistance, int targetIndex)
                = (Vector3.Distance(targetCol[0].transform.position, center), 0);

            float nowDistance;
            for (int i = 1; i < length; i++)
            {
                nowDistance = Vector3.Distance(targetCol[i].transform.position, center);

                if (nowDistance < minDistance)
                    targetIndex = i;
            }

            return targetCol[targetIndex];
        }

        /// <summary>
        /// Ʈ�������� �������� ���� �ݶ��̴� �˻�
        /// <para/> ���� �ݶ��̴��� ������
        /// <para/> �Ÿ��� ���� ����� �ݶ��̴� ����
        /// </summary>
        public static Collider GetColliderInSphere(Transform mySelf, in float radius = 1, in int layerMask = -1)
        {
            if (mySelf == null)
            {
                Debugger.LogError("Transform Is Missing");
                return null;
            }

            Collider[] colliders = Physics.OverlapSphere(mySelf.position, radius, layerMask);

            // �ڽ� ���� �ٸ� �ݶ��̴��� ã��
            var targetCol = from col in colliders
                            where col.transform != mySelf
                            select col;

            // ������ null ����
            int length = targetCol.Count();
            if (length == 0) return null;

            // ������ �Ÿ� �˻�
            var targetArr = targetCol.ToArray();
            (float minDistance, int targetIndex)
                = (Vector3.Distance(targetArr[0].transform.position, mySelf.position), 0);

            float nowDistance;
            for (int i = 1; i < length; i++)
            {
                nowDistance = Vector3.Distance(targetArr[i].transform.position, mySelf.position);

                if (nowDistance < minDistance)
                    targetIndex = i;
            }

            return targetArr[targetIndex];
        }

        /// <summary>
        /// Ư�� ��ǥ �������� ���� �ݶ��̴� �˻�
        /// <para/> ã�Ƴ� ��� �ݶ��̴� ����
        /// <para/> �ڰŸ��� ����� ������ �ε��� ���ĵ� !! ��
        /// </summary>
        public static Collider[] GetCollidersInSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);

            // ������ null ����
            int length = colliders.Count();
            if (length == 0) return null;

            // ������ �Ÿ� �˻�
            var targetArr = colliders.ToArray();

            // ���ĸ���Ʈ�� (�Ÿ�, �ݶ��̴�) �� ����
            SortedList<float, Collider> lenCol = new SortedList<float, Collider>();

            for (int i = 0; i < length; i++)
            {
                lenCol.Add(Vector3.Distance(center, targetArr[i].transform.position), targetArr[i]);
            }

            // ���ĵ� �ݶ��̴� ����Ʈ ����
            int index = 0;
            foreach (var lc in lenCol)
            {
                targetArr[index++] = lc.Value;
            }

            return targetArr;
        }

        /// <summary>
        /// Ʈ�������� �������� ���� �ݶ��̴� �˻�
        /// <para/> ���� �ݶ��̴��� ������
        /// <para/> ã�Ƴ� ��� �ݶ��̴� ����
        /// <para/> �ڰŸ��� ����� ������ �ε��� ���ĵ� !! ��
        /// </summary>
        public static Collider[] GetCollidersInSphere(Transform mySelf, in float radius = 1, in int layerMask = -1)
        {
            if (mySelf == null)
            {
                Debugger.LogError("Transform Is Missing");
                return null;
            }

            Collider[] colliders = Physics.OverlapSphere(mySelf.position, radius, layerMask);

            // �ڽ� ���� �ٸ� �ݶ��̴��� ã��
            var targetCol = from col in colliders
                            where col.transform != mySelf
                            select col;

            // ������ null ����
            int length = targetCol.Count();
            if (length == 0) return null;

            // ������ �Ÿ� �˻�
            var targetArr = targetCol.ToArray();

            // ���ĸ���Ʈ�� (�Ÿ�, �ݶ��̴�) �� ����
            SortedList<float, Collider> lenCol = new SortedList<float, Collider>();

            for (int i = 0; i < length; i++)
            {
                lenCol.Add(Vector3.Distance(mySelf.position, targetArr[i].transform.position), targetArr[i]);
            }

            // ���ĵ� �ݶ��̴� ����Ʈ ����
            int index = 0;
            foreach (var lc in lenCol)
            {
                targetArr[index++] = lc.Value;
            }

            return targetArr;
        }

        #endregion
    }
}