using System;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace GildedRoseTests;

[UseReporter(typeof(DiffReporter))]
public class ApprovalTest
{
    [Test]
    public void ThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        TextTestFixture.Main(new string[] { "30" });
        var output = fakeOutput.ToString();

        Approvals.Verify(output);
    }
}