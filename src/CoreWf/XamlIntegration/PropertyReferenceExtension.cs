// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;
    using System.ComponentModel;
    using System.Runtime;
    using Portable.Xaml.Markup;
    using System.Xml.Linq;
    using Portable.Xaml;
    using System.Reflection;

    [MarkupExtensionReturnType(typeof(object))]
    public sealed class PropertyReferenceExtension<T> : MarkupExtension
    {
        public PropertyReferenceExtension()
            : base()
        {
        }

        public string PropertyName
        {
            get;
            set;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (!string.IsNullOrEmpty(this.PropertyName))
            {
                object targetObject = ActivityWithResultConverter.GetRootTemplatedActivity(serviceProvider);
                if (targetObject != null)
                {
                    PropertyDescriptor property = TypeDescriptor.GetProperties(targetObject)[PropertyName];

                    if (property != null)
                    {
                        return property.GetValue(targetObject);
                    }
                }
            }

            throw CoreWf.Internals.FxTrace.Exception.AsError(
                new InvalidOperationException(SR.PropertyReferenceNotFound(this.PropertyName)));
        }
    }
}
