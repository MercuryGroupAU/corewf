using CoreWf;
using CoreWf.Statements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Samples
{
    /// <summary>
    /// Sample DynamicActivity based on https://docs.microsoft.com/en-us/dotnet/framework/windows-workflow-foundation/creating-an-activity-at-runtime-with-dynamicactivity
    /// </summary>
    public class DynamicDemo
    {
        [Fact]
        public void RunDynamicWriteLine()
        {
            //Define the input argument for the activity  
            var textOut = new InArgument<string>();
            //Create the activity, property, and implementation  
            Activity dynamicWorkflow = new DynamicActivity()
            { 
                Properties =
                {
                    new DynamicActivityProperty
                    {
                        Name = "Text",
                        Type = typeof(InArgument<String>),
                        Value = textOut
                    }
                },
                Implementation = () => new Sequence()
                {
                    Activities =
                    {
                        new WriteLine()
                        {
                            Text = new InArgument<string>(env => textOut.Get(env))
                        }
                    }
                }
            };
            //Execute the activity with a parameter dictionary  
            WorkflowInvoker.Invoke(dynamicWorkflow, new Dictionary<string, object> { { "Text", "Hello World!" } });
        }
    }

    
}
