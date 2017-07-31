// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;
    using System.ComponentModel;
    using Portable.Xaml.Markup;

    public sealed class InOutArgumentConverter : TypeConverterBase
    {
        public InOutArgumentConverter()
            : base(typeof(InOutArgument<>), typeof(InOutArgumentConverterHelper<>))
        {
        }

        public InOutArgumentConverter(Type type)
            : base(type, typeof(InOutArgument<>), typeof(InOutArgumentConverterHelper<>))
        {
        }

        internal sealed class InOutArgumentConverterHelper<T> : TypeConverterHelper<InOutArgument<T>>
        {
            ActivityWithResultConverter.ExpressionConverterHelper<Location<T>> expressionHelper;

            public InOutArgumentConverterHelper()
            {
                this.expressionHelper = new ActivityWithResultConverter.ExpressionConverterHelper<Location<T>>(true);
            }

            public override InOutArgument<T> ConvertFromString(string text, ITypeDescriptorContext context)
            {
                return new InOutArgument<T>
                    {
                        Expression = this.expressionHelper.ConvertFromString(text.Trim(), context)
                    };
            }
        }
    }
}
