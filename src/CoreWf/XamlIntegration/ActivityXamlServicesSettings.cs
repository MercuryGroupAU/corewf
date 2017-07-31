// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CoreWf.XamlIntegration
{
    using System;

    public class ActivityXamlServicesSettings
    {
        public bool CompileExpressions
        {
            get;
            set;
        }

        public LocationReferenceEnvironment LocationReferenceEnvironment
        {
            get;
            set;
        }
    }
}
