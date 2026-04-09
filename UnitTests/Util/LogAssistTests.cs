using NUnit.Framework;
using QuickFix.Util;

namespace UnitTests.Util;

[TestFixture]
public class LogAssistTests {

    private static string Sohize(string s) {
        return s.Replace('|', QuickFix.Message.SOH);
    }

    [Test]
    public void RedactSensitiveFieldsTest()
    {
        // no change
        string inp = Sohize("8=FIX.4.3|9=100|35=B|34=2|49=ISLD|52=20260116-03:12:25.547|56=TW|148=blah|10=126|");
        Assert.That(inp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, []))); // no fields to redact
        Assert.That(inp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, [3,48]))); // fields are not in message

        // redact 1 tag
        string outp = Sohize("8=FIX.4.3|9=100|35=B|34=2|49=<redacted>|52=20260116-03:12:25.547|56=TW|148=blah|10=126|");
        Assert.That(outp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, [49])));

        // redact 2 tags
        outp = Sohize("8=FIX.4.3|9=100|35=B|34=2|49=<redacted>|52=<redacted>|56=TW|148=blah|10=126|");
        Assert.That(outp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, [49,52])));

        // redact 2 tags and specify alt redaction text
        outp = Sohize("8=FIX.4.3|9=100|35=B|34=2|49=hidden|52=hidden|56=TW|148=blah|10=126|");
        Assert.That(outp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, [49,52], "hidden")));

        // redact multiple occurrences of a tag
        inp = Sohize("8=FIX.4.2|9=91|35=B|34=2|49=TW|52=20111011-15:06:23.103|56=ISLD|148=headline|33=3|"
                     + "58=line1|354=3|355=uno|58=line2|354=9|355=dos|58=line3|354=4|355=tres|10=193|");

        outp = Sohize("8=FIX.4.2|9=91|35=B|34=2|49=TW|52=20111011-15:06:23.103|56=ISLD|148=<redacted>|33=3|"
                      + "58=line1|354=3|355=<redacted>|58=line2|354=9|355=<redacted>|58=line3|354=4|355=<redacted>|10=193|");
        Assert.That(outp, Is.EqualTo(LogAssist.RedactSensitiveFields(inp, [148,355])));
    }
}
