// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;
    using System.ComponentModel;
    using Portable.Xaml.Markup;

    public sealed class OutArgumentConverter : TypeConverterBase
    {
        public OutArgumentConverter()
            : base(typeof(OutArgument<>), typeof(OutArgumentConverterHelper<>))
        {
        }

        public OutArgumentConverter(Type type)
            : base(type, typeof(OutArgument<>), typeof(OutArgumentConverterHelper<>))
        {
        }

        internal sealed class OutArgumentConverterHelper<T> : TypeConverterHelper<OutArgument<T>>
        {
            ActivityWithResultConverter.ExpressionConverterHelper<Location<T>> expressionHelper;

            public OutArgumentConverterHelper()
            {
                this.expressionHelper = new ActivityWithResultConverter.ExpressionConverterHelper<Location<T>>(true);
            }

            public override OutArgument<T> ConvertFromString(string text, ITypeDescriptorContext context)
            {
                return new OutArgument<T>
                    {
                        Expression = this.expressionHelper.ConvertFromString(text.Trim(), context)
                    };
            }
        }
    }
}
