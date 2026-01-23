using FlowEngine.Domain.Projects.Enums;
using System.Collections.Generic;

namespace FlowEngine.Domain.Projects.ValueObjects
{
    public class ProjectJob
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public Dictionary<string, JobParameter> JobParameters { get; set; }
        public List<string> NextJob { get; set; }
    }

    public class JobParameter
    {
        public JobParameter()
        {

        }
        public JobParameter(JobParameterType parameterType, string? value = null)
        {
            this.ParameterType = parameterType;
            this.Value = value;
        }
        public JobParameterType ParameterType { get; set; }
        public string? Value { get; set; }
    }
}
