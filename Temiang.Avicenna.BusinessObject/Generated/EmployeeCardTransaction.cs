/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/31/2021 5:47:59 PM
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
	abstract public class esEmployeeCardTransactionCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeCardTransactionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "EmployeeCardTransactionCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeCardTransactionQuery query)
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
			this.InitQuery(query as esEmployeeCardTransactionQuery);
		}
		#endregion
		
		virtual public EmployeeCardTransaction DetachEntity(EmployeeCardTransaction entity)
		{
			return base.DetachEntity(entity) as EmployeeCardTransaction;
		}
		
		virtual public EmployeeCardTransaction AttachEntity(EmployeeCardTransaction entity)
		{
			return base.AttachEntity(entity) as EmployeeCardTransaction;
		}
		
		virtual public void Combine(EmployeeCardTransactionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public EmployeeCardTransaction this[int index]
		{
			get
			{
				return base[index] as EmployeeCardTransaction;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeCardTransaction);
		}
	}



	[Serializable]
	abstract public class esEmployeeCardTransaction : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeCardTransactionQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeCardTransaction()
		{

		}

		public esEmployeeCardTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 employeeCardTransactionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeCardTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeCardTransactionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 employeeCardTransactionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(employeeCardTransactionID);
			else
				return LoadByPrimaryKeyStoredProcedure(employeeCardTransactionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 employeeCardTransactionID)
		{
			esEmployeeCardTransactionQuery query = this.GetDynamicQuery();
			query.Where(query.EmployeeCardTransactionID == employeeCardTransactionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 employeeCardTransactionID)
		{
			esParameters parms = new esParameters();
			parms.Add("EmployeeCardTransactionID",employeeCardTransactionID);
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
						case "EmployeeCardTransactionID": this.str.EmployeeCardTransactionID = (string)value; break;							
						case "Datetime": this.str.Datetime = (string)value; break;							
						case "PersonID": this.str.PersonID = (string)value; break;							
						case "OldCardID": this.str.OldCardID = (string)value; break;							
						case "NewCardID": this.str.NewCardID = (string)value; break;							
						case "Notes": this.str.Notes = (string)value; break;							
						case "IsApproved": this.str.IsApproved = (string)value; break;							
						case "IsVoid": this.str.IsVoid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateUserID": this.str.LastUpdateUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "EmployeeCardTransactionID":
						
							if (value == null || value is System.Int32)
								this.EmployeeCardTransactionID = (System.Int32?)value;
							break;
						
						case "Datetime":
						
							if (value == null || value is System.DateTime)
								this.Datetime = (System.DateTime?)value;
							break;
						
						case "PersonID":
						
							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						
						case "IsApproved":
						
							if (value == null || value is System.Boolean)
								this.IsApproved = (System.Boolean?)value;
							break;
						
						case "IsVoid":
						
							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
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
		/// Maps to EmployeeCardTransaction.EmployeeCardTransactionID
		/// </summary>
		virtual public System.Int32? EmployeeCardTransactionID
		{
			get
			{
				return base.GetSystemInt32(EmployeeCardTransactionMetadata.ColumnNames.EmployeeCardTransactionID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeCardTransactionMetadata.ColumnNames.EmployeeCardTransactionID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.Datetime
		/// </summary>
		virtual public System.DateTime? Datetime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeCardTransactionMetadata.ColumnNames.Datetime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeCardTransactionMetadata.ColumnNames.Datetime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeCardTransactionMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				base.SetSystemInt32(EmployeeCardTransactionMetadata.ColumnNames.PersonID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.OldCardID
		/// </summary>
		virtual public System.String OldCardID
		{
			get
			{
				return base.GetSystemString(EmployeeCardTransactionMetadata.ColumnNames.OldCardID);
			}
			
			set
			{
				base.SetSystemString(EmployeeCardTransactionMetadata.ColumnNames.OldCardID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.NewCardID
		/// </summary>
		virtual public System.String NewCardID
		{
			get
			{
				return base.GetSystemString(EmployeeCardTransactionMetadata.ColumnNames.NewCardID);
			}
			
			set
			{
				base.SetSystemString(EmployeeCardTransactionMetadata.ColumnNames.NewCardID, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(EmployeeCardTransactionMetadata.ColumnNames.Notes);
			}
			
			set
			{
				base.SetSystemString(EmployeeCardTransactionMetadata.ColumnNames.Notes, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.IsApproved
		/// </summary>
		virtual public System.Boolean? IsApproved
		{
			get
			{
				return base.GetSystemBoolean(EmployeeCardTransactionMetadata.ColumnNames.IsApproved);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeCardTransactionMetadata.ColumnNames.IsApproved, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(EmployeeCardTransactionMetadata.ColumnNames.IsVoid);
			}
			
			set
			{
				base.SetSystemBoolean(EmployeeCardTransactionMetadata.ColumnNames.IsVoid, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to EmployeeCardTransaction.LastUpdateUserID
		/// </summary>
		virtual public System.String LastUpdateUserID
		{
			get
			{
				return base.GetSystemString(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateUserID);
			}
			
			set
			{
				base.SetSystemString(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateUserID, value);
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
			public esStrings(esEmployeeCardTransaction entity)
			{
				this.entity = entity;
			}
			
	
			public System.String EmployeeCardTransactionID
			{
				get
				{
					System.Int32? data = entity.EmployeeCardTransactionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmployeeCardTransactionID = null;
					else entity.EmployeeCardTransactionID = Convert.ToInt32(value);
				}
			}
				
			public System.String Datetime
			{
				get
				{
					System.DateTime? data = entity.Datetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Datetime = null;
					else entity.Datetime = Convert.ToDateTime(value);
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
				
			public System.String OldCardID
			{
				get
				{
					System.String data = entity.OldCardID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OldCardID = null;
					else entity.OldCardID = Convert.ToString(value);
				}
			}
				
			public System.String NewCardID
			{
				get
				{
					System.String data = entity.NewCardID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NewCardID = null;
					else entity.NewCardID = Convert.ToString(value);
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
				
			public System.String IsApproved
			{
				get
				{
					System.Boolean? data = entity.IsApproved;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsApproved = null;
					else entity.IsApproved = Convert.ToBoolean(value);
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
				
			public System.String LastUpdateUserID
			{
				get
				{
					System.String data = entity.LastUpdateUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateUserID = null;
					else entity.LastUpdateUserID = Convert.ToString(value);
				}
			}
			

			private esEmployeeCardTransaction entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeCardTransactionQuery query)
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
				throw new Exception("esEmployeeCardTransaction can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esEmployeeCardTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeCardTransactionMetadata.Meta();
			}
		}	
		

		public esQueryItem EmployeeCardTransactionID
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.EmployeeCardTransactionID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem Datetime
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.Datetime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		} 
		
		public esQueryItem OldCardID
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.OldCardID, esSystemType.String);
			}
		} 
		
		public esQueryItem NewCardID
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.NewCardID, esSystemType.String);
			}
		} 
		
		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.Notes, esSystemType.String);
			}
		} 
		
		public esQueryItem IsApproved
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.IsApproved, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeCardTransactionMetadata.ColumnNames.LastUpdateUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeCardTransactionCollection")]
	public partial class EmployeeCardTransactionCollection : esEmployeeCardTransactionCollection, IEnumerable<EmployeeCardTransaction>
	{
		public EmployeeCardTransactionCollection()
		{

		}
		
		public static implicit operator List<EmployeeCardTransaction>(EmployeeCardTransactionCollection coll)
		{
			List<EmployeeCardTransaction> list = new List<EmployeeCardTransaction>();
			
			foreach (EmployeeCardTransaction emp in coll)
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
				return  EmployeeCardTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeCardTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeCardTransaction(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeCardTransaction();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public EmployeeCardTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeCardTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(EmployeeCardTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public EmployeeCardTransaction AddNew()
		{
			EmployeeCardTransaction entity = base.AddNewEntity() as EmployeeCardTransaction;
			
			return entity;
		}

		public EmployeeCardTransaction FindByPrimaryKey(System.Int32 employeeCardTransactionID)
		{
			return base.FindByPrimaryKey(employeeCardTransactionID) as EmployeeCardTransaction;
		}


		#region IEnumerable<EmployeeCardTransaction> Members

		IEnumerator<EmployeeCardTransaction> IEnumerable<EmployeeCardTransaction>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeCardTransaction;
			}
		}

		#endregion
		
		private EmployeeCardTransactionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeCardTransaction' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("EmployeeCardTransaction ({EmployeeCardTransactionID})")]
	[Serializable]
	public partial class EmployeeCardTransaction : esEmployeeCardTransaction
	{
		public EmployeeCardTransaction()
		{

		}
	
		public EmployeeCardTransaction(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeCardTransactionMetadata.Meta();
			}
		}
		
		
		
		override protected esEmployeeCardTransactionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeCardTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public EmployeeCardTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeCardTransactionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(EmployeeCardTransactionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private EmployeeCardTransactionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class EmployeeCardTransactionQuery : esEmployeeCardTransactionQuery
	{
		public EmployeeCardTransactionQuery()
		{

		}		
		
		public EmployeeCardTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "EmployeeCardTransactionQuery";
        }
		
			
	}


	[Serializable]
	public partial class EmployeeCardTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeCardTransactionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.EmployeeCardTransactionID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.EmployeeCardTransactionID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.Datetime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.Datetime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.PersonID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.OldCardID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.OldCardID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.NewCardID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.NewCardID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.Notes, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.IsApproved, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.IsApproved;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.IsVoid, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(EmployeeCardTransactionMetadata.ColumnNames.LastUpdateUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeCardTransactionMetadata.PropertyNames.LastUpdateUserID;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public EmployeeCardTransactionMetadata Meta()
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
			 public const string EmployeeCardTransactionID = "EmployeeCardTransactionID";
			 public const string Datetime = "Datetime";
			 public const string PersonID = "PersonID";
			 public const string OldCardID = "OldCardID";
			 public const string NewCardID = "NewCardID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string EmployeeCardTransactionID = "EmployeeCardTransactionID";
			 public const string Datetime = "Datetime";
			 public const string PersonID = "PersonID";
			 public const string OldCardID = "OldCardID";
			 public const string NewCardID = "NewCardID";
			 public const string Notes = "Notes";
			 public const string IsApproved = "IsApproved";
			 public const string IsVoid = "IsVoid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateUserID = "LastUpdateUserID";
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
			lock (typeof(EmployeeCardTransactionMetadata))
			{
				if(EmployeeCardTransactionMetadata.mapDelegates == null)
				{
					EmployeeCardTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmployeeCardTransactionMetadata.meta == null)
				{
					EmployeeCardTransactionMetadata.meta = new EmployeeCardTransactionMetadata();
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
				

				meta.AddTypeMap("EmployeeCardTransactionID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Datetime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OldCardID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("NewCardID", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsApproved", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateUserID", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				meta.Source = "EmployeeCardTransaction";
				meta.Destination = "EmployeeCardTransaction";
				
				meta.spInsert = "proc_EmployeeCardTransactionInsert";				
				meta.spUpdate = "proc_EmployeeCardTransactionUpdate";		
				meta.spDelete = "proc_EmployeeCardTransactionDelete";
				meta.spLoadAll = "proc_EmployeeCardTransactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeCardTransactionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeCardTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
