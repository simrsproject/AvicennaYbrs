using System.Configuration;
using System.Data;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Util;
using Temiang.Avicenna.ReportLibrary.Properties;
using System.Drawing;

namespace Temiang.Avicenna.ReportLibrary
{
    public class Helper
    {
        internal static DataTable ReportDataSource(string programID, PrintJobParameterCollection printJobParameters)
        {
            var rptData = new ReportDataSource();
            return rptData.GetDataTable(programID, printJobParameters);
        }

        internal static void InitializeDataSource(Report report, string programID, PrintJobParameterCollection printJobParameters)
        {
            report.DataSource = ReportDataSource(programID, printJobParameters);
        }

        internal static void InitializeLogo(ReportSectionBase reportSection)
        {
            InitializeLogo(reportSection, 0, 0);
        }

        internal static void InitializeLogo(ReportSectionBase reportSection,double padingLeft, double padingTop)
        {
            var picHealthcareLogo = new PictureBox();
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[]
            {
                picHealthcareLogo,
                txtHealthcareInfo
            });

            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Location = new PointU(new Unit(padingLeft, UnitType.Inch), new Unit(padingTop, UnitType.Inch));
            picHealthcareLogo.Size = new SizeU(new Unit(0.89999997615814209, UnitType.Inch), new Unit(0.60000002384185791 , UnitType.Inch));
            picHealthcareLogo.Sizing = ImageSizeMode.ScaleProportional;
            picHealthcareLogo.Style.Font.Size = new Unit(8, UnitType.Point);
            picHealthcareLogo.Value = ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));

            txtHealthcareInfo.Location = new PointU(new Unit(0.90007877349853516 + padingLeft, UnitType.Inch), new Unit(0.006249984260648489 + padingTop, UnitType.Inch));
            txtHealthcareInfo.Size = new SizeU(new Unit(3.4999606609344482, UnitType.Inch), new Unit(0.59367138147354126, UnitType.Inch));
            txtHealthcareInfo.Style.Font.Name = "Tahoma";
            txtHealthcareInfo.Style.Font.Size = new Unit(8, UnitType.Point);
            txtHealthcareInfo.Style.TextAlign = HorizontalAlign.Left;
            txtHealthcareInfo.Style.VerticalAlign = VerticalAlign.Middle;
            txtHealthcareInfo.TextWrap = false;
            txtHealthcareInfo.Value = Healthcare.AddressComplete();
        }
        internal static void InitializeLogoOnly(ReportSectionBase reportSection)
        {
            var picHealthcareLogo = new PictureBox();
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[]
            {
                picHealthcareLogo
            });

            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Size = new SizeU(new Unit(3.39999997615814209, UnitType.Inch), new Unit(0.60000002384185791, UnitType.Inch));
            picHealthcareLogo.Sizing = ImageSizeMode.ScaleProportional;
            picHealthcareLogo.Style.Font.Size = new Unit(8, UnitType.Point);
            picHealthcareLogo.Value = ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));
        }

        internal static void InitializeLogoOnlyLeft(ReportSectionBase reportSection)
        {
            var picHealthcareLogo = new PictureBox();
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[]
            {
                picHealthcareLogo,
                txtHealthcareInfo
            });

            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Size = new SizeU(new Unit(0.89999997615814209, UnitType.Inch), new Unit(0.60000002384185791, UnitType.Inch));
            picHealthcareLogo.Sizing = ImageSizeMode.ScaleProportional;
            picHealthcareLogo.Style.Font.Size = new Unit(8, UnitType.Point);
            picHealthcareLogo.Value = ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));

            txtHealthcareInfo.Location = new PointU(new Unit(0.90007877349853516, UnitType.Inch), new Unit(0.006249984260648489, UnitType.Inch));
            txtHealthcareInfo.Size = new SizeU(new Unit(3.4999606609344482, UnitType.Inch), new Unit(0.59367138147354126, UnitType.Inch));
            txtHealthcareInfo.Style.Font.Name = "Tahoma";
            txtHealthcareInfo.Style.Font.Size = new Unit(8, UnitType.Point);
            txtHealthcareInfo.Style.TextAlign = HorizontalAlign.Left;
            txtHealthcareInfo.Style.VerticalAlign = VerticalAlign.Middle;
            txtHealthcareInfo.TextWrap = false;
            txtHealthcareInfo.Value = "";
        }

        internal static void InitializeLogoAndTextBottom(ReportSectionBase reportSection)
        {
            var picHealthcareLogo = new PictureBox();
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[]
            {
                picHealthcareLogo,txtHealthcareInfo
            });

            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.88240450620651245D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
            picHealthcareLogo.Sizing = ImageSizeMode.ScaleProportional;
            picHealthcareLogo.Style.Font.Size = new Unit(8, UnitType.Point);
            picHealthcareLogo.Value = ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));
            picHealthcareLogo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.48718643188476562D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));

            txtHealthcareInfo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.11157187074422836D), Telerik.Reporting.Drawing.Unit.Inch(0.800078809261322D));
            txtHealthcareInfo.Name = "textBox4";
            txtHealthcareInfo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5567854642868042D), Telerik.Reporting.Drawing.Unit.Inch(0.40007886290550232D));
            txtHealthcareInfo.Style.Font.Bold = true;
            txtHealthcareInfo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            txtHealthcareInfo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            txtHealthcareInfo.Value = "Rumah Sakit\r\nRK. Charitas Palembang";

        }
        internal static void InitializeLogoOnlySizeModeNormal(ReportSectionBase reportSection)
        {
            var picHealthcareLogo = new PictureBox();
            reportSection.Items.AddRange(new ReportItemBase[]
            {
                picHealthcareLogo
            });

            picHealthcareLogo.MimeType = "image/bmp";
            picHealthcareLogo.Size = new SizeU(new Unit(3.39999997615814209, UnitType.Inch), new Unit(0.60000002384185791, UnitType.Inch));
            picHealthcareLogo.Sizing = ImageSizeMode.Normal;
            picHealthcareLogo.Style.Font.Size = new Unit(8, UnitType.Point);
            picHealthcareLogo.Value = ResourceLogo(AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial));
        }

        internal static void InitializeNoLogo(ReportSectionBase reportSection)
        {
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[] { txtHealthcareInfo });

            txtHealthcareInfo.Location = new PointU(new Unit(0.13007877349853516, UnitType.Inch), new Unit(0.0849984260648489, UnitType.Inch));
            txtHealthcareInfo.Size = new SizeU(new Unit(3.4999606609344482, UnitType.Inch), new Unit(0.69367138147354126, UnitType.Inch));
            txtHealthcareInfo.Style.Font.Name = "MS Reference Sans Serif";
            txtHealthcareInfo.Style.Font.Size = new Unit(9, UnitType.Point);
            txtHealthcareInfo.Style.TextAlign = HorizontalAlign.Center;
            txtHealthcareInfo.Style.VerticalAlign = VerticalAlign.Middle;
            txtHealthcareInfo.TextWrap = false;
            txtHealthcareInfo.Value = Healthcare.AddressComplete();
        }

        internal static void InitializeNoLogoAlignLeft(ReportSectionBase reportSection)
        {
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[] { txtHealthcareInfo });

            txtHealthcareInfo.Location = new PointU(new Unit(0.13007877349853516, UnitType.Inch), new Unit(0.0849984260648489, UnitType.Inch));
            txtHealthcareInfo.Size = new SizeU(new Unit(3.4999606609344482, UnitType.Inch), new Unit(0.69367138147354126, UnitType.Inch));
            txtHealthcareInfo.Style.Font.Name = "Tahoma";
            txtHealthcareInfo.Style.Font.Size = new Unit(8, UnitType.Point);
            txtHealthcareInfo.Style.TextAlign = HorizontalAlign.Left;
            txtHealthcareInfo.Style.VerticalAlign = VerticalAlign.Middle;
            txtHealthcareInfo.TextWrap = true;
            txtHealthcareInfo.Value = Healthcare.AddressComplete();
        }

        internal static void InitializeNoLogoBigFont(ReportSectionBase reportSection)
        {
            var txtHealthcareInfo = new TextBox();
            reportSection.Items.AddRange(new ReportItemBase[] { txtHealthcareInfo });

            txtHealthcareInfo.Location = new PointU(new Unit(0.00007877349853516, UnitType.Inch), new Unit(0.006249984260648489, UnitType.Inch));
            txtHealthcareInfo.Size = new SizeU(new Unit(4.4999606609344482, UnitType.Inch), new Unit(0.99367138147354126, UnitType.Inch));
            txtHealthcareInfo.Style.Font.Name = "Microsoft Sans Serif";
            txtHealthcareInfo.Style.Font.Size = new Unit(11, UnitType.Point);
            txtHealthcareInfo.Style.TextAlign = HorizontalAlign.Left;
            txtHealthcareInfo.Style.VerticalAlign = VerticalAlign.Middle;
            txtHealthcareInfo.Style.Font.Bold = true;
            txtHealthcareInfo.TextWrap = false;
            txtHealthcareInfo.Value = Healthcare.AddressComplete();
        }

        public static Bitmap ResourceLogo(string healthcareInitial)
        {
            if (ConfigurationManager.AppSettings["IsDemo"] != null &&
                ("yes".Equals(ConfigurationManager.AppSettings["IsDemo"].ToLower()) ||
                 "true".Equals(ConfigurationManager.AppSettings["IsDemo"].ToLower()))) // Demo use DefaultLogo
            {
                healthcareInitial = "Default";
            }

            switch (healthcareInitial)
            {
                case "RSSA":
                    return Resources.LogoRSSA;
                case "RSGPI":
                    return Resources.LogoRSGPI;
                case "RSIAMTP":
                    return Resources.LogoRSIAMTP;
                case "RSPP":
                    return Resources.LogoRSPP;
                case "RSARS":
                    return Resources.LogoARS;
                case "RSIAM":
                    return Resources.LogoRSIAM;
                case "RSCH":
                    return Resources.LogoRSCH;
                case "RSUI":
                    return Resources.LogoRSUI;
                case "PAC":
                    return Resources.HealthcareLogo;
                case "LMC":
                    return Resources.LogoLMC;
                case "RSALMAH":
                    return Resources.LogoRSALMAH;
                case "RSUTAMA":
                    return Resources.LogoRSUTAMA;
                case "RSMP":
                    return Resources.LogoRSMP;
                case "RSBHP":
                    return Resources.LogoRSBHP;
                case "RSPM":
                    return Resources.LogoRSPM;
                case "RSI":
                    return Resources.LogoRSI;
                default:
                    return Resources.DefaultLogo;
            }
        }
    }
}
