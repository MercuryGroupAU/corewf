// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;
    using Portable.Xaml.Markup;
    using CoreWf;

    public class ArgumentValueSerializer : ValueSerializer
    {        
        public override bool CanConvertToString(object value, IValueSerializerContext context)
        {
            Argument argument = value as Argument;
            if (argument == null)
            {
                return false;
            }
            if (ActivityBuilder.HasPropertyReferences(value))
            {
                // won't be able to attach the property references if we convert to string
                return false;
            }

            return argument.CanConvertToString(context);
        }

        public override string ConvertToString(object value, IValueSerializerContext context)
        {
            Argument argument = value as Argument;
            if (argument == null)
            {
                // expect CanConvertToString() always comes before ConvertToString()
                throw CoreWf.Internals.FxTrace.Exception.Argument("value", SR.CannotSerializeExpression(value.GetType()));                   
            }

            return argument.ConvertToString(context);
        }
    }
}
