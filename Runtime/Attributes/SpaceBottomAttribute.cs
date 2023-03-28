using UnityEngine;
using Rito.ut23.Extensions;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// <para/> 날짜 : 2020-05-20 AM 12:31:40
    /// <para/> 필드 하단 공백
    /// <para/> * 배열에는 작동하지 않음
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SpaceBottomAttribute : PropertyAttribute
    {
        public float SpaceHeight { get; set; }

        public SpaceBottomAttribute(float space = 9f) => SpaceHeight = space.Ex_ClampMin(0f);
    }
}