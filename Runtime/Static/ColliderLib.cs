using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
   [ 기록]
   2020. 01. 15. 검토 완료, in 한정자 추가
   2020. 04. 05. 네이밍 변경 : ColliderLib
   2023. 03. 26.
   - Ut23 라이브러리에 편입
*/

namespace Rito.ut23
{
    using Debugger = Debugs.Debug;

    public static class ColliderLib
    {
        #region Collider - Check

        /// <summary>
        /// 특정 좌표 기준으로 구형 범위 콜라이더 검사
        /// <para/> 리턴 : (체크 여부, 범위 내 콜라이더 개수)
        /// </summary>
        public static (bool, int) CheckSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            var colliders = GetCollidersInSphere(center, radius, layerMask);
            int count = colliders.Length;

            return (count > 0, count);

        }

        /// <summary>
        /// 트랜스폼 기준으로 구형 범위 콜라이더 검사
        /// <para/> 리턴 : (체크 여부, 범위 내 콜라이더 개수)
        /// <para/> * 트랜스폼 null 체크 포함
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

        /// <summary> 구 형태의 콜라이더를 생성하여 해당 범위 내의 layer 오브젝트 검사</summary>
        public static Collider GetColliderInSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            Collider[] targetCol = Physics.OverlapSphere(center, radius, layerMask);

            // 없으면 null 리턴
            int length = targetCol.Count();
            if (length == 0) return null;

            // 있으면 거리 검사
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
        /// 트랜스폼을 기준으로 원형 콜라이더 검사
        /// <para/> 본인 콜라이더는 미포함
        /// <para/> 거리가 가장 가까운 콜라이더 리턴
        /// </summary>
        public static Collider GetColliderInSphere(Transform mySelf, in float radius = 1, in int layerMask = -1)
        {
            if (mySelf == null)
            {
                Debugger.LogError("Transform Is Missing");
                return null;
            }

            Collider[] colliders = Physics.OverlapSphere(mySelf.position, radius, layerMask);

            // 자신 말고 다른 콜라이더들 찾기
            var targetCol = from col in colliders
                            where col.transform != mySelf
                            select col;

            // 없으면 null 리턴
            int length = targetCol.Count();
            if (length == 0) return null;

            // 있으면 거리 검사
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
        /// 특정 좌표 기준으로 원형 콜라이더 검사
        /// <para/> 찾아낸 모든 콜라이더 리턴
        /// <para/> ★거리가 가까운 순으로 인덱스 정렬됨 !! ★
        /// </summary>
        public static Collider[] GetCollidersInSphere(in Vector3 center, in float radius = 1, in int layerMask = -1)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);

            // 없으면 null 리턴
            int length = colliders.Count();
            if (length == 0) return null;

            // 있으면 거리 검사
            var targetArr = colliders.ToArray();

            // 정렬리스트에 (거리, 콜라이더) 쌍 생성
            SortedList<float, Collider> lenCol = new SortedList<float, Collider>();

            for (int i = 0; i < length; i++)
            {
                lenCol.Add(Vector3.Distance(center, targetArr[i].transform.position), targetArr[i]);
            }

            // 정렬된 콜라이더 리스트 생성
            int index = 0;
            foreach (var lc in lenCol)
            {
                targetArr[index++] = lc.Value;
            }

            return targetArr;
        }

        /// <summary>
        /// 트랜스폼을 기준으로 원형 콜라이더 검사
        /// <para/> 본인 콜라이더는 미포함
        /// <para/> 찾아낸 모든 콜라이더 리턴
        /// <para/> ★거리가 가까운 순으로 인덱스 정렬됨 !! ★
        /// </summary>
        public static Collider[] GetCollidersInSphere(Transform mySelf, in float radius = 1, in int layerMask = -1)
        {
            if (mySelf == null)
            {
                Debugger.LogError("Transform Is Missing");
                return null;
            }

            Collider[] colliders = Physics.OverlapSphere(mySelf.position, radius, layerMask);

            // 자신 말고 다른 콜라이더들 찾기
            var targetCol = from col in colliders
                            where col.transform != mySelf
                            select col;

            // 없으면 null 리턴
            int length = targetCol.Count();
            if (length == 0) return null;

            // 있으면 거리 검사
            var targetArr = targetCol.ToArray();

            // 정렬리스트에 (거리, 콜라이더) 쌍 생성
            SortedList<float, Collider> lenCol = new SortedList<float, Collider>();

            for (int i = 0; i < length; i++)
            {
                lenCol.Add(Vector3.Distance(mySelf.position, targetArr[i].transform.position), targetArr[i]);
            }

            // 정렬된 콜라이더 리스트 생성
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