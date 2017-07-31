using System;
using System.IO;
using System.Reflection;
using Portable.Xaml;
using System.Linq;
using CoreWf;
using CoreWf.XamlIntegration;
using System.Xml;
using System.Collections.Generic;

namespace XAMLConsoleApp
{
    class Program
    {
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        static void Main(string[] args)
        {
            ActivityXamlServicesSettings settings = new CoreWf.XamlIntegration.ActivityXamlServicesSettings { CompileExpressions = false };
            try
            {
                string ActivityAlone = @"
<Activity x:Class=""WFTemplate"" 
xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" 
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">   
</Activity>";
                var act = CoreWf.XamlIntegration.ActivityXamlServices.Load(GenerateStreamFromString(ActivityAlone), settings);
                WorkflowInvoker.Invoke(act);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string ActivityWriteLine = @"
<Activity 
x:Class=""WFTemplate"" 
xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" 
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">  
<WriteLine Text=""HelloWorld"" /> 
</Activity>";
                var act = CoreWf.XamlIntegration.ActivityXamlServices.Load(GenerateStreamFromString(ActivityWriteLine), settings);
                WorkflowInvoker.Invoke(act);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string InOutActivityOnly = @"
<Activity 
x:Class=""WFTemplate""  
xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities""  
xmlns:s=""clr-namespace:System;assembly=mscorlib""  
xmlns:s1=""clr-namespace:System;assembly=System""  
xmlns:sa=""clr-namespace:CoreWf;assembly=CoreWf""  
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">   
<x:Members>     
<x:Property Name=""myOutput"" Type=""OutArgument(x:Int32)"" />     
<x:Property Name=""myInput"" Type=""InArgument(x:Int32)"" />   
</x:Members> </Activity>";
                var act = CoreWf.XamlIntegration.ActivityXamlServices.Load(GenerateStreamFromString(InOutActivityOnly), settings);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("myInput", 1);
                Dictionary<string, object> outputDict = WorkflowInvoker.Invoke(act, dic) as Dictionary<string, object>;
                foreach (var kvp in outputDict.ToList())
                {
                    Console.WriteLine(kvp.Key.ToString() + " " + kvp.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string InOutActivity = @"
<Activity x:Class=""WFTemplate""  
xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities""  
xmlns:s=""clr-namespace:System;assembly=mscorlib""  
xmlns:s1=""clr-namespace:System;assembly=System""  
xmlns:sa=""clr-namespace:CoreWf;assembly=CoreWf""  
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">   
<x:Members>     
<x:Property Name=""myOutput"" Type=""OutArgument(x:Int32)"" />     
<x:Property Name=""myInput"" Type=""InArgument(x:Int32)"" />   
</x:Members>   
<Assign>     
<Assign.To> <OutArgument x:TypeArguments=""x:Int32"">[myOutput]</OutArgument> </Assign.To>     
<Assign.Value>       <InArgument x:TypeArguments=""x:Int32"">[myInput]</InArgument>     </Assign.Value>   
</Assign> 
</Activity>";
                var act = CoreWf.XamlIntegration.ActivityXamlServices.Load(GenerateStreamFromString(InOutActivity), settings);
                var outputDict = WorkflowInvoker.Invoke(act) as Dictionary<string, object>;
                foreach (var kvp in outputDict)
                {
                    Console.WriteLine(kvp.Key.ToString() + " " + kvp.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string XamlText = @"
<Activity x:Class=""WFTemplate"" 
xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" 
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">   
<Sequence>     
<WriteLine Text=""HelloWorld"" />   </Sequence> </Activity>";
                var act = CoreWf.XamlIntegration.ActivityXamlServices.Load(GenerateStreamFromString(XamlText), settings);
                WorkflowInvoker.Invoke(act);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}