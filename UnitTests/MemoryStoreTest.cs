using System.Collections.Generic;
using NUnit.Framework;
using QuickFix.Store;

namespace UnitTests;

[TestFixture]
public class MemoryStoreTests
{
    [Test]
    public void GetTest()
    {
        MemoryStore store = new MemoryStore();
        store.Set(1, "dude");
        store.Set(2, "pude");
        store.Set(3, "ok");
        store.Set(4, "ohai");
        List<string> msgs = [];
        store.Get(2, 3, msgs);
        List<string> expected = ["pude", "ok"];
        Assert.That(msgs, Is.EqualTo(expected));

        msgs = [];
        store.Get(5, 6, msgs);
        Assert.That(msgs, Is.Empty);
    }
}
