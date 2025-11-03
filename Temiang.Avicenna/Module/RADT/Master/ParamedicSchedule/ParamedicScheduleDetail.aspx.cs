using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicScheduleDetail : BasePageDetail
    {
        private void SetEntityValue(ParamedicSchedule entity, ParamedicScheduleDateCollection collDetail)
        {
            entity.PeriodYear = cboPeriodYear.SelectedValue;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.ParamedicID = cboParamedicID.SelectedValue;
            entity.ExamDuration = Convert.ToInt32(txtExamDuration.Value);
            entity.Quota = Convert.ToInt16(txtQuota.Value);
            entity.QuotaOnline = Convert.ToInt16(txtQuotaOnline.Value);
            entity.QuotaBpjs = Convert.ToInt16(txtQuotaBpjs.Value);
            entity.QuotaBpjsOnline = Convert.ToInt16(txtQuotaBpjsOnline.Value);
            entity.Notes = txtNotes.Text;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            //UserGroupProgram
            collDetail.Query.Where
                (
                    collDetail.Query.PeriodYear == cboPeriodYear.SelectedValue &
                    collDetail.Query.ParamedicID == cboParamedicID.SelectedValue
                );
            collDetail.LoadAll();

            DataTable dtbEdited = ParamedicScheduleDates;
            string serviceUnitID = cboServiceUnitID.SelectedValue;
            string paramedicID = cboParamedicID.SelectedValue;
            string year = cboPeriodYear.SelectedValue;

            //New and Updated
            foreach (DataRow row in dtbEdited.Rows)
            {
                DateTime date = (row.RowState == DataRowState.Added) ? Convert.ToDateTime(row["ScheduleDate"]) : Convert.ToDateTime(row["ScheduleDate", DataRowVersion.Original]);
                ParamedicScheduleDate item = collDetail.FirstOrDefault(itemSearch => itemSearch.ServiceUnitID == serviceUnitID && itemSearch.ParamedicID == paramedicID && itemSearch.PeriodYear == year && itemSearch.ScheduleDate == date);

                //ParamedicScheduleDate item = collDetail.FindByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue,
                //    cboPeriodYear.SelectedValue, Convert.ToDateTime(row["ScheduleDate", DataRowVersion.Original]));

                if (row.RowState == DataRowState.Deleted)
                {
                    if (item != null)
                        item.MarkAsDeleted();

                    continue;
                }

                if (item == null)
                {
                    item = collDetail.AddNew();
                    item.ServiceUnitID = cboServiceUnitID.SelectedValue;
                    item.ParamedicID = cboParamedicID.SelectedValue;
                    item.PeriodYear = cboPeriodYear.SelectedValue;
                    item.ScheduleDate = Convert.ToDateTime(row["ScheduleDate"]);
                    item.IsIpr = Convert.ToBoolean(row["IsIpr"]);
                    item.IsOpr = Convert.ToBoolean(row["IsOpr"]);
                    item.IsEmr = Convert.ToBoolean(row["IsEmr"]);
                }

                item.OperationalTimeID = Convert.ToString(row["OperationalTimeID"]);

                item.IsClosedTime1 = false;
                item.IsClosedTime2 = false;
                item.IsClosedTime3 = false;
                item.IsClosedTime4 = false;
                item.IsClosedTime5 = false;

                if (item.es.IsAdded)
                {
                    item.CreatedByUserID = AppSession.UserLogin.UserID;
                    item.CreatedDateTime = DateTime.Now;
                }

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                if (item.PeriodYear != item.ScheduleDate.Value.Year.ToString())
                    item.MarkAsDeleted();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            ParamedicScheduleQuery que = new ParamedicScheduleQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.ParamedicID > cboParamedicID.SelectedValue);
                que.OrderBy(que.PeriodYear.Ascending);
            }
            else
            {
                que.Where(que.PeriodYear == cboPeriodYear.SelectedValue & que.ParamedicID < cboParamedicID.SelectedValue);
                que.OrderBy(que.PeriodYear.Descending);
            }

            ParamedicSchedule entity = new ParamedicSchedule();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ParamedicScheduleSearch.aspx";
            UrlPageList = "ParamedicScheduleList.aspx";

            ProgramID = AppConstant.Program.ParamedicSchedule;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                int year = DateTime.Now.Year;
                for (int i = 0; i < 15; i++)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem((year - 10 + i).ToString(), (year - 10 + i).ToString()));
                }

                Session.Remove("collOperationalTime");

                pnlQuota.Visible = AppSession.Parameter.IsUsingLimitQuotaInPhysicianSchedule;

                var ots = new OperationalTimeCollection();
                ots.Query.OrderBy(ots.Query.StartTime1.Ascending);
                ots.Query.Load();

                cboSenin.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSelasa.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboRabu.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboKamis.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboJumat.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboSabtu.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                cboMinggu.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                foreach (var item in ots)
                {
                    cboSenin.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                    cboSelasa.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                    cboRabu.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                    cboKamis.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                    cboJumat.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                    cboSabtu.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [ {item.OperationalTimeName}]", item.OperationalTimeID));
                    cboMinggu.Items.Add(new RadComboBoxItem($"{item.OperationalTimeID} [{item.OperationalTimeName}]", item.OperationalTimeID));
                }
            }

            AjaxPanel.AjaxRequest += new RadAjaxControl.AjaxRequestDelegate(AjaxPanel_AjaxRequest);
        }

        /// <summary>
        /// Dijalankan dari javascript var ajxPanel=$find("<%= AjaxPanel.ClientID %>");ajxPanel.ajaxRequest('parameternya');
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "cldSchedule")
            {
                //Not todo just callback for refresh calendar
            }
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            ParamedicSchedule entity = new ParamedicSchedule();
            if (parameters.Length > 0)
            {
                String periodYear = (String)parameters[2];
                String paramedicID = (String)parameters[1];
                String clusterID = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(clusterID, paramedicID, periodYear);
            }
            else
            {
                entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, cboPeriodYear.SelectedValue);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            ParamedicSchedule paramedicScheduleHd = (ParamedicSchedule)entity;

            if (!string.IsNullOrEmpty(paramedicScheduleHd.ServiceUnitID))
            {
                var query = new ServiceUnitQuery("a");
                query.Where(query.ServiceUnitID == paramedicScheduleHd.ServiceUnitID);
                DataTable dtb = query.LoadDataTable();

                cboServiceUnitID.DataSource = dtb;
                cboServiceUnitID.DataBind();

                cboServiceUnitID.SelectedValue = paramedicScheduleHd.ServiceUnitID;
            }
            else
            {
                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.SelectedValue = string.Empty;
                cboServiceUnitID.Text = string.Empty;
            }

            cboPeriodYear.SelectedValue = paramedicScheduleHd.PeriodYear;

            if (!string.IsNullOrEmpty(paramedicScheduleHd.ParamedicID))
            {
                ParamedicQuery query = new ParamedicQuery("a");
                query.Where(query.ParamedicID == paramedicScheduleHd.ParamedicID);
                DataTable dtb = query.LoadDataTable();

                cboParamedicID.DataSource = dtb;
                cboParamedicID.DataBind();

                cboParamedicID.SelectedValue = paramedicScheduleHd.ParamedicID;
            }
            else
            {
                cboParamedicID.Items.Clear();
                cboParamedicID.SelectedValue = string.Empty;
                cboParamedicID.Text = string.Empty;
            }

            txtExamDuration.Value = paramedicScheduleHd.ExamDuration ?? 0;
            txtQuota.Value = paramedicScheduleHd.Quota ?? 0;
            txtQuotaOnline.Value = paramedicScheduleHd.QuotaOnline ?? 0;
            txtQuotaBpjs.Value = paramedicScheduleHd.QuotaBpjs ?? 0;
            txtQuotaBpjsOnline.Value = paramedicScheduleHd.QuotaBpjsOnline ?? 0;
            txtNotes.Text = paramedicScheduleHd.Notes;

            SetRangeDateSchedule(Convert.ToInt16(paramedicScheduleHd.PeriodYear));

            //Reset
            ParamedicScheduleDates = null;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            ParamedicSchedule rec = new ParamedicSchedule();
            rec.PeriodYear = DateTime.Now.Year.ToString();
            OnPopulateEntryControl(rec);

            cboServiceUnitID.SelectedValue = string.Empty;
            cboServiceUnitID.Text = string.Empty;
            cboParamedicID.Items.Clear();
            cboParamedicID.SelectedValue = string.Empty;
            cboParamedicID.Text = string.Empty;
        }

        protected override void OnMenuEditClick()
        {
            cboPeriodYear.Enabled = false;
            cboServiceUnitID.Enabled = false;
            cboParamedicID.Enabled = false;
            txtExamDuration.ReadOnly = true;
        }

        private void SetRangeDateSchedule(int year)
        {
            if (year == 0)
                return;

            cldSchedule.RangeMinDate = new DateTime(year, 1, 1);
            cldSchedule.RangeMaxDate = new DateTime(year, 12, 31);
            cldSchedule.FocusedDate = new DateTime(year, 1, 1);
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("ParamedicID='{0}' AND PeriodYear='{1}'", cboPeriodYear.SelectedValue, cboParamedicID.SelectedValue);
            auditLogFilter.TableName = "ParamedicSchedule";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            btnChangeSchedule.Enabled = (newVal == AppEnum.DataMode.Edit && cboPeriodYear.Text != string.Empty && cboParamedicID.SelectedValue != string.Empty);
            btnGlobalSchedule.Enabled = (newVal == AppEnum.DataMode.Read && cboPeriodYear.Text != string.Empty && cboParamedicID.SelectedValue != string.Empty);
            btnUpdateHFIS.Enabled = (newVal == AppEnum.DataMode.Edit && cboPeriodYear.Text != string.Empty && cboParamedicID.SelectedValue != string.Empty);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            ParamedicSchedule entity = new ParamedicSchedule();
            entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, cboPeriodYear.SelectedValue);
            entity.MarkAsDeleted();

            ParamedicScheduleDateCollection collDt = new ParamedicScheduleDateCollection();
            collDt.Query.Where(
                collDt.Query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                collDt.Query.PeriodYear == cboPeriodYear.SelectedValue,
                collDt.Query.ParamedicID == cboParamedicID.SelectedValue
                );
            collDt.LoadAll();
            collDt.MarkAllAsDeleted();

            using (esTransactionScope trans = new esTransactionScope())
            {
                //Delete detil
                collDt.Save();

                //Delete Header
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (txtExamDuration.Value <= 0)
            {
                args.MessageText = "Exam duration value is invalid.";
                args.IsCancel = true;
                return;
            }
            ParamedicScheduleDateCollection collDetail = new ParamedicScheduleDateCollection();
            ParamedicSchedule entity = new ParamedicSchedule();
            entity.AddNew();
            SetEntityValue(entity, collDetail);
            SaveEntity(entity, collDetail);
        }

        private void SaveEntity(ParamedicSchedule entity, ParamedicScheduleDateCollection collDetail)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                //Save Header
                entity.Save();

                //Save Detail
                collDetail.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (txtExamDuration.Value <= 0)
            {
                args.MessageText = "Exam duration value is invalid.";
                args.IsCancel = true;
                return;
            }
            ParamedicSchedule entity = new ParamedicSchedule();
            if (entity.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, cboPeriodYear.SelectedValue))
            {
                ParamedicScheduleDateCollection collDetail = new ParamedicScheduleDateCollection();
                SetEntityValue(entity, collDetail);
                SaveEntity(entity, collDetail);
            }
        }

        #endregion

        #region Calendar

        private DataTable OperationalTimes
        {
            get
            {
                object obj = this.Session["collOperationalTime"];
                if (obj != null)
                    return ((DataTable)(obj));

                OperationalTimeQuery query;
                if (Session["queOperationalTime"] != null)
                    query = (OperationalTimeQuery)Session["queOperationalTime"];
                else
                    query = new OperationalTimeQuery();

                query.Select
                    (
                        query.OperationalTimeID,
                        query.OperationalTimeName,
                        query.OperationalTimeBackcolor,
                        (query.StartTime1 + "-" + query.EndTime1).As("Time1"),
                        (query.StartTime2 + "-" + query.EndTime2).As("Time2"),
                        (query.StartTime3 + "-" + query.EndTime3).As("Time3"),
                        (query.StartTime4 + "-" + query.EndTime4).As("Time4"),
                        (query.StartTime5 + "-" + query.EndTime5).As("Time5")
                    );
                DataTable dtb = query.LoadDataTable();
                DataRow newRow = dtb.NewRow();
                newRow["OperationalTimeID"] = "";
                newRow["OperationalTimeName"] = "Remove Schedule";
                dtb.Rows.Add(newRow);

                dtb.PrimaryKey = new DataColumn[] { dtb.Columns["OperationalTimeID"] };
                this.Session["collOperationalTime"] = dtb;
                return dtb;
            }
        }

        protected void CustomizeDay(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
            //Jika text kosong
            if (e.Cell.Text == "&#160;") return;

            TableCell currentCell = e.Cell;
            DateTime currentDate = e.Day.Date;
            DataRow drw = ParamedicScheduleDates.Rows.Find(currentDate);
            if (drw != null)
            {
                DataRow drwOT = OperationalTimes.Rows.Find(drw["OperationalTimeID"]);
                if (drwOT != null)
                {
                    currentCell.BackColor = ColorTranslator.FromHtml(drwOT["OperationalTimeBackcolor"].ToString());

                    string toolTip = drwOT["Time1"].ToString();
                    if (drwOT["Time2"].ToString().Trim() != "-")
                        toolTip += ", " + drwOT["Time2"].ToString();
                    if (drwOT["Time3"].ToString().Trim() != "-")
                        toolTip += ", " + drwOT["Time3"].ToString();
                    if (drwOT["Time4"].ToString().Trim() != "-")
                        toolTip += ", " + drwOT["Time4"].ToString();
                    if (drwOT["Time5"].ToString().Trim() != "-")
                        toolTip += ", " + drwOT["Time5"].ToString();

                    //if (Helper.IsBpjsAntrolIntegration)
                    //{
                    //    var svc = new Common.BPJS.Antrian.Service();
                    //    var response = svc.GetJadwalDokter("", currentDate.ToString("yyyy-MM-dd"));
                    //    if (response.Metadata.IsAntrolValid)
                    //    {

                    //    }
                    //}

                    currentCell.ToolTip = toolTip;
                }
            }

        }

        private DataTable ParamedicScheduleDates
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["dtbParamedicScheduleDate"];
                    if (obj != null)
                        return ((DataTable)(obj));
                }

                ParamedicScheduleDateQuery query = new ParamedicScheduleDateQuery();

                query.Where
                    (
                        query.ServiceUnitID == cboServiceUnitID.SelectedValue,
                        query.PeriodYear == cboPeriodYear.SelectedValue,
                        query.ParamedicID == cboParamedicID.SelectedValue
                    );

                DataTable dtb = query.LoadDataTable();
                dtb.PrimaryKey = new DataColumn[]
                    {
                        dtb.Columns["ScheduleDate"]
                    };

                Session["dtbParamedicScheduleDate"] = dtb;
                return dtb;
            }
            set { Session["dtbParamedicScheduleDate"] = value; }
        }

        #endregion

        protected void cboPeriodYear_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ParamedicScheduleDates = null;
            SetRangeDateSchedule(Convert.ToInt16(cboPeriodYear.SelectedValue));
        }

        protected void cvParamedicID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DataModeCurrent != AppEnum.DataMode.New || DataModeCurrent == AppEnum.DataMode.Read)
                return;

            //Check exist
            ParamedicSchedule rec = new ParamedicSchedule();
            if (rec.LoadByPrimaryKey(cboServiceUnitID.SelectedValue, cboParamedicID.SelectedValue, cboPeriodYear.SelectedValue))
            {
                args.IsValid = false;
                btnChangeSchedule.Enabled = false;
                btnGlobalSchedule.Enabled = false;
            }
            else
            {
                args.IsValid = true;
                btnChangeSchedule.Enabled = true;
                btnGlobalSchedule.Enabled = true;
            }
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            ParamedicQuery query = new ParamedicQuery("a");
            ServiceUnitParamedicQuery serviceUnitParamedic = new ServiceUnitParamedicQuery("b");
            query.Where
                (
                    query.Or
                        (
                             query.ParamedicName.Like(searchTextContain),
                             query.ParamedicID.Like(searchTextContain)
                        ),
                        serviceUnitParamedic.ServiceUnitID == cboServiceUnitID.SelectedValue
                );
            query.InnerJoin(serviceUnitParamedic).On(query.ParamedicID == serviceUnitParamedic.ParamedicID);
            query.OrderBy(query.ParamedicName.Ascending);
            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();

        }

        protected void cboParamedicID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cvParamedicID.Validate();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var unit = new ServiceUnitQuery("a");
            if (!this.IsUserCrossUnitAble)
            {
                var user = new AppUserServiceUnitQuery("b");
                unit.InnerJoin(user).On(user.ServiceUnitID == unit.ServiceUnitID && user.UserID == AppSession.UserLogin.UserID);
            }
            var tcode = new ServiceUnitTransactionCodeQuery("c");
            unit.InnerJoin(tcode).On(tcode.ServiceUnitID == unit.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.Appointment.ToString());
            unit.Where(
                     unit.SRRegistrationType.In(
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                            ),
                unit.IsActive == true,
                tcode.ServiceUnitID.IsNotNull(),
                unit.Or(
                    unit.ServiceUnitName.Like(searchTextContain),
                    unit.ServiceUnitID.Like(searchTextContain))
                );
            unit.OrderBy(unit.ServiceUnitName.Ascending);

            unit.es.Top = 10;
            DataTable dtb = unit.LoadDataTable();

            cboServiceUnitID.DataSource = dtb;
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == string.Empty)
                //GetParamedicCollection(e.Value);
                //else
                cboParamedicID.Items.Clear();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument == "rebind")
            {
                OnPopulateEntryControl(new string[] { });
            }
        }

        protected void btnUpdateHFIS_Click(object sender, EventArgs e)
        {
            HideInformationHeader();

            if (Helper.IsBpjsAntrolIntegration)
            {
                var poli = new ServiceUnitBridging();
                poli.Query.Where(poli.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString());
                poli.Query.Where(poli.Query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                var go = poli.Query.Load();

                var dokter = new ParamedicBridging();
                dokter.Query.Where(dokter.Query.SRBridgingType == AppEnum.BridgingType.ANTROL.ToString() && dokter.Query.ParamedicID == cboParamedicID.SelectedValue);
                go = dokter.Query.Load();

                OperationalTime ot;

                try
                {
                    var list = new List<Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal>();
                    if (!string.IsNullOrWhiteSpace(cboSenin.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboSenin.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "1",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "1",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "1",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "1",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "1",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboSelasa.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboSelasa.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "2",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "2",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "2",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "2",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "2",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboRabu.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboRabu.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "3",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "3",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "3",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "3",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "3",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboKamis.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboKamis.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "4",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "4",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "4",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "4",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "4",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboJumat.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboJumat.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "5",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "5",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "5",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "5",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "5",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboSabtu.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboSabtu.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "6",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "6",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "6",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "6",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "6",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (!string.IsNullOrWhiteSpace(cboMinggu.SelectedValue))
                    {
                        ot = new OperationalTime();
                        ot.LoadByPrimaryKey(cboMinggu.SelectedValue);

                        list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                        {
                            Hari = "7",
                            Buka = ot.StartTime1,
                            Tutup = ot.EndTime1
                        });

                        if (!string.IsNullOrWhiteSpace(ot.StartTime2) && !string.IsNullOrWhiteSpace(ot.EndTime2))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "7",
                                Buka = ot.StartTime2,
                                Tutup = ot.EndTime2
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime3) && !string.IsNullOrWhiteSpace(ot.EndTime3))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "7",
                                Buka = ot.StartTime3,
                                Tutup = ot.EndTime3
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime4) && !string.IsNullOrWhiteSpace(ot.EndTime4))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "7",
                                Buka = ot.StartTime4,
                                Tutup = ot.EndTime4
                            });
                        if (!string.IsNullOrWhiteSpace(ot.StartTime5) && !string.IsNullOrWhiteSpace(ot.EndTime5))
                            list.Add(new Common.BPJS.Antrian.Update.JadwalDokter.Request.Jadwal()
                            {
                                Hari = "7",
                                Buka = ot.StartTime5,
                                Tutup = ot.EndTime5
                            });
                    }
                    if (go)
                    {
                        var update = new Common.BPJS.Antrian.Update.JadwalDokter.Request.Root()
                        {
                            Kodedokter = dokter.BridgingID.ToInt(),
                            Kodepoli = poli.BridgingID.Split(';')[0],
                            Kodesubspesialis = poli.BridgingID.Split(';')[1],
                            Jadwal = list
                        };
                        var svc = new Common.BPJS.Antrian.Service();
                        var response = svc.UpdateJadwalDokter(update);
                        {
                            var log = new WebServiceAPILog
                            {
                                DateRequest = DateTime.Now,
                                IPAddress = "10.200.200.188",
                                UrlAddress = "ParamedicScheduleDetail",
                                Params = JsonConvert.SerializeObject(update),
                                Response = JsonConvert.SerializeObject(response)
                            };
                            log.Save();
                        }
                        if (!response.Metadata.IsAntrolValid) ShowInformationHeader($"{response.Metadata.Code}, {response.Metadata.Message}");
                        else
                        {
                            for (int i = 1; i <= 12; i++)
                            {
                                for (int day = 1; day <= DateTime.DaysInMonth(cboPeriodYear.SelectedValue.ToInt(), i); day++)
                                {
                                    var date = new DateTime(cboPeriodYear.SelectedValue.ToInt(), i, day).Date;
                                    if (date < DateTime.Now) continue;
                                    var time = string.Empty;
                                    if (date.DayOfWeek == DayOfWeek.Monday) time = cboSenin.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Tuesday) time = cboSelasa.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Wednesday) time = cboRabu.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Thursday) time = cboKamis.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Friday) time = cboJumat.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Saturday) time = cboSabtu.SelectedValue;
                                    else if (date.DayOfWeek == DayOfWeek.Sunday) time = cboMinggu.SelectedValue;
                                    DataRow row = ParamedicScheduleDates.Rows.Find(date);
                                    if (row == null)
                                    {
                                        if (!string.IsNullOrEmpty(time))
                                        {
                                            row = ParamedicScheduleDates.NewRow();
                                            row["ScheduleDate"] = date;
                                            row["OperationalTimeID"] = time;
                                            row["IsIpr"] = true;
                                            row["IsOpr"] = true;
                                            row["IsEmr"] = true;
                                            row["AddQuota"] = 0;
                                            ParamedicScheduleDates.Rows.Add(row);
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(time)) row.Delete();
                                        else row["OperationalTimeID"] = time;
                                    }
                                }
                            }
                            ShowInformationHeader($"{response.Metadata.Code}, {response.Metadata.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        //private void GetParamedicCollection(string serviceUnitID)
        //{
        //    var medic = new ParamedicQuery("a");
        //    var unit = new ServiceUnitParamedicQuery("b");

        //    medic.InnerJoin(unit).On(medic.ParamedicID == unit.ParamedicID);
        //    medic.Where
        //        (
        //            medic.IsActive == true,
        //            unit.ServiceUnitID == serviceUnitID
        //        );

        //    var coll = new ParamedicCollection();
        //    coll.Load(medic);

        //    cboParamedicID.Items.Clear();
        //    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        //    foreach (var entity in coll)
        //    {
        //        cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
        //    }
        //}
    }
}