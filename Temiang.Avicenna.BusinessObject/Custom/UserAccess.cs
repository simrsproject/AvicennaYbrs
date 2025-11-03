using System;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    ///<summary>
    ///Hak akses user pada program
    ///</summary>
    [Serializable]
    public class UserAccess
    {
        private string _programID;

        private bool _isAddAble;
        private bool _isDeleteAble;
        private bool _isEditAble;
        private bool _isApprovalAble;
        private bool _isUnApprovalAble;
        private bool _isVoidAble;
        private bool _isUnVoidAble;
        private bool _isExportAble;
        private bool _isCrossUnitAble;
        private bool _isPowerUserAble;
        private bool _isExist;
        
        private bool _isProgramAddAble;
        private bool _isProgramDeleteAble;
        private bool _isProgramEditAble;
        private bool _isProgramApprovalAble;
        private bool _isProgramUnApprovalAble;
        private bool _isProgramVoidAble;
        private bool _isProgramUnVoidAble;
        private bool _isProgramExportAble;
        private bool _isProgramCrossUnitAble;
        private bool _isProgramPowerUserAble;

        private bool _isMenuAddVisible = true;
        private bool _isMenuHomeVisible = true;

        private bool _isListLoadRecordOnInit;
        private bool _isListLoadRecordIfFiltered;
        private string _navigateUrl;

        public UserAccess()
        {
        }

        public UserAccess(string userID, string programID)
        {
            if (string.IsNullOrEmpty(programID)) return;
            using (esTransactionScope trans = new esTransactionScope())
            {
                AppUserUserGroupQuery uuGroupQuery = new AppUserUserGroupQuery("a");
                AppUserGroupProgramQuery ugPrgQuery = new AppUserGroupProgramQuery("b");
                ugPrgQuery.InnerJoin(uuGroupQuery).On(ugPrgQuery.UserGroupID == uuGroupQuery.UserGroupID);
                ugPrgQuery.Where(uuGroupQuery.UserID == userID & ugPrgQuery.ProgramID == programID);
                ugPrgQuery.Select(
                    ugPrgQuery.IsUserGroupAddAble.Cast(esCastType.Single).Max().As("IsUserGroupAddAble"),
                    ugPrgQuery.IsUserGroupEditAble.Cast(esCastType.Single).Max().As("IsUserGroupEditAble"),
                    ugPrgQuery.IsUserGroupDeleteAble.Cast(esCastType.Single).Max().As("IsUserGroupDeleteAble"),
                    ugPrgQuery.IsUserGroupApprovalAble.Cast(esCastType.Single).Max().As("IsUserGroupApprovalAble"),
                    ugPrgQuery.IsUserGroupUnApprovalAble.Cast(esCastType.Single).Max().As("IsUserGroupUnApprovalAble"),
                    ugPrgQuery.IsUserGroupVoidAble.Cast(esCastType.Single).Max().As("IsUserGroupVoidAble"),
                    ugPrgQuery.IsUserGroupUnVoidAble.Cast(esCastType.Single).Max().As("IsUserGroupUnVoidAble"),
                    ugPrgQuery.IsUserGroupExportAble.Cast(esCastType.Single).Max().As("IsUserGroupExportAble"),
                    ugPrgQuery.IsUserGroupCrossUnitAble.Cast(esCastType.Single).Max().As("IsUserGroupCrossUnitAble"),
                    ugPrgQuery.IsUserGroupPowerUserAble.Cast(esCastType.Single).Max().As("IsUserGroupPowerUserAble")
                    );
                ugPrgQuery.GroupBy(ugPrgQuery.ProgramID);
                DataTable dtb = ugPrgQuery.LoadDataTable();
                if (dtb.Rows.Count > 0)
                {
                    _programID = programID;
                    _isAddAble = dtb.Rows[0]["IsUserGroupAddAble"] == DBNull.Value
                                     ? false
                                     : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupAddAble"]);
                    _isEditAble = dtb.Rows[0]["IsUserGroupEditAble"] == DBNull.Value
                                      ? false
                                      : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupEditAble"]);
                    _isDeleteAble = dtb.Rows[0]["IsUserGroupDeleteAble"] == DBNull.Value
                                        ? false
                                        : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupDeleteAble"]);
                    _isApprovalAble = dtb.Rows[0]["IsUserGroupApprovalAble"] == DBNull.Value
                                          ? false
                                          : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupApprovalAble"]);
                    _isUnApprovalAble = dtb.Rows[0]["IsUserGroupUnApprovalAble"] == DBNull.Value
                                            ? false
                                            : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupUnApprovalAble"]);
                    _isVoidAble = dtb.Rows[0]["IsUserGroupVoidAble"] == DBNull.Value
                                      ? false
                                      : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupVoidAble"]);
                    _isUnVoidAble = dtb.Rows[0]["IsUserGroupUnVoidAble"] == DBNull.Value
                                        ? false
                                        : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupUnVoidAble"]);

                    _isExportAble = dtb.Rows[0]["IsUserGroupExportAble"] == DBNull.Value
                                        ? false
                                        : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupExportAble"]);

                    _isCrossUnitAble = dtb.Rows[0]["IsUserGroupCrossUnitAble"] == DBNull.Value
                        ? false
                        : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupCrossUnitAble"]);

                    _isPowerUserAble = dtb.Rows[0]["IsUserGroupPowerUserAble"] == DBNull.Value
                        ? false
                        : Convert.ToBoolean(dtb.Rows[0]["IsUserGroupPowerUserAble"]);

                    AppProgram prog = new AppProgram();
                    prog.LoadByPrimaryKey(programID);
                    _isProgramAddAble = prog.IsProgramAddAble ?? false;
                    _isProgramEditAble = prog.IsProgramEditAble ?? false;
                    _isProgramDeleteAble = prog.IsProgramDeleteAble ?? false;
                    _isProgramApprovalAble = prog.IsProgramApprovalAble ?? false;
                    _isProgramUnApprovalAble = prog.IsProgramUnApprovalAble ?? false;
                    _isProgramVoidAble = prog.IsProgramVoidAble ?? false;
                    _isProgramUnVoidAble = prog.IsProgramUnVoidAble ?? false;
                    _isProgramExportAble = prog.IsProgramExportAble ?? false;
                    _isProgramCrossUnitAble = prog.IsProgramCrossUnitAble ?? false;
                    _isProgramPowerUserAble = prog.IsProgramPowerUserAble ?? false;
                    _isMenuAddVisible = prog.IsMenuAddVisible ?? true;
                    _isMenuHomeVisible = prog.IsMenuHomeVisible ?? false;
                    _isListLoadRecordOnInit = prog.IsListLoadRecordOnInit ?? false;
                    _isListLoadRecordIfFiltered = prog.IsListLoadRecordIfFiltered ?? false;
                    _navigateUrl = prog.NavigateUrl;
                    _isExist = true;

                }
            }
        }

        public string ProgramID
        {
            get { return _programID; }
        }

        public bool IsProgramApprovalAble
        {
            get { return _isProgramApprovalAble; }
            set { _isProgramApprovalAble = value; }
        }

        public bool IsApprovalAble
        {
            get { return _isApprovalAble; }
            set { _isApprovalAble = value; }
        }

        public bool IsProgramUnApprovalAble
        {
            get { return _isProgramUnApprovalAble; }
            set { _isProgramUnApprovalAble = value; }
        }

        public bool IsUnApprovalAble
        {
            get { return _isUnApprovalAble; }
            set { _isUnApprovalAble = value; }
        }

        public bool IsProgramVoidAble
        {
            get { return _isProgramVoidAble; }
            set { _isProgramVoidAble = value; }
        }

        public bool IsVoidAble
        {
            get { return _isVoidAble; }
            set { _isVoidAble = value; }
        }

        public bool IsProgramUnVoidAble
        {
            get { return _isProgramUnVoidAble; }
            set { _isProgramUnVoidAble = value; }
        }

        public bool IsUnVoidAble
        {
            get { return _isUnVoidAble; }
            set { _isUnVoidAble = value; }
        }

        public bool IsProgramAddAble
        {
            get { return _isProgramAddAble; }
        }

        public bool IsAddAble
        {
            get { return _isAddAble; }
        }

        public bool IsProgramEditAble
        {
            get { return _isProgramEditAble; }
        }

        public bool IsEditAble
        {
            get { return _isEditAble; }
        }

        public bool IsProgramDeleteAble
        {
            get { return _isProgramDeleteAble; }
        }

        public bool IsDeleteAble
        {
            get { return _isDeleteAble; }
        }

        public bool IsProgramExportAble
        {
            get { return _isProgramExportAble; }
        }

        public bool IsExportAble
        {
            get { return _isExportAble; }
        }

        public bool IsProgramCrossUnitAble
        {
            get { return _isProgramCrossUnitAble; }
        }

        public bool IsCrossUnitAble
        {
            get { return _isCrossUnitAble; }
        }

        public bool IsProgramPowerUserAble
        {
            get { return _isProgramPowerUserAble; }
        }

        public bool IsPowerUserAble
        {
            get { return _isPowerUserAble; }
        }

        public bool IsExist
        {
            get { return _isExist; }
        }

        public bool IsMenuAddVisible
        {
            get { return _isMenuAddVisible; }
            set { _isMenuAddVisible = value; }
        }

        public bool IsMenuHomeVisible
        {
            get { return _isMenuHomeVisible; }
            set { _isMenuHomeVisible = value; }
        }

        public bool IsListLoadRecordOnInit
        {
            get { return _isListLoadRecordOnInit; }
            set { _isListLoadRecordOnInit = value; }
        }

        public bool IsListLoadRecordIfFiltered
        {
            get { return _isListLoadRecordIfFiltered; }
            set { _isListLoadRecordIfFiltered = value; }
        }

        public string NavigateUrl
        {
            get { return _navigateUrl; }
            set { _navigateUrl = value; }
        }
    }
}