using FlowEngine.Domain.Projects.Constants;
using FlowEngine.Domain.Projects.Entities;
using FlowEngine.Infrastructure.Worker.Core.Jobs;
using System.Net;
using System.Text.Json;

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

            var project = new Project("Test")
            {
                ProjectJobs = [
                    new Job_Start()
                    {
                        Name="Start",
                        Position= new System.Drawing.Point(100,50)
                    },

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest",
                        Position= new System.Drawing.Point(100,170)
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_Random()
                    {
                        Name="Random",
                        Position= new System.Drawing.Point(440,320)
                    }.UpdateJobParameter(FlowEngineConst.From,"100")
                     .UpdateJobParameter(FlowEngineConst.To,"1000"),

                    new Job_Sleep()
                    {
                        Name="Sleep",
                        Position= new System.Drawing.Point(90,390)

                    }.UpdateJobParameter(FlowEngineConst.SleepTimeMs,"${Random}"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                        Position= new System.Drawing.Point(290,560)
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson?parameter=${HttpRequest.Data.data.number}"),

                    ]
            };

            return (project, nextJobs);

        }
        internal static (Project Project, Dictionary<string, string> NextJobs) GetTestTemplate2()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","HttpRequest1" },
                { "HttpRequest1","HttpRequest2" },
            };


            var project = new Project("Test2")
            {
                ProjectJobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                        Position= new System.Drawing.Point(100,50)

                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"10000"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest1",
                        Position= new System.Drawing.Point(100,150)

                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                        Position= new System.Drawing.Point(100,250)
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJson")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${HttpRequest1.Data.data.number}}"),
                ]
            };

            return (project, nextJobs);

        }
        internal static (Project Project, Dictionary<string, string> NextJobs, List<Tuple<string, string, string>> paramsNextJobs) GetTestTemplate3()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","Random" },
                { "Random","If" },
            };
            var paramsNextJobs = new List<Tuple<string, string, string>>
            {
               new ( "If",FlowEngineConst.IfTrue,"HttpRequest1" ),
                new( "If",FlowEngineConst.IfFalse,"HttpRequest2" ),
            };

            var project = new Project("Test3")
            {
                ProjectJobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                        Position= new System.Drawing.Point(170,50)
                    }.UpdateJobParameter(FlowEngineConst.IntervalMs,"5000"),

                    new Job_Random()
                    {
                        Name="Random",
                        Position= new System.Drawing.Point(170,150)
                    }.UpdateJobParameter(FlowEngineConst.From,"0")
                     .UpdateJobParameter(FlowEngineConst.To,"1000"),

                    new Job_If()
                    {
                        Name="If",
                        Position= new System.Drawing.Point(170,250)
                    }.UpdateJobParameter(FlowEngineConst.Expression,"${Random} >= 500"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest1",
                        Position= new System.Drawing.Point(300,350)
                    }.UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/GetJson"),

                    new Job_HttpRequest()
                    {
                        Name="HttpRequest2",
                        Position= new System.Drawing.Point(50,350)
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJson")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": ${Random}}"),
                ]
            };

            return (project, nextJobs, paramsNextJobs);
        }

        internal static (Project Project, Dictionary<string, string> NextJobs) GetTestTemplate4()
        {
            var nextJobs = new Dictionary<string, string>
            {
                { "Start","Job_HttpRequest_1" },
                { "Job_HttpRequest_1","Job_HttpRequest_2" },
            };

            var project = new Project("Test4")
            {
                ProjectJobs = [
                    new Job_Timer()
                    {
                        Name="Start",
                        Position= new System.Drawing.Point(100,50)
                    }
                    .UpdateJobParameter(FlowEngineConst.IntervalMs,"5000")
                    .UpdateJobParameter(FlowEngineConst.EnvironmentVariables,"{\r\n    \"User\": \"Test\",\r\n    \"Pass\":\"Test@12345\"\r\n}"),


                    new Job_HttpRequest()
                    {
                        Name="Job_HttpRequest_1",
                        Position= new System.Drawing.Point(100,150)
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Account/Authenticate")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\r\n  \"userName\": \"${EnvironmentVariables.User}\",\r\n  \"password\": \"${EnvironmentVariables.Pass}\"\r\n}"),

                    new Job_HttpRequest()
                    {
                        Name="Job_HttpRequest_2",
                        Position= new System.Drawing.Point(100,250)
                    }
                    .UpdateJobParameter(FlowEngineConst.Url,"https://localhost:5001/api/Test/PostJsonRequireAuthorization")
                    .UpdateJobParameter(FlowEngineConst.Method,"Post")
                    .UpdateJobParameter(FlowEngineConst.Body,"{\"Number\": 123456,\"Date\":\"${DateTime.Now}\"}")
                    .UpdateJobParameter(FlowEngineConst.Headers, JsonSerializer.Serialize(new {Authorization="bearer ${Job_HttpRequest_1.Data.data.jwToken}"})),
                ]
            };

            return (project, nextJobs);

        }
    }
}
