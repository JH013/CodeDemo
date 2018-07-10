using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Monitor
{
    public class ComputerInfo
    {
        public static List<Dictionary<string, string>> GetWin32ProcessorInfos(ProcessorInfoEnums.ProcessorInfoEnum eEnum = ProcessorInfoEnums.ProcessorInfoEnum.All, bool isPrint = true)
        {
            var _LocalManagementScope = new ManagementScope($"\\\\{Dns.GetHostName()}\\root\\cimv2");
            var result = new List<Dictionary<string, string>>();
            try
            {
                _LocalManagementScope.Connect();
                foreach (var managementBaseObject in new ManagementObjectSearcher(_LocalManagementScope,
                    new SelectQuery($"SELECT {ProcessorInfoEnums.GetQueryString(eEnum)} FROM Win32_Processor")).Get())
                {
                    if (isPrint)
                        Console.WriteLine($@"<=========={managementBaseObject}===========>");
                    var thisObjectsDictionary = new Dictionary<string, string>();
                    foreach (var property in managementBaseObject.Properties)
                    {
                        if (isPrint)
                            Console.WriteLine(
                                $@"[{property.Name,40}] -> [{property.Value,-40}]//{
                                        ProcessorInfoEnums.GetDescriptionString(property.Name)
                                    }.");
                        thisObjectsDictionary.Add(property.Name, property.Value.ToString());
                    }
                    result.Add(thisObjectsDictionary);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return result;
        }

        public static double GetProcessCpuProcessorRatio(Process process)
        {
            if (!process.HasExited)
            {
                var processorTime = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                processorTime.NextValue();
                return processorTime.NextValue() / Environment.ProcessorCount;
            }
            return 0;
        }
    }
}
