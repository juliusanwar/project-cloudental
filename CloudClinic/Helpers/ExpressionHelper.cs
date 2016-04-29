using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CloudClinic.Helpers
{
    // Summary:

    //     Provides a helper class to get the model name from an expression.

    public static class ExpressionHelper
    {

        // Summary:

        //     Gets the model name from a lambda expression.

        //

        // Parameters:

        //   expression:

        //     The expression.

        //

        // Returns:

        //     The model name.

        public static string GetExpressionText(LambdaExpression expression);

        //

        // Summary:

        //     Gets the model name from a string expression.

        //

        // Parameters:

        //   expression:

        //     The expression.

        //

        // Returns:

        //     The model name.

        public static string GetExpressionText(string expression);

    }
}