namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Charges
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class PaymentReturnReceipt
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TextBox TxtAlamat1;
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.txtTotalAmountInWords = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.TxtAmount = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.TxtUserName = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            TxtAlamat1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TxtAlamat1
            // 
            TxtAlamat1.CanGrow = false;
            TxtAlamat1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841), Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525));
            TxtAlamat1.Name = "TxtAlamat1";
            TxtAlamat1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9061708450317383), Telerik.Reporting.Drawing.Unit.Inch(0.20000045001506805));
            TxtAlamat1.Style.Font.Name = "Microsoft Sans Serif";
            TxtAlamat1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            TxtAlamat1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            TxtAlamat1.Value = "=StreetName";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(4.8000006675720215);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox14,
            this.textBox16,
            this.textBox17,
            this.textBox5,
            this.textBox13,
            this.textBox12,
            this.textBox26,
            this.textBox27,
            this.textBox21,
            this.txtTotalAmountInWords,
            this.textBox10,
            this.textBox6,
            this.textBox4,
            this.textBox18,
            this.textBox3,
            this.textBox7,
            this.textBox9,
            TxtAlamat1,
            this.textBox15,
            this.textBox30,
            this.textBox32,
            this.TxtAmount,
            this.textBox2,
            this.textBox8,
            this.textBox11,
            this.textBox19,
            this.TxtUserName,
            this.textBox22,
            this.textBox23,
            this.TxtCityRS,
            this.textBox24});
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Style.Font.Name = "Microsoft Sans Serif";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.3000001907348633), Telerik.Reporting.Drawing.Unit.Inch(3.93986701965332E-05));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8000004291534424), Telerik.Reporting.Drawing.Unit.Inch(0.28958341479301453));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=Title";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8020834922790527), Telerik.Reporting.Drawing.Unit.Inch(0.28970152139663696));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox14.Style.Font.Name = "Tahoma";
            this.textBox14.Value = "=PaymentNo";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(1.2000000476837158));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox16.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox16.Value = "=MedicalNo";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.8959524631500244));
            this.textBox17.Name = "textBox15";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox17.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Value = "=Ket";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.6000006198883057));
            this.textBox5.Name = "textBox15";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79158735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Value = "Klasifikasi";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.79166668653488159), Telerik.Reporting.Drawing.Unit.Inch(2.2000007629394531));
            this.textBox13.Name = "textBox2";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox13.Value = ":";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.2000007629394531));
            this.textBox12.Name = "textBox3";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79158735275268555), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox12.Value = "Nama";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525));
            this.textBox26.Name = "textBox20";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.795754611492157), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox26.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox26.Value = "Alamat";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = false;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841), Telerik.Reporting.Drawing.Unit.Inch(2.1980361938476562));
            this.textBox27.Name = "textBox22";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.8041667938232422), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox27.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Inch(0);
            this.textBox27.Value = "=PatientName";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.4895833730697632), Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9886078834533691), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox21.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox21.Value = "=Notes";
            // 
            // txtTotalAmountInWords
            // 
            this.txtTotalAmountInWords.CanGrow = false;
            this.txtTotalAmountInWords.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(1.5999997854232788));
            this.txtTotalAmountInWords.Name = "txtTotalAmountInWords";
            this.txtTotalAmountInWords.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9803147315979), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.txtTotalAmountInWords.Style.Font.Name = "Microsoft Sans Serif";
            this.txtTotalAmountInWords.Value = "=TotalAmountInWords";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421));
            this.textBox10.Name = "textBox32";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3957544565200806), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox10.Value = "Keterangan";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.5999997854232788));
            this.textBox6.Name = "textBox32";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox6.Value = "Uang Sebesar";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3958333730697632), Telerik.Reporting.Drawing.Unit.Inch(1.8999999761581421));
            this.textBox4.Name = "textBox31";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.093671165406703949), Telerik.Reporting.Drawing.Unit.Inch(0.20220279693603516));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox4.Value = ":";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.89583331346511841), Telerik.Reporting.Drawing.Unit.Inch(2.6000006198883057));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.5998435020446777), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox18.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox18.Value = "=GuarantorName";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.99999982118606567));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3979164361953735), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox3.Value = "No. Registration";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.1999994516372681));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox7.Value = "No. MR";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.0022015571594238));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox9.Value = ":";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(0.99999982118606567));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox15.Style.Font.Bold = false;
            this.textBox15.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox15.Value = "=RegistrationNo";
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.278191089630127), Telerik.Reporting.Drawing.Unit.Inch(4.5999608039855957));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.200000524520874), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox30.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox30.Value = "=PrintReceiptAsName";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.3999996185302734));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox32.Style.Font.Bold = true;
            this.textBox32.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox32.Value = "Sudah Terima Dari";
            // 
            // TxtAmount
            // 
            this.TxtAmount.CanGrow = false;
            this.TxtAmount.Format = "Rp. {0:N0}";
            this.TxtAmount.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.3999999463558197), Telerik.Reporting.Drawing.Unit.Inch(3.4999992847442627));
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9041663408279419), Telerik.Reporting.Drawing.Unit.Inch(0.39996066689491272));
            this.TxtAmount.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.TxtAmount.Style.Font.Bold = true;
            this.TxtAmount.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtAmount.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15);
            this.TxtAmount.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.TxtAmount.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtAmount.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.TxtAmount.Value = "=Amount";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.2022806406021118));
            this.textBox2.Name = "textBox9";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.19771909713745117));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox2.Value = ":";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.3999996185302734));
            this.textBox8.Name = "textBox9";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox8.Value = ":";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3999999761581421), Telerik.Reporting.Drawing.Unit.Inch(1.6000785827636719));
            this.textBox11.Name = "textBox9";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844), Telerik.Reporting.Drawing.Unit.Inch(0.19992129504680634));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox11.Value = ":";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.7958332896232605), Telerik.Reporting.Drawing.Unit.Inch(2.3999998569488525));
            this.textBox19.Name = "textBox9";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.095754623413085938), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox19.Value = ":";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5), Telerik.Reporting.Drawing.Unit.Inch(1.4000791311264038));
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.9956762790679932), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtUserName.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtUserName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.TxtUserName.Value = "";
            // 
            // textBox22
            // 
            this.textBox22.Format = ",{0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.3000006675720215), Telerik.Reporting.Drawing.Unit.Inch(3.0930557250976562));
            this.textBox22.Name = "TxtUserName";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1802746057510376), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Value = "=PaymentDate";
            // 
            // textBox23
            // 
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.79583334922790527), Telerik.Reporting.Drawing.Unit.Inch(2.6000003814697266));
            this.textBox23.Name = "textBox9";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.09992130845785141), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox23.Value = ":";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Format = "{0:dd-MMM-yyyy}";
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(3.0930557250976562));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999210119247437), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCityRS.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.1000800132751465), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05));
            this.textBox24.Name = "textBox1";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.38023534417152405), Telerik.Reporting.Drawing.Unit.Inch(0.28958341479301453));
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Style.Visible = false;
            this.textBox24.Value = "=Status";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699);
            this.pageFooter.Name = "pageFooter";
            // 
            // PaymentReturnReceipt
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.Name = "PaymentReturnReceipt";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.699999988079071);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.5), Telerik.Reporting.Drawing.Unit.Inch(5.5));
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.4802756309509277);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox txtTotalAmountInWords;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.TextBox TxtAmount;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox TxtUserName;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox textBox24;
    }
}