using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020. 01. 15.
/*
   [ ���]
   2023. 03. 26.
     - Ut23 ���̺귯���� ����
*/

namespace Rito.ut23
{
    using Debugger = Debugs.Debug;

    public class Vector3Lib
    {
        #region Distance 3D

        /// <summary> �� ���� ��ġ�� ���� �Ÿ� �̳��� �ִ��� �˻� </summary>
        public static bool WithinDistance(Vector3 pos1, Vector3 pos2, float distance)
        {
            return Vector3.Distance(pos1, pos2) <= distance;
        }
        public static bool WithinDistance(Transform tr1, Vector3 pos2, float distance)
        {
            if (tr1 == null)
            {
                Debugger.Log("Transform 1 Is Missing");
                return false;
            }

            return Vector3.Distance(tr1.position, pos2) <= distance;
        }
        public static bool WithinDistance(Vector3 pos1, Transform tr2, float distance)
        {
            if (tr2 == null)
            {
                Debugger.Log("Transform 2 Is Missing");
                return false;
            }

            return Vector3.Distance(pos1, tr2.position) <= distance;
        }
        public static bool WithinDistance(Transform tr1, Transform tr2, float distance)
        {
            if (tr1 == null)
            {
                Debugger.Log("Transform 1 Is Missing");
                return false;
            }
            if (tr2 == null)
            {
                Debugger.Log("Transform 2 Is Missing");
                return false;
            }

            return Vector3.Distance(tr1.position, tr2.position) <= distance;
        }

        // �� ������Ʈ ���� �Ÿ� �˻�
        public static float Distance(GameObject go1, GameObject go2, float exceptionalDistance = -1f)
        {
            // exceptionalDistance : ���ܰ� �߻����� �� �ӽ÷� ������ �Ÿ� ��

            if (go1 == null)
            {
                Debugger.Log("GameObject 1 Is Missing");
                return exceptionalDistance;
            }
            if (go2 == null)
            {
                Debugger.Log("GameObject 2 Is Missing");
                return exceptionalDistance;
            }

            return Vector3.Distance(go1.transform.position, go2.transform.position);
        }
        public static float Distance(Transform tr1, Transform tr2, float exceptionalDistance = -1f)
        {
            // exceptionalDistance : ���ܰ� �߻����� �� �ӽ÷� ������ �Ÿ� ��

            if (tr1 == null)
            {
                Debugger.Log("Transform 1 Is Missing");
                return exceptionalDistance;
            }
            if (tr2 == null)
            {
                Debugger.Log("Transform 2 Is Missing");
                return exceptionalDistance;
            }

            return Vector3.Distance(tr1.position, tr2.position);
        }

        #endregion // ================================================================

        #region Direction

        // ũ�� 1¥�� ���� ���� ����
        public static Vector3 Direction(Vector3 from, Vector3 to)
        {
            return (to - from).normalized;
        }
        public static Vector3 Direction(Transform from, Vector3 to)
        {
            if (from == null)
            {
                Debugger.Log("Transform(From) Is Missing");
                return Vector3.forward;
            }

            return (to - from.position).normalized;
        }
        public static Vector3 Direction(Vector3 from, Transform to)
        {
            if (to == null)
            {
                Debugger.Log("Transform(To) Is Missing");
                return Vector3.forward;
            }

            return (to.position - from).normalized;
        }
        public static Vector3 Direction(Transform from, Transform to)
        {
            if (from == null)
            {
                Debugger.Log("Transform(From) Is Missing");
                return Vector3.forward;
            }
            if (to == null)
            {
                Debugger.Log("Transform(To) Is Missing");
                return Vector3.forward;
            }

            return (to.position - from.position).normalized;
        }
        public static Vector3 Direction(GameObject from, Vector3 to)
        {
            if (from == null)
            {
                Debugger.Log("Game Object((From) Is Missing");
                return Vector3.forward;
            }

            return (to - from.transform.position).normalized;
        }
        public static Vector3 Direction(Vector3 from, GameObject to)
        {
            if (to == null)
            {
                Debugger.Log("Game Object((To) Is Missing");
                return Vector3.forward;
            }

            return (to.transform.position - from).normalized;
        }
        public static Vector3 Direction(GameObject from, GameObject to)
        {
            if (from == null)
            {
                Debugger.Log("Game Object((From) Is Missing");
                return Vector3.forward;
            }
            if (to == null)
            {
                Debugger.Log("Game Object((To) Is Missing");
                return Vector3.forward;
            }

            return (to.transform.position - from.transform.position).normalized;
        }

        #endregion // ================================================================

        #region Angle, Rotate

        /* 
         * �� ���� ���� ������ ���� ���ϱ� : Vector3.Angle(Vector3 from, Vector3 to)
         */
        float Angle(Vector3 from, Vector3 to)
        {
            return Vector3.Angle(from, to);
        }

        // ȸ�� ������(axisPoint)�� �������� ���� ���, ������ (0,0,0)
        // => �׷� x ȸ���� x�� ����, .... �̷��� �ȴ�.
        // + ȸ�� �������� ������ ���, (0,0,0)�� �������� ȸ����Ų �� �ٽ� �����ǥ�� �Űܼ� ����
        // + Ȱ�� ��� : 1. ��ǥ ���͸� ȸ�� / 2. ���� ���͸� ȸ��
        // + http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=46332
        /// <summary> Ư�� ��ǥ�� �������� Ư�� ����(degree)��ŭ ȸ����Ų ���� ��� </summary>
        public static Vector3 Rotate(Vector3 originVector, float xDeg, float yDeg, float zDeg, Vector3 axisPoint = default)
        {
            /* xDeg = x�� �������� ȸ����ų ��, y z ��������
             * 
             * x, y, z ��� ���� �־��� ��� ȸ���� �켱����
             * : z -> x -> y
             *   (z������ ȸ����Ű�� x������ �ٽ� ȸ����Ų ����, ���������� y�� ȸ��)
             * 
             */

            // ȸ�� ���� ��ǥ���� ��� ��ǥ
            Vector3 transitionVector = axisPoint;

            // 1. ��� ��ǥ �̵�
            originVector -= transitionVector;

            // ȸ����� = ȸ����(x,y,z)
            Quaternion rotationCoef = Quaternion.Euler(xDeg, yDeg, zDeg);

            // 2. x,y,z ������ "��"���� ȸ��
            originVector = rotationCoef * originVector;

            // 3. �ٽ� ��ǥ ����
            originVector += transitionVector;

            return originVector;
        }

        /// <summary> Y��(�⺻) �Ǵ� Ư�� ��ǥ �������� �������� ȸ���� ���� </summary>
        public static Vector3 RotateRight(Vector3 originVector, float rightDegree, Vector3 axisPoint = default)
        {
            return Rotate(originVector, 0f, rightDegree, 0f, axisPoint);
        }

        /// <summary> Y��(�⺻) �Ǵ� Ư�� ��ǥ �������� �������� ȸ���� ���� </summary>
        public static Vector3 RotateLeft(Vector3 originVector, float leftDegree, Vector3 axisPoint = default)
        {
            return Rotate(originVector, 0f, -leftDegree, 0f, axisPoint);
        }

        #endregion // ================================================================

        #region Convert 3D -> 2D

        /// <summary> 3D ��ǥ�� 2D ��ũ�� ��ǥ�� ��ȯ </summary>
        public static Vector2 Convert3DTo2D(Vector3 position3D)
        {
            return Camera.main.WorldToScreenPoint(position3D);
        }

        #endregion
    }
}