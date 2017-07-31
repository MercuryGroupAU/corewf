// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using Portable.Xaml.Markup;
    using System;
    using System.ComponentModel;
    using Portable.Xaml;

    public sealed class ActivityWithResultValueSerializer : ValueSerializer
    {
        static ActivityWithResultValueSerializer valueSerializer;

        public override bool CanConvertToString(object value, IValueSerializerContext context)
        {
            if (AttachablePropertyServices.GetAttachedPropertyCount(value) > 0)
            {
                return false;
            }
            else if (value != null && 
                value is IValueSerializableExpression && 
                ((IValueSerializableExpression)value).CanConvertToString(context))
            {
                return true;
            }

            return false;
        }

        public override string ConvertToString(object value, IValueSerializerContext context)
        {
            IValueSerializableExpression ivsExpr;

            ivsExpr = value as IValueSerializableExpression;
            if (ivsExpr == null)
            {
                throw CoreWf.Internals.FxTrace.Exception.AsError(new InvalidOperationException(SR.CannotSerializeExpression(value.GetType())));
            }
            return ivsExpr.ConvertToString(context);
        }

        internal static bool CanConvertToStringWrapper(object value, IValueSerializerContext context)
        {
            if (valueSerializer == null)
            {
                valueSerializer = new ActivityWithResultValueSerializer();
            }

            return valueSerializer.CanConvertToString(value, context);
        }

        internal static string ConvertToStringWrapper(object value, IValueSerializerContext context)
        {
            if (valueSerializer == null)
            {
                valueSerializer = new ActivityWithResultValueSerializer();
            }

            return valueSerializer.ConvertToString(value, context);
        }
    }
}
