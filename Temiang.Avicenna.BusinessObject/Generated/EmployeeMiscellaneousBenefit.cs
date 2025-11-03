/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:15 PM
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
	abstract public class esEmployeeMiscellaneousBenefitCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeMiscellaneousBenefitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeMiscellaneousBenefitCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeMiscellaneousBenefitQuery query)
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
			this.InitQuery(query as esEmployeeMiscellaneousBenefitQuery);
		}
		#endregion
		
		virtual public EmployeeMiscellaneousBenefit DetachEntity(EmployeeMiscellaneousBenefit entity)
		{
			return base.DetachEntity(entity) as EmployeeMiscellaneousBenefit;
		}
		
		virtual public EmployeeMiscellaneousBenefit AttachEntity(EmployeeMiscellaneousBenefit entity)
		{
			return base.AttachEntity(entity) as EmployeeMiscellaneousBenefit;
		}
		
		virtual public void Combine(EmployeeMiscellaneousBenefitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeMiscellaneousBenefit this[int index]
		{
			get
			{
				return base[index] as EmployeeMiscellaneousBenefit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeMiscellaneousBenefit);
		}
	}



	[Serializable]
	abstract public class esEmployeeMiscellaneousBenefit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeMiscellaneousBenefitQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeMiscellaneousBenefit()
		{

		}

		public esEmployeeMiscellaneousBenefit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeMiscellaneousBenefitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMiscellaneousBenefitID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMiscellaneousBenefitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeMiscellaneousBenefitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeMiscellaneousBenefitID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeMiscellaneousBenefitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeMiscellaneousBenefitID)
		{
			esEmployeeMiscellaneousBenefitQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeMiscellaneousBenefitID == employeeMiscellaneousBenefitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeMiscellaneousBenefitID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeMiscellaneousBenefitID",employeeMiscellaneousBenefitID);
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
						case "EmployeeMiscellaneousBenefitID": this.str.EmployeeMiscellaneousBenefitID = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "SRMiscellaneousBenefit": this.str.SRMiscellaneousBenefit = (string)value; break;							
						case "ValidFrom": this.str.ValidFrom = (string)value; break;							
						case "ValidTo": this.str.ValidTo = (string)value; break;							
						case "Note": this.str.Note = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeMiscellaneousBenefitID":
						
							if (value == null || value is System.Int32)
								this.EmployeeMiscellaneousBenefitID = (System.Int32?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "ValidFrom":
						
							if (value == null || value is System.DateTime)
								this.ValidFrom = (System.DateTime?)value;
							break;
						
						case "ValidTo":
						
							if (value == null || value is System.DateTime)
								this.ValidTo = (System.DateTime?)value;
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
		/// Maps to EmployeeMiscellaneousBenefit.EmployeeMiscellaneousBenefitID
		/// </summary>
		virtual public System.Int32? EmployeeMiscellaneousBenefitID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeMiscellaneousBenefitMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeMiscellaneousBenefitMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.SRMiscellaneousBenefit
		/// </summary>
		virtual public System.String SRMiscellaneousBenefit
		{
			get
			{
				return base.GetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.SRMiscellaneousBenefit);
			}
			
			set
			{
				base.SetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.SRMiscellaneousBenefit, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.ValidFrom
		/// </summary>
		virtual public System.DateTime? ValidFrom
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidFrom);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidFrom, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.ValidTo
		/// </summary>
		virtual public System.DateTime? ValidTo
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidTo);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidTo, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.Note);
			}
			
			set
			{
				base.SetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.Note, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeMiscellaneousBenefit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeMiscellaneousBenefit entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeMiscellaneousBenefitID
			{
				get
				{
					System.Int32? data = entity.EmployeeMiscellaneousBenefitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeMiscellaneousBenefitID = null;
					else entity.EmployeeMiscellaneousBenefitID = Convert.ToInt32(value);
				}
			}
				
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
				
			public System.String SRMiscellaneousBenefit
			{
				get
				{
					System.String data = entity.SRMiscellaneousBenefit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRMiscellaneousBenefit = null;
					else entity.SRMiscellaneousBenefit = Convert.ToString(value);
				}
			}
				
			public System.String ValidFrom
			{
				get
				{
					System.DateTime? data = entity.ValidFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFrom = null;
					else entity.ValidFrom = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidTo
			{
				get
				{
					System.DateTime? data = entity.ValidTo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidTo = null;
					else entity.ValidTo = Convert.ToDateTime(value);
				}
			}
				
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
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
			

			private esEmployeeMiscellaneousBenefit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeMiscellaneousBenefitQuery query)
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
				throw new Exception("esEmployeeMiscellaneousBenefit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class EmployeeMiscellaneousBenefit : esEmployeeMiscellaneousBenefit
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
	abstract public class esEmployeeMiscellaneousBenefitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMiscellaneousBenefitMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeMiscellaneousBenefitID
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem SRMiscellaneousBenefit
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.SRMiscellaneousBenefit, esSystemType.String);
			}
		} 
		
		public esQueryItem ValidFrom
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidFrom, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem ValidTo
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidTo, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.Note, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeMiscellaneousBenefitCollection")]
	public partial class EmployeeMiscellaneousBenefitCollection : esEmployeeMiscellaneousBenefitCollection, IEnumerable<EmployeeMiscellaneousBenefit>
	{
		public EmployeeMiscellaneousBenefitCollection()
		{

		}
		
		public static implicit operator List<EmployeeMiscellaneousBenefit>(EmployeeMiscellaneousBenefitCollection coll)
		{
			List<EmployeeMiscellaneousBenefit> list = new List<EmployeeMiscellaneousBenefit>();
			
			foreach (EmployeeMiscellaneousBenefit emp in coll)
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
				return  EmployeeMiscellaneousBenefitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMiscellaneousBenefitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeMiscellaneousBenefit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeMiscellaneousBenefit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeMiscellaneousBenefitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMiscellaneousBenefitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeMiscellaneousBenefitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeMiscellaneousBenefit AddNew()
		{
			EmployeeMiscellaneousBenefit entity = base.AddNewEntity() as EmployeeMiscellaneousBenefit;
			
			return entity;
		}

		public EmployeeMiscellaneousBenefit FindByPrimaryKey(System.Int32 employeeMiscellaneousBenefitID)
		{
			return base.FindByPrimaryKey(employeeMiscellaneousBenefitID) as EmployeeMiscellaneousBenefit;
		}


		#region IEnumerable<EmployeeMiscellaneousBenefit> Members

		IEnumerator<EmployeeMiscellaneousBenefit> IEnumerable<EmployeeMiscellaneousBenefit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeMiscellaneousBenefit;
			}
		}

		#endregion
		
		private EmployeeMiscellaneousBenefitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeMiscellaneousBenefit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeMiscellaneousBenefit ({EmployeeMiscellaneousBenefitID})")]
	[Serializable]
	public partial class EmployeeMiscellaneousBenefit : esEmployeeMiscellaneousBenefit
	{
		public EmployeeMiscellaneousBenefit()
		{

		}
	
		public EmployeeMiscellaneousBenefit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeMiscellaneousBenefitMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeMiscellaneousBenefitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeMiscellaneousBenefitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeMiscellaneousBenefitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeMiscellaneousBenefitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeMiscellaneousBenefitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeMiscellaneousBenefitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeMiscellaneousBenefitQuery : esEmployeeMiscellaneousBenefitQuery
	{
		public EmployeeMiscellaneousBenefitQuery()
		{

		}		
		
		public EmployeeMiscellaneousBenefitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeMiscellaneousBenefitQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeMiscellaneousBenefitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeMiscellaneousBenefitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.EmployeeMiscellaneousBenefitID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.EmployeeMiscellaneousBenefitID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.SRMiscellaneousBenefit, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.SRMiscellaneousBenefit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidFrom, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.ValidFrom;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.ValidTo, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.ValidTo;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.Note, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeMiscellaneousBenefitMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeMiscellaneousBenefitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeMiscellaneousBenefitMetadata Meta()
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
			 public const string EmployeeMiscellaneousBenefitID = "EmployeeMiscellaneousBenefitID";
			 public const string PersonID = "PersonID";
			 public const string SRMiscellaneousBenefit = "SRMiscellaneousBenefit";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeMiscellaneousBenefitID = "EmployeeMiscellaneousBenefitID";
			 public const string PersonID = "PersonID";
			 public const string SRMiscellaneousBenefit = "SRMiscellaneousBenefit";
			 public const string ValidFrom = "ValidFrom";
			 public const string ValidTo = "ValidTo";
			 public const string Note = "Note";
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
			lock (typeof(EmployeeMiscellaneousBenefitMetadata))
			{
				if(EmployeeMiscellaneousBenefitMetadata.mapDelegates == null)
				{
					EmployeeMiscellaneousBenefitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeMiscellaneousBenefitMetadata.meta == null)
				{
					EmployeeMiscellaneousBenefitMetadata.meta = new EmployeeMiscellaneousBenefitMetadata();
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
				

				meta.AddTypeMap("EmployeeMiscellaneousBenefitID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRMiscellaneousBenefit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ValidFrom", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidTo", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeMiscellaneousBenefit";
				meta.Destination = "EmployeeMiscellaneousBenefit";
				
				meta.spInsert = "proc_EmployeeMiscellaneousBenefitInsert";				
				meta.spUpdate = "proc_EmployeeMiscellaneousBenefitUpdate";		
				meta.spDelete = "proc_EmployeeMiscellaneousBenefitDelete";
				meta.spLoadAll = "proc_EmployeeMiscellaneousBenefitLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeMiscellaneousBenefitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeMiscellaneousBenefitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
