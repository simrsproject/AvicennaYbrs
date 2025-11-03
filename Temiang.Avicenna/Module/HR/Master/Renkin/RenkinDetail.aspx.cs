using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR
{
    public partial class RenkinDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "RenkinSearch.aspx";
            UrlPageList = "RenkinList.aspx";

            ProgramID = AppConstant.Program.Renkin; //TODO: Sesuaikan ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRRenkinJenisKegiatan, AppEnum.StandardReference.RenkinJenisKegiatan);
            }

            if (!IsCallback)
            {
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Renkin());
            txtTargetPersen.Value = 100;
            txtTargetBulan.Value = 12;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            Renkin entity = new Renkin();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRenkinID.Text)))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Renkin entity = new Renkin();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRenkinID.Text)))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new Renkin();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Renkin entity = new Renkin();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtRenkinID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
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
            auditLogFilter.PrimaryKeyData = string.Format("RenkinID='{0}'", txtRenkinID.Text.Trim());
            auditLogFilter.TableName = "Renkin";
        }
        #endregion

        #region ToolBar Menu Support	
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtRenkinID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            Renkin entity = new Renkin();
            if (parameters.Length > 0)
            {
                string renkinID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(renkinID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtRenkinID.Text));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            Renkin renkin = (Renkin)entity;
            txtRenkinID.Value = Convert.ToInt32(renkin.RenkinID);
            //if (renkin.PositionID != null)
            //{
            //    var pgQuery = new PositionQuery();
            //    pgQuery.Where(pgQuery.PositionID == Convert.ToInt32(renkin.PositionID));
            //    cboPositionID.DataSource = pgQuery.LoadDataTable();
            //    cboPositionID.DataBind();
            //    cboPositionID.SelectedValue = pgQuery.PositionID;
            //}
            PositionQuery pQuery = new PositionQuery();
            pQuery.Where(pQuery.PositionID == Convert.ToInt32(renkin.PositionID));
            var dtb = pQuery.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                cboPositionID.DataSource = dtb;
                cboPositionID.DataBind();
                cboPositionID.SelectedValue = renkin.PositionID.ToString();
            }
            txtKegiatan.Text = renkin.Kegiatan;
            cboSRRenkinJenisKegiatan.SelectedValue = renkin.SRRenkinJenisKegiatan;
            txtTargetPersen.Value = Convert.ToInt32(renkin.TargetPersen);
            txtTargetBulan.Value = Convert.ToInt32(renkin.TargetBulan);
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(Renkin entity)
        {
            entity.RenkinID = Convert.ToInt32(txtRenkinID.Value);
            entity.PositionID = string.IsNullOrEmpty(cboPositionID.SelectedValue) ? -0 : Convert.ToInt32(cboPositionID.SelectedValue);
            entity.Kegiatan = txtKegiatan.Text;
            entity.SRRenkinJenisKegiatan = cboSRRenkinJenisKegiatan.SelectedValue;
            entity.TargetPersen = Convert.ToInt32(txtTargetPersen.Value);
            entity.TargetBulan = Convert.ToInt32(txtTargetBulan.Value);

            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(Renkin entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            RenkinQuery que = new RenkinQuery();
            que.es.Top = 1;
            if (isNextRecord)
            {
                que.Where(que.RenkinID > txtRenkinID.Text);
                que.OrderBy(que.RenkinID.Ascending);
            }
            else
            {
                que.Where(que.RenkinID < txtRenkinID.Text);
                que.OrderBy(que.RenkinID.Descending);
            }
            Renkin entity = new Renkin();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        //#region Method & Event TextChanged
        //protected void txtPositionID_TextChanged(object sender, EventArgs e)
        //{
        //    PopulatePositionName(true);
        //}

        //private void PopulatePositionName(bool isResetIdIfNotExist)
        //{
        //    //TODO: Fix generated Code
        //    if (txtPositionID.Text == string.Empty)
        //    {
        //        lblPositionName.Text = string.Empty;
        //        return;
        //    }
        //    Position entity = new Position();
        //    if (entity.LoadByPrimaryKey(Convert.ToInt32(txtPositionID.Text)))
        //    {
        //        txtPositionID.Text = entity.PositionID;
        //        lblPositionName.Text = entity.PositionName;
        //    }
        //    else
        //    {
        //        lblPositionName.Text = string.Empty;
        //        if (isResetIdIfNotExist)
        //            txtPositionID.Text = string.Empty;
        //    }
        //}
        //#endregion

        #region ComboBox Function

        protected void cboPositionID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new PositionQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PositionID,
                    query.PositionCode,
                    query.PositionName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PositionCode.Like(string.Format("%{0}%", e.Text)),
                            query.PositionName.Like(string.Format("%{0}%", e.Text))
                        )
                );

            cboPositionID.DataSource = query.LoadDataTable();
            cboPositionID.DataBind();
        }

        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        #endregion ComboBox Function
    }
}
