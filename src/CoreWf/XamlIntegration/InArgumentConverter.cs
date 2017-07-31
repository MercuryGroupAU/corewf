// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using CoreWf.XamlIntegration;
    using System;
    using System.ComponentModel;
    using Portable.Xaml.Markup;

    public sealed class InArgumentConverter : TypeConverterBase
    {
        public InArgumentConverter()
            : base(typeof(InArgument<>), typeof(InArgumentConverterHelper<>))
        {
        }

        public InArgumentConverter(Type type)
            : base(type, typeof(InArgument<>), typeof(InArgumentConverterHelper<>))
        {
        }

        internal sealed class InArgumentConverterHelper<T> : TypeConverterHelper<InArgument<T>>
        {
            ActivityWithResultConverter.ExpressionConverterHelper<T> valueExpressionHelper;

            public InArgumentConverterHelper()
            {
                this.valueExpressionHelper = new ActivityWithResultConverter.ExpressionConverterHelper<T>(false);
            }

            public override InArgument<T> ConvertFromString(string text, ITypeDescriptorContext context)
            {
                return new InArgument<T>
                    {
                        Expression = this.valueExpressionHelper.ConvertFromString(text, context)
                    };
            }
        }
    }
}
