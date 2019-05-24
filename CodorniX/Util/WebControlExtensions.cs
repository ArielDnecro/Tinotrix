using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CodorniX.Util
{
    /// <summary>
    /// Add useful extensions to WebControls
    /// </summary>
    public static class WebControlExtensions
    {
        /// <summary>
        /// Add a CSS class to control without rewrite all CssClass property.
        /// </summary>
        /// <param name="control">Current <see cref="WebControl"/></param>
        /// <param name="cssClass">A CSS class</param>
        /// <returns>The same <see cref="WebControl"/></returns>
        public static WebControl AddCssClass(this WebControl control, string cssClass)
        {
            if (!control.CssClass.Contains(cssClass))
                control.CssClass += " " + cssClass;

            control.CssClass = control.CssClass.Trim();
            return control;
        }

        /// <summary>
        /// Remove a CSS class to control without rewrite all CssClass property.
        /// </summary>
        /// <param name="control">Current <see cref="WebControl"/></param>
        /// <param name="cssClass">A CSS class</param>
        /// <returns>The same <see cref="WebControl"/></returns>
        public static WebControl RemoveCssClass(this WebControl control, string cssClass)
        {
            control.CssClass = control.CssClass.Replace(cssClass, "").Trim();
            return control;
        }

        /// <summary>
        /// Enable a <see cref="WebControl"/> in server-side and change appearance in client-side.
        /// </summary>
        /// <param name="control">Current <see cref="WebControl"/></param>
        /// <param name="cssClass">CSS class that define disabled state in client view. By default is "disabled" class.</param>
        /// <returns>The Same <see cref="WebControl"/></returns>
        public static WebControl Enable(this WebControl control, string cssClass = "disabled")
        {
            control.RemoveCssClass(cssClass);
            control.Enabled = true;
            return control;
        }

        /// <summary>
        /// Disable a <see cref="WebControl"/> in server-side and change appearance in client-side.
        /// </summary>
        /// <param name="control">Current <see cref="WebControl"/></param>
        /// <param name="cssClass">CSS class that define disabled state in client view. By default is "disabled" class.</param>
        /// <returns>The Same <see cref="WebControl"/></returns>
        public static WebControl Disable(this WebControl control, string cssClass = "disabled")
        {
            control.AddCssClass(cssClass);
            control.Enabled = false;
            return control;
        }
    }
}