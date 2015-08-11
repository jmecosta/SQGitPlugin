// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CxxPlugin.cs" company="Copyright © 2014 jmecsoftware">
//   Copyright (C) 2014 [jmecsoftware, jmecsoftware2014@tekla.com]
// </copyright>
// <summary>
//   The cpp plugin.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GitPlugin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Threading;

    using VSSonarPlugins;
    using VSSonarPlugins.Types;
    using LibGit2Sharp;

    /// <summary>
    ///     The cpp plugin.
    /// </summary>
    [Export(typeof(IPlugin))]
    public class GitPlugin : ISourceVersionPlugin
    {
        public GitPlugin()
        {
        }

        /// <summary>
        /// dll path locations
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private readonly List<string> DllPaths = new List<string>();

        public void AssociateProject(Resource project, ISonarConfiguration configuration)
        {
        }

        public void Dispose()
        {
        }

        public IList<string> DllLocations()
        {
            return DllPaths;
        }

        public string GenerateTokenId(ISonarConfiguration configuration)
        {
            return "";
        }

        public string GetBranch(string basePath)
        {
            using (var repo = new Repository(basePath))
            {
                return repo.Head.Name;
            }
        }

        public IList<string> GetHistory(Resource item)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, VsLicense> GetLicenses(ISonarConfiguration configuration)
        {
            return null;
        }

        public IPluginControlOption GetPluginControlOptions(Resource project, ISonarConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public PluginDescription GetPluginDescription()
        {
            throw new NotImplementedException();
        }

        public void ResetDefaults()
        {
            throw new NotImplementedException();
        }

        public void SetDllLocation(string path)
        {
            throw new NotImplementedException();
        }
    }
}