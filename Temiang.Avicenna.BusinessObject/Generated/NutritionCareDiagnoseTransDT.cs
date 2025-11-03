/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/16/2019 1:39:59 PM
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
    abstract public class esNutritionCareDiagnoseTransDTCollection : esEntityCollectionWAuditLog
    {
        public esNutritionCareDiagnoseTransDTCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "NutritionCareDiagnoseTransDTCollection";
        }

        #region Query Logic
        protected void InitQuery(esNutritionCareDiagnoseTransDTQuery query)
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
            this.InitQuery(query as esNutritionCareDiagnoseTransDTQuery);
        }
        #endregion

        virtual public NutritionCareDiagnoseTransDT DetachEntity(NutritionCareDiagnoseTransDT entity)
        {
            return base.DetachEntity(entity) as NutritionCareDiagnoseTransDT;
        }

        virtual public NutritionCareDiagnoseTransDT AttachEntity(NutritionCareDiagnoseTransDT entity)
        {
            return base.AttachEntity(entity) as NutritionCareDiagnoseTransDT;
        }

        virtual public void Combine(NutritionCareDiagnoseTransDTCollection collection)
        {
            base.Combine(collection);
        }

        new public NutritionCareDiagnoseTransDT this[int index]
        {
            get
            {
                return base[index] as NutritionCareDiagnoseTransDT;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(NutritionCareDiagnoseTransDT);
        }
    }

    [Serializable]
    abstract public class esNutritionCareDiagnoseTransDT : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esNutritionCareDiagnoseTransDTQuery GetDynamicQuery()
        {
            return null;
        }

        public esNutritionCareDiagnoseTransDT()
        {
        }

        public esNutritionCareDiagnoseTransDT(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(Int64 iD)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int64 iD)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(iD);
            else
                return LoadByPrimaryKeyStoredProcedure(iD);
        }

        private bool LoadByPrimaryKeyDynamic(Int64 iD)
        {
            esNutritionCareDiagnoseTransDTQuery query = this.GetDynamicQuery();
            query.Where(query.ID == iD);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(Int64 iD)
        {
            esParameters parms = new esParameters();
            parms.Add("ID", iD);
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
                        case "ID": this.str.ID = (string)value; break;
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TerminologyID": this.str.TerminologyID = (string)value; break;
                        case "TerminologyName": this.str.TerminologyName = (string)value; break;
                        case "SRNutritionCareTerminologyLevel": this.str.SRNutritionCareTerminologyLevel = (string)value; break;
                        case "TerminologyParentID": this.str.TerminologyParentID = (string)value; break;
                        case "TmpTerminologyID": this.str.TmpTerminologyID = (string)value; break;
                        case "TmpTerminologyParentID": this.str.TmpTerminologyParentID = (string)value; break;
                        case "ParentID": this.str.ParentID = (string)value; break;
                        case "ReferenceToPhrNo": this.str.ReferenceToPhrNo = (string)value; break;
                        case "S": this.str.S = (string)value; break;
                        case "O": this.str.O = (string)value; break;
                        case "D": this.str.D = (string)value; break;
                        case "I": this.str.I = (string)value; break;
                        case "ME": this.str.ME = (string)value; break;
                        case "ExecuteDateTime": this.str.ExecuteDateTime = (string)value; break;
                        case "IsDeleted": this.str.IsDeleted = (string)value; break;
                        case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
                        case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "ID":

                            if (value == null || value is System.Int64)
                                this.ID = (System.Int64?)value;
                            break;
                        case "ParentID":

                            if (value == null || value is System.Int64)
                                this.ParentID = (System.Int64?)value;
                            break;
                        case "ExecuteDateTime":

                            if (value == null || value is System.DateTime)
                                this.ExecuteDateTime = (System.DateTime?)value;
                            break;
                        case "IsDeleted":

                            if (value == null || value is System.Boolean)
                                this.IsDeleted = (System.Boolean?)value;
                            break;
                        case "CreateDateTime":

                            if (value == null || value is System.DateTime)
                                this.CreateDateTime = (System.DateTime?)value;
                            break;
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
        /// Maps to NutritionCareDiagnoseTransDT.ID
        /// </summary>
        virtual public System.Int64? ID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TransactionNo, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TerminologyID
        /// </summary>
        virtual public System.String TerminologyID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TerminologyName
        /// </summary>
        virtual public System.String TerminologyName
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyName);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyName, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.SRNutritionCareTerminologyLevel
        /// </summary>
        virtual public System.String SRNutritionCareTerminologyLevel
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.SRNutritionCareTerminologyLevel);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.SRNutritionCareTerminologyLevel, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TerminologyParentID
        /// </summary>
        virtual public System.String TerminologyParentID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyParentID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyParentID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TmpTerminologyID
        /// </summary>
        virtual public System.String TmpTerminologyID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.TmpTerminologyParentID
        /// </summary>
        virtual public System.String TmpTerminologyParentID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyParentID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyParentID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.ParentID
        /// </summary>
        virtual public System.Int64? ParentID
        {
            get
            {
                return base.GetSystemInt64(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ParentID);
            }

            set
            {
                base.SetSystemInt64(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ParentID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.ReferenceToPhrNo
        /// </summary>
        virtual public System.String ReferenceToPhrNo
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ReferenceToPhrNo);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ReferenceToPhrNo, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.S
        /// </summary>
        virtual public System.String S
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.S);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.S, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.O
        /// </summary>
        virtual public System.String O
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.O);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.O, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.D
        /// </summary>
        virtual public System.String D
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.D);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.D, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.I
        /// </summary>
        virtual public System.String I
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.I);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.I, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.ME
        /// </summary>
        virtual public System.String ME
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ME);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ME, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.ExecuteDateTime
        /// </summary>
        virtual public System.DateTime? ExecuteDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ExecuteDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ExecuteDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.IsDeleted
        /// </summary>
        virtual public System.Boolean? IsDeleted
        {
            get
            {
                return base.GetSystemBoolean(NutritionCareDiagnoseTransDTMetadata.ColumnNames.IsDeleted);
            }

            set
            {
                base.SetSystemBoolean(NutritionCareDiagnoseTransDTMetadata.ColumnNames.IsDeleted, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.CreateByUserID
        /// </summary>
        virtual public System.String CreateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.CreateDateTime
        /// </summary>
        virtual public System.DateTime? CreateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateByUserID, value);
            }
        }
        /// <summary>
        /// Maps to NutritionCareDiagnoseTransDT.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateDateTime, value);
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
            public esStrings(esNutritionCareDiagnoseTransDT entity)
            {
                this.entity = entity;
            }
            public System.String ID
            {
                get
                {
                    System.Int64? data = entity.ID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ID = null;
                    else entity.ID = Convert.ToInt64(value);
                }
            }
            public System.String TransactionNo
            {
                get
                {
                    System.String data = entity.TransactionNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionNo = null;
                    else entity.TransactionNo = Convert.ToString(value);
                }
            }
            public System.String TerminologyID
            {
                get
                {
                    System.String data = entity.TerminologyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyID = null;
                    else entity.TerminologyID = Convert.ToString(value);
                }
            }
            public System.String TerminologyName
            {
                get
                {
                    System.String data = entity.TerminologyName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyName = null;
                    else entity.TerminologyName = Convert.ToString(value);
                }
            }
            public System.String SRNutritionCareTerminologyLevel
            {
                get
                {
                    System.String data = entity.SRNutritionCareTerminologyLevel;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRNutritionCareTerminologyLevel = null;
                    else entity.SRNutritionCareTerminologyLevel = Convert.ToString(value);
                }
            }
            public System.String TerminologyParentID
            {
                get
                {
                    System.String data = entity.TerminologyParentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TerminologyParentID = null;
                    else entity.TerminologyParentID = Convert.ToString(value);
                }
            }
            public System.String TmpTerminologyID
            {
                get
                {
                    System.String data = entity.TmpTerminologyID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TmpTerminologyID = null;
                    else entity.TmpTerminologyID = Convert.ToString(value);
                }
            }
            public System.String TmpTerminologyParentID
            {
                get
                {
                    System.String data = entity.TmpTerminologyParentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TmpTerminologyParentID = null;
                    else entity.TmpTerminologyParentID = Convert.ToString(value);
                }
            }
            public System.String ParentID
            {
                get
                {
                    System.Int64? data = entity.ParentID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ParentID = null;
                    else entity.ParentID = Convert.ToInt64(value);
                }
            }
            public System.String ReferenceToPhrNo
            {
                get
                {
                    System.String data = entity.ReferenceToPhrNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceToPhrNo = null;
                    else entity.ReferenceToPhrNo = Convert.ToString(value);
                }
            }
            public System.String S
            {
                get
                {
                    System.String data = entity.S;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.S = null;
                    else entity.S = Convert.ToString(value);
                }
            }
            public System.String O
            {
                get
                {
                    System.String data = entity.O;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.O = null;
                    else entity.O = Convert.ToString(value);
                }
            }
            public System.String D
            {
                get
                {
                    System.String data = entity.D;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.D = null;
                    else entity.D = Convert.ToString(value);
                }
            }
            public System.String I
            {
                get
                {
                    System.String data = entity.I;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.I = null;
                    else entity.I = Convert.ToString(value);
                }
            }
            public System.String ME
            {
                get
                {
                    System.String data = entity.ME;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ME = null;
                    else entity.ME = Convert.ToString(value);
                }
            }
            public System.String ExecuteDateTime
            {
                get
                {
                    System.DateTime? data = entity.ExecuteDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ExecuteDateTime = null;
                    else entity.ExecuteDateTime = Convert.ToDateTime(value);
                }
            }
            public System.String IsDeleted
            {
                get
                {
                    System.Boolean? data = entity.IsDeleted;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsDeleted = null;
                    else entity.IsDeleted = Convert.ToBoolean(value);
                }
            }
            public System.String CreateByUserID
            {
                get
                {
                    System.String data = entity.CreateByUserID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateByUserID = null;
                    else entity.CreateByUserID = Convert.ToString(value);
                }
            }
            public System.String CreateDateTime
            {
                get
                {
                    System.DateTime? data = entity.CreateDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CreateDateTime = null;
                    else entity.CreateDateTime = Convert.ToDateTime(value);
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
            private esNutritionCareDiagnoseTransDT entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esNutritionCareDiagnoseTransDTQuery query)
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
                throw new Exception("esNutritionCareDiagnoseTransDT can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class NutritionCareDiagnoseTransDT : esNutritionCareDiagnoseTransDT
    {
    }

    [Serializable]
    abstract public class esNutritionCareDiagnoseTransDTQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareDiagnoseTransDTMetadata.Meta();
            }
        }

        public esQueryItem ID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ID, esSystemType.Int64);
            }
        }

        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TerminologyID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyID, esSystemType.String);
            }
        }

        public esQueryItem TerminologyName
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyName, esSystemType.String);
            }
        }

        public esQueryItem SRNutritionCareTerminologyLevel
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.SRNutritionCareTerminologyLevel, esSystemType.String);
            }
        }

        public esQueryItem TerminologyParentID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyParentID, esSystemType.String);
            }
        }

        public esQueryItem TmpTerminologyID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyID, esSystemType.String);
            }
        }

        public esQueryItem TmpTerminologyParentID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyParentID, esSystemType.String);
            }
        }

        public esQueryItem ParentID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ParentID, esSystemType.Int64);
            }
        }

        public esQueryItem ReferenceToPhrNo
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ReferenceToPhrNo, esSystemType.String);
            }
        }

        public esQueryItem S
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.S, esSystemType.String);
            }
        }

        public esQueryItem O
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.O, esSystemType.String);
            }
        }

        public esQueryItem D
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.D, esSystemType.String);
            }
        }

        public esQueryItem I
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.I, esSystemType.String);
            }
        }

        public esQueryItem ME
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ME, esSystemType.String);
            }
        }

        public esQueryItem ExecuteDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.ExecuteDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem IsDeleted
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.IsDeleted, esSystemType.Boolean);
            }
        }

        public esQueryItem CreateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateByUserID, esSystemType.String);
            }
        }

        public esQueryItem CreateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("NutritionCareDiagnoseTransDTCollection")]
    public partial class NutritionCareDiagnoseTransDTCollection : esNutritionCareDiagnoseTransDTCollection, IEnumerable<NutritionCareDiagnoseTransDT>
    {
        public NutritionCareDiagnoseTransDTCollection()
        {

        }

        public static implicit operator List<NutritionCareDiagnoseTransDT>(NutritionCareDiagnoseTransDTCollection coll)
        {
            List<NutritionCareDiagnoseTransDT> list = new List<NutritionCareDiagnoseTransDT>();

            foreach (NutritionCareDiagnoseTransDT emp in coll)
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
                return NutritionCareDiagnoseTransDTMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareDiagnoseTransDTQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new NutritionCareDiagnoseTransDT(row);
        }

        override protected esEntity CreateEntity()
        {
            return new NutritionCareDiagnoseTransDT();
        }

        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareDiagnoseTransDTQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareDiagnoseTransDTQuery();
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
        public bool Load(NutritionCareDiagnoseTransDTQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public NutritionCareDiagnoseTransDT AddNew()
        {
            NutritionCareDiagnoseTransDT entity = base.AddNewEntity() as NutritionCareDiagnoseTransDT;

            return entity;
        }
        public NutritionCareDiagnoseTransDT FindByPrimaryKey(Int64 iD)
        {
            return base.FindByPrimaryKey(iD) as NutritionCareDiagnoseTransDT;
        }

        #region IEnumerable< NutritionCareDiagnoseTransDT> Members

        IEnumerator<NutritionCareDiagnoseTransDT> IEnumerable<NutritionCareDiagnoseTransDT>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as NutritionCareDiagnoseTransDT;
            }
        }

        #endregion

        private NutritionCareDiagnoseTransDTQuery query;
    }


    /// <summary>
    /// Encapsulates the 'NutritionCareDiagnoseTransDT' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("NutritionCareDiagnoseTransDT ({ID})")]
    [Serializable]
    public partial class NutritionCareDiagnoseTransDT : esNutritionCareDiagnoseTransDT
    {
        public NutritionCareDiagnoseTransDT()
        {
        }

        public NutritionCareDiagnoseTransDT(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return NutritionCareDiagnoseTransDTMetadata.Meta();
            }
        }

        override protected esNutritionCareDiagnoseTransDTQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new NutritionCareDiagnoseTransDTQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public NutritionCareDiagnoseTransDTQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new NutritionCareDiagnoseTransDTQuery();
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
        public bool Load(NutritionCareDiagnoseTransDTQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private NutritionCareDiagnoseTransDTQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class NutritionCareDiagnoseTransDTQuery : esNutritionCareDiagnoseTransDTQuery
    {
        public NutritionCareDiagnoseTransDTQuery()
        {

        }

        public NutritionCareDiagnoseTransDTQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "NutritionCareDiagnoseTransDTQuery";
        }
    }

    [Serializable]
    public partial class NutritionCareDiagnoseTransDTMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected NutritionCareDiagnoseTransDTMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ID, 0, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.ID;
            c.IsInPrimaryKey = true;
            c.IsAutoIncrement = true;
            c.NumericPrecision = 19;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TransactionNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TransactionNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyID, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TerminologyID;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyName, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TerminologyName;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.SRNutritionCareTerminologyLevel, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.SRNutritionCareTerminologyLevel;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TerminologyParentID, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TerminologyParentID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyID, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TmpTerminologyID;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.TmpTerminologyParentID, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.TmpTerminologyParentID;
            c.CharacterMaxLength = 20;
            c.HasDefault = true;
            c.Default = @"('')";
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ParentID, 8, typeof(System.Int64), esSystemType.Int64);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.ParentID;
            c.NumericPrecision = 19;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ReferenceToPhrNo, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.ReferenceToPhrNo;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.S, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.S;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.O, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.O;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.D, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.D;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.I, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.I;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ME, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.ME;
            c.CharacterMaxLength = 2147483647;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.ExecuteDateTime, 15, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.ExecuteDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.IsDeleted, 16, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.IsDeleted;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateByUserID, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.CreateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.CreateDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.CreateDateTime;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateByUserID, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(NutritionCareDiagnoseTransDTMetadata.ColumnNames.LastUpdateDateTime, 20, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = NutritionCareDiagnoseTransDTMetadata.PropertyNames.LastUpdateDateTime;
            _columns.Add(c);


        }
        #endregion

        static public NutritionCareDiagnoseTransDTMetadata Meta()
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
            public const string ID = "ID";
            public const string TransactionNo = "TransactionNo";
            public const string TerminologyID = "TerminologyID";
            public const string TerminologyName = "TerminologyName";
            public const string SRNutritionCareTerminologyLevel = "SRNutritionCareTerminologyLevel";
            public const string TerminologyParentID = "TerminologyParentID";
            public const string TmpTerminologyID = "TmpTerminologyID";
            public const string TmpTerminologyParentID = "TmpTerminologyParentID";
            public const string ParentID = "ParentID";
            public const string ReferenceToPhrNo = "ReferenceToPhrNo";
            public const string S = "S";
            public const string O = "O";
            public const string D = "D";
            public const string I = "I";
            public const string ME = "ME";
            public const string ExecuteDateTime = "ExecuteDateTime";
            public const string IsDeleted = "IsDeleted";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ID = "ID";
            public const string TransactionNo = "TransactionNo";
            public const string TerminologyID = "TerminologyID";
            public const string TerminologyName = "TerminologyName";
            public const string SRNutritionCareTerminologyLevel = "SRNutritionCareTerminologyLevel";
            public const string TerminologyParentID = "TerminologyParentID";
            public const string TmpTerminologyID = "TmpTerminologyID";
            public const string TmpTerminologyParentID = "TmpTerminologyParentID";
            public const string ParentID = "ParentID";
            public const string ReferenceToPhrNo = "ReferenceToPhrNo";
            public const string S = "S";
            public const string O = "O";
            public const string D = "D";
            public const string I = "I";
            public const string ME = "ME";
            public const string ExecuteDateTime = "ExecuteDateTime";
            public const string IsDeleted = "IsDeleted";
            public const string CreateByUserID = "CreateByUserID";
            public const string CreateDateTime = "CreateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
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
            lock (typeof(NutritionCareDiagnoseTransDTMetadata))
            {
                if (NutritionCareDiagnoseTransDTMetadata.mapDelegates == null)
                {
                    NutritionCareDiagnoseTransDTMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (NutritionCareDiagnoseTransDTMetadata.meta == null)
                {
                    NutritionCareDiagnoseTransDTMetadata.meta = new NutritionCareDiagnoseTransDTMetadata();
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

                meta.AddTypeMap("ID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRNutritionCareTerminologyLevel", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TerminologyParentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TmpTerminologyID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TmpTerminologyParentID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ParentID", new esTypeMap("bigint", "System.Int64"));
                meta.AddTypeMap("ReferenceToPhrNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("S", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("O", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("D", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("I", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ME", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ExecuteDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));


                meta.Source = "NutritionCareDiagnoseTransDT";
                meta.Destination = "NutritionCareDiagnoseTransDT";
                meta.spInsert = "proc_NutritionCareDiagnoseTransDTInsert";
                meta.spUpdate = "proc_NutritionCareDiagnoseTransDTUpdate";
                meta.spDelete = "proc_NutritionCareDiagnoseTransDTDelete";
                meta.spLoadAll = "proc_NutritionCareDiagnoseTransDTLoadAll";
                meta.spLoadByPrimaryKey = "proc_NutritionCareDiagnoseTransDTLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private NutritionCareDiagnoseTransDTMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
