using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 2020-05-18 AM 1:11:23
    /// <para/> 클릭하면 메소드를 실행하는 버튼을 인스펙터에 표시합니다.
    /// <para/> * 해당 스크립트에 작성된 메소드 이름을 파라미터로 입력합니다.
    /// <para/> * 매개변수가 없는 메소드만 등록할 수 있습니다.
    /// <para/> * 여러 개의 스트링을 입력할 경우, 각각의 버튼이 생성됩니다.
    /// <para/> * 지정된 필드는 기본적으로 숨겨지며, ShowField 옵션을 통해 나타낼 수 있습니다.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class MethodButtonAttribute : PropertyAttribute
    {
        /// <summary> 버튼의 색상을 지정합니다. </summary>
        public EColor ButtonColor { get; set; } = EColor.None;

        /// <summary> 글자의 색상을 지정합니다. </summary>
        public EColor TextColor { get; set; } = EColor.Black;

        /// <summary> 버튼으로 등록할 메소드들의 이름을 등록합니다. </summary>
        public string[] MethodNames { get; private set; }

        /// <summary> 글자의 크기를 지정합니다. (최소 12) </summary>
        public int TextSize { get; set; } = 12;

        /// <summary> 각 버튼의 높이를 지정합니다. (최소 18) </summary>
        public float Height { get; set; } = 18f;

        /// <summary> 필드를 인스펙터에서 숨기지 않고 함께 표시합니다. </summary>
        public bool ShowField { get; set; } = false;

        public MethodButtonAttribute(params string[] methodName) => MethodNames = methodName;
    }
}