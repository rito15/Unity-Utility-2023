using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-06 PM 4:33:43
// �ۼ��� : Rito

/*
    [���]

    - CamToMouse(hit, distance, layer) : ���� Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ
    - SkyToPoint(hit, point, height, layer) : Y�� ������� height��ŭ ������ �������� ��� �������� ����ĳ��Ʈ
    - IsCursorOverUI() : ���콺 Ŀ���� UI ���� �ִ��� ���� �˻�
*/
/*
   [ ���]
   2023. 03. 26.
   - Ut23 ���̺귯���� ����
*/

namespace Rito.ut23
{
    public static class Raycaster
    {
        public const float MaxRayDistance = 9999f;

        /***********************************************************************
        *                               Raycast 3D
        ***********************************************************************/
        #region .
        /// <summary> ���� Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ </summary>
        public static bool CamToMouse(out RaycastHit hit, in float distance = MaxRayDistance, in int layerMask = -1,
            QueryTriggerInteraction interaction = QueryTriggerInteraction.Collide)
        {
            return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, layerMask, interaction);
        }

        /// <summary> Y�� ������� height��ŭ ������ �������� ��� �������� ����ĳ��Ʈ </summary>
        public static bool SkyToPoint(out RaycastHit hit, in Vector3 point, in float height = MaxRayDistance,
            in int layerMask = -1, QueryTriggerInteraction interaction = QueryTriggerInteraction.Collide)
        {
            Ray ray = new Ray(new Vector3(point.x, point.y + height, point.x), Vector3.down);
            return Physics.Raycast(ray, out hit, height, layerMask, interaction);
        }

        #endregion

        /***********************************************************************
        *                               UI Raycast
        ***********************************************************************/
        #region .

        /// <summary>
        /// ���콺 Ŀ���� UI ���� �ִ��� �˻� <para/>
        /// �̺�Ʈ �ý����� ���� ���, �� ���ӿ�����Ʈ�� �����ؼ� �ڵ����� �߰�
        /// </summary>
        public static bool IsCursorOverUI()
        {
            // �̺�Ʈ �ý����� ���� ��� ����
            if (UnityEngine.EventSystems.EventSystem.current == null)
            {
                new GameObject("EventSystem",
                    typeof(UnityEngine.EventSystems.EventSystem),
                    typeof(UnityEngine.EventSystems.StandaloneInputModule)
                );
            }

            return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        }

        #endregion
    }
}