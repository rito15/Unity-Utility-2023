using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-14 PM 7:56:04
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - 이름 규칙 변경: 항상 Ex_ 접두어 사용
*/

namespace Rito.ut23.Extensions
{
    public static class ComponentExtension
    {
        [TestCompleted(2021, 06, 16)]
        public static T Ex_GetOrAddComponent<T>(this Component @this)
            where T : Component
        {
            if (!@this.TryGetComponent(out T component))
            {
                component = @this.gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary> 모든 하위 게임오브젝트(비활성화 포함)에서 컴포넌트 찾아오기 </summary>
        [TestCompleted(2021, 06, 16)]
        public static T Ex_GetComponentInDescendants<T>(this Component @this)
            where T : Component
        {
            List<Transform> allDes = @this.transform.Ex_GetAllDescendants();
            foreach (var tr in allDes)
            {
                if (tr.TryGetComponent(out T target))
                    return target;
            }

            return null;
        }

        /// <summary> 모든 하위 게임오브젝트(비활성화 포함)에서 컴포넌트 모두 찾아오기 </summary>
        [TestCompleted(2021, 06, 16)]
        public static List<T> Ex_GetComponentsInDescendants<T>(this Component @this)
            where T : Component
        {
            List<Transform> allDes = @this.transform.Ex_GetAllDescendants();
            List<T> tList = new List<T>(allDes.Count);

            foreach (var tr in allDes)
            {
                if (tr.TryGetComponent(out T target))
                    tList.Add(target);
            }

            return tList;
        }

        /// <summary>
        /// <para/> 자기 게임오브젝트를 제외하고 자식 게임오브젝트들에서만 컴포넌트 가져오기
        /// </summary>
        [TestCompleted(2020, 05, 18)]
        public static Component Ex_GetComponentInChildrenOnly(this Component @this, Type targetType)
        {
            Transform transform = @this.transform;
            int childCount = transform.childCount;

            if (childCount == 0)
                return null;

            for (int i = 0; i < childCount; i++)
            {
                Component targetComponent = transform.GetChild(i).GetComponentInChildren(targetType);
                if (targetComponent != null)
                    return targetComponent;
            }

            return null;
        }

        /// <summary>
        /// <para/> 자기 게임오브젝트를 제외하고 부모 게임오브젝트들에서만 컴포넌트 가져오기
        /// </summary>
        [TestCompleted(2020, 05, 18)]
        public static Component Ex_GetComponentInParentOnly(this Component @this, Type targetType)
        {
            Transform transform = @this.transform;
            return (transform.parent == null) ? null : transform.parent.GetComponentInParent(targetType);
        }
    }
}