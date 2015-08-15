namespace GitPlugin.Test
{
    using NUnit.Framework;
    using System.Reflection;
    using System.IO;
    using VSSonarPlugins;
    using System;

    [TestFixture]
    public class TestGitPlugin
    {
        private readonly string executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));

        [Test]
        public void CorrectlyGetBranch()
        {
            var plugin = new GitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen");
            Assert.That(plugin.GetBranch(repo), Is.EqualTo("master"));
        }

        [Test]
        public void CorrectlyGetBranchIfNotRootRepo()
        {
            var plugin = new GitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen" , "src");
            Assert.That(plugin.GetBranch(repo), Is.EqualTo("master"));
        }

        [Test]
        public void ThrwosForNotAvaiableRepo()
        {
            var plugin = new GitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen", "src");
            Assert.Throws<NullReferenceException>(() => plugin.GetBranch("c:\\temp"));
        }
    }
}
