
using UnityEngine;

namespace Rito.ut23.Attributes
{
    /// <summary>
    /// 2020. 05. 15.
    /// <para/> 헤더 한 줄 작성 + 여러 필드를 하나의 컬러 박스로 묶어주기
    /// </summary>
    public class BoxHeaderAttribute : HeaderAttributeBase
    {
        public string HeaderText { get; private set; } = "";

        // 몇 줄의 필드를 묶을 것인지 결정
        public int FieldCount { get; private set; } = 0;

        public EColor HeaderColor { get; set; } = EColor.Black;
        public EColor BoxColor { get; set; } = EColor.None;

        // 추가 하단 높이
        public float BottomHeight { get; private set; } = 0f;

        // 색상 직접 결정할 수 있게 해주는 색상 선택 도구 노출
        public bool UseColorPicker { get; private set; } = false;

        // Header, (Field, Bottom, UsePicker)
        public BoxHeaderAttribute(
            string headerText,
            int    fieldCount = 0,
            float  bottomHeight = 0f,
            bool   useColorPicker = false)
        {
            HeaderText = headerText;
            FieldCount = fieldCount;
            BottomHeight = bottomHeight;
            UseColorPicker = useColorPicker;

            if (FieldCount < 0)
                FieldCount = 0;

            if (BottomHeight < 0f)
                BottomHeight = 0f;
        }

        // Header, Color, (Field, Bottom, UsePicker)
        public BoxHeaderAttribute(
            string headerText, 
            EColor headerTextColor,
            int    fieldCount = 0,
            float  bottomHeight = 0f,
            bool   useColorPicker = false)

            : this(headerText, fieldCount, bottomHeight, useColorPicker)
        {
            HeaderColor = headerTextColor;
        }



        // Header, Field, Color, (Bottom, UsePicker)
        public BoxHeaderAttribute(
            string headerText,
            int    fieldCount,
            EColor headerTextColor,
            float  bottomHeight = 0f,
            bool   useColorPicker = false)

            : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }

        // Header, Field, Bottom, (Color, UsePicker)
        public BoxHeaderAttribute(
            string headerText,
            int    fieldCount,
            float  bottomHeight,
            EColor headerTextColor = EColor.Black,
            bool   useColorPicker = false)

            : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }

        // Header, Field, UsePicker, (Bottom, Color)
        public BoxHeaderAttribute(
            string headerText,
            int    fieldCount,
            bool   useColorPicker,
            float  bottomHeight = 0f,
            EColor headerTextColor = EColor.Black)

            : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }



        // Header, Color, UsePicker, (Field, Bottom)
        public BoxHeaderAttribute(
            string headerText,
            EColor headerTextColor,
            bool   useColorPicker,
            int    fieldCount = 0,
            float  bottomHeight = 0f)

            : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }

        // Header, UsePicker, (Color, Field, Bottom)
        public BoxHeaderAttribute(
        string headerText,
        bool   useColorPicker,
        EColor headerTextColor,
        int    fieldCount = 0,
        float  bottomHeight = 0f)

        : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }

        // Header, UsePicker, Field, (Bottom, Color)
        public BoxHeaderAttribute(
        string headerText,
        bool   useColorPicker,
        int    fieldCount,
        float  bottomHeight = 0f,
        EColor headerTextColor = EColor.Black)

        : this(headerText, headerTextColor, fieldCount, bottomHeight, useColorPicker) { }
    }
}
