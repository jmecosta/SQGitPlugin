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
    using System.Diagnostics;

    /// <summary>
    ///     The cpp plugin.
    /// </summary>
    [Export(typeof(IPlugin))]
    public class GitPlugin : ISourceVersionPlugin
    {
        public GitPlugin()
        {
            this.descrition = new PluginDescription();
            this.descrition.Description = "Git Source Code Provider";
            this.descrition.Enabled = true;
            this.descrition.Name = "Git Plugin";
            this.descrition.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.descrition.AssemblyPath = Assembly.GetExecutingAssembly().Location;
        }

        /// <summary>
        /// dll path locations
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private readonly List<string> DllPaths = new List<string>();
        private readonly PluginDescription descrition;

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
            string currePath = basePath;

            while (Path.IsPathRooted(Path.GetFullPath(currePath)))
            {
                try
                {
                    using (var repo = new Repository(currePath))
                    {
                        return repo.Head.Name;
                    }
                }
                catch (RepositoryNotFoundException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                currePath = Directory.GetParent(currePath).ToString();
            }

            return "";

        }

        public bool IsSupported(string basePath)
        {
            string currePath = basePath;

            while (Path.IsPathRooted(Path.GetFullPath(currePath)))
            {
                try
                {
                    using (new Repository(currePath))
                    {
                        return true;
                    }
                }
                catch (RepositoryNotFoundException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                currePath = Directory.GetParent(currePath).ToString();
            }

            return false;
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
            return null;
        }

        public PluginDescription GetPluginDescription()
        {
            return this.descrition;
        }

        public void ResetDefaults()
        {
        }

        public void SetDllLocation(string path)
        {
            this.DllPaths.Add(path);
        }


    }
}