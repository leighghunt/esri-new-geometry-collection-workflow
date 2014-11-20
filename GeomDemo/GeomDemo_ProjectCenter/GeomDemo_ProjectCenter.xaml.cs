using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ESRI.ArcGIS.Mobile.ClientManager.Extensions;

namespace GeomDemo
{
    /// <summary>
    /// Interaction logic for GeomDemo_ProjectCenter.xaml
    ///
    /// Note: Make sure the following attributes are the same for all 
    /// the projects in this solution
    /// 1. Current Namespace
    /// 2. Task class name and project center class name
    /// 3. Assembly name (through Project's Property dialog)
    /// 4. Default Namespace (through Project's Property dialog)
    /// </summary>
    public partial class GeomDemoClass : ProjectTaskControl
    {
        // TODO: Set DisplayName
        private string m_displayName = "GeomDemoClass_ProjectCentre";

        // TODO: Always update constructor name if you change task name
        public GeomDemoClass()
        {
            InitializeComponent();
            txtTaskName.Text = DisplayName;
            lblTitle.Text = DisplayName;
        }

        /// <summary>
        /// Gets/sets Description for your capability
        /// </summary>
        public override string Description
        {
            get { return base.Description; }
            set
            {
                base.Description = value;
                RaisepropertyChangedEvent("Description");
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets/sets DisplayName for your capability
        /// </summary>
        public override string DisplayName
        {
            get { return m_displayName; }
            set
            {
                m_displayName = value;
                RaisepropertyChangedEvent("DisplayName");
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the icon for your capability (for display within Mobile Project Center)
        /// TODO: If you change Mobile Project assembly name, replace "GeomDemo"	
        /// in the Uri with new assembly name.
        /// </summary>
        /// <remarks>You can embed custom icons with this project, and return it through getter</remarks>
        public override ImageSource Icon
        {
            get
            {
                Uri uri = new Uri("pack://application:,,,/GeomDemo;Component/Task72.png");
                return new BitmapImage(uri);
            }
        }

        /// <summary>
        /// When the value of the txtTaskName text box changes
        /// the DisplayName value of the task will be updated accordingly
        /// </summary>
        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayName = txtTaskName.Text;
            lblTitle.Text = txtTaskName.Text;
        }

        #region IJsonSerializer Members
        /// <summary>
        /// Generates an object from its JSON representation.
        /// </summary>
        public override void ReadJson(IDictionary<string, object> readData)
        {
            if (!readData.ContainsKey("Attributes"))
                return;
            IDictionary<string, object> values = readData["Attributes"] as Dictionary<string, object>;
            DisplayName = values["name"] as string;
            Description = values["description"] as string;

            //populate the name and description textboxes when an existing project opens
            txtTaskName.Text = DisplayName;
            txtTaskDescription.Text = Description;
        }

        /// <summary>
        /// Converts an object into its JSON representation.
        /// </summary>
        public override void WriteJson(IDictionary<string, object> writeData)
        {
            //Obtain the task description from the txtTaskDescription textbox
            Description = txtTaskDescription.Text;

            IDictionary<string, object> values = new Dictionary<string, object>();
            writeData.Add("Attributes", values);
            values.Add("name", DisplayName);
            values.Add("description", Description);
        }
        #endregion
    }
}
