﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Test.Common.TestObjects.Utilities.Validation
{
    [DataContract]
    public class SynchronizeTrace : WorkflowTraceStep, IActualTraceStep
    {
        internal UserTrace userTrace;
        public SynchronizeTrace(string message)
        {
            this.userTrace = new UserTrace(message);
        }

        internal SynchronizeTrace(Guid instanceId, string message)
        {
            this.userTrace = new UserTrace(instanceId, message);
        }

        DateTime IActualTraceStep.TimeStamp
        {
            get { return ((IActualTraceStep)this.userTrace).TimeStamp; }
            set { ((IActualTraceStep)this.userTrace).TimeStamp = value; }
        }

        int IActualTraceStep.Validated
        {
            get { return ((IActualTraceStep)this.userTrace).Validated; }
            set { ((IActualTraceStep)this.userTrace).Validated = value; }
        }

        public override string ToString()
        {
            return ((IActualTraceStep)this.userTrace).GetStringId();
        }

        public override bool Equals(object obj)
        {
            SynchronizeTrace trace = obj as SynchronizeTrace;
            if (trace != null)
            {
                if (this.ToString() == trace.ToString())
                {
                    return true;
                }
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IActualTraceStep implementation

        bool IActualTraceStep.Equals(IActualTraceStep trace)
        {
            SynchronizeTrace synchronizeTrace = trace as SynchronizeTrace;

            if (synchronizeTrace != null &&
                synchronizeTrace.userTrace.Message == this.userTrace.Message)
            {
                return true;
            }

            return false;
        }

        string IActualTraceStep.GetStringId()
        {
            return ((IActualTraceStep)this.userTrace).GetStringId();
        }
        #endregion

        #region SynchronizeTrace helpers
        public static void Trace(Guid instanceId, string format, params object[] args)
        {
            SynchronizeTrace.Trace(instanceId, String.Format(format, args));
        }

        public static void Trace(Guid instanceId, string message)
        {
            SynchronizeTrace synchronizeTrace = new SynchronizeTrace(instanceId, message);
            TraceSource ts = new TraceSource("CoreWf.Tracking", SourceLevels.Information);
            ts.TraceData(TraceEventType.Information, 1, synchronizeTrace);
        }
        #endregion
    }
}
