namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.PatientManagement.HealthCertification
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class HealthLetter
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.picHealthcareLogo = new Telerik.Reporting.PictureBox();
            this.txtHealthcareInfo = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.txtParamedicName = new Telerik.Reporting.TextBox();
            this.textBox64 = new Telerik.Reporting.TextBox();
            this.txtNama = new Telerik.Reporting.TextBox();
            this.txtCityAndDate = new Telerik.Reporting.TextBox();
            this.textBox53 = new Telerik.Reporting.TextBox();
            this.txtDate = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.txtDes = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.txtBeratBadan = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.txtTinggiBadan = new Telerik.Reporting.TextBox();
            this.txtTekananDarah = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1687501668930054D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.picHealthcareLogo,
            this.txtHealthcareInfo});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageHeader.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            this.pageHeader.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // picHealthcareLogo
            // 
            this.picHealthcareLogo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3125D), Telerik.Reporting.Drawing.Unit.Inch(0.09375D));
            this.picHealthcareLogo.Name = "picHealthcareLogo";
            this.picHealthcareLogo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841D), Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841D));
            // 
            // txtHealthcareInfo
            // 
            this.txtHealthcareInfo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3125D), Telerik.Reporting.Drawing.Unit.Inch(0.09375D));
            this.txtHealthcareInfo.Name = "txtHealthcareInfo";
            this.txtHealthcareInfo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.1458334922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.90833348035812378D));
            this.txtHealthcareInfo.Style.Font.Bold = false;
            this.txtHealthcareInfo.Style.Font.Name = "Tahoma";
            this.txtHealthcareInfo.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtHealthcareInfo.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtHealthcareInfo.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.txtHealthcareInfo.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtHealthcareInfo.Value = "";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(3.6479167938232422D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtParamedicName,
            this.textBox64,
            this.txtNama,
            this.txtCityAndDate,
            this.textBox53,
            this.txtDate,
            this.textBox3,
            this.textBox9,
            this.txtDes,
            this.textBox13,
            this.textBox4,
            this.txtBeratBadan,
            this.textBox1,
            this.textBox2,
            this.textBox5,
            this.txtTinggiBadan,
            this.txtTekananDarah});
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Microsoft Sans Serif";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8.5D);
            // 
            // txtParamedicName
            // 
            this.txtParamedicName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.65625D), Telerik.Reporting.Drawing.Unit.Inch(3.2466843128204346D));
            this.txtParamedicName.Name = "txtParamedicName";
            this.txtParamedicName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999975681304932D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtParamedicName.Style.Font.Bold = false;
            this.txtParamedicName.Style.Font.Name = "Tahoma";
            this.txtParamedicName.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtParamedicName.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtParamedicName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtParamedicName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtParamedicName.Value = "";
            // 
            // textBox64
            // 
            this.textBox64.CanGrow = false;
            this.textBox64.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D), Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841D));
            this.textBox64.Name = "textBox64";
            this.textBox64.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49583339691162109D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox64.Style.Font.Bold = false;
            this.textBox64.Style.Font.Name = "Tahoma";
            this.textBox64.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox64.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox64.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox64.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox64.Value = "Nama";
            // 
            // txtNama
            // 
            this.txtNama.CanGrow = false;
            this.txtNama.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841D));
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.23341178894043D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtNama.Style.Font.Bold = false;
            this.txtNama.Style.Font.Name = "Tahoma";
            this.txtNama.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtNama.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtNama.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtNama.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtNama.Value = "";
            // 
            // txtCityAndDate
            // 
            this.txtCityAndDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6562480926513672D), Telerik.Reporting.Drawing.Unit.Inch(2.7612686157226562D));
            this.txtCityAndDate.Name = "txtCityAndDate";
            this.txtCityAndDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtCityAndDate.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtCityAndDate.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtCityAndDate.Style.Font.Bold = false;
            this.txtCityAndDate.Style.Font.Name = "Tahoma";
            this.txtCityAndDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtCityAndDate.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtCityAndDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtCityAndDate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtCityAndDate.Value = "";
            // 
            // textBox53
            // 
            this.textBox53.CanGrow = false;
            this.textBox53.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(0.56458336114883423D));
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.6749999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.20246219635009766D));
            this.textBox53.Style.Font.Bold = false;
            this.textBox53.Style.Font.Name = "Tahoma";
            this.textBox53.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox53.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox53.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox53.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox53.Value = "Dengan ini kami menerangkan bahwa :";
            // 
            // txtDate
            // 
            this.txtDate.CanGrow = false;
            this.txtDate.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(1.095833420753479D));
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.2542452812194824D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtDate.Style.Font.Bold = false;
            this.txtDate.Style.Font.Name = "Tahoma";
            this.txtDate.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtDate.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtDate.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtDate.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtDate.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = false;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69791668653488159D), Telerik.Reporting.Drawing.Unit.Inch(1.3041667938232422D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.93333339691162109D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox3.Style.Font.Bold = false;
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox3.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "dan ternyata";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D), Telerik.Reporting.Drawing.Unit.Inch(1.095833420753479D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.91666668653488159D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox9.Style.Font.Bold = false;
            this.textBox9.Style.Font.Name = "Tahoma";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox9.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "Pada tanggal";
            // 
            // txtDes
            // 
            this.txtDes.CanGrow = false;
            this.txtDes.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.7000001668930054D), Telerik.Reporting.Drawing.Unit.Inch(1.3374999761581421D));
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.20216178894043D), Telerik.Reporting.Drawing.Unit.Inch(0.50208348035812378D));
            this.txtDes.Style.Font.Bold = false;
            this.txtDes.Style.Font.Name = "Tahoma";
            this.txtDes.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtDes.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtDes.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtDes.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtDes.Value = "";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(1.8854166269302368D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.6875D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox13.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox13.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Catatan :";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.2604167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.1458333283662796D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5291671752929688D), Telerik.Reporting.Drawing.Unit.Inch(0.24174520373344421D));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.textBox4.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.Value = "SURAT KETERANGAN SEHAT";
            // 
            // txtBeratBadan
            // 
            this.txtBeratBadan.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(2.1187498569488525D));
            this.txtBeratBadan.Name = "txtBeratBadan";
            this.txtBeratBadan.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4874997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtBeratBadan.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtBeratBadan.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtBeratBadan.Style.Font.Bold = false;
            this.txtBeratBadan.Style.Font.Name = "Tahoma";
            this.txtBeratBadan.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtBeratBadan.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtBeratBadan.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtBeratBadan.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtBeratBadan.Value = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69791668653488159D), Telerik.Reporting.Drawing.Unit.Inch(2.1187498569488525D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox1.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "Berat Badan";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69791668653488159D), Telerik.Reporting.Drawing.Unit.Inch(2.3333332538604736D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Tinggi Badan";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.69791668653488159D), Telerik.Reporting.Drawing.Unit.Inch(2.5416667461395264D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0833333730697632D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox5.Style.Font.Bold = false;
            this.textBox5.Style.Font.Name = "Tahoma";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox5.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "Tekanan Darah";
            // 
            // txtTinggiBadan
            // 
            this.txtTinggiBadan.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(2.3333332538604736D));
            this.txtTinggiBadan.Name = "txtTinggiBadan";
            this.txtTinggiBadan.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4874997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtTinggiBadan.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtTinggiBadan.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtTinggiBadan.Style.Font.Bold = false;
            this.txtTinggiBadan.Style.Font.Name = "Tahoma";
            this.txtTinggiBadan.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtTinggiBadan.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtTinggiBadan.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTinggiBadan.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtTinggiBadan.Value = "";
            // 
            // txtTekananDarah
            // 
            this.txtTekananDarah.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(2.5416667461395264D));
            this.txtTekananDarah.Name = "txtTekananDarah";
            this.txtTekananDarah.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.4874997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.txtTekananDarah.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.txtTekananDarah.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.txtTekananDarah.Style.Font.Bold = false;
            this.txtTekananDarah.Style.Font.Name = "Tahoma";
            this.txtTekananDarah.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.txtTekananDarah.Style.Padding.Top = Telerik.Reporting.Drawing.Unit.Inch(0D);
            this.txtTekananDarah.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.txtTekananDarah.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.txtTekananDarah.Value = "";
            // 
            // HealthLetter
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail});
            this.Name = "SickLetter";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A5;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4770045280456543D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox txtParamedicName;
        private Telerik.Reporting.TextBox textBox64;
        private Telerik.Reporting.TextBox txtNama;
        private Telerik.Reporting.TextBox txtCityAndDate;
        private Telerik.Reporting.TextBox textBox53;
        private Telerik.Reporting.TextBox txtDate;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox txtDes;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox txtHealthcareInfo;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.PictureBox picHealthcareLogo;
        private Telerik.Reporting.TextBox txtBeratBadan;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtTinggiBadan;
        private Telerik.Reporting.TextBox txtTekananDarah;
    }
}