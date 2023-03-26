using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ 기록]
   2020. 01. 15. 작성
   2020. 04. 05. 네이밍 변경 : LayerLib, Compare() 메소드들 개편, GetMask() 메소드 추가, Include() 메소드 추가
   2023. 03. 26.
   - Ut23 라이브러리에 편입
*/

namespace Rito
{
    public static class LayerLib
    {
        /// <summary> 레이어 아무것도 선택 X </summary>
        public const int None = 0;
        /// <summary> 모든 레이어 선택 </summary>
        public const int All = -1;

        #region Public Methods

        /// <summary>
        /// 레이어 일치 비교
        /// </summary>
        public static bool Compare(in int layerA, in int layerB)
        {
            return layerA.Equals(layerB);
        }

        /// <summary> 레이어 번호들을 받아, 레이어 마스크 생성 </summary>
        public static LayerMask GetMask(params int[] layers)
        {
            int mask = 0;

            for (int i = 0; i < layers.Length; i++)
            {
                mask |= (1 << layers[i]);
            }
            return mask;
        }

        /// <summary> 레이어마스크가 레이어를 포함하는지 검사 </summary>
        public static bool Contains(in LayerMask mask, in int layer)
        {
            return (mask.value | 1 << layer).Equals(mask.value);
        }

        #endregion // ==========================================================

        #region Comparations - GameObject, Transform Overloadings

        /// <summary>
        /// 레이어 비교 : 게임오브젝트 &lt;-&gt; 레이어 번호
        /// </summary>
        public static bool Compare(in GameObject gobj, in int layer)
        {
            if (gobj == null) return false;

            return gobj.layer.Equals(layer);
        }

        /// <summary>
        /// 레이어 비교 : 레이어 번호 &lt;-&gt; 게임오브젝트 
        /// </summary>
        public static bool Compare(in int layer, in GameObject gobj)
        {
            if (gobj == null) return false;

            return gobj.layer.Equals(layer);
        }

        /// <summary>
        /// 레이어 비교 : 게임오브젝트 &lt;-&gt; 게임오브젝트
        /// </summary>
        public static bool Compare(in GameObject gobjA, in GameObject gobjB)
        {
            if (gobjA == null) return false;
            if (gobjB == null) return false;

            return gobjA.layer.Equals(gobjB.layer);
        }

        // ====================================================================

        /// <summary>
        /// 레이어 비교 : 트랜스폼 &lt;-&gt; 레이어 번호
        /// </summary>
        public static bool Compare(in Transform transform, in int layer)
        {
            if (transform == null) return false;

            return transform.gameObject.layer.Equals(layer);
        }


        /// <summary>
        /// 레이어 비교 : 레이어 번호 &lt;-&gt; 트랜스폼
        /// </summary>
        public static bool Compare(in int layer, in Transform transform)
        {
            if (transform == null) return false;

            return transform.gameObject.layer.Equals(layer);
        }

        /// <summary>
        /// 레이어 비교 : 트랜스폼
        /// </summary>
        public static bool Compare(in Transform trA, in Transform trB)
        {
            if (trA == null) return false;
            if (trB == null) return false;

            return trA.gameObject.layer.Equals(trB.gameObject.layer);
        }

        // ====================================================================

        /// <summary>
        /// 레이어 비교 : 게임오브젝트 &lt;-&gt; 트랜스폼
        /// </summary>
        public static bool Compare(in GameObject gobj, in Transform tr)
        {
            if (gobj == null) return false;
            if (tr == null) return false;

            return gobj.layer.Equals(tr.gameObject.layer);
        }

        /// <summary>
        /// 레이어 비교 : 트랜스폼 &lt;-&gt; 게임오브젝트
        /// </summary>
        public static bool Compare(in Transform tr, in GameObject gobj)
        {
            if (tr == null) return false;
            if (gobj == null) return false;

            return gobj.layer.Equals(tr.gameObject.layer);
        }

        #endregion // ==========================================================
    }
}