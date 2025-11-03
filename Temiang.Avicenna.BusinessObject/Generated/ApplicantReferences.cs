/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:10 PM
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
	abstract public class esApplicantReferencesCollection : esEntityCollectionWAuditLog
	{
		public esApplicantReferencesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantReferencesCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantReferencesQuery query)
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
			this.InitQuery(query as esApplicantReferencesQuery);
		}
		#endregion
		
		virtual public ApplicantReferences DetachEntity(ApplicantReferences entity)
		{
			return base.DetachEntity(entity) as ApplicantReferences;
		}
		
		virtual public ApplicantReferences AttachEntity(ApplicantReferences entity)
		{
			return base.AttachEntity(entity) as ApplicantReferences;
		}
		
		virtual public void Combine(ApplicantReferencesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantReferences this[int index]
		{
			get
			{
				return base[index] as ApplicantReferences;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantReferences);
		}
	}



	[Serializable]
	abstract public class esApplicantReferences : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantReferencesQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantReferences()
		{

		}

		public esApplicantReferences(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantReferencesID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantReferencesID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantReferencesID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantReferencesID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantReferencesID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantReferencesID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantReferencesID)
		{
			esApplicantReferencesQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantReferencesID == applicantReferencesID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantReferencesID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantReferencesID",applicantReferencesID);
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
						case "ApplicantReferencesID": this.str.ApplicantReferencesID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "ReferencesName": this.str.ReferencesName = (string)value; break;							
						case "Relationship": this.str.Relationship = (string)value; break;							
						case "OrganizationName": this.str.OrganizationName = (string)value; break;							
						case "JobTitle": this.str.JobTitle = (string)value; break;							
						case "Address": this.str.Address = (string)value; break;							
						case "Phone": this.str.Phone = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantReferencesID":
						
							if (value == null || value is System.Int32)
								this.ApplicantReferencesID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
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
		/// Maps to ApplicantReferences.ApplicantReferencesID
		/// </summary>
		virtual public System.Int32? ApplicantReferencesID
		{
			get
			{
				return base.GetSystemInt32(ApplicantReferencesMetadata.ColumnNames.ApplicantReferencesID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantReferencesMetadata.ColumnNames.ApplicantReferencesID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantReferencesMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantReferencesMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.ReferencesName
		/// </summary>
		virtual public System.String ReferencesName
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.ReferencesName);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.ReferencesName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.Relationship
		/// </summary>
		virtual public System.String Relationship
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.Relationship);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.Relationship, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.OrganizationName
		/// </summary>
		virtual public System.String OrganizationName
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.OrganizationName);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.OrganizationName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.JobTitle
		/// </summary>
		virtual public System.String JobTitle
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.JobTitle);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.JobTitle, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.Address);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.Address, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.Phone
		/// </summary>
		virtual public System.String Phone
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.Phone);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.Phone, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantReferencesMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantReferencesMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantReferences.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantReferencesMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantReferencesMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantReferences entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantReferencesID
			{
				get
				{
					System.Int32? data = entity.ApplicantReferencesID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantReferencesID = null;
					else entity.ApplicantReferencesID = Convert.ToInt32(value);
				}
			}
				
			public System.String ApplicantID
			{
				get
				{
					System.Int32? data = entity.ApplicantID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantID = null;
					else entity.ApplicantID = Convert.ToInt32(value);
				}
			}
				
			public System.String ReferencesName
			{
				get
				{
					System.String data = entity.ReferencesName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferencesName = null;
					else entity.ReferencesName = Convert.ToString(value);
				}
			}
				
			public System.String Relationship
			{
				get
				{
					System.String data = entity.Relationship;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Relationship = null;
					else entity.Relationship = Convert.ToString(value);
				}
			}
				
			public System.String OrganizationName
			{
				get
				{
					System.String data = entity.OrganizationName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrganizationName = null;
					else entity.OrganizationName = Convert.ToString(value);
				}
			}
				
			public System.String JobTitle
			{
				get
				{
					System.String data = entity.JobTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobTitle = null;
					else entity.JobTitle = Convert.ToString(value);
				}
			}
				
			public System.String Address
			{
				get
				{
					System.String data = entity.Address;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Address = null;
					else entity.Address = Convert.ToString(value);
				}
			}
				
			public System.String Phone
			{
				get
				{
					System.String data = entity.Phone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Phone = null;
					else entity.Phone = Convert.ToString(value);
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
			

			private esApplicantReferences entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantReferencesQuery query)
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
				throw new Exception("esApplicantReferences can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantReferences : esApplicantReferences
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
	abstract public class esApplicantReferencesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantReferencesMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantReferencesID
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.ApplicantReferencesID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ReferencesName
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.ReferencesName, esSystemType.String);
			}
		} 
		
		public esQueryItem Relationship
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.Relationship, esSystemType.String);
			}
		} 
		
		public esQueryItem OrganizationName
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.OrganizationName, esSystemType.String);
			}
		} 
		
		public esQueryItem JobTitle
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.JobTitle, esSystemType.String);
			}
		} 
		
		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.Address, esSystemType.String);
			}
		} 
		
		public esQueryItem Phone
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.Phone, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantReferencesMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantReferencesCollection")]
	public partial class ApplicantReferencesCollection : esApplicantReferencesCollection, IEnumerable<ApplicantReferences>
	{
		public ApplicantReferencesCollection()
		{

		}
		
		public static implicit operator List<ApplicantReferences>(ApplicantReferencesCollection coll)
		{
			List<ApplicantReferences> list = new List<ApplicantReferences>();
			
			foreach (ApplicantReferences emp in coll)
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
				return  ApplicantReferencesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantReferencesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantReferences(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantReferences();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantReferencesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantReferencesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantReferencesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantReferences AddNew()
		{
			ApplicantReferences entity = base.AddNewEntity() as ApplicantReferences;
			
			return entity;
		}

		public ApplicantReferences FindByPrimaryKey(System.Int32 applicantReferencesID)
		{
			return base.FindByPrimaryKey(applicantReferencesID) as ApplicantReferences;
		}


		#region IEnumerable<ApplicantReferences> Members

		IEnumerator<ApplicantReferences> IEnumerable<ApplicantReferences>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantReferences;
			}
		}

		#endregion
		
		private ApplicantReferencesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantReferences' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantReferences ({ApplicantReferencesID})")]
	[Serializable]
	public partial class ApplicantReferences : esApplicantReferences
	{
		public ApplicantReferences()
		{

		}
	
		public ApplicantReferences(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantReferencesMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantReferencesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantReferencesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantReferencesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantReferencesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantReferencesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantReferencesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantReferencesQuery : esApplicantReferencesQuery
	{
		public ApplicantReferencesQuery()
		{

		}		
		
		public ApplicantReferencesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantReferencesQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantReferencesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantReferencesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.ApplicantReferencesID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.ApplicantReferencesID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.ReferencesName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.ReferencesName;
			c.CharacterMaxLength = 200;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.Relationship, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.Relationship;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.OrganizationName, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.OrganizationName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.JobTitle, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.JobTitle;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.Address, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 4000;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.Phone, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.Phone;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantReferencesMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantReferencesMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantReferencesMetadata Meta()
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
			 public const string ApplicantReferencesID = "ApplicantReferencesID";
			 public const string ApplicantID = "ApplicantID";
			 public const string ReferencesName = "ReferencesName";
			 public const string Relationship = "Relationship";
			 public const string OrganizationName = "OrganizationName";
			 public const string JobTitle = "JobTitle";
			 public const string Address = "Address";
			 public const string Phone = "Phone";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantReferencesID = "ApplicantReferencesID";
			 public const string ApplicantID = "ApplicantID";
			 public const string ReferencesName = "ReferencesName";
			 public const string Relationship = "Relationship";
			 public const string OrganizationName = "OrganizationName";
			 public const string JobTitle = "JobTitle";
			 public const string Address = "Address";
			 public const string Phone = "Phone";
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
			lock (typeof(ApplicantReferencesMetadata))
			{
				if(ApplicantReferencesMetadata.mapDelegates == null)
				{
					ApplicantReferencesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantReferencesMetadata.meta == null)
				{
					ApplicantReferencesMetadata.meta = new ApplicantReferencesMetadata();
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
				

				meta.AddTypeMap("ApplicantReferencesID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ReferencesName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Relationship", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrganizationName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Phone", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantReferences";
				meta.Destination = "ApplicantReferences";
				
				meta.spInsert = "proc_ApplicantReferencesInsert";				
				meta.spUpdate = "proc_ApplicantReferencesUpdate";		
				meta.spDelete = "proc_ApplicantReferencesDelete";
				meta.spLoadAll = "proc_ApplicantReferencesLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantReferencesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantReferencesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
