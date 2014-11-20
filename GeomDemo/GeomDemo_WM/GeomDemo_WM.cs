using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Mobile.Client;

namespace GeomDemo
{
    /// <summary>
    /// Task class
    /// Note: Make sure the following attributes are the same for all 
    /// the projects in this solution
    /// 1. Current Namespace
    /// 2. Task class name and project center class name
    /// 3. Assembly name (through Project's Property dialog)
    /// 4. Default Namespace (through Project's Property dialog)
    /// </summary>
    public class GeomDemoClass : Task
    {
        // TODO: Always update constructor name if you change task name
        public GeomDemoClass()
        {
            // TODO: Set task name and description
            Name = "GeomDemoClass";
            Description = "Task Description";

            // TODO: Set a custom task icon (optional)
            //LargeIcon = null;
        }

        public override void Execute()
        {
            MobileApplication.Current.Transition(new Page1());
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
            Name = values["name"] as string;
            Description = values["description"] as string;
        }

        /// <summary>
        /// Converts an object into its JSON representation.
        /// </summary>
        public override void WriteJson(IDictionary<string, object> writeData)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();
            writeData.Add("Attributes", values);
            values.Add("name", Name);
            values.Add("description", Description);
        }
        #endregion

    }
}
