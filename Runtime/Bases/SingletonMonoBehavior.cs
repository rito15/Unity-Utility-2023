
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성자 : Rito
// 최초 작성 : 2021-01-25 PM 2:36:18
/*
   [ 기록]
   2023. 03. 27.
   - Ut23 라이브러리에 편입
   - 매크로상수 옵션 -> public 필드 옵션으로 이동(라이브러리화 감안)
     - 디버그 On/Off
     - 공통 부모 설정
   - 상속 클래스에서 옵션 필드 강제 오버라이딩 추가
     - 예시:
       - public override bool? _AllowDebug => false;
       - public override bool? _DontDestroyOnLoad => false;
       - public override bool? _SetParentGameObject => false;
       - public override string _ParentGameObjectName => "PARENT_NAME";
   - 디버그 로그 디테일 개선
   - 디버그 로그에 컨텍스트 추가
*/

namespace Rito.ut23
{
    [DisallowMultipleComponent]
    public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        /***********************************************************************
        *                       Public Static Properties
        ***********************************************************************/
        #region .
        /// <summary> 싱글톤 인스턴스 Getter </summary>
        public static T I
        {
            get
            {
                // 객체 참조 확인
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    // 인스턴스 가진 오브젝트가 존재하지 않을 경우, 빈 오브젝트를 임의로 생성하여 인스턴스 할당
                    if (_instance == null)
                    {
                        // 게임 오브젝트에 클래스 컴포넌트 추가 후 인스턴스 할당
                        _instance = ContainerObject.GetComponent<T>();
                    }
                }
                return _instance;
            }
        }

        /// <summary> 싱글톤 인스턴스 Getter </summary>
        public static T Instance => I;

        /// <summary> 싱글톤 게임오브젝트의 참조 </summary>
        public static GameObject ContainerObject
        {
            get
            {
                if (_containerObject == null)
                    CreateContainerObjectAndThis();

                return _containerObject;
            }
        }

        #endregion
        /***********************************************************************
        *                       Private Static Variables
        ***********************************************************************/
        #region .

        /// <summary> 싱글톤 인스턴스 </summary>
        private static T _instance;
        private static GameObject _containerObject;

        #endregion
        /***********************************************************************
        *                       Private Static Methods
        ***********************************************************************/
        #region .
        protected void OptionalDebug(string msg)
        {
            if (allowDebug)
                Debug.Log(msg);
        }
        protected void OptionalDebug(string msg, GameObject context)
        {
            if (allowDebug)
                Debug.Log(msg, context);
        }

        /// <summary> 싱글톤 컴포넌트를 담을 게임 오브젝트 + 싱글톤 컴포넌트 생성 </summary>
        private static void CreateContainerObjectAndThis()
        {
            // null이 아니면 Do Nothing
            if (_containerObject != null) return;

            // 빈 게임 오브젝트 생성
            _containerObject = new GameObject($"[Singleton] {typeof(T)}");

            // 인스턴스가 없던 경우, 새로 생성
            if (_instance == null)
                _instance = ContainerObject.AddComponent<T>();
        }

        #endregion
        /***********************************************************************
        *                       Public Options & Methods
        ***********************************************************************/
        #region .

        // 인스펙터 설정 옵션
        public bool _allowDebug = true;
        public bool _dontDestroyOnLoad = false;
        public bool _setParentGameObject = true;
        public string _parentGameObjectName = "[Singleton Objects]";

        // 자식 클래스에서 오버라이딩
        public virtual bool? _AllowDebug => null;
        public virtual bool? _DontDestroyOnLoad => null;
        public virtual bool? _SetParentGameObject => null;
        public virtual string _ParentGameObjectName => null;

        // 자식 클래스 스크립트에서 오버라이딩한 경우, 인스펙터 설정보다 우선 적용
        public bool allowDebug => (_AllowDebug.HasValue) ? (_allowDebug = _AllowDebug.Value) : _allowDebug;
        public bool dontDestroyOnLoad => (_DontDestroyOnLoad.HasValue) ? (_dontDestroyOnLoad = _DontDestroyOnLoad.Value) : _dontDestroyOnLoad;
        public bool setParentGameObject => (_SetParentGameObject.HasValue) ? (_setParentGameObject = _SetParentGameObject.Value) : _setParentGameObject;
        public string parentGameObjectName => (_ParentGameObjectName != null) ? (_parentGameObjectName = _ParentGameObjectName) : _parentGameObjectName;

        /// <summary> 공통 부모 게임오브젝트에 모아주기 </summary>
        protected void GatherGameObjectIntoParentObject()
        {
            // 게임오브젝트 "Singleton Objects" 찾기 or 생성
            GameObject parentContainer = GameObject.Find(parentGameObjectName);
            if (parentContainer == null)
                parentContainer = new GameObject(parentGameObjectName);

            // 부모 오브젝트에 넣어주기
            _containerObject.transform.SetParent(parentContainer.transform);

            OptionalDebug($"싱글톤 {typeof(T)} (GO: {gameObject.name}) => 부모 게임 오브젝트 {parentGameObjectName}로 이동", gameObject);
        }

        #endregion

        protected virtual void Awake()
        {
            // 싱글톤 인스턴스가 미리 존재하지 않았을 경우, 본인으로 초기화
            if (_instance == null)
            {
                OptionalDebug($"싱글톤 생성 : {typeof(T)} (GO: {gameObject.name})", gameObject);

                // 싱글톤 컴포넌트 초기화
                _instance = this as T;

                // 싱글톤 컴포넌트를 담고 있는 게임오브젝트로 초기화
                _containerObject = gameObject;
            }

            // 싱글톤 인스턴스가 존재하는데, 본인이 아닐 경우, 스스로(컴포넌트)를 파괴
            if (_instance != null && _instance != this)
            {
                var components = gameObject.GetComponents<Component>();

                // 만약 게임 오브젝트에 컴포넌트가 자신만 있었다면, 게임 오브젝트도 파괴
                if (components.Length <= 2)
                {
                    OptionalDebug($"이미 {typeof(T)} 싱글톤이 존재하므로 게임오브젝트를 파괴합니다. (게임오브젝트: {gameObject.name})");
                    Destroy(gameObject);
                }

                // 다른 컴포넌트도 존재하면 자신만 파괴
                else
                {
                    OptionalDebug($"이미 {typeof(T)} 싱글톤이 존재하므로 컴포넌트를 파괴합니다. (게임오브젝트: {gameObject.name})", gameObject);
                    Destroy(this);
                }

                return;
            }

            // 옵션 적용: DDOL
            if (dontDestroyOnLoad)
            {
                OptionalDebug($"싱글톤 {typeof(T)} (GO: {gameObject.name}) => Don't Destroy On Load", gameObject);
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }

            // 옵션 적용: 부모 이동
            else if (setParentGameObject)
            {
                GatherGameObjectIntoParentObject();
            }
        }
    }
}