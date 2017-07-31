// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;
    using Portable.Xaml.Markup;
    
    public interface IValueSerializableExpression
    {
        bool CanConvertToString(IValueSerializerContext context);
        string ConvertToString(IValueSerializerContext context);
    }
}
