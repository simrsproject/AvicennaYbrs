using System.Data;
using System.Globalization;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.Interfaces;
using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Common;
namespace Temiang.Avicenna.Common
{
    public static class ComboBox
    {
        #region SetVal
        public static void SetValue(RadComboBox cbo, string Val) {
            var cboItem = cbo.Items.FindItemByValue(Val);
            if (cboItem != null)
            {
                cbo.SelectedValue = Val;
            }
        }
        #endregion
        #region Populate Item

        public static string PopulateWithOneRow(RadComboBox cbo, string selectedValue, Enums.EntityClassName entityClassName)
        {
            var valueFieldName = string.Format("{0}ID", entityClassName);
            var textFieldName = string.Format("{0}Name", entityClassName);
            return PopulateWithOneRow(cbo, selectedValue, entityClassName, valueFieldName, textFieldName);
        }

        public static string PopulateWithOneRow(RadComboBox cbo, string selectedValue, Enums.EntityClassName entityClassName, string valueFieldName, string textFieldName)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(selectedValue))
                return string.Empty;

            const string assemblyName = "Temiang.Avicenna.BusinessObject";
            Type entityType = Type.GetType(string.Concat(assemblyName, ".", entityClassName, ", ", assemblyName));
            var entity = (esEntityWAuditLog)Activator.CreateInstance(entityType);

            var whereClause = string.IsNullOrWhiteSpace(valueFieldName)
                ? string.Format("{0}ID = '{1}'", entityClassName, selectedValue)
                : string.Format("{0} = '{1}'", valueFieldName, selectedValue);

            if (entity.Load(whereClause))
            {
                var value = Convert.ToString(entity.GetColumn(valueFieldName));
                var text = Convert.ToString(entity.GetColumn(textFieldName));
                cbo.Items.Add(new RadComboBoxItem(text, value));
                cbo.SelectedIndex = 0;
                return text;
            }
            return string.Empty;
        }

        public static void SelectedValue(RadComboBox cbo, string value)
        {
            int i = 0;
            cbo.SelectedIndex = -1;
            cbo.Text = string.Empty;
            foreach (RadComboBoxItem item in cbo.Items)
            {
                if (value == item.Value)
                {
                    cbo.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        public static void SelectedValue(RadDropDownList cbo, string value)
        {
            int i = 0;
            foreach (DropDownListItem item in cbo.Items)
            {
                if (value == item.Value)
                {
                    cbo.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }
        public static void PopulateWithDeparment(RadComboBox cboDepartmentID)
        {
            var coll = new DepartmentCollection();
            var query = new DepartmentQuery("a");
            query.Where
                (
                    query.IsActive == true
                );
            query.OrderBy(query.DepartmentName.Ascending);

            coll.Load(query);

            cboDepartmentID.Items.Clear();
            cboDepartmentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Department item in coll)
            {
                cboDepartmentID.Items.Add(new RadComboBoxItem(item.DepartmentName, item.DepartmentID));
            }
        }
        public static void PopulateWithParamedicTeam(RadComboBox cbo, string registrationNo, string regType, DateTime dateEntry, string paramedicID)
        {
            cbo.Items.Clear();

            // 1. Ambil dari ParamedicTeam
            var pt = new ParamedicTeamQuery("pt");
            var md = new ParamedicQuery("m");
            pt.InnerJoin(md).On(pt.ParamedicID == md.ParamedicID);
            pt.Where(pt.RegistrationNo == registrationNo, pt.Or(pt.EndDate.IsNull(), pt.EndDate >= dateEntry.Date), md.IsActive == true);

            pt.Select(pt.ParamedicID, md.ParamedicName);
            var dtb = pt.LoadDataTable();

            // 2. Tambah dg dokter Emergency jika tipe reg emergency
            if (regType == AppConstant.RegistrationType.EmergencyPatient)
            {
                md = new ParamedicQuery("m");
                var sp = new ServiceUnitParamedicQuery("sp");
                sp.InnerJoin(md).On(sp.ParamedicID == md.ParamedicID);

                var su = new ServiceUnitQuery("su");
                sp.InnerJoin(su).On(sp.ServiceUnitID == su.ServiceUnitID & su.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient);

                sp.Select(sp.ParamedicID, md.ParamedicName);

                dtb.Merge(sp.LoadDataTable());
            }

            // 3. Tambah dokter Bedah & Anestesi
            var subColl = new ServiceUnitBookingCollection();
            subColl.Query.Where(subColl.Query.RegistrationNo == registrationNo, subColl.Query.Or(subColl.Query.IsVoid.IsNull(), subColl.Query.IsVoid == false));
            if (subColl.LoadAll())
            {
                foreach (var sub in subColl)
                {

                    if (!string.IsNullOrEmpty(sub.ParamedicID))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID2))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID2;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID2);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID3))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID3;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID3);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicID4))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicID4;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicID4);
                        dtb.Rows.Add(newRow);
                    }

                    if (!string.IsNullOrEmpty(sub.ParamedicIDAnestesi))
                    {
                        var newRow = dtb.NewRow();
                        newRow["ParamedicID"] = sub.ParamedicIDAnestesi;
                        newRow["ParamedicName"] = Paramedic.GetParamedicName(sub.ParamedicIDAnestesi);
                        dtb.Rows.Add(newRow);
                    }
                }
            }
            //// 4. Tambah paramedicID krn bisa jadi belum ada
            //if (ParamedicTeam.IsParamedicInTeam(paramedicID, registrationNo))
            //{
            //    var newRow = dtb.NewRow();
            //    newRow["ParamedicID"] = paramedicID;
            //    newRow["ParamedicName"] = Paramedic.GetParamedicName(paramedicID);
            //    dtb.Rows.Add(newRow);
            //}

            // kalau bukan rawat inap, tambahkan dokter dari registrasi
            var reg = new Registration();
            if(reg.LoadByPrimaryKey(registrationNo)){
                if (reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient) {
                    var newRow = dtb.NewRow();
                    newRow["ParamedicID"] = reg.ParamedicID;
                    newRow["ParamedicName"] = Paramedic.GetParamedicName(reg.ParamedicID);
                    dtb.Rows.Add(newRow);
                }
            }

            // Sort dan hapus redundance
            var sorted = dtb.Select(null, "ParamedicName ASC");
            var dtbSorted = dtb.Clone();
            var prevID = string.Empty;
            foreach (var row in sorted)
            {
                if (!prevID.Equals(row["ParamedicID"]))
                {
                    prevID = row["ParamedicID"].ToString();
                    dtbSorted.Rows.Add(row.ItemArray);
                }
            }

            foreach (DataRow row in dtbSorted.Rows)
            {
                cbo.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(), row["ParamedicID"].ToString()));
            }
            ComboBox.SelectedValue(cbo, paramedicID);
        }

        public static void PopulateWithGuarantor(RadComboBox cboGuarantorID)
        {
            var coll = new GuarantorCollection();
            var query = new GuarantorQuery("a");
            query.Where
            (
                query.IsActive == true
            );
            query.OrderBy(query.GuarantorID.Ascending);

            coll.Load(query);

            cboGuarantorID.Items.Clear();
            cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (Guarantor item in coll)
            {
                cboGuarantorID.Items.Add(new RadComboBoxItem(item.GuarantorName, item.GuarantorID));
            }
        }

        public static void PopulateWithServiceUnit(RadComboBox cboServiceUnitID, string registrationType, bool isFilterByUserID, string srBuilding)
        {
            var coll = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            switch (registrationType)
            {
                case AppConstant.RegistrationType.InPatient:
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    break;
                case AppConstant.RegistrationType.OutPatient:
                case AppConstant.RegistrationType.Ancillary:
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient);
                    break;
                case AppConstant.RegistrationType.ClusterPatient:
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.ClusterPatient);
                    break;
                case AppConstant.RegistrationType.EmergencyPatient:
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient);
                    break;
                case AppConstant.RegistrationType.MedicalCheckUp:
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp);
                    break;
            }
            query.Where
                (
                    query.IsActive == true
                );
            if (registrationType == AppConstant.RegistrationType.OutPatient && !string.IsNullOrEmpty(srBuilding))
            {
                query.Where(query.SRBuilding.IsNull(), query.SRBuilding == string.Empty, query.SRBuilding == srBuilding);
            }
            query.OrderBy(query.ServiceUnitName.Ascending);

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            coll.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }

        public static void PopulateWithServiceUnit(RadComboBox cboServiceUnitID, bool isFilterByUserID)
        {
            var coll = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            query.Where
                (
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            coll.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }
        public static void PopulateWithServiceUnitForLocation(RadComboBox cboLocationId, string unitId)
        {
            cboLocationId.Items.Clear();
            cboLocationId.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var query = new ServiceUnitLocationQuery("a");
            var locq = new LocationQuery("b");

            query.Select(query.LocationID, locq.LocationName);
            query.OrderBy(query.IsLocationMain.Descending);
            query.InnerJoin(locq).On(query.LocationID == locq.LocationID);
            query.Where
                (
                    query.ServiceUnitID == unitId, locq.IsConsignment == false
                );
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow item in dtb.Rows)
            {
                cboLocationId.Items.Add(new RadComboBoxItem(item["LocationName"].ToString(), item["LocationID"].ToString()));
            }
        }
        public static void PopulateWithServiceUnitForRefer(RadComboBox cboServiceUnitID)
        {
            var coll = new ServiceUnitCollection();
            var query = new ServiceUnitQuery("a");
            var suq = new ServiceUnitTransactionCodeQuery("b");
            var transCode = new ServiceUnitTransactionCodeQuery("txc");
            query.LeftJoin(suq).On(query.ServiceUnitID == suq.ServiceUnitID &&
                                   suq.SRTransactionCode == TransactionCode.JobOrder);
            query.InnerJoin(transCode).On(transCode.SRTransactionCode == TransactionCode.Registration && transCode.ServiceUnitID == query.ServiceUnitID);
            query.Where(query.SRRegistrationType == AppConstant.RegistrationType.OutPatient, query.IsActive == true,
                        query.IsUsingJobOrder == false, suq.ServiceUnitID.IsNull());
            query.OrderBy(query.ServiceUnitName.Ascending);

            coll.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }

        public static void PopulateWithServiceUnitForTransaction(RadComboBox cboServiceUnitID, bool isFilterByUserID)
        {
            var coll = new ServiceUnitCollection();

            var query = new ServiceUnitQuery("a");
            var access = new ServiceUnitTransactionCodeQuery("b");

            query.InnerJoin(access).On(query.ServiceUnitID == access.ServiceUnitID);
            query.Where(
                access.SRTransactionCode == TransactionCode.Charges,
                query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            coll.Load(query);

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }
        }

        public static void PopulateWithServiceUnitForTransaction(RadComboBox cboServiceUnitID, string transactionCode, bool isFilterByUserID)
        {
            PopulateWithServiceUnitForTransaction(cboServiceUnitID, transactionCode, isFilterByUserID, string.Empty, string.Empty);
        }
        public static void PopulateWithServiceUnitForTransaction(RadComboBox cboServiceUnitID, string transactionCode, bool isFilterByUserID, string defaultSelectedVal, string srItemType)
        {
            var query = new ServiceUnitTransactionCodeQuery("a");
            var qrServ = new ServiceUnitQuery("c");

            query.es.Distinct = true;
            query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName, query.IsItemProductMedic, query.IsItemProductNonMedic, query.IsItemKitchen);
            query.OrderBy(qrServ.ServiceUnitName.Ascending);
            query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
            query.Where
                (
                    query.SRTransactionCode == transactionCode,
                    qrServ.IsActive == true
                );

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            foreach (DataRow item in dtb.Rows)
            {
                if (!string.IsNullOrEmpty(srItemType))
                {
                    switch (srItemType)
                    {
                        case ItemType.Medical:
                            if (Convert.ToBoolean(item["IsItemProductMedic"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;

                        case ItemType.NonMedical:
                            if (Convert.ToBoolean(item["IsItemProductNonMedic"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;

                        case ItemType.Kitchen:
                            if (Convert.ToBoolean(item["IsItemKitchen"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;
                    }
                }
                else
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }

            cboServiceUnitID.SelectedValue = defaultSelectedVal;
        }

        public static void PopulateWithServiceUnitForTransactionAppt(RadComboBox cboServiceUnitID, string transactionCode, bool isFilterByUserID, string regType)
        {
            var query = new ServiceUnitTransactionCodeQuery("a");
            var qrServ = new ServiceUnitQuery("c");

            query.es.Distinct = true;
            query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName);
            query.OrderBy(qrServ.ServiceUnitName.Ascending);
            query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
            query.Where
                (
                    query.SRTransactionCode == transactionCode,
                    qrServ.IsActive == true
                );

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }
            if (!string.IsNullOrEmpty(regType))
                query.Where(qrServ.SRRegistrationType == regType);

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }
        }

        public static void PopulateWithServiceUnitForTwoTransactionCode(RadComboBox cboServiceUnitID, string transactionCode1, string transactionCode2, bool isFilterByUserID)
        {
            var query = new ServiceUnitTransactionCodeQuery("a");
            var qrServ = new ServiceUnitQuery("c");

            query.es.Distinct = true;
            query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName);
            query.OrderBy(qrServ.ServiceUnitName.Ascending);
            query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
            query.Where
                (
                    query.SRTransactionCode.In(transactionCode1, transactionCode2),
                    qrServ.IsActive == true
                );

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }
        }

        public static void PopulateWithServiceUnitForTransactionWithException(RadComboBox cboServiceUnitID, string transactionCode, bool isFilterByUserID, string srItemType, string exceptionUnitId, bool isInclude)
        {
            var query = new ServiceUnitTransactionCodeQuery("a");
            var qrServ = new ServiceUnitQuery("c");

            query.es.Distinct = true;
            query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName, query.IsItemProductMedic, query.IsItemProductNonMedic, query.IsItemKitchen);
            query.OrderBy(qrServ.ServiceUnitName.Ascending);
            query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
            query.Where
                (
                    query.SRTransactionCode == transactionCode,
                    qrServ.IsActive == true
                );

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }
            if (isInclude)
                query.Where(query.ServiceUnitID == exceptionUnitId);
            else
                query.Where(query.ServiceUnitID != exceptionUnitId);

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            foreach (DataRow item in dtb.Rows)
            {
                if (!string.IsNullOrEmpty(srItemType))
                {
                    switch (srItemType)
                    {
                        case ItemType.Medical:
                            if (Convert.ToBoolean(item["IsItemProductMedic"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;

                        case ItemType.NonMedical:
                            if (Convert.ToBoolean(item["IsItemProductNonMedic"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;

                        case ItemType.Kitchen:
                            if (Convert.ToBoolean(item["IsItemKitchen"]) == true)
                                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                            break;
                    }
                }
                else
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }

            if (isInclude)
                cboServiceUnitID.SelectedValue = exceptionUnitId;
        }

        public static void PopulateWithServiceUnitForTransactionItemType(RadComboBox cboItemType, string unitId, string transactionCode)
        {
            cboItemType.Items.Clear();
            cboItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (!string.IsNullOrEmpty(unitId))
            {
                var it = new ServiceUnitTransactionCode();
                if (it.LoadByPrimaryKey(unitId, transactionCode))
                {
                    if (it.IsItemProductMedic == true)
                        cboItemType.Items.Add(new RadComboBoxItem("Medical", ItemType.Medical));
                    if (it.IsItemProductNonMedic == true)
                        cboItemType.Items.Add(new RadComboBoxItem("Non Medical", ItemType.NonMedical));
                    if (it.IsItemKitchen == true)
                        cboItemType.Items.Add(new RadComboBoxItem("Kitchen", ItemType.Kitchen));
                }
            }
            cboItemType.Text = string.Empty;
        }

        public static string PopulateWithServiceUnitForDefaultLocation(string unitId)
        {
            var query = new ServiceUnitLocation();

            query.Query.es.Top = 1;
            query.Query.Select(query.Query.LocationID);
            query.Query.Where
                (
                    query.Query.ServiceUnitID == unitId,
                    query.Query.IsLocationMain == true
                );
            if (query.Query.Load()) return query.LocationID;
            return string.Empty;
        }


        public static void PopulateWithSupplierForLocation(RadComboBox cboLocationId, string supplierId)
        {
            cboLocationId.Items.Clear();
            cboLocationId.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var query = new SupplierLocationQuery("a");
            var locq = new LocationQuery("b");

            query.Select(query.LocationID, locq.LocationName);
            query.InnerJoin(locq).On(query.LocationID == locq.LocationID);
            query.Where
                (
                    query.SupplierID == supplierId, locq.IsConsignment == true
                );
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow item in dtb.Rows)
            {
                cboLocationId.Items.Add(new RadComboBoxItem(item["LocationName"].ToString(), item["LocationID"].ToString()));
            }
        }

        public static void PopulateWithItemBinByLocation(RadComboBox cboItemBin, string locationId)
        {
            cboItemBin.Items.Clear();
            cboItemBin.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var query = new AppStandardReferenceItemQuery("a");

            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ItemBin,
                    query.ReferenceID == locationId
                );
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow item in dtb.Rows)
            {
                cboItemBin.Items.Add(new RadComboBoxItem(item["ItemName"].ToString(), item["ItemID"].ToString()));
            }
        }

        public static void PopulateWithServiceUnitForTransactionJO(RadComboBox cboServiceUnitID, string transactionCode, bool isFilterByUserID)
        {
            var query = new ServiceUnitTransactionCodeQuery("a");
            var qrServ = new ServiceUnitQuery("c");

            query.es.Distinct = true;
            query.Select(qrServ.ServiceUnitID, qrServ.ServiceUnitName);
            query.OrderBy(qrServ.ServiceUnitName.Ascending);
            query.InnerJoin(qrServ).On(query.ServiceUnitID == qrServ.ServiceUnitID);
            query.Where
                (
                    query.SRTransactionCode == transactionCode,
                    qrServ.IsActive == true,
                    qrServ.IsUsingJobOrder == true
                );

            if (isFilterByUserID)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }
            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }
        }

        public static void ServiceUnitAncillaryItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.DepartmentID == AppSession.Parameter.MedicalSupportDepartmentID
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }


        public static void PopulateWithServiceUnitForTransactionJOLab(RadComboBox cboServiceUnitID)
        {
            var query = new ServiceUnitQuery("a");
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where(query.ServiceUnitID.In(AppSession.Parameter.ServiceUnitLaboratoryID, AppSession.Parameter.ServiceUnitPathologyAnatomyID));

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }
        }

        public static void PopulateWithServiceUnitFilterByUserID(RadComboBox cboServiceUnitID)
        {
            var query = new ServiceUnitQuery("a");
            var qusr = new AppUserServiceUnitQuery("u");
            query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
            query.Where(qusr.UserID == AppSession.UserLogin.UserID, query.IsActive == true,
                        query.SRRegistrationType == "IPR");
            query.OrderBy(query.ServiceUnitName.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow item in dtb.Rows)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
            }
        }

        public static void PopulateWithOneServiceUnit(RadComboBox cboServiceUnitID, string serviceUnitID)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;

            if (string.IsNullOrEmpty(serviceUnitID))
                return;

            var coll = new ServiceUnitCollection();
            coll.Query.Where
                (
                    coll.Query.ServiceUnitID == serviceUnitID
                );
            coll.LoadAll();

            foreach (ServiceUnit item in coll)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(item.ServiceUnitName, item.ServiceUnitID));
            }

            cboServiceUnitID.SelectedValue = serviceUnitID;
        }

        public static void PopulateWithOneParamedic(RadComboBox cbo, string id)
        {
            cbo.Items.Clear();
            cbo.Text = string.Empty;
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new ParamedicCollection();
            coll.Query.Select(coll.Query.ParamedicID, coll.Query.ParamedicName);
            coll.Query.Where
                (
                    coll.Query.ParamedicID == id
                );
            coll.LoadAll();

            foreach (var item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.ParamedicName, item.ParamedicID));
                cbo.SelectedIndex = 0;
            }

        }

        public static void PopulateWithOneUser(RadComboBox cbo, string id)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new AppUserCollection();
            coll.Query.Where
            (
                coll.Query.UserID == id
            );
            coll.LoadAll();

            foreach (var item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.UserName, item.UserID));
                cbo.SelectedIndex = 0;
            }

        }
        public static void PopulateWithOneRoom(RadComboBox cbo, string id)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new ServiceRoomCollection();
            coll.Query.Where
                (
                    coll.Query.RoomID == id
                );
            coll.LoadAll();

            foreach (var item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.RoomName, item.RoomID));
                cbo.SelectedIndex = 0;
            }
        }

        public static void PopulateWithOneSupplier(RadComboBox cbo, string supplierID)
        {
            cbo.Items.Clear();
            cbo.Text = string.Empty;
            if (string.IsNullOrEmpty(supplierID))
                return;

            var coll = new SupplierCollection();
            coll.Query.Where
                (
                    coll.Query.SupplierID == supplierID
                );
            coll.LoadAll();

            foreach (Supplier item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.SupplierName, item.SupplierID));
                cbo.SelectedIndex = 0;
            }
        }

        public static void PopulateWithOneStandardReference(RadComboBox cbo, string standardReferenceID, string itemID)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(standardReferenceID) || string.IsNullOrEmpty(itemID))
                return;

            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where
                (
                    coll.Query.StandardReferenceID == standardReferenceID,
                    coll.Query.ItemID == itemID
                );
            coll.LoadAll();

            foreach (AppStandardReferenceItem item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
                cbo.SelectedIndex = 0;
            }
        }

        public static void PopulateWithOneLocation(RadComboBox cbo, string locationId)
        {
            cbo.Items.Clear();
            cbo.Text = string.Empty;
            if (string.IsNullOrEmpty(locationId))
                return;

            var coll = new LocationCollection();
            coll.Query.Where
            (
                coll.Query.LocationID == locationId
            );
            coll.LoadAll();

            foreach (Location item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.LocationName, item.LocationID));
                cbo.SelectedIndex = 0;
            }
        }

        public static void PopulateWithOneItem(RadComboBox cboItemID, string itemID)
        {
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            if (string.IsNullOrEmpty(itemID))
                return;

            var coll = new ItemCollection();
            coll.Query.Where
                (
                    coll.Query.ItemID == itemID
                );
            coll.LoadAll();

            foreach (Item item in coll)
            {
                cboItemID.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }

            cboItemID.SelectedValue = itemID;
        }

        public static void PopulateWithOneConsumeMethod(RadComboBox cbo, string consumeMethod)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(consumeMethod))
                return;

            var coll = new ConsumeMethodCollection();
            coll.Query.Where
            (
                coll.Query.SRConsumeMethod == consumeMethod
            );
            coll.LoadAll();

            foreach (ConsumeMethod item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.SRConsumeMethodName, item.SRConsumeMethod));
            }

            cbo.SelectedValue = consumeMethod;
        }
        public static void PopulateWithOneEmbalace(RadComboBox cbo, string unitID)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(unitID))
                return;

            var coll = new EmbalaceCollection();
            coll.Query.Where
            (
                coll.Query.EmbalaceID == unitID
            );
            coll.LoadAll();

            foreach (Embalace item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.EmbalaceName, item.EmbalaceID));
            }

            ComboBox.SelectedValue(cbo, unitID);
        }

        public static void PopulateWithOneConsumeUnit(RadComboBox cbo, string consumeUnitID)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(consumeUnitID))
                return;

            ComboBox.PopulateWithOneStandardReference(cbo, AppEnum.StandardReference.DosageUnit.ToString(), consumeUnitID);
            if (cbo.SelectedIndex == -1)
            {
                PopulateWithOneEmbalace(cbo, consumeUnitID);

            }

            SelectedValue(cbo, consumeUnitID);
        }

        public static void PopulateWithOneItemGroup(RadComboBox cboItemGroupID, string itemGroupID)
        {
            cboItemGroupID.Items.Clear();
            cboItemGroupID.Text = string.Empty;
            if (string.IsNullOrEmpty(itemGroupID))
                return;

            var coll = new ItemGroupCollection();
            coll.Query.Where
            (
                coll.Query.ItemGroupID == itemGroupID
            );
            coll.LoadAll();

            foreach (ItemGroup item in coll)
            {
                cboItemGroupID.Items.Add(new RadComboBoxItem(item.ItemGroupName, item.ItemGroupID));
                cboItemGroupID.SelectedValue = item.ItemGroupID;
            }
        }

        public static void PopulateWithOneQuestionAnswerSelection(RadComboBox cbo, string id)
        {
            cbo.Items.Clear();
            cbo.Text = string.Empty;
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new QuestionAnswerSelectionCollection();
            coll.Query.Where
                (
                    coll.Query.QuestionAnswerSelectionID == id
                );
            coll.LoadAll();

            foreach (QuestionAnswerSelection item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionText, item.QuestionAnswerSelectionID));
                cbo.SelectedValue = id;
            }

        }
        public static void PopulateWithOneQuestionAnswerSelectionLine(RadComboBox cbo, string parentId, string id)
        {
            cbo.Items.Clear();
            cbo.Text = string.Empty;
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new QuestionAnswerSelectionLineCollection();
            coll.Query.Where
                (
                    coll.Query.QuestionAnswerSelectionID == parentId,
                    coll.Query.QuestionAnswerSelectionLineID == id
                );
            coll.LoadAll();

            foreach (QuestionAnswerSelectionLine item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionLineText, item.QuestionAnswerSelectionLineID));
                cbo.SelectedValue = id;
            }

        }
        public static void PopulateWithQuestionAnswerSelectionLine(RadComboBox cbo, string id)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(id))
                return;

            var coll = new QuestionAnswerSelectionLineCollection();
            coll.Query.Where
                (
                    coll.Query.QuestionAnswerSelectionID == id
                );
            coll.LoadAll();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (QuestionAnswerSelectionLine item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionLineText, item.QuestionAnswerSelectionLineID));
            }
        }

        public static void PopulateWithOneItemUnit(RadComboBox cboSRItemUnit, string itemUnit)
        {
            cboSRItemUnit.Items.Clear();
            cboSRItemUnit.Text = string.Empty;
            if (string.IsNullOrEmpty(itemUnit))
                return;

            var coll = new AppStandardReferenceItemCollection();
            coll.Query.Where
                (
                    coll.Query.ItemID == itemUnit,
                    coll.Query.StandardReferenceID == "ItemUnit"
                );
            coll.LoadAll();

            foreach (AppStandardReferenceItem item in coll)
            {
                cboSRItemUnit.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }

            cboSRItemUnit.SelectedValue = itemUnit;
        }

        public static void PopulateWithParamedic(RadComboBox cbo, string serviceUnitID)
        {
            var prevVal = cbo.SelectedValue;
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(serviceUnitID))
                return;

            var query = new ServiceUnitParamedicQuery("a");
            var qPar = new ParamedicQuery("b");

            query.Select
                (
                    qPar.ParamedicID,
                    qPar.ParamedicName
                );
            query.InnerJoin(qPar).On(query.ParamedicID == qPar.ParamedicID);
            query.Where
                (
                    query.ServiceUnitID == serviceUnitID,
                    qPar.IsActive == true,
                    qPar.IsAvailable == true
                );
            query.OrderBy(qPar.ParamedicName.Ascending);

            DataTable dtb = query.LoadDataTable();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cbo.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(),
                                                             row["ParamedicID"].ToString()));
            }

            ComboBox.SelectedValue(cbo, prevVal);
        }

        public static void PopulateWithParamedic(RadComboBox cboParamedicID)
        {
            cboParamedicID.Items.Clear();

            var qPar = new ParamedicQuery("a");

            qPar.Select
                (
                    qPar.ParamedicID,
                    qPar.ParamedicName
                );
            qPar.Where
                (
                    qPar.IsActive == true,
                    qPar.IsAvailable == true
                );
            qPar.OrderBy(qPar.ParamedicName.Ascending);

            DataTable dtb = qPar.LoadDataTable();

            cboParamedicID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboParamedicID.Items.Add(new RadComboBoxItem(row["ParamedicName"].ToString(),
                                                             row["ParamedicID"].ToString()));
            }
        }

        public static void PopulateWithUser(RadComboBox cbo)
        {
            cbo.Items.Clear();

            var query = new AppUserQuery("a");

            query.Select
                (
                    query.UserID,
                    query.UserName
                );

            query.OrderBy(query.UserName.Ascending);

            DataTable dtb = query.LoadDataTable();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cbo.Items.Add(new RadComboBoxItem(row["UserName"].ToString(),
                                                  row["UserID"].ToString()));
            }
        }

        public static void PopulateWithVisitType(RadComboBox cboVisitTypeID, string serviceUnitID)
        {
            cboVisitTypeID.Items.Clear();
            if (string.IsNullOrEmpty(serviceUnitID))
                return;

            var query = new ServiceUnitVisitTypeQuery("a");
            var qVisit = new VisitTypeQuery("b");

            query.Select
                (
                    qVisit.VisitTypeID,
                    qVisit.VisitTypeName
                );
            query.InnerJoin(qVisit).On(query.VisitTypeID == qVisit.VisitTypeID);
            query.Where(query.ServiceUnitID == serviceUnitID);
            query.OrderBy(qVisit.VisitTypeName.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboVisitTypeID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboVisitTypeID.Items.Add(new RadComboBoxItem(row["VisitTypeName"].ToString(),
                                                             row["VisitTypeID"].ToString()));
            }
        }

        public static void PopulateWithRoom(RadComboBox cbo, string serviceUnitID)
        {
            var prevVal = cbo.SelectedValue;
            cbo.Items.Clear();
            cbo.Text = string.Empty;

            if (string.IsNullOrEmpty(serviceUnitID))
                return;

            var query = new ServiceRoomQuery("a");
            query.Select
                (
                    query.RoomID,
                    query.RoomName
                );
            query.Where
                (
                    query.ServiceUnitID == serviceUnitID,
                    query.IsActive == true
                );
            query.OrderBy(query.RoomName.Ascending);

            var dtb = query.LoadDataTable();

            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cbo.Items.Add(new RadComboBoxItem(row["RoomName"].ToString(), row["RoomID"].ToString()));
            }

            ComboBox.SelectedValue(cbo, prevVal);

        }
        public static void PopulateWithQuestionAnswerSelection(RadComboBox cbo)
        {
            cbo.Items.Clear();
            var coll = new QuestionAnswerSelectionCollection();
            coll.Query.OrderBy(coll.Query.QuestionAnswerSelectionText.Ascending);
            coll.LoadAll();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (QuestionAnswerSelection item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionText, item.QuestionAnswerSelectionID));
            }

        }
        public static void PopulateWithNursingAssessmentQuestionAnswerSelection(RadComboBox cbo)
        {
            cbo.Items.Clear();
            var coll = new QuestionAnswerSelectionCollection();
            coll.Query.OrderBy(coll.Query.QuestionAnswerSelectionText.Ascending);
            coll.LoadAll();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (QuestionAnswerSelection item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionText, item.QuestionAnswerSelectionID));
            }

        }
        public static void PopulateWithNutritionCareAssessmentQuestionAnswerSelection(RadComboBox cbo)
        {
            cbo.Items.Clear();
            var coll = new QuestionAnswerSelectionCollection();
            coll.Query.OrderBy(coll.Query.QuestionAnswerSelectionText.Ascending);
            coll.LoadAll();

            cbo.Items.Add(new RadComboBoxItem("", ""));
            foreach (QuestionAnswerSelection item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.QuestionAnswerSelectionText, item.QuestionAnswerSelectionID));
            }

        }
        public static void PopulateWithBed(RadComboBox cboBedID, string roomID)
        {
            cboBedID.Items.Clear();
            if (roomID != string.Empty)
            {
                var query = new BedQuery("a");
                query.Where(query.RoomID == roomID);
                query.Select(query.BedID);
                query.OrderBy(query.BedID.Ascending);
                DataTable dtb = query.LoadDataTable();

                cboBedID.Items.Add(new RadComboBoxItem("", ""));
                foreach (DataRow row in dtb.Rows)
                {
                    cboBedID.Items.Add(new RadComboBoxItem(row["BedID"].ToString(),
                                                           row["BedID"].ToString()));
                }
            }
        }

        public static void PopulateWithItemBaseUnit(RadComboBox cboSRItemUnit, string itemID, string itemType)
        {
            cboSRItemUnit.Items.Clear();
            if (itemID == string.Empty)
                return;

            cboSRItemUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            using (new esTransactionScope()) // Use 1 connection process
            {
                if (itemType == ItemType.Medical)
                {
                    var item = new ItemProductMedic();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRItemUnit, item.SRItemUnit));
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var item = new ItemProductNonMedic();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRItemUnit, item.SRItemUnit));
                }
                else if (itemType == ItemType.Kitchen)
                {
                    var item = new ItemKitchen();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRItemUnit, item.SRItemUnit));
                }
            }
        }

        public static void PopulateWithItemPurchaseUnit(RadComboBox cboSRItemUnit, string itemID, string itemType)
        {
            cboSRItemUnit.Items.Clear();
            if (itemID == string.Empty)
                return;

            cboSRItemUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            using (new esTransactionScope()) // Use 1 connection process
            {
                if (itemType == ItemType.Medical)
                {
                    var item = new ItemProductMedic();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRPurchaseUnit, item.SRPurchaseUnit));
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var item = new ItemProductNonMedic();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRPurchaseUnit, item.SRPurchaseUnit));
                }
                else if (itemType == ItemType.Kitchen)
                {
                    var item = new ItemKitchen();
                    item.LoadByPrimaryKey(itemID);
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(item.SRPurchaseUnit, item.SRPurchaseUnit));
                }
            }
        }

        public static void PopulateWithItemUnit(RadComboBox cboSRItemUnit, string itemID, string itemType)
        {
            cboSRItemUnit.Items.Clear();
            if (itemID == string.Empty)
                return;

            cboSRItemUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            using (new esTransactionScope()) // Use 1 connection process
            {
                string baseUnit, purchaseUnit;
                if (itemType == ItemType.Medical)
                {
                    var item = new ItemProductMedic();
                    item.LoadByPrimaryKey(itemID);
                    baseUnit = item.SRItemUnit;
                    purchaseUnit = item.SRPurchaseUnit;
                }
                else if (itemType == ItemType.NonMedical)
                {
                    var item = new ItemProductNonMedic();
                    item.LoadByPrimaryKey(itemID);
                    baseUnit = item.SRItemUnit;
                    purchaseUnit = item.SRPurchaseUnit;
                }
                else
                {
                    var item = new ItemKitchen();
                    item.LoadByPrimaryKey(itemID);
                    baseUnit = item.SRItemUnit;
                    purchaseUnit = item.SRPurchaseUnit;
                }

                cboSRItemUnit.Items.Add(new RadComboBoxItem(baseUnit, baseUnit));
                if (baseUnit != purchaseUnit)
                    cboSRItemUnit.Items.Add(new RadComboBoxItem(purchaseUnit, purchaseUnit));
            }
        }

        public static void PopulateWithItemUnitForNonMaster(RadComboBox cboSRItemUnit)
        {
            cboSRItemUnit.Items.Clear();
            cboSRItemUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            cboSRItemUnit.Items.Add(new RadComboBoxItem("PCS", "PCS"));
        }

        public static void PopulateWithAllItemUnit(RadComboBox cboSRItemUnit)
        {
            cboSRItemUnit.Items.Clear();
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == "ItemUnit", query.IsActive == true);
            query.Select(query.ItemID, query.ItemName);
            query.OrderBy(query.ItemID.Ascending);
            DataTable dtb = query.LoadDataTable();

            cboSRItemUnit.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboSRItemUnit.Items.Add(new RadComboBoxItem(row["ItemID"].ToString(),
                                                       row["ItemID"].ToString()));
            }
        }

        public static void PopulateWithItemTypeProduct(RadComboBox cboItemType)
        {
            cboItemType.Items.Clear();

            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.StandardReferenceID == "ItemType",
                    query.IsActive == true,
                    query.ReferenceID == "Product"
                );

            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboItemType.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }

        public static void PopulateWithItemTypeProductPerUser(RadComboBox cboItemType, string userId)
        {
            cboItemType.Items.Clear();

            DataTable dtb = (new AppStandardReferenceItemCollection()).PopulateItemTypeProductPerUser(userId);

            cboItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboItemType.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }

            if (dtb.Rows.Count > 0)
                cboItemType.SelectedIndex = 1;
        }

        public static void PopulateWithItemTypeService(RadComboBox cboItemType)
        {
            cboItemType.Items.Clear();


            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.StandardReferenceID == "ItemType",
                    query.IsActive == true,
                    query.ReferenceID == "Service"
                );

            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboItemType.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }

        public static void PopulateWithItemTypeProduct(RadComboBox cboItemType, string transactionCode)
        {
            cboItemType.Items.Clear();


            var query = new AppStandardReferenceItemQuery();
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where
                (
                    query.StandardReferenceID == "ItemType",
                    query.IsActive == true,
                    query.ReferenceID == "Product"
                );

            query.OrderBy(query.ItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboItemType.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboItemType.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }

            var coll = new ServiceUnitTransactionCodeCollection();
            var tc = new ServiceUnitTransactionCodeQuery("a");
            var usr = new AppUserServiceUnitQuery("b");
            tc.InnerJoin(usr).On(tc.ServiceUnitID == usr.ServiceUnitID & usr.UserID == AppSession.UserLogin.UserID);
            tc.Where(tc.SRTransactionCode == transactionCode);
            tc.OrderBy(tc.IsItemProductMedic.Descending, tc.IsItemProductNonMedic.Descending,
                       tc.IsItemKitchen.Descending);
            coll.Load(tc);
            var itemTypeId = string.Empty;
            foreach (var c in coll)
            {
                if (itemTypeId == string.Empty)
                {
                    if (c.IsItemProductMedic == true)
                        itemTypeId = ItemType.Medical;
                    else if (c.IsItemProductNonMedic == true)
                        itemTypeId = ItemType.NonMedical;
                    else if (c.IsItemKitchen == true)
                        itemTypeId = ItemType.Kitchen;
                }
            }

            cboItemType.SelectedValue = itemTypeId;

        }

        public static void PopulateWithItemProductMargin(RadComboBox cboMarginID)
        {
            cboMarginID.Items.Clear();

            var query = new ItemProductMarginQuery();
            query.Select
                (
                    query.MarginID,
                    query.MarginName
                );
            query.Where(query.IsActive == true);
            query.OrderBy(query.MarginID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboMarginID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboMarginID.Items.Add(
                    new RadComboBoxItem(row["MarginName"].ToString(), row["MarginID"].ToString()));
            }
        }

        public static void PopulateWithClass(RadComboBox cboClassID)
        {
            cboClassID.Items.Clear();

            var qCl = new ClassQuery("a");

            qCl.Select
                (
                    qCl.ClassID,
                    qCl.ClassName
                );
            qCl.Where
                (
                    qCl.IsActive == true
                );
            qCl.OrderBy(qCl.ClassID.Ascending);

            DataTable dtb = qCl.LoadDataTable();

            cboClassID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboClassID.Items.Add(new RadComboBoxItem(row["ClassName"].ToString(),
                                                             row["ClassID"].ToString()));
            }
        }

        public static void PopulateWithClassTariff(RadComboBox cboClassID)
        {
            cboClassID.Items.Clear();

            var qCl = new ClassQuery("a");

            qCl.Select
            (
                qCl.ClassID,
                qCl.ClassName
            );
            qCl.Where
            (
                qCl.IsActive == true, qCl.IsTariffClass == true
            );
            qCl.OrderBy(qCl.ClassID.Ascending);

            DataTable dtb = qCl.LoadDataTable();

            cboClassID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboClassID.Items.Add(new RadComboBoxItem(row["ClassName"].ToString(),
                    row["ClassID"].ToString()));
            }
        }

        public static void PopulateWithInpatientClassTariff(RadComboBox cboClassID)
        {
            cboClassID.Items.Clear();

            var qCl = new ClassQuery("a");

            qCl.Select
            (
                qCl.ClassID,
                qCl.ClassName
            );
            qCl.Where
            (
                qCl.IsActive == true, qCl.IsInPatientClass == true, qCl.IsTariffClass == true
            );
            qCl.OrderBy(qCl.ClassID.Ascending);

            DataTable dtb = qCl.LoadDataTable();

            cboClassID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboClassID.Items.Add(new RadComboBoxItem(row["ClassName"].ToString(),
                    row["ClassID"].ToString()));
            }
        }

        public static void PopulateWithClassInpatient(RadComboBox cboClassID)
        {
            cboClassID.Items.Clear();
            var qCl = new ClassQuery("a");
            qCl.Select(qCl.ClassID, qCl.ClassName);
            qCl.Where(qCl.IsActive == true, qCl.IsInPatientClass == true);
            qCl.OrderBy(qCl.ClassID.Ascending);
            DataTable dtb = qCl.LoadDataTable();

            cboClassID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboClassID.Items.Add(new RadComboBoxItem(row["ClassName"].ToString(), row["ClassID"].ToString()));
            }
        }

        public static void PopulateWithSmf(RadComboBox cboSmfID)
        {
            cboSmfID.Items.Clear();

            var q = new SmfQuery("a");

            q.Select
                (
                    q.SmfID,
                    q.SmfName
                );
            q.OrderBy(q.SmfID.Ascending);

            DataTable dtb = q.LoadDataTable();

            cboSmfID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboSmfID.Items.Add(new RadComboBoxItem(row["SmfName"].ToString(), row["SmfID"].ToString()));
            }
        }

        public static void PopulateWithLabel(RadComboBox cboLabelID)
        {
            cboLabelID.Items.Clear();

            var qCl = new LabellQuery("a");

            qCl.Select
                (
                    qCl.LabelID,
                    qCl.LabelName
                );
            qCl.Where
                (
                    qCl.IsActive == true
                );
            qCl.OrderBy(qCl.LabelName.Ascending);

            DataTable dtb = qCl.LoadDataTable();

            cboLabelID.Items.Add(new RadComboBoxItem("", ""));
            foreach (DataRow row in dtb.Rows)
            {
                cboLabelID.Items.Add(new RadComboBoxItem(row["LabelName"].ToString(),
                                                             row["LabelID"].ToString()));
            }
        }

        public static void PopulateWithOneLabel(RadComboBox cboLabelID, string LabelID)
        {
            cboLabelID.Items.Clear();
            if (string.IsNullOrEmpty(LabelID))
                return;

            var coll = new LabellCollection();
            coll.Query.Where
                (
                    coll.Query.LabelID == LabelID,
                    coll.Query.IsActive == true
                );
            coll.LoadAll();

            foreach (Labell item in coll)
            {
                cboLabelID.Items.Add(new RadComboBoxItem(item.LabelName, item.LabelID));
            }

            cboLabelID.SelectedValue = LabelID;
        }

        public static void PopulateWithOneZatActive(RadComboBox cboZatActiveID, string ZatActiveID)
        {
            cboZatActiveID.Items.Clear();
            if (string.IsNullOrEmpty(ZatActiveID))
                return;

            var coll = new ZatActiveCollection();
            coll.Query.Where
                (
                    coll.Query.ZatActiveID == ZatActiveID
                );
            coll.LoadAll();

            foreach (ZatActive item in coll)
            {
                cboZatActiveID.Items.Add(new RadComboBoxItem(item.ZatActiveName, item.ZatActiveID));
            }

            cboZatActiveID.SelectedValue = ZatActiveID;
        }

        public static void PopulateWithOneIndication(RadComboBox cboIndicationID, string IndicationID)
        {
            cboIndicationID.Items.Clear();
            if (string.IsNullOrEmpty(IndicationID))
                return;

            var coll = new IndicationCollection();
            coll.Query.Where
                (
                    coll.Query.IndicationID == IndicationID,
                    coll.Query.IsActive == true
                );
            coll.LoadAll();

            foreach (Indication item in coll)
            {
                cboIndicationID.Items.Add(new RadComboBoxItem(item.IndicationName, item.IndicationID));
            }

            cboIndicationID.SelectedValue = IndicationID;
        }
        public static void PopulateWithOneDiagnose(RadComboBox cbo, string diagnoseID)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(diagnoseID))
                cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            else
            {
                var ent = new Diagnose();
                if (ent.LoadByPrimaryKey(diagnoseID))
                {
                    cbo.Items.Add(new RadComboBoxItem(ent.DiagnoseName, ent.DiagnoseID));
                }
                else
                {
                    cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                }
            }

            cbo.SelectedIndex = 0;
        }
        public static void PopulateWithOneProcedure(RadComboBox cbo, string procedureID)
        {
            cbo.Items.Clear();
            if (string.IsNullOrEmpty(procedureID))
                cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            else
            {
                var ent = new Procedure();
                if (ent.LoadByPrimaryKey(procedureID))
                {
                    cbo.Items.Add(new RadComboBoxItem(ent.ProcedureName, ent.ProcedureID));
                }
                else
                {
                    cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                }
            }

            cbo.SelectedIndex = 0;
        }

        public static void PopulateWithOneNursingDiagnosa(RadComboBox cboNursingDiagnosaID, string NursingDiagnosaID, string SRNursingDiagnosaLevel)
        {
            cboNursingDiagnosaID.Items.Clear();

            var coll = new NursingDiagnosaCollection();
            coll.Query.Where
                (
                    coll.Query.SRNursingDiagnosaLevel == SRNursingDiagnosaLevel,
                    coll.Query.IsActive == true
                ).OrderBy(coll.Query.NursingDiagnosaName.Ascending);
            coll.LoadAll();

            foreach (NursingDiagnosa item in coll)
            {
                cboNursingDiagnosaID.Items.Add(new RadComboBoxItem(item.NursingDiagnosaName, item.NursingDiagnosaID));
            }

            if (string.IsNullOrEmpty(NursingDiagnosaID))
                return;
            cboNursingDiagnosaID.SelectedValue = NursingDiagnosaID;
        }

        public static void PopulateWithOneNutritionCareAssessmentQuestionDiagnose(RadComboBox cboTerminologyId, string terminologyId, string srNutritionCareTerminologyLevel)
        {
            cboTerminologyId.Items.Clear();

            var coll = new NutritionCareTerminologyCollection();
            coll.Query.Where
            (
                coll.Query.SRNutritionCareTerminologyLevel == srNutritionCareTerminologyLevel,
                coll.Query.IsDetail == true,
                coll.Query.IsActive == true
            ).OrderBy(coll.Query.SequenceNo.Ascending);
            coll.LoadAll();

            foreach (NutritionCareTerminology item in coll)
            {
                cboTerminologyId.Items.Add(new RadComboBoxItem(item.TerminologyID + " - " + item.TerminologyName, item.TerminologyID));
            }

            if (string.IsNullOrEmpty(terminologyId))
                return;

            cboTerminologyId.SelectedValue = terminologyId;
        }

        public static void PopulateCboChartOfAccount(RadComboBox comboBox, int coaId)
        {
            ChartOfAccountsQuery coaQ = new ChartOfAccountsQuery();
            coaQ.Select(coaQ.ChartOfAccountId, coaQ.ChartOfAccountCode, coaQ.ChartOfAccountName);
            coaQ.Where(coaQ.ChartOfAccountId == coaId);
            DataTable dtbCoa = coaQ.LoadDataTable();
            comboBox.DataSource = dtbCoa;
            comboBox.DataBind();
            comboBox.SelectedValue = coaId.ToString();
        }

        public static void PopulateCboSubLedger(RadComboBox comboBox, int subLedgerID)
        {
            SubLedgersQuery slQ = new SubLedgersQuery();
            slQ.Select(slQ.SubLedgerId, slQ.SubLedgerName, slQ.Description);
            slQ.Where(slQ.SubLedgerId == subLedgerID);
            DataTable dtbSl = slQ.LoadDataTable();
            comboBox.DataSource = dtbSl;
            comboBox.DataBind();
            comboBox.SelectedValue = subLedgerID.ToString();
        }

        public static void PopulatePrescriptionLocation(RadComboBox cbo)
        {
            cbo.Items.Clear();

            var loc = new LocationQuery("loc");
            var su = new ServiceUnitQuery("su");
            var sul = new ServiceUnitLocationQuery("sul");
            var par1 = new AppParameterQuery("par1");
            var par2 = new AppParameterQuery("par2");

            loc.InnerJoin(sul).On(loc.LocationID == sul.LocationID)
                .InnerJoin(su).On(sul.ServiceUnitID == su.ServiceUnitID)
                .LeftJoin(par1).On(par1.ParameterID == "ServiceUnitPharmacyID" && par1.ParameterValue == su.ServiceUnitID)
                .LeftJoin(par2).On(par2.ParameterID == "ServiceUnitPharmacyIdOpr" && par2.ParameterValue == su.ServiceUnitID)
                .Where(su.IsActive == true, loc.IsActive == true)
                .Where(loc.Or(
                    su.IsDispensaryUnit,
                    par1.ParameterValue.Coalesce("''") != "",
                    par2.ParameterValue.Coalesce("''") != ""))
                .Select(loc.LocationID, loc.LocationName);

            loc.es.Distinct = true;

            DataTable dtb = loc.LoadDataTable();

            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cbo.Items.Add(
                    new RadComboBoxItem(row["LocationName"].ToString(), row["LocationID"].ToString()));
            }
        }

        public static void PopulateWorkTradeItemList(RadComboBox cbo, string srWorkTrade, bool isNew)
        {
            cbo.Items.Clear();
            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            if (srWorkTrade != string.Empty)
            {
                var sr = new AppStandardReferenceItemCollection();
                sr.Query.Where(sr.Query.StandardReferenceID == "WorkTradeItem", sr.Query.ReferenceID == srWorkTrade);

                if (isNew)
                    sr.Query.Where(sr.Query.IsActive == true);

                sr.LoadAll();

                foreach (AppStandardReferenceItem entity in sr)
                {
                    cbo.Items.Add(new RadComboBoxItem(entity.ItemName, entity.ItemID));
                }
            }
        }

        public static void PopulateMenuVersionSeqNoList(RadComboBox cbo, string versionId)
        {
            cbo.Items.Clear();
            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var mv = new MenuVersion();
            mv.LoadByPrimaryKey(versionId);
            var max = mv.Cycle + ((mv.IsPlusOneRule ?? false) ? 1 : 0);
            var i = 1;
            while (i <= max)
            {
                string.Format("{0:00}", i);
                cbo.Items.Add(new RadComboBoxItem(string.Format("{0:00}", i), string.Format("{0:00}", i)));
                i += 1;
            }
        }

        public static void PopulateWithDiagnoseSynonym(RadComboBox cboSynonym, string diagnoseID)
        {
            cboSynonym.Items.Clear();
            cboSynonym.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var query = new DiagnoseSynonymQuery("a");

            query.Select(query.SynonymText, query.SequenceNo);
            query.OrderBy(query.SequenceNo.Ascending);
            query.Where(query.DiagnoseID == diagnoseID);
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow item in dtb.Rows)
            {
                cboSynonym.Items.Add(new RadComboBoxItem(item["SynonymText"].ToString(), item["SequenceNo"].ToString()));
            }
        }

        public static void PopulateWithProcedureSynonym(RadComboBox cboSynonym, string procedureID)
        {
            cboSynonym.Items.Clear();
            cboSynonym.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

            var query = new ProcedureSynonymQuery("a");

            query.Select(query.SynonymText, query.SequenceNo);
            query.OrderBy(query.SequenceNo.Ascending);
            query.Where(query.ProcedureID == procedureID);
            DataTable dtb = query.LoadDataTable();

            foreach (DataRow item in dtb.Rows)
            {
                cboSynonym.Items.Add(new RadComboBoxItem(item["SynonymText"].ToString(), item["SequenceNo"].ToString()));
            }
        }

        #endregion

        #region Populate Item on Demand

        #region Supplier
        public static void SupplierItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new SupplierQuery("a");
            query.Where(
                query.IsActive == true,
                query.Or(query.SupplierID == textSearch, query.SupplierName.Like(searchTextContain))
                );
            query.Select(query.SupplierID, query.SupplierName);
            query.OrderBy(query.SupplierName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["SupplierName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SupplierID"].ToString();
            }
        }
        public static void SupplierItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[SupplierMetadata.ColumnNames.SupplierName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[SupplierMetadata.ColumnNames.SupplierID].ToString();
        }
        #endregion

        #region Customer
        public static void CustomerItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new CustomerQuery("a");
            query.Where(
                query.Or(query.CustomerID == textSearch, query.CustomerName.Like(searchTextContain))
                );
            query.Select(query.CustomerID, query.CustomerName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["CustomerName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["CustomerID"].ToString();
            }
        }
        public static void CustomerItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[CustomerMetadata.ColumnNames.CustomerName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[CustomerMetadata.ColumnNames.CustomerID].ToString();
        }
        #endregion

        #region Guarantor

        public static void GuarantorItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new GuarantorQuery("a");
            query.Where(
                query.Or(query.GuarantorID == textSearch, query.GuarantorName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.GuarantorID, query.GuarantorName);
            query.OrderBy(query.GuarantorName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["GuarantorName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["GuarantorID"].ToString();
            }
        }

        public static void EmployeeGuarantorItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new GuarantorQuery("a");
            query.Where(
                query.Or(query.GuarantorID == textSearch, query.GuarantorName.Like(searchTextContain)),
                query.SRGuarantorType == AppSession.Parameter.GuarantorTypeEmployee
                );
            query.Select(query.GuarantorID, query.GuarantorName);
            query.OrderBy(query.GuarantorName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["GuarantorName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["GuarantorID"].ToString();
            }
        }

        public static void GuarantorItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[GuarantorMetadata.ColumnNames.GuarantorName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[GuarantorMetadata.ColumnNames.GuarantorID].ToString();
        }
        #endregion

        #region Smf

        public static void SMFItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new SmfQuery("a");
            query.Where(
                query.Or(query.SmfID == textSearch, query.SmfName.Like(searchTextContain))
                );
            query.Select(query.SmfID, query.SmfName);
            query.OrderBy(query.SmfName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["SmfName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SmfID"].ToString();
            }
        }

        public static void SMFItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[SmfMetadata.ColumnNames.SmfName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[SmfMetadata.ColumnNames.SmfID].ToString();
        }
        #endregion

        //#region BankHRD

        //public static void BankHRDItemsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    comboBox.Items.Clear();
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    var query = new BankQuery("a");
        //    query.Where(
        //        query.Or(query.BankID == textSearch, query.BankName.Like(string.Format("%{0}%", textSearch)), query.BankID.Like(string.Format("%HRD%")))
        //        );
        //    query.Select(query.BankID, query.BankName);
        //    query.OrderBy(query.BankName.Ascending);
        //    query.es.Top = 20;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.DataSource = dtb;
        //    comboBox.DataBind();
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BankName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BankID"].ToString();
        //    }
        //}

        //public static void BankItemDataBound(RadComboBoxItemEventArgs e)
        //{
        //    e.Item.Text = ((DataRowView)e.Item.DataItem)[BankMetadata.ColumnNames.BankName].ToString();
        //    e.Item.Value = ((DataRowView)e.Item.DataItem)[BankMetadata.ColumnNames.BankID].ToString();
        //}
        //#endregion



        #region Class

        public static void ClassItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ClassQuery("a");
            query.Where(
                query.IsActive == true,
                query.Or(query.ClassID == textSearch, query.ClassName.Like(searchTextContain))
                );
            query.Select(query.ClassID, query.ClassName);
            query.OrderBy(query.ClassID.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ClassName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ClassID"].ToString();
            }
        }

        public static void ClassItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ClassMetadata.ColumnNames.ClassName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ClassMetadata.ColumnNames.ClassID].ToString();
        }
        #endregion

        #region ChartOfAccounts

        public static void ChartOfAccountItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ChartOfAccountsQuery("a");
            query.Where(
                        query.Or
                        (
                            query.ChartOfAccountCode.Like(searchTextContain),
                            query.ChartOfAccountName.Like(searchTextContain)
                        )
                       );
            query.Where(query.IsDetail == 1);
            query.Select(query.ChartOfAccountCode, query.ChartOfAccountName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ChartOfAccountName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ChartOfAccountCode"].ToString();
            }
        }
        public static void ChartOfAccountItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ChartOfAccountsMetadata.ColumnNames.ChartOfAccountName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ChartOfAccountsMetadata.ColumnNames.ChartOfAccountCode].ToString();
        }
        #endregion

        #region SubledgerProfitCost
        //untuk mengeluarkan unit
        public static void SublegderItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new SubLedgersQuery("a");
            query.Where(
                query.Description.Like(searchTextContain),
                query.GroupId == AppSession.Parameter.SubLedgerGroupIdServiceUnit

            );
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["Description"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["Description"].ToString();
            }
        }
        public static void SubledgerItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[SubLedgersMetadata.ColumnNames.Description].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[SubLedgersMetadata.ColumnNames.Description].ToString();
        }
        #endregion

        #region ServiceRoom

        public static void ServiceRoomItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceRoomQuery("a");
            query.Where(
                query.Or(query.RoomID == textSearch, query.RoomName.Like(searchTextContain))
                );
            query.Where(query.ServiceUnitID == AppSession.Parameter.InPatientServiceUnitID);
            query.Select(query.RoomID, query.RoomName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["RoomName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["RoomID"].ToString();
            }
        }

        public static void ServiceRoomItemsRequested(RadComboBox comboBox, string textSearch, string unitId)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceRoomQuery("a");
            query.Where(
                query.Or(query.RoomID == textSearch, query.RoomName.Like(searchTextContain))
                );
            query.Where(query.ServiceUnitID == unitId);
            query.Select(query.RoomID, query.RoomName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["RoomName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["RoomID"].ToString();
            }
        }
        public static void ServiceRoomItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ServiceRoomMetadata.ColumnNames.RoomName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ServiceRoomMetadata.ColumnNames.RoomID].ToString();
        }

        public static void OutPatientRoomItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceRoomQuery("a");
            var unit = new ServiceUnitQuery("b");
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID &
                  unit.DepartmentID == AppSession.Parameter.OutPatientDepartmentID);

            query.Where(
                query.Or(query.RoomID == textSearch, query.RoomName.Like(searchTextContain))
                );
            query.Select(query.RoomID, query.RoomName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["RoomName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["RoomID"].ToString();
            }
        }

        public static void OutPatientRoomItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ServiceRoomMetadata.ColumnNames.RoomName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ServiceRoomMetadata.ColumnNames.RoomID].ToString();
        }
        #endregion

        #region ImpressionGroup
        public static void ImpressionGroupItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new PathologyAnatomyImpressionGroupQuery("a");
            query.Where(
                query.Or(query.GroupID == textSearch, query.GroupName.Like(searchTextContain))
            );
            query.Select(query.GroupID, query.GroupName);
            query.OrderBy(query.GroupName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["GroupName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["GroupID"].ToString();
            }
        }

        public static void ImpressionGroupItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[PathologyAnatomyImpressionGroupMetadata.ColumnNames.GroupID].ToString();
        }
        #endregion

        #region Bank

        public static void BankItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new BankQuery("a");
            query.Where(
                query.Or(query.BankID == textSearch, query.BankName.Like(searchTextContain)));
            query.Select(query.BankID, query.BankName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.Items.Clear();
            foreach (DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["BankName"].ToString(), row["BankID"].ToString()));
            }
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["BankName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["BankID"].ToString();
            }
        }

        #endregion

        #region Bank A/C No

        //public static void BankAccountItemsRequested(RadComboBox comboBox, string textBankID, string textSearch)
        //{
        //    if (textBankID == null)
        //        textBankID = string.Empty;

        //    if (textSearch == null)
        //        textSearch = string.Empty;

        //    BankAccountQuery query = new BankAccountQuery("a");
        //    query.Where(
        //        query.Or(query.BankID == textBankID, query.BankAccountNo == textSearch, query.Notes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BankAccountNo, query.Notes);

        //    query.es.Top = 20;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["Notes"].ToString(), row["BankAccountNo"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["Notes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BankAccountNo"].ToString();
        //    }
        //}

        #endregion


        #region EDC
        public static void EDCItemsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new EDCMachineQuery("a");
            query.Where(
                query.IsActive == true,
                query.Or(query.EDCMachineID == textSearch, query.EDCMachineName.Like(searchTextContain))
                );
            query.Select(query.EDCMachineID, query.EDCMachineName);
            query.OrderBy(query.EDCMachineID.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["EDCMachineName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["EDCMachineID"].ToString();
            }
        }

        public static void EDCItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[EDCMachineMetadata.ColumnNames.EDCMachineName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[EDCMachineMetadata.ColumnNames.EDCMachineID].ToString();
        }
        #endregion

        #region Finance

        //public static void AcctItemsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}
        //public static void AcctLinkEmployeesRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(query.SRAcctLink.Trim().Equal(AppConstant.BillingLink.EMPLOYEE));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}
        //public static void AcctLinkGuarantorsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(query.SRAcctLink.Trim().Equal(AppConstant.BillingLink.GUARANTOR));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}
        //public static void AcctLinkSuppliersRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(query.SRAcctLink.Trim().Equal(AppConstant.BillingLink.SUPPLIER));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}
        //public static void AcctLinkLoanBanksRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(query.SRAcctLink.Trim().Equal(AppConstant.BillingLink.LOAN_BANK));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}

        //public static void AcctInitialGLsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AccountsQuery query = new AccountsQuery("a");
        //    query.Where(query.SRAcctLevel.Trim().GreaterThan("3"));
        //    query.Where(query.SRAcctGroup.Trim().LessThan(AppConstant.AcctGroup.REVENUE));
        //    query.Where(
        //        query.Or(query.AccountID == textSearch, query.AccountName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AccountID, query.AccountName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AccountName"].ToString(), row["AccountID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AccountName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AccountID"].ToString();
        //    }
        //}
        //public static void AcctSubGroupsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    AcctSubGroupQuery query = new AcctSubGroupQuery("a");
        //    query.Where(
        //        query.Or(query.AcctSubGroupID == textSearch, query.AcctSubGroupName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.AcctSubGroupID, query.AcctSubGroupName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["AcctSubGroupName"].ToString(), row["AcctSubGroupID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["AcctSubGroupName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["AcctSubGroupID"].ToString();
        //    }
        //}
        //public static void BankAccountsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BankAccountQuery query = new BankAccountQuery("a");
        //    query.Where(
        //        query.Or(query.BankAccountNo == textSearch, query.Notes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BankAccountNo, query.Notes);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["Notes"].ToString(), row["BankAccountNo"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["Notes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BankAccountNo"].ToString();
        //    }
        //}
        //public static void BanksRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BankQuery query = new BankQuery("a");
        //    query.Where(
        //        query.Or(query.BankID == textSearch, query.BankName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BankID, query.BankName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BankName"].ToString(), row["BankID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BankName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BankID"].ToString();
        //    }
        //}
        //public static void DonatorsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    DonatorQuery query = new DonatorQuery("a");
        //    query.Where(
        //        query.Or(query.DonatorID == textSearch, query.DonatorName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.DonatorID, query.DonatorName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["DonatorName"].ToString(), row["DonatorID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["DonatorName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["DonatorID"].ToString();
        //    }
        //}
        //public static void EmployeesRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    EmployeeQuery query = new EmployeeQuery("a");
        //    query.Where(
        //        query.Or(query.EmployeeID == textSearch, query.EmployeeName.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.EmployeeID, query.EmployeeName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["EmployeeName"].ToString(), row["EmployeeID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["EmployeeName"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["EmployeeID"].ToString();
        //    }
        //}

        //public static void VoucherCodeReceivesRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    VoucherCodeQuery query = new VoucherCodeQuery("a");
        //    query.Where(query.SRVoucherType == AppConstant.VoucherType.RECEIVE);
        //    query.Where(
        //        query.Or(query.VoucherCode == textSearch, query.VoucherNote.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.VoucherCode, query.VoucherNote);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["VoucherNote"].ToString(), row["VoucherCode"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["VoucherNote"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["VoucherCode"].ToString();
        //    }
        //}
        //public static void VoucherCodePaymentsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    VoucherCodeQuery query = new VoucherCodeQuery("a");
        //    query.Where(query.SRVoucherType == AppConstant.VoucherType.PAYMENT);
        //    query.Where(
        //        query.Or(query.VoucherCode == textSearch, query.VoucherNote.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.VoucherCode, query.VoucherNote);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["VoucherNote"].ToString(), row["VoucherCode"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["VoucherNote"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["VoucherCode"].ToString();
        //    }
        //}
        //public static void VoucherCodeMemorialsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    VoucherCodeQuery query = new VoucherCodeQuery("a");
        //    query.Where(query.SRVoucherType == AppConstant.VoucherType.MEMORIAL);
        //    query.Where(
        //        query.Or(query.VoucherCode == textSearch, query.VoucherNote.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.VoucherCode, query.VoucherNote);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["VoucherNote"].ToString(), row["VoucherCode"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["VoucherNote"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["VoucherCode"].ToString();
        //    }
        //}
        //public static void VoucherCodeAutomaticsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    VoucherCodeQuery query = new VoucherCodeQuery("a");
        //    query.Where(query.SRVoucherType == AppConstant.VoucherType.AUTOMATIC);
        //    query.Where(
        //        query.Or(query.VoucherCode == textSearch, query.VoucherNote.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.VoucherCode, query.VoucherNote);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["VoucherNote"].ToString(), row["VoucherCode"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["VoucherNote"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["VoucherCode"].ToString();
        //    }
        //}

        //public static void BillGurantorsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BillingMasterQuery query = new BillingMasterQuery("a");
        //    GuarantorQuery qgua = new GuarantorQuery("b");

        //    query.LeftJoin(qgua).On(query.SubsidiaryID == qgua.GuarantorID);
        //    query.Where(query.AccountID.Equal(AppSession.AcctBillingID.AccountID));
        //    query.Where(query.SRAcctLink.Equal(AppSession.AcctBillingID.SRAcctLink));
        //    query.Where(query.SRAcctSubsidiary.Equal(AppSession.AcctBillingID.SRAcctSubsidiary));
        //    query.Where(query.SubsidiaryID.Equal(AppSession.AcctGuarantorID.GuarantorID));
        //    query.Where(query.BillingConvert.NotEqual(query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert));
        //    query.Where(
        //        query.Or(query.BillingID == textSearch, query.BillingNotes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BillingID, query.SubsidiaryID,
        //                 query.BillingDate, query.BillingDueDate,
        //                 query.SRCurrency, query.BillingConvert.As("BillingAmount"),
        //                 (query.BillingConvert - (query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert)).As("BalanceAmount"),
        //                 qgua.GuarantorName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BillingNotes"].ToString(), row["BillingID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BillingNotes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BillingID"].ToString();
        //    }
        //}
        //public static void BillSuppliersRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BillingMasterQuery query = new BillingMasterQuery("a");
        //    SupplierQuery qsup = new SupplierQuery("b");

        //    query.LeftJoin(qsup).On(query.SubsidiaryID == qsup.SupplierID);
        //    query.Where(query.AccountID.Equal(AppSession.AcctBillingID.AccountID));
        //    query.Where(query.SRAcctLink.Equal(AppSession.AcctBillingID.SRAcctLink));
        //    query.Where(query.SRAcctSubsidiary.Equal(AppSession.AcctBillingID.SRAcctSubsidiary));
        //    query.Where(query.SubsidiaryID.Equal(AppSession.AcctSupplierID.SupplierID));
        //    query.Where(query.BillingConvert.NotEqual(query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert));
        //    query.Where(
        //        query.Or(query.BillingID == textSearch, query.BillingNotes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BillingID, query.SubsidiaryID,
        //                 query.BillingDate, query.BillingDueDate,
        //                 query.SRCurrency, query.BillingConvert.As("BillingAmount"),
        //                 (query.BillingConvert - (query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert)).As("BalanceAmount"),
        //                 qsup.SupplierName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BillingNotes"].ToString(), row["BillingID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BillingNotes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BillingID"].ToString();
        //    }
        //}
        //public static void BillDonatorsRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BillingMasterQuery query = new BillingMasterQuery("a");
        //    DonatorQuery qdon = new DonatorQuery("b");

        //    query.LeftJoin(qdon).On(query.SubsidiaryID == qdon.DonatorID);
        //    query.Where(query.AccountID.Equal(AppSession.AcctBillingID.AccountID));
        //    query.Where(query.SRAcctLink.Equal(AppSession.AcctBillingID.SRAcctLink));
        //    query.Where(query.SRAcctSubsidiary.Equal(AppSession.AcctBillingID.SRAcctSubsidiary));
        //    query.Where(query.SubsidiaryID.Equal(AppSession.AcctDonatorID.DonatorID));
        //    query.Where(query.BillingConvert.NotEqual(query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert));
        //    query.Where(
        //        query.Or(query.BillingID == textSearch, query.BillingNotes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BillingID, query.SubsidiaryID,
        //                 query.BillingDate, query.BillingDueDate,
        //                 query.SRCurrency, query.BillingConvert.As("BillingAmount"),
        //                 (query.BillingConvert - (query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert)).As("BalanceAmount"),
        //                 qdon.DonatorName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BillingNotes"].ToString(), row["BillingID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BillingNotes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BillingID"].ToString();
        //    }
        //}
        //public static void BillEmployeesRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BillingMasterQuery query = new BillingMasterQuery("a");
        //    EmployeeQuery qempl = new EmployeeQuery("b");

        //    query.LeftJoin(qempl).On(query.SubsidiaryID == qempl.EmployeeID);
        //    query.Where(query.AccountID.Equal(AppSession.AcctBillingID.AccountID));
        //    query.Where(query.SRAcctLink.Equal(AppSession.AcctBillingID.SRAcctLink));
        //    query.Where(query.SRAcctSubsidiary.Equal(AppSession.AcctBillingID.SRAcctSubsidiary));
        //    query.Where(query.SubsidiaryID.Equal(AppSession.AcctEmployeeID.EmployeeID));
        //    query.Where(query.BillingConvert.NotEqual(query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert));
        //    query.Where(
        //        query.Or(query.BillingID == textSearch, query.BillingNotes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BillingID, query.SubsidiaryID,
        //                 query.BillingDate, query.BillingDueDate,
        //                 query.SRCurrency, query.BillingConvert.As("BillingAmount"),
        //                 (query.BillingConvert - (query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert)).As("BalanceAmount"),
        //                 qempl.EmployeeName);

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BillingNotes"].ToString(), row["BillingID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BillingNotes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BillingID"].ToString();
        //    }
        //}
        //public static void BillLoanBanksRequested(RadComboBox comboBox, string textSearch)
        //{
        //    if (textSearch == null)
        //        textSearch = string.Empty;
        //    BillingMasterQuery query = new BillingMasterQuery("a");
        //    BankAccountQuery qbac = new BankAccountQuery("b");

        //    query.LeftJoin(qbac).On(query.SubsidiaryID == qbac.BankAccountNo);
        //    query.Where(query.AccountID.Equal(AppSession.AcctBillingID.AccountID));
        //    query.Where(query.SRAcctLink.Equal(AppSession.AcctBillingID.SRAcctLink));
        //    query.Where(query.SRAcctSubsidiary.Equal(AppSession.AcctBillingID.SRAcctSubsidiary));
        //    query.Where(query.SubsidiaryID.Equal(AppSession.AcctBankAccountID.BankAccountNo));
        //    query.Where(query.BillingConvert.NotEqual(query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert));
        //    query.Where(
        //        query.Or(query.BillingID == textSearch, query.BillingNotes.Like(string.Format("%{0}%", textSearch)))
        //        );
        //    query.Select(query.BillingID, query.SubsidiaryID,
        //                 query.BillingDate, query.BillingDueDate,
        //                 query.SRCurrency, query.BillingConvert.As("BillingAmount"),
        //                 (query.BillingConvert - (query.LastPaidConvert + query.TransPaidConvert + query.ReturnPaidConvert)).As("BalanceAmount"),
        //                 qbac.Notes.As("BankNotes"));

        //    query.es.Top = AppSession.Parameter.MaxResultRecord;
        //    DataTable dtb = query.LoadDataTable();
        //    comboBox.Items.Clear();
        //    foreach (DataRow row in dtb.Rows)
        //    {
        //        comboBox.Items.Add(new RadComboBoxItem(row["BillingNotes"].ToString(), row["BillingID"].ToString()));
        //    }
        //    if (dtb.Rows.Count > 0)
        //    {
        //        comboBox.Text = dtb.Rows[0]["BillingNotes"].ToString();
        //        comboBox.SelectedValue = dtb.Rows[0]["BillingID"].ToString();
        //    }
        //}

        #endregion

        #region Item Entry

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
                                              string locationID, string supplierID, bool isDirectPurchase, bool? isConsignment, string srItemCategory, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");

            query.LeftJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationID
                );
            query.Where(query.SRItemType == itemType);
            if (isNew)
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where
                (

                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            }
            else
                query.Where(query.ItemID == textSearch);

            if (!string.IsNullOrEmpty(srItemCategory))
                query.Where(query.SRItemCategory == srItemCategory);

            var prod = new ItemProductMedicQuery("c");
            var std = new AppStandardReferenceItemQuery("d");
            var suppItem = new SupplierItemQuery("e");

            var nmed = new ItemProductNonMedicQuery("f");
            var kitchen = new ItemKitchenQuery("g");

            if (itemType == ItemType.Medical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(prod.IsDirectPurchase == isDirectPurchase, prod.IsConsignment == isConsignment);
            }
            else if (itemType == ItemType.NonMedical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID);
                query.LeftJoin(std).On
                    (
                        nmed.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(nmed.IsConsignment == isConsignment);
            }
            else if (itemType == ItemType.Kitchen)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                query.LeftJoin(std).On
                    (
                        kitchen.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
            }

            if (supplierID != string.Empty)
                query.LeftJoin(suppItem).On(query.ItemID == suppItem.ItemID & suppItem.SupplierID == supplierID);

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }
        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
            string locationID, string supplierID, bool isDirectPurchase, bool isInventoryItem, bool? isConsignment, bool isNew)
        {
            ItemItemsRequested(comboBox, textSearch, itemType,
                locationID, supplierID, isDirectPurchase, isInventoryItem, isConsignment, string.Empty, string.Empty, string.Empty, isNew);
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
            string locationID, string supplierID, bool isDirectPurchase, bool isInventoryItem, bool? isConsignment,
            string supType, string itemGroupID, string srItemCategory, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var x = AppSession.Parameter.IsDistributionRequestBasedOnItemsPerLocation;

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");

            // Untuk kasus Distributon Request, boleh memilih obat walaupun belum di mapping di lokasinya (Handono & Teguh S)
            if (x)
            {
                query.InnerJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationID
                );
            }
            else
            {
                query.LeftJoin(bal).On
                 (
                     query.ItemID == bal.ItemID &
                     bal.LocationID == locationID
                 );
            }

            query.Where
                (
                    query.SRItemType == itemType
                );

            if (!string.IsNullOrEmpty(srItemCategory))
                query.Where(query.SRItemCategory == srItemCategory);

            var prod = new ItemProductMedicQuery("c");
            var std = new AppStandardReferenceItemQuery("d");


            var nmed = new ItemProductNonMedicQuery("f");
            var kitchen = new ItemKitchenQuery("g");

            if (itemType == ItemType.Medical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );

                query.Where(prod.IsDirectPurchase == isDirectPurchase, prod.IsInventoryItem == isInventoryItem);
                if (isConsignment.HasValue)
                    query.Where(prod.IsConsignment == isConsignment);

                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.IsActive == true,
                    query.Or
                        (
                            prod.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else 
                    query.Where(query.ItemID == textSearch);
            }
            else if (itemType == ItemType.NonMedical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID);
                query.LeftJoin(std).On
                    (
                        nmed.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(nmed.IsInventoryItem == isInventoryItem);
                if (isConsignment.HasValue)
                    query.Where(nmed.IsConsignment == isConsignment);

                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.IsActive == true,
                    query.Or
                        (
                            nmed.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else
                    query.Where(query.ItemID == textSearch);
            }
            else if (itemType == ItemType.Kitchen)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                query.LeftJoin(std).On
                    (
                        kitchen.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                
                query.Where(kitchen.IsInventoryItem == isInventoryItem);
                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.IsActive == true,
                    query.Or
                        (
                            kitchen.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else
                    query.Where(query.ItemID == textSearch);
            }

            var suppItem = new SupplierItemQuery("e");
            if (!string.IsNullOrEmpty(supplierID) || !string.IsNullOrEmpty(supType))
                query.InnerJoin(suppItem).On(query.ItemID == suppItem.ItemID & suppItem.SupplierID == supplierID);

            if (!string.IsNullOrEmpty(itemGroupID))
            {
                query.Where(query.ItemGroupID == itemGroupID);
            }

            if (AppSession.Parameter.IsDistributionRequestOnlyForUnderMinValue)
                query.Where(bal.Balance < bal.Minimum);

            query.OrderBy(query.ItemName.Ascending);

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
            string locationID, string supplierID, string productAccountID, bool isDirectPurchase, bool isInventoryItem,
            bool isConsignment, bool isPorByStockGroup, string srPurchaseCategorization, bool isPerLocation, string srItemCategory, bool isFilterAsset, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ItemQuery("a");
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );

            if (isFilterAsset)
            {
                query.Where(query.IsAsset == true);
            }

            // Base Unit
            var nonMed = new ItemProductNonMedicQuery("i2");
            var kitchen = new ItemKitchenQuery("i2");
            var med = new ItemProductMedicQuery("i2");
            var std = new AppStandardReferenceItemQuery("d");

            switch (itemType)
            {
                case ItemType.NonMedical:
                    query.InnerJoin(nonMed).On(query.ItemID == nonMed.ItemID);
                    query.LeftJoin(std).On
                        (
                            nonMed.SRItemUnit == std.ItemID &
                            std.StandardReferenceID == "ItemUnit"
                        );
                    query.Where(nonMed.IsInventoryItem == isInventoryItem, nonMed.IsConsignment == isConsignment);
                    if (!string.IsNullOrEmpty(srPurchaseCategorization))
                        query.Where(nonMed.SRPurchaseCategorization == srPurchaseCategorization);

                    break;
                case ItemType.Kitchen:
                    query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                    query.LeftJoin(std).On
                        (
                            kitchen.SRItemUnit == std.ItemID &
                            std.StandardReferenceID == "ItemUnit"
                        );
                    query.Where(kitchen.IsInventoryItem == isInventoryItem);
                    break;
                default:
                    query.InnerJoin(med).On(query.ItemID == med.ItemID);
                    query.LeftJoin(std).On
                        (
                            med.SRItemUnit == std.ItemID &
                            std.StandardReferenceID == "ItemUnit"
                        );
                    query.Where(med.IsDirectPurchase == isDirectPurchase, med.IsInventoryItem == isInventoryItem,
                                med.IsConsignment == isConsignment);
                    break;
            }

            // Balance Min Max
            if (isPorByStockGroup)
            {
                var dayForMinStock =
                    AppSession.Parameter.GetParameterValue(
                        AppParameter.ParameterItem.PeriodDayHistUsingForCalcMinBalance).ToInt();

                var stockGroup = "-";
                var ibq = new ItemBalanceByStockGroupQuery("c");
                var loc = new Location();
                loc.LoadByPrimaryKey(locationID);
                if (!string.IsNullOrEmpty(loc.SRStockGroup))
                    stockGroup = loc.SRStockGroup;
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.SRStockGroup == stockGroup);

                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceSG>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    String.Format("<{0} AS DayForMinStock>", dayForMinStock),
                    String.Format("<'{0}' AS SRStockGroup>", stockGroup)
                    );

                //Sub Query Sales n out per periode Minimum stock
                var sqSales = new ItemSalesPerDateQuery("ispd");
                sqSales.Select(sqSales.QuantityOut.Sum().As("QuantityOut"));
                sqSales.Where(sqSales.SRStockGroup == stockGroup, sqSales.ItemID == query.ItemID,
                    sqSales.MovementDate >= DateTime.Today.AddDays(0 - dayForMinStock));

                query.Select(sqSales.Select().As("QuantityOut"));

                //Sub Query Sales n out per periode Minimum stock
                var sqBalLoc = new ItemBalanceQuery("ibl");
                sqBalLoc.Select(sqBalLoc.Balance);
                sqBalLoc.Where(sqBalLoc.LocationID == locationID, sqBalLoc.ItemID == query.ItemID);
                query.Select(sqBalLoc.Select().As("BalanceLoc"));
            }
            else
            {
                var ibq = new ItemBalanceQuery("c");
                if (string.IsNullOrEmpty(locationID))
                    locationID = "-";
                query.LeftJoin(ibq).On(query.ItemID == ibq.ItemID && ibq.LocationID == locationID);

                query.Select(@"<CONVERT(decimal(10,2),COALESCE(c.Balance,0)) AS BalanceLoc>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Minimum,0)) AS Minimum>",
                    @"<CONVERT(decimal(10,2),COALESCE(c.Maximum,0)) AS Maximum>",
                    "<'-' AS DayForMinStock>",
                    String.Format("<'{0}' AS SRStockGroup>", "-"),
                    @"<CONVERT(decimal(10,2),0) AS QuantityOut>",
                    @"<CONVERT(decimal(10,2),0) AS BalanceSG>"
                    );
            }
            // Sub Query BalanceTotal
            var itemBalTot = new ItemBalanceQuery("ibt");
            itemBalTot.Select((itemBalTot.Balance.Sum().As("BalanceTotal")));
            itemBalTot.Where(itemBalTot.ItemID == query.ItemID);
            query.Select(itemBalTot.Select().As("BalanceTotal"));

            query.Select(std.ItemName.As("Unit"));

            query.Where(query.SRItemType == itemType);
            if (isNew)
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            }
            else
                query.Where(query.ItemID == textSearch);
            if (!string.IsNullOrEmpty(srItemCategory))
                query.Where(query.SRItemCategory == srItemCategory);

            var suppItem = new SupplierItemQuery("e");
            if (supplierID != string.Empty)
                query.InnerJoin(suppItem).On(query.ItemID == suppItem.ItemID & suppItem.SupplierID == supplierID);

            if (!string.IsNullOrEmpty(productAccountID))
                query.Where(query.ProductAccountID == productAccountID);

            if (isPerLocation && isInventoryItem)
            {
                var iBalq = new ItemBalanceQuery("iBalq");
                query.InnerJoin(iBalq).On(iBalq.LocationID == locationID & iBalq.ItemID == query.ItemID);
            }

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
            string locationID, string supplierID, string contractNo, bool isInventoryItem, bool isConsignment, bool isAssets, bool isNew)
        {
            ItemItemsRequested(comboBox, textSearch, itemType, locationID, supplierID, contractNo, isInventoryItem, isConsignment, string.Empty, string.Empty, isAssets, isNew);
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType,
            string locationID, string supplierID, string contractNo, bool isInventoryItem, bool isConsignment,
            string supType, string srItemCategory, bool isAssets, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");

            query.LeftJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationID
                );

            query.Where(query.SRItemType == itemType, query.IsActive == true);
            if (!string.IsNullOrEmpty(srItemCategory))
                query.Where(query.SRItemCategory == srItemCategory);
            //if (AppSession.Application.IsModuleAssetActive)
            //{
            //    if (isAssets)
            //        query.Where(query.IsAsset == true);
            //    else
            //        query.Where(query.Or(query.IsAsset == false, query.IsAsset.IsNull()));
            //}

            var prod = new ItemProductMedicQuery("c");
            var std = new AppStandardReferenceItemQuery("d");

            var nmed = new ItemProductNonMedicQuery("f");
            var kitchen = new ItemKitchenQuery("g");

            if (itemType == ItemType.Medical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID && prod.IsInventoryItem == isInventoryItem &&
                                         prod.IsConsignment == isConsignment);
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.Or
                        (
                            prod.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else
                    query.Where(query.ItemID == textSearch);

            }
            else if (itemType == ItemType.NonMedical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID && nmed.IsInventoryItem == isInventoryItem &&
                                         nmed.IsConsignment == isConsignment);
                query.LeftJoin(std).On
                    (
                        nmed.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.Or
                        (
                            nmed.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else
                    query.Where(query.ItemID == textSearch);
            }
            else if (itemType == ItemType.Kitchen)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                query.InnerJoin(kitchen)
                    .On(query.ItemID == kitchen.ItemID && kitchen.IsInventoryItem == isInventoryItem);
                query.LeftJoin(std).On
                    (
                        kitchen.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );

                if (isNew)
                {
                    string searchTextContain = string.Format("%{0}%", textSearch);
                    query.Where(
                    query.Or
                        (
                            kitchen.Barcode == textSearch,
                            query.ItemName.Like(searchTextContain),
                            query.ItemID.Like(searchTextContain)
                        )
                    );
                }
                else
                    query.Where(query.ItemID == textSearch);
            }

            bool isFilterItemBySupplier = !string.IsNullOrEmpty(supType);
            var suppItem = new SupplierItemQuery("e");
            if (supplierID != string.Empty)
            {
                if (contractNo != string.Empty)
                {
                    if (AppSession.Parameter.HealthcareInitial != "RSCH")
                    {
                        var scItem = new SupplierContractItemQuery("x");
                        scItem.Where(scItem.TransactionNo == contractNo, scItem.IsActive == true);
                        DataTable dtsci = scItem.LoadDataTable();
                        if (dtsci.Rows.Count > 0)
                            query.InnerJoin(scItem).On(query.ItemID == scItem.ItemID && scItem.IsActive == true);
                        else if (isFilterItemBySupplier)
                            query.InnerJoin(suppItem)
                                .On(query.ItemID == suppItem.ItemID && suppItem.SupplierID == supplierID);
                    }
                    else if (isFilterItemBySupplier)
                        query.InnerJoin(suppItem)
                            .On(query.ItemID == suppItem.ItemID && suppItem.SupplierID == supplierID);
                }
                else if (isFilterItemBySupplier)
                    query.InnerJoin(suppItem).On(query.ItemID == suppItem.ItemID && suppItem.SupplierID == supplierID);
            }
            else if (isFilterItemBySupplier)
                query.InnerJoin(suppItem).On(query.ItemID == suppItem.ItemID && suppItem.SupplierID == supplierID);

            query.OrderBy(query.ItemName.Ascending);

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType, string locationID, string itemGroupID, bool isNew)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var x = AppSession.Parameter.IsDistributionRequestBasedOnItemsPerLocation;

            var query = new ItemQuery("a");
            var bal = new ItemBalanceQuery("b");
            var prod = new ItemProductMedicQuery("c");
            var std = new AppStandardReferenceItemQuery("d");

            var nmed = new ItemProductNonMedicQuery("f");
            var kitchen = new ItemKitchenQuery("g");

            if (itemType == ItemType.Medical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );

                // Untuk kasus Distributon Request, boleh memilih obat walaupun belum di mapping di lokasinya (Handono & Teguh S)
                if (x)
                {
                    query.InnerJoin(bal).On
                    (
                        query.ItemID == bal.ItemID &
                        bal.LocationID == locationID
                    );
                }
                else
                {
                    query.LeftJoin(bal).On
                (
                    query.ItemID == bal.ItemID &
                    bal.LocationID == locationID
                );
                }
                query.InnerJoin(prod).On(query.ItemID == prod.ItemID);
                query.LeftJoin(std).On
                    (
                        prod.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(prod.IsInventoryItem == true);
            }
            else if (itemType == ItemType.NonMedical)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );

                if (x)
                {
                    query.InnerJoin(bal).On
                    (
                        query.ItemID == bal.ItemID &
                        bal.LocationID == locationID
                    );
                }
                else
                {
                    query.LeftJoin(bal).On
                    (
                        query.ItemID == bal.ItemID &
                        bal.LocationID == locationID
                    );
                }

                query.InnerJoin(nmed).On(query.ItemID == nmed.ItemID);
                query.LeftJoin(std).On
                    (
                        nmed.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(nmed.IsInventoryItem == true);
            }
            else if (itemType == ItemType.Kitchen)
            {
                query.Select
                    (
                        query.ItemID,
                        query.ItemName,
                        bal.Balance,
                        bal.Minimum,
                        bal.Maximum,
                        std.ItemName.As("Unit")
                    );
                if (x)
                {
                    query.InnerJoin(bal).On
                    (
                        query.ItemID == bal.ItemID &
                        bal.LocationID == locationID
                    );
                }
                else
                {
                    query.LeftJoin(bal).On
                    (
                        query.ItemID == bal.ItemID &
                        bal.LocationID == locationID
                    );
                }
                query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                query.LeftJoin(std).On
                    (
                        kitchen.SRItemUnit == std.ItemID &
                        std.StandardReferenceID == "ItemUnit"
                    );
                query.Where(kitchen.IsInventoryItem == true);
            }

            if (!string.IsNullOrEmpty(itemGroupID))
            {
                query.Where(query.ItemGroupID == itemGroupID);
            }
            query.Where(query.SRItemType == itemType);
            if (isNew)
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            }
            else
                query.Where(query.ItemID == textSearch);

            if (AppSession.Parameter.IsDistributionRequestOnlyForUnderMinValue)
                query.Where(bal.Balance < bal.Minimum);

            query.OrderBy(query.ItemName.Ascending);

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemItemsRequested(RadComboBox comboBox, string textSearch, string itemType)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ItemQuery("a");
            string searchTextContain = string.Format("%{0}%", textSearch);
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            var med = new ItemProductMedicQuery("f");
            var nonmed = new ItemProductNonMedicQuery("g");
            var lab = new ItemLaboratoryQuery("h");
            var rad = new ItemRadiologyQuery("i");
            var kitchen = new ItemKitchenQuery("j");

            switch (itemType)
            {
                default:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.Where(query.SRItemType.NotIn(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen, ItemType.Radiology, ItemType.Laboratory));
                    break;
                case ItemType.Medical:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.InnerJoin(med).On(query.ItemID == med.ItemID);
                    query.Where(query.SRItemType == itemType);
                    break;
                case ItemType.NonMedical:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.InnerJoin(nonmed).On(query.ItemID == nonmed.ItemID);
                    query.Where(query.SRItemType == itemType);
                    break;
                case ItemType.Radiology:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.InnerJoin(rad).On(query.ItemID == rad.ItemID);
                    query.Where(query.SRItemType == itemType);
                    break;
                case ItemType.Laboratory:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.InnerJoin(lab).On(query.ItemID == lab.ItemID);
                    query.Where(query.SRItemType == itemType);
                    break;
                case ItemType.Kitchen:
                    query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
                    query.InnerJoin(kitchen).On(query.ItemID == kitchen.ItemID);
                    query.Where(query.SRItemType == itemType);
                    break;
            }

            query.es.Top = 20;
            query.OrderBy(query.ItemName.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }


        public static void ItemItemsRequestedForGroupService(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");
            var stdRef = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(stdRef).On(
                stdRef.StandardReferenceID == "ItemType" &&
                stdRef.IsActive == true && stdRef.ReferenceID == "Service" &&
                stdRef.ItemID == query.SRItemType)
                .Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                )
                .Select(query.ItemID, query.ItemName)
                .OrderBy(query.ItemName.Ascending)
                .es.Top = 20;

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemProductItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(query.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            query.es.Top = 20;
            query.OrderBy(query.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemProductItemsRequested(RadComboBox comboBox, string textSearch, string productAcc)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(query.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            if (!string.IsNullOrEmpty(productAcc))
                query.Where(query.ProductAccountID == productAcc);

            query.es.Top = 20;
            query.OrderBy(query.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemByGroupItemsRequested(RadComboBox comboBox, string textSearch, string groupId)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            if (!string.IsNullOrEmpty(groupId))
                query.Where(query.ItemGroupID == groupId);

            query.es.Top = 20;
            query.OrderBy(query.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion

        #region Paramedic

        public static void ParamedicItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ParamedicQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ParamedicID == textSearch,
                            query.ParamedicName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            //query.es.Top = 20;
            query.Select(query.ParamedicID, query.ParamedicName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ParamedicName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
            }
        }

        public static void ParamedicItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        #endregion

        #region Anesthesia

        public static void AnesthesiaItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ParamedicQuery("a");
            query.Where
                (query.SRParamedicType == AppSession.Parameter.PhysicianTypeAnesthetic,
                    query.Or
                        (
                            query.ParamedicID == textSearch,
                            query.ParamedicName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            //query.es.Top = 20;
            query.Select(query.ParamedicID, query.ParamedicName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ParamedicName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ParamedicID"].ToString();
            }
        }

        public static void AnesthesiaItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        #endregion

        #region PaymentMethod

        public static void PaymentMethodItemRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new PaymentTypeQuery("a");
            var PaymentMethod = new PaymentMethodQuery("b");


            query.LeftJoin(PaymentMethod).On(query.SRPaymentTypeID == PaymentMethod.SRPaymentTypeID);
            query.es.Top = 20;
            query.Select(query.SRPaymentTypeID, PaymentMethod.SRPaymentMethodID, query.PaymentTypeName, PaymentMethod.PaymentMethodName);

            textSearch = "%" + textSearch + "%";
            query.Where
                (
                             //query.Or
                             //    (
                             // query.ParamedicID == textSearch,
                             //  query.ParamedicName.Like(string.Format("%{0}%", textSearch))
                             string.Format("<a.PaymentTypeName +' '+ ISNULL(b.PaymentMethodName,'') LIKE '{0}' >", textSearch),
                    //),
                    query.IsCashierFrontOffice == true
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["PaymentTypeName"].ToString() + ' ' + dtb.Rows[0]["PaymentMethodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SRPaymentTypeID"].ToString() + '*' + dtb.Rows[0]["SRPaymentMethodID"].ToString();
            }
        }

        public static void PaymentMethodItemRequestedByPaymentType(RadComboBox comboBox, string textSearch, string paymentType)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new PaymentTypeQuery("a");
            var paymentMethod = new PaymentMethodQuery("b");


            query.InnerJoin(paymentMethod).On(query.SRPaymentTypeID == paymentMethod.SRPaymentTypeID &&
                                              query.SRPaymentTypeID == paymentType);
            query.es.Top = 20;
            query.Select(query.SRPaymentTypeID, paymentMethod.SRPaymentMethodID, query.PaymentTypeName,
                         paymentMethod.PaymentMethodName);

            textSearch = "%" + textSearch + "%";
            query.Where
                (
                             //query.Or
                             //    (
                             // query.ParamedicID == textSearch,
                             //  query.ParamedicName.Like(string.Format("%{0}%", textSearch))
                             string.Format("<a.PaymentTypeName +' '+ ISNULL(b.PaymentMethodName,'') LIKE '{0}' >", textSearch),
                    //),
                    query.IsCashierFrontOffice == true
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["PaymentTypeName"].ToString() + ' ' + dtb.Rows[0]["PaymentMethodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SRPaymentTypeID"].ToString() + '*' + dtb.Rows[0]["SRPaymentMethodID"].ToString();
            }
        }

        public static void PaymentMethodItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PaymentTypeName"].ToString() + ' ' + ((DataRowView)e.Item.DataItem)["PaymentMethodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRPaymentTypeID"].ToString() + '*' + ((DataRowView)e.Item.DataItem)["SRPaymentMethodID"].ToString();
        }

        #endregion

        #region ItemProductMedic

        public static void ItemProductMedicItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemProductMedicQuery("a");
            var item = new ItemQuery("b");

            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.Where
                (
                    query.Or
                        (
                            query.ItemID == textSearch,
                            item.ItemName.Like(searchTextContain)
                        )
                );

            query.es.Top = 20;
            query.Select(query.ItemID, item.ItemName);
            query.OrderBy(item.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }

        public static void ItemProductMedicItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion

        #region ServiceUnit
        public static void RegTypeServiceUnitItemsRequested(RadComboBox comboBox, string textSearch, string regtype)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");

            query.Where
                (
                query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true, query.SRRegistrationType == regtype
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }
        public static void ServiceUnitItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitItemsRequested(RadComboBox comboBox, string textSearch, string transactionCode)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            var stc = new ServiceUnitTransactionCodeQuery("b");

            query.InnerJoin(stc).On(query.ServiceUnitID == stc.ServiceUnitID)
                .Where
                (
                    query.Or
                    (
                        query.ServiceUnitID == textSearch,
                        query.ServiceUnitName.Like(searchTextContain)
                    ),
                    query.IsActive == true,
                    stc.SRTransactionCode == transactionCode
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitInpatientItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient
                //query.DepartmentID == AppSession.Parameter.InPatientDepartmentID

                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitOutpatientItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRRegistrationType.In(AppConstant.RegistrationType.OutPatient, AppConstant.RegistrationType.EmergencyPatient, AppConstant.RegistrationType.MedicalCheckUp)

                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitJobOrderItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("b");
            query.InnerJoin(tcode).On(query.ServiceUnitID == tcode.ServiceUnitID &&
                                      tcode.SRTransactionCode == TransactionCode.JobOrder);

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.IsUsingJobOrder == true
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitPurchasingRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("b");
            query.InnerJoin(tcode).On(query.ServiceUnitID == tcode.ServiceUnitID &&
                                      tcode.SRTransactionCode == TransactionCode.PurchaseOrder);

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitUserItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            var ausuQ = new AppUserServiceUnitQuery("b");

            query.InnerJoin(ausuQ).On(query.ServiceUnitID == ausuQ.ServiceUnitID);
            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    ausuQ.UserID == AppSession.UserLogin.UserID
                );

            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitWorkOrderItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("b");
            query.InnerJoin(tcode).On(query.ServiceUnitID == tcode.ServiceUnitID &&
                                      tcode.SRTransactionCode == TransactionCode.AssetWorkOrderRealization);

            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == textSearch,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Distinct = true;
            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitByTransactionCodesRequested(RadComboBox comboBox, string textSearch, string transactionCode, bool isFilterByUserId)
        {
            if (textSearch == null)
                textSearch = string.Empty;

            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("b");
            query.InnerJoin(tcode).On(query.ServiceUnitID == tcode.ServiceUnitID &&
                                      tcode.SRTransactionCode == transactionCode);

            if (isFilterByUserId)
            {
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
            }

            string searchTextContain = string.Format("%{0}%", textSearch);
            query.Where
            (
                query.Or
                (
                    query.ServiceUnitID == textSearch,
                    query.ServiceUnitName.Like(searchTextContain)
                ),
                query.IsActive == true
            );

            query.es.Distinct = true;
            query.es.Top = 20;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ServiceUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ServiceUnitID"].ToString();
            }
        }

        public static void ServiceUnitItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        #endregion

        #region Department
        public static void DepartmentItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new DepartmentQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.DepartmentID == textSearch,
                            query.DepartmentName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.DepartmentID, query.DepartmentName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["DepartmentName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["DepartmentID"].ToString();
            }
        }

        public static void DepartmentItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DepartmentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DepartmentID"].ToString();
        }
        #endregion

        #region ItemGroup

        public static void ItemGroupItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemGroupQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID == textSearch,
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.ItemGroupID, query.ItemGroupName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemGroupName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemGroupID"].ToString();
            }
        }

        public static void ItemGroupMedicalItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemGroupQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID == textSearch,
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRItemType == ItemType.Medical
                );

            query.es.Top = 20;
            query.Select(query.ItemGroupID, query.ItemGroupName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemGroupName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemGroupID"].ToString();
            }
        }

        public static void ItemGroupNarcoticPsycotropicItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemGroupQuery("a");
            var itemQ = new ItemQuery("b");
            var itemMedicQ = new ItemProductMedicQuery("c");

            itemMedicQ.InnerJoin(itemQ).On(itemMedicQ.ItemID == itemQ.ItemID);
            itemMedicQ.InnerJoin(query).On(itemQ.ItemGroupID == query.ItemGroupID);

            itemMedicQ.Where
                (
                    itemMedicQ.Or
                        (
                            query.ItemGroupID == textSearch,
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    itemMedicQ.Or
                    (
                        itemMedicQ.IsNarcotic == true,
                        itemMedicQ.IsPsychotropic == true
                    )
                );

            itemMedicQ.Select(query.ItemGroupID, query.ItemGroupName);
            itemMedicQ.es.Distinct = true;

            DataTable dtb = itemMedicQ.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemGroupName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemGroupID"].ToString();
            }
        }

        public static void ItemGroupItemsRequested(RadComboBox comboBox, string textSearch, string itemType)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ItemGroupQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ItemGroupID.Like(searchTextContain),
                            query.ItemGroupName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.SRItemType == itemType
                );

            query.es.Top = 20;
            query.Select(query.ItemGroupID, query.ItemGroupName);
            query.OrderBy(query.ItemGroupID.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ItemGroupName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ItemGroupID"].ToString();
            }
        }

        public static void ItemGroupItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        #endregion

        #region ProductAccount

        public static void ProductAccountItemsRequested(RadComboBox comboBox, string textSearch, string itemType)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ProductAccountQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ProductAccountID == textSearch,
                            query.ProductAccountName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(itemType))
                query.Where(query.SRItemType == itemType);

            query.es.Top = 20;
            query.Select(query.ProductAccountID, query.ProductAccountName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ProductAccountName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ProductAccountID"].ToString();
            }
        }

        public static void ProductAccountItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProductAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProductAccountID"].ToString();
        }

        #endregion

        #region Cashier

        public static void UserCashierItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            var uug = new AppUserUserGroupQuery("uug");

            //var ugp = new AppUserGroupProgramQuery("ugp");
            //var prog = new AppProgramQuery("prog");
            //ugp.InnerJoin(prog).On(ugp.ProgramID == prog.ProgramID);
            //ugp.Select(ugp.UserGroupID).Where(prog.ParentProgramID == "02.04");
            //ugp.es.Distinct = true;

            //uug.Select(uug.UserID).Where(uug.UserGroupID.In(ugp));

            //var py = new TransPaymentQuery();
            //py.Select(py.CreatedBy);
            //py.es.Distinct = true;
            //uug.Select(uug.UserID).Where(uug.UserID.In(py));

            var query = new AppUserQuery("a");
            var py = new TransPaymentQuery("b");
            query.InnerJoin(py).On(query.UserID == py.CreatedBy);

            query.es.Top = 20;
            query.es.Distinct = true;

            query.Select(query.UserID, query.UserName);
            query.OrderBy(query.UserName.Ascending);

            string searchTextContain = string.Format("%{0}%", textSearch);
            query.Where
                (
                    query.Or
                        (
                            query.UserID == textSearch,
                            query.UserName.Like(searchTextContain)
                        )
                );
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        public static void UserCashierItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        #endregion

        #region Accountant

        public static void UserAccountingItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var uug = new AppUserUserGroupQuery("uug");

            var ugp = new AppUserGroupProgramQuery("ugp");
            var prog = new AppProgramQuery("prog");
            ugp.InnerJoin(prog).On(ugp.ProgramID == prog.ProgramID);
            ugp.Select(ugp.UserGroupID).Where(prog.ParentProgramID == "05.01");
            ugp.es.Distinct = true;

            uug.Select(uug.UserID).Where(uug.UserGroupID.In(ugp));

            var query = new AppUserQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                ).Where(query.UserID.In(uug));

            query.OrderBy(query.UserName.Ascending);
            
            query.Where
                (
                    query.Or
                        (
                            query.UserID == textSearch,
                            query.UserName.Like(searchTextContain)
                        )
                );
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        public static void UserAccountingItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        #endregion

        #region User All

        public static void UserItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new AppUserQuery("a");
            query.es.Top = 20;
            query.Select
                (
                    query.UserID,
                    query.UserName
                );

            query.OrderBy(query.UserName.Ascending);
            query.Where
                (
                    query.Or
                        (
                            query.UserID == textSearch,
                            query.UserName.Like(searchTextContain)
                        )
                );
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        public static void UserByUnitItemRequested(RadComboBox comboBox, string serviceUnitId, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new AppUserQuery("a");
            var usrUnit = new AppUserServiceUnitQuery("b");
            query.InnerJoin(usrUnit).On(query.UserID == usrUnit.UserID);
            query.es.Top = 20;
            query.Where
            (query.Or(query.UserName.Like(searchTextContain),
                    query.UserID.Like(searchTextContain)),
                query.ExpireDate >= DateTime.Now.Date,
                usrUnit.ServiceUnitID == serviceUnitId);
            query.OrderBy(query.UserName.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();

            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["UserName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["UserID"].ToString();
            }
        }
        public static void UserItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["UserName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["UserID"].ToString();
        }

        #endregion

        #region StandardReference

        public static void StandardReferenceItemsRequested(RadComboBox comboBox, string standardReferenceID,
            string textSearch)
        {
            StandardReferenceItemsRequested(comboBox, standardReferenceID, textSearch, false);
        }

        public static void StandardReferenceItemsRequested(RadComboBox comboBox, string standardReferenceID,
            string textSearch, bool WithEmpty)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 50;
            query.Select(query.ItemID, query.ItemName);
            query.Where
            (
                query.Or(
                    query.ItemName.Like(searchTextContain),
                    query.ItemID.Like(searchTextContain)),
                query.StandardReferenceID == standardReferenceID,
                query.IsActive == true
            );
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();

            if (WithEmpty)
            {
                var r = dtb.NewRow();
                r["ItemName"] = string.Empty; r["ItemID"] = string.Empty;
                dtb.Rows.InsertAt(r, 0);
                dtb.AcceptChanges();
            }

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemName].ToString();
                comboBox.SelectedValue = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemID].ToString();
            }
        }

        public static void StandardReferenceItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text =
                ((DataRowView)e.Item.DataItem)[AppStandardReferenceItemMetadata.ColumnNames.ItemName].ToString();
            e.Item.Value =
                ((DataRowView)e.Item.DataItem)[AppStandardReferenceItemMetadata.ColumnNames.ItemID].ToString();
        }

        public static void StandartReferenceItemSelectOne(RadComboBox comboBox, string standardReferenceID,
                                                           string ItemID)
        {
            if (ItemID == null) return;

            var query = new AppStandardReferenceItemQuery();
            //query.es.Top = 50;
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (
                    query.StandardReferenceID == standardReferenceID,
                    query.ItemID == ItemID
                );
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemName].ToString();
                comboBox.SelectedValue = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemID].ToString();
            }
        }

        public static void StandardReferenceItem(RadComboBox comboBox, string standardReferenceID)
        {
            StandardReferenceItem(comboBox, standardReferenceID, true);
        }

        private static DataTable GetStandardReference(string standardReferenceID, string ReferenceID)
        {
            var query = new AppStandardReferenceItemQuery();
            //query.es.Top = 50;
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (
                    query.StandardReferenceID == standardReferenceID
                );
            if (!string.IsNullOrEmpty(ReferenceID))
            {
                query.Where(query.ReferenceID == ReferenceID);
            }
            //query.OrderBy(query.ItemName.Ascending);
            query.OrderBy(query.LineNumber.Ascending, query.ItemName.Ascending);
            var dtb = query.LoadDataTable();
            return dtb;
        }
        public static void StandardReferenceItem(RadComboBox comboBox, string standardReferenceID, bool isWithBlankOption, string ReferenceID)
        {
            var dtb = GetStandardReference(standardReferenceID, ReferenceID);
            if (isWithBlankOption)
                comboBox.Items.Add(new RadComboBoxItem(String.Empty, String.Empty));

            foreach (System.Data.DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }
        public static void StandardReferenceItem(RadComboBox comboBox, string standardReferenceID, bool isWithBlankOption)
        {
            StandardReferenceItem(comboBox, standardReferenceID, isWithBlankOption, "");
        }
        public static void StandardReferenceItem(RadDropDownList comboBox, string standardReferenceID)
        {
            var dtb = GetStandardReference(standardReferenceID, string.Empty);
            comboBox.Items.Clear();
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                comboBox.Items.Add(new DropDownListItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }
        #endregion

        #region StandardReferenceWithReferenceID
        public static void StandardReferenceWithReferenceIDItemsRequested(RadComboBox comboBox, string standardReferenceID, string refID,
                                                           string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new AppStandardReferenceItemQuery();
            query.es.Top = 50;
            query.Select(query.ItemID, query.ItemName);
            query.Where
                (
                    query.ItemName.Like(searchTextContain),
                    query.StandardReferenceID == standardReferenceID,
                    query.ReferenceID == refID,
                    query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemName].ToString();
                comboBox.SelectedValue = dtb.Rows[0][AppStandardReferenceItemMetadata.ColumnNames.ItemID].ToString();
            }
        }

        public static void StandardReferenceWithReferenceIDItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text =
                ((DataRowView)e.Item.DataItem)[AppStandardReferenceItemMetadata.ColumnNames.ItemName].ToString();
            e.Item.Value =
                ((DataRowView)e.Item.DataItem)[AppStandardReferenceItemMetadata.ColumnNames.ItemID].ToString();
        }

        #endregion

        #region Diagnose

        public static void DiagnoseItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new DiagnoseQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.DiagnoseID == textSearch,
                            query.DiagnoseName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.DiagnoseID, query.DiagnoseName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["DiagnoseName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["DiagnoseID"].ToString();
            }
        }

        public static void DiagnoseItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[DiagnoseMetadata.ColumnNames.DiagnoseName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[DiagnoseMetadata.ColumnNames.DiagnoseID].ToString();
        }

        #endregion

        #region Procedure

        public static void ProcedureItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ProcedureQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.ProcedureID == textSearch,
                            query.ProcedureName.Like(searchTextContain)
                        )
                        );

            query.es.Top = 20;
            query.Select(query.ProcedureID, query.ProcedureName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ProcedureName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ProcedureID"].ToString();
            }
        }

        public static void ProcedureItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ProcedureMetadata.ColumnNames.ProcedureName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ProcedureMetadata.ColumnNames.ProcedureID].ToString();
        }

        #endregion

        #region Location

        public static void LocationItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new LocationQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.LocationID == textSearch,
                            query.LocationName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.LocationID, query.LocationName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["LocationName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["LocationID"].ToString();
            }
        }

        public static void LocationConsignmentItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new LocationQuery("a");

            query.Where
            (
                query.Or
                (
                    query.LocationID == textSearch,
                    query.LocationName.Like(searchTextContain)
                ),
                query.IsActive == true, query.IsConsignment == true
            );

            query.es.Top = 20;
            query.Select(query.LocationID, query.LocationName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["LocationName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["LocationID"].ToString();
            }
        }

        public static void LocationItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[LocationMetadata.ColumnNames.LocationName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[LocationMetadata.ColumnNames.LocationID].ToString();
        }

        #endregion

        #region TestResultTemplate

        public static void TestResultTemplateItemsRequested(RadComboBox comboBox, string paramedicID, string itemID, string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new TestResultTemplateQuery("a");

            query.Where(
                query.Or(
                    query.ParamedicID == paramedicID,
                    query.ParamedicID.IsNull(),
                    query.ParamedicID == string.Empty
                ),
                query.ItemID == itemID,
                query.TestResultTemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;
            query.Select(query.TestResultTemplateID, query.TestResultTemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["TestResultTemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["TestResultTemplateID"].ToString();
            }
        }

        public static void TestResultTemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TestResultTemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TestResultTemplateID"].ToString();
        }

        #endregion

        #region PATemplate

        public static void PATemplateItemsRequested(RadComboBox comboBox, string transactionNo, string reType, string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;
            var items = new TransChargesItemQuery();
            items.Where(items.TransactionNo == transactionNo);
            items.Select(items.ItemID);

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new PATemplateQuery("a");            
          
            query.Where(
                //query.Or(
                //    query.ParamedicID == paramedicID,
                //    query.ParamedicID.IsNull(),
                //    query.ParamedicID == string.Empty
                //),               
                query.Or(
                    query.ItemID == string.Empty,
                    query.ItemID.In(items)
                    ),
                //reg.ToServiceUnitID == AppSession.Parameter.ServiceUnitPathologyAnatomyID,                
                query.ResultType == reType,
                query.TemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;           
            query.Select(query.TemplateID, query.TemplateName);
            query.GroupBy(query.TemplateID,query.TemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["TemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["TemplateID"].ToString();
            }
        }

        public static void PATemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString();
        }

        #endregion        

        #region PA2Template

        public static void PA2TemplateItemsRequested(RadComboBox comboBox, string transactionNo, string reType, string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;

            var items = new TransChargesItemQuery();
            items.Where(items.TransactionNo == transactionNo);
            items.Select(items.ItemID);

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new PA2TemplateQuery("a");

            query.Where(
                //query.Or(
                //    query.ParamedicID == paramedicID,
                //    query.ParamedicID.IsNull(),
                //    query.ParamedicID == string.Empty
                //),
                query.Or(
                    query.ItemID == string.Empty,
                    query.ItemID.In(items)
                    ),                
                query.ResultType == reType,
                query.TemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;            
            query.Select(query.TemplateID, query.TemplateName);
            query.GroupBy(query.TemplateID, query.TemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["TemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["TemplateID"].ToString();
            }
        }

        public static void PA2TemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString();
        }

        #endregion        

        #region SanitationActivityResultTemplate

        public static void SanitationActivityResultTemplateItemsRequested(RadComboBox comboBox, string srWorkTradeItem, string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new SanitationActivityResultTemplateQuery("a");

            query.Where(
                query.SRWorkTradeItem == srWorkTradeItem,
                query.ResultTemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;
            query.Select(query.SanitationActivityResultID, query.ResultTemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ResultTemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SanitationActivityResultID"].ToString();
            }
        }

        public static void SanitationActivityResultTemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ResultTemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SanitationActivityResultID"].ToString();
        }

        #endregion

        #region K3rsFormTemplate
        public static void K3rsFormTemplateItemsRequested(RadComboBox comboBox, string textSearch)
        {
            //if (textSearch == null)
            //    textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new K3rsFormTemplateQuery("a");

            query.Where(
                query.TemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;
            query.Select(query.TemplateID, query.TemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["TemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["TemplateID"].ToString();
            }
        }

        public static void K3rsFormTemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString();
        }

        #endregion

        #region EmployeeFormTemplate
        public static void EmployeeFormTemplateItemsRequested(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new EmployeeFormTemplateQuery("a");

            query.Where(
                query.TemplateName.Like(searchTextContain)
            );

            query.es.Top = 20;
            query.Select(query.TemplateID, query.TemplateName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["TemplateName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["TemplateID"].ToString();
            }
        }

        public static void EmployeeFormTemplateItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TemplateName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TemplateID"].ToString();
        }

        #endregion

        #region Payroll
        public static void PayrollPeriodItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            
            var query = new PayrollPeriodQuery("a");
            query.Select(query.PayrollPeriodID, query.PayrollPeriodCode, query.PayrollPeriodName);

            if (string.IsNullOrEmpty(textSearch))
            {
                query.Where
               (
                   //query.Or
                   //    (
                   //        query.PayrollPeriodID.Like(string.Format("%{0}%", string.Empty)),
                   //        query.PayrollPeriodCode.Like(string.Format("%{0}%", string.Empty)),
                   //        query.PayrollPeriodName.Like(string.Format("%{0}%", string.Empty))
                   //    ),
                   query.SPTYear == DateTime.Now.Year
               );
            }
            else
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodID.Like(searchTextContain),
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            }

            query.es.Top = 12;
            query.Select(Convert.ToString(query.PayrollPeriodID), query.PayrollPeriodName);
            query.OrderBy(query.PayrollPeriodCode.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["PayrollPeriodID"].ToString();
            }
        }

        public static void PayrollPeriodItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
        }

        public static void SalaryComponentItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new SalaryComponentQuery("a");

            query.Where
                (
                    query.Or
                        (
                            Convert.ToString(query.SalaryComponentID) == textSearch,
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );

            query.es.Top = 20;
            query.Select(Convert.ToString(query.SalaryComponentID), query.SalaryComponentName);
            query.OrderBy(query.SalaryComponentCode.Ascending);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["SalaryComponentName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SalaryComponentID"].ToString();
            }
        }

        public static void SalaryComponentItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        public static void EmployeeNameItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        ),
                    query.SREmployeeStatus == "1"

                );

            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["EmployeeName"].ToString() + " (" + dtb.Rows[0]["EmployeeNumber"].ToString() + ")";
                comboBox.SelectedValue = dtb.Rows[0]["PersonID"].ToString();
            }
        }

        public static void EmployeeNameItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString() + " (" + ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + ")";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        #endregion

        #region Nutrient
        public static void FoodsRequested(RadComboBox comboBox, string textSearch, bool isSalesAvailable, bool isNew)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new FoodQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.FoodID.Like(searchTextContain),
                            query.FoodName.Like(searchTextContain)
                        ),
                    query.IsSalesAvailable == isSalesAvailable
                );
            if (isNew)
                query.Where(query.IsActive == true);

            query.es.Top = 20;
            query.OrderBy(query.FoodID.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["FoodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["FoodID"].ToString();
            }
        }

        public static void LiquidFoodsRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new FoodQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.FoodID.Like(searchTextContain),
                            query.FoodName.Like(searchTextContain)
                        ),
                    query.IsActive == true, query.SRFoodGroup1 == "VIII"
                );

            query.es.Top = 20;
            query.OrderBy(query.FoodID.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["FoodName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["FoodID"].ToString();
            }
        }

        public static void DietsRequested(RadComboBox comboBox, string textSearch, string formOfFood, bool isNew)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new DietQuery("a");
            query.Select(query.DietID, query.DietName);
            query.Where
            (
                query.Or
                (
                    query.DietID.Like(searchTextContain),
                    query.DietName.Like(searchTextContain)
                ),
                query.IsActive == true
            );
            if (!string.IsNullOrEmpty(formOfFood) || isNew)
            {
                var mq = new DietMenuQuery("b");
                query.InnerJoin(mq).On(query.DietID == mq.DietID);
                query.Where(mq.FormOfFood == formOfFood);
            }

            query.es.Top = 20;
            query.OrderBy(query.DietID.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["DietName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["DietID"].ToString();
            }
        }

        public static void DietItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DietName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DietID"].ToString();
        }

        public static void DietComplicationsRequested(RadComboBox comboBox, string textSearch, string dietId)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new DietComplicationQuery("a");
            var dietQ = new DietQuery("b");
            query.Select(query.DietComplicationID.As("DietID"), dietQ.DietName);
            query.InnerJoin(dietQ).On(query.DietComplicationID == dietQ.DietID);
            query.Where
                (
                    query.Or
                        (
                            query.DietComplicationID.Like(searchTextContain),
                            dietQ.DietName.Like(searchTextContain)
                        ),
                    query.IsActive == true,
                    query.DietID == dietId
                );

            query.es.Top = 20;
            query.OrderBy(query.DietComplicationID.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["DietName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["DietID"].ToString();
            }
        }

        public static void PopulateFormOfFood(RadComboBox cbo, string dietId)
        {
            if (string.IsNullOrEmpty(dietId))
                return;

            cbo.Items.Clear();

            var coll = new AppStandardReferenceItemCollection();
            var query = new AppStandardReferenceItemQuery("a");
            var dietMenuQ = new DietMenuQuery("b");
            query.InnerJoin(dietMenuQ).On(query.StandardReferenceID == AppEnum.StandardReference.FormOfFood.ToString() &
                                          query.ItemID == dietMenuQ.FormOfFood);
            query.Where(dietMenuQ.DietID == dietId);

            coll.Load(query);

            cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            }
        }

        public static void SnacksRequested(RadComboBox comboBox, string textSearch)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new SnackQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.SnackID.Like(searchTextContain),
                            query.SnackName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.OrderBy(query.SnackID.Ascending);

            var dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["SnackName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["SnackID"].ToString();
            }
        }

        public static void SnackItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SnackName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SnackID"].ToString();
        }
        #endregion

        #region OrganizationUnit

        public static void OrganizationUnitItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new OrganizationUnitQuery("a");

            query.Where
                (
                    query.OrganizationUnitName.Like(searchTextContain),
                    query.IsActive == true
                );

            //query.es.Top = 20;
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["OrganizationUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["OrganizationUnitID"].ToString();
            }
        }

        public static void OrganizationUnitSectionItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new OrganizationUnitQuery("a");

            query.Where
                (
                    query.OrganizationUnitName.Like(searchTextContain),
                    query.SROrganizationLevel == "0",
                    query.IsActive == true
                );

            //query.es.Top = 20;
            query.Select(query.OrganizationUnitID, query.OrganizationUnitName);
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["OrganizationUnitName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["OrganizationUnitID"].ToString();
            }
        }

        public static void OrganizationUnitItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        #endregion

        #region Tariff
        public static void PopulateWithTariffComponent(RadComboBox cbo, bool isParamedicOnly, bool WithSpace)
        {
            var coll = new TariffComponentCollection();
            var query = new TariffComponentQuery("a");

            query.OrderBy(query.IsTariffParamedic.Ascending);

            if (isParamedicOnly)
            {
                query.Where
                (
                    query.IsTariffParamedic == true
                );
            }

            coll.Load(query);

            cbo.Items.Clear();
            if (WithSpace)
            {
                cbo.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            }
            foreach (TariffComponent item in coll)
            {
                cbo.Items.Add(new RadComboBoxItem(item.TariffComponentName, item.TariffComponentID));
            }
        }
        #endregion

        #region Transaction Code
        public static void PopulateWithTransactionCodeWithApprovalLevel(RadComboBox cboTransactionCode)
        {
            cboTransactionCode.Items.Clear();

            var q = new AppStandardReferenceItemQuery("a");

            q.Select
            (
                q.ItemID,
                q.ItemName
            );
            q.Where(q.StandardReferenceID == AppEnum.StandardReference.TransactionCode,
                q.CustomField == "IsUseApprovalLevel=Yes", q.IsActive == true);
            q.OrderBy(q.ItemID.Ascending);

            DataTable dtb = q.LoadDataTable();

            cboTransactionCode.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboTransactionCode.Items.Add(new RadComboBoxItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }
        }
        #endregion

        #region Referral
        public static void ReferralItemsRequested(RadComboBox comboBox, string textSearch, string referralGroup)
        {
            comboBox.Items.Clear();
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new ReferralQuery("a");
            query.Where(
                query.Or(query.ReferralID == textSearch, query.ReferralName.Like(searchTextContain)),
                query.IsActive == true
                );
            if (!string.IsNullOrEmpty(referralGroup))
                query.Where(query.SRReferralGroup == referralGroup);

            query.Select(query.ReferralID, query.ReferralName);
            query.OrderBy(query.ReferralName.Ascending);
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["ReferralName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["ReferralID"].ToString();
            }
        }

        public static void ReferralItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[ReferralMetadata.ColumnNames.ReferralName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[ReferralMetadata.ColumnNames.ReferralID].ToString();
        }
        #endregion

        public static void NursingDiagnoseItemsRequested(RadComboBox comboBox, string textSearch)
        {
            if (textSearch == null)
                textSearch = string.Empty;
            string searchTextContain = string.Format("%{0}%", textSearch);

            var query = new NursingDiagnosaQuery("a");

            query.Where
                (
                    query.Or
                        (
                            query.NursingDiagnosaID == textSearch,
                            query.NursingDiagnosaName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.NursingDiagnosaID, query.NursingDiagnosaName);
            query.Where(query.SRNursingDiagnosaLevel == "10");
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Text = dtb.Rows[0]["NursingDiagnosaName"].ToString();
                comboBox.SelectedValue = dtb.Rows[0]["NursingDiagnosaID"].ToString();
            }
        }

        public static void NursingDiagnoseItemDataBound(RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)[NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaName].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)[NursingDiagnosaMetadata.ColumnNames.NursingDiagnosaID].ToString();
        }


        #endregion
    }
}