/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/20/2016 12:49:32 AM
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
    abstract public class esPatientOdontogramCollection : esEntityCollectionWAuditLog
    {
        public esPatientOdontogramCollection()
        {

        }


        protected override string GetCollectionName()
        {
            return "PatientOdontogramCollection";
        }

        #region Query Logic
        protected void InitQuery(esPatientOdontogramQuery query)
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
            this.InitQuery(query as esPatientOdontogramQuery);
        }
        #endregion

        virtual public PatientOdontogram DetachEntity(PatientOdontogram entity)
        {
            return base.DetachEntity(entity) as PatientOdontogram;
        }

        virtual public PatientOdontogram AttachEntity(PatientOdontogram entity)
        {
            return base.AttachEntity(entity) as PatientOdontogram;
        }

        virtual public void Combine(PatientOdontogramCollection collection)
        {
            base.Combine(collection);
        }

        new public PatientOdontogram this[int index]
        {
            get
            {
                return base[index] as PatientOdontogram;
            }
        }

        public override Type GetEntityType()
        {
            return typeof(PatientOdontogram);
        }
    }

    [Serializable]
    abstract public class esPatientOdontogram : esEntityWAuditLog
    {
        /// <summary>
        /// Used internally by the entity's DynamicQuery mechanism.
        /// </summary>
        virtual protected esPatientOdontogramQuery GetDynamicQuery()
        {
            return null;
        }

        public esPatientOdontogram()
        {
        }

        public esPatientOdontogram(DataRow row)
            : base(row)
        {
        }


        #region LoadByPrimaryKey
        public virtual bool LoadByPrimaryKey(String patientID, String registrationNo)
        {
            if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, registrationNo);
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
        public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String patientID, String registrationNo)
        {
            if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(patientID, registrationNo);
            else
                return LoadByPrimaryKeyStoredProcedure(patientID, registrationNo);
        }

        private bool LoadByPrimaryKeyDynamic(String patientID, String registrationNo)
        {
            esPatientOdontogramQuery query = this.GetDynamicQuery();
            query.Where(query.PatientID == patientID, query.RegistrationNo == registrationNo);
            return query.Load();
        }

        private bool LoadByPrimaryKeyStoredProcedure(String patientID, String registrationNo)
        {
            esParameters parms = new esParameters();
            parms.Add("PatientID", patientID);
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
                        case "PatientID": this.str.PatientID = (string)value; break;
                        case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
                        case "OdontogramDateTime": this.str.OdontogramDateTime = (string)value; break;
                        case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
                        case "T11": this.str.T11 = (string)value; break;
                        case "T51": this.str.T51 = (string)value; break;
                        case "T12": this.str.T12 = (string)value; break;
                        case "T52": this.str.T52 = (string)value; break;
                        case "T13": this.str.T13 = (string)value; break;
                        case "T53": this.str.T53 = (string)value; break;
                        case "T14": this.str.T14 = (string)value; break;
                        case "T54": this.str.T54 = (string)value; break;
                        case "T15": this.str.T15 = (string)value; break;
                        case "T55": this.str.T55 = (string)value; break;
                        case "T16": this.str.T16 = (string)value; break;
                        case "T17": this.str.T17 = (string)value; break;
                        case "T18": this.str.T18 = (string)value; break;
                        case "T61": this.str.T61 = (string)value; break;
                        case "T21": this.str.T21 = (string)value; break;
                        case "T62": this.str.T62 = (string)value; break;
                        case "T22": this.str.T22 = (string)value; break;
                        case "T63": this.str.T63 = (string)value; break;
                        case "T23": this.str.T23 = (string)value; break;
                        case "T64": this.str.T64 = (string)value; break;
                        case "T24": this.str.T24 = (string)value; break;
                        case "T65": this.str.T65 = (string)value; break;
                        case "T25": this.str.T25 = (string)value; break;
                        case "T26": this.str.T26 = (string)value; break;
                        case "T27": this.str.T27 = (string)value; break;
                        case "T28": this.str.T28 = (string)value; break;
                        case "T48": this.str.T48 = (string)value; break;
                        case "T47": this.str.T47 = (string)value; break;
                        case "T46": this.str.T46 = (string)value; break;
                        case "T45": this.str.T45 = (string)value; break;
                        case "T85": this.str.T85 = (string)value; break;
                        case "T44": this.str.T44 = (string)value; break;
                        case "T84": this.str.T84 = (string)value; break;
                        case "T43": this.str.T43 = (string)value; break;
                        case "T83": this.str.T83 = (string)value; break;
                        case "T42": this.str.T42 = (string)value; break;
                        case "T82": this.str.T82 = (string)value; break;
                        case "T41": this.str.T41 = (string)value; break;
                        case "T81": this.str.T81 = (string)value; break;
                        case "T38": this.str.T38 = (string)value; break;
                        case "T37": this.str.T37 = (string)value; break;
                        case "T36": this.str.T36 = (string)value; break;
                        case "T75": this.str.T75 = (string)value; break;
                        case "T35": this.str.T35 = (string)value; break;
                        case "T74": this.str.T74 = (string)value; break;
                        case "T34": this.str.T34 = (string)value; break;
                        case "T73": this.str.T73 = (string)value; break;
                        case "T33": this.str.T33 = (string)value; break;
                        case "T72": this.str.T72 = (string)value; break;
                        case "T32": this.str.T32 = (string)value; break;
                        case "T71": this.str.T71 = (string)value; break;
                        case "T31": this.str.T31 = (string)value; break;
                        case "T1151Notes": this.str.T1151Notes = (string)value; break;
                        case "T1252Notes": this.str.T1252Notes = (string)value; break;
                        case "T1353Notes": this.str.T1353Notes = (string)value; break;
                        case "T1454Notes": this.str.T1454Notes = (string)value; break;
                        case "T1555Notes": this.str.T1555Notes = (string)value; break;
                        case "T16Notes": this.str.T16Notes = (string)value; break;
                        case "T17Notes": this.str.T17Notes = (string)value; break;
                        case "T18Notes": this.str.T18Notes = (string)value; break;
                        case "T6121Notes": this.str.T6121Notes = (string)value; break;
                        case "T6222Notes": this.str.T6222Notes = (string)value; break;
                        case "T6323Notes": this.str.T6323Notes = (string)value; break;
                        case "T6424Notes": this.str.T6424Notes = (string)value; break;
                        case "T6525Notes": this.str.T6525Notes = (string)value; break;
                        case "T26Notes": this.str.T26Notes = (string)value; break;
                        case "T27Notes": this.str.T27Notes = (string)value; break;
                        case "T28Notes": this.str.T28Notes = (string)value; break;
                        case "T48Notes": this.str.T48Notes = (string)value; break;
                        case "T47Notes": this.str.T47Notes = (string)value; break;
                        case "T46Notes": this.str.T46Notes = (string)value; break;
                        case "T4585Notes": this.str.T4585Notes = (string)value; break;
                        case "T4484Notes": this.str.T4484Notes = (string)value; break;
                        case "T4383Notes": this.str.T4383Notes = (string)value; break;
                        case "T4282Notes": this.str.T4282Notes = (string)value; break;
                        case "T4181Notes": this.str.T4181Notes = (string)value; break;
                        case "T38Notes": this.str.T38Notes = (string)value; break;
                        case "T37Notes": this.str.T37Notes = (string)value; break;
                        case "T36Notes": this.str.T36Notes = (string)value; break;
                        case "T7535Notes": this.str.T7535Notes = (string)value; break;
                        case "T7434Notes": this.str.T7434Notes = (string)value; break;
                        case "T7333Notes": this.str.T7333Notes = (string)value; break;
                        case "T7232Notes": this.str.T7232Notes = (string)value; break;
                        case "T7131Notes": this.str.T7131Notes = (string)value; break;
                        case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
                        case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
                    }
                }
                else
                {
                    switch (name)
                    {
                        case "OdontogramDateTime":

                            if (value == null || value is System.DateTime)
                                this.OdontogramDateTime = (System.DateTime?)value;
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
        /// Maps to PatientOdontogram.PatientID
        /// </summary>
        virtual public System.String PatientID
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.PatientID);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.PatientID, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.RegistrationNo
        /// </summary>
        virtual public System.String RegistrationNo
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.RegistrationNo);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.RegistrationNo, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.OdontogramDateTime
        /// </summary>
        virtual public System.DateTime? OdontogramDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientOdontogramMetadata.ColumnNames.OdontogramDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientOdontogramMetadata.ColumnNames.OdontogramDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.ServiceUnitID
        /// </summary>
        virtual public System.String ServiceUnitID
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.ServiceUnitID);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.ServiceUnitID, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T11
        /// </summary>
        virtual public System.String T11
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T11);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T11, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T51
        /// </summary>
        virtual public System.String T51
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T51);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T51, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T12
        /// </summary>
        virtual public System.String T12
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T12);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T12, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T52
        /// </summary>
        virtual public System.String T52
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T52);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T52, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T13
        /// </summary>
        virtual public System.String T13
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T13);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T13, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T53
        /// </summary>
        virtual public System.String T53
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T53);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T53, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T14
        /// </summary>
        virtual public System.String T14
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T14);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T14, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T54
        /// </summary>
        virtual public System.String T54
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T54);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T54, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T15
        /// </summary>
        virtual public System.String T15
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T15);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T15, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T55
        /// </summary>
        virtual public System.String T55
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T55);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T55, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T16
        /// </summary>
        virtual public System.String T16
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T16);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T16, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T17
        /// </summary>
        virtual public System.String T17
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T17);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T17, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T18
        /// </summary>
        virtual public System.String T18
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T18);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T18, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T61
        /// </summary>
        virtual public System.String T61
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T61);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T61, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T21
        /// </summary>
        virtual public System.String T21
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T21);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T21, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T62
        /// </summary>
        virtual public System.String T62
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T62);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T62, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T22
        /// </summary>
        virtual public System.String T22
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T22);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T22, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T63
        /// </summary>
        virtual public System.String T63
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T63);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T63, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T23
        /// </summary>
        virtual public System.String T23
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T23);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T23, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T64
        /// </summary>
        virtual public System.String T64
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T64);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T64, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T24
        /// </summary>
        virtual public System.String T24
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T24);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T24, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T65
        /// </summary>
        virtual public System.String T65
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T65);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T65, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T25
        /// </summary>
        virtual public System.String T25
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T25);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T25, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T26
        /// </summary>
        virtual public System.String T26
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T26);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T26, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T27
        /// </summary>
        virtual public System.String T27
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T27);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T27, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T28
        /// </summary>
        virtual public System.String T28
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T28);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T28, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T48
        /// </summary>
        virtual public System.String T48
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T48);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T48, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T47
        /// </summary>
        virtual public System.String T47
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T47);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T47, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T46
        /// </summary>
        virtual public System.String T46
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T46);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T46, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T45
        /// </summary>
        virtual public System.String T45
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T45);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T45, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T85
        /// </summary>
        virtual public System.String T85
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T85);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T85, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T44
        /// </summary>
        virtual public System.String T44
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T44);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T44, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T84
        /// </summary>
        virtual public System.String T84
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T84);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T84, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T43
        /// </summary>
        virtual public System.String T43
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T43);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T43, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T83
        /// </summary>
        virtual public System.String T83
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T83);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T83, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T42
        /// </summary>
        virtual public System.String T42
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T42);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T42, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T82
        /// </summary>
        virtual public System.String T82
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T82);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T82, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T41
        /// </summary>
        virtual public System.String T41
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T41);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T41, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T81
        /// </summary>
        virtual public System.String T81
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T81);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T81, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T38
        /// </summary>
        virtual public System.String T38
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T38);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T38, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T37
        /// </summary>
        virtual public System.String T37
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T37);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T37, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T36
        /// </summary>
        virtual public System.String T36
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T36);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T36, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T75
        /// </summary>
        virtual public System.String T75
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T75);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T75, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T35
        /// </summary>
        virtual public System.String T35
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T35);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T35, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T74
        /// </summary>
        virtual public System.String T74
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T74);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T74, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T34
        /// </summary>
        virtual public System.String T34
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T34);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T34, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T73
        /// </summary>
        virtual public System.String T73
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T73);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T73, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T33
        /// </summary>
        virtual public System.String T33
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T33);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T33, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T72
        /// </summary>
        virtual public System.String T72
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T72);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T72, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T32
        /// </summary>
        virtual public System.String T32
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T32);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T32, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T71
        /// </summary>
        virtual public System.String T71
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T71);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T71, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T31
        /// </summary>
        virtual public System.String T31
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T31);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T31, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T1151Notes
        /// </summary>
        virtual public System.String T1151Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T1151Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T1151Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T1252Notes
        /// </summary>
        virtual public System.String T1252Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T1252Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T1252Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T1353Notes
        /// </summary>
        virtual public System.String T1353Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T1353Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T1353Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T1454Notes
        /// </summary>
        virtual public System.String T1454Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T1454Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T1454Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T1555Notes
        /// </summary>
        virtual public System.String T1555Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T1555Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T1555Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T16Notes
        /// </summary>
        virtual public System.String T16Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T16Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T16Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T17Notes
        /// </summary>
        virtual public System.String T17Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T17Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T17Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T18Notes
        /// </summary>
        virtual public System.String T18Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T18Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T18Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T6121Notes
        /// </summary>
        virtual public System.String T6121Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T6121Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T6121Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T6222Notes
        /// </summary>
        virtual public System.String T6222Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T6222Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T6222Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T6323Notes
        /// </summary>
        virtual public System.String T6323Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T6323Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T6323Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T6424Notes
        /// </summary>
        virtual public System.String T6424Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T6424Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T6424Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T6525Notes
        /// </summary>
        virtual public System.String T6525Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T6525Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T6525Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T26Notes
        /// </summary>
        virtual public System.String T26Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T26Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T26Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T27Notes
        /// </summary>
        virtual public System.String T27Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T27Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T27Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T28Notes
        /// </summary>
        virtual public System.String T28Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T28Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T28Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T48Notes
        /// </summary>
        virtual public System.String T48Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T48Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T48Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T47Notes
        /// </summary>
        virtual public System.String T47Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T47Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T47Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T46Notes
        /// </summary>
        virtual public System.String T46Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T46Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T46Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T4585Notes
        /// </summary>
        virtual public System.String T4585Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T4585Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T4585Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T4484Notes
        /// </summary>
        virtual public System.String T4484Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T4484Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T4484Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T4383Notes
        /// </summary>
        virtual public System.String T4383Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T4383Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T4383Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T4282Notes
        /// </summary>
        virtual public System.String T4282Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T4282Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T4282Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T4181Notes
        /// </summary>
        virtual public System.String T4181Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T4181Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T4181Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T38Notes
        /// </summary>
        virtual public System.String T38Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T38Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T38Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T37Notes
        /// </summary>
        virtual public System.String T37Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T37Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T37Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T36Notes
        /// </summary>
        virtual public System.String T36Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T36Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T36Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T7535Notes
        /// </summary>
        virtual public System.String T7535Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T7535Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T7535Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T7434Notes
        /// </summary>
        virtual public System.String T7434Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T7434Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T7434Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T7333Notes
        /// </summary>
        virtual public System.String T7333Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T7333Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T7333Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T7232Notes
        /// </summary>
        virtual public System.String T7232Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T7232Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T7232Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.T7131Notes
        /// </summary>
        virtual public System.String T7131Notes
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.T7131Notes);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.T7131Notes, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.LastUpdateDateTime
        /// </summary>
        virtual public System.DateTime? LastUpdateDateTime
        {
            get
            {
                return base.GetSystemDateTime(PatientOdontogramMetadata.ColumnNames.LastUpdateDateTime);
            }

            set
            {
                base.SetSystemDateTime(PatientOdontogramMetadata.ColumnNames.LastUpdateDateTime, value);
            }
        }
        /// <summary>
        /// Maps to PatientOdontogram.LastUpdateByUserID
        /// </summary>
        virtual public System.String LastUpdateByUserID
        {
            get
            {
                return base.GetSystemString(PatientOdontogramMetadata.ColumnNames.LastUpdateByUserID);
            }

            set
            {
                base.SetSystemString(PatientOdontogramMetadata.ColumnNames.LastUpdateByUserID, value);
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
            public esStrings(esPatientOdontogram entity)
            {
                this.entity = entity;
            }
            public System.String PatientID
            {
                get
                {
                    System.String data = entity.PatientID;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.PatientID = null;
                    else entity.PatientID = Convert.ToString(value);
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
            public System.String OdontogramDateTime
            {
                get
                {
                    System.DateTime? data = entity.OdontogramDateTime;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.OdontogramDateTime = null;
                    else entity.OdontogramDateTime = Convert.ToDateTime(value);
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
            public System.String T11
            {
                get
                {
                    System.String data = entity.T11;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T11 = null;
                    else entity.T11 = Convert.ToString(value);
                }
            }
            public System.String T51
            {
                get
                {
                    System.String data = entity.T51;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T51 = null;
                    else entity.T51 = Convert.ToString(value);
                }
            }
            public System.String T12
            {
                get
                {
                    System.String data = entity.T12;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T12 = null;
                    else entity.T12 = Convert.ToString(value);
                }
            }
            public System.String T52
            {
                get
                {
                    System.String data = entity.T52;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T52 = null;
                    else entity.T52 = Convert.ToString(value);
                }
            }
            public System.String T13
            {
                get
                {
                    System.String data = entity.T13;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T13 = null;
                    else entity.T13 = Convert.ToString(value);
                }
            }
            public System.String T53
            {
                get
                {
                    System.String data = entity.T53;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T53 = null;
                    else entity.T53 = Convert.ToString(value);
                }
            }
            public System.String T14
            {
                get
                {
                    System.String data = entity.T14;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T14 = null;
                    else entity.T14 = Convert.ToString(value);
                }
            }
            public System.String T54
            {
                get
                {
                    System.String data = entity.T54;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T54 = null;
                    else entity.T54 = Convert.ToString(value);
                }
            }
            public System.String T15
            {
                get
                {
                    System.String data = entity.T15;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T15 = null;
                    else entity.T15 = Convert.ToString(value);
                }
            }
            public System.String T55
            {
                get
                {
                    System.String data = entity.T55;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T55 = null;
                    else entity.T55 = Convert.ToString(value);
                }
            }
            public System.String T16
            {
                get
                {
                    System.String data = entity.T16;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T16 = null;
                    else entity.T16 = Convert.ToString(value);
                }
            }
            public System.String T17
            {
                get
                {
                    System.String data = entity.T17;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T17 = null;
                    else entity.T17 = Convert.ToString(value);
                }
            }
            public System.String T18
            {
                get
                {
                    System.String data = entity.T18;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T18 = null;
                    else entity.T18 = Convert.ToString(value);
                }
            }
            public System.String T61
            {
                get
                {
                    System.String data = entity.T61;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T61 = null;
                    else entity.T61 = Convert.ToString(value);
                }
            }
            public System.String T21
            {
                get
                {
                    System.String data = entity.T21;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T21 = null;
                    else entity.T21 = Convert.ToString(value);
                }
            }
            public System.String T62
            {
                get
                {
                    System.String data = entity.T62;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T62 = null;
                    else entity.T62 = Convert.ToString(value);
                }
            }
            public System.String T22
            {
                get
                {
                    System.String data = entity.T22;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T22 = null;
                    else entity.T22 = Convert.ToString(value);
                }
            }
            public System.String T63
            {
                get
                {
                    System.String data = entity.T63;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T63 = null;
                    else entity.T63 = Convert.ToString(value);
                }
            }
            public System.String T23
            {
                get
                {
                    System.String data = entity.T23;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T23 = null;
                    else entity.T23 = Convert.ToString(value);
                }
            }
            public System.String T64
            {
                get
                {
                    System.String data = entity.T64;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T64 = null;
                    else entity.T64 = Convert.ToString(value);
                }
            }
            public System.String T24
            {
                get
                {
                    System.String data = entity.T24;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T24 = null;
                    else entity.T24 = Convert.ToString(value);
                }
            }
            public System.String T65
            {
                get
                {
                    System.String data = entity.T65;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T65 = null;
                    else entity.T65 = Convert.ToString(value);
                }
            }
            public System.String T25
            {
                get
                {
                    System.String data = entity.T25;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T25 = null;
                    else entity.T25 = Convert.ToString(value);
                }
            }
            public System.String T26
            {
                get
                {
                    System.String data = entity.T26;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T26 = null;
                    else entity.T26 = Convert.ToString(value);
                }
            }
            public System.String T27
            {
                get
                {
                    System.String data = entity.T27;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T27 = null;
                    else entity.T27 = Convert.ToString(value);
                }
            }
            public System.String T28
            {
                get
                {
                    System.String data = entity.T28;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T28 = null;
                    else entity.T28 = Convert.ToString(value);
                }
            }
            public System.String T48
            {
                get
                {
                    System.String data = entity.T48;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T48 = null;
                    else entity.T48 = Convert.ToString(value);
                }
            }
            public System.String T47
            {
                get
                {
                    System.String data = entity.T47;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T47 = null;
                    else entity.T47 = Convert.ToString(value);
                }
            }
            public System.String T46
            {
                get
                {
                    System.String data = entity.T46;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T46 = null;
                    else entity.T46 = Convert.ToString(value);
                }
            }
            public System.String T45
            {
                get
                {
                    System.String data = entity.T45;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T45 = null;
                    else entity.T45 = Convert.ToString(value);
                }
            }
            public System.String T85
            {
                get
                {
                    System.String data = entity.T85;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T85 = null;
                    else entity.T85 = Convert.ToString(value);
                }
            }
            public System.String T44
            {
                get
                {
                    System.String data = entity.T44;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T44 = null;
                    else entity.T44 = Convert.ToString(value);
                }
            }
            public System.String T84
            {
                get
                {
                    System.String data = entity.T84;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T84 = null;
                    else entity.T84 = Convert.ToString(value);
                }
            }
            public System.String T43
            {
                get
                {
                    System.String data = entity.T43;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T43 = null;
                    else entity.T43 = Convert.ToString(value);
                }
            }
            public System.String T83
            {
                get
                {
                    System.String data = entity.T83;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T83 = null;
                    else entity.T83 = Convert.ToString(value);
                }
            }
            public System.String T42
            {
                get
                {
                    System.String data = entity.T42;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T42 = null;
                    else entity.T42 = Convert.ToString(value);
                }
            }
            public System.String T82
            {
                get
                {
                    System.String data = entity.T82;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T82 = null;
                    else entity.T82 = Convert.ToString(value);
                }
            }
            public System.String T41
            {
                get
                {
                    System.String data = entity.T41;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T41 = null;
                    else entity.T41 = Convert.ToString(value);
                }
            }
            public System.String T81
            {
                get
                {
                    System.String data = entity.T81;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T81 = null;
                    else entity.T81 = Convert.ToString(value);
                }
            }
            public System.String T38
            {
                get
                {
                    System.String data = entity.T38;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T38 = null;
                    else entity.T38 = Convert.ToString(value);
                }
            }
            public System.String T37
            {
                get
                {
                    System.String data = entity.T37;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T37 = null;
                    else entity.T37 = Convert.ToString(value);
                }
            }
            public System.String T36
            {
                get
                {
                    System.String data = entity.T36;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T36 = null;
                    else entity.T36 = Convert.ToString(value);
                }
            }
            public System.String T75
            {
                get
                {
                    System.String data = entity.T75;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T75 = null;
                    else entity.T75 = Convert.ToString(value);
                }
            }
            public System.String T35
            {
                get
                {
                    System.String data = entity.T35;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T35 = null;
                    else entity.T35 = Convert.ToString(value);
                }
            }
            public System.String T74
            {
                get
                {
                    System.String data = entity.T74;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T74 = null;
                    else entity.T74 = Convert.ToString(value);
                }
            }
            public System.String T34
            {
                get
                {
                    System.String data = entity.T34;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T34 = null;
                    else entity.T34 = Convert.ToString(value);
                }
            }
            public System.String T73
            {
                get
                {
                    System.String data = entity.T73;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T73 = null;
                    else entity.T73 = Convert.ToString(value);
                }
            }
            public System.String T33
            {
                get
                {
                    System.String data = entity.T33;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T33 = null;
                    else entity.T33 = Convert.ToString(value);
                }
            }
            public System.String T72
            {
                get
                {
                    System.String data = entity.T72;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T72 = null;
                    else entity.T72 = Convert.ToString(value);
                }
            }
            public System.String T32
            {
                get
                {
                    System.String data = entity.T32;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T32 = null;
                    else entity.T32 = Convert.ToString(value);
                }
            }
            public System.String T71
            {
                get
                {
                    System.String data = entity.T71;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T71 = null;
                    else entity.T71 = Convert.ToString(value);
                }
            }
            public System.String T31
            {
                get
                {
                    System.String data = entity.T31;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T31 = null;
                    else entity.T31 = Convert.ToString(value);
                }
            }
            public System.String T1151Notes
            {
                get
                {
                    System.String data = entity.T1151Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T1151Notes = null;
                    else entity.T1151Notes = Convert.ToString(value);
                }
            }
            public System.String T1252Notes
            {
                get
                {
                    System.String data = entity.T1252Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T1252Notes = null;
                    else entity.T1252Notes = Convert.ToString(value);
                }
            }
            public System.String T1353Notes
            {
                get
                {
                    System.String data = entity.T1353Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T1353Notes = null;
                    else entity.T1353Notes = Convert.ToString(value);
                }
            }
            public System.String T1454Notes
            {
                get
                {
                    System.String data = entity.T1454Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T1454Notes = null;
                    else entity.T1454Notes = Convert.ToString(value);
                }
            }
            public System.String T1555Notes
            {
                get
                {
                    System.String data = entity.T1555Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T1555Notes = null;
                    else entity.T1555Notes = Convert.ToString(value);
                }
            }
            public System.String T16Notes
            {
                get
                {
                    System.String data = entity.T16Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T16Notes = null;
                    else entity.T16Notes = Convert.ToString(value);
                }
            }
            public System.String T17Notes
            {
                get
                {
                    System.String data = entity.T17Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T17Notes = null;
                    else entity.T17Notes = Convert.ToString(value);
                }
            }
            public System.String T18Notes
            {
                get
                {
                    System.String data = entity.T18Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T18Notes = null;
                    else entity.T18Notes = Convert.ToString(value);
                }
            }
            public System.String T6121Notes
            {
                get
                {
                    System.String data = entity.T6121Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T6121Notes = null;
                    else entity.T6121Notes = Convert.ToString(value);
                }
            }
            public System.String T6222Notes
            {
                get
                {
                    System.String data = entity.T6222Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T6222Notes = null;
                    else entity.T6222Notes = Convert.ToString(value);
                }
            }
            public System.String T6323Notes
            {
                get
                {
                    System.String data = entity.T6323Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T6323Notes = null;
                    else entity.T6323Notes = Convert.ToString(value);
                }
            }
            public System.String T6424Notes
            {
                get
                {
                    System.String data = entity.T6424Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T6424Notes = null;
                    else entity.T6424Notes = Convert.ToString(value);
                }
            }
            public System.String T6525Notes
            {
                get
                {
                    System.String data = entity.T6525Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T6525Notes = null;
                    else entity.T6525Notes = Convert.ToString(value);
                }
            }
            public System.String T26Notes
            {
                get
                {
                    System.String data = entity.T26Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T26Notes = null;
                    else entity.T26Notes = Convert.ToString(value);
                }
            }
            public System.String T27Notes
            {
                get
                {
                    System.String data = entity.T27Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T27Notes = null;
                    else entity.T27Notes = Convert.ToString(value);
                }
            }
            public System.String T28Notes
            {
                get
                {
                    System.String data = entity.T28Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T28Notes = null;
                    else entity.T28Notes = Convert.ToString(value);
                }
            }
            public System.String T48Notes
            {
                get
                {
                    System.String data = entity.T48Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T48Notes = null;
                    else entity.T48Notes = Convert.ToString(value);
                }
            }
            public System.String T47Notes
            {
                get
                {
                    System.String data = entity.T47Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T47Notes = null;
                    else entity.T47Notes = Convert.ToString(value);
                }
            }
            public System.String T46Notes
            {
                get
                {
                    System.String data = entity.T46Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T46Notes = null;
                    else entity.T46Notes = Convert.ToString(value);
                }
            }
            public System.String T4585Notes
            {
                get
                {
                    System.String data = entity.T4585Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T4585Notes = null;
                    else entity.T4585Notes = Convert.ToString(value);
                }
            }
            public System.String T4484Notes
            {
                get
                {
                    System.String data = entity.T4484Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T4484Notes = null;
                    else entity.T4484Notes = Convert.ToString(value);
                }
            }
            public System.String T4383Notes
            {
                get
                {
                    System.String data = entity.T4383Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T4383Notes = null;
                    else entity.T4383Notes = Convert.ToString(value);
                }
            }
            public System.String T4282Notes
            {
                get
                {
                    System.String data = entity.T4282Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T4282Notes = null;
                    else entity.T4282Notes = Convert.ToString(value);
                }
            }
            public System.String T4181Notes
            {
                get
                {
                    System.String data = entity.T4181Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T4181Notes = null;
                    else entity.T4181Notes = Convert.ToString(value);
                }
            }
            public System.String T38Notes
            {
                get
                {
                    System.String data = entity.T38Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T38Notes = null;
                    else entity.T38Notes = Convert.ToString(value);
                }
            }
            public System.String T37Notes
            {
                get
                {
                    System.String data = entity.T37Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T37Notes = null;
                    else entity.T37Notes = Convert.ToString(value);
                }
            }
            public System.String T36Notes
            {
                get
                {
                    System.String data = entity.T36Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T36Notes = null;
                    else entity.T36Notes = Convert.ToString(value);
                }
            }
            public System.String T7535Notes
            {
                get
                {
                    System.String data = entity.T7535Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T7535Notes = null;
                    else entity.T7535Notes = Convert.ToString(value);
                }
            }
            public System.String T7434Notes
            {
                get
                {
                    System.String data = entity.T7434Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T7434Notes = null;
                    else entity.T7434Notes = Convert.ToString(value);
                }
            }
            public System.String T7333Notes
            {
                get
                {
                    System.String data = entity.T7333Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T7333Notes = null;
                    else entity.T7333Notes = Convert.ToString(value);
                }
            }
            public System.String T7232Notes
            {
                get
                {
                    System.String data = entity.T7232Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T7232Notes = null;
                    else entity.T7232Notes = Convert.ToString(value);
                }
            }
            public System.String T7131Notes
            {
                get
                {
                    System.String data = entity.T7131Notes;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.T7131Notes = null;
                    else entity.T7131Notes = Convert.ToString(value);
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
            private esPatientOdontogram entity;
        }
        #endregion

        #region Query Logic
        protected void InitQuery(esPatientOdontogramQuery query)
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
                throw new Exception("esPatientOdontogram can only hold one record of data");
            }

            return dataFound;
        }
        #endregion

        [NonSerialized]
        private esStrings esstrings;
    }


    public partial class PatientOdontogram : esPatientOdontogram
    {
    }

    [Serializable]
    abstract public class esPatientOdontogramQuery : esDynamicQuery
    {

        override protected IMetadata Meta
        {
            get
            {
                return PatientOdontogramMetadata.Meta();
            }
        }

        public esQueryItem PatientID
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.PatientID, esSystemType.String);
            }
        }

        public esQueryItem RegistrationNo
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.RegistrationNo, esSystemType.String);
            }
        }

        public esQueryItem OdontogramDateTime
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.OdontogramDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem ServiceUnitID
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
            }
        }

        public esQueryItem T11
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T11, esSystemType.String);
            }
        }

        public esQueryItem T51
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T51, esSystemType.String);
            }
        }

        public esQueryItem T12
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T12, esSystemType.String);
            }
        }

        public esQueryItem T52
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T52, esSystemType.String);
            }
        }

        public esQueryItem T13
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T13, esSystemType.String);
            }
        }

        public esQueryItem T53
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T53, esSystemType.String);
            }
        }

        public esQueryItem T14
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T14, esSystemType.String);
            }
        }

        public esQueryItem T54
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T54, esSystemType.String);
            }
        }

        public esQueryItem T15
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T15, esSystemType.String);
            }
        }

        public esQueryItem T55
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T55, esSystemType.String);
            }
        }

        public esQueryItem T16
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T16, esSystemType.String);
            }
        }

        public esQueryItem T17
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T17, esSystemType.String);
            }
        }

        public esQueryItem T18
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T18, esSystemType.String);
            }
        }

        public esQueryItem T61
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T61, esSystemType.String);
            }
        }

        public esQueryItem T21
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T21, esSystemType.String);
            }
        }

        public esQueryItem T62
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T62, esSystemType.String);
            }
        }

        public esQueryItem T22
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T22, esSystemType.String);
            }
        }

        public esQueryItem T63
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T63, esSystemType.String);
            }
        }

        public esQueryItem T23
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T23, esSystemType.String);
            }
        }

        public esQueryItem T64
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T64, esSystemType.String);
            }
        }

        public esQueryItem T24
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T24, esSystemType.String);
            }
        }

        public esQueryItem T65
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T65, esSystemType.String);
            }
        }

        public esQueryItem T25
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T25, esSystemType.String);
            }
        }

        public esQueryItem T26
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T26, esSystemType.String);
            }
        }

        public esQueryItem T27
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T27, esSystemType.String);
            }
        }

        public esQueryItem T28
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T28, esSystemType.String);
            }
        }

        public esQueryItem T48
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T48, esSystemType.String);
            }
        }

        public esQueryItem T47
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T47, esSystemType.String);
            }
        }

        public esQueryItem T46
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T46, esSystemType.String);
            }
        }

        public esQueryItem T45
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T45, esSystemType.String);
            }
        }

        public esQueryItem T85
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T85, esSystemType.String);
            }
        }

        public esQueryItem T44
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T44, esSystemType.String);
            }
        }

        public esQueryItem T84
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T84, esSystemType.String);
            }
        }

        public esQueryItem T43
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T43, esSystemType.String);
            }
        }

        public esQueryItem T83
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T83, esSystemType.String);
            }
        }

        public esQueryItem T42
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T42, esSystemType.String);
            }
        }

        public esQueryItem T82
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T82, esSystemType.String);
            }
        }

        public esQueryItem T41
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T41, esSystemType.String);
            }
        }

        public esQueryItem T81
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T81, esSystemType.String);
            }
        }

        public esQueryItem T38
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T38, esSystemType.String);
            }
        }

        public esQueryItem T37
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T37, esSystemType.String);
            }
        }

        public esQueryItem T36
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T36, esSystemType.String);
            }
        }

        public esQueryItem T75
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T75, esSystemType.String);
            }
        }

        public esQueryItem T35
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T35, esSystemType.String);
            }
        }

        public esQueryItem T74
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T74, esSystemType.String);
            }
        }

        public esQueryItem T34
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T34, esSystemType.String);
            }
        }

        public esQueryItem T73
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T73, esSystemType.String);
            }
        }

        public esQueryItem T33
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T33, esSystemType.String);
            }
        }

        public esQueryItem T72
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T72, esSystemType.String);
            }
        }

        public esQueryItem T32
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T32, esSystemType.String);
            }
        }

        public esQueryItem T71
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T71, esSystemType.String);
            }
        }

        public esQueryItem T31
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T31, esSystemType.String);
            }
        }

        public esQueryItem T1151Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T1151Notes, esSystemType.String);
            }
        }

        public esQueryItem T1252Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T1252Notes, esSystemType.String);
            }
        }

        public esQueryItem T1353Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T1353Notes, esSystemType.String);
            }
        }

        public esQueryItem T1454Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T1454Notes, esSystemType.String);
            }
        }

        public esQueryItem T1555Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T1555Notes, esSystemType.String);
            }
        }

        public esQueryItem T16Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T16Notes, esSystemType.String);
            }
        }

        public esQueryItem T17Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T17Notes, esSystemType.String);
            }
        }

        public esQueryItem T18Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T18Notes, esSystemType.String);
            }
        }

        public esQueryItem T6121Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T6121Notes, esSystemType.String);
            }
        }

        public esQueryItem T6222Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T6222Notes, esSystemType.String);
            }
        }

        public esQueryItem T6323Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T6323Notes, esSystemType.String);
            }
        }

        public esQueryItem T6424Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T6424Notes, esSystemType.String);
            }
        }

        public esQueryItem T6525Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T6525Notes, esSystemType.String);
            }
        }

        public esQueryItem T26Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T26Notes, esSystemType.String);
            }
        }

        public esQueryItem T27Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T27Notes, esSystemType.String);
            }
        }

        public esQueryItem T28Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T28Notes, esSystemType.String);
            }
        }

        public esQueryItem T48Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T48Notes, esSystemType.String);
            }
        }

        public esQueryItem T47Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T47Notes, esSystemType.String);
            }
        }

        public esQueryItem T46Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T46Notes, esSystemType.String);
            }
        }

        public esQueryItem T4585Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T4585Notes, esSystemType.String);
            }
        }

        public esQueryItem T4484Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T4484Notes, esSystemType.String);
            }
        }

        public esQueryItem T4383Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T4383Notes, esSystemType.String);
            }
        }

        public esQueryItem T4282Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T4282Notes, esSystemType.String);
            }
        }

        public esQueryItem T4181Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T4181Notes, esSystemType.String);
            }
        }

        public esQueryItem T38Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T38Notes, esSystemType.String);
            }
        }

        public esQueryItem T37Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T37Notes, esSystemType.String);
            }
        }

        public esQueryItem T36Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T36Notes, esSystemType.String);
            }
        }

        public esQueryItem T7535Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T7535Notes, esSystemType.String);
            }
        }

        public esQueryItem T7434Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T7434Notes, esSystemType.String);
            }
        }

        public esQueryItem T7333Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T7333Notes, esSystemType.String);
            }
        }

        public esQueryItem T7232Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T7232Notes, esSystemType.String);
            }
        }

        public esQueryItem T7131Notes
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.T7131Notes, esSystemType.String);
            }
        }

        public esQueryItem LastUpdateDateTime
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
            }
        }

        public esQueryItem LastUpdateByUserID
        {
            get
            {
                return new esQueryItem(this, PatientOdontogramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
    [Serializable]
    [XmlType("PatientOdontogramCollection")]
    public partial class PatientOdontogramCollection : esPatientOdontogramCollection, IEnumerable<PatientOdontogram>
    {
        public PatientOdontogramCollection()
        {

        }

        public static implicit operator List<PatientOdontogram>(PatientOdontogramCollection coll)
        {
            List<PatientOdontogram> list = new List<PatientOdontogram>();

            foreach (PatientOdontogram emp in coll)
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
                return PatientOdontogramMetadata.Meta();
            }
        }

        override protected esDynamicQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientOdontogramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }

        override protected esEntity CreateEntityForCollection(DataRow row)
        {
            return new PatientOdontogram(row);
        }

        override protected esEntity CreateEntity()
        {
            return new PatientOdontogram();
        }

        #endregion

        [BrowsableAttribute(false)]
        public PatientOdontogramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientOdontogramQuery();
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
        public bool Load(PatientOdontogramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// Always calls AddNew() on the entity, in case it is overridden.
        /// </summary>
        public PatientOdontogram AddNew()
        {
            PatientOdontogram entity = base.AddNewEntity() as PatientOdontogram;

            return entity;
        }
        public PatientOdontogram FindByPrimaryKey(String patientID, String registrationNo)
        {
            return base.FindByPrimaryKey(patientID, registrationNo) as PatientOdontogram;
        }

        #region IEnumerable< PatientOdontogram> Members

        IEnumerator<PatientOdontogram> IEnumerable<PatientOdontogram>.GetEnumerator()
        {
            System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
            System.Collections.IEnumerator iterator = enumer.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return iterator.Current as PatientOdontogram;
            }
        }

        #endregion

        private PatientOdontogramQuery query;
    }


    /// <summary>
    /// Encapsulates the 'PatientOdontogram' table
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("PatientOdontogram ({PatientID, RegistrationNo})")]
    [Serializable]
    public partial class PatientOdontogram : esPatientOdontogram
    {
        public PatientOdontogram()
        {
        }

        public PatientOdontogram(DataRow row)
            : base(row)
        {
        }

        #region Housekeeping methods
        override protected IMetadata Meta
        {
            get
            {
                return PatientOdontogramMetadata.Meta();
            }
        }

        override protected esPatientOdontogramQuery GetDynamicQuery()
        {
            if (this.query == null)
            {
                this.query = new PatientOdontogramQuery();
                this.InitQuery(query);
            }
            return this.query;
        }
        #endregion

        [BrowsableAttribute(false)]
        public PatientOdontogramQuery Query
        {
            get
            {
                if (this.query == null)
                {
                    this.query = new PatientOdontogramQuery();
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
        public bool Load(PatientOdontogramQuery query)
        {
            this.query = query;
            base.InitQuery(this.query);
            return this.Query.Load();
        }

        private PatientOdontogramQuery query;
    }

    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
    [Serializable]
    public partial class PatientOdontogramQuery : esPatientOdontogramQuery
    {
        public PatientOdontogramQuery()
        {

        }

        public PatientOdontogramQuery(string joinAlias)
        {
            this.es.JoinAlias = joinAlias;
        }

        override protected string GetQueryName()
        {
            return "PatientOdontogramQuery";
        }
    }

    [Serializable]
    public partial class PatientOdontogramMetadata : esMetadata, IMetadata
    {
        #region Protected Constructor
        protected PatientOdontogramMetadata()
        {
            _columns = new esColumnMetadataCollection();
            esColumnMetadata c;

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.PatientID;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 15;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.RegistrationNo;
            c.IsInPrimaryKey = true;
            c.CharacterMaxLength = 20;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.OdontogramDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.OdontogramDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.ServiceUnitID, 3, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.ServiceUnitID;
            c.CharacterMaxLength = 10;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T11, 4, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T11;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T51, 5, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T51;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T12, 6, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T12;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T52, 7, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T52;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T13, 8, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T13;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T53, 9, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T53;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T14, 10, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T14;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T54, 11, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T54;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T15, 12, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T15;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T55, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T55;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T16, 14, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T16;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T17, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T17;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T18, 16, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T18;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T61, 17, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T61;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T21, 18, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T21;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T62, 19, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T62;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T22, 20, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T22;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T63, 21, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T63;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T23, 22, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T23;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T64, 23, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T64;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T24, 24, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T24;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T65, 25, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T65;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T25, 26, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T25;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T26, 27, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T26;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T27, 28, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T27;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T28, 29, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T28;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T48, 30, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T48;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T47, 31, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T47;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T46, 32, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T46;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T45, 33, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T45;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T85, 34, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T85;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T44, 35, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T44;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T84, 36, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T84;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T43, 37, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T43;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T83, 38, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T83;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T42, 39, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T42;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T82, 40, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T82;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T41, 41, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T41;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T81, 42, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T81;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T38, 43, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T38;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T37, 44, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T37;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T36, 45, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T36;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T75, 46, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T75;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T35, 47, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T35;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T74, 48, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T74;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T34, 49, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T34;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T73, 50, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T73;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T33, 51, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T33;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T72, 52, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T72;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T32, 53, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T32;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T71, 54, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T71;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T31, 55, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T31;
            c.CharacterMaxLength = 2;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T1151Notes, 56, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T1151Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T1252Notes, 57, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T1252Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T1353Notes, 58, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T1353Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T1454Notes, 59, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T1454Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T1555Notes, 60, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T1555Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T16Notes, 61, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T16Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T17Notes, 62, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T17Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T18Notes, 63, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T18Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T6121Notes, 64, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T6121Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T6222Notes, 65, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T6222Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T6323Notes, 66, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T6323Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T6424Notes, 67, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T6424Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T6525Notes, 68, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T6525Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T26Notes, 69, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T26Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T27Notes, 70, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T27Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T28Notes, 71, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T28Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T48Notes, 72, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T48Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T47Notes, 73, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T47Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T46Notes, 74, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T46Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T4585Notes, 75, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T4585Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T4484Notes, 76, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T4484Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T4383Notes, 77, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T4383Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T4282Notes, 78, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T4282Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T4181Notes, 79, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T4181Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T38Notes, 80, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T38Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T37Notes, 81, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T37Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T36Notes, 82, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T36Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T7535Notes, 83, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T7535Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T7434Notes, 84, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T7434Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T7333Notes, 85, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T7333Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T7232Notes, 86, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T7232Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.T7131Notes, 87, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.T7131Notes;
            c.CharacterMaxLength = 250;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.LastUpdateDateTime, 88, typeof(System.DateTime), esSystemType.DateTime);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.LastUpdateDateTime;
            c.IsNullable = true;
            _columns.Add(c);

            c = new esColumnMetadata(PatientOdontogramMetadata.ColumnNames.LastUpdateByUserID, 89, typeof(System.String), esSystemType.String);
            c.PropertyName = PatientOdontogramMetadata.PropertyNames.LastUpdateByUserID;
            c.CharacterMaxLength = 20;
            c.IsNullable = true;
            _columns.Add(c);


        }
        #endregion

        static public PatientOdontogramMetadata Meta()
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
            public const string PatientID = "PatientID";
            public const string RegistrationNo = "RegistrationNo";
            public const string OdontogramDateTime = "OdontogramDateTime";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string T11 = "T11";
            public const string T51 = "T51";
            public const string T12 = "T12";
            public const string T52 = "T52";
            public const string T13 = "T13";
            public const string T53 = "T53";
            public const string T14 = "T14";
            public const string T54 = "T54";
            public const string T15 = "T15";
            public const string T55 = "T55";
            public const string T16 = "T16";
            public const string T17 = "T17";
            public const string T18 = "T18";
            public const string T61 = "T61";
            public const string T21 = "T21";
            public const string T62 = "T62";
            public const string T22 = "T22";
            public const string T63 = "T63";
            public const string T23 = "T23";
            public const string T64 = "T64";
            public const string T24 = "T24";
            public const string T65 = "T65";
            public const string T25 = "T25";
            public const string T26 = "T26";
            public const string T27 = "T27";
            public const string T28 = "T28";
            public const string T48 = "T48";
            public const string T47 = "T47";
            public const string T46 = "T46";
            public const string T45 = "T45";
            public const string T85 = "T85";
            public const string T44 = "T44";
            public const string T84 = "T84";
            public const string T43 = "T43";
            public const string T83 = "T83";
            public const string T42 = "T42";
            public const string T82 = "T82";
            public const string T41 = "T41";
            public const string T81 = "T81";
            public const string T38 = "T38";
            public const string T37 = "T37";
            public const string T36 = "T36";
            public const string T75 = "T75";
            public const string T35 = "T35";
            public const string T74 = "T74";
            public const string T34 = "T34";
            public const string T73 = "T73";
            public const string T33 = "T33";
            public const string T72 = "T72";
            public const string T32 = "T32";
            public const string T71 = "T71";
            public const string T31 = "T31";
            public const string T1151Notes = "T1151Notes";
            public const string T1252Notes = "T1252Notes";
            public const string T1353Notes = "T1353Notes";
            public const string T1454Notes = "T1454Notes";
            public const string T1555Notes = "T1555Notes";
            public const string T16Notes = "T16Notes";
            public const string T17Notes = "T17Notes";
            public const string T18Notes = "T18Notes";
            public const string T6121Notes = "T6121Notes";
            public const string T6222Notes = "T6222Notes";
            public const string T6323Notes = "T6323Notes";
            public const string T6424Notes = "T6424Notes";
            public const string T6525Notes = "T6525Notes";
            public const string T26Notes = "T26Notes";
            public const string T27Notes = "T27Notes";
            public const string T28Notes = "T28Notes";
            public const string T48Notes = "T48Notes";
            public const string T47Notes = "T47Notes";
            public const string T46Notes = "T46Notes";
            public const string T4585Notes = "T4585Notes";
            public const string T4484Notes = "T4484Notes";
            public const string T4383Notes = "T4383Notes";
            public const string T4282Notes = "T4282Notes";
            public const string T4181Notes = "T4181Notes";
            public const string T38Notes = "T38Notes";
            public const string T37Notes = "T37Notes";
            public const string T36Notes = "T36Notes";
            public const string T7535Notes = "T7535Notes";
            public const string T7434Notes = "T7434Notes";
            public const string T7333Notes = "T7333Notes";
            public const string T7232Notes = "T7232Notes";
            public const string T7131Notes = "T7131Notes";
            public const string LastUpdateDateTime = "LastUpdateDateTime";
            public const string LastUpdateByUserID = "LastUpdateByUserID";
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string PatientID = "PatientID";
            public const string RegistrationNo = "RegistrationNo";
            public const string OdontogramDateTime = "OdontogramDateTime";
            public const string ServiceUnitID = "ServiceUnitID";
            public const string T11 = "T11";
            public const string T51 = "T51";
            public const string T12 = "T12";
            public const string T52 = "T52";
            public const string T13 = "T13";
            public const string T53 = "T53";
            public const string T14 = "T14";
            public const string T54 = "T54";
            public const string T15 = "T15";
            public const string T55 = "T55";
            public const string T16 = "T16";
            public const string T17 = "T17";
            public const string T18 = "T18";
            public const string T61 = "T61";
            public const string T21 = "T21";
            public const string T62 = "T62";
            public const string T22 = "T22";
            public const string T63 = "T63";
            public const string T23 = "T23";
            public const string T64 = "T64";
            public const string T24 = "T24";
            public const string T65 = "T65";
            public const string T25 = "T25";
            public const string T26 = "T26";
            public const string T27 = "T27";
            public const string T28 = "T28";
            public const string T48 = "T48";
            public const string T47 = "T47";
            public const string T46 = "T46";
            public const string T45 = "T45";
            public const string T85 = "T85";
            public const string T44 = "T44";
            public const string T84 = "T84";
            public const string T43 = "T43";
            public const string T83 = "T83";
            public const string T42 = "T42";
            public const string T82 = "T82";
            public const string T41 = "T41";
            public const string T81 = "T81";
            public const string T38 = "T38";
            public const string T37 = "T37";
            public const string T36 = "T36";
            public const string T75 = "T75";
            public const string T35 = "T35";
            public const string T74 = "T74";
            public const string T34 = "T34";
            public const string T73 = "T73";
            public const string T33 = "T33";
            public const string T72 = "T72";
            public const string T32 = "T32";
            public const string T71 = "T71";
            public const string T31 = "T31";
            public const string T1151Notes = "T1151Notes";
            public const string T1252Notes = "T1252Notes";
            public const string T1353Notes = "T1353Notes";
            public const string T1454Notes = "T1454Notes";
            public const string T1555Notes = "T1555Notes";
            public const string T16Notes = "T16Notes";
            public const string T17Notes = "T17Notes";
            public const string T18Notes = "T18Notes";
            public const string T6121Notes = "T6121Notes";
            public const string T6222Notes = "T6222Notes";
            public const string T6323Notes = "T6323Notes";
            public const string T6424Notes = "T6424Notes";
            public const string T6525Notes = "T6525Notes";
            public const string T26Notes = "T26Notes";
            public const string T27Notes = "T27Notes";
            public const string T28Notes = "T28Notes";
            public const string T48Notes = "T48Notes";
            public const string T47Notes = "T47Notes";
            public const string T46Notes = "T46Notes";
            public const string T4585Notes = "T4585Notes";
            public const string T4484Notes = "T4484Notes";
            public const string T4383Notes = "T4383Notes";
            public const string T4282Notes = "T4282Notes";
            public const string T4181Notes = "T4181Notes";
            public const string T38Notes = "T38Notes";
            public const string T37Notes = "T37Notes";
            public const string T36Notes = "T36Notes";
            public const string T7535Notes = "T7535Notes";
            public const string T7434Notes = "T7434Notes";
            public const string T7333Notes = "T7333Notes";
            public const string T7232Notes = "T7232Notes";
            public const string T7131Notes = "T7131Notes";
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
            lock (typeof(PatientOdontogramMetadata))
            {
                if (PatientOdontogramMetadata.mapDelegates == null)
                {
                    PatientOdontogramMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
                }

                if (PatientOdontogramMetadata.meta == null)
                {
                    PatientOdontogramMetadata.meta = new PatientOdontogramMetadata();
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

                meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("OdontogramDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T11", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T51", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T12", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T52", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T13", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T53", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T14", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T54", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T15", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T55", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T16", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T17", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T18", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T61", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T21", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T62", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T22", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T63", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T23", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T64", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T24", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T65", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T25", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T26", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T27", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T28", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T48", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T47", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T46", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T45", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T85", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T44", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T84", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T43", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T83", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T42", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T82", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T41", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T81", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T38", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T37", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T36", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T75", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T35", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T74", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T34", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T73", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T33", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T72", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T32", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T71", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T31", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T1151Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T1252Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T1353Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T1454Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T1555Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T16Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T17Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T18Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T6121Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T6222Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T6323Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T6424Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T6525Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T26Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T27Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T28Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T48Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T47Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T46Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T4585Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T4484Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T4383Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T4282Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T4181Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T38Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T37Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T36Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T7535Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T7434Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T7333Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T7232Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("T7131Notes", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
                meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


                meta.Source = "PatientOdontogram";
                meta.Destination = "PatientOdontogram";
                meta.spInsert = "proc_PatientOdontogramInsert";
                meta.spUpdate = "proc_PatientOdontogramUpdate";
                meta.spDelete = "proc_PatientOdontogramDelete";
                meta.spLoadAll = "proc_PatientOdontogramLoadAll";
                meta.spLoadByPrimaryKey = "proc_PatientOdontogramLoadByPrimaryKey";

                this._providerMetadataMaps["esDefault"] = meta;
            }

            return this._providerMetadataMaps["esDefault"];
        }

        #endregion

        static private PatientOdontogramMetadata meta;
        static protected Dictionary<string, MapToMeta> mapDelegates;
        static private int _esDefault = RegisterDelegateesDefault();
    }

}
