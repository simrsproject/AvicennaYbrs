/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/7/2025 10:03:35 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    [Serializable]
    abstract public class esNccIDRGCollection : esEntityCollectionWAuditLog
    {
        public esNccIDRGCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NccIDRGCollection";
        }

        #region Query Logic
        protected void InitQuery(esNccIDRGQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntityCollection)this).Connection;
        }

        protected bool OnQueryLoaded(DataTable table)
        {
            this.PopulateCollection(table);
            return (this.RowCount > 0) ? true : false;
        }

        protected override void HookupQuery(esDynamicQuery query)
        {
            this.InitQuery(query as esNccIDRGQuery);
        }
        #endregion

        virtual public NccIDRG DetachEntity(NccIDRG entity)
        {
            return base.DetachEntity(entity) as NccIDRG;
        }

        virtual public NccIDRG AttachEntity(NccIDRG entity)
        {
            return base.AttachEntity(entity) as NccIDRG;
        }

        virtual public void Combine(NccIDRGCollection collection)
        {
            base.Combine(collection);
        }

        new public NccIDRG this[int index]
        {
            get
            {
                return base[index] as NccIDRG;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NccIDRG);
        }
    }

    [Serializable]
    abstract public class esNccIDRG : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNccIDRGQuery GetDynamicQuery()
        {
            return null;
        }

        public esNccIDRG()
        {
        }

        public esNccIDRG(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        /// <summary>
        /// Loads an entity by primary key
        /// </summary>
        /// <remarks>
        /// Requires primary keys be defined on all tables.
        /// If a table does not have a primary key set,
        /// this method will not compile.
        /// </remarks>
        /// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String registrationNo)
        {
            esNccIDRGQuery query = this.GetDynamicQuery();
            query.Where(query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("RegistrationNo", registrationNo);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
        #endregion

        #region Properties

        public override void SetProperties(IDictionary values)
        {
            foreach (string propertyName in values.Keys)
            {
                this.SetProperty(propertyName, values[propertyName]);
            }
        }

        public override void SetProperty(string name, object value)
        {
            if (this.Row == null) this.AddNew();

            esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
            if (col != null)
            {
                if (value == null || value is System.String)
                {
                    // Use the strongly typed property
                    switch (name)
                    {
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "MedicalNo": this.str.MedicalNo = (string)value; break;
                        case "SEP": this.str.SEP = (string)value; break;
                        case "ClaimDataRequest": this.str.ClaimDataRequest = (string)value; break;
                        case "ClaimDataResponse": this.str.ClaimDataResponse = (string)value; break;
                        case "IdrgDiagnosaSetReq": this.str.IdrgDiagnosaSetReq = (string)value; break;
                        case "IdrgDiagnosaSetRes": this.str.IdrgDiagnosaSetRes = (string)value; break;
                        case "IdrgDiagnosaGetReq": this.str.IdrgDiagnosaGetReq = (string)value; break;
                        case "IdrgDiagnosaGetRes": this.str.IdrgDiagnosaGetRes = (string)value; break;
                        case "IdrgProcedureSetReq": this.str.IdrgProcedureSetReq = (string)value; break;
                        case "IdrgProcedureSetRes": this.str.IdrgProcedureSetRes = (string)value; break;
                        case "IdrgProcedureGetReq": this.str.IdrgProcedureGetReq = (string)value; break;
                        case "IdrgProcedureGetRes": this.str.IdrgProcedureGetRes = (string)value; break;
                        case "GroupingIdrgReq": this.str.GroupingIdrgReq = (string)value; break;
                        case "GroupingIdrgRes": this.str.GroupingIdrgRes = (string)value; break;
                        case "FinalIdrgReq": this.str.FinalIdrgReq = (string)value; break;
                        case "FinalIdrgRes": this.str.FinalIdrgRes = (string)value; break;
                        case "ReEditIdrgReq": this.str.ReEditIdrgReq = (string)value; break;
                        case "ReEditIdrgRes": this.str.ReEditIdrgRes = (string)value; break;
                        case "IdrgToInacbgImportReq": this.str.IdrgToInacbgImportReq = (string)value; break;
                        case "IdrgToInacbgImportRes": this.str.IdrgToInacbgImportRes = (string)value; break;
                        case "InacbgDiagnosaGetReq": this.str.InacbgDiagnosaGetReq = (string)value; break;
                        case "InacbgDiagnosaGetRes": this.str.InacbgDiagnosaGetRes = (string)value; break;
                        case "InacbgDiagnosaSetReq": this.str.InacbgDiagnosaSetReq = (string)value; break;
                        case "InacbgDiagnosaSetRes": this.str.InacbgDiagnosaSetRes = (string)value; break;
                        case "InacbgProcedureSetReq": this.str.InacbgProcedureSetReq = (string)value; break;
                        case "InacbgProcedureSetRes": this.str.InacbgProcedureSetRes = (string)value; break;
                        case "InacbgProcedureGetReq": this.str.InacbgProcedureGetReq = (string)value; break;
                        case "InacbgProcedureGetRes": this.str.InacbgProcedureGetRes = (string)value; break;
                        case "GroupingInacbgStage1Req": this.str.GroupingInacbgStage1Req = (string)value; break;
                        case "GroupingInacbgStage1Res": this.str.GroupingInacbgStage1Res = (string)value; break;
                        case "GroupingInacbgStage2Req": this.str.GroupingInacbgStage2Req = (string)value; break;
                        case "GroupingInacbgStage2Res": this.str.GroupingInacbgStage2Res = (string)value; break;
                        case "FinalInacbgReq": this.str.FinalInacbgReq = (string)value; break;
                        case "FinalInacbgRes": this.str.FinalInacbgRes = (string)value; break;
                        case "ReEditInacbgReq": this.str.ReEditInacbgReq = (string)value; break;
                        case "ReEditInacbgRes": this.str.ReEditInacbgRes = (string)value; break;
                        case "ClaimFinalReq": this.str.ClaimFinalReq = (string)value; break;
                        case "ClaimFinalRes": this.str.ClaimFinalRes = (string)value; break;
                        case "ClaimReEditReq": this.str.ClaimReEditReq = (string)value; break;
                        case "ClaimReEditRes": this.str.ClaimReEditRes = (string)value; break;
                        case "ClaimSendReq": this.str.ClaimSendReq = (string)value; break;
                        case "ClaimSendRes": this.str.ClaimSendRes = (string)value; break;
                        case "GetClaimDataReq": this.str.GetClaimDataReq = (string)value; break;
                        case "GetClaimDataRes": this.str.GetClaimDataRes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "LastUpdateDateTime":

                            if (value == null || value is System.DateTime)
                                this.LastUpdateDateTime = (System.DateTime?)value;
                            break;

                        default:
                            break;
                    }
                }
            }
            else if (this.Row.Table.Columns.Contains(name))
            {
                this.Row[name] = value;
            }
            else
            {
                throw new Exception("SetProperty Error: '" + name + "' not found");
            }
        }

        /// <summary>
        /// Maps to NccIDRG.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.MedicalNo
        /// </summary>
        virtual public System.String MedicalNo
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.MedicalNo);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.MedicalNo, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.SEP
        /// </summary>
        virtual public System.String SEP
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.SEP);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.SEP, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimDataRequest
        /// </summary>
        virtual public System.String ClaimDataRequest
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimDataRequest);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimDataRequest, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimDataResponse
        /// </summary>
        virtual public System.String ClaimDataResponse
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimDataResponse);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimDataResponse, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgDiagnosaSetReq
        /// </summary>
        virtual public System.String IdrgDiagnosaSetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgDiagnosaSetRes
        /// </summary>
        virtual public System.String IdrgDiagnosaSetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgDiagnosaGetReq
        /// </summary>
        virtual public System.String IdrgDiagnosaGetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgDiagnosaGetRes
        /// </summary>
        virtual public System.String IdrgDiagnosaGetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgProcedureSetReq
        /// </summary>
        virtual public System.String IdrgProcedureSetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureSetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureSetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgProcedureSetRes
        /// </summary>
        virtual public System.String IdrgProcedureSetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureSetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureSetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgProcedureGetReq
        /// </summary>
        virtual public System.String IdrgProcedureGetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureGetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureGetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgProcedureGetRes
        /// </summary>
        virtual public System.String IdrgProcedureGetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureGetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgProcedureGetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingIdrgReq
        /// </summary>
        virtual public System.String GroupingIdrgReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingIdrgReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingIdrgReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingIdrgRes
        /// </summary>
        virtual public System.String GroupingIdrgRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingIdrgRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingIdrgRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.FinalIdrgReq
        /// </summary>
        virtual public System.String FinalIdrgReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.FinalIdrgReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.FinalIdrgReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.FinalIdrgRes
        /// </summary>
        virtual public System.String FinalIdrgRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.FinalIdrgRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.FinalIdrgRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ReEditIdrgReq
        /// </summary>
        virtual public System.String ReEditIdrgReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ReEditIdrgReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ReEditIdrgReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ReEditIdrgRes
        /// </summary>
        virtual public System.String ReEditIdrgRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ReEditIdrgRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ReEditIdrgRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgToInacbgImportReq
        /// </summary>
        virtual public System.String IdrgToInacbgImportReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.IdrgToInacbgImportRes
        /// </summary>
        virtual public System.String IdrgToInacbgImportRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgDiagnosaGetReq
        /// </summary>
        virtual public System.String InacbgDiagnosaGetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgDiagnosaGetRes
        /// </summary>
        virtual public System.String InacbgDiagnosaGetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgDiagnosaSetReq
        /// </summary>
        virtual public System.String InacbgDiagnosaSetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgDiagnosaSetRes
        /// </summary>
        virtual public System.String InacbgDiagnosaSetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgProcedureSetReq
        /// </summary>
        virtual public System.String InacbgProcedureSetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureSetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureSetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgProcedureSetRes
        /// </summary>
        virtual public System.String InacbgProcedureSetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureSetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureSetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgProcedureGetReq
        /// </summary>
        virtual public System.String InacbgProcedureGetReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureGetReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureGetReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.InacbgProcedureGetRes
        /// </summary>
        virtual public System.String InacbgProcedureGetRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureGetRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.InacbgProcedureGetRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingInacbgStage1Req
        /// </summary>
        virtual public System.String GroupingInacbgStage1Req
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Req);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Req, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingInacbgStage1Res
        /// </summary>
        virtual public System.String GroupingInacbgStage1Res
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Res);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Res, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingInacbgStage2Req
        /// </summary>
        virtual public System.String GroupingInacbgStage2Req
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Req);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Req, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GroupingInacbgStage2Res
        /// </summary>
        virtual public System.String GroupingInacbgStage2Res
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Res);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Res, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.FinalInacbgReq
        /// </summary>
        virtual public System.String FinalInacbgReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.FinalInacbgReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.FinalInacbgReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.FinalInacbgRes
        /// </summary>
        virtual public System.String FinalInacbgRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.FinalInacbgRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.FinalInacbgRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ReEditInacbgReq
        /// </summary>
        virtual public System.String ReEditInacbgReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ReEditInacbgReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ReEditInacbgReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ReEditInacbgRes
        /// </summary>
        virtual public System.String ReEditInacbgRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ReEditInacbgRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ReEditInacbgRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimFinalReq
        /// </summary>
        virtual public System.String ClaimFinalReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimFinalReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimFinalReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimFinalRes
        /// </summary>
        virtual public System.String ClaimFinalRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimFinalRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimFinalRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimReEditReq
        /// </summary>
        virtual public System.String ClaimReEditReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimReEditReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimReEditReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimReEditRes
        /// </summary>
        virtual public System.String ClaimReEditRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimReEditRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimReEditRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimSendReq
        /// </summary>
        virtual public System.String ClaimSendReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimSendReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimSendReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.ClaimSendRes
        /// </summary>
        virtual public System.String ClaimSendRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.ClaimSendRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.ClaimSendRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GetClaimDataReq
        /// </summary>
        virtual public System.String GetClaimDataReq
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GetClaimDataReq);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GetClaimDataReq, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.GetClaimDataRes
        /// </summary>
        virtual public System.String GetClaimDataRes
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.GetClaimDataRes);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.GetClaimDataRes, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NccIDRGMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NccIDRGMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NccIDRG.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NccIDRGMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NccIDRGMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }

        #endregion

        #region String Properties

        /// <summary>
        /// Converts an entity's properties to
        /// and from strings.
        /// </summary>
        /// <remarks>
        /// The str properties Get and Set provide easy conversion
        /// between a string and a property's data type. Not all
        /// data types will get a str property.
        /// </remarks>
        /// <example>
        /// Set a datetime from a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// entity.str.HireDate = "2007-01-01 00:00:00";
        /// entity.Save();
        /// </code>
        /// Get a datetime as a string.
        /// <code>
        /// Employees entity = new Employees();
        /// entity.LoadByPrimaryKey(10);
        /// string theDate = entity.str.HireDate;
        /// </code>
        /// </example>
        [BrowsableAttribute(false)]
        public esStrings str
        {
            get
            {
                if (esstrings == null)
                {
                    esstrings = new esStrings(this);
                }
                return esstrings;
            }
        }

        [Serializable]
        sealed public class esStrings
        {
            public esStrings(esNccIDRG entity)
            {
                this.entity = entity;
            }
            public System.String RegistrationNo
            {
                get
                {
                    System.String data = entity.RegistrationNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.RegistrationNo = null;
                    else entity.RegistrationNo = Convert.ToString(value);
                }
            }
            public System.String MedicalNo
            {
                get
                {
                    System.String data = entity.MedicalNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.MedicalNo = null;
                    else entity.MedicalNo = Convert.ToString(value);
                }
            }
            public System.String SEP
            {
                get
                {
                    System.String data = entity.SEP;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SEP = null;
                    else entity.SEP = Convert.ToString(value);
                }
            }
            public System.String ClaimDataRequest
            {
                get
                {
                    System.String data = entity.ClaimDataRequest;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimDataRequest = null;
                    else entity.ClaimDataRequest = Convert.ToString(value);
                }
            }
            public System.String ClaimDataResponse
            {
                get
                {
                    System.String data = entity.ClaimDataResponse;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimDataResponse = null;
                    else entity.ClaimDataResponse = Convert.ToString(value);
                }
            }
            public System.String IdrgDiagnosaSetReq
            {
                get
                {
                    System.String data = entity.IdrgDiagnosaSetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgDiagnosaSetReq = null;
                    else entity.IdrgDiagnosaSetReq = Convert.ToString(value);
                }
            }
            public System.String IdrgDiagnosaSetRes
            {
                get
                {
                    System.String data = entity.IdrgDiagnosaSetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgDiagnosaSetRes = null;
                    else entity.IdrgDiagnosaSetRes = Convert.ToString(value);
                }
            }
            public System.String IdrgDiagnosaGetReq
            {
                get
                {
                    System.String data = entity.IdrgDiagnosaGetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgDiagnosaGetReq = null;
                    else entity.IdrgDiagnosaGetReq = Convert.ToString(value);
                }
            }
            public System.String IdrgDiagnosaGetRes
            {
                get
                {
                    System.String data = entity.IdrgDiagnosaGetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgDiagnosaGetRes = null;
                    else entity.IdrgDiagnosaGetRes = Convert.ToString(value);
                }
            }
            public System.String IdrgProcedureSetReq
            {
                get
                {
                    System.String data = entity.IdrgProcedureSetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgProcedureSetReq = null;
                    else entity.IdrgProcedureSetReq = Convert.ToString(value);
                }
            }
            public System.String IdrgProcedureSetRes
            {
                get
                {
                    System.String data = entity.IdrgProcedureSetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgProcedureSetRes = null;
                    else entity.IdrgProcedureSetRes = Convert.ToString(value);
                }
            }
            public System.String IdrgProcedureGetReq
            {
                get
                {
                    System.String data = entity.IdrgProcedureGetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgProcedureGetReq = null;
                    else entity.IdrgProcedureGetReq = Convert.ToString(value);
                }
            }
            public System.String IdrgProcedureGetRes
            {
                get
                {
                    System.String data = entity.IdrgProcedureGetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgProcedureGetRes = null;
                    else entity.IdrgProcedureGetRes = Convert.ToString(value);
                }
            }
            public System.String GroupingIdrgReq
            {
                get
                {
                    System.String data = entity.GroupingIdrgReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingIdrgReq = null;
                    else entity.GroupingIdrgReq = Convert.ToString(value);
                }
            }
            public System.String GroupingIdrgRes
            {
                get
                {
                    System.String data = entity.GroupingIdrgRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingIdrgRes = null;
                    else entity.GroupingIdrgRes = Convert.ToString(value);
                }
            }
            public System.String FinalIdrgReq
            {
                get
                {
                    System.String data = entity.FinalIdrgReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalIdrgReq = null;
                    else entity.FinalIdrgReq = Convert.ToString(value);
                }
            }
            public System.String FinalIdrgRes
            {
                get
                {
                    System.String data = entity.FinalIdrgRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalIdrgRes = null;
                    else entity.FinalIdrgRes = Convert.ToString(value);
                }
            }
            public System.String ReEditIdrgReq
            {
                get
                {
                    System.String data = entity.ReEditIdrgReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReEditIdrgReq = null;
                    else entity.ReEditIdrgReq = Convert.ToString(value);
                }
            }
            public System.String ReEditIdrgRes
            {
                get
                {
                    System.String data = entity.ReEditIdrgRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReEditIdrgRes = null;
                    else entity.ReEditIdrgRes = Convert.ToString(value);
                }
            }
            public System.String IdrgToInacbgImportReq
            {
                get
                {
                    System.String data = entity.IdrgToInacbgImportReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgToInacbgImportReq = null;
                    else entity.IdrgToInacbgImportReq = Convert.ToString(value);
                }
            }
            public System.String IdrgToInacbgImportRes
            {
                get
                {
                    System.String data = entity.IdrgToInacbgImportRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IdrgToInacbgImportRes = null;
                    else entity.IdrgToInacbgImportRes = Convert.ToString(value);
                }
            }
            public System.String InacbgDiagnosaGetReq
            {
                get
                {
                    System.String data = entity.InacbgDiagnosaGetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgDiagnosaGetReq = null;
                    else entity.InacbgDiagnosaGetReq = Convert.ToString(value);
                }
            }
            public System.String InacbgDiagnosaGetRes
            {
                get
                {
                    System.String data = entity.InacbgDiagnosaGetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgDiagnosaGetRes = null;
                    else entity.InacbgDiagnosaGetRes = Convert.ToString(value);
                }
            }
            public System.String InacbgDiagnosaSetReq
            {
                get
                {
                    System.String data = entity.InacbgDiagnosaSetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgDiagnosaSetReq = null;
                    else entity.InacbgDiagnosaSetReq = Convert.ToString(value);
                }
            }
            public System.String InacbgDiagnosaSetRes
            {
                get
                {
                    System.String data = entity.InacbgDiagnosaSetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgDiagnosaSetRes = null;
                    else entity.InacbgDiagnosaSetRes = Convert.ToString(value);
                }
            }
            public System.String InacbgProcedureSetReq
            {
                get
                {
                    System.String data = entity.InacbgProcedureSetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgProcedureSetReq = null;
                    else entity.InacbgProcedureSetReq = Convert.ToString(value);
                }
            }
            public System.String InacbgProcedureSetRes
            {
                get
                {
                    System.String data = entity.InacbgProcedureSetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgProcedureSetRes = null;
                    else entity.InacbgProcedureSetRes = Convert.ToString(value);
                }
            }
            public System.String InacbgProcedureGetReq
            {
                get
                {
                    System.String data = entity.InacbgProcedureGetReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgProcedureGetReq = null;
                    else entity.InacbgProcedureGetReq = Convert.ToString(value);
                }
            }
            public System.String InacbgProcedureGetRes
            {
                get
                {
                    System.String data = entity.InacbgProcedureGetRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.InacbgProcedureGetRes = null;
                    else entity.InacbgProcedureGetRes = Convert.ToString(value);
                }
            }
            public System.String GroupingInacbgStage1Req
            {
                get
                {
                    System.String data = entity.GroupingInacbgStage1Req;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingInacbgStage1Req = null;
                    else entity.GroupingInacbgStage1Req = Convert.ToString(value);
                }
            }
            public System.String GroupingInacbgStage1Res
            {
                get
                {
                    System.String data = entity.GroupingInacbgStage1Res;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingInacbgStage1Res = null;
                    else entity.GroupingInacbgStage1Res = Convert.ToString(value);
                }
            }
            public System.String GroupingInacbgStage2Req
            {
                get
                {
                    System.String data = entity.GroupingInacbgStage2Req;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingInacbgStage2Req = null;
                    else entity.GroupingInacbgStage2Req = Convert.ToString(value);
                }
            }
            public System.String GroupingInacbgStage2Res
            {
                get
                {
                    System.String data = entity.GroupingInacbgStage2Res;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GroupingInacbgStage2Res = null;
                    else entity.GroupingInacbgStage2Res = Convert.ToString(value);
                }
            }
            public System.String FinalInacbgReq
            {
                get
                {
                    System.String data = entity.FinalInacbgReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalInacbgReq = null;
                    else entity.FinalInacbgReq = Convert.ToString(value);
                }
            }
            public System.String FinalInacbgRes
            {
                get
                {
                    System.String data = entity.FinalInacbgRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FinalInacbgRes = null;
                    else entity.FinalInacbgRes = Convert.ToString(value);
                }
            }
            public System.String ReEditInacbgReq
            {
                get
                {
                    System.String data = entity.ReEditInacbgReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReEditInacbgReq = null;
                    else entity.ReEditInacbgReq = Convert.ToString(value);
                }
            }
            public System.String ReEditInacbgRes
            {
                get
                {
                    System.String data = entity.ReEditInacbgRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReEditInacbgRes = null;
                    else entity.ReEditInacbgRes = Convert.ToString(value);
                }
            }
            public System.String ClaimFinalReq
            {
                get
                {
                    System.String data = entity.ClaimFinalReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimFinalReq = null;
                    else entity.ClaimFinalReq = Convert.ToString(value);
                }
            }
            public System.String ClaimFinalRes
            {
                get
                {
                    System.String data = entity.ClaimFinalRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimFinalRes = null;
                    else entity.ClaimFinalRes = Convert.ToString(value);
                }
            }
            public System.String ClaimReEditReq
            {
                get
                {
                    System.String data = entity.ClaimReEditReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimReEditReq = null;
                    else entity.ClaimReEditReq = Convert.ToString(value);
                }
            }
            public System.String ClaimReEditRes
            {
                get
                {
                    System.String data = entity.ClaimReEditRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimReEditRes = null;
                    else entity.ClaimReEditRes = Convert.ToString(value);
                }
            }
            public System.String ClaimSendReq
            {
                get
                {
                    System.String data = entity.ClaimSendReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimSendReq = null;
                    else entity.ClaimSendReq = Convert.ToString(value);
                }
            }
            public System.String ClaimSendRes
            {
                get
                {
                    System.String data = entity.ClaimSendRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClaimSendRes = null;
                    else entity.ClaimSendRes = Convert.ToString(value);
                }
            }
            public System.String GetClaimDataReq
            {
                get
                {
                    System.String data = entity.GetClaimDataReq;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GetClaimDataReq = null;
                    else entity.GetClaimDataReq = Convert.ToString(value);
                }
            }
            public System.String GetClaimDataRes
            {
                get
                {
                    System.String data = entity.GetClaimDataRes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.GetClaimDataRes = null;
                    else entity.GetClaimDataRes = Convert.ToString(value);
                }
            }
            public System.String LastUpdateDateTime
            {
                get
                {
                    System.DateTime? data = entity.LastUpdateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
                    else entity.LastUpdateDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String LastUpdateByUserID
            {
                get
                {
                    System.String data = entity.LastUpdateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
                    else entity.LastUpdateByUserID = Convert.ToString(value);
                }
            }
            private esNccIDRG entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNccIDRGQuery query)
        {
            query.OnLoadDelegate = this.OnQueryLoaded;
            query.es2.Connection = ((IEntity)this).Connection;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        protected bool OnQueryLoaded(DataTable table)
        {
            bool dataFound = this.PopulateEntity(table);

            if (this.RowCount > 1)
            {
                throw new Exception("esNccIDRG can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NccIDRG : esNccIDRG
    {
    }

    [Serializable]
    abstract public class esNccIDRGQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NccIDRGMetadata.Meta();
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem MedicalNo
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.MedicalNo, esSystemType.String);
            }
        }

        public esQueryItem SEP
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.SEP, esSystemType.String);
            }
        }

        public esQueryItem ClaimDataRequest
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimDataRequest, esSystemType.String);
            }
        }

        public esQueryItem ClaimDataResponse
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimDataResponse, esSystemType.String);
            }
        }

        public esQueryItem IdrgDiagnosaSetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetReq, esSystemType.String);
            }
        }

        public esQueryItem IdrgDiagnosaSetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetRes, esSystemType.String);
            }
        }

        public esQueryItem IdrgDiagnosaGetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetReq, esSystemType.String);
            }
        }

        public esQueryItem IdrgDiagnosaGetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetRes, esSystemType.String);
            }
        }

        public esQueryItem IdrgProcedureSetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgProcedureSetReq, esSystemType.String);
            }
        }

        public esQueryItem IdrgProcedureSetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgProcedureSetRes, esSystemType.String);
            }
        }

        public esQueryItem IdrgProcedureGetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgProcedureGetReq, esSystemType.String);
            }
        }

        public esQueryItem IdrgProcedureGetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgProcedureGetRes, esSystemType.String);
            }
        }

        public esQueryItem GroupingIdrgReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingIdrgReq, esSystemType.String);
            }
        }

        public esQueryItem GroupingIdrgRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingIdrgRes, esSystemType.String);
            }
        }

        public esQueryItem FinalIdrgReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.FinalIdrgReq, esSystemType.String);
            }
        }

        public esQueryItem FinalIdrgRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.FinalIdrgRes, esSystemType.String);
            }
        }

        public esQueryItem ReEditIdrgReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ReEditIdrgReq, esSystemType.String);
            }
        }

        public esQueryItem ReEditIdrgRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ReEditIdrgRes, esSystemType.String);
            }
        }

        public esQueryItem IdrgToInacbgImportReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgToInacbgImportReq, esSystemType.String);
            }
        }

        public esQueryItem IdrgToInacbgImportRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.IdrgToInacbgImportRes, esSystemType.String);
            }
        }

        public esQueryItem InacbgDiagnosaGetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetReq, esSystemType.String);
            }
        }

        public esQueryItem InacbgDiagnosaGetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetRes, esSystemType.String);
            }
        }

        public esQueryItem InacbgDiagnosaSetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetReq, esSystemType.String);
            }
        }

        public esQueryItem InacbgDiagnosaSetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetRes, esSystemType.String);
            }
        }

        public esQueryItem InacbgProcedureSetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgProcedureSetReq, esSystemType.String);
            }
        }

        public esQueryItem InacbgProcedureSetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgProcedureSetRes, esSystemType.String);
            }
        }

        public esQueryItem InacbgProcedureGetReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgProcedureGetReq, esSystemType.String);
            }
        }

        public esQueryItem InacbgProcedureGetRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.InacbgProcedureGetRes, esSystemType.String);
            }
        }

        public esQueryItem GroupingInacbgStage1Req
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Req, esSystemType.String);
            }
        }

        public esQueryItem GroupingInacbgStage1Res
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Res, esSystemType.String);
            }
        }

        public esQueryItem GroupingInacbgStage2Req
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Req, esSystemType.String);
            }
        }

        public esQueryItem GroupingInacbgStage2Res
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Res, esSystemType.String);
            }
        }

        public esQueryItem FinalInacbgReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.FinalInacbgReq, esSystemType.String);
            }
        }

        public esQueryItem FinalInacbgRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.FinalInacbgRes, esSystemType.String);
            }
        }

        public esQueryItem ReEditInacbgReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ReEditInacbgReq, esSystemType.String);
            }
        }

        public esQueryItem ReEditInacbgRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ReEditInacbgRes, esSystemType.String);
            }
        }

        public esQueryItem ClaimFinalReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimFinalReq, esSystemType.String);
            }
        }

        public esQueryItem ClaimFinalRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimFinalRes, esSystemType.String);
            }
        }

        public esQueryItem ClaimReEditReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimReEditReq, esSystemType.String);
            }
        }

        public esQueryItem ClaimReEditRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimReEditRes, esSystemType.String);
            }
        }

        public esQueryItem ClaimSendReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimSendReq, esSystemType.String);
            }
        }

        public esQueryItem ClaimSendRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.ClaimSendRes, esSystemType.String);
            }
        }

        public esQueryItem GetClaimDataReq
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GetClaimDataReq, esSystemType.String);
            }
        }

        public esQueryItem GetClaimDataRes
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.GetClaimDataRes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NccIDRGMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NccIDRGCollection")]
    public partial class NccIDRGCollection : esNccIDRGCollection, IEnumerable<NccIDRG>
    {
        public NccIDRGCollection()
        {

        }

        public static implicit operator List<NccIDRG>(NccIDRGCollection coll)
        {
            List<NccIDRG> list = new List<NccIDRG>();

            foreach (NccIDRG emp in coll)
            {
                list.Add(emp);
            }

            return list;
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NccIDRGMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NccIDRGQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NccIDRG(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NccIDRG();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NccIDRGQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NccIDRGQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one record was loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(NccIDRGQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NccIDRG AddNew()
        {
            NccIDRG entity = base.AddNewEntity() as NccIDRG;

            return entity;
        }
        public NccIDRG FindByPrimaryKey(String registrationNo)
        {
            return base.FindByPrimaryKey(registrationNo) as NccIDRG;
        }

        #region IEnumerable< NccIDRG> Members

        IEnumerator<NccIDRG> IEnumerable<NccIDRG>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NccIDRG;
            }
        }

        #endregion

        private NccIDRGQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NccIDRG' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NccIDRG ({RegistrationNo})")]
    [Serializable]
    public partial class NccIDRG : esNccIDRG
    {
        public NccIDRG()
        {
        }

        public NccIDRG(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NccIDRGMetadata.Meta();
            }
        }

        override protected esNccIDRGQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NccIDRGQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NccIDRGQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NccIDRGQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        /// <summary>
        /// Useful for building up conditional queries.
        /// In most cases, before loading an entity or collection,
        /// you should instantiate a new one. This method was added
        /// to handle specialized circumstances, and should not be
        /// used as a substitute for that.
        /// </summary>
        /// <remarks>
        /// This just sets obj.Query to null/Nothing.
        /// In most cases, you will 'new' your object before
        /// loading it, rather than calling this method.
        /// It only affects obj.Query.Load(), so is not useful
        /// when Joins are involved, or for many other situations.
        /// Because it clears out any obj.Query.Where clauses,
        /// it can be useful for building conditional queries on the fly.
        /// <code>
        /// public bool ReQuery(string lastName, string firstName)
        /// {
        ///     this.QueryReset();
        ///     
        ///     if(!String.IsNullOrEmpty(lastName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.LastName == lastName);
        ///     }
        ///     if(!String.IsNullOrEmpty(firstName))
        ///     {
        ///         this.Query.Where(
        ///             this.Query.FirstName == firstName);
        ///     }
        ///     
        ///     return this.Query.Load();
        /// }
        /// </code>
        /// <code lang="vbnet">
        /// Public Function ReQuery(ByVal lastName As String, _
        ///     ByVal firstName As String) As Boolean
        /// 
        ///     Me.QueryReset()
        /// 
        ///     If Not [String].IsNullOrEmpty(lastName) Then
        ///         Me.Query.Where(Me.Query.LastName = lastName)
        ///     End If
        ///     If Not [String].IsNullOrEmpty(firstName) Then
        ///         Me.Query.Where(Me.Query.FirstName = firstName)
        ///     End If
        /// 
        ///     Return Me.Query.Load()
        /// End Function
        /// </code>
        /// </remarks>
        public void QueryReset()
        {
            this.query = null;
        }

        /// <summary>
        /// Used to custom load a Join query.
        /// Returns true if at least one row is loaded.
        /// For an entity, an exception will be thrown
        /// if more than one row is loaded.
        /// </summary>
        /// <remarks>
        /// Provides support for InnerJoin, LeftJoin,
        /// RightJoin, and FullJoin. You must provide an alias
        /// for each query when instantiating them.
        /// <code>
        /// EmployeeCollection collection = new EmployeeCollection();
        /// 
        /// EmployeeQuery emp = new EmployeeQuery("eq");
        /// CustomerQuery cust = new CustomerQuery("cq");
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
        /// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
        /// 
        /// collection.Load(emp);
        /// </code>
        /// <code lang="vbnet">
        /// Dim collection As New EmployeeCollection()
        /// 
        /// Dim emp As New EmployeeQuery("eq")
        /// Dim cust As New CustomerQuery("cq")
        /// 
        /// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
        /// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
        /// 
        /// collection.Load(emp)
        /// </code>
        /// </remarks>
        /// <param name="query">The query object instance name.</param>
        /// <returns>True if at least one record was loaded.</returns>
        public bool Load(NccIDRGQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NccIDRGQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NccIDRGQuery : esNccIDRGQuery
    {
        public NccIDRGQuery()
        {

        }

        public NccIDRGQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NccIDRGQuery";
        }
    }

    [Serializable]
    public partial class NccIDRGMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NccIDRGMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.MedicalNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.MedicalNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.SEP, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.SEP;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimDataRequest, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimDataRequest;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimDataResponse, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimDataResponse;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetReq, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgDiagnosaSetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgDiagnosaSetRes, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgDiagnosaSetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetReq, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgDiagnosaGetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgDiagnosaGetRes, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgDiagnosaGetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgProcedureSetReq, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgProcedureSetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgProcedureSetRes, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgProcedureSetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgProcedureGetReq, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgProcedureGetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgProcedureGetRes, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgProcedureGetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingIdrgReq, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingIdrgReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingIdrgRes, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingIdrgRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.FinalIdrgReq, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.FinalIdrgReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.FinalIdrgRes, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.FinalIdrgRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ReEditIdrgReq, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ReEditIdrgReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ReEditIdrgRes, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ReEditIdrgRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportReq, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgToInacbgImportReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.IdrgToInacbgImportRes, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.IdrgToInacbgImportRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetReq, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgDiagnosaGetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgDiagnosaGetRes, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgDiagnosaGetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetReq, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgDiagnosaSetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgDiagnosaSetRes, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgDiagnosaSetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgProcedureSetReq, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgProcedureSetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgProcedureSetRes, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgProcedureSetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgProcedureGetReq, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgProcedureGetReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.InacbgProcedureGetRes, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.InacbgProcedureGetRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Req, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingInacbgStage1Req;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingInacbgStage1Res, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingInacbgStage1Res;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Req, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingInacbgStage2Req;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GroupingInacbgStage2Res, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GroupingInacbgStage2Res;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.FinalInacbgReq, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.FinalInacbgReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.FinalInacbgRes, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.FinalInacbgRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ReEditInacbgReq, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ReEditInacbgReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ReEditInacbgRes, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ReEditInacbgRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimFinalReq, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimFinalReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimFinalRes, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimFinalRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimReEditReq, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimReEditReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimReEditRes, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimReEditRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimSendReq, 41, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimSendReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.ClaimSendRes, 42, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.ClaimSendRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GetClaimDataReq, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GetClaimDataReq;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.GetClaimDataRes, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.GetClaimDataRes;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.LastUpdateDateTime, 45, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NccIDRGMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NccIDRGMetadata.ColumnNames.LastUpdateByUserID, 46, typeof(System.String), esSystemType.String);
            c.PropertyName = NccIDRGMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public NccIDRGMetadata Meta()
        {
            return meta;
        }

        public Guid DataID
        {
            get { return base._dataID; }
        }

        public bool MultiProviderMode
        {
            get { return false; }
        }

        public esColumnMetadataCollection Columns
        {
            get { return base._columns; }
        }

        #region ColumnNames
        public class ColumnNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string MedicalNo = "MedicalNo";
            public const string SEP = "SEP";
            public const string ClaimDataRequest = "ClaimDataRequest";
            public const string ClaimDataResponse = "ClaimDataResponse";
            public const string IdrgDiagnosaSetReq = "IdrgDiagnosaSetReq";
            public const string IdrgDiagnosaSetRes = "IdrgDiagnosaSetRes";
            public const string IdrgDiagnosaGetReq = "IdrgDiagnosaGetReq";
            public const string IdrgDiagnosaGetRes = "IdrgDiagnosaGetRes";
            public const string IdrgProcedureSetReq = "IdrgProcedureSetReq";
            public const string IdrgProcedureSetRes = "IdrgProcedureSetRes";
            public const string IdrgProcedureGetReq = "IdrgProcedureGetReq";
            public const string IdrgProcedureGetRes = "IdrgProcedureGetRes";
            public const string GroupingIdrgReq = "GroupingIdrgReq";
            public const string GroupingIdrgRes = "GroupingIdrgRes";
            public const string FinalIdrgReq = "FinalIdrgReq";
            public const string FinalIdrgRes = "FinalIdrgRes";
            public const string ReEditIdrgReq = "ReEditIdrgReq";
            public const string ReEditIdrgRes = "ReEditIdrgRes";
            public const string IdrgToInacbgImportReq = "IdrgToInacbgImportReq";
            public const string IdrgToInacbgImportRes = "IdrgToInacbgImportRes";
            public const string InacbgDiagnosaGetReq = "InacbgDiagnosaGetReq";
            public const string InacbgDiagnosaGetRes = "InacbgDiagnosaGetRes";
            public const string InacbgDiagnosaSetReq = "InacbgDiagnosaSetReq";
            public const string InacbgDiagnosaSetRes = "InacbgDiagnosaSetRes";
            public const string InacbgProcedureSetReq = "InacbgProcedureSetReq";
            public const string InacbgProcedureSetRes = "InacbgProcedureSetRes";
            public const string InacbgProcedureGetReq = "InacbgProcedureGetReq";
            public const string InacbgProcedureGetRes = "InacbgProcedureGetRes";
            public const string GroupingInacbgStage1Req = "GroupingInacbgStage1Req";
            public const string GroupingInacbgStage1Res = "GroupingInacbgStage1Res";
            public const string GroupingInacbgStage2Req = "GroupingInacbgStage2Req";
            public const string GroupingInacbgStage2Res = "GroupingInacbgStage2Res";
            public const string FinalInacbgReq = "FinalInacbgReq";
            public const string FinalInacbgRes = "FinalInacbgRes";
            public const string ReEditInacbgReq = "ReEditInacbgReq";
            public const string ReEditInacbgRes = "ReEditInacbgRes";
            public const string ClaimFinalReq = "ClaimFinalReq";
            public const string ClaimFinalRes = "ClaimFinalRes";
            public const string ClaimReEditReq = "ClaimReEditReq";
            public const string ClaimReEditRes = "ClaimReEditRes";
            public const string ClaimSendReq = "ClaimSendReq";
            public const string ClaimSendRes = "ClaimSendRes";
            public const string GetClaimDataReq = "GetClaimDataReq";
            public const string GetClaimDataRes = "GetClaimDataRes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string RegistrationNo = "RegistrationNo";
            public const string MedicalNo = "MedicalNo";
            public const string SEP = "SEP";
            public const string ClaimDataRequest = "ClaimDataRequest";
            public const string ClaimDataResponse = "ClaimDataResponse";
            public const string IdrgDiagnosaSetReq = "IdrgDiagnosaSetReq";
            public const string IdrgDiagnosaSetRes = "IdrgDiagnosaSetRes";
            public const string IdrgDiagnosaGetReq = "IdrgDiagnosaGetReq";
            public const string IdrgDiagnosaGetRes = "IdrgDiagnosaGetRes";
            public const string IdrgProcedureSetReq = "IdrgProcedureSetReq";
            public const string IdrgProcedureSetRes = "IdrgProcedureSetRes";
            public const string IdrgProcedureGetReq = "IdrgProcedureGetReq";
            public const string IdrgProcedureGetRes = "IdrgProcedureGetRes";
            public const string GroupingIdrgReq = "GroupingIdrgReq";
            public const string GroupingIdrgRes = "GroupingIdrgRes";
            public const string FinalIdrgReq = "FinalIdrgReq";
            public const string FinalIdrgRes = "FinalIdrgRes";
            public const string ReEditIdrgReq = "ReEditIdrgReq";
            public const string ReEditIdrgRes = "ReEditIdrgRes";
            public const string IdrgToInacbgImportReq = "IdrgToInacbgImportReq";
            public const string IdrgToInacbgImportRes = "IdrgToInacbgImportRes";
            public const string InacbgDiagnosaGetReq = "InacbgDiagnosaGetReq";
            public const string InacbgDiagnosaGetRes = "InacbgDiagnosaGetRes";
            public const string InacbgDiagnosaSetReq = "InacbgDiagnosaSetReq";
            public const string InacbgDiagnosaSetRes = "InacbgDiagnosaSetRes";
            public const string InacbgProcedureSetReq = "InacbgProcedureSetReq";
            public const string InacbgProcedureSetRes = "InacbgProcedureSetRes";
            public const string InacbgProcedureGetReq = "InacbgProcedureGetReq";
            public const string InacbgProcedureGetRes = "InacbgProcedureGetRes";
            public const string GroupingInacbgStage1Req = "GroupingInacbgStage1Req";
            public const string GroupingInacbgStage1Res = "GroupingInacbgStage1Res";
            public const string GroupingInacbgStage2Req = "GroupingInacbgStage2Req";
            public const string GroupingInacbgStage2Res = "GroupingInacbgStage2Res";
            public const string FinalInacbgReq = "FinalInacbgReq";
            public const string FinalInacbgRes = "FinalInacbgRes";
            public const string ReEditInacbgReq = "ReEditInacbgReq";
            public const string ReEditInacbgRes = "ReEditInacbgRes";
            public const string ClaimFinalReq = "ClaimFinalReq";
            public const string ClaimFinalRes = "ClaimFinalRes";
            public const string ClaimReEditReq = "ClaimReEditReq";
            public const string ClaimReEditRes = "ClaimReEditRes";
            public const string ClaimSendReq = "ClaimSendReq";
            public const string ClaimSendRes = "ClaimSendRes";
            public const string GetClaimDataReq = "GetClaimDataReq";
            public const string GetClaimDataRes = "GetClaimDataRes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        public esProviderSpecificMetadata GetProviderMetadata(string mapName)
        {
            MapToMeta mapMethod = mapDelegates[mapName];

            if (mapMethod != null)
                return mapMethod(mapName);
            else
                return null;
        }

        #region MAP esDefault

        static private int RegisterDelegateesDefault()
        {
            // This is only executed once per the life of the application
            lock (typeof(NccIDRGMetadata))
            {
                if (NccIDRGMetadata.mapDelegates == null)
                {
                    NccIDRGMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NccIDRGMetadata.meta == null)
                {
                    NccIDRGMetadata.meta = new NccIDRGMetadata();
                }

                MapToMeta mapMethod = new MapToMeta(meta.esDefault);
                mapDelegates.Add("esDefault", mapMethod);
                mapMethod("esDefault");
            }
            return 0;
        }

        private esProviderSpecificMetadata esDefault(string mapName)
        {
            if (!_providerMetadataMaps.ContainsKey(mapName))
            {
                esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("MedicalNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SEP", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimDataRequest", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimDataResponse", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgDiagnosaSetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgDiagnosaSetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgDiagnosaGetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgDiagnosaGetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgProcedureSetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgProcedureSetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgProcedureGetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgProcedureGetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingIdrgReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingIdrgRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalIdrgReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalIdrgRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReEditIdrgReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReEditIdrgRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgToInacbgImportReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IdrgToInacbgImportRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgDiagnosaGetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgDiagnosaGetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgDiagnosaSetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgDiagnosaSetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgProcedureSetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgProcedureSetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgProcedureGetReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("InacbgProcedureGetRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingInacbgStage1Req", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingInacbgStage1Res", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingInacbgStage2Req", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GroupingInacbgStage2Res", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalInacbgReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("FinalInacbgRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReEditInacbgReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReEditInacbgRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimFinalReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimFinalRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimReEditReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimReEditRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimSendReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ClaimSendRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GetClaimDataReq", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("GetClaimDataRes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "NccIDRG";
                meta.Destination = "NccIDRG";
                meta.spInsert = "proc_NccIDRGInsert";
                meta.spUpdate = "proc_NccIDRGUpdate";
                meta.spDelete = "proc_NccIDRGDelete";
                meta.spLoadAll = "proc_NccIDRGLoadAll";
                meta.spLoadByPrimaryKey = "proc_NccIDRGLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NccIDRGMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
