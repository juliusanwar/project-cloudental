using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CloudClinic.Helpers
{
    public static class IdHtmlHelper
    {

        public static String GetIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {

            return TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));

        }

    }
}