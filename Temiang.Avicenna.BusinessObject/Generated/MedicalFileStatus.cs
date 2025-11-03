/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esMedicalFileStatusCollection : esEntityCollectionWAuditLog
	{
		public esMedicalFileStatusCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "MedicalFileStatusCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalFileStatusQuery query)
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
			this.InitQuery(query as esMedicalFileStatusQuery);
		}
		#endregion
		
		virtual public MedicalFileStatus DetachEntity(MedicalFileStatus entity)
		{
			return base.DetachEntity(entity) as MedicalFileStatus;
		}
		
		virtual public MedicalFileStatus AttachEntity(MedicalFileStatus entity)
		{
			return base.AttachEntity(entity) as MedicalFileStatus;
		}
		
		virtual public void Combine(MedicalFileStatusCollection collection)
		{
			base.Combine(collection);
		}
		
		new public MedicalFileStatus this[int index]
		{
			get
			{
				return base[index] as MedicalFileStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalFileStatus);
		}
	}



	[Serializable]
	abstract public class esMedicalFileStatus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalFileStatusQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalFileStatus()
		{

		}

		public esMedicalFileStatus(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientID);
			else
				return LoadByPrimaryKeyStoredProcedure(patientID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientID)
		{
			esMedicalFileStatusQuery query = this.GetDynamicQuery();
			query.Where(query.PatientID == patientID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientID)
		{
			esParameters parms = new esParameters();
			parms.Add("PatientID",patientID);
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
						case "PatientID": this.str.PatientID = (string)value; break;							
						case "TransactionDate": this.str.TransactionDate = (string)value; break;							
						case "SRMedicalFileStatusCategory": this.str.SRMedicalFileStatusCategory = (string)value; break;							
						case "SRMedicalFileStatus": this.str.SRMedicalFileStatus = (string)value; break;							
						case "Expeditor": this.str.Expeditor = (string)value; break;							
						case "DepartmentID": this.str.DepartmentID = (string)value; break;							
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to MedicalFileStatus.PatientID
		/// </summary>
		virtual public System.String PatientID
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.PatientID);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.PatientID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.TransactionDate
		/// </summary>
		virtual public System.DateTime? TransactionDate
		{
			get
			{
				return base.GetSystemDateTime(MedicalFileStatusMetadata.ColumnNames.TransactionDate);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalFileStatusMetadata.ColumnNames.TransactionDate, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.SRMedicalFileStatusCategory
		/// </summary>
		virtual public System.String SRMedicalFileStatusCategory
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatusCategory);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatusCategory, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.SRMedicalFileStatus
		/// </summary>
		virtual public System.String SRMedicalFileStatus
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatus);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.Expeditor
		/// </summary>
		virtual public System.String Expeditor
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.Expeditor);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.Expeditor, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.DepartmentID
		/// </summary>
		virtual public System.String DepartmentID
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.DepartmentID);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.DepartmentID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalFileStatusMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(MedicalFileStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to MedicalFileStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalFileStatusMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(MedicalFileStatusMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esMedicalFileStatus entity)
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
				
			public System.String SRMedicalFileStatusCategory
			{
				get
				{
					System.String data = entity.SRMedicalFileStatusCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalFileStatusCategory = null;
					else entity.SRMedicalFileStatusCategory = Convert.ToString(value);
				}
			}
				
			public System.String SRMedicalFileStatus
			{
				get
				{
					System.String data = entity.SRMedicalFileStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMedicalFileStatus = null;
					else entity.SRMedicalFileStatus = Convert.ToString(value);
				}
			}
				
			public System.String Expeditor
			{
				get
				{
					System.String data = entity.Expeditor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Expeditor = null;
					else entity.Expeditor = Convert.ToString(value);
				}
			}
				
			public System.String DepartmentID
			{
				get
				{
					System.String data = entity.DepartmentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DepartmentID = null;
					else entity.DepartmentID = Convert.ToString(value);
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
				
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
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
			

			private esMedicalFileStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalFileStatusQuery query)
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
				throw new Exception("esMedicalFileStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class MedicalFileStatus : esMedicalFileStatus
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esMedicalFileStatusQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return MedicalFileStatusMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientID
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.PatientID, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionDate
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.TransactionDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SRMedicalFileStatusCategory
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatusCategory, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMedicalFileStatus
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem Expeditor
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.Expeditor, esSystemType.String);
			}
		} 
		
		public esQueryItem DepartmentID
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.DepartmentID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalFileStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalFileStatusCollection")]
	public partial class MedicalFileStatusCollection : esMedicalFileStatusCollection, IEnumerable<MedicalFileStatus>
	{
		public MedicalFileStatusCollection()
		{

		}
		
		public static implicit operator List<MedicalFileStatus>(MedicalFileStatusCollection coll)
		{
			List<MedicalFileStatus> list = new List<MedicalFileStatus>();
			
			foreach (MedicalFileStatus emp in coll)
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
				return  MedicalFileStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalFileStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalFileStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalFileStatus();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public MedicalFileStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalFileStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(MedicalFileStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public MedicalFileStatus AddNew()
		{
			MedicalFileStatus entity = base.AddNewEntity() as MedicalFileStatus;
			
			return entity;
		}

		public MedicalFileStatus FindByPrimaryKey(System.String patientID)
		{
			return base.FindByPrimaryKey(patientID) as MedicalFileStatus;
		}


		#region IEnumerable<MedicalFileStatus> Members

		IEnumerator<MedicalFileStatus> IEnumerable<MedicalFileStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as MedicalFileStatus;
			}
		}

		#endregion
		
		private MedicalFileStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalFileStatus' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("MedicalFileStatus ({PatientID})")]
	[Serializable]
	public partial class MedicalFileStatus : esMedicalFileStatus
	{
		public MedicalFileStatus()
		{

		}
	
		public MedicalFileStatus(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalFileStatusMetadata.Meta();
			}
		}
		
		
		
		override protected esMedicalFileStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalFileStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public MedicalFileStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalFileStatusQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(MedicalFileStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private MedicalFileStatusQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class MedicalFileStatusQuery : esMedicalFileStatusQuery
	{
		public MedicalFileStatusQuery()
		{

		}		
		
		public MedicalFileStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "MedicalFileStatusQuery";
        }
		
			
	}


	[Serializable]
	public partial class MedicalFileStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalFileStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.PatientID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.PatientID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.TransactionDate, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.TransactionDate;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatusCategory, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.SRMedicalFileStatusCategory;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.SRMedicalFileStatus, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.SRMedicalFileStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.Expeditor, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.Expeditor;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.DepartmentID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.DepartmentID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.ServiceUnitID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.ServiceUnitID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.ParamedicID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.Notes, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(MedicalFileStatusMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalFileStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public MedicalFileStatusMetadata Meta()
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
			 public const string PatientID = "PatientID";
			 public const string TransactionDate = "TransactionDate";
			 public const string SRMedicalFileStatusCategory = "SRMedicalFileStatusCategory";
			 public const string SRMedicalFileStatus = "SRMedicalFileStatus";
			 public const string Expeditor = "Expeditor";
			 public const string DepartmentID = "DepartmentID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string Notes = "Notes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientID = "PatientID";
			 public const string TransactionDate = "TransactionDate";
			 public const string SRMedicalFileStatusCategory = "SRMedicalFileStatusCategory";
			 public const string SRMedicalFileStatus = "SRMedicalFileStatus";
			 public const string Expeditor = "Expeditor";
			 public const string DepartmentID = "DepartmentID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string ParamedicID = "ParamedicID";
			 public const string Notes = "Notes";
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
			lock (typeof(MedicalFileStatusMetadata))
			{
				if(MedicalFileStatusMetadata.mapDelegates == null)
				{
					MedicalFileStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (MedicalFileStatusMetadata.meta == null)
				{
					MedicalFileStatusMetadata.meta = new MedicalFileStatusMetadata();
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
				

				meta.AddTypeMap("PatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SRMedicalFileStatusCategory", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMedicalFileStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Expeditor", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DepartmentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "MedicalFileStatus";
				meta.Destination = "MedicalFileStatus";
				
				meta.spInsert = "proc_MedicalFileStatusInsert";				
				meta.spUpdate = "proc_MedicalFileStatusUpdate";		
				meta.spDelete = "proc_MedicalFileStatusDelete";
				meta.spLoadAll = "proc_MedicalFileStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalFileStatusLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalFileStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
