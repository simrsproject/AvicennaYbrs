/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/10/2023 10:14:46 AM
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
	abstract public class esMedicalDischargeSummaryDiagnoseBakCollection : esEntityCollectionWAuditLog
	{
		public esMedicalDischargeSummaryDiagnoseBakCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "MedicalDischargeSummaryDiagnoseBakCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryDiagnoseBakQuery query)
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
			this.InitQuery(query as esMedicalDischargeSummaryDiagnoseBakQuery);
		}
		#endregion
			
		virtual public MedicalDischargeSummaryDiagnoseBak DetachEntity(MedicalDischargeSummaryDiagnoseBak entity)
		{
			return base.DetachEntity(entity) as MedicalDischargeSummaryDiagnoseBak;
		}
		
		virtual public MedicalDischargeSummaryDiagnoseBak AttachEntity(MedicalDischargeSummaryDiagnoseBak entity)
		{
			return base.AttachEntity(entity) as MedicalDischargeSummaryDiagnoseBak;
		}
		
		virtual public void Combine(MedicalDischargeSummaryDiagnoseBakCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalDischargeSummaryDiagnoseBak this[int index]
		{
			get
			{
				return base[index] as MedicalDischargeSummaryDiagnoseBak;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalDischargeSummaryDiagnoseBak);
		}
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryDiagnoseBak : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalDischargeSummaryDiagnoseBakQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esMedicalDischargeSummaryDiagnoseBak()
		{
		}
	
		public esMedicalDischargeSummaryDiagnoseBak(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationNo, String sequenceNo)
		{
			esMedicalDischargeSummaryDiagnoseBakQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo",registrationNo);
			parms.Add("SequenceNo",sequenceNo);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
						case "SRDiagnoseType": this.str.SRDiagnoseType = (string)value; break;
						case "DiagnosisText": this.str.DiagnosisText = (string)value; break;
						case "ExternalCauseID": this.str.ExternalCauseID = (string)value; break;
						case "IsOldCase": this.str.IsOldCase = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
                        case "DiagnoseSynonym": this.str.DiagnoseSynonym = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{	
						case "IsOldCase":
						
							if (value == null || value is System.Boolean)
								this.IsOldCase = (System.Boolean?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "CreatedDateTime":
						
							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
							break;
					
						default:
							break;
					}
				}
			}
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.SRDiagnoseType
		/// </summary>
		virtual public System.String SRDiagnoseType
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SRDiagnoseType);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SRDiagnoseType, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.DiagnosisText
		/// </summary>
		virtual public System.String DiagnosisText
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnosisText);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnosisText, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.ExternalCauseID
		/// </summary>
		virtual public System.String ExternalCauseID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ExternalCauseID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ExternalCauseID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.IsOldCase
		/// </summary>
		virtual public System.Boolean? IsOldCase
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsOldCase);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsOldCase, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalDischargeSummaryDiagnoseBak.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ParamedicID, value);
			}
		}
        /// <summary>
        /// Maps to MedicalDischargeSummaryDiagnoseBak.DiagnoseSynonym
        /// </summary>
        virtual public System.String DiagnoseSynonym
        {
            get
            {
                return base.GetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseSynonym);
            }

            set
            {
                base.SetSystemString(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseSynonym, value);
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
        [BrowsableAttribute( false )]		
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
			public esStrings(esMedicalDischargeSummaryDiagnoseBak entity)
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
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
				}
			}
			public System.String DiagnoseID
			{
				get
				{
					System.String data = entity.DiagnoseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseID = null;
					else entity.DiagnoseID = Convert.ToString(value);
				}
			}
			public System.String SRDiagnoseType
			{
				get
				{
					System.String data = entity.SRDiagnoseType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDiagnoseType = null;
					else entity.SRDiagnoseType = Convert.ToString(value);
				}
			}
			public System.String DiagnosisText
			{
				get
				{
					System.String data = entity.DiagnosisText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnosisText = null;
					else entity.DiagnosisText = Convert.ToString(value);
				}
			}
			public System.String ExternalCauseID
			{
				get
				{
					System.String data = entity.ExternalCauseID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExternalCauseID = null;
					else entity.ExternalCauseID = Convert.ToString(value);
				}
			}
			public System.String IsOldCase
			{
				get
				{
					System.Boolean? data = entity.IsOldCase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOldCase = null;
					else entity.IsOldCase = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
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
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
				}
			}
            public System.String DiagnoseSynonym
            {
                get
                {
                    System.String data = entity.DiagnoseSynonym;
                    return (data == null) ? String.Empty : Convert.ToString(data);
                }

                set
                {
                    if (value == null || value.Length == 0) entity.DiagnoseSynonym = null;
                    else entity.DiagnoseSynonym = Convert.ToString(value);
                }
            }
            private esMedicalDischargeSummaryDiagnoseBak entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalDischargeSummaryDiagnoseBakQuery query)
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
				throw new Exception("esMedicalDischargeSummaryDiagnoseBak can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalDischargeSummaryDiagnoseBak : esMedicalDischargeSummaryDiagnoseBak
	{	
	}

	[Serializable]
	abstract public class esMedicalDischargeSummaryDiagnoseBakQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryDiagnoseBakMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
			
		public esQueryItem SRDiagnoseType
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SRDiagnoseType, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnosisText
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnosisText, esSystemType.String);
			}
		} 
			
		public esQueryItem ExternalCauseID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ExternalCauseID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsOldCase
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsOldCase, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
        }

        public esQueryItem DiagnoseSynonym
        {
            get
            {
                return new esQueryItem(this, MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseSynonym, esSystemType.String);
            }
        }
    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalDischargeSummaryDiagnoseBakCollection")]
	public partial class MedicalDischargeSummaryDiagnoseBakCollection : esMedicalDischargeSummaryDiagnoseBakCollection, IEnumerable< MedicalDischargeSummaryDiagnoseBak>
	{
		public MedicalDischargeSummaryDiagnoseBakCollection()
		{

		}	
		
		public static implicit operator List< MedicalDischargeSummaryDiagnoseBak>(MedicalDischargeSummaryDiagnoseBakCollection coll)
		{
			List< MedicalDischargeSummaryDiagnoseBak> list = new List< MedicalDischargeSummaryDiagnoseBak>();
			
			foreach (MedicalDischargeSummaryDiagnoseBak emp in coll)
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
				return  MedicalDischargeSummaryDiagnoseBakMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryDiagnoseBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalDischargeSummaryDiagnoseBak(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalDischargeSummaryDiagnoseBak();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public MedicalDischargeSummaryDiagnoseBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryDiagnoseBakQuery();
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
		public bool Load(MedicalDischargeSummaryDiagnoseBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalDischargeSummaryDiagnoseBak AddNew()
		{
			MedicalDischargeSummaryDiagnoseBak entity = base.AddNewEntity() as MedicalDischargeSummaryDiagnoseBak;
			
			return entity;		
		}
		public MedicalDischargeSummaryDiagnoseBak FindByPrimaryKey(String registrationNo, String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationNo, sequenceNo) as MedicalDischargeSummaryDiagnoseBak;
		}

		#region IEnumerable< MedicalDischargeSummaryDiagnoseBak> Members

		IEnumerator< MedicalDischargeSummaryDiagnoseBak> IEnumerable< MedicalDischargeSummaryDiagnoseBak>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalDischargeSummaryDiagnoseBak;
			}
		}

		#endregion
		
		private MedicalDischargeSummaryDiagnoseBakQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalDischargeSummaryDiagnoseBak' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalDischargeSummaryDiagnoseBak ({RegistrationNo, SequenceNo})")]
	[Serializable]
	public partial class MedicalDischargeSummaryDiagnoseBak : esMedicalDischargeSummaryDiagnoseBak
	{
		public MedicalDischargeSummaryDiagnoseBak()
		{
		}	
	
		public MedicalDischargeSummaryDiagnoseBak(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalDischargeSummaryDiagnoseBakMetadata.Meta();
			}
		}	
	
		override protected esMedicalDischargeSummaryDiagnoseBakQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalDischargeSummaryDiagnoseBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public MedicalDischargeSummaryDiagnoseBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalDischargeSummaryDiagnoseBakQuery();
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
		public bool Load(MedicalDischargeSummaryDiagnoseBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private MedicalDischargeSummaryDiagnoseBakQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalDischargeSummaryDiagnoseBakQuery : esMedicalDischargeSummaryDiagnoseBakQuery
	{
		public MedicalDischargeSummaryDiagnoseBakQuery()
		{

		}		
		
		public MedicalDischargeSummaryDiagnoseBakQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "MedicalDischargeSummaryDiagnoseBakQuery";
        }
	}

	[Serializable]
	public partial class MedicalDischargeSummaryDiagnoseBakMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalDischargeSummaryDiagnoseBakMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.DiagnoseID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.SRDiagnoseType, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.SRDiagnoseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnosisText, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.DiagnosisText;
			c.CharacterMaxLength = 4000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ExternalCauseID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.ExternalCauseID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsOldCase, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.IsOldCase;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.CreatedDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.ParamedicID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(MedicalDischargeSummaryDiagnoseBakMetadata.ColumnNames.DiagnoseSynonym, 13, typeof(System.String), esSystemType.String);
            c.PropertyName = MedicalDischargeSummaryDiagnoseBakMetadata.PropertyNames.DiagnoseSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);
        }
		#endregion
	
		static public MedicalDischargeSummaryDiagnoseBakMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string DiagnoseID = "DiagnoseID";
			public const string SRDiagnoseType = "SRDiagnoseType";
			public const string DiagnosisText = "DiagnosisText";
			public const string ExternalCauseID = "ExternalCauseID";
			public const string IsOldCase = "IsOldCase";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string ParamedicID = "ParamedicID";
            public const string DiagnoseSynonym = "DiagnoseSynonym";
        }
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationNo = "RegistrationNo";
			public const string SequenceNo = "SequenceNo";
			public const string DiagnoseID = "DiagnoseID";
			public const string SRDiagnoseType = "SRDiagnoseType";
			public const string DiagnosisText = "DiagnosisText";
			public const string ExternalCauseID = "ExternalCauseID";
			public const string IsOldCase = "IsOldCase";
			public const string IsVoid = "IsVoid";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string ParamedicID = "ParamedicID";
            public const string DiagnoseSynonym = "DiagnoseSynonym";
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
			lock (typeof(MedicalDischargeSummaryDiagnoseBakMetadata))
			{
				if(MedicalDischargeSummaryDiagnoseBakMetadata.mapDelegates == null)
				{
					MedicalDischargeSummaryDiagnoseBakMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalDischargeSummaryDiagnoseBakMetadata.meta == null)
				{
					MedicalDischargeSummaryDiagnoseBakMetadata.meta = new MedicalDischargeSummaryDiagnoseBakMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiagnoseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosisText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExternalCauseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOldCase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnoseSynonym", new esTypeMap("varchar", "System.String"));


                meta.Source = "MedicalDischargeSummaryDiagnoseBak";
				meta.Destination = "MedicalDischargeSummaryDiagnoseBak";
				meta.spInsert = "proc_MedicalDischargeSummaryDiagnoseBakInsert";				
				meta.spUpdate = "proc_MedicalDischargeSummaryDiagnoseBakUpdate";		
				meta.spDelete = "proc_MedicalDischargeSummaryDiagnoseBakDelete";
				meta.spLoadAll = "proc_MedicalDischargeSummaryDiagnoseBakLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalDischargeSummaryDiagnoseBakLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalDischargeSummaryDiagnoseBakMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
