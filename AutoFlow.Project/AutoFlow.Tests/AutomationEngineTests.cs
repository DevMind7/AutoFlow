using AutoFlow.AutomationEngine;
using AutoFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFlow.Tests
{
    public class AutomationEngineTests
    {
        //[Fact]
        //public async Task WorkflowEngine_ShouldExecuteAllStepsSuccessfully()
        //{
        //    var workflow = new Workflow
        //    {
        //        Name = "TestFlow",
        //        Steps = new()
        //    {
        //        new WorkFlowStep
        //        {
        //            Name = "Email",
        //            Module = "Email",
        //            Parameters = new()
        //            {
        //                { "to", "test@example.com" },
        //                { "subject", "Hello" }
        //            }
        //        },
        //        new WorkFlowStep
        //        {
        //            Name = "Transform",
        //            Module = "Transform",
        //            Parameters = new()
        //            {
        //                { "input", "hello world" }
        //            }
        //        }
        //    }
        //    };

        //    var engine = new WorkflowEngine();
        //    var results = await engine.ExecuteAsync(workflow);

        //    Assert.All(results, r => Assert.True(r.Success));
        //    Assert.Contains(results[1].Message, m => m.Contains("HELLO WORLD"));
        //}
    }
}
