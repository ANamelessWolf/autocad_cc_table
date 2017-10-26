using Nameless.Flareon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Nameless.Flareon.Assets.Constants;
namespace Nameless.Flareon.Runtime
{
    /// <summary>
    /// Creates a new application settings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the number of decimals.
        /// </summary>
        /// <value>
        /// The number of decimals.
        /// </value>
        public int? NumberOfDecimals
        {
            get
            {
                var att = this.GetValue(ATT_NUM_DEC);
                int val;
                if (att != null && int.TryParse(att, out val))
                    return val;
                else
                    return new Nullable<int>();
            }
            set
            {
                this.SetValue(ATT_NUM_DEC, value.Value.ToString());
            }
        }
        /// <summary>
        /// Define el formato que utiliza la aplicación para mostrar los
        /// datos del azimut
        /// </summary>
        /// <value>
        /// El formato del Azimut.
        /// </value>
        public SexagesimalFormat AzimutFormat
        {
            get
            {
                var att = this.GetValue(ATT_AZI_FORMAT);
                int val;
                if (att != null && int.TryParse(att, out val))
                    return (SexagesimalFormat)val;
                else
                    return SexagesimalFormat.NONE;
            }
            set
            {
                this.SetValue(ATT_NUM_DEC, ((int)value).ToString());
            }
        }
        /// <summary>
        /// The XML file path
        /// </summary>
        readonly String XmlFilePath;
        /// <summary>
        /// The XElement root node
        /// </summary>
        XElement Root;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        public AppSettings(string xmlFilePath)
        {
            this.XmlFilePath = xmlFilePath;
            if (!File.Exists(xmlFilePath))
                this.CreateXmlFile();
            else
            {
                XDocument doc = XDocument.Load(this.XmlFilePath);
                this.Root = doc.Elements().FirstOrDefault(x => x.Name == APP_DIR_NAME);
            }
            this.Init();
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                var value = prop.GetValue(this);
                var key = prop.Name;
                if (value is Nullable<int> && !(value as Nullable<int>).HasValue)
                    this.SetValue(key, DFTL_NUM_DEC.ToString());
            }
        }
        /// <summary>
        /// Creates the XML file.
        /// </summary>
        private void CreateXmlFile()
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            this.Root = new XElement(APP_DIR_NAME);
            doc.Add(this.Root);
            this.Save();
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {
            this.Root.Document.Save(this.XmlFilePath);
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="attName">Name of the attribute.</param>
        /// <returns>The attribute value</returns>
        private String GetValue(string attName)
        {
            var att = this.Root.Attribute(attName);
            if (att != null)
                return att.Value;
            else
                return null;
        }
        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="attName">Name of the attribute.</param>
        /// <param name="value">The value for the attribute.</param>
        private void SetValue(string attName, string value)
        {
            var att = this.Root.Attribute(attName);
            if (att != null)
                att.Value = value;
            else
            {
                att = new XAttribute(attName, value);
                this.Root.Add(att);
            }
            this.Save();
        }
    }
}
