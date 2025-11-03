namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.BillingCharges.Billing
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class DepositStatement
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.NamaKaSubKeuangan = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox33 = new Telerik.Reporting.TextBox();
            this.textBox34 = new Telerik.Reporting.TextBox();
            this.textBox35 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox37 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.reportHeaderSection1 = new Telerik.Reporting.ReportHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.TxtNameRS = new Telerik.Reporting.TextBox();
            this.TxtCityRS = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.TxtCity = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox1
            // 
            this.textBox1.Format = "";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05), Telerik.Reporting.Drawing.Unit.Inch(1.9000000953674316));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.2999610900878906), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox1.Value = "=\'Penanggung Jawab Pasien a/n \' + SRTitle +\" \"+ PatientName + \" [\" + MedicalNo +\"" +
                "]\"";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(1.4999212026596069));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox2.Value = "Kepada Yth.";
            // 
            // textBox5
            // 
            this.textBox5.Format = "";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8519187809433788E-05), Telerik.Reporting.Drawing.Unit.Inch(2.100078821182251));
            this.textBox5.Name = "textBox4";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4000396728515625), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox5.Value = "=\'Alamat : \' + StreetName + \" \" + City";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5999603271484375), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox7.Value = "Perihal : Pemberitahuan";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(3));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox8.Value = "Dengan hormat,";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:dd MMM yyyy}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.2000398635864258));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.9000000953674316), Telerik.Reporting.Drawing.Unit.Inch(0.19996054470539093));
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox9.Value = "Harap Saudara ke kasir untuk melakukan pembayaran deposit (DP) perawatan         " +
                "       ";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003938674926758);
            this.detail.Name = "detail";
            this.detail.Style.Font.Name = "Tahoma";
            this.detail.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            this.detail.Style.Visible = false;
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00015703837561886758), Telerik.Reporting.Drawing.Unit.Inch(1.8999608755111694));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.2998433113098145), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox15.Value = "Demikian surat pemberitahuan ini kami sampaikan, atas perhatiannya diucapkan teri" +
                "ma kasih.";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05), Telerik.Reporting.Drawing.Unit.Inch(2.699960470199585));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.8999605178833008), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox16.Value = "Penanggung jawab Pasien";
            // 
            // NamaKaSubKeuangan
            // 
            this.NamaKaSubKeuangan.CanGrow = false;
            this.NamaKaSubKeuangan.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5000004768371582), Telerik.Reporting.Drawing.Unit.Inch(3.7999591827392578));
            this.NamaKaSubKeuangan.Name = "NamaKaSubKeuangan";
            this.NamaKaSubKeuangan.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.80007803440094), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.NamaKaSubKeuangan.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.NamaKaSubKeuangan.Value = "textBox23";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(4.600039005279541);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox16,
            this.NamaKaSubKeuangan,
            this.textBox29,
            this.textBox30,
            this.textBox18,
            this.textBox25,
            this.textBox6,
            this.textBox11,
            this.textBox12,
            this.textBox14,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.textBox23,
            this.textBox26,
            this.shape1,
            this.textBox13,
            this.textBox33,
            this.textBox34,
            this.textBox35,
            this.textBox36,
            this.textBox37,
            this.textBox4});
            this.reportFooterSection1.Name = "reportFooterSection1";
            this.reportFooterSection1.Style.Font.Name = "Tahoma";
            this.reportFooterSection1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12);
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.39996084570884705));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox29.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox29.Value = "1. Kelas III";
            // 
            // textBox30
            // 
            this.textBox30.CanGrow = false;
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4999222755432129), Telerik.Reporting.Drawing.Unit.Inch(2.699960470199585));
            this.textBox30.Name = "textBox18";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.80007803440094), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox30.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox30.Value = "Staff Perbendaharaan";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00011793772137025371), Telerik.Reporting.Drawing.Unit.Inch(3.9999606609344482));
            this.textBox18.Name = "textBox16";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox18.Value = "Tembusan :";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(4.4000391960144043));
            this.textBox25.Name = "textBox16";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0999605655670166), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox25.Value = "2. Pertinggal";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.60003918409347534));
            this.textBox6.Name = "textBox29";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox6.Value = "2. Kelas II";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.80011749267578125));
            this.textBox11.Name = "textBox29";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.19976456463336945));
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox11.Value = "3. Kelas I";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347));
            this.textBox12.Name = "textBox29";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5998821258544922), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox12.Value = "4. ICU/ICCU";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(1.2000783681869507));
            this.textBox14.Name = "textBox29";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.4000003337860107), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox14.Value = "5. Deposito Perawatan + Obat";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.09999942779541), Telerik.Reporting.Drawing.Unit.Inch(0.39996084570884705));
            this.textBox19.Name = "textBox29";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Value = "";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.09999942779541), Telerik.Reporting.Drawing.Unit.Inch(0.60003918409347534));
            this.textBox20.Name = "textBox29";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Value = "";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.09999942779541), Telerik.Reporting.Drawing.Unit.Inch(0.80011749267578125));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Value = "";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(1.0001958608627319));
            this.textBox23.Name = "textBox21";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Value = "";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(1.2002741098403931));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2000001668930054), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Value = "";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.8999214172363281));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000785827636719), Telerik.Reporting.Drawing.Unit.Inch(0.052162487059831619));
            this.shape1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.shape1.Style.LineStyle = Telerik.Reporting.Drawing.LineStyle.Dotted;
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = false;
            this.textBox13.Format = "{0}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2999229431152344), Telerik.Reporting.Drawing.Unit.Inch(1.2000783681869507));
            this.textBox13.Name = "textBox26";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.000077486038208), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Mm(1);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Value = "(Pasien VIP + Super VIP)";
            // 
            // textBox33
            // 
            this.textBox33.Format = "{0}";
            this.textBox33.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.39996084570884705));
            this.textBox33.Name = "textBox29";
            this.textBox33.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.399920791387558), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox33.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox33.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox33.Value = "Rp.";
            // 
            // textBox34
            // 
            this.textBox34.Format = "{0}";
            this.textBox34.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.59996098279953));
            this.textBox34.Name = "textBox29";
            this.textBox34.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.399920791387558), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox34.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox34.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox34.Value = "Rp.";
            // 
            // textBox35
            // 
            this.textBox35.Format = "{0}";
            this.textBox35.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.79996109008789062));
            this.textBox35.Name = "textBox29";
            this.textBox35.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.399920791387558), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox35.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox35.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox35.Value = "Rp.";
            // 
            // textBox36
            // 
            this.textBox36.Format = "{0}";
            this.textBox36.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(0.99996060132980347));
            this.textBox36.Name = "textBox29";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.399920791387558), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox36.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.Value = "Rp.";
            // 
            // textBox37
            // 
            this.textBox37.Format = "{0}";
            this.textBox37.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000000476837158), Telerik.Reporting.Drawing.Unit.Inch(1.1999607086181641));
            this.textBox37.Name = "textBox29";
            this.textBox37.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.399920791387558), Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672));
            this.textBox37.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox37.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox37.Value = "Rp.";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(4.1999607086181641));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.1000003814697266), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox4.Value = "textBox4";
            // 
            // reportHeaderSection1
            // 
            this.reportHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949);
            this.reportHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox1,
            this.textBox5,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox22,
            this.TxtNameRS,
            this.TxtCityRS,
            this.textBox3,
            this.textBox31,
            this.shape2,
            this.textBox28,
            this.TxtCity,
            this.textBox10});
            this.reportHeaderSection1.Name = "reportHeaderSection1";
            this.reportHeaderSection1.Style.Font.Name = "Tahoma";
            // 
            // textBox22
            // 
            this.textBox22.Format = ",{0:dd-MMM-yyyy}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9000792503356934), Telerik.Reporting.Drawing.Unit.Inch(0.70000028610229492));
            this.textBox22.Name = "textBox2";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.5000395774841309), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox22.Style.Font.Name = "Microsoft Sans Serif";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox22.Value = "=now()";
            // 
            // TxtNameRS
            // 
            this.TxtNameRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8519187809433788E-05), Telerik.Reporting.Drawing.Unit.Inch(0));
            this.TxtNameRS.Name = "TxtNameRS";
            this.TxtNameRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4999232292175293), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.TxtNameRS.Style.Font.Bold = true;
            this.TxtNameRS.Style.Font.Name = "Tahoma";
            this.TxtNameRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16);
            this.TxtNameRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtNameRS.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtNameRS.Value = "";
            // 
            // TxtCityRS
            // 
            this.TxtCityRS.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582));
            this.TxtCityRS.Name = "TxtCityRS";
            this.TxtCityRS.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.4999232292175293), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.TxtCityRS.Style.Font.Name = "Tahoma";
            this.TxtCityRS.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16);
            this.TxtCityRS.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.TxtCityRS.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.TxtCityRS.Value = "";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8519187809433788E-05), Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633));
            this.textBox3.Name = "textBox11";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.8999214172363281), Telerik.Reporting.Drawing.Unit.Inch(0.25566211342811584));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox3.Value = "adapun biaya yang harus dibayar sbb :";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00011793772137025371), Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263));
            this.textBox31.Name = "textBox2";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox31.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox31.Value = "Saudara ";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.00011793772137025371), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(7.49988317489624), Telerik.Reporting.Drawing.Unit.Inch(0.052162487059831619));
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(3.4000790119171143));
            this.textBox28.Name = "textBox8";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89999991655349731), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox28.Value = "di ruangan: ";
            // 
            // TxtCity
            // 
            this.TxtCity.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8000392913818359), Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582));
            this.TxtCity.Name = "TxtCity";
            this.TxtCity.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0999608039855957), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179));
            this.TxtCity.Style.Font.Name = "Microsoft Sans Serif";
            this.TxtCity.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.TxtCity.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.TxtCity.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.90007877349853516), Telerik.Reporting.Drawing.Unit.Inch(3.4000790119171143));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4999215602874756), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985));
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11);
            this.textBox10.Value = "";
            // 
            // DepositStatement
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.reportFooterSection1,
            this.reportHeaderSection1});
            this.Name = "DepositStatement";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Inch(0.5);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.Font.Name = "Tahoma";
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.5000014305114746);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox NamaKaSubKeuangan;
        private ReportFooterSection reportFooterSection1;
        private ReportHeaderSection reportHeaderSection1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox TxtNameRS;
        private Telerik.Reporting.TextBox TxtCityRS;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox31;
        private Shape shape2;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox26;
        private Shape shape1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox33;
        private Telerik.Reporting.TextBox textBox34;
        private Telerik.Reporting.TextBox textBox35;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox37;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox TxtCity;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox10;
    }
}