using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core.Jobs;

namespace FlowEngine.Infrastructure.Worker.Seeds
{
    public class DefaultData
    {
        internal static Project GetTestTemplate()
        {
            return new Project("Test")
            {
                Jobs = [
                    new Job_Start()
                    {
                        Name="Start",
                        NextJob=["HttpRequest"],
                    },
                    new Job_HttpRequest()
                    {
                        Name="HttpRequest",
                        NextJob=["Random"],
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://www.arvancloud.ir/api/v1/notice-banner"),
                    new Job_Random()
                    {
                        Name="Random",
                        NextJob=["Sleep"],

                    }.UpdateJobParameter(FlowEngineConst.From,"100")
                     .UpdateJobParameter(FlowEngineConst.To,"1000"),

                    new Job_Sleep()
                    {
                        Name="Sleep",
                        NextJob=["HttpRequest2"],
                    }.UpdateJobParameter(FlowEngineConst.SleepTimeMs,"${Random}"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                        NextJob=["Random"]
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://www.arvancloud.ir/api/v1/notice-banner?z=${HttpRequest.Data.data}"),

                    ]
            };
        }
        internal static Project GetTestTemplate2()
        {
            return new Project("Test2")
            {
                Jobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                        NextJob=["HttpRequest"],
                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"10000"),
                    new Job_HttpRequest()
                    {
                        Name="HttpRequest",

                    }.UpdateJobParameter(FlowEngineConst.Url,"https://www.arvancloud.ir/api/v1/notice-banner"),
                ]
            };
        }
    }
}
