namespace NUnit.MSBuild.Example
{
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestThatPasses()
        {
            Assert.True(true);
        }

        [Test]
        public void TestThatFails()
        {
            Assert.True(false);
        }

        [Test]
        public void TestThatWarns()
        {
            Assert.Warn("Some Warning");
        }
    }
}
