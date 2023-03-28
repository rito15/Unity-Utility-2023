using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 날짜 : 2021-06-15 PM 3:26:00
// 작성자 : Rito

/*
   [ 기록]
   2023. 03. 26.
   - Ut23 라이브러리에 편입
   - 이름 규칙 변경: 항상 Ex_ 접두어 사용
*/

namespace Rito.ut23.Extensions
{
    public static class ColorExtension
    {
        /***********************************************************************
        *                               Add
        ***********************************************************************/
        #region .
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddR(in this Color color, float r)
        {
            return new Color(color.r + r, color.g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddG(in this Color color, float g)
        {
            return new Color(color.r, color.g + g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddB(in this Color color, float b)
        {
            return new Color(color.r, color.g, color.b + b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddA(in this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, color.a + a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddRG(in this Color color, float rg)
        {
            return new Color(color.r + rg, color.g + rg, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddGB(in this Color color, float gb)
        {
            return new Color(color.r, color.g + gb, color.b + gb, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddRB(in this Color color, float rb)
        {
            return new Color(color.r + rb, color.g, color.b + rb, color.a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddRGB(in this Color color, float rgb)
        {
            return new Color(color.r + rgb, color.g + rgb, color.b + rgb, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddRGB(in this Color color, float r, float g, float b)
        {
            return new Color(color.r + r, color.g + g, color.b + b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_AddRGB(in this Color color, in Color other)
        {
            return new Color(color.r + other.r, color.g + other.g, color.b + other.b, color.a);
        }

        #endregion
        /***********************************************************************
        *                               Multiply
        ***********************************************************************/
        #region .
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyR(in this Color color, float r)
        {
            return new Color(color.r * r, color.g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyG(in this Color color, float g)
        {
            return new Color(color.r, color.g * g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyB(in this Color color, float b)
        {
            return new Color(color.r, color.g, color.b * b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyA(in this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, color.a * a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyRG(in this Color color, float rg)
        {
            return new Color(color.r * rg, color.g * rg, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyGB(in this Color color, float gb)
        {
            return new Color(color.r, color.g * gb, color.b * gb, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyRB(in this Color color, float rb)
        {
            return new Color(color.r * rb, color.g, color.b * rb, color.a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyRGB(in this Color color, float rgb)
        {
            return new Color(color.r * rgb, color.g * rgb, color.b * rgb, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyRGB(in this Color color, float r, float g, float b)
        {
            return new Color(color.r * r, color.g * g, color.b * b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_MultiplyRGB(in this Color color, in Color other)
        {
            return new Color(color.r * other.r, color.g * other.g, color.b * other.b, color.a);
        }

        #endregion
        /***********************************************************************
        *                               Set
        ***********************************************************************/
        #region .
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetR(in this Color color, float r)
        {
            return new Color(r, color.g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetG(in this Color color, float g)
        {
            return new Color(color.r, g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetB(in this Color color, float b)
        {
            return new Color(color.r, color.g, b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetA(in this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetRG(in this Color color, float r, float g)
        {
            return new Color(r, g, color.b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetGB(in this Color color, float g, float b)
        {
            return new Color(color.r, g, b, color.a);
        }
        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetRB(in this Color color, float r, float b)
        {
            return new Color(r, color.g, b, color.a);
        }

        [TestCompleted(2021, 06, 15)]
        public static Color Ex_SetRGB(in this Color color, float r, float g, float b)
        {
            return new Color(r, g, b, color.a);
        }

        #endregion

        /***********************************************************************
        *                               Migrated
        ***********************************************************************/
        #region
        /// <summary>
        /// <para/> EColor -> Color 변환
        /// </summary>
        public static Color Ex_Convert(this EColor eColor)
            => EColorConverter.Convert(eColor);

        /// <summary>
        /// <para/> Alpha 값 변경 후 리턴
        /// </summary>
        public static Color Ex_SetAlpha(this Color color, float alpha)
        {
            if (alpha > 1) alpha = 1f;
            else if (alpha < 0) alpha = 0f;

            return new Color(color.r, color.g, color.b, alpha);
        }

        /// <summary>
        /// <para/> RGB값 더한 후 리턴
        /// </summary>
        public static Color Ex_Plus(this Color color, float r, float g, float b)
        {
            return new Color(color.r + r, color.g + g, color.b + b);
        }

        /// <summary>
        /// <para/> RGBA값 더한 후 리턴
        /// </summary>
        public static Color Ex_Plus(this Color color, float r, float g, float b, float a)
        {
            return new Color(color.r + r, color.g + g, color.b + b, color.a + a);
        }

        /// <summary>
        /// <para/> RGB값 더한 후 리턴
        /// </summary>
        public static Color Ex_PlusRGB(this Color color, float rgb)
        {
            return new Color(color.r + rgb, color.g + rgb, color.b + rgb, color.a);
        }

        /// <summary>
        /// <para/> RGBA값 더한 후 리턴
        /// </summary>
        public static Color Ex_PlusRGBA(this Color color, float rgba)
        {
            return new Color(color.r + rgba, color.g + rgba, color.b + rgba, color.a + rgba);
        }
        #endregion
    }
}