/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:09 PM
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
	abstract public class esApplicantFamilyCollection : esEntityCollectionWAuditLog
	{
		public esApplicantFamilyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ApplicantFamilyCollection";
		}

		#region Query Logic
		protected void InitQuery(esApplicantFamilyQuery query)
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
			this.InitQuery(query as esApplicantFamilyQuery);
		}
		#endregion
		
		virtual public ApplicantFamily DetachEntity(ApplicantFamily entity)
		{
			return base.DetachEntity(entity) as ApplicantFamily;
		}
		
		virtual public ApplicantFamily AttachEntity(ApplicantFamily entity)
		{
			return base.AttachEntity(entity) as ApplicantFamily;
		}
		
		virtual public void Combine(ApplicantFamilyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ApplicantFamily this[int index]
		{
			get
			{
				return base[index] as ApplicantFamily;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ApplicantFamily);
		}
	}



	[Serializable]
	abstract public class esApplicantFamily : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esApplicantFamilyQuery GetDynamicQuery()
		{
			return null;
		}

		public esApplicantFamily()
		{

		}

		public esApplicantFamily(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 applicantFamilyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantFamilyID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantFamilyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 applicantFamilyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicantFamilyID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicantFamilyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 applicantFamilyID)
		{
			esApplicantFamilyQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicantFamilyID == applicantFamilyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 applicantFamilyID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicantFamilyID",applicantFamilyID);
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
						case "ApplicantFamilyID": this.str.ApplicantFamilyID = (string)value; break;							
						case "ApplicantID": this.str.ApplicantID = (string)value; break;							
						case "SRFamilyRelation": this.str.SRFamilyRelation = (string)value; break;							
						case "FamilyName": this.str.FamilyName = (string)value; break;							
						case "DateBirth": this.str.DateBirth = (string)value; break;							
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;							
						case "Address": this.str.Address = (string)value; break;							
						case "SRMaritalStatus": this.str.SRMaritalStatus = (string)value; break;							
						case "SRGenderType": this.str.SRGenderType = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ApplicantFamilyID":
						
							if (value == null || value is System.Int32)
								this.ApplicantFamilyID = (System.Int32?)value;
							break;
						
						case "ApplicantID":
						
							if (value == null || value is System.Int32)
								this.ApplicantID = (System.Int32?)value;
							break;
						
						case "DateBirth":
						
							if (value == null || value is System.DateTime)
								this.DateBirth = (System.DateTime?)value;
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
		/// Maps to ApplicantFamily.ApplicantFamilyID
		/// </summary>
		virtual public System.Int32? ApplicantFamilyID
		{
			get
			{
				return base.GetSystemInt32(ApplicantFamilyMetadata.ColumnNames.ApplicantFamilyID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantFamilyMetadata.ColumnNames.ApplicantFamilyID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.ApplicantID
		/// </summary>
		virtual public System.Int32? ApplicantID
		{
			get
			{
				return base.GetSystemInt32(ApplicantFamilyMetadata.ColumnNames.ApplicantID);
			}
			
			set
			{
				base.SetSystemInt32(ApplicantFamilyMetadata.ColumnNames.ApplicantID, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.SRFamilyRelation
		/// </summary>
		virtual public System.String SRFamilyRelation
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.SRFamilyRelation);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.SRFamilyRelation, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.FamilyName
		/// </summary>
		virtual public System.String FamilyName
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.FamilyName);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.FamilyName, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.DateBirth
		/// </summary>
		virtual public System.DateTime? DateBirth
		{
			get
			{
				return base.GetSystemDateTime(ApplicantFamilyMetadata.ColumnNames.DateBirth);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantFamilyMetadata.ColumnNames.DateBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.SREducationLevel);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.Address
		/// </summary>
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.Address);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.Address, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.SRMaritalStatus
		/// </summary>
		virtual public System.String SRMaritalStatus
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.SRMaritalStatus);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.SRMaritalStatus, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.SRGenderType
		/// </summary>
		virtual public System.String SRGenderType
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.SRGenderType);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.SRGenderType, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ApplicantFamilyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ApplicantFamilyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ApplicantFamily.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ApplicantFamilyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ApplicantFamilyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esApplicantFamily entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ApplicantFamilyID
			{
				get
				{
					System.Int32? data = entity.ApplicantFamilyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicantFamilyID = null;
					else entity.ApplicantFamilyID = Convert.ToInt32(value);
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
				
			public System.String SRFamilyRelation
			{
				get
				{
					System.String data = entity.SRFamilyRelation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRFamilyRelation = null;
					else entity.SRFamilyRelation = Convert.ToString(value);
				}
			}
				
			public System.String FamilyName
			{
				get
				{
					System.String data = entity.FamilyName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FamilyName = null;
					else entity.FamilyName = Convert.ToString(value);
				}
			}
				
			public System.String DateBirth
			{
				get
				{
					System.DateTime? data = entity.DateBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateBirth = null;
					else entity.DateBirth = Convert.ToDateTime(value);
				}
			}
				
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
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
				
			public System.String SRMaritalStatus
			{
				get
				{
					System.String data = entity.SRMaritalStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMaritalStatus = null;
					else entity.SRMaritalStatus = Convert.ToString(value);
				}
			}
				
			public System.String SRGenderType
			{
				get
				{
					System.String data = entity.SRGenderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRGenderType = null;
					else entity.SRGenderType = Convert.ToString(value);
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
			

			private esApplicantFamily entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esApplicantFamilyQuery query)
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
				throw new Exception("esApplicantFamily can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ApplicantFamily : esApplicantFamily
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
	abstract public class esApplicantFamilyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantFamilyMetadata.Meta();
			}
		}	
		

		public esQueryItem ApplicantFamilyID
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.ApplicantFamilyID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ApplicantID
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.ApplicantID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRFamilyRelation
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.SRFamilyRelation, esSystemType.String);
			}
		} 
		
		public esQueryItem FamilyName
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.FamilyName, esSystemType.String);
			}
		} 
		
		public esQueryItem DateBirth
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.DateBirth, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		} 
		
		public esQueryItem Address
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.Address, esSystemType.String);
			}
		} 
		
		public esQueryItem SRMaritalStatus
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.SRMaritalStatus, esSystemType.String);
			}
		} 
		
		public esQueryItem SRGenderType
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.SRGenderType, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ApplicantFamilyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ApplicantFamilyCollection")]
	public partial class ApplicantFamilyCollection : esApplicantFamilyCollection, IEnumerable<ApplicantFamily>
	{
		public ApplicantFamilyCollection()
		{

		}
		
		public static implicit operator List<ApplicantFamily>(ApplicantFamilyCollection coll)
		{
			List<ApplicantFamily> list = new List<ApplicantFamily>();
			
			foreach (ApplicantFamily emp in coll)
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
				return  ApplicantFamilyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantFamilyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ApplicantFamily(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ApplicantFamily();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ApplicantFamilyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantFamilyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ApplicantFamilyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ApplicantFamily AddNew()
		{
			ApplicantFamily entity = base.AddNewEntity() as ApplicantFamily;
			
			return entity;
		}

		public ApplicantFamily FindByPrimaryKey(System.Int32 applicantFamilyID)
		{
			return base.FindByPrimaryKey(applicantFamilyID) as ApplicantFamily;
		}


		#region IEnumerable<ApplicantFamily> Members

		IEnumerator<ApplicantFamily> IEnumerable<ApplicantFamily>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ApplicantFamily;
			}
		}

		#endregion
		
		private ApplicantFamilyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ApplicantFamily' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ApplicantFamily ({ApplicantFamilyID})")]
	[Serializable]
	public partial class ApplicantFamily : esApplicantFamily
	{
		public ApplicantFamily()
		{

		}
	
		public ApplicantFamily(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ApplicantFamilyMetadata.Meta();
			}
		}
		
		
		
		override protected esApplicantFamilyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ApplicantFamilyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ApplicantFamilyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ApplicantFamilyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ApplicantFamilyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ApplicantFamilyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ApplicantFamilyQuery : esApplicantFamilyQuery
	{
		public ApplicantFamilyQuery()
		{

		}		
		
		public ApplicantFamilyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ApplicantFamilyQuery";
        }
		
			
	}


	[Serializable]
	public partial class ApplicantFamilyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ApplicantFamilyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.ApplicantFamilyID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.ApplicantFamilyID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.ApplicantID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.ApplicantID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.SRFamilyRelation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.SRFamilyRelation;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.FamilyName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.FamilyName;
			c.CharacterMaxLength = 100;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.DateBirth, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.DateBirth;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.SREducationLevel, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.Address, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 400;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.SRMaritalStatus, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.SRMaritalStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.SRGenderType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.SRGenderType;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ApplicantFamilyMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ApplicantFamilyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ApplicantFamilyMetadata Meta()
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
			 public const string ApplicantFamilyID = "ApplicantFamilyID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRFamilyRelation = "SRFamilyRelation";
			 public const string FamilyName = "FamilyName";
			 public const string DateBirth = "DateBirth";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string Address = "Address";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string SRGenderType = "SRGenderType";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ApplicantFamilyID = "ApplicantFamilyID";
			 public const string ApplicantID = "ApplicantID";
			 public const string SRFamilyRelation = "SRFamilyRelation";
			 public const string FamilyName = "FamilyName";
			 public const string DateBirth = "DateBirth";
			 public const string SREducationLevel = "SREducationLevel";
			 public const string Address = "Address";
			 public const string SRMaritalStatus = "SRMaritalStatus";
			 public const string SRGenderType = "SRGenderType";
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
			lock (typeof(ApplicantFamilyMetadata))
			{
				if(ApplicantFamilyMetadata.mapDelegates == null)
				{
					ApplicantFamilyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ApplicantFamilyMetadata.meta == null)
				{
					ApplicantFamilyMetadata.meta = new ApplicantFamilyMetadata();
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
				

				meta.AddTypeMap("ApplicantFamilyID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ApplicantID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRFamilyRelation", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FamilyName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateBirth", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Address", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRMaritalStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRGenderType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ApplicantFamily";
				meta.Destination = "ApplicantFamily";
				
				meta.spInsert = "proc_ApplicantFamilyInsert";				
				meta.spUpdate = "proc_ApplicantFamilyUpdate";		
				meta.spDelete = "proc_ApplicantFamilyDelete";
				meta.spLoadAll = "proc_ApplicantFamilyLoadAll";
				meta.spLoadByPrimaryKey = "proc_ApplicantFamilyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ApplicantFamilyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
