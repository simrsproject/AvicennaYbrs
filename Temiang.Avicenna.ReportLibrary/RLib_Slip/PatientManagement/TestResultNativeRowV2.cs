namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement
{
    using Telerik.Reporting;
    using BusinessObject;
    using System;
    using System.Data;
    using System.Linq;
    using Temiang.Avicenna.BusinessObject.Util;
    using System.Drawing.Drawing2D;
    using System.Drawing;

    /// <summary>
    /// Summary description for TestResultNative.
    /// </summary>
    public partial class TestResultNativeRowV2 : Telerik.Reporting.Report
    {
        public TestResultNativeRowV2(string programID, PrintJobParameterCollection printJobParameters)
        {
            InitializeComponent();
            //Helper.InitializeLogo(pageHeader);

            var rptData = new ReportDataSource();
            this.DataSource = rptData.GetDataTable(programID, printJobParameters);

            this.detail.ItemDataBinding += new EventHandler(detail_ItemDataBinding);
        }

        private void detail_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.DetailSection section = (sender as Telerik.Reporting.Processing.DetailSection);

            var iTableIndex = System.Convert.ToInt32(section.DataObject["TableIndex"]);

            if (iTableIndex > 0) {
                Telerik.Reporting.Processing.PictureBox pb = (Telerik.Reporting.Processing.PictureBox)Telerik.Reporting.Processing.ElementTreeHelper.GetChildByName(section, "pictureBox1");

                //var sTable = section.DataObject["ResultRows"].ToString();
                //string base64String = Convert.ToBase64String(Helper.HtmlToImage(sTable));
                //var img = Helper.HtmlToImage(sTable);

                //var fReduce = System.Convert.ToDecimal(AppParameter.GetParameterValue(AppParameter.ParameterItem.RptTableToImageReducePctg));
                //if (fReduce < 100) {
                //    img = Helper.ResizeImageForHTML(img, fReduce, InterpolationMode.Default);
                //}

                //pb.Image = img;
            }
        }
    }
}