using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-06 PM 4:33:43
// �ۼ��� : Rito

/*
   [ ���]
   2023. 03. 26.
   - Ut23 ���̺귯���� ����
   - Cast, Get, UI ���� Ŭ������ �и�
*/

namespace Rito.ut23
{
    /// <summary>
    /// [���� ����ĳ��Ʈ]
    /// <para/> 1. Cast: ����ĳ��Ʈ �˻� (return: bool)
    /// <para/> 2. Get: ����ĳ��Ʈ �� ���� ���� ��ġ ��ȯ (return: Vector3)
    /// <para/> 3. UI: UI ����(TODO)
    /// </summary>
    public static class Raycaster
    {
        // ����ĳ��Ʈ �Ÿ��� �������� �ʾ��� �� ������ �⺻ �Ÿ�
        private const float DefaultDistance = 9999f;

        // �⺻ ����� ���� ���ӽð�
        private const float DefaultDebugRayDuration = 5f;

        /// <summary> 
        /// <para/> 1. ����ĳ��Ʈ ���� Ȯ��(return bool)
        /// <para/> - CamToMouse : Ȱ��ȭ�� ī�޶� -> ���콺 ��ġ
        /// <para/> - CamToPos   : Ȱ��ȭ�� ī�޶� -> Ư�� ��ġ
        /// <para/> - AtoB       : ��ġ A -> ��ġ B
        /// <para/> - SkyToPos   : height��ŭ ���� ��ġ -> �ش� ��ġ
        /// </summary>
        public static class Cast
        {
            #region Raycast 3D
            /// <summary> ���� Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ </summary>
            public static bool CamToMouse (
                out RaycastHit              hit
            
               ,in  float                   distance    = DefaultDistance
               ,in  int                     layerMask   = -1
               ,    QueryTriggerInteraction interaction = QueryTriggerInteraction.Collide
            )
            {
                return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, layerMask, interaction);
            }
            
            /// <summary> Y�� ������� height��ŭ ������ �������� ��� �������� ����ĳ��Ʈ </summary>
            public static bool SkyToPoint (
                out RaycastHit              hit
               ,in  Vector3                 point
            
               ,in  float                   height      = DefaultDistance
               ,in  int                     layerMask   = -1
               ,    QueryTriggerInteraction interaction = QueryTriggerInteraction.Collide
            )
            {
                Ray ray = new Ray(new Vector3(point.x, point.y + height, point.x), Vector3.down);
                return Physics.Raycast(ray, out hit, height, layerMask, interaction);
            }
            
            #endregion

            #region RayCast : Camera -> Mouse

