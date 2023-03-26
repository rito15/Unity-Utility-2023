using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2020. 01. 15.
/*
   [ 기록]
   2023. 03. 26.
     - Ut23 라이브러리에 편입
*/

namespace Rito.ut23
{
    using Debugger = Debugs.Debug;

    public class Vector3Lib
    {
        #region Distance 3D

        /// <summary> 두 벡터 위치가 지정 거리 이내에 있는지 검사 </summary>
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

        // 두 오브젝트 사이 거리 검사
        public static float Distance(GameObject go1, GameObject go2, float exceptionalDistance = -1f)
        {
            // exceptionalDistance : 예외가 발생했을 때 임시로 리턴할 거리 값

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
            // exceptionalDistance : 예외가 발생했을 때 임시로 리턴할 거리 값

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

        // 크기 1짜리 방향 벡터 리턴
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
         * 두 방향 벡터 사이의 각도 구하기 : Vector3.Angle(Vector3 from, Vector3 to)
         */
        float Angle(Vector3 from, Vector3 to)
        {
            return Vector3.Angle(from, to);
        }

        // 회전 기준점(axisPoint)을 지정하지 않을 경우, 기준은 (0,0,0)
        // => 그럼 x 회전은 x축 기준, .... 이렇게 된다.
        // + 회전 기준점을 지정할 경우, (0,0,0)을 기준으로 회전시킨 뒤 다시 상대좌표로 옮겨서 리턴
        // + 활용 방안 : 1. 좌표 벡터를 회전 / 2. 방향 벡터를 회전
        // + http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=46332
        /// <summary> 특정 좌표를 기준으로 특정 각도(degree)만큼 회전시킨 벡터 계산 </summary>
        public static Vector3 Rotate(Vector3 originVector, float xDeg, float yDeg, float zDeg, Vector3 axisPoint = default)
        {
            /* xDeg = x축 기준으로 회전시킬 값, y z 마찬가지
             * 
             * x, y, z 모두 값이 주어질 경우 회전축 우선순위
             * : z -> x -> y
             *   (z축으로 회전시키고 x축으로 다시 회전시킨 다음, 마지막으로 y축 회전)
             * 
             */

            // 회전 기준 좌표이자 상대 좌표
            Vector3 transitionVector = axisPoint;

            // 1. 상대 좌표 이동
            originVector -= transitionVector;

            // 회전계수 = 회전각(x,y,z)
            Quaternion rotationCoef = Quaternion.Euler(xDeg, yDeg, zDeg);

            // 2. x,y,z 각각의 "축"으로 회전
            originVector = rotationCoef * originVector;

            // 3. 다시 좌표 복귀
            originVector += transitionVector;

            return originVector;
        }

        /// <summary> Y축(기본) 또는 특정 좌표 기준으로 우측으로 회전한 벡터 </summary>
        public static Vector3 RotateRight(Vector3 originVector, float rightDegree, Vector3 axisPoint = default)
        {
            return Rotate(originVector, 0f, rightDegree, 0f, axisPoint);
        }

        /// <summary> Y축(기본) 또는 특정 좌표 기준으로 좌측으로 회전한 벡터 </summary>
        public static Vector3 RotateLeft(Vector3 originVector, float leftDegree, Vector3 axisPoint = default)
        {
            return Rotate(originVector, 0f, -leftDegree, 0f, axisPoint);
        }

        #endregion // ================================================================

        #region Convert 3D -> 2D

        /// <summary> 3D 좌표를 2D 스크린 좌표로 변환 </summary>
        public static Vector2 Convert3DTo2D(Vector3 position3D)
        {
            return Camera.main.WorldToScreenPoint(position3D);
        }

        #endregion
    }
}