/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/8/2021 2:37:55 PM
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
	abstract public class esJournalCodesCollection : esEntityCollectionWAuditLog
	{
		public esJournalCodesCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "JournalCodesCollection";
		}

		#region Query Logic
		protected void InitQuery(esJournalCodesQuery query)
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
			this.InitQuery(query as esJournalCodesQuery);
		}
		#endregion
		
		virtual public JournalCodes DetachEntity(JournalCodes entity)
		{
			return base.DetachEntity(entity) as JournalCodes;
		}
		
		virtual public JournalCodes AttachEntity(JournalCodes entity)
		{
			return base.AttachEntity(entity) as JournalCodes;
		}
		
		virtual public void Combine(JournalCodesCollection collection)
		{
			base.Combine(collection);
		}
		
		new public JournalCodes this[int index]
		{
			get
			{
				return base[index] as JournalCodes;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(JournalCodes);
		}
	}



	[Serializable]
	abstract public class esJournalCodes : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esJournalCodesQuery GetDynamicQuery()
		{
			return null;
		}

		public esJournalCodes()
		{

		}

		public esJournalCodes(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 journalCodeId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalCodeId);
			else
				return LoadByPrimaryKeyStoredProcedure(journalCodeId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 journalCodeId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(journalCodeId);
			else
				return LoadByPrimaryKeyStoredProcedure(journalCodeId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 journalCodeId)
		{
			esJournalCodesQuery query = this.GetDynamicQuery();
			query.Where(query.JournalCodeId == journalCodeId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 journalCodeId)
		{
			esParameters parms = new esParameters();
			parms.Add("JournalCodeId",journalCodeId);
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
						case "JournalCodeId": this.str.JournalCodeId = (string)value; break;							
						case "JournalCode": this.str.JournalCode = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "CurrentNumber": this.str.CurrentNumber = (string)value; break;							
						case "NumberFormat": this.str.NumberFormat = (string)value; break;							
						case "NumberSeed": this.str.NumberSeed = (string)value; break;							
						case "IsEnabled": this.str.IsEnabled = (string)value; break;							
						case "IsAutoNumber": this.str.IsAutoNumber = (string)value; break;							
						case "BankID": this.str.BankID = (string)value; break;							
						case "CashType": this.str.CashType = (string)value; break;							
						case "IsVisible": this.str.IsVisible = (string)value; break;							
						case "IsBku": this.str.IsBku = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "JournalCodeId":
						
							if (value == null || value is System.Int32)
								this.JournalCodeId = (System.Int32?)value;
							break;
						
						case "CurrentNumber":
						
							if (value == null || value is System.Int32)
								this.CurrentNumber = (System.Int32?)value;
							break;
						
						case "NumberSeed":
						
							if (value == null || value is System.Int32)
								this.NumberSeed = (System.Int32?)value;
							break;
						
						case "IsEnabled":
						
							if (value == null || value is System.Boolean)
								this.IsEnabled = (System.Boolean?)value;
							break;
						
						case "IsAutoNumber":
						
							if (value == null || value is System.Boolean)
								this.IsAutoNumber = (System.Boolean?)value;
							break;
						
						case "IsVisible":
						
							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
							break;
						
						case "IsBku":
						
							if (value == null || value is System.Boolean)
								this.IsBku = (System.Boolean?)value;
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
		/// Maps to JournalCodes.JournalCodeId
		/// </summary>
		virtual public System.Int32? JournalCodeId
		{
			get
			{
				return base.GetSystemInt32(JournalCodesMetadata.ColumnNames.JournalCodeId);
			}
			
			set
			{
				base.SetSystemInt32(JournalCodesMetadata.ColumnNames.JournalCodeId, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.JournalCode
		/// </summary>
		virtual public System.String JournalCode
		{
			get
			{
				return base.GetSystemString(JournalCodesMetadata.ColumnNames.JournalCode);
			}
			
			set
			{
				base.SetSystemString(JournalCodesMetadata.ColumnNames.JournalCode, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(JournalCodesMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(JournalCodesMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.CurrentNumber
		/// </summary>
		virtual public System.Int32? CurrentNumber
		{
			get
			{
				return base.GetSystemInt32(JournalCodesMetadata.ColumnNames.CurrentNumber);
			}
			
			set
			{
				base.SetSystemInt32(JournalCodesMetadata.ColumnNames.CurrentNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.NumberFormat
		/// </summary>
		virtual public System.String NumberFormat
		{
			get
			{
				return base.GetSystemString(JournalCodesMetadata.ColumnNames.NumberFormat);
			}
			
			set
			{
				base.SetSystemString(JournalCodesMetadata.ColumnNames.NumberFormat, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.NumberSeed
		/// </summary>
		virtual public System.Int32? NumberSeed
		{
			get
			{
				return base.GetSystemInt32(JournalCodesMetadata.ColumnNames.NumberSeed);
			}
			
			set
			{
				base.SetSystemInt32(JournalCodesMetadata.ColumnNames.NumberSeed, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.IsEnabled
		/// </summary>
		virtual public System.Boolean? IsEnabled
		{
			get
			{
				return base.GetSystemBoolean(JournalCodesMetadata.ColumnNames.IsEnabled);
			}
			
			set
			{
				base.SetSystemBoolean(JournalCodesMetadata.ColumnNames.IsEnabled, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.IsAutoNumber
		/// </summary>
		virtual public System.Boolean? IsAutoNumber
		{
			get
			{
				return base.GetSystemBoolean(JournalCodesMetadata.ColumnNames.IsAutoNumber);
			}
			
			set
			{
				base.SetSystemBoolean(JournalCodesMetadata.ColumnNames.IsAutoNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.BankID
		/// </summary>
		virtual public System.String BankID
		{
			get
			{
				return base.GetSystemString(JournalCodesMetadata.ColumnNames.BankID);
			}
			
			set
			{
				base.SetSystemString(JournalCodesMetadata.ColumnNames.BankID, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.CashType
		/// </summary>
		virtual public System.String CashType
		{
			get
			{
				return base.GetSystemString(JournalCodesMetadata.ColumnNames.CashType);
			}
			
			set
			{
				base.SetSystemString(JournalCodesMetadata.ColumnNames.CashType, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(JournalCodesMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				base.SetSystemBoolean(JournalCodesMetadata.ColumnNames.IsVisible, value);
			}
		}
		
		/// <summary>
		/// Maps to JournalCodes.IsBku
		/// </summary>
		virtual public System.Boolean? IsBku
		{
			get
			{
				return base.GetSystemBoolean(JournalCodesMetadata.ColumnNames.IsBku);
			}
			
			set
			{
				base.SetSystemBoolean(JournalCodesMetadata.ColumnNames.IsBku, value);
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
			public esStrings(esJournalCodes entity)
			{
				this.entity = entity;
			}
			
	
			public System.String JournalCodeId
			{
				get
				{
					System.Int32? data = entity.JournalCodeId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalCodeId = null;
					else entity.JournalCodeId = Convert.ToInt32(value);
				}
			}
				
			public System.String JournalCode
			{
				get
				{
					System.String data = entity.JournalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JournalCode = null;
					else entity.JournalCode = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String CurrentNumber
			{
				get
				{
					System.Int32? data = entity.CurrentNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CurrentNumber = null;
					else entity.CurrentNumber = Convert.ToInt32(value);
				}
			}
				
			public System.String NumberFormat
			{
				get
				{
					System.String data = entity.NumberFormat;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberFormat = null;
					else entity.NumberFormat = Convert.ToString(value);
				}
			}
				
			public System.String NumberSeed
			{
				get
				{
					System.Int32? data = entity.NumberSeed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumberSeed = null;
					else entity.NumberSeed = Convert.ToInt32(value);
				}
			}
				
			public System.String IsEnabled
			{
				get
				{
					System.Boolean? data = entity.IsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEnabled = null;
					else entity.IsEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAutoNumber
			{
				get
				{
					System.Boolean? data = entity.IsAutoNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAutoNumber = null;
					else entity.IsAutoNumber = Convert.ToBoolean(value);
				}
			}
				
			public System.String BankID
			{
				get
				{
					System.String data = entity.BankID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BankID = null;
					else entity.BankID = Convert.ToString(value);
				}
			}
				
			public System.String CashType
			{
				get
				{
					System.String data = entity.CashType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CashType = null;
					else entity.CashType = Convert.ToString(value);
				}
			}
				
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsBku
			{
				get
				{
					System.Boolean? data = entity.IsBku;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBku = null;
					else entity.IsBku = Convert.ToBoolean(value);
				}
			}
			

			private esJournalCodes entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esJournalCodesQuery query)
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
				throw new Exception("esJournalCodes can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esJournalCodesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return JournalCodesMetadata.Meta();
			}
		}	
		

		public esQueryItem JournalCodeId
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.JournalCodeId, esSystemType.Int32);
			}
		} 
		
		public esQueryItem JournalCode
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.JournalCode, esSystemType.String);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem CurrentNumber
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.CurrentNumber, esSystemType.Int32);
			}
		} 
		
		public esQueryItem NumberFormat
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.NumberFormat, esSystemType.String);
			}
		} 
		
		public esQueryItem NumberSeed
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.NumberSeed, esSystemType.Int32);
			}
		} 
		
		public esQueryItem IsEnabled
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.IsEnabled, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAutoNumber
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.IsAutoNumber, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem BankID
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.BankID, esSystemType.String);
			}
		} 
		
		public esQueryItem CashType
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.CashType, esSystemType.String);
			}
		} 
		
		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBku
		{
			get
			{
				return new esQueryItem(this, JournalCodesMetadata.ColumnNames.IsBku, esSystemType.Boolean);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("JournalCodesCollection")]
	public partial class JournalCodesCollection : esJournalCodesCollection, IEnumerable<JournalCodes>
	{
		public JournalCodesCollection()
		{

		}
		
		public static implicit operator List<JournalCodes>(JournalCodesCollection coll)
		{
			List<JournalCodes> list = new List<JournalCodes>();
			
			foreach (JournalCodes emp in coll)
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
				return  JournalCodesMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalCodesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new JournalCodes(row);
		}

		override protected esEntity CreateEntity()
		{
			return new JournalCodes();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public JournalCodesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalCodesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(JournalCodesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public JournalCodes AddNew()
		{
			JournalCodes entity = base.AddNewEntity() as JournalCodes;
			
			return entity;
		}

		public JournalCodes FindByPrimaryKey(System.Int32 journalCodeId)
		{
			return base.FindByPrimaryKey(journalCodeId) as JournalCodes;
		}


		#region IEnumerable<JournalCodes> Members

		IEnumerator<JournalCodes> IEnumerable<JournalCodes>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as JournalCodes;
			}
		}

		#endregion
		
		private JournalCodesQuery query;
	}


	/// <summary>
	/// Encapsulates the 'JournalCodes' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("JournalCodes ({JournalCodeId})")]
	[Serializable]
	public partial class JournalCodes : esJournalCodes
	{
		public JournalCodes()
		{

		}
	
		public JournalCodes(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return JournalCodesMetadata.Meta();
			}
		}
		
		
		
		override protected esJournalCodesQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new JournalCodesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public JournalCodesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new JournalCodesQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(JournalCodesQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private JournalCodesQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class JournalCodesQuery : esJournalCodesQuery
	{
		public JournalCodesQuery()
		{

		}		
		
		public JournalCodesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "JournalCodesQuery";
        }
		
			
	}


	[Serializable]
	public partial class JournalCodesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected JournalCodesMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.JournalCodeId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalCodesMetadata.PropertyNames.JournalCodeId;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.JournalCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalCodesMetadata.PropertyNames.JournalCode;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalCodesMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 150;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.CurrentNumber, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalCodesMetadata.PropertyNames.CurrentNumber;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.NumberFormat, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalCodesMetadata.PropertyNames.NumberFormat;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.NumberSeed, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = JournalCodesMetadata.PropertyNames.NumberSeed;
			c.NumericPrecision = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.IsEnabled, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalCodesMetadata.PropertyNames.IsEnabled;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.IsAutoNumber, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalCodesMetadata.PropertyNames.IsAutoNumber;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.BankID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalCodesMetadata.PropertyNames.BankID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.CashType, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = JournalCodesMetadata.PropertyNames.CashType;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.IsVisible, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalCodesMetadata.PropertyNames.IsVisible;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(JournalCodesMetadata.ColumnNames.IsBku, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = JournalCodesMetadata.PropertyNames.IsBku;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public JournalCodesMetadata Meta()
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
			 public const string JournalCodeId = "JournalCodeId";
			 public const string JournalCode = "JournalCode";
			 public const string Description = "Description";
			 public const string CurrentNumber = "CurrentNumber";
			 public const string NumberFormat = "NumberFormat";
			 public const string NumberSeed = "NumberSeed";
			 public const string IsEnabled = "IsEnabled";
			 public const string IsAutoNumber = "IsAutoNumber";
			 public const string BankID = "BankID";
			 public const string CashType = "CashType";
			 public const string IsVisible = "IsVisible";
			 public const string IsBku = "IsBku";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string JournalCodeId = "JournalCodeId";
			 public const string JournalCode = "JournalCode";
			 public const string Description = "Description";
			 public const string CurrentNumber = "CurrentNumber";
			 public const string NumberFormat = "NumberFormat";
			 public const string NumberSeed = "NumberSeed";
			 public const string IsEnabled = "IsEnabled";
			 public const string IsAutoNumber = "IsAutoNumber";
			 public const string BankID = "BankID";
			 public const string CashType = "CashType";
			 public const string IsVisible = "IsVisible";
			 public const string IsBku = "IsBku";
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
			lock (typeof(JournalCodesMetadata))
			{
				if(JournalCodesMetadata.mapDelegates == null)
				{
					JournalCodesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (JournalCodesMetadata.meta == null)
				{
					JournalCodesMetadata.meta = new JournalCodesMetadata();
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
				

				meta.AddTypeMap("JournalCodeId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("JournalCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CurrentNumber", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NumberFormat", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("NumberSeed", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("IsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAutoNumber", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BankID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CashType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBku", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				meta.Source = "JournalCodes";
				meta.Destination = "JournalCodes";
				
				meta.spInsert = "proc_JournalCodesInsert";				
				meta.spUpdate = "proc_JournalCodesUpdate";		
				meta.spDelete = "proc_JournalCodesDelete";
				meta.spLoadAll = "proc_JournalCodesLoadAll";
				meta.spLoadByPrimaryKey = "proc_JournalCodesLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private JournalCodesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
