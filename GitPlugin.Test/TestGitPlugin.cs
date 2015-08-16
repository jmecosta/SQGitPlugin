// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestGitPlugin.cs" company="Copyright � 2015 jmecsoftware">
//     Copyright (C) 2014 [jmecsoftware, jmecsoftware2014@tekla.com]
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License
// as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
// of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details. 
// You should have received a copy of the GNU Lesser General Public License along with this program; if not, write to the Free
// Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// --------------------------------------------------------------------------------------------------------------------
namespace SQGitPlugin.Test
{
    using NUnit.Framework;
    using System.Reflection;
    using System.IO;
    using VSSonarPlugins;
    using System;

    [TestFixture]
    public class TestSQGitPlugin
    {
        private readonly string executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));

        [Test]
        public void CorrectlyGetBranch()
        {
            var plugin = new SQGitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen");
            Assert.That(plugin.GetBranch(repo), Is.EqualTo("master"));
        }

        [Test]
        public void CorrectlyGetBranchIfNotRootRepo()
        {
            var plugin = new SQGitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen" , "src");
            Assert.That(plugin.GetBranch(repo), Is.EqualTo("master"));
        }

        [Test]
        public void ThrwosForNotAvaiableRepo()
        {
            var plugin = new SQGitPlugin() as ISourceVersionPlugin;
            var sourceDirPath = Directory.GetParent(Directory.GetParent(executionPath).ToString()).ToString();
            var repo = Path.Combine(sourceDirPath, "TestData", "sonar-doxygen", "src");
            Assert.Throws<NullReferenceException>(() => plugin.GetBranch("c:\\temp"));
        }
    }
}
