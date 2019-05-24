using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace CodorniX.Util
{
    public static class HtmlGenericControlExtensions
    {
        /// <summary>
        /// Add a CSS class to control without rewrite all Attributes["class"] property.
        /// </summary>
        /// <param name="control">Current <see cref="HtmlGenericControl"/></param>
        /// <param name="cssClass">A CSS class</param>
        /// <returns>The same <see cref="HtmlGenericControl"/></returns>
        public static HtmlGenericControl  AddCssClass(this HtmlGenericControl control, string cssClass)
        {
            if (control.Attributes["class"] == null || !control.Attributes["class"].Contains(cssClass))
                control.Attributes["class"] += " " + cssClass;

            control.Attributes["class"] = control.Attributes["class"].Trim();
            return control;
        }

        /// <summary>
        /// Remove a CSS class to control without rewrite all Attributes["class"] property.
        /// </summary>
        /// <param name="control">Current <see cref="HtmlGenericControl"/></param>
        /// <param name="cssClass">A CSS class</param>
        /// <returns>The same <see cref="HtmlGenericControl"/></returns>
        public static HtmlGenericControl RemoveCssClass(this HtmlGenericControl control, string cssClass)
        {
            if (control.Attributes["class"] != null)
                control.Attributes["class"] = control.Attributes["class"].Replace(cssClass, "").Trim();
            return control;
        }
    }
}