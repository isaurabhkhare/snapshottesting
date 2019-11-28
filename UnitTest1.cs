using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Snapper;
using Snapper.Attributes;
using System;
using System.Collections.Generic;
using Xunit;

namespace SnapshotTests
{
    [Custom]
    [TestFixture]
    public class Tests
    {
        [Custom]
        [UpdateSnapshots(true)]
        [Test]
        public void Test1()
        {

            var obj = new
            {
                Key = "123"
            };

            obj.ShouldMatchSnapshot();
            string schemafile = System.IO.File.ReadAllText("Schema.json");
            string jsonFile = System.IO.File.ReadAllText("data.json");
            var schema = JSchema.Parse(schemafile);
            var json = JObject.Parse(jsonFile);
            IList<string> messages;
            bool t = json.IsValid(schema, out messages);
            if (t)
            {
                Console.WriteLine(messages);

            }
            else
            {

                Console.WriteLine(messages);
            }
           
        }


        // [UpdateSnapshots]
        [Custom]
        [Test,TestCaseSource("SavingsTests")]
        public void Test2(object str)
        {
          
            var obj = new
            {
                Key = "value"
            };

            obj.ShouldMatchSnapshot();
            string schemafile = System.IO.File.ReadAllText("Schema.json");
            string jsonFile = System.IO.File.ReadAllText("data.json");
            var schema = JSchema.Parse(schemafile);
            var json = JObject.Parse(jsonFile);
            IList<string> messages;
            bool t = json.IsValid(schema, out messages);
            if (t)
            {
                Console.WriteLine(messages);

            }
            else
            {

                Console.WriteLine(messages);
            }

        }

        public static IEnumerable<TestCaseData> SavingsTests()
        {
            string schemafile = System.IO.File.ReadAllText("testdata.json");
            var data = JsonConvert.DeserializeObject(schemafile);
            var job = JObject.Parse(data.ToString());
            var bro = new List<object>();
            bro.Add(data);
            yield return new TestCaseData(bro).SetName("TestCase");
        }



    }






[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAttribute : CategoryAttribute
    {

    }

    [Custom]
    [TestFixture]
    public class SomeTests
    {

    }





    public class IgnoreIfConfigAttribute : Attribute, ITestAction
    {
        public IgnoreIfConfigAttribute(string config)
        {
            if (config == "UpdateSnapshot")
            _config = config;
        }
        public void BeforeTest(ITest test)
        {
            if (_config != "Enabled") NUnit.Framework.Assert.Ignore("Test is Ignored due to Access level");
        }
        public void AfterTest(ITest test)
        {

        }
        public ActionTargets Targets { get; private set; }
        public string _config { get; set; }
    }
}