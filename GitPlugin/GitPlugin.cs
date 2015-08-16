// --------------------------------------------------------------------------------------------------------------------
// <copyright file="gitplugin.cs" company="Copyright © 2015 jmecsoftware">
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

namespace SQGitPlugin
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
    public class SQGitPlugin : ISourceVersionPlugin
    {
        public SQGitPlugin()
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