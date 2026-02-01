using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core.Jobs;

namespace FlowEngine.Infrastructure.Worker.Seeds
{
    public class DefaultData
    {
        internal static (Project Project, Dictionary<string, string> NextJobs) GetTestTemplate()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","HttpRequest" },
                { "HttpRequest","Random" },
                { "Random","Sleep" },
                { "Sleep","HttpRequest2" },
                { "HttpRequest2","Random" },
            };

            return (new Project("Test")
            {
                ProjectJobs = [
                    new Job_Start()
                    {
                        Name="Start",
                    },

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest",
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_Random()
                    {
                        Name="Random",
                    }.UpdateJobParameter(FlowEngineConst.From,"100")
                     .UpdateJobParameter(FlowEngineConst.To,"1000"),

                    new Job_Sleep()
                    {
                        Name="Sleep",
                    }.UpdateJobParameter(FlowEngineConst.SleepTimeMs,"${Random}"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson?parameter=${HttpRequest.Data.data.number}"),

                    ]
            }, nextJobs);
        }
        internal static (Project Project, Dictionary<string, string> NextJobs) GetTestTemplate2()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","HttpRequest1" },
                { "HttpRequest1","HttpRequest2" },
            };


            return (new Project("Test2")
            {
                ProjectJobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"10000"),

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
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${HttpRequest1.Data.data.number}}"),
                ]
            },nextJobs);
        }
        //internal static Project GetTestTemplate3()
        //{
        //    var nextJobs = new Dictionary<string, string[]>
        //    {
        //        { "Start",["Random"] },
        //        { "Random",["If"] },
        //        //{ "Random",["If"] },
        //    };

        //    return new Project("Test3")
        //    {
        //        ProjectJobs = [
        //            new Job_Timer()
        //            {
        //                Name="Start",
        //            }.UpdateJobParameter(FlowEngineConst.IntervalMs,"5000"),

        //            new Job_Random()
        //            {
        //                Name="Random",

        //            }.UpdateJobParameter(FlowEngineConst.From,"0")
        //             .UpdateJobParameter(FlowEngineConst.To,"1000"),

        //            new Job_HttpRequest()
        //            {
        //                Name="HttpRequest1",
        //            }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

        //            new Job_HttpRequest()
        //            {
        //                Name="HttpRequest2",
        //            }
        //            .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJson")
        //            .UpdateJobParameter(FlowEngineConst.Method,"Post")
        //            .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${Random}}"),
        //        ]
        //    };
        //}
        internal static (Project Project, Dictionary<string, string> NextJobs) GetTestTemplate4()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","HttpRequest_Authenticate" },
                { "HttpRequest_Authenticate","HttpRequest_PostJsonRequireAuthorization" },
            };

            return (new Project("Test4")
            {
                ProjectJobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                    }
                    .UpdateJobParameter(FlowEngineConst.IntervalMs,"5000")
                    .UpdateJobParameter(FlowEngineConst.EnvironmentVariables,"{\r\n    \"User\": \"Test\",\r\n    \"Pass\":\"Test@12345\"\r\n}"),


                    new Job_HttpRequest()
                    {
                        Name="HttpRequest_Authenticate",
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Account/Authenticate")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\r\n  \"userName\": \"${EnvironmentVariables.User}\",\r\n  \"password\": \"${EnvironmentVariables.Pass}\"\r\n}"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest_PostJsonRequireAuthorization",
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJsonRequireAuthorization")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": 123456,\"Date\":\"${DateTime.Now}\"}")
                    .UpdateJobParameter(FlowEngineConst.Headers,"Authorization:bearer ${HttpRequest_Authenticate.Data.data.jwToken}"),
                ]
            },nextJobs);
        }
    }
}
