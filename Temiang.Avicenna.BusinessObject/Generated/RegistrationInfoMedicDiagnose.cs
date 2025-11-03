/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/30/2021 3:22:13 PM
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
	abstract public class esRegistrationInfoMedicDiagnoseCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationInfoMedicDiagnoseCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "RegistrationInfoMedicDiagnoseCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicDiagnoseQuery query)
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
			this.InitQuery(query as esRegistrationInfoMedicDiagnoseQuery);
		}
		#endregion
			
		virtual public RegistrationInfoMedicDiagnose DetachEntity(RegistrationInfoMedicDiagnose entity)
		{
			return base.DetachEntity(entity) as RegistrationInfoMedicDiagnose;
		}
		
		virtual public RegistrationInfoMedicDiagnose AttachEntity(RegistrationInfoMedicDiagnose entity)
		{
			return base.AttachEntity(entity) as RegistrationInfoMedicDiagnose;
		}
		
		virtual public void Combine(RegistrationInfoMedicDiagnoseCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationInfoMedicDiagnose this[int index]
		{
			get
			{
				return base[index] as RegistrationInfoMedicDiagnose;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationInfoMedicDiagnose);
		}
	}

	[Serializable]
	abstract public class esRegistrationInfoMedicDiagnose : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationInfoMedicDiagnoseQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esRegistrationInfoMedicDiagnose()
		{
		}
	
		public esRegistrationInfoMedicDiagnose(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationInfoMedicID, String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, sequenceNo);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationInfoMedicID, String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, sequenceNo);
		}
	
		private bool LoadByPrimaryKeyDynamic(String registrationInfoMedicID, String sequenceNo)
		{
			esRegistrationInfoMedicDiagnoseQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID, query.SequenceNo == sequenceNo);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String registrationInfoMedicID, String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoMedicID",registrationInfoMedicID);
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
						case "RegistrationInfoMedicID": this.str.RegistrationInfoMedicID = (string)value; break;
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
						case "DiagnoseID": this.str.DiagnoseID = (string)value; break;
						case "DiagnosisText": this.str.DiagnosisText = (string)value; break;
						case "SRDiagnoseType": this.str.SRDiagnoseType = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "DiagnoseDateTime": this.str.DiagnoseDateTime = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "IsOldCase": this.str.IsOldCase = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "CreateByUserID": this.str.CreateByUserID = (string)value; break;
						case "CreateDateTime": this.str.CreateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "ExternalCauseID": this.str.ExternalCauseID = (string)value; break;
                        case "DiagnoseSynonym": this.str.DiagnoseSynonym = (string)value; break;
                    }
				}
				else
				{
					switch (name)
					{	
						case "DiagnoseDateTime":
						
							if (value == null || value is System.DateTime)
								this.DiagnoseDateTime = (System.DateTime?)value;
							break;
						case "IsOldCase":
						
							if (value == null || value is System.Boolean)
								this.IsOldCase = (System.Boolean?)value;
							break;
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to RegistrationInfoMedicDiagnose.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationInfoMedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SequenceNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.DiagnoseID
		/// </summary>
		virtual public System.String DiagnoseID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.DiagnosisText
		/// </summary>
		virtual public System.String DiagnosisText
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnosisText);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnosisText, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.SRDiagnoseType
		/// </summary>
		virtual public System.String SRDiagnoseType
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SRDiagnoseType);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SRDiagnoseType, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.DiagnoseDateTime
		/// </summary>
		virtual public System.DateTime? DiagnoseDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.IsOldCase
		/// </summary>
		virtual public System.Boolean? IsOldCase
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsOldCase);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsOldCase, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.CreateByUserID
		/// </summary>
		virtual public System.String CreateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.CreateDateTime
		/// </summary>
		virtual public System.DateTime? CreateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationInfoMedicDiagnose.ExternalCauseID
		/// </summary>
		virtual public System.String ExternalCauseID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ExternalCauseID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ExternalCauseID, value);
			}
        }
        /// <summary>
        /// Maps to RegistrationInfoMedicDiagnose.DiagnoseSynonym
        /// </summary>
        virtual public System.String DiagnoseSynonym
        {
            get
            {
                return base.GetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseSynonym);
            }

            set
            {
                base.SetSystemString(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseSynonym, value);
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
			public esStrings(esRegistrationInfoMedicDiagnose entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationInfoMedicID
			{
				get
				{
					System.String data = entity.RegistrationInfoMedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationInfoMedicID = null;
					else entity.RegistrationInfoMedicID = Convert.ToString(value);
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
			public System.String DiagnoseDateTime
			{
				get
				{
					System.DateTime? data = entity.DiagnoseDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiagnoseDateTime = null;
					else entity.DiagnoseDateTime = Convert.ToDateTime(value);
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
            private esRegistrationInfoMedicDiagnose entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicDiagnoseQuery query)
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
				throw new Exception("esRegistrationInfoMedicDiagnose can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationInfoMedicDiagnose : esRegistrationInfoMedicDiagnose
	{	
	}

	[Serializable]
	abstract public class esRegistrationInfoMedicDiagnoseQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicDiagnoseMetadata.Meta();
			}
		}	
			
		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnoseID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseID, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnosisText
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnosisText, esSystemType.String);
			}
		} 
			
		public esQueryItem SRDiagnoseType
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SRDiagnoseType, esSystemType.String);
			}
		} 
			
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
			
		public esQueryItem DiagnoseDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
			
		public esQueryItem IsOldCase
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsOldCase, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
			
		public esQueryItem CreateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem CreateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
			
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
			
		public esQueryItem ExternalCauseID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ExternalCauseID, esSystemType.String);
			}
        }

        public esQueryItem DiagnoseSynonym
        {
            get
            {
                return new esQueryItem(this, RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseSynonym, esSystemType.String);
            }
        }

    }

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoMedicDiagnoseCollection")]
	public partial class RegistrationInfoMedicDiagnoseCollection : esRegistrationInfoMedicDiagnoseCollection, IEnumerable< RegistrationInfoMedicDiagnose>
	{
		public RegistrationInfoMedicDiagnoseCollection()
		{

		}	
		
		public static implicit operator List< RegistrationInfoMedicDiagnose>(RegistrationInfoMedicDiagnoseCollection coll)
		{
			List< RegistrationInfoMedicDiagnose> list = new List< RegistrationInfoMedicDiagnose>();
			
			foreach (RegistrationInfoMedicDiagnose emp in coll)
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
				return  RegistrationInfoMedicDiagnoseMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicDiagnoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationInfoMedicDiagnose(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationInfoMedicDiagnose();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public RegistrationInfoMedicDiagnoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicDiagnoseQuery();
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
		public bool Load(RegistrationInfoMedicDiagnoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationInfoMedicDiagnose AddNew()
		{
			RegistrationInfoMedicDiagnose entity = base.AddNewEntity() as RegistrationInfoMedicDiagnose;
			
			return entity;		
		}
		public RegistrationInfoMedicDiagnose FindByPrimaryKey(String registrationInfoMedicID, String sequenceNo)
		{
			return base.FindByPrimaryKey(registrationInfoMedicID, sequenceNo) as RegistrationInfoMedicDiagnose;
		}

		#region IEnumerable< RegistrationInfoMedicDiagnose> Members

		IEnumerator< RegistrationInfoMedicDiagnose> IEnumerable< RegistrationInfoMedicDiagnose>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationInfoMedicDiagnose;
			}
		}

		#endregion
		
		private RegistrationInfoMedicDiagnoseQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationInfoMedicDiagnose' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationInfoMedicDiagnose ({RegistrationInfoMedicID, SequenceNo})")]
	[Serializable]
	public partial class RegistrationInfoMedicDiagnose : esRegistrationInfoMedicDiagnose
	{
		public RegistrationInfoMedicDiagnose()
		{
		}	
	
		public RegistrationInfoMedicDiagnose(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicDiagnoseMetadata.Meta();
			}
		}	
	
		override protected esRegistrationInfoMedicDiagnoseQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicDiagnoseQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public RegistrationInfoMedicDiagnoseQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicDiagnoseQuery();
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
		public bool Load(RegistrationInfoMedicDiagnoseQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private RegistrationInfoMedicDiagnoseQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationInfoMedicDiagnoseQuery : esRegistrationInfoMedicDiagnoseQuery
	{
		public RegistrationInfoMedicDiagnoseQuery()
		{

		}		
		
		public RegistrationInfoMedicDiagnoseQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "RegistrationInfoMedicDiagnoseQuery";
        }
	}

	[Serializable]
	public partial class RegistrationInfoMedicDiagnoseMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationInfoMedicDiagnoseMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.RegistrationInfoMedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.DiagnoseID;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnosisText, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.DiagnosisText;
			c.CharacterMaxLength = 1000;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.SRDiagnoseType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.SRDiagnoseType;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.RegistrationNo, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.DiagnoseDateTime;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsOldCase, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.IsOldCase;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.IsVoid, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.IsVoid;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.CreateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.CreateDateTime, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.CreateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateByUserID, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c); 
				
			c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.ExternalCauseID, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.ExternalCauseID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

            c = new esColumnMetadata(RegistrationInfoMedicDiagnoseMetadata.ColumnNames.DiagnoseSynonym, 15, typeof(System.String), esSystemType.String);
            c.PropertyName = RegistrationInfoMedicDiagnoseMetadata.PropertyNames.DiagnoseSynonym;
            c.CharacterMaxLength = 200;
            c.HasDefault = true;
            _columns.Add(c);

        }
		#endregion
	
		static public RegistrationInfoMedicDiagnoseMetadata Meta()
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
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string SequenceNo = "SequenceNo";
			public const string DiagnoseID = "DiagnoseID";
			public const string DiagnosisText = "DiagnosisText";
			public const string SRDiagnoseType = "SRDiagnoseType";
			public const string RegistrationNo = "RegistrationNo";
			public const string DiagnoseDateTime = "DiagnoseDateTime";
			public const string ParamedicID = "ParamedicID";
			public const string IsOldCase = "IsOldCase";
			public const string IsVoid = "IsVoid";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ExternalCauseID = "ExternalCauseID";
            public const string DiagnoseSynonym = "DiagnoseSynonym";
        }
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			public const string SequenceNo = "SequenceNo";
			public const string DiagnoseID = "DiagnoseID";
			public const string DiagnosisText = "DiagnosisText";
			public const string SRDiagnoseType = "SRDiagnoseType";
			public const string RegistrationNo = "RegistrationNo";
			public const string DiagnoseDateTime = "DiagnoseDateTime";
			public const string ParamedicID = "ParamedicID";
			public const string IsOldCase = "IsOldCase";
			public const string IsVoid = "IsVoid";
			public const string CreateByUserID = "CreateByUserID";
			public const string CreateDateTime = "CreateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string ExternalCauseID = "ExternalCauseID";
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
			lock (typeof(RegistrationInfoMedicDiagnoseMetadata))
			{
				if(RegistrationInfoMedicDiagnoseMetadata.mapDelegates == null)
				{
					RegistrationInfoMedicDiagnoseMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationInfoMedicDiagnoseMetadata.meta == null)
				{
					RegistrationInfoMedicDiagnoseMetadata.meta = new RegistrationInfoMedicDiagnoseMetadata();
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
				
				meta.AddTypeMap("RegistrationInfoMedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnosisText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRDiagnoseType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiagnoseDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsOldCase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CreateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ExternalCauseID", new esTypeMap("varchar", "System.String"));
                meta.AddTypeMap("DiagnoseSynonym", new esTypeMap("varchar", "System.String"));


                meta.Source = "RegistrationInfoMedicDiagnose";
				meta.Destination = "RegistrationInfoMedicDiagnose";
				meta.spInsert = "proc_RegistrationInfoMedicDiagnoseInsert";				
				meta.spUpdate = "proc_RegistrationInfoMedicDiagnoseUpdate";		
				meta.spDelete = "proc_RegistrationInfoMedicDiagnoseDelete";
				meta.spLoadAll = "proc_RegistrationInfoMedicDiagnoseLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationInfoMedicDiagnoseLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationInfoMedicDiagnoseMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
