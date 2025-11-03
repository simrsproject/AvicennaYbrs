using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Control untuk display fluid balance
    /// </summary>
    /// Created by: Handono 2019
    /// 
    public partial class FluidBalanceCtl : System.Web.UI.UserControl
    {
        protected virtual List<string> MergeRegistrations
        {
            get
            {
                return AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            grdFluidBalance.Height = GridHeight;
        }
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        public int GridHeight
        {
            get
            {
                var height = ViewState["gridheight"].ToInt();
                if (height == 0) return 500;
                return height;
            }
            set { ViewState["gridheight"] = value; }
        }
        public bool IsModeHistory
        {
            set { grdFluidBalance.Columns[0].Visible = !value; }
        }
        public string GridFluidBalanceClientID
        {
            get { return grdFluidBalance.ClientID; }
        }

        public string RegistrationNoClientID
        {
            get { return txtRegistrationNo.ClientID; }
        }
        public string SequencegNoClientID
        {
            get { return txtSequenceNo.ClientID; }
        }

        public void Rebind(string registrationNo, int seqNo)
        {
            txtRegistrationNo.Text = registrationNo;
            txtSequenceNo.Text = seqNo.ToString();
            grdFluidBalance.Rebind();
        }


        #region Fluid Balance
        protected void grdFluidBalance_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {

            }
            else if (e.CommandName == "Rebind")
            {
            }
        }

        protected void grdFluidBalance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdFluidBalanceRebind();
            grdSchemaInfusRebind();
            trSchemaInfusOldVersion.Visible = !string.IsNullOrEmpty(txtFluidBalSchemaInfus.Text);
        }
        private void grdFluidBalanceRebind()
        {
            grdFluidBalance.DataSource = null;

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            var ageInYear = 0;
            var ageInMonth = 0;
            var ageInDay = 0;

            var qr = new PatientFluidBalanceQuery("a");
            var fb = new PatientFluidBalance();

            if (pat.DateOfBirth != null)
            {

                if (txtSequenceNo.Text.ToInt() == 0)
                {
                    // Tampilkan yg terakhir diedit
                    qr.Where(qr.RegistrationNo.In(MergeRegistrations));
                    qr.es.Top = 1;
                    qr.OrderBy(qr.LastUpdateDateTime.Descending);
                }
                else
                {
                    qr.Where(qr.RegistrationNo == txtRegistrationNo.Text, qr.SequenceNo == txtSequenceNo.Text.ToInt());
                }


                if (fb.Load(qr))
                {
                    txtRegistrationNo.Text = fb.RegistrationNo;
                    txtSequenceNo.Text = fb.SequenceNo.ToString();

                    txtFluidBalInOutDate.Text = fb.InOutDate.Value.ToString(AppConstant.DisplayFormat.Date);
                    txtFluidBalSeqNo.Value = fb.SequenceNo;
                    txtFluidBalSchemaInfus.Text = fb.SchemaInfus;
                    txtBodyWeight.Value = fb.BodyWeight.ToDouble();

                    var dob = pat.DateOfBirth.Value.Date;
                    ageInYear = Helper.GetAgeInYear(dob, fb.InOutDate.Value.Date);
                    ageInMonth = Helper.GetAgeInMonth(dob, fb.InOutDate.Value.Date);
                    ageInDay = Helper.GetAgeInDay(dob, fb.InOutDate.Value.Date);

                    txtAge.Text = string.Format("{0}Y {1}M {2}D", ageInYear, ageInMonth, ageInDay);
                }


            }

            // Tetap Refresh
            var dtb = FluidBalanceDataTable(fb.RegistrationNo ?? string.Empty, fb.SequenceNo ?? 0, fb.InOutDate ?? DateTime.Now);
            // Tambah kolom 
            dtb.Columns.Add("CummulativeQtyIn", typeof(System.Decimal));
            dtb.Columns.Add("CummulativeQtyOut", typeof(System.Decimal));
            dtb.Columns.Add("SchemaInfusBalance", typeof(System.Decimal));

            // Hitung total
            double totalIn = 0;
            double totalOut = 0;
            double totalUrine = 0;
            if (dtb.Rows.Count > 0)
            {
                var infBalQ = new PatientFluidBalanceSchemaInfusQuery();
                infBalQ.Where(infBalQ.RegistrationNo == fb.RegistrationNo, infBalQ.SequenceNo == (fb.SequenceNo ?? 0));
                infBalQ.Select(infBalQ.SchemaInfusNo, infBalQ.QtyVolume);
                var dtbInfusBal = infBalQ.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    totalIn += row["InfusQtyIn"].ToDouble() + row["DrugQtyIn"].ToDouble() + row["OralQtyIn"].ToDouble() + row["OtherInQtyIn"].ToDouble();
                    totalOut += row["UrineQty"].ToDouble() + row["DefecateQty"].ToDouble() + row["GagQty"].ToDouble() + row["BleedingQty"].ToDouble() + row["DrainQty"].ToDouble();
                    totalUrine += row["UrineQty"].ToDouble();

                    row["CummulativeQtyIn"] = totalIn;
                    row["CummulativeQtyOut"] = totalOut;

                    // Infus Balance
                    foreach (DataRow rowIB in dtbInfusBal.Rows)
                    {
                        if (rowIB["SchemaInfusNo"].Equals(row["SchemaInfusNo"]))
                        {
                            rowIB["QtyVolume"] = rowIB["QtyVolume"].ToInt() - row["InfusQtyIn"].ToInt();
                            row["SchemaInfusBalance"] = rowIB["QtyVolume"];
                        }
                    }
                }
            }
            txtFluidBalQtyIn.Value = totalIn;
            txtFluidBalQtyOut.Value = totalOut;

            // IWL
            var iwl = Iwl(fb, totalIn, ageInYear, ageInMonth, ageInDay);
            txtFluidBalIwlQty.Value = iwl;

            txtFluidBalBalanceQty.Value = totalIn - iwl - totalOut;

            // Diuresis (urien/bb/jam kerja)
            var iwlForHour = fb.IwlForHour.ToDouble() == 0 ? 24 : fb.IwlForHour.ToDouble();
            if ((fb.BodyWeight.ToDouble() > 0) && (iwlForHour > 0))
                txtDiuresis.Value = totalUrine / fb.BodyWeight.ToDouble() / iwlForHour;
            else
                txtDiuresis.Value = 0;

            grdFluidBalance.DataSource = dtb;
        }

        private double Iwl(PatientFluidBalance fb, double totalIn, int ageInYear, int ageInMonth, int ageInDay)
        {
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsIwlUsingTemperature))
                return IwlUsingTemperature(fb, totalIn, ageInYear, ageInMonth, ageInDay);
            else
                return IwlNotUsingTemperature(fb, totalIn, ageInYear, ageInMonth, ageInDay);
        }
        private double IwlUsingTemperature(PatientFluidBalance fb, double totalIn, int ageInYear, int ageInMonth, int ageInDay)
        {
            // IWL menggunakan parameter suhu
            double ccQty = 0;
            var bw = fb.BodyWeight.ToDouble();

            if (fb.IwlConstant == 0) 
            {
                if (ageInYear > 14 || (ageInYear == 14 && (ageInMonth > 0 || ageInDay > 0)))
                {
                    ccQty = 15;
                }
                else if (ageInYear >= 5 && ageInYear <= 14)
                {
                    ccQty = 30;
                }
                else if (ageInYear < 5)
                {
                    ccQty = 30;
                }
                else
                    ccQty = 40;
            }
            else
            {
                ccQty = fb.IwlConstant.ToDouble();
            }

            var normalTemp = fb.NormalTemp.ToDouble() == 0
    ? AppParameter.GetParameterValue(AppParameter.ParameterItem.NormalTemperature).ToDouble()
    : fb.NormalTemp.ToDouble();

            var iwlForHour = fb.IwlForHour.ToDouble() == 0 ? 24 : fb.IwlForHour.ToDouble();

            // IWL Normal
            var iwlNormalPerHour = (ccQty * bw) / 24;
            var iwlNormal = iwlNormalPerHour * iwlForHour;

            // IWL Kenaikan Suhu
            var difTemp = fb.LastTemp.ToDouble() - normalTemp;
            var iwlAddPerHour = (((0.1 * totalIn) * difTemp) / 24);
            var iwlAdditional = iwlAddPerHour * iwlForHour;

            var iwlTotal = iwlNormal + iwlAdditional;
            return iwlTotal;
        }

        private double IwlNotUsingTemperature(PatientFluidBalance fb, double totalIn, int ageInYear, int ageInMonth, int ageInDay)
        {
            // RS Utama
            var bw = fb.BodyWeight.ToDouble();
            var stdIwl = 0;

            if (ageInYear > 14 || (ageInYear == 14 && (ageInMonth > 0 || ageInDay > 0)))
            {
                stdIwl = 15;
            }
            else if (ageInYear >= 5 && ageInYear <= 14)
            {
                stdIwl = 20;
            }
            else if (ageInYear < 5)
            {
                stdIwl = 30;
            }
            else
                stdIwl = 40;

            var iwlForHour = fb.IwlForHour.ToDouble() == 0 ? 24 : fb.IwlForHour.ToDouble();

            return (stdIwl * bw * iwlForHour) / 24;
        }

        protected void grdFluidBalance_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var regNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientFluidBalanceDetailMetadata.ColumnNames.RegistrationNo].ToString();
            var seqNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientFluidBalanceDetailMetadata.ColumnNames.SequenceNo].ToInt();
            var detNo = item.OwnerTableView.DataKeyValues[item.ItemIndex][PatientFluidBalanceDetailMetadata.ColumnNames.DetailSequenceNo].ToInt();
            var fb = new PatientFluidBalanceDetail();
            if (fb.LoadByPrimaryKey(regNo, seqNo, detNo))
            {
                fb.MarkAsDeleted();
                fb.Save();
            }

            grdFluidBalance.Rebind();
        }

        private DataTable FluidBalanceDataTable(string registrationNo, int sequenceNo, DateTime fluidBalanceDate)
        {
            var query = new PatientFluidBalanceDetailQuery("a");
            var user = new AppUserQuery("u");
            query.InnerJoin(user).On(query.CreatedByUserID == user.UserID);
            var schemaInfus = new PatientFluidBalanceSchemaInfusQuery("si");
            query.LeftJoin(schemaInfus).On(schemaInfus.RegistrationNo == registrationNo & schemaInfus.SequenceNo == sequenceNo & query.SchemaInfusNo == schemaInfus.SchemaInfusNo);
            query.Select(
                query.RegistrationNo,
                query.SequenceNo,
                query.DetailSequenceNo,
                query.InOutDateTime,
                query.CreatedByUserID,
                query.SchemaInfusNo,

                @"<SchemaInfusInitQty = COALESCE(si.QtyVolume, 0),
'1. Morning' as TimeGroup,
       InfusName = CASE 
                        WHEN a.SRFluidInOutMethod = 'INF' THEN CASE WHEN COALESCE(si.SchemaInfusName, '')>'' AND a.FluidName>'' THEN si.SchemaInfusName + ' ('+ a.FluidName+')' ELSE COALESCE(si.SchemaInfusName, a.FluidName) END 
                        ELSE            ''
                   END,
       InfusQtyIn = CASE 
                         WHEN a.SRFluidInOutMethod = 'INF' THEN a.InOutQty
                         ELSE 0
                    END,

       DrugName = CASE 
                        WHEN a.SRFluidInOutMethod = 'DRG' THEN a.FluidName
                        ELSE            ''
                   END,
       DrugQtyIn = CASE 
                         WHEN a.SRFluidInOutMethod = 'DRG' THEN a.InOutQty
                         ELSE 0
                    END,
       OralName = CASE 
                       WHEN a.SRFluidInOutMethod = 'ORL' THEN a.FluidName
                       ELSE             ''
                  END,
       OralQtyIn = CASE 
                        WHEN a.SRFluidInOutMethod = 'ORL' THEN a.InOutQty
                        ELSE 0
                   END,
       OtherInName = CASE 
                       WHEN a.SRFluidInOutMethod = 'OTI' THEN a.FluidName
                       ELSE             ''
                  END,
       OtherInQtyIn = CASE 
                        WHEN a.SRFluidInOutMethod = 'OTI' THEN a.InOutQty
                        ELSE 0
                   END,

       UrineQty = CASE 
                       WHEN a.SRFluidInOutMethod = 'URN' THEN a.InOutQty
                       ELSE 0
                  END,
       DefecateQty = CASE 
                     WHEN a.SRFluidInOutMethod = 'BAB' THEN a.InOutQty
                     ELSE 0
                END,
       GagQty = CASE 
                     WHEN a.SRFluidInOutMethod = 'GAG' THEN a.InOutQty
                     ELSE 0
                END,

       BleedingQty = CASE 
                          WHEN a.SRFluidInOutMethod = 'BLD' THEN a.InOutQty
                          ELSE 0
                     END,
       DrainQty = CASE 
                       WHEN a.SRFluidInOutMethod = 'DRN' THEN a.InOutQty
                       ELSE 0
                  END,

       FluidOutDescription = CASE 
                               WHEN CHARINDEX(a.SRFluidInOutMethod, 'URN GAG BAB BLD DRH DRN', 0) > 0 THEN a.FluidName
                               ELSE     ''
                          END>", user.UserName
                );
            query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);

            query.OrderBy(query.InOutDateTime.Ascending);

            var dtb = query.LoadDataTable();

            dtb.Columns.Add("InOutDate", typeof(DateTime));


            // Set Shift
            // RSMM
            // 07 - 14 shift pagi
            // 14 - 20 shift siang
            // 21 - 07 shift malam

            var morning = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartMorning);
            var afternoon = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartAfternoon);
            var night = AppParameter.GetParameterValue(AppParameter.ParameterItem.ShiftStartNight);

            // Hitung pengurang waktu supaya putaran shift dalam satu tgl (trims ide brilliant nya bangTe)
            var arr = morning.Split(':');
            var minuteFixed = 0 - ((arr[0].ToInt() * 60) + arr[1].ToInt()); // Hitung pengurang spy jadi jam 00:00

            foreach (DataRow row in dtb.Rows)
            {
                var date = Convert.ToDateTime(row["InOutDateTime"]);
                var totalMinutes = date.TimeOfDay.TotalMinutes;

                var startMorning = TotalMinutes(morning, date);
                var startAfternoon = TotalMinutes(afternoon, date);
                var startNight = TotalMinutes(night, date);

                if (totalMinutes >= startMorning && totalMinutes < startAfternoon)
                {
                    row["TimeGroup"] = "1. Morning";
                }
                else if (totalMinutes >= startAfternoon && totalMinutes < startNight)
                {
                    row["TimeGroup"] = "2. Afternoon";
                }
                else
                {
                    row["TimeGroup"] = "3. Night";
                }

                // Buat supaya shift dalam 1 tgl supaya tergrouping yg beda tgl
                row["InOutDate"] = date.AddMinutes(minuteFixed).Date;
            }
            return dtb;
        }

        private static double TotalMinutes(string shiftTime, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(shiftTime) || !shiftTime.Contains(':')) return date.Date.TimeOfDay.TotalMinutes;

            var arr = shiftTime.Split(':');
            var minute = (arr[0].ToInt() * 60) + arr[1].ToInt();

            return date.Date.AddMinutes(minute).TimeOfDay.TotalMinutes;
        }

        #endregion


        protected void grdFluidBalance_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var zeroText = "0.00";

                foreach (TableCell itemCell in e.Item.Cells)
                {
                    if (itemCell.Text == zeroText)
                        itemCell.Text = string.Empty;
                }
            }
        }

        protected bool IsMonitoringEditable(GridItem gridItem)
        {
            var dataItem = gridItem.DataItem;
            if (!(DataBinder.Eval(dataItem, "CreatedByUserID").Equals(AppSession.UserLogin.UserID)))
                return false;

            // Batas waktu
            if (Helper.IsDeadlineEditedOver(Convert.ToDateTime(DataBinder.Eval(dataItem, "InOutDateTime"))))
                return false;

            return true;
        }
        protected bool IsMonitoringDeleteable(GridItem gridItem)
        {
            return IsMonitoringEditable(gridItem);
        }

        protected string ScriptMonitoringEdit(GridItem gridItem)
        {
            var dataItem = gridItem.DataItem;
            return !IsMonitoringEditable(gridItem)
                ? string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />", Helper.UrlRoot())
                : string.Format("<a href=\"#\" onclick=\"javascript:entryMonitoringDetail('edit', '{0}','{1}','{2}'); return false;\"><img src=\"{3}/Images/Toolbar/edit16.png\"  alt=\"Edit\" /></a>", DataBinder.Eval(dataItem, "RegistrationNo"), DataBinder.Eval(dataItem, "SequenceNo"), DataBinder.Eval(dataItem, "DetailSequenceNo"), Helper.UrlRoot());
        }

        //protected void grdSchemaInfus_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        private void grdSchemaInfusRebind()
        {
            if (txtSequenceNo.Text.ToInt() == 0)
            {
                grdSchemaInfus.DataSource = string.Empty;
                grdSchemaInfus.DataBind();
                return;
            }
            var coll = new PatientFluidBalanceSchemaInfusCollection();
            var query = new PatientFluidBalanceSchemaInfusQuery("a");

            query.Where(query.RegistrationNo == txtRegistrationNo.Text, query.SequenceNo == txtSequenceNo.Text.ToInt());
            coll.Load(query);

            grdSchemaInfus.DataSource = coll;
            grdSchemaInfus.DataBind();
        }

    }
}