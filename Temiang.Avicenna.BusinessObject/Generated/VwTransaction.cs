/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 9/24/2014 10:50:32 AM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

    [Serializable]
    abstract public class esVwTransactionCollection : esEntityCollectionWAuditLog
    {
        public esVwTransactionCollection()
        {

        }

        protected override string GetCollectionName()
        {
            return "VwTransactionCollection";
        }

        #region Query Logic
        protected void InitQuery(esVwTransactionQuery query)
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
            this.InitQuery(query as esVwTransactionQuery);
        }
        #endregion

        virtual public VwTransaction DetachEntity(VwTransaction entity)
        {
            return base.DetachEntity(entity) as VwTransaction;
        }

        virtual public VwTransaction AttachEntity(VwTransaction entity)
        {
            return base.AttachEntity(entity) as VwTransaction;
        }

        virtual public void Combine(VwTransactionCollection collection)
        {
            base.Combine(collection);
        }

        new public VwTransaction this[int index]
        {
            get
            {
                return base[index] as VwTransaction;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(VwTransaction);
        }
    }



    [Serializable]
    abstract public class esVwTransaction : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esVwTransactionQuery GetDynamicQuery()
        {
            return null;
        }

        public esVwTransaction()
        {

        }

        public esVwTransaction(DataRow row)
            : base(row)
        {

        }

        #region LoadByPrimaryKey

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
                        case "TransactionNo": this.str.TransactionNo = (string)value; break;
                        case "TransactionDate": this.str.TransactionDate = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "ReferenceNo": this.str.ReferenceNo = (string)value; break;
                        case "IsCorrection": this.str.IsCorrection = (string)value; break;
                        case "FilterDate": this.str.FilterDate = (string)value; break;
                        case "OrderTransNo": this.str.OrderTransNo = (string)value; break;
                        case "OrderDate": this.str.OrderDate = (string)value; break;
                        case "IsPackage": this.str.IsPackage = (string)value; break;
                        case "PackageReferenceNo": this.str.PackageReferenceNo = (string)value; break;
                        case "IsPrescription": this.str.IsPrescription = (string)value; break;
                        case "ClassID": this.str.ClassID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "TransactionDate":

                            if (value == null || value is System.DateTime)
                                this.TransactionDate = (System.DateTime?)value;
                            break;

                        case "IsCorrection":

                            if (value == null || value is System.Boolean)
                                this.IsCorrection = (System.Boolean?)value;
                            break;

                        case "FilterDate":

                            if (value == null || value is System.DateTime)
                                this.FilterDate = (System.DateTime?)value;
                            break;

                        case "OrderDate":

                            if (value == null || value is System.DateTime)
                                this.OrderDate = (System.DateTime?)value;
                            break;

                        case "IsPackage":

                            if (value == null || value is System.Int32)
                                this.IsPackage = (System.Int32?)value;
                            break;

                        case "IsPrescription":

                            if (value == null || value is System.Boolean)
                                this.IsPrescription = (System.Boolean?)value;
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
        /// Maps to vw_Transaction.TransactionNo
        /// </summary>
        virtual public System.String TransactionNo
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.TransactionNo);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.TransactionNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.TransactionDate
        /// </summary>
        virtual public System.DateTime? TransactionDate
        {
            get
            {
                return base.GetSystemDateTime(VwTransactionMetadata.ColumnNames.TransactionDate);
            }

            set
            {
                base.SetSystemDateTime(VwTransactionMetadata.ColumnNames.TransactionDate, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.RegistrationNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.ServiceUnitID, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.ReferenceNo
        /// </summary>
        virtual public System.String ReferenceNo
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.ReferenceNo);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.ReferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.IsCorrection
        /// </summary>
        virtual public System.Boolean? IsCorrection
        {
            get
            {
                return base.GetSystemBoolean(VwTransactionMetadata.ColumnNames.IsCorrection);
            }

            set
            {
                base.SetSystemBoolean(VwTransactionMetadata.ColumnNames.IsCorrection, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.FilterDate
        /// </summary>
        virtual public System.DateTime? FilterDate
        {
            get
            {
                return base.GetSystemDateTime(VwTransactionMetadata.ColumnNames.FilterDate);
            }

            set
            {
                base.SetSystemDateTime(VwTransactionMetadata.ColumnNames.FilterDate, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.OrderTransNo
        /// </summary>
        virtual public System.String OrderTransNo
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.OrderTransNo);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.OrderTransNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.OrderDate
        /// </summary>
        virtual public System.DateTime? OrderDate
        {
            get
            {
                return base.GetSystemDateTime(VwTransactionMetadata.ColumnNames.OrderDate);
            }

            set
            {
                base.SetSystemDateTime(VwTransactionMetadata.ColumnNames.OrderDate, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.IsPackage
        /// </summary>
        virtual public System.Int32? IsPackage
        {
            get
            {
                return base.GetSystemInt32(VwTransactionMetadata.ColumnNames.IsPackage);
            }

            set
            {
                base.SetSystemInt32(VwTransactionMetadata.ColumnNames.IsPackage, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.PackageReferenceNo
        /// </summary>
        virtual public System.String PackageReferenceNo
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.PackageReferenceNo);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.PackageReferenceNo, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.IsPrescription
        /// </summary>
        virtual public System.Boolean? IsPrescription
        {
            get
            {
                return base.GetSystemBoolean(VwTransactionMetadata.ColumnNames.IsPrescription);
            }

            set
            {
                base.SetSystemBoolean(VwTransactionMetadata.ColumnNames.IsPrescription, value);
            }
        }

        /// <summary>
        /// Maps to vw_Transaction.ClassID
        /// </summary>
        virtual public System.String ClassID
        {
            get
            {
                return base.GetSystemString(VwTransactionMetadata.ColumnNames.ClassID);
            }

            set
            {
                base.SetSystemString(VwTransactionMetadata.ColumnNames.ClassID, value);
            }
        }

        #endregion

        #region String Properties


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
            public esStrings(esVwTransaction entity)
            {
                this.entity = entity;
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

            public System.String TransactionDate
            {
                get
                {
                    System.DateTime? data = entity.TransactionDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.TransactionDate = null;
                    else entity.TransactionDate = Convert.ToDateTime(value);
                }
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

            public System.String ServiceUnitID
            {
                get
                {
                    System.String data = entity.ServiceUnitID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ServiceUnitID = null;
                    else entity.ServiceUnitID = Convert.ToString(value);
                }
            }

            public System.String ReferenceNo
            {
                get
                {
                    System.String data = entity.ReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ReferenceNo = null;
                    else entity.ReferenceNo = Convert.ToString(value);
                }
            }

            public System.String IsCorrection
            {
                get
                {
                    System.Boolean? data = entity.IsCorrection;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsCorrection = null;
                    else entity.IsCorrection = Convert.ToBoolean(value);
                }
            }

            public System.String FilterDate
            {
                get
                {
                    System.DateTime? data = entity.FilterDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FilterDate = null;
                    else entity.FilterDate = Convert.ToDateTime(value);
                }
            }

            public System.String OrderTransNo
            {
                get
                {
                    System.String data = entity.OrderTransNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderTransNo = null;
                    else entity.OrderTransNo = Convert.ToString(value);
                }
            }

            public System.String OrderDate
            {
                get
                {
                    System.DateTime? data = entity.OrderDate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OrderDate = null;
                    else entity.OrderDate = Convert.ToDateTime(value);
                }
            }

            public System.String IsPackage
            {
                get
                {
                    System.Int32? data = entity.IsPackage;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPackage = null;
                    else entity.IsPackage = Convert.ToInt32(value);
                }
            }

            public System.String PackageReferenceNo
            {
                get
                {
                    System.String data = entity.PackageReferenceNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PackageReferenceNo = null;
                    else entity.PackageReferenceNo = Convert.ToString(value);
                }
            }

            public System.String IsPrescription
            {
                get
                {
                    System.Boolean? data = entity.IsPrescription;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsPrescription = null;
                    else entity.IsPrescription = Convert.ToBoolean(value);
                }
            }

            public System.String ClassID
            {
                get
                {
                    System.String data = entity.ClassID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ClassID = null;
                    else entity.ClassID = Convert.ToString(value);
                }
            }


            private esVwTransaction entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esVwTransactionQuery query)
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
                throw new Exception("esVwTransaction can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }



    [Serializable]
    abstract public class esVwTransactionQuery : esDynamicQuery
    {
        override protected IMetadata Meta
        {
            get
            {
                return VwTransactionMetadata.Meta();
            }
        }


        public esQueryItem TransactionNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.TransactionNo, esSystemType.String);
            }
        }

        public esQueryItem TransactionDate
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem ReferenceNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.ReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem IsCorrection
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.IsCorrection, esSystemType.Boolean);
            }
        }

        public esQueryItem FilterDate
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.FilterDate, esSystemType.DateTime);
            }
        }

        public esQueryItem OrderTransNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.OrderTransNo, esSystemType.String);
            }
        }

        public esQueryItem OrderDate
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.OrderDate, esSystemType.DateTime);
            }
        }

        public esQueryItem IsPackage
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.IsPackage, esSystemType.Int32);
            }
        }

        public esQueryItem PackageReferenceNo
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.PackageReferenceNo, esSystemType.String);
            }
        }

        public esQueryItem IsPrescription
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.IsPrescription, esSystemType.Boolean);
            }
        }

        public esQueryItem ClassID
        {
            get
            {
                return new esQueryItem(this, VwTransactionMetadata.ColumnNames.ClassID, esSystemType.String);
            }
        }

    }



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("VwTransactionCollection")]
    public partial class VwTransactionCollection : esVwTransactionCollection, IEnumerable<VwTransaction>
    {
        public VwTransactionCollection()
        {

        }

        public static implicit operator List<VwTransaction>(VwTransactionCollection coll)
        {
            List<VwTransaction> list = new List<VwTransaction>();

            foreach (VwTransaction emp in coll)
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
                return VwTransactionMetadata.Meta();
            }
        }



        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new VwTransaction(row);
        }

        override protected esEntity CreateEntity()
        {
            return new VwTransaction();
        }


        override public bool LoadAll()
        {
            return base.LoadAll(esSqlAccessType.DynamicSQL);
        }

        #endregion


        [BrowsableAttribute(false)]
        public VwTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransactionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }

        public bool Load(VwTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        public VwTransaction AddNew()
        {
            VwTransaction entity = base.AddNewEntity() as VwTransaction;

            return entity;
        }


        #region IEnumerable<VwTransaction> Members

        IEnumerator<VwTransaction> IEnumerable<VwTransaction>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as VwTransaction;
            }
        }

        #endregion

        private VwTransactionQuery query;
    }


    /// <summary>
    /// Encapsulates the 'vw_Transaction' view
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("VwTransaction ()")]
    [Serializable]
    public partial class VwTransaction : esVwTransaction
    {
        public VwTransaction()
        {

        }

        public VwTransaction(DataRow row)
            : base(row)
        {

        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return VwTransactionMetadata.Meta();
            }
        }



        override protected esVwTransactionQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new VwTransactionQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion




        [BrowsableAttribute(false)]
        public VwTransactionQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new VwTransactionQuery();
                    base.InitQuery(this.query);
                }

                return this.query;
            }
        }

        public void QueryReset()
        {
            this.query = null;
        }


        public bool Load(VwTransactionQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private VwTransactionQuery query;
    }



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]

    public partial class VwTransactionQuery : esVwTransactionQuery
    {
        public VwTransactionQuery()
        {

        }

        public VwTransactionQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "VwTransactionQuery";
        }


    }


    [Serializable]
    public partial class VwTransactionMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected VwTransactionMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.TransactionNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwTransactionMetadata.PropertyNames.TransactionDate;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.RegistrationNo, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.RegistrationNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.ReferenceNo, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.ReferenceNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.IsCorrection, 5, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = VwTransactionMetadata.PropertyNames.IsCorrection;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.FilterDate, 6, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwTransactionMetadata.PropertyNames.FilterDate;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.OrderTransNo, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.OrderTransNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.OrderDate, 8, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = VwTransactionMetadata.PropertyNames.OrderDate;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.IsPackage, 9, typeof(System.Int32), esSystemType.Int32);
            c.PropertyName = VwTransactionMetadata.PropertyNames.IsPackage;
            c.NumericPrecision = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.PackageReferenceNo, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.PackageReferenceNo;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.IsPrescription, 11, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = VwTransactionMetadata.PropertyNames.IsPrescription;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(VwTransactionMetadata.ColumnNames.ClassID, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = VwTransactionMetadata.PropertyNames.ClassID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

        }
        #endregion

        static public VwTransactionMetadata Meta()
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
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ReferenceNo = "ReferenceNo";
            public const string IsCorrection = "IsCorrection";
            public const string FilterDate = "FilterDate";
            public const string OrderTransNo = "OrderTransNo";
            public const string OrderDate = "OrderDate";
            public const string IsPackage = "IsPackage";
            public const string PackageReferenceNo = "PackageReferenceNo";
            public const string IsPrescription = "IsPrescription";
            public const string ClassID = "ClassID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string TransactionNo = "TransactionNo";
            public const string TransactionDate = "TransactionDate";
            public const string RegistrationNo = "RegistrationNo";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string ReferenceNo = "ReferenceNo";
            public const string IsCorrection = "IsCorrection";
            public const string FilterDate = "FilterDate";
            public const string OrderTransNo = "OrderTransNo";
            public const string OrderDate = "OrderDate";
            public const string IsPackage = "IsPackage";
            public const string PackageReferenceNo = "PackageReferenceNo";
            public const string IsPrescription = "IsPrescription";
            public const string ClassID = "ClassID";
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
            lock (typeof(VwTransactionMetadata))
            {
                if (VwTransactionMetadata.mapDelegates == null)
                {
                    VwTransactionMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (VwTransactionMetadata.meta == null)
                {
                    VwTransactionMetadata.meta = new VwTransactionMetadata();
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


                meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsCorrection", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("FilterDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("OrderTransNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OrderDate", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("IsPackage", new esTypeMap("int", "System.Int32"));
                meta.AddTypeMap("PackageReferenceNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("IsPrescription", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));



                meta.Source = "vw_Transaction";
                meta.Destination = "vw_Transaction";

                meta.spInsert = "proc_vw_TransactionInsert";
                meta.spUpdate = "proc_vw_TransactionUpdate";
                meta.spDelete = "proc_vw_TransactionDelete";
                meta.spLoadAll = "proc_vw_TransactionLoadAll";
                meta.spLoadByPrimaryKey = "proc_vw_TransactionLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private VwTransactionMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }
}
