/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:14 PM
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
	abstract public class esDonatorCollection : esEntityCollectionWAuditLog
	{
		public esDonatorCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DonatorCollection";
		}

		#region Query Logic
		protected void InitQuery(esDonatorQuery query)
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
			this.InitQuery(query as esDonatorQuery);
		}
		#endregion
		
		virtual public Donator DetachEntity(Donator entity)
		{
			return base.DetachEntity(entity) as Donator;
		}
		
		virtual public Donator AttachEntity(Donator entity)
		{
			return base.AttachEntity(entity) as Donator;
		}
		
		virtual public void Combine(DonatorCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Donator this[int index]
		{
			get
			{
				return base[index] as Donator;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Donator);
		}
	}



	[Serializable]
	abstract public class esDonator : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDonatorQuery GetDynamicQuery()
		{
			return null;
		}

		public esDonator()
		{

		}

		public esDonator(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String donatorID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(donatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(donatorID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String donatorID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(donatorID);
			else
				return LoadByPrimaryKeyStoredProcedure(donatorID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String donatorID)
		{
			esDonatorQuery query = this.GetDynamicQuery();
			query.Where(query.DonatorID == donatorID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String donatorID)
		{
			esParameters parms = new esParameters();
			parms.Add("DonatorID",donatorID);
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
						case "DonatorID": this.str.DonatorID = (string)value; break;							
						case "DonatorName": this.str.DonatorName = (string)value; break;							
						case "ShortName": this.str.ShortName = (string)value; break;							
						case "StreetName": this.str.StreetName = (string)value; break;							
						case "City": this.str.City = (string)value; break;							
						case "Country": this.str.Country = (string)value; break;							
						case "PhoneNo": this.str.PhoneNo = (string)value; break;							
						case "FaxNo": this.str.FaxNo = (string)value; break;							
						case "ContactPerson": this.str.ContactPerson = (string)value; break;							
						case "MobilePhoneNo": this.str.MobilePhoneNo = (string)value; break;							
						case "Email": this.str.Email = (string)value; break;							
						case "DonatorNotes": this.str.DonatorNotes = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
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
		/// Maps to Donator.DonatorID
		/// </summary>
		virtual public System.String DonatorID
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.DonatorID);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.DonatorID, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.DonatorName
		/// </summary>
		virtual public System.String DonatorName
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.DonatorName);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.DonatorName, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.ShortName
		/// </summary>
		virtual public System.String ShortName
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.ShortName);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.ShortName, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.StreetName
		/// </summary>
		virtual public System.String StreetName
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.StreetName);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.StreetName, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.City
		/// </summary>
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.City);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.City, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.Country
		/// </summary>
		virtual public System.String Country
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.Country);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.Country, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.PhoneNo
		/// </summary>
		virtual public System.String PhoneNo
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.PhoneNo);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.PhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.FaxNo
		/// </summary>
		virtual public System.String FaxNo
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.FaxNo);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.FaxNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.ContactPerson
		/// </summary>
		virtual public System.String ContactPerson
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.ContactPerson);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.ContactPerson, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.MobilePhoneNo
		/// </summary>
		virtual public System.String MobilePhoneNo
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.MobilePhoneNo);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.MobilePhoneNo, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.DonatorNotes
		/// </summary>
		virtual public System.String DonatorNotes
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.DonatorNotes);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.DonatorNotes, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(DonatorMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(DonatorMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to Donator.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(DonatorMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(DonatorMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esDonator entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DonatorID
			{
				get
				{
					System.String data = entity.DonatorID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DonatorID = null;
					else entity.DonatorID = Convert.ToString(value);
				}
			}
				
			public System.String DonatorName
			{
				get
				{
					System.String data = entity.DonatorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DonatorName = null;
					else entity.DonatorName = Convert.ToString(value);
				}
			}
				
			public System.String ShortName
			{
				get
				{
					System.String data = entity.ShortName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShortName = null;
					else entity.ShortName = Convert.ToString(value);
				}
			}
				
			public System.String StreetName
			{
				get
				{
					System.String data = entity.StreetName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StreetName = null;
					else entity.StreetName = Convert.ToString(value);
				}
			}
				
			public System.String City
			{
				get
				{
					System.String data = entity.City;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.City = null;
					else entity.City = Convert.ToString(value);
				}
			}
				
			public System.String Country
			{
				get
				{
					System.String data = entity.Country;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Country = null;
					else entity.Country = Convert.ToString(value);
				}
			}
				
			public System.String PhoneNo
			{
				get
				{
					System.String data = entity.PhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNo = null;
					else entity.PhoneNo = Convert.ToString(value);
				}
			}
				
			public System.String FaxNo
			{
				get
				{
					System.String data = entity.FaxNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNo = null;
					else entity.FaxNo = Convert.ToString(value);
				}
			}
				
			public System.String ContactPerson
			{
				get
				{
					System.String data = entity.ContactPerson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ContactPerson = null;
					else entity.ContactPerson = Convert.ToString(value);
				}
			}
				
			public System.String MobilePhoneNo
			{
				get
				{
					System.String data = entity.MobilePhoneNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobilePhoneNo = null;
					else entity.MobilePhoneNo = Convert.ToString(value);
				}
			}
				
			public System.String Email
			{
				get
				{
					System.String data = entity.Email;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Email = null;
					else entity.Email = Convert.ToString(value);
				}
			}
				
			public System.String DonatorNotes
			{
				get
				{
					System.String data = entity.DonatorNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DonatorNotes = null;
					else entity.DonatorNotes = Convert.ToString(value);
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
			

			private esDonator entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDonatorQuery query)
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
				throw new Exception("esDonator can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class Donator : esDonator
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
	abstract public class esDonatorQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DonatorMetadata.Meta();
			}
		}	
		

		public esQueryItem DonatorID
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.DonatorID, esSystemType.String);
			}
		} 
		
		public esQueryItem DonatorName
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.DonatorName, esSystemType.String);
			}
		} 
		
		public esQueryItem ShortName
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.ShortName, esSystemType.String);
			}
		} 
		
		public esQueryItem StreetName
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.StreetName, esSystemType.String);
			}
		} 
		
		public esQueryItem City
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.City, esSystemType.String);
			}
		} 
		
		public esQueryItem Country
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.Country, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNo
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.PhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem FaxNo
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.FaxNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ContactPerson
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.ContactPerson, esSystemType.String);
			}
		} 
		
		public esQueryItem MobilePhoneNo
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.MobilePhoneNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem DonatorNotes
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.DonatorNotes, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, DonatorMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DonatorCollection")]
	public partial class DonatorCollection : esDonatorCollection, IEnumerable<Donator>
	{
		public DonatorCollection()
		{

		}
		
		public static implicit operator List<Donator>(DonatorCollection coll)
		{
			List<Donator> list = new List<Donator>();
			
			foreach (Donator emp in coll)
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
				return  DonatorMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DonatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Donator(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Donator();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DonatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DonatorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DonatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Donator AddNew()
		{
			Donator entity = base.AddNewEntity() as Donator;
			
			return entity;
		}

		public Donator FindByPrimaryKey(System.String donatorID)
		{
			return base.FindByPrimaryKey(donatorID) as Donator;
		}


		#region IEnumerable<Donator> Members

		IEnumerator<Donator> IEnumerable<Donator>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Donator;
			}
		}

		#endregion
		
		private DonatorQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Donator' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Donator ({DonatorID})")]
	[Serializable]
	public partial class Donator : esDonator
	{
		public Donator()
		{

		}
	
		public Donator(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DonatorMetadata.Meta();
			}
		}
		
		
		
		override protected esDonatorQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DonatorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DonatorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DonatorQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DonatorQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DonatorQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DonatorQuery : esDonatorQuery
	{
		public DonatorQuery()
		{

		}		
		
		public DonatorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DonatorQuery";
        }
		
			
	}


	[Serializable]
	public partial class DonatorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DonatorMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DonatorMetadata.ColumnNames.DonatorID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.DonatorID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.DonatorName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.DonatorName;
			c.CharacterMaxLength = 60;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.ShortName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.ShortName;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.StreetName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.StreetName;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.City, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.City;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.Country, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.Country;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.PhoneNo, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.PhoneNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.FaxNo, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.FaxNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.ContactPerson, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.ContactPerson;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.MobilePhoneNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.MobilePhoneNo;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.Email, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.DonatorNotes, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.DonatorNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.LastUpdateDateTime, 12, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DonatorMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DonatorMetadata.ColumnNames.LastUpdateByUserID, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = DonatorMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DonatorMetadata Meta()
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
			 public const string DonatorID = "DonatorID";
			 public const string DonatorName = "DonatorName";
			 public const string ShortName = "ShortName";
			 public const string StreetName = "StreetName";
			 public const string City = "City";
			 public const string Country = "Country";
			 public const string PhoneNo = "PhoneNo";
			 public const string FaxNo = "FaxNo";
			 public const string ContactPerson = "ContactPerson";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string Email = "Email";
			 public const string DonatorNotes = "DonatorNotes";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DonatorID = "DonatorID";
			 public const string DonatorName = "DonatorName";
			 public const string ShortName = "ShortName";
			 public const string StreetName = "StreetName";
			 public const string City = "City";
			 public const string Country = "Country";
			 public const string PhoneNo = "PhoneNo";
			 public const string FaxNo = "FaxNo";
			 public const string ContactPerson = "ContactPerson";
			 public const string MobilePhoneNo = "MobilePhoneNo";
			 public const string Email = "Email";
			 public const string DonatorNotes = "DonatorNotes";
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
			lock (typeof(DonatorMetadata))
			{
				if(DonatorMetadata.mapDelegates == null)
				{
					DonatorMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DonatorMetadata.meta == null)
				{
					DonatorMetadata.meta = new DonatorMetadata();
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
				

				meta.AddTypeMap("DonatorID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DonatorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShortName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StreetName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Country", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ContactPerson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobilePhoneNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DonatorNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Donator";
				meta.Destination = "Donator";
				
				meta.spInsert = "proc_DonatorInsert";				
				meta.spUpdate = "proc_DonatorUpdate";		
				meta.spDelete = "proc_DonatorDelete";
				meta.spLoadAll = "proc_DonatorLoadAll";
				meta.spLoadByPrimaryKey = "proc_DonatorLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DonatorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
