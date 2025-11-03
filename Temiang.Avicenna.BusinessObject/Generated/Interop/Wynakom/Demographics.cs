/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/4/2021 3:01:11 PM
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



namespace Temiang.Avicenna.BusinessObject.Interop.Wynakom
{

	[Serializable]
	abstract public class esDemographicsCollection : esEntityCollectionWAuditLog
	{
		public esDemographicsCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "DemographicsCollection";
		}

		#region Query Logic
		protected void InitQuery(esDemographicsQuery query)
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
			this.InitQuery(query as esDemographicsQuery);
		}
		#endregion
		
		virtual public Demographics DetachEntity(Demographics entity)
		{
			return base.DetachEntity(entity) as Demographics;
		}
		
		virtual public Demographics AttachEntity(Demographics entity)
		{
			return base.AttachEntity(entity) as Demographics;
		}
		
		virtual public void Combine(DemographicsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public Demographics this[int index]
		{
			get
			{
				return base[index] as Demographics;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(Demographics);
		}
	}



	[Serializable]
	abstract public class esDemographics : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esDemographicsQuery GetDynamicQuery()
		{
			return null;
		}

		public esDemographics()
		{

		}

		public esDemographics(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String patientId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientId);
			else
				return LoadByPrimaryKeyStoredProcedure(patientId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String patientId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(patientId);
			else
				return LoadByPrimaryKeyStoredProcedure(patientId);
		}

		private bool LoadByPrimaryKeyDynamic(System.String patientId)
		{
			esDemographicsQuery query = this.GetDynamicQuery();
			query.Where(query.PatientId == patientId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String patientId)
		{
			esParameters parms = new esParameters();
			parms.Add("Patient_Id",patientId);
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
						case "PatientId": this.str.PatientId = (string)value; break;							
						case "GenderId": this.str.GenderId = (string)value; break;							
						case "DateOfBirth": this.str.DateOfBirth = (string)value; break;							
						case "PatientName": this.str.PatientName = (string)value; break;							
						case "PatientAddress": this.str.PatientAddress = (string)value; break;							
						case "CityName": this.str.CityName = (string)value; break;							
						case "PhoneNumber": this.str.PhoneNumber = (string)value; break;							
						case "FaxNumber": this.str.FaxNumber = (string)value; break;							
						case "MobileNumber": this.str.MobileNumber = (string)value; break;							
						case "Email": this.str.Email = (string)value; break;							
						case "KtpNumber": this.str.KtpNumber = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "DateOfBirth":
						
							if (value == null || value is System.DateTime)
								this.DateOfBirth = (System.DateTime?)value;
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
		/// Maps to Demographics.Patient_Id
		/// </summary>
		virtual public System.String PatientId
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.PatientId);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.PatientId, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Gender_Id
		/// </summary>
		virtual public System.String GenderId
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.GenderId);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.GenderId, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Date_of_Birth
		/// </summary>
		virtual public System.DateTime? DateOfBirth
		{
			get
			{
				return base.GetSystemDateTime(DemographicsMetadata.ColumnNames.DateOfBirth);
			}
			
			set
			{
				base.SetSystemDateTime(DemographicsMetadata.ColumnNames.DateOfBirth, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Patient_Name
		/// </summary>
		virtual public System.String PatientName
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.PatientName);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.PatientName, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Patient_Address
		/// </summary>
		virtual public System.String PatientAddress
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.PatientAddress);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.PatientAddress, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.City_Name
		/// </summary>
		virtual public System.String CityName
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.CityName);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.CityName, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Phone_Number
		/// </summary>
		virtual public System.String PhoneNumber
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.PhoneNumber);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.PhoneNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Fax_Number
		/// </summary>
		virtual public System.String FaxNumber
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.FaxNumber);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.FaxNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Mobile_Number
		/// </summary>
		virtual public System.String MobileNumber
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.MobileNumber);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.MobileNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Email
		/// </summary>
		virtual public System.String Email
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.Email);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.Email, value);
			}
		}
		
		/// <summary>
		/// Maps to Demographics.Ktp_Number
		/// </summary>
		virtual public System.String KtpNumber
		{
			get
			{
				return base.GetSystemString(DemographicsMetadata.ColumnNames.KtpNumber);
			}
			
			set
			{
				base.SetSystemString(DemographicsMetadata.ColumnNames.KtpNumber, value);
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
			public esStrings(esDemographics entity)
			{
				this.entity = entity;
			}
			
	
			public System.String PatientId
			{
				get
				{
					System.String data = entity.PatientId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientId = null;
					else entity.PatientId = Convert.ToString(value);
				}
			}
				
			public System.String GenderId
			{
				get
				{
					System.String data = entity.GenderId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GenderId = null;
					else entity.GenderId = Convert.ToString(value);
				}
			}
				
			public System.String DateOfBirth
			{
				get
				{
					System.DateTime? data = entity.DateOfBirth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DateOfBirth = null;
					else entity.DateOfBirth = Convert.ToDateTime(value);
				}
			}
				
			public System.String PatientName
			{
				get
				{
					System.String data = entity.PatientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientName = null;
					else entity.PatientName = Convert.ToString(value);
				}
			}
				
			public System.String PatientAddress
			{
				get
				{
					System.String data = entity.PatientAddress;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PatientAddress = null;
					else entity.PatientAddress = Convert.ToString(value);
				}
			}
				
			public System.String CityName
			{
				get
				{
					System.String data = entity.CityName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CityName = null;
					else entity.CityName = Convert.ToString(value);
				}
			}
				
			public System.String PhoneNumber
			{
				get
				{
					System.String data = entity.PhoneNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PhoneNumber = null;
					else entity.PhoneNumber = Convert.ToString(value);
				}
			}
				
			public System.String FaxNumber
			{
				get
				{
					System.String data = entity.FaxNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FaxNumber = null;
					else entity.FaxNumber = Convert.ToString(value);
				}
			}
				
			public System.String MobileNumber
			{
				get
				{
					System.String data = entity.MobileNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MobileNumber = null;
					else entity.MobileNumber = Convert.ToString(value);
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
				
			public System.String KtpNumber
			{
				get
				{
					System.String data = entity.KtpNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.KtpNumber = null;
					else entity.KtpNumber = Convert.ToString(value);
				}
			}
			

			private esDemographics entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esDemographicsQuery query)
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
				throw new Exception("esDemographics can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esDemographicsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DemographicsMetadata.Meta();
			}
		}	
		

		public esQueryItem PatientId
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.PatientId, esSystemType.String);
			}
		} 
		
		public esQueryItem GenderId
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.GenderId, esSystemType.String);
			}
		} 
		
		public esQueryItem DateOfBirth
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.DateOfBirth, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem PatientName
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.PatientName, esSystemType.String);
			}
		} 
		
		public esQueryItem PatientAddress
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.PatientAddress, esSystemType.String);
			}
		} 
		
		public esQueryItem CityName
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.CityName, esSystemType.String);
			}
		} 
		
		public esQueryItem PhoneNumber
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.PhoneNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem FaxNumber
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.FaxNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem MobileNumber
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.MobileNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem Email
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.Email, esSystemType.String);
			}
		} 
		
		public esQueryItem KtpNumber
		{
			get
			{
				return new esQueryItem(this, DemographicsMetadata.ColumnNames.KtpNumber, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("DemographicsCollection")]
	public partial class DemographicsCollection : esDemographicsCollection, IEnumerable<Demographics>
	{
		public DemographicsCollection()
		{

		}
		
		public static implicit operator List<Demographics>(DemographicsCollection coll)
		{
			List<Demographics> list = new List<Demographics>();
			
			foreach (Demographics emp in coll)
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
				return  DemographicsMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DemographicsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new Demographics(row);
		}

		override protected esEntity CreateEntity()
		{
			return new Demographics();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public DemographicsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DemographicsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(DemographicsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public Demographics AddNew()
		{
			Demographics entity = base.AddNewEntity() as Demographics;
			
			return entity;
		}

		public Demographics FindByPrimaryKey(System.String patientId)
		{
			return base.FindByPrimaryKey(patientId) as Demographics;
		}


		#region IEnumerable<Demographics> Members

		IEnumerator<Demographics> IEnumerable<Demographics>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as Demographics;
			}
		}

		#endregion
		
		private DemographicsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'Demographics' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("Demographics ({PatientId})")]
	[Serializable]
	public partial class Demographics : esDemographics
	{
		public Demographics()
		{

		}
	
		public Demographics(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DemographicsMetadata.Meta();
			}
		}
		
		
		
		override protected esDemographicsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DemographicsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public DemographicsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DemographicsQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(DemographicsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private DemographicsQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class DemographicsQuery : esDemographicsQuery
	{
		public DemographicsQuery()
		{

		}		
		
		public DemographicsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "DemographicsQuery";
        }
		
			
	}


	[Serializable]
	public partial class DemographicsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DemographicsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.PatientId, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.PatientId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.GenderId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.GenderId;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.DateOfBirth, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DemographicsMetadata.PropertyNames.DateOfBirth;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.PatientName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.PatientName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.PatientAddress, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.PatientAddress;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.CityName, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.CityName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.PhoneNumber, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.PhoneNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.FaxNumber, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.FaxNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.MobileNumber, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.MobileNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.Email, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.Email;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(DemographicsMetadata.ColumnNames.KtpNumber, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = DemographicsMetadata.PropertyNames.KtpNumber;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public DemographicsMetadata Meta()
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
			 public const string PatientId = "Patient_Id";
			 public const string GenderId = "Gender_Id";
			 public const string DateOfBirth = "Date_of_Birth";
			 public const string PatientName = "Patient_Name";
			 public const string PatientAddress = "Patient_Address";
			 public const string CityName = "City_Name";
			 public const string PhoneNumber = "Phone_Number";
			 public const string FaxNumber = "Fax_Number";
			 public const string MobileNumber = "Mobile_Number";
			 public const string Email = "Email";
			 public const string KtpNumber = "Ktp_Number";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PatientId = "PatientId";
			 public const string GenderId = "GenderId";
			 public const string DateOfBirth = "DateOfBirth";
			 public const string PatientName = "PatientName";
			 public const string PatientAddress = "PatientAddress";
			 public const string CityName = "CityName";
			 public const string PhoneNumber = "PhoneNumber";
			 public const string FaxNumber = "FaxNumber";
			 public const string MobileNumber = "MobileNumber";
			 public const string Email = "Email";
			 public const string KtpNumber = "KtpNumber";
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
			lock (typeof(DemographicsMetadata))
			{
				if(DemographicsMetadata.mapDelegates == null)
				{
					DemographicsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DemographicsMetadata.meta == null)
				{
					DemographicsMetadata.meta = new DemographicsMetadata();
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
				

				meta.AddTypeMap("PatientId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GenderId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DateOfBirth", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("PatientName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PatientAddress", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CityName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PhoneNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FaxNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MobileNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("KtpNumber", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "Demographics";
				meta.Destination = "Demographics";
				
				meta.spInsert = "proc_DemographicsInsert";				
				meta.spUpdate = "proc_DemographicsUpdate";		
				meta.spDelete = "proc_DemographicsDelete";
				meta.spLoadAll = "proc_DemographicsLoadAll";
				meta.spLoadByPrimaryKey = "proc_DemographicsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DemographicsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
