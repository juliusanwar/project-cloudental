using Omu.AwesomeMvc;

namespace CloudClinic.Helpers
{
    public static class AngularExtensions
    {
        public static AjaxRadioList<T> NgModel<T>(this AjaxRadioList<T> h, string scopeProperty)
        {
            // angular can't handle hidden type inputs, using text input with display:none instead
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }

        public static AjaxDropdown<T> NgModel<T>(this AjaxDropdown<T> h, string scopeProperty)
        {
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }

        public static AjaxCheckboxList<T> NgModel<T>(this AjaxCheckboxList<T> h, string scopeProperty)
        {
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }
    }
}