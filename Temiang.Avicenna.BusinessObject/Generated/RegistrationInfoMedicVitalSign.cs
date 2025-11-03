/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 3/24/2016 2:46:19 AM
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
	abstract public class esRegistrationInfoMedicVitalSignCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationInfoMedicVitalSignCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "RegistrationInfoMedicVitalSignCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicVitalSignQuery query)
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
			this.InitQuery(query as esRegistrationInfoMedicVitalSignQuery);
		}
		#endregion
		
		virtual public RegistrationInfoMedicVitalSign DetachEntity(RegistrationInfoMedicVitalSign entity)
		{
			return base.DetachEntity(entity) as RegistrationInfoMedicVitalSign;
		}
		
		virtual public RegistrationInfoMedicVitalSign AttachEntity(RegistrationInfoMedicVitalSign entity)
		{
			return base.AttachEntity(entity) as RegistrationInfoMedicVitalSign;
		}
		
		virtual public void Combine(RegistrationInfoMedicVitalSignCollection collection)
		{
			base.Combine(collection);
		}
		
		new public RegistrationInfoMedicVitalSign this[int index]
		{
			get
			{
				return base[index] as RegistrationInfoMedicVitalSign;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationInfoMedicVitalSign);
		}
	}



	[Serializable]
	abstract public class esRegistrationInfoMedicVitalSign : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationInfoMedicVitalSignQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationInfoMedicVitalSign()
		{

		}

		public esRegistrationInfoMedicVitalSign(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String registrationInfoMedicID, System.String vitalSignID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, vitalSignID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String registrationInfoMedicID, System.String vitalSignID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationInfoMedicID, vitalSignID);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationInfoMedicID, vitalSignID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String registrationInfoMedicID, System.String vitalSignID)
		{
			esRegistrationInfoMedicVitalSignQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationInfoMedicID == registrationInfoMedicID, query.VitalSignID == vitalSignID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String registrationInfoMedicID, System.String vitalSignID)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationInfoMedicID",registrationInfoMedicID);			parms.Add("VitalSignID",vitalSignID);
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
						case "VitalSignID": this.str.VitalSignID = (string)value; break;							
						case "VitalSignValueText": this.str.VitalSignValueText = (string)value; break;							
						case "VitalSignValueNum": this.str.VitalSignValueNum = (string)value; break;							
						case "VitalSignUnit": this.str.VitalSignUnit = (string)value; break;							
						case "EntryMask": this.str.EntryMask = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "VitalSignValueNum":
						
							if (value == null || value is System.Decimal)
								this.VitalSignValueNum = (System.Decimal?)value;
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
		/// Maps to RegistrationInfoMedicVitalSign.RegistrationInfoMedicID
		/// </summary>
		virtual public System.String RegistrationInfoMedicID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationInfoMedicID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationInfoMedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.VitalSignID
		/// </summary>
		virtual public System.String VitalSignID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.VitalSignValueText
		/// </summary>
		virtual public System.String VitalSignValueText
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueText);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueText, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.VitalSignValueNum
		/// </summary>
		virtual public System.Decimal? VitalSignValueNum
		{
			get
			{
				return base.GetSystemDecimal(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueNum);
			}
			
			set
			{
				base.SetSystemDecimal(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueNum, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.VitalSignUnit
		/// </summary>
		virtual public System.String VitalSignUnit
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignUnit);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.EntryMask
		/// </summary>
		virtual public System.String EntryMask
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.EntryMask);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.EntryMask, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to RegistrationInfoMedicVitalSign.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(RegistrationInfoMedicVitalSignMetadata.ColumnNames.SequenceNo, value);
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
			public esStrings(esRegistrationInfoMedicVitalSign entity)
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
				
			public System.String VitalSignID
			{
				get
				{
					System.String data = entity.VitalSignID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignID = null;
					else entity.VitalSignID = Convert.ToString(value);
				}
			}
				
			public System.String VitalSignValueText
			{
				get
				{
					System.String data = entity.VitalSignValueText;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignValueText = null;
					else entity.VitalSignValueText = Convert.ToString(value);
				}
			}
				
			public System.String VitalSignValueNum
			{
				get
				{
					System.Decimal? data = entity.VitalSignValueNum;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignValueNum = null;
					else entity.VitalSignValueNum = Convert.ToDecimal(value);
				}
			}
				
			public System.String VitalSignUnit
			{
				get
				{
					System.String data = entity.VitalSignUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VitalSignUnit = null;
					else entity.VitalSignUnit = Convert.ToString(value);
				}
			}
				
			public System.String EntryMask
			{
				get
				{
					System.String data = entity.EntryMask;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EntryMask = null;
					else entity.EntryMask = Convert.ToString(value);
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
			

			private esRegistrationInfoMedicVitalSign entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationInfoMedicVitalSignQuery query)
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
				throw new Exception("esRegistrationInfoMedicVitalSign can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esRegistrationInfoMedicVitalSignQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicVitalSignMetadata.Meta();
			}
		}	
		

		public esQueryItem RegistrationInfoMedicID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationInfoMedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem VitalSignID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignID, esSystemType.String);
			}
		} 
		
		public esQueryItem VitalSignValueText
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueText, esSystemType.String);
			}
		} 
		
		public esQueryItem VitalSignValueNum
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueNum, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem VitalSignUnit
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem EntryMask
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.EntryMask, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, RegistrationInfoMedicVitalSignMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationInfoMedicVitalSignCollection")]
	public partial class RegistrationInfoMedicVitalSignCollection : esRegistrationInfoMedicVitalSignCollection, IEnumerable<RegistrationInfoMedicVitalSign>
	{
		public RegistrationInfoMedicVitalSignCollection()
		{

		}
		
		public static implicit operator List<RegistrationInfoMedicVitalSign>(RegistrationInfoMedicVitalSignCollection coll)
		{
			List<RegistrationInfoMedicVitalSign> list = new List<RegistrationInfoMedicVitalSign>();
			
			foreach (RegistrationInfoMedicVitalSign emp in coll)
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
				return  RegistrationInfoMedicVitalSignMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicVitalSignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationInfoMedicVitalSign(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationInfoMedicVitalSign();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public RegistrationInfoMedicVitalSignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicVitalSignQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(RegistrationInfoMedicVitalSignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public RegistrationInfoMedicVitalSign AddNew()
		{
			RegistrationInfoMedicVitalSign entity = base.AddNewEntity() as RegistrationInfoMedicVitalSign;
			
			return entity;
		}

		public RegistrationInfoMedicVitalSign FindByPrimaryKey(System.String registrationInfoMedicID, System.String vitalSignID)
		{
			return base.FindByPrimaryKey(registrationInfoMedicID, vitalSignID) as RegistrationInfoMedicVitalSign;
		}


		#region IEnumerable<RegistrationInfoMedicVitalSign> Members

		IEnumerator<RegistrationInfoMedicVitalSign> IEnumerable<RegistrationInfoMedicVitalSign>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationInfoMedicVitalSign;
			}
		}

		#endregion
		
		private RegistrationInfoMedicVitalSignQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationInfoMedicVitalSign' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("RegistrationInfoMedicVitalSign ({RegistrationInfoMedicID},{VitalSignID})")]
	[Serializable]
	public partial class RegistrationInfoMedicVitalSign : esRegistrationInfoMedicVitalSign
	{
		public RegistrationInfoMedicVitalSign()
		{

		}
	
		public RegistrationInfoMedicVitalSign(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationInfoMedicVitalSignMetadata.Meta();
			}
		}
		
		
		
		override protected esRegistrationInfoMedicVitalSignQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationInfoMedicVitalSignQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public RegistrationInfoMedicVitalSignQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationInfoMedicVitalSignQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(RegistrationInfoMedicVitalSignQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private RegistrationInfoMedicVitalSignQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class RegistrationInfoMedicVitalSignQuery : esRegistrationInfoMedicVitalSignQuery
	{
		public RegistrationInfoMedicVitalSignQuery()
		{

		}		
		
		public RegistrationInfoMedicVitalSignQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "RegistrationInfoMedicVitalSignQuery";
        }
		
			
	}


	[Serializable]
	public partial class RegistrationInfoMedicVitalSignMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationInfoMedicVitalSignMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationInfoMedicID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.RegistrationInfoMedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.VitalSignID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueText, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.VitalSignValueText;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignValueNum, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.VitalSignValueNum;
			c.NumericPrecision = 18;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.VitalSignUnit, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.VitalSignUnit;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.EntryMask, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.EntryMask;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.RegistrationNo, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(RegistrationInfoMedicVitalSignMetadata.ColumnNames.SequenceNo, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationInfoMedicVitalSignMetadata.PropertyNames.SequenceNo;
			c.CharacterMaxLength = 3;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public RegistrationInfoMedicVitalSignMetadata Meta()
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
			 public const string VitalSignID = "VitalSignID";
			 public const string VitalSignValueText = "VitalSignValueText";
			 public const string VitalSignValueNum = "VitalSignValueNum";
			 public const string VitalSignUnit = "VitalSignUnit";
			 public const string EntryMask = "EntryMask";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SequenceNo = "SequenceNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RegistrationInfoMedicID = "RegistrationInfoMedicID";
			 public const string VitalSignID = "VitalSignID";
			 public const string VitalSignValueText = "VitalSignValueText";
			 public const string VitalSignValueNum = "VitalSignValueNum";
			 public const string VitalSignUnit = "VitalSignUnit";
			 public const string EntryMask = "EntryMask";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string RegistrationNo = "RegistrationNo";
			 public const string SequenceNo = "SequenceNo";
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
			lock (typeof(RegistrationInfoMedicVitalSignMetadata))
			{
				if(RegistrationInfoMedicVitalSignMetadata.mapDelegates == null)
				{
					RegistrationInfoMedicVitalSignMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RegistrationInfoMedicVitalSignMetadata.meta == null)
				{
					RegistrationInfoMedicVitalSignMetadata.meta = new RegistrationInfoMedicVitalSignMetadata();
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
				meta.AddTypeMap("VitalSignID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignValueText", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("VitalSignValueNum", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("VitalSignUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EntryMask", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "RegistrationInfoMedicVitalSign";
				meta.Destination = "RegistrationInfoMedicVitalSign";
				
				meta.spInsert = "proc_RegistrationInfoMedicVitalSignInsert";				
				meta.spUpdate = "proc_RegistrationInfoMedicVitalSignUpdate";		
				meta.spDelete = "proc_RegistrationInfoMedicVitalSignDelete";
				meta.spLoadAll = "proc_RegistrationInfoMedicVitalSignLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationInfoMedicVitalSignLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationInfoMedicVitalSignMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