            /// <summary> Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ �˻� </summary>
            public static bool CamToMouse (
                out RaycastHit hit

               ,in  int        layerMask     = -1
               ,in  float      distance      = DefaultDistance
               ,in  bool       debug         = false
               ,in  float      debugDuration = DefaultDebugRayDuration
               ,    Color      debugColor    = default
            )
            {
                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * distance, debugColor, debugDuration);
                }
                return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, layerMask);
            }

            /// <summary> Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ �˻� </summary>
            public static bool CamToMouse (
                in int   layerMask     = -1
               ,in float distance      = DefaultDistance
               ,in bool  debug         = false
               ,in float debugDuration = DefaultDebugRayDuration
               ,   Color debugColor    = default
            )
            {
                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * distance, debugColor, debugDuration);
                }
                return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _, distance, layerMask);
            }

            #endregion // ================================================================

            #region RayCast : Camera -> Position

            /// <summary> Ȱ��ȭ�� ī�޶�κ��� Ư�� ��ġ�� ����ĳ��Ʈ </summary>
            public static bool CamToPos (
                out RaycastHit hit
               ,in  Vector3    pos

               ,in  int        layerMask     = -1
               ,in  float      distance      = DefaultDistance
               ,in  bool       debug         = false
               ,in  float      debugDuration = DefaultDebugRayDuration
               ,    Color      debugColor    = default
            )
            {
                Vector3 cameraPos = Camera.main.transform.position;
                Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(cameraPos, ray.direction * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out hit, distance, layerMask);
            }

            /// <summary> Ȱ��ȭ�� ī�޶�κ��� Ư�� ��ġ�� ����ĳ��Ʈ </summary>
            public static bool CamToPos (
                in Vector3 pos

               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 cameraPos = Camera.main.transform.position;
                Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(cameraPos, ray.direction * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out _, distance, layerMask);
            }

            #endregion // ================================================================

            #region RayCast : A -> B

            /// <summary> A ��ġ���� B ��ġ�� ����ĳ��Ʈ </summary>
            public static bool AtoB (
                out RaycastHit hit
               ,in  Vector3    posA
               ,in  Vector3    posB
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 dir = (posB - posA).normalized;
                Ray ray = new Ray(posA, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(posA, dir * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out hit, distance, layerMask);
            }

            /// <summary> A ��ġ���� B ��ġ�� ����ĳ��Ʈ </summary>
            public static bool AtoB (
                in Vector3 posA
               ,in Vector3 posB
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 dir = (posB - posA).normalized;
                Ray ray = new Ray(posA, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(posA, dir * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out _, distance, layerMask);
            }

            #endregion // ================================================================

            #region RayCast : A -(Dir)->

            /// <summary> from ��ġ���� dir �������� ����ĳ��Ʈ </summary>
            public static bool FromDir (
                out RaycastHit hit
               ,in  Vector3    from
               ,in  Vector3    dir
                
               ,in int         layerMask     = -1
               ,in float       distance      = DefaultDistance
               ,in bool        debug         = false
               ,in float       debugDuration = DefaultDebugRayDuration
               ,   Color       debugColor    = default
            )
            {
                Ray ray = new Ray(from, dir.normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(from, dir * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out hit, distance, layerMask);
            }
            /// <summary> from ��ġ���� dir �������� ����ĳ��Ʈ </summary>
            public static bool FromDir (
                in Vector3 from
               ,in Vector3 dir
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Ray ray = new Ray(from, dir.normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(from, dir * distance, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out _, distance, layerMask);
            }

            #endregion // ==========================================================

            #region RayCast : Sky -> Position

            /// <summary> �ش� ��ġ�� ���� ����⿡�� �ش� ��ġ�� ����ĳ��Ʈ</summary>
            public static bool SkyToPos (
                out RaycastHit hit
               ,in  Vector3    pos
                
               ,in  int        layerMask     = -1
               ,in  float      height        = DefaultDistance
               ,in  bool       debug         = false
               ,in  float      debugDuration = DefaultDebugRayDuration
               ,    Color      debugColor    = default
            )
            {
                Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
                Vector3 dir = (pos - sky).normalized;
                Ray ray = new Ray(sky, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(sky, dir * height, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out hit, height * 2f, layerMask);
            }
            /// <summary> �ش� ��ġ�� ���� ����⿡�� �ش� ��ġ�� ����ĳ��Ʈ</summary>
            public static bool SkyToPos (
                in Vector3 pos
            
               ,in  int    layerMask     = -1
               ,in  float  height        = DefaultDistance
               ,in  bool   debug         = false
               ,in  float  debugDuration = DefaultDebugRayDuration
               ,    Color  debugColor    = default
            )
            {
                Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
                Vector3 dir = (pos - sky).normalized;
                Ray ray = new Ray(sky, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.red;
                    Debug.DrawRay(sky, dir * height, debugColor, debugDuration);
                }

                return Physics.Raycast(ray, out _, height * 2f, layerMask);
            }

            #endregion // ================================================================
        }

        /// <summary> 
        /// <para/> 2. ����ĳ��Ʈ ���� ����(return Vector3)
        /// <para/> (* ����ĳ��Ʈ ���� ��, Vector3.NegativeInfinity ����)
        /// <para/> - GetCamToMouse : Ȱ��ȭ�� ī�޶� -> ���콺 ��ġ
        /// <para/> - GetCamToPos   : Ȱ��ȭ�� ī�޶� -> Ư�� ��ġ
        /// <para/> - GetAtoB       : ��ġ A -> ��ġ B
        /// <para/> - GetSkyToPos   : height��ŭ ���� ��ġ -> �ش� ��ġ </summary>
        public static class Get
        {
            #region RayCast

            /// <summary>
            /// Ȱ��ȭ�� ī�޶�κ��� ���콺 ��ġ�� ����ĳ��Ʈ�Ͽ� 3D ��ǥ ����
            /// <para/> ����ĳ��Ʈ �����ϴ� ��� Vector3.negativeInfinity ����
            /// </summary>
            public static Vector3 GetCamToMouse (
                in int   layerMask     = -1
               ,in float distance      = DefaultDistance
               ,in bool  debug         = false
               ,in float debugDuration = DefaultDebugRayDuration
               ,   Color debugColor    = default
            )
            {
                if (debug)
                {
                    if (debugColor == default) debugColor = Color.blue;
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction * distance, debugColor, debugDuration);
                }

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, distance, layerMask))
                    return hit.point;

                return Vector3.negativeInfinity;
            }

            /// <summary>
            /// Ȱ��ȭ�� ī�޶�κ��� Ư�� ��ġ�� ����ĳ��Ʈ�Ͽ� 3D ��ǥ ����
            /// <para/> ����ĳ��Ʈ �����ϴ� ��� Vector3.negativeInfinity ����
            /// </summary>
            public static Vector3 GetCamToPos (
                in Vector3 pos
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 cameraPos = Camera.main.transform.position;
                Ray ray = new Ray(cameraPos, (pos - cameraPos).normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.blue;
                    Debug.DrawRay(cameraPos, ray.direction * distance, debugColor, debugDuration);
                }

                if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
                    return hit.point;

                return Vector3.negativeInfinity;
            }

            /// <summary>
            /// �ش� ��ġ�� ���� ����⿡�� �ش� ��ġ�� ����ĳ��Ʈ(-y ����)�Ͽ� 3D ��ǥ ����
            /// <para/> height : �ش� ��ġ�κ��� ���� ����
            /// <para/> ����ĳ��Ʈ �����ϴ� ��� Vector3.negativeInfinity ����
            /// </summary>
            public static Vector3 GetSkyToPos (
                in Vector3 pos
            
               ,in int     layerMask     = -1
               ,in float   height        = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 sky = new Vector3(pos.x, pos.y + height, pos.z);
                Vector3 dir = (pos - sky).normalized;
                Ray ray = new Ray(sky, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.blue;
                    Debug.DrawRay(sky, dir * height, debugColor, debugDuration);
                }

                if (Physics.Raycast(ray, out RaycastHit hit, height * 2f, layerMask))
                    return hit.point;

                return Vector3.negativeInfinity;
            }

            /// <summary>
            /// ��ġ A���� B�� ����ĳ��Ʈ�Ͽ� 3D ��ǥ ����
            /// <para/> ����ĳ��Ʈ �����ϴ� ��� Vector3.negativeInfinity ����
            /// </summary>
            public static Vector3 GetAtoB (
                in Vector3 posA
               ,in Vector3 posB
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Vector3 dir = (posB - posA).normalized;
                Ray ray = new Ray(posA, dir);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.blue;
                    Debug.DrawRay(posA, dir * distance, debugColor, debugDuration);
                }

                if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
                    return hit.point;

                return Vector3.negativeInfinity;
            }

            /// <summary>
            /// from���� dir �������� ����ĳ��Ʈ�Ͽ� 3D ��ǥ ����
            /// <para/> ����ĳ��Ʈ �����ϴ� ��� Vector3.negativeInfinity ����
            /// </summary>
            public static Vector3 GetFromDir (
                in Vector3 from
               ,in Vector3 dir
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
               ,   Color   debugColor    = default
            )
            {
                Ray ray = new Ray(from, dir.normalized);

                if (debug)
                {
                    if (debugColor == default) debugColor = Color.blue;
                    Debug.DrawRay(from, dir * distance, debugColor, debugDuration);
                }

                if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
                    return hit.point;

                return Vector3.negativeInfinity;
            }

            #endregion // ================================================================

            #region RayCast - Reflect

            /// <summary> A ��ġ���� B ��ġ�� ����ĳ��Ʈ�ϰ�, ƨ�ܳ��� �ݻ� ���͸� ���⺤��(ũ�� 1)�� ����
            /// <para/> ���� A-B ĳ��Ʈ�� ������ ���, A-B ���⺤�͸� ����
            /// </summary>
            public static Vector3 GetReflectDirFromAtoB (
                out RaycastHit hit 
               ,in  Vector3    posA
               ,in  Vector3    posB
            
               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
            )
            {
                Vector3 dir = (posB - posA).normalized;
                Ray ray = new Ray(posA, dir);

                // ����ĳ��Ʈ A->B ���� : A->B ���⺤�͸� ����
                if (Physics.Raycast(ray, out hit, distance, layerMask) == false)
                    return dir;

                if (debug)
                {
                    Debug.DrawRay(posA, dir * distance, Color.red, debugDuration); // dir
                    Debug.DrawRay(hit.point, hit.normal * distance, Color.Lerp(Color.red, Color.blue, 0.5f), debugDuration); // normal
                    Debug.DrawRay(hit.point, Vector3.Reflect(dir, hit.normal) * distance, Color.blue, debugDuration); // reflect
                }

                return Vector3.Reflect(dir, hit.normal);
            }
            /// <summary> A ��ġ���� B ��ġ�� ����ĳ��Ʈ�ϰ�, ƨ�ܳ��� �ݻ� ���͸� ���⺤��(ũ�� 1)�� ����
            /// <para/> ���� A-B ĳ��Ʈ�� ������ ���, A-B ���⺤�͸� ����
            /// </summary>
            public static Vector3 GetReflectDirFromAtoB (
                in Vector3 posA
               ,in Vector3 posB

               ,in int     layerMask     = -1
               ,in float   distance      = DefaultDistance
               ,in bool    debug         = false
               ,in float   debugDuration = DefaultDebugRayDuration
            )
            {
                Vector3 dir = (posB - posA).normalized;
                Ray ray = new Ray(posA, dir);

                // ����ĳ��Ʈ A->B ���� : A->B ���⺤�͸� ����
                if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask) == false)
                    return dir;

                if (debug)
                {
                    Debug.DrawRay(posA, dir * distance, Color.red, debugDuration); // dir
                    Debug.DrawRay(hit.point, hit.normal * distance, Color.Lerp(Color.red, Color.blue, 0.5f), debugDuration); // normal
                    Debug.DrawRay(hit.point, Vector3.Reflect(dir, hit.normal) * distance, Color.blue, debugDuration); // reflect
                }

                return Vector3.Reflect(dir, hit.normal);
            }

            #endregion // ==========================================================
        }

        public static class UI
        {
            #region UI Check

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
}