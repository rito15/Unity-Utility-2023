using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-06 PM 4:33:43
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - Cast, Get, UI 하위 클래스로 분리
*/

namespace Rito.ut23
{
    /// <summary>
    /// [간편 레이캐스트]
    /// <para/> 1. Cast: 레이캐스트 검사 (return: bool)
    /// <para/> 2. Get: 레이캐스트 및 레이 도달 위치 반환 (return: Vector3)
    /// <para/> 3. UI: UI 관련(TODO)
    /// </summary>
    public static class Raycaster
    {
        // 레이캐스트 거리를 지정하지 않았을 때 지정할 기본 거리
        private const float DefaultDistance = 9999f;

        // 기본 디버그 레이 지속시간
        private const float DefaultDebugRayDuration = 5f;

        /// <summary> 
        /// <para/> 1. 레이캐스트 여부 확인(return bool)
        /// <para/> - CamToMouse : 활성화된 카메라 -> 마우스 위치
        /// <para/> - CamToPos   : 활성화된 카메라 -> 특정 위치
        /// <para/> - AtoB       : 위치 A -> 위치 B
        /// <para/> - SkyToPos   : height만큼 높은 위치 -> 해당 위치
        /// </summary>
        public static class Cast
        {
            #region Raycast 3D
            /// <summary> 현재 활성화된 카메라로부터 마우스 위치에 레이캐스트 </summary>
            public static bool CamToMouse (
                out RaycastHit              hit
            
               ,in  float                   distance    = DefaultDistance
               ,in  int                     layerMask   = -1
               ,    QueryTriggerInteraction interaction = QueryTriggerInteraction.Collide
            )
            {
                return Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, layerMask, interaction);
            }
            
            /// <summary> Y축 상단으로 height만큼 떨어진 지점에서 대상 지점으로 레이캐스트 </summary>
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

            /// <summary> 활성화된 카메라로부터 마우스 위치에 레이캐스트 검사 </summary>
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

            /// <summary> 활성화된 카메라로부터 마우스 위치에 레이캐스트 검사 </summary>
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

            /// <summary> 활성화된 카메라로부터 특정 위치로 레이캐스트 </summary>
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

            /// <summary> 활성화된 카메라로부터 특정 위치로 레이캐스트 </summary>
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

            /// <summary> A 위치에서 B 위치로 레이캐스트 </summary>
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

            /// <summary> A 위치에서 B 위치로 레이캐스트 </summary>
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

            /// <summary> from 위치에서 dir 방향으로 레이캐스트 </summary>
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
            /// <summary> from 위치에서 dir 방향으로 레이캐스트 </summary>
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

            /// <summary> 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트</summary>
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
            /// <summary> 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트</summary>
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
        /// <para/> 2. 레이캐스트 지점 리턴(return Vector3)
        /// <para/> (* 레이캐스트 실패 시, Vector3.NegativeInfinity 리턴)
        /// <para/> - GetCamToMouse : 활성화된 카메라 -> 마우스 위치
        /// <para/> - GetCamToPos   : 활성화된 카메라 -> 특정 위치
        /// <para/> - GetAtoB       : 위치 A -> 위치 B
        /// <para/> - GetSkyToPos   : height만큼 높은 위치 -> 해당 위치 </summary>
        public static class Get
        {
            #region RayCast

            /// <summary>
            /// 활성화된 카메라로부터 마우스 위치로 레이캐스트하여 3D 좌표 얻어내기
            /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
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
            /// 활성화된 카메라로부터 특정 위치로 레이캐스트하여 3D 좌표 얻어내기
            /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
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
            /// 해당 위치의 수직 꼭대기에서 해당 위치로 레이캐스트(-y 방향)하여 3D 좌표 얻어내기
            /// <para/> height : 해당 위치로부터 수직 높이
            /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
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
            /// 위치 A에서 B로 레이캐스트하여 3D 좌표 얻어내기
            /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
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
            /// from에서 dir 방향으로 레이캐스트하여 3D 좌표 얻어내기
            /// <para/> 레이캐스트 실패하는 경우 Vector3.negativeInfinity 리턴
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

            /// <summary> A 위치에서 B 위치로 레이캐스트하고, 튕겨나온 반사 벡터를 방향벡터(크기 1)로 리턴
            /// <para/> 만약 A-B 캐스트가 실패할 경우, A-B 방향벡터를 리턴
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

                // 레이캐스트 A->B 실패 : A->B 방향벡터를 리턴
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
            /// <summary> A 위치에서 B 위치로 레이캐스트하고, 튕겨나온 반사 벡터를 방향벡터(크기 1)로 리턴
            /// <para/> 만약 A-B 캐스트가 실패할 경우, A-B 방향벡터를 리턴
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

                // 레이캐스트 A->B 실패 : A->B 방향벡터를 리턴
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
            /// 마우스 커서가 UI 위에 있는지 검사 <para/>
            /// 이벤트 시스템이 없는 경우, 빈 게임오브젝트를 생성해서 자동으로 추가
            /// </summary>
            public static bool IsCursorOverUI()
            {
                // 이벤트 시스템이 없는 경우 생성
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