using Nameless.Flareon.Yggdrasil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nameless.Flareon.Assets.Constants;
namespace Nameless.Flareon.Runtime
{
    /// <summary>
    /// Defines the addin application
    /// </summary>
    public class AutoCADCCApplication : NamelessObject
    {
        /// <summary>
        /// Gets the application directory.
        /// </summary>
        /// <value>
        /// The application directory.
        /// </value>
        public String AppDirectory
        {
            get
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(path, COMPANY_DIR_NAME, APP_DIR_NAME);
            }
        }
        /// <summary>
        /// Gets the settings file path.
        /// </summary>
        /// <value>
        /// The settings file path.
        /// </value>
        public String SettingsFilePath
        {
            get
            {
                return Path.Combine(AppDirectory, SETT_FILE);
            }
        }
        /// <summary>
        /// The application settings
        /// </summary>
        public AppSettings Settings;
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCADCCApplication"/> class.
        /// </summary>
        public AutoCADCCApplication()
        {
            DirectoryInfo dir = new DirectoryInfo(this.AppDirectory);
            if (Directory.Exists(dir.Parent.FullName))
                Directory.CreateDirectory(dir.Parent.FullName);
            if (Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            this.Settings = new AppSettings(this.SettingsFilePath);
        }
    }
}
