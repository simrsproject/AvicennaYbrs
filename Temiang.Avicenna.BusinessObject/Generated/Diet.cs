/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/29/2019 2:30:20 PM
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
    abstract public class esDietCollection : esEntityCollectionWAuditLog
    {
        public esDietCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "DietCollection";
        }

        #region Query Logic
        protected void InitQuery(esDietQuery query)
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
            this.InitQuery(query as esDietQuery);
        }
        #endregion

        virtual public Diet DetachEntity(Diet entity)
        {
            return base.DetachEntity(entity) as Diet;
        }

        virtual public Diet AttachEntity(Diet entity)
        {
            return base.AttachEntity(entity) as Diet;
        }

        virtual public void Combine(DietCollection collection)
        {
            base.Combine(collection);
        }

        new public Diet this[int index]
        {
            get
            {
                return base[index] as Diet;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(Diet);
        }
    }

    [Serializable]
    abstract public class esDiet : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esDietQuery GetDynamicQuery()
        {
            return null;
        }

        public esDiet()
        {
        }

        public esDiet(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String dietID)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dietID);
            else
                return LoadByPrimaryKeyStoredProcedure(dietID);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String dietID)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(dietID);
            else
                return LoadByPrimaryKeyStoredProcedure(dietID);
        }

        private bool LoadByPrimaryKeyDynamic(String dietID)
        {
            esDietQuery query = this.GetDynamicQuery();
            query.Where(query.DietID == dietID);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String dietID)
        {
            esParameters parms = new esParameters();
            parms.Add("DietID", dietID);
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
                        case "DietID": this.str.DietID = (string)value; break;
                        case "DietName": this.str.DietName = (string)value; break;
                        case "SRDietType": this.str.SRDietType = (string)value; break;
                        case "Calorie": this.str.Calorie = (string)value; break;
                        case "CalorieMin": this.str.CalorieMin = (string)value; break;
                        case "CalorieMax": this.str.CalorieMax = (string)value; break;
                        case "CalorieInterval": this.str.CalorieInterval = (string)value; break;
                        case "Protein": this.str.Protein = (string)value; break;
                        case "ProteinMin": this.str.ProteinMin = (string)value; break;
                        case "ProteinMax": this.str.ProteinMax = (string)value; break;
                        case "ProteinInterval": this.str.ProteinInterval = (string)value; break;
                        case "Fat": this.str.Fat = (string)value; break;
                        case "FatMin": this.str.FatMin = (string)value; break;
                        case "FatMax": this.str.FatMax = (string)value; break;
                        case "FatInterval": this.str.FatInterval = (string)value; break;
                        case "Carbohydrate": this.str.Carbohydrate = (string)value; break;
                        case "CarbohydrateMin": this.str.CarbohydrateMin = (string)value; break;
                        case "CarbohydrateMax": this.str.CarbohydrateMax = (string)value; break;
                        case "CarbohydrateInterval": this.str.CarbohydrateInterval = (string)value; break;
                        case "Salt": this.str.Salt = (string)value; break;
                        case "SaltMin": this.str.SaltMin = (string)value; break;
                        case "SaltMax": this.str.SaltMax = (string)value; break;
                        case "SaltInterval": this.str.SaltInterval = (string)value; break;
                        case "Fiber": this.str.Fiber = (string)value; break;
                        case "FiberMin": this.str.FiberMin = (string)value; break;
                        case "FiberMax": this.str.FiberMax = (string)value; break;
                        case "FiberInterval": this.str.FiberInterval = (string)value; break;
                        case "PriorityNo": this.str.PriorityNo = (string)value; break;
                        case "IsGetSnack": this.str.IsGetSnack = (string)value; break;
                        case "IsActive": this.str.IsActive = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "Calorie":

                            if (value == null || value is System.Decimal)
                                this.Calorie = (System.Decimal?)value;
                            break;
                        case "CalorieMin":

                            if (value == null || value is System.Decimal)
                                this.CalorieMin = (System.Decimal?)value;
                            break;
                        case "CalorieMax":

                            if (value == null || value is System.Decimal)
                                this.CalorieMax = (System.Decimal?)value;
                            break;
                        case "CalorieInterval":

                            if (value == null || value is System.Decimal)
                                this.CalorieInterval = (System.Decimal?)value;
                            break;
                        case "Protein":

                            if (value == null || value is System.Decimal)
                                this.Protein = (System.Decimal?)value;
                            break;
                        case "ProteinMin":

                            if (value == null || value is System.Decimal)
                                this.ProteinMin = (System.Decimal?)value;
                            break;
                        case "ProteinMax":

                            if (value == null || value is System.Decimal)
                                this.ProteinMax = (System.Decimal?)value;
                            break;
                        case "ProteinInterval":

                            if (value == null || value is System.Decimal)
                                this.ProteinInterval = (System.Decimal?)value;
                            break;
                        case "Fat":

                            if (value == null || value is System.Decimal)
                                this.Fat = (System.Decimal?)value;
                            break;
                        case "FatMin":

                            if (value == null || value is System.Decimal)
                                this.FatMin = (System.Decimal?)value;
                            break;
                        case "FatMax":

                            if (value == null || value is System.Decimal)
                                this.FatMax = (System.Decimal?)value;
                            break;
                        case "FatInterval":

                            if (value == null || value is System.Decimal)
                                this.FatInterval = (System.Decimal?)value;
                            break;
                        case "Carbohydrate":

                            if (value == null || value is System.Decimal)
                                this.Carbohydrate = (System.Decimal?)value;
                            break;
                        case "CarbohydrateMin":

                            if (value == null || value is System.Decimal)
                                this.CarbohydrateMin = (System.Decimal?)value;
                            break;
                        case "CarbohydrateMax":

                            if (value == null || value is System.Decimal)
                                this.CarbohydrateMax = (System.Decimal?)value;
                            break;
                        case "CarbohydrateInterval":

                            if (value == null || value is System.Decimal)
                                this.CarbohydrateInterval = (System.Decimal?)value;
                            break;
                        case "Salt":

                            if (value == null || value is System.Decimal)
                                this.Salt = (System.Decimal?)value;
                            break;
                        case "SaltMin":

                            if (value == null || value is System.Decimal)
                                this.SaltMin = (System.Decimal?)value;
                            break;
                        case "SaltMax":

                            if (value == null || value is System.Decimal)
                                this.SaltMax = (System.Decimal?)value;
                            break;
                        case "SaltInterval":

                            if (value == null || value is System.Decimal)
                                this.SaltInterval = (System.Decimal?)value;
                            break;
                        case "Fiber":

                            if (value == null || value is System.Decimal)
                                this.Fiber = (System.Decimal?)value;
                            break;
                        case "FiberMin":

                            if (value == null || value is System.Decimal)
                                this.FiberMin = (System.Decimal?)value;
                            break;
                        case "FiberMax":

                            if (value == null || value is System.Decimal)
                                this.FiberMax = (System.Decimal?)value;
                            break;
                        case "FiberInterval":

                            if (value == null || value is System.Decimal)
                                this.FiberInterval = (System.Decimal?)value;
                            break;
                        case "PriorityNo":

                            if (value == null || value is System.Int16)
                                this.PriorityNo = (System.Int16?)value;
                            break;
                        case "IsGetSnack":

                            if (value == null || value is System.Boolean)
                                this.IsGetSnack = (System.Boolean?)value;
                            break;
                        case "IsActive":

                            if (value == null || value is System.Boolean)
                                this.IsActive = (System.Boolean?)value;
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
        /// Maps to Diet.DietID
        /// </summary>
        virtual public System.String DietID
        {
            get
            {
                return base.GetSystemString(DietMetadata.ColumnNames.DietID);
            }

            set
            {
                base.SetSystemString(DietMetadata.ColumnNames.DietID, value);
            }
        }
        /// <summary>
        /// Maps to Diet.DietName
        /// </summary>
        virtual public System.String DietName
        {
            get
            {
                return base.GetSystemString(DietMetadata.ColumnNames.DietName);
            }

            set
            {
                base.SetSystemString(DietMetadata.ColumnNames.DietName, value);
            }
        }
        /// <summary>
        /// Maps to Diet.SRDietType
        /// </summary>
        virtual public System.String SRDietType
        {
            get
            {
                return base.GetSystemString(DietMetadata.ColumnNames.SRDietType);
            }

            set
            {
                base.SetSystemString(DietMetadata.ColumnNames.SRDietType, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Calorie
        /// </summary>
        virtual public System.Decimal? Calorie
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Calorie);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Calorie, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CalorieMin
        /// </summary>
        virtual public System.Decimal? CalorieMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CalorieMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CalorieMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CalorieMax
        /// </summary>
        virtual public System.Decimal? CalorieMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CalorieMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CalorieMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CalorieInterval
        /// </summary>
        virtual public System.Decimal? CalorieInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CalorieInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CalorieInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Protein
        /// </summary>
        virtual public System.Decimal? Protein
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Protein);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Protein, value);
            }
        }
        /// <summary>
        /// Maps to Diet.ProteinMin
        /// </summary>
        virtual public System.Decimal? ProteinMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.ProteinMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.ProteinMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.ProteinMax
        /// </summary>
        virtual public System.Decimal? ProteinMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.ProteinMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.ProteinMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.ProteinInterval
        /// </summary>
        virtual public System.Decimal? ProteinInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.ProteinInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.ProteinInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Fat
        /// </summary>
        virtual public System.Decimal? Fat
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Fat);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Fat, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FatMin
        /// </summary>
        virtual public System.Decimal? FatMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FatMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FatMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FatMax
        /// </summary>
        virtual public System.Decimal? FatMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FatMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FatMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FatInterval
        /// </summary>
        virtual public System.Decimal? FatInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FatInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FatInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Carbohydrate
        /// </summary>
        virtual public System.Decimal? Carbohydrate
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Carbohydrate);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Carbohydrate, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CarbohydrateMin
        /// </summary>
        virtual public System.Decimal? CarbohydrateMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CarbohydrateMax
        /// </summary>
        virtual public System.Decimal? CarbohydrateMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.CarbohydrateInterval
        /// </summary>
        virtual public System.Decimal? CarbohydrateInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.CarbohydrateInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Salt
        /// </summary>
        virtual public System.Decimal? Salt
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Salt);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Salt, value);
            }
        }
        /// <summary>
        /// Maps to Diet.SaltMin
        /// </summary>
        virtual public System.Decimal? SaltMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.SaltMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.SaltMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.SaltMax
        /// </summary>
        virtual public System.Decimal? SaltMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.SaltMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.SaltMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.SaltInterval
        /// </summary>
        virtual public System.Decimal? SaltInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.SaltInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.SaltInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.Fiber
        /// </summary>
        virtual public System.Decimal? Fiber
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.Fiber);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.Fiber, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FiberMin
        /// </summary>
        virtual public System.Decimal? FiberMin
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FiberMin);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FiberMin, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FiberMax
        /// </summary>
        virtual public System.Decimal? FiberMax
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FiberMax);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FiberMax, value);
            }
        }
        /// <summary>
        /// Maps to Diet.FiberInterval
        /// </summary>
        virtual public System.Decimal? FiberInterval
        {
            get
            {
                return base.GetSystemDecimal(DietMetadata.ColumnNames.FiberInterval);
            }

            set
            {
                base.SetSystemDecimal(DietMetadata.ColumnNames.FiberInterval, value);
            }
        }
        /// <summary>
        /// Maps to Diet.PriorityNo
        /// </summary>
        virtual public System.Int16? PriorityNo
        {
            get
            {
                return base.GetSystemInt16(DietMetadata.ColumnNames.PriorityNo);
            }

            set
            {
                base.SetSystemInt16(DietMetadata.ColumnNames.PriorityNo, value);
            }
        }
        /// <summary>
        /// Maps to Diet.IsGetSnack
        /// </summary>
        virtual public System.Boolean? IsGetSnack
        {
            get
            {
                return base.GetSystemBoolean(DietMetadata.ColumnNames.IsGetSnack);
            }

            set
            {
                base.SetSystemBoolean(DietMetadata.ColumnNames.IsGetSnack, value);
            }
        }
        /// <summary>
        /// Maps to Diet.IsActive
        /// </summary>
        virtual public System.Boolean? IsActive
        {
            get
            {
                return base.GetSystemBoolean(DietMetadata.ColumnNames.IsActive);
            }

            set
            {
                base.SetSystemBoolean(DietMetadata.ColumnNames.IsActive, value);
            }
        }
        /// <summary>
        /// Maps to Diet.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(DietMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(DietMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to Diet.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(DietMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(DietMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esDiet entity)
            {
                this.entity = entity;
            }
            public System.String DietID
            {
                get
                {
                    System.String data = entity.DietID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietID = null;
                    else entity.DietID = Convert.ToString(value);
                }
            }
            public System.String DietName
            {
                get
                {
                    System.String data = entity.DietName;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DietName = null;
                    else entity.DietName = Convert.ToString(value);
                }
            }
            public System.String SRDietType
            {
                get
                {
                    System.String data = entity.SRDietType;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SRDietType = null;
                    else entity.SRDietType = Convert.ToString(value);
                }
            }
            public System.String Calorie
            {
                get
                {
                    System.Decimal? data = entity.Calorie;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Calorie = null;
                    else entity.Calorie = Convert.ToDecimal(value);
                }
            }
            public System.String CalorieMin
            {
                get
                {
                    System.Decimal? data = entity.CalorieMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CalorieMin = null;
                    else entity.CalorieMin = Convert.ToDecimal(value);
                }
            }
            public System.String CalorieMax
            {
                get
                {
                    System.Decimal? data = entity.CalorieMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CalorieMax = null;
                    else entity.CalorieMax = Convert.ToDecimal(value);
                }
            }
            public System.String CalorieInterval
            {
                get
                {
                    System.Decimal? data = entity.CalorieInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CalorieInterval = null;
                    else entity.CalorieInterval = Convert.ToDecimal(value);
                }
            }
            public System.String Protein
            {
                get
                {
                    System.Decimal? data = entity.Protein;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Protein = null;
                    else entity.Protein = Convert.ToDecimal(value);
                }
            }
            public System.String ProteinMin
            {
                get
                {
                    System.Decimal? data = entity.ProteinMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProteinMin = null;
                    else entity.ProteinMin = Convert.ToDecimal(value);
                }
            }
            public System.String ProteinMax
            {
                get
                {
                    System.Decimal? data = entity.ProteinMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProteinMax = null;
                    else entity.ProteinMax = Convert.ToDecimal(value);
                }
            }
            public System.String ProteinInterval
            {
                get
                {
                    System.Decimal? data = entity.ProteinInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.ProteinInterval = null;
                    else entity.ProteinInterval = Convert.ToDecimal(value);
                }
            }
            public System.String Fat
            {
                get
                {
                    System.Decimal? data = entity.Fat;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Fat = null;
                    else entity.Fat = Convert.ToDecimal(value);
                }
            }
            public System.String FatMin
            {
                get
                {
                    System.Decimal? data = entity.FatMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatMin = null;
                    else entity.FatMin = Convert.ToDecimal(value);
                }
            }
            public System.String FatMax
            {
                get
                {
                    System.Decimal? data = entity.FatMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatMax = null;
                    else entity.FatMax = Convert.ToDecimal(value);
                }
            }
            public System.String FatInterval
            {
                get
                {
                    System.Decimal? data = entity.FatInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FatInterval = null;
                    else entity.FatInterval = Convert.ToDecimal(value);
                }
            }
            public System.String Carbohydrate
            {
                get
                {
                    System.Decimal? data = entity.Carbohydrate;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Carbohydrate = null;
                    else entity.Carbohydrate = Convert.ToDecimal(value);
                }
            }
            public System.String CarbohydrateMin
            {
                get
                {
                    System.Decimal? data = entity.CarbohydrateMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CarbohydrateMin = null;
                    else entity.CarbohydrateMin = Convert.ToDecimal(value);
                }
            }
            public System.String CarbohydrateMax
            {
                get
                {
                    System.Decimal? data = entity.CarbohydrateMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CarbohydrateMax = null;
                    else entity.CarbohydrateMax = Convert.ToDecimal(value);
                }
            }
            public System.String CarbohydrateInterval
            {
                get
                {
                    System.Decimal? data = entity.CarbohydrateInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.CarbohydrateInterval = null;
                    else entity.CarbohydrateInterval = Convert.ToDecimal(value);
                }
            }
            public System.String Salt
            {
                get
                {
                    System.Decimal? data = entity.Salt;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Salt = null;
                    else entity.Salt = Convert.ToDecimal(value);
                }
            }
            public System.String SaltMin
            {
                get
                {
                    System.Decimal? data = entity.SaltMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SaltMin = null;
                    else entity.SaltMin = Convert.ToDecimal(value);
                }
            }
            public System.String SaltMax
            {
                get
                {
                    System.Decimal? data = entity.SaltMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SaltMax = null;
                    else entity.SaltMax = Convert.ToDecimal(value);
                }
            }
            public System.String SaltInterval
            {
                get
                {
                    System.Decimal? data = entity.SaltInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.SaltInterval = null;
                    else entity.SaltInterval = Convert.ToDecimal(value);
                }
            }
            public System.String Fiber
            {
                get
                {
                    System.Decimal? data = entity.Fiber;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.Fiber = null;
                    else entity.Fiber = Convert.ToDecimal(value);
                }
            }
            public System.String FiberMin
            {
                get
                {
                    System.Decimal? data = entity.FiberMin;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FiberMin = null;
                    else entity.FiberMin = Convert.ToDecimal(value);
                }
            }
            public System.String FiberMax
            {
                get
                {
                    System.Decimal? data = entity.FiberMax;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FiberMax = null;
                    else entity.FiberMax = Convert.ToDecimal(value);
                }
            }
            public System.String FiberInterval
            {
                get
                {
                    System.Decimal? data = entity.FiberInterval;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.FiberInterval = null;
                    else entity.FiberInterval = Convert.ToDecimal(value);
                }
            }
            public System.String PriorityNo
            {
                get
                {
                    System.Int16? data = entity.PriorityNo;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PriorityNo = null;
                    else entity.PriorityNo = Convert.ToInt16(value);
                }
            }
            public System.String IsGetSnack
            {
                get
                {
                    System.Boolean? data = entity.IsGetSnack;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsGetSnack = null;
                    else entity.IsGetSnack = Convert.ToBoolean(value);
                }
            }
            public System.String IsActive
            {
                get
                {
                    System.Boolean? data = entity.IsActive;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.IsActive = null;
                    else entity.IsActive = Convert.ToBoolean(value);
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
            private esDiet entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esDietQuery query)
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
                throw new Exception("esDiet can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class Diet : esDiet
    {
    }

    [Serializable]
    abstract public class esDietQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return DietMetadata.Meta();
            }
        }

        public esQueryItem DietID
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.DietID, esSystemType.String);
            }
        }

        public esQueryItem DietName
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.DietName, esSystemType.String);
            }
        }

        public esQueryItem SRDietType
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.SRDietType, esSystemType.String);
            }
        }

        public esQueryItem Calorie
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Calorie, esSystemType.Decimal);
            }
        }

        public esQueryItem CalorieMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CalorieMin, esSystemType.Decimal);
            }
        }

        public esQueryItem CalorieMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CalorieMax, esSystemType.Decimal);
            }
        }

        public esQueryItem CalorieInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CalorieInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem Protein
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Protein, esSystemType.Decimal);
            }
        }

        public esQueryItem ProteinMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.ProteinMin, esSystemType.Decimal);
            }
        }

        public esQueryItem ProteinMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.ProteinMax, esSystemType.Decimal);
            }
        }

        public esQueryItem ProteinInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.ProteinInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem Fat
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Fat, esSystemType.Decimal);
            }
        }

        public esQueryItem FatMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FatMin, esSystemType.Decimal);
            }
        }

        public esQueryItem FatMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FatMax, esSystemType.Decimal);
            }
        }

        public esQueryItem FatInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FatInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem Carbohydrate
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Carbohydrate, esSystemType.Decimal);
            }
        }

        public esQueryItem CarbohydrateMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CarbohydrateMin, esSystemType.Decimal);
            }
        }

        public esQueryItem CarbohydrateMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CarbohydrateMax, esSystemType.Decimal);
            }
        }

        public esQueryItem CarbohydrateInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.CarbohydrateInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem Salt
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Salt, esSystemType.Decimal);
            }
        }

        public esQueryItem SaltMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.SaltMin, esSystemType.Decimal);
            }
        }

        public esQueryItem SaltMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.SaltMax, esSystemType.Decimal);
            }
        }

        public esQueryItem SaltInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.SaltInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem Fiber
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.Fiber, esSystemType.Decimal);
            }
        }

        public esQueryItem FiberMin
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FiberMin, esSystemType.Decimal);
            }
        }

        public esQueryItem FiberMax
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FiberMax, esSystemType.Decimal);
            }
        }

        public esQueryItem FiberInterval
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.FiberInterval, esSystemType.Decimal);
            }
        }

        public esQueryItem PriorityNo
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.PriorityNo, esSystemType.Int16);
            }
        }

        public esQueryItem IsGetSnack
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.IsGetSnack, esSystemType.Boolean);
            }
        }

        public esQueryItem IsActive
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.IsActive, esSystemType.Boolean);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, DietMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("DietCollection")]
    public partial class DietCollection : esDietCollection, IEnumerable<Diet>
    {
        public DietCollection()
        {

        }

        public static implicit operator List<Diet>(DietCollection coll)
        {
            List<Diet> list = new List<Diet>();

            foreach (Diet emp in coll)
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
                return DietMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new Diet(row);
        }

        override protected esEntity CreateEntity()
        {
            return new Diet();
        }

        #endregion

        [BrowsableAttribute(false)]
        public DietQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietQuery();
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
        public bool Load(DietQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public Diet AddNew()
        {
            Diet entity = base.AddNewEntity() as Diet;

            return entity;
        }
        public Diet FindByPrimaryKey(String dietID)
        {
            return base.FindByPrimaryKey(dietID) as Diet;
        }

        #region IEnumerable< Diet> Members

        IEnumerator<Diet> IEnumerable<Diet>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as Diet;
            }
        }

        #endregion

        private DietQuery query;
    }


    /// <summary>
    /// Encapsulates the 'Diet' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Diet ({DietID})")]
    [Serializable]
    public partial class Diet : esDiet
    {
        public Diet()
        {
        }

        public Diet(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return DietMetadata.Meta();
            }
        }

        override protected esDietQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new DietQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public DietQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new DietQuery();
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
        public bool Load(DietQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private DietQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class DietQuery : esDietQuery
    {
        public DietQuery()
        {

        }

        public DietQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "DietQuery";
        }
    }

    [Serializable]
    public partial class DietMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected DietMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(DietMetadata.ColumnNames.DietID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMetadata.PropertyNames.DietID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.DietName, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMetadata.PropertyNames.DietName;
            c.CharacterMaxLength = 200;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.SRDietType, 2, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMetadata.PropertyNames.SRDietType;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Calorie, 3, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Calorie;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CalorieMin, 4, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CalorieMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CalorieMax, 5, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CalorieMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CalorieInterval, 6, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CalorieInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Protein, 7, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Protein;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.ProteinMin, 8, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.ProteinMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.ProteinMax, 9, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.ProteinMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.ProteinInterval, 10, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.ProteinInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Fat, 11, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Fat;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FatMin, 12, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FatMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FatMax, 13, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FatMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FatInterval, 14, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FatInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Carbohydrate, 15, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Carbohydrate;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CarbohydrateMin, 16, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CarbohydrateMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CarbohydrateMax, 17, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CarbohydrateMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.CarbohydrateInterval, 18, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.CarbohydrateInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Salt, 19, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Salt;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.SaltMin, 20, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.SaltMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.SaltMax, 21, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.SaltMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.SaltInterval, 22, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.SaltInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.Fiber, 23, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.Fiber;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FiberMin, 24, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FiberMin;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FiberMax, 25, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FiberMax;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.FiberInterval, 26, typeof(System.Decimal), esSystemType.Decimal);
            c.PropertyName = DietMetadata.PropertyNames.FiberInterval;
            c.NumericPrecision = 10;
            c.NumericScale = 2;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.PriorityNo, 27, typeof(System.Int16), esSystemType.Int16);
            c.PropertyName = DietMetadata.PropertyNames.PriorityNo;
            c.NumericPrecision = 5;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.IsGetSnack, 28, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DietMetadata.PropertyNames.IsGetSnack;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.IsActive, 29, typeof(System.Boolean), esSystemType.Boolean);
            c.PropertyName = DietMetadata.PropertyNames.IsActive;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.LastUpdateDateTime, 30, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = DietMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(DietMetadata.ColumnNames.LastUpdateByUserID, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = DietMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 15;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public DietMetadata Meta()
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
            public const string DietID = "DietID";
            public const string DietName = "DietName";
            public const string SRDietType = "SRDietType";
            public const string Calorie = "Calorie";
            public const string CalorieMin = "CalorieMin";
            public const string CalorieMax = "CalorieMax";
            public const string CalorieInterval = "CalorieInterval";
            public const string Protein = "Protein";
            public const string ProteinMin = "ProteinMin";
            public const string ProteinMax = "ProteinMax";
            public const string ProteinInterval = "ProteinInterval";
            public const string Fat = "Fat";
            public const string FatMin = "FatMin";
            public const string FatMax = "FatMax";
            public const string FatInterval = "FatInterval";
            public const string Carbohydrate = "Carbohydrate";
            public const string CarbohydrateMin = "CarbohydrateMin";
            public const string CarbohydrateMax = "CarbohydrateMax";
            public const string CarbohydrateInterval = "CarbohydrateInterval";
            public const string Salt = "Salt";
            public const string SaltMin = "SaltMin";
            public const string SaltMax = "SaltMax";
            public const string SaltInterval = "SaltInterval";
            public const string Fiber = "Fiber";
            public const string FiberMin = "FiberMin";
            public const string FiberMax = "FiberMax";
            public const string FiberInterval = "FiberInterval";
            public const string PriorityNo = "PriorityNo";
            public const string IsGetSnack = "IsGetSnack";
            public const string IsActive = "IsActive";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string DietID = "DietID";
            public const string DietName = "DietName";
            public const string SRDietType = "SRDietType";
            public const string Calorie = "Calorie";
            public const string CalorieMin = "CalorieMin";
            public const string CalorieMax = "CalorieMax";
            public const string CalorieInterval = "CalorieInterval";
            public const string Protein = "Protein";
            public const string ProteinMin = "ProteinMin";
            public const string ProteinMax = "ProteinMax";
            public const string ProteinInterval = "ProteinInterval";
            public const string Fat = "Fat";
            public const string FatMin = "FatMin";
            public const string FatMax = "FatMax";
            public const string FatInterval = "FatInterval";
            public const string Carbohydrate = "Carbohydrate";
            public const string CarbohydrateMin = "CarbohydrateMin";
            public const string CarbohydrateMax = "CarbohydrateMax";
            public const string CarbohydrateInterval = "CarbohydrateInterval";
            public const string Salt = "Salt";
            public const string SaltMin = "SaltMin";
            public const string SaltMax = "SaltMax";
            public const string SaltInterval = "SaltInterval";
            public const string Fiber = "Fiber";
            public const string FiberMin = "FiberMin";
            public const string FiberMax = "FiberMax";
            public const string FiberInterval = "FiberInterval";
            public const string PriorityNo = "PriorityNo";
            public const string IsGetSnack = "IsGetSnack";
            public const string IsActive = "IsActive";
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
            lock (typeof(DietMetadata))
            {
                if (DietMetadata.mapDelegates == null)
                {
                    DietMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (DietMetadata.meta == null)
                {
                    DietMetadata.meta = new DietMetadata();
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

                meta.AddTypeMap("DietID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DietName", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("SRDietType", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("Calorie", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CalorieMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CalorieMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CalorieInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Protein", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ProteinMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ProteinMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("ProteinInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Fat", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FatMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FatMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FatInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Carbohydrate", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CarbohydrateMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CarbohydrateMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("CarbohydrateInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Salt", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SaltMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SaltMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("SaltInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("Fiber", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FiberMin", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FiberMax", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("FiberInterval", new esTypeMap("numeric", "System.Decimal"));
                meta.AddTypeMap("PriorityNo", new esTypeMap("smallint", "System.Int16"));
                meta.AddTypeMap("IsGetSnack", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "Diet";
                meta.Destination = "Diet";
                meta.spInsert = "proc_DietInsert";
                meta.spUpdate = "proc_DietUpdate";
                meta.spDelete = "proc_DietDelete";
                meta.spLoadAll = "proc_DietLoadAll";
                meta.spLoadByPrimaryKey = "proc_DietLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private DietMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
