using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   [ ���]
   2020. 01. 15. �ۼ�
   2020. 04. 05. ���̹� ���� : LayerLib, Compare() �޼ҵ�� ����, GetMask() �޼ҵ� �߰�, Include() �޼ҵ� �߰�
   2023. 03. 26.
   - Ut23 ���̺귯���� ����
*/

namespace Rito
{
    public static class LayerLib
    {
        /// <summary> ���̾� �ƹ��͵� ���� X </summary>
        public const int None = 0;
        /// <summary> ��� ���̾� ���� </summary>
        public const int All = -1;

        #region Public Methods

        /// <summary>
        /// ���̾� ��ġ ��
        /// </summary>
        public static bool Compare(in int layerA, in int layerB)
        {
            return layerA.Equals(layerB);
        }

        /// <summary> ���̾� ��ȣ���� �޾�, ���̾� ����ũ ���� </summary>
        public static LayerMask GetMask(params int[] layers)
        {
            int mask = 0;

            for (int i = 0; i < layers.Length; i++)
            {
                mask |= (1 << layers[i]);
            }
            return mask;
        }

        /// <summary> ���̾��ũ�� ���̾ �����ϴ��� �˻� </summary>
        public static bool Contains(in LayerMask mask, in int layer)
        {
            return (mask.value | 1 << layer).Equals(mask.value);
        }

        #endregion // ==========================================================

        #region Comparations - GameObject, Transform Overloadings

        /// <summary>
        /// ���̾� �� : ���ӿ�����Ʈ &lt;-&gt; ���̾� ��ȣ
        /// </summary>
        public static bool Compare(in GameObject gobj, in int layer)
        {
            if (gobj == null) return false;

            return gobj.layer.Equals(layer);
        }

        /// <summary>
        /// ���̾� �� : ���̾� ��ȣ &lt;-&gt; ���ӿ�����Ʈ 
        /// </summary>
        public static bool Compare(in int layer, in GameObject gobj)
        {
            if (gobj == null) return false;

            return gobj.layer.Equals(layer);
        }

        /// <summary>
        /// ���̾� �� : ���ӿ�����Ʈ &lt;-&gt; ���ӿ�����Ʈ
        /// </summary>
        public static bool Compare(in GameObject gobjA, in GameObject gobjB)
        {
            if (gobjA == null) return false;
            if (gobjB == null) return false;

            return gobjA.layer.Equals(gobjB.layer);
        }

        // ====================================================================

        /// <summary>
        /// ���̾� �� : Ʈ������ &lt;-&gt; ���̾� ��ȣ
        /// </summary>
        public static bool Compare(in Transform transform, in int layer)
        {
            if (transform == null) return false;

            return transform.gameObject.layer.Equals(layer);
        }


        /// <summary>
        /// ���̾� �� : ���̾� ��ȣ &lt;-&gt; Ʈ������
        /// </summary>
        public static bool Compare(in int layer, in Transform transform)
        {
            if (transform == null) return false;

            return transform.gameObject.layer.Equals(layer);
        }

        /// <summary>
        /// ���̾� �� : Ʈ������
        /// </summary>
        public static bool Compare(in Transform trA, in Transform trB)
        {
            if (trA == null) return false;
            if (trB == null) return false;

            return trA.gameObject.layer.Equals(trB.gameObject.layer);
        }

        // ====================================================================

        /// <summary>
        /// ���̾� �� : ���ӿ�����Ʈ &lt;-&gt; Ʈ������
        /// </summary>
        public static bool Compare(in GameObject gobj, in Transform tr)
        {
            if (gobj == null) return false;
            if (tr == null) return false;

            return gobj.layer.Equals(tr.gameObject.layer);
        }

        /// <summary>
        /// ���̾� �� : Ʈ������ &lt;-&gt; ���ӿ�����Ʈ
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