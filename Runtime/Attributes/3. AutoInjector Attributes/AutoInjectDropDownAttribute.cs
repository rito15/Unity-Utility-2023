/*
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 2020-05-18 PM 9:12:43
    /// <para/> 웨않되
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class AutoInjectDropDownAttribute : PropertyAttribute
    {
        public EInjection2 Option { get; private set; } = EInjection2.SelectInInspector;

        public EModeOption ModeOption { get; private set; } = EModeOption.Always;


        public AutoInjectDropDownAttribute() { }

        public AutoInjectDropDownAttribute(EInjection2 option) => Option = option;

        public AutoInjectDropDownAttribute(EInjection2 option, EModeOption modeOption) : this(option) => ModeOption = modeOption;

        public AutoInjectDropDownAttribute(EModeOption modeOption) => ModeOption = modeOption;

        public AutoInjectDropDownAttribute(EModeOption modeOption, EInjection2 option) : this(option, modeOption) { }
    }

    // 애트리뷰트에서 선택할 옵션
    public enum EInjection2
    {
        SelectInInspector,

        GetComponent,
        GetComponentInChildren,
        /// <summary> 자기 게임오브젝트를 제외하고 자식 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInChildrenOnly,
        GetComponentInparent,
        /// <summary> 자기 게임오브젝트를 제외하고 부모 게임오브젝트들을 대상으로 수행합니다. </summary>
        GetComponentInparentOnly,

        FindObjectOfType
    }
}
*/