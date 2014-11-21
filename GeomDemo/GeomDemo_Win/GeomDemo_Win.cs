using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ESRI.ArcGIS.Mobile;
using ESRI.ArcGIS.Mobile.Client;
using ESRI.ArcGIS.Mobile.Client.Pages;
using ESRI.ArcGIS.Mobile.Client.Tasks.CollectFeatures;
using ESRI.ArcGIS.Mobile.FeatureCaching;

namespace GeomDemo
{
    /// <summary>///
    /// Note: Make sure the following attributes are the same for all 
    /// the projects in this solution
    /// 1. Current Namespace
    /// 2. Task class name and project center class name
    /// 3. Assembly name (through Project's Property dialog)
    /// 4. Default Namespace (through Project's Property dialog)
    /// </summary>
    public class GeomDemoClass : Task
    {
        SketchGeometryCollectionMethod _sketchGeometryCollectionMethod;
        FeatureSourceInfo _featureSourceInfo;
        EditFeatureAttributesPage _editFeatureAttributesPage;
        EditFeatureAttributesViewModel _editFeatureAttributesViewModel;
        SketchGeometryPage _sketchGP;

        protected Feature FeatureToCreate;

        // TODO: Update constructor name after you change task name
        public GeomDemoClass()
        {
            //Name
            Name = "GeomDemoClass";
            //Description
            Description = "Task description";
        }

        // TODO: If you change task assembly name, replace the "GeomDemo" 
        // in the Uri with new Task assembly name.
        protected override System.Windows.Media.ImageSource GetImageSource()
        {
            Uri uri = new Uri("pack://application:,,,/GeomDemo;Component/TaskIcon72.png");
            return new System.Windows.Media.Imaging.BitmapImage(uri);
        }

        public override void Execute()
        {
//            MobileApplication.Current.Transition(new Page1());

            Helpers.NavigateToMap();

            var layer = MobileApplication.Current.Project.FindFeatureSourceInfo("FB_Split");

            if (layer == null)
            {
                ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(string.Format("The '{0}' layer is not in the map.", "FB_Split"), "Layer not found", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!layer.CanCreate)
            {
                ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog(string.Format("The '{0}' layer is not editable.", "FB_Split"), "Layer not editable", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Reset _feature
                FeatureToCreate = null;
                _featureSourceInfo = Helpers.FindFeatureSourceInfoByName("FB_Split");
                if (_featureSourceInfo == null)
                {
                    ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Can't find layer.", "Warning");
                    return;
                }

                // TODO : change this
                // Create a new Feature, this will automatically call StartEditing on this feature
                FeatureToCreate = new Feature(_featureSourceInfo.FeatureTypes.FirstOrDefault(), null);

                //THIS IS NOT WORKING- NOT CAPTURING DATA
                _sketchGeometryCollectionMethod = new SketchGeometryCollectionMethod();

                _sketchGP = new SketchGeometryPage();
                _sketchGP.ClickBack += GeometryCollectionPageClickBack;
                _sketchGP.ClickNext += SketchGeometryCollectionMethodOnCompleted;

                MobileApplication.Current.Transition(_sketchGP);

                _sketchGeometryCollectionMethod.StartGeometryCollection(_sketchGP.Geometry);


            }
            catch
            {
                GoToHomePage();
            }

        }

        void GeometryCollectionPageClickBack(object sender, EventArgs e)
        {
            GoToHomePage();
        }

        void SketchGeometryCollectionMethodOnCompleted(object sender, EventArgs completedEventArgs)
        {
            try
            {

                if (!_sketchGeometryCollectionMethod.Geometry.IsValid) return;

                FeatureToCreate.Geometry = _sketchGP.Geometry;
                // Use this if you want to go to the edit attributes page
                if (_editFeatureAttributesPage == null)
                    _editFeatureAttributesViewModel = new EditFeatureAttributesViewModel(FeatureToCreate);
                _editFeatureAttributesPage = new EditFeatureAttributesPage(_editFeatureAttributesViewModel);

                _editFeatureAttributesPage.ClickOk += EditFeatureAttributesPageClickOk;
                _editFeatureAttributesPage.ClickCancel += EditFeatureAttributesPageClickCancel;


                // Pass the sketched Feature to EditFeatureAttributesPage, and transition to this page

                MobileApplication.Current.Transition(_editFeatureAttributesPage);
            }

            catch (Exception ex)
            { Helpers.MattMessage("Error going to Edit page", ex.ToString()); }
        }

        void EditFeatureAttributesPageClickOk(object sender, EventArgs e)
        {
            // Save the feature
            if (!FeatureToCreate.SaveEditing())
                ESRI.ArcGIS.Mobile.Client.Windows.MessageBox.ShowDialog("Cannot save edits.", "Warning");

            // Go back to homepage
            GoToHomePage();
        }

        void EditFeatureAttributesPageClickCancel(object sender, EventArgs e)
        {
            // Cancel the edits
            FeatureToCreate.CancelEditing();
            FeatureToCreate = null;
            _featureSourceInfo = null;

            // Go back to homepage
            GoToHomePage();
        }


        void GoToHomePage()
        {
            Helpers.NavigateToMap();
        }


        #region IJsonSerializer Members
        /// <summary>
        /// Generates an object from its JSON representation
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
