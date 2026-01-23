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
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),
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
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson?parameter=${HttpRequest.Data.data.number}"),

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
                        NextJob=["HttpRequest1"],
                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"10000"),
                    new Job_HttpRequest()
                    {
                        Name="HttpRequest1",
                        NextJob=["HttpRequest2"],
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",

                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJson")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${HttpRequest1.Data.data.number}}"),
                ]
            };
        }
        internal static Project GetTestTemplate3()
        {
            return new Project("Test3")
            {
                Jobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                        NextJob=["Random"],
                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"5000"),
                    
                    new Job_Random()
                    {
                        Name="Random",
                        NextJob=["If"],

                    }.UpdateJobParameter(FlowEngineConst.From,"0")
                     .UpdateJobParameter(FlowEngineConst.To,"1000"),

                    new Job_If()
                    {
                        Name="If",
                    }.UpdateJobParameter(FlowEngineConst.Expression,"${Random} >= 500")
                     .UpdateJobParameter(FlowEngineConst.True,"HttpRequest1")
                     .UpdateJobParameter(FlowEngineConst.False,"HttpRequest2"),


                    new Job_HttpRequest()
                    {
                        Name="HttpRequest1",
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJson")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${Random}}"),
                ]
            };
        }
    }
}
