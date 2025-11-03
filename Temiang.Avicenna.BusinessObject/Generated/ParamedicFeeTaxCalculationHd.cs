/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:20 PM
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
	abstract public class esParamedicFeeTaxCalculationHdCollection : esEntityCollectionWAuditLog
	{
		public esParamedicFeeTaxCalculationHdCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ParamedicFeeTaxCalculationHdCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicFeeTaxCalculationHdQuery query)
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
			this.InitQuery(query as esParamedicFeeTaxCalculationHdQuery);
		}
		#endregion
		
		virtual public ParamedicFeeTaxCalculationHd DetachEntity(ParamedicFeeTaxCalculationHd entity)
		{
			return base.DetachEntity(entity) as ParamedicFeeTaxCalculationHd;
		}
		
		virtual public ParamedicFeeTaxCalculationHd AttachEntity(ParamedicFeeTaxCalculationHd entity)
		{
			return base.AttachEntity(entity) as ParamedicFeeTaxCalculationHd;
		}
		
		virtual public void Combine(ParamedicFeeTaxCalculationHdCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ParamedicFeeTaxCalculationHd this[int index]
		{
			get
			{
				return base[index] as ParamedicFeeTaxCalculationHd;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicFeeTaxCalculationHd);
		}
	}



	[Serializable]
	abstract public class esParamedicFeeTaxCalculationHd : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicFeeTaxCalculationHdQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicFeeTaxCalculationHd()
		{

		}

		public esParamedicFeeTaxCalculationHd(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String verificationNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String verificationNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(verificationNo);
			else
				return LoadByPrimaryKeyStoredProcedure(verificationNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String verificationNo)
		{
			esParamedicFeeTaxCalculationHdQuery query = this.GetDynamicQuery();
			query.Where(query.VerificationNo == verificationNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String verificationNo)
		{
			esParameters parms = new esParameters();
			parms.Add("VerificationNo",verificationNo);
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
						case "VerificationNo": this.str.VerificationNo = (string)value; break;							
						case "ParamedicID": this.str.ParamedicID = (string)value; break;							
						case "Period": this.str.Period = (string)value; break;							
						case "GrossAccumulation": this.str.GrossAccumulation = (string)value; break;							
						case "TaxBaseGrossAccumulation": this.str.TaxBaseGrossAccumulation = (string)value; break;							
						case "AccumulationTax": this.str.AccumulationTax = (string)value; break;							
						case "AccumulationOfRecentTax": this.str.AccumulationOfRecentTax = (string)value; break;							
						case "TaxToBePaid": this.str.TaxToBePaid = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Period":
						
							if (value == null || value is System.Int16)
								this.Period = (System.Int16?)value;
							break;
						
						case "GrossAccumulation":
						
							if (value == null || value is System.Decimal)
								this.GrossAccumulation = (System.Decimal?)value;
							break;
						
						case "TaxBaseGrossAccumulation":
						
							if (value == null || value is System.Decimal)
								this.TaxBaseGrossAccumulation = (System.Decimal?)value;
							break;
						
						case "AccumulationTax":
						
							if (value == null || value is System.Decimal)
								this.AccumulationTax = (System.Decimal?)value;
							break;
						
						case "AccumulationOfRecentTax":
						
							if (value == null || value is System.Decimal)
								this.AccumulationOfRecentTax = (System.Decimal?)value;
							break;
						
						case "TaxToBePaid":
						
							if (value == null || value is System.Decimal)
								this.TaxToBePaid = (System.Decimal?)value;
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
		/// Maps to ParamedicFeeTaxCalculationHd.VerificationNo
		/// </summary>
		virtual public System.String VerificationNo
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.VerificationNo);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.VerificationNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.ParamedicID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.ParamedicID, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.Period
		/// </summary>
		virtual public System.Int16? Period
		{
			get
			{
				return base.GetSystemInt16(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.Period);
			}
			
			set
			{
				base.SetSystemInt16(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.Period, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.GrossAccumulation
		/// </summary>
		virtual public System.Decimal? GrossAccumulation
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.GrossAccumulation);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.GrossAccumulation, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.TaxBaseGrossAccumulation
		/// </summary>
		virtual public System.Decimal? TaxBaseGrossAccumulation
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxBaseGrossAccumulation);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxBaseGrossAccumulation, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.AccumulationTax
		/// </summary>
		virtual public System.Decimal? AccumulationTax
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationTax);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationTax, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.AccumulationOfRecentTax
		/// </summary>
		virtual public System.Decimal? AccumulationOfRecentTax
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationOfRecentTax);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationOfRecentTax, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.TaxToBePaid
		/// </summary>
		virtual public System.Decimal? TaxToBePaid
		{
			get
			{
				return base.GetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxToBePaid);
			}
			
			set
			{
				base.SetSystemDecimal(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxToBePaid, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ParamedicFeeTaxCalculationHd.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicFeeTaxCalculationHd entity)
			{
				this.entity = entity;
			}
			
	
			public System.String VerificationNo
			{
				get
				{
					System.String data = entity.VerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VerificationNo = null;
					else entity.VerificationNo = Convert.ToString(value);
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
				
			public System.String Period
			{
				get
				{
					System.Int16? data = entity.Period;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Period = null;
					else entity.Period = Convert.ToInt16(value);
				}
			}
				
			public System.String GrossAccumulation
			{
				get
				{
					System.Decimal? data = entity.GrossAccumulation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GrossAccumulation = null;
					else entity.GrossAccumulation = Convert.ToDecimal(value);
				}
			}
				
			public System.String TaxBaseGrossAccumulation
			{
				get
				{
					System.Decimal? data = entity.TaxBaseGrossAccumulation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxBaseGrossAccumulation = null;
					else entity.TaxBaseGrossAccumulation = Convert.ToDecimal(value);
				}
			}
				
			public System.String AccumulationTax
			{
				get
				{
					System.Decimal? data = entity.AccumulationTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccumulationTax = null;
					else entity.AccumulationTax = Convert.ToDecimal(value);
				}
			}
				
			public System.String AccumulationOfRecentTax
			{
				get
				{
					System.Decimal? data = entity.AccumulationOfRecentTax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AccumulationOfRecentTax = null;
					else entity.AccumulationOfRecentTax = Convert.ToDecimal(value);
				}
			}
				
			public System.String TaxToBePaid
			{
				get
				{
					System.Decimal? data = entity.TaxToBePaid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxToBePaid = null;
					else entity.TaxToBePaid = Convert.ToDecimal(value);
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
			

			private esParamedicFeeTaxCalculationHd entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicFeeTaxCalculationHdQuery query)
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
				throw new Exception("esParamedicFeeTaxCalculationHd can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ParamedicFeeTaxCalculationHd : esParamedicFeeTaxCalculationHd
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
	abstract public class esParamedicFeeTaxCalculationHdQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTaxCalculationHdMetadata.Meta();
			}
		}	
		

		public esQueryItem VerificationNo
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.VerificationNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		} 
		
		public esQueryItem Period
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.Period, esSystemType.Int16);
			}
		} 
		
		public esQueryItem GrossAccumulation
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.GrossAccumulation, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TaxBaseGrossAccumulation
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxBaseGrossAccumulation, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AccumulationTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationTax, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AccumulationOfRecentTax
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationOfRecentTax, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem TaxToBePaid
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxToBePaid, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicFeeTaxCalculationHdCollection")]
	public partial class ParamedicFeeTaxCalculationHdCollection : esParamedicFeeTaxCalculationHdCollection, IEnumerable<ParamedicFeeTaxCalculationHd>
	{
		public ParamedicFeeTaxCalculationHdCollection()
		{

		}
		
		public static implicit operator List<ParamedicFeeTaxCalculationHd>(ParamedicFeeTaxCalculationHdCollection coll)
		{
			List<ParamedicFeeTaxCalculationHd> list = new List<ParamedicFeeTaxCalculationHd>();
			
			foreach (ParamedicFeeTaxCalculationHd emp in coll)
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
				return  ParamedicFeeTaxCalculationHdMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTaxCalculationHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicFeeTaxCalculationHd(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicFeeTaxCalculationHd();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ParamedicFeeTaxCalculationHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTaxCalculationHdQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ParamedicFeeTaxCalculationHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ParamedicFeeTaxCalculationHd AddNew()
		{
			ParamedicFeeTaxCalculationHd entity = base.AddNewEntity() as ParamedicFeeTaxCalculationHd;
			
			return entity;
		}

		public ParamedicFeeTaxCalculationHd FindByPrimaryKey(System.String verificationNo)
		{
			return base.FindByPrimaryKey(verificationNo) as ParamedicFeeTaxCalculationHd;
		}


		#region IEnumerable<ParamedicFeeTaxCalculationHd> Members

		IEnumerator<ParamedicFeeTaxCalculationHd> IEnumerable<ParamedicFeeTaxCalculationHd>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicFeeTaxCalculationHd;
			}
		}

		#endregion
		
		private ParamedicFeeTaxCalculationHdQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicFeeTaxCalculationHd' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ParamedicFeeTaxCalculationHd ({VerificationNo})")]
	[Serializable]
	public partial class ParamedicFeeTaxCalculationHd : esParamedicFeeTaxCalculationHd
	{
		public ParamedicFeeTaxCalculationHd()
		{

		}
	
		public ParamedicFeeTaxCalculationHd(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicFeeTaxCalculationHdMetadata.Meta();
			}
		}
		
		
		
		override protected esParamedicFeeTaxCalculationHdQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicFeeTaxCalculationHdQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ParamedicFeeTaxCalculationHdQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicFeeTaxCalculationHdQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ParamedicFeeTaxCalculationHdQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ParamedicFeeTaxCalculationHdQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ParamedicFeeTaxCalculationHdQuery : esParamedicFeeTaxCalculationHdQuery
	{
		public ParamedicFeeTaxCalculationHdQuery()
		{

		}		
		
		public ParamedicFeeTaxCalculationHdQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ParamedicFeeTaxCalculationHdQuery";
        }
		
			
	}


	[Serializable]
	public partial class ParamedicFeeTaxCalculationHdMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicFeeTaxCalculationHdMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.VerificationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.VerificationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.ParamedicID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.Period, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.Period;
			c.NumericPrecision = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.GrossAccumulation, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.GrossAccumulation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxBaseGrossAccumulation, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.TaxBaseGrossAccumulation;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationTax, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.AccumulationTax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.AccumulationOfRecentTax, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.AccumulationOfRecentTax;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.TaxToBePaid, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.TaxToBePaid;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ParamedicFeeTaxCalculationHdMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicFeeTaxCalculationHdMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ParamedicFeeTaxCalculationHdMetadata Meta()
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
			 public const string VerificationNo = "VerificationNo";
			 public const string ParamedicID = "ParamedicID";
			 public const string Period = "Period";
			 public const string GrossAccumulation = "GrossAccumulation";
			 public const string TaxBaseGrossAccumulation = "TaxBaseGrossAccumulation";
			 public const string AccumulationTax = "AccumulationTax";
			 public const string AccumulationOfRecentTax = "AccumulationOfRecentTax";
			 public const string TaxToBePaid = "TaxToBePaid";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string VerificationNo = "VerificationNo";
			 public const string ParamedicID = "ParamedicID";
			 public const string Period = "Period";
			 public const string GrossAccumulation = "GrossAccumulation";
			 public const string TaxBaseGrossAccumulation = "TaxBaseGrossAccumulation";
			 public const string AccumulationTax = "AccumulationTax";
			 public const string AccumulationOfRecentTax = "AccumulationOfRecentTax";
			 public const string TaxToBePaid = "TaxToBePaid";
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
			lock (typeof(ParamedicFeeTaxCalculationHdMetadata))
			{
				if(ParamedicFeeTaxCalculationHdMetadata.mapDelegates == null)
				{
					ParamedicFeeTaxCalculationHdMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ParamedicFeeTaxCalculationHdMetadata.meta == null)
				{
					ParamedicFeeTaxCalculationHdMetadata.meta = new ParamedicFeeTaxCalculationHdMetadata();
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
				

				meta.AddTypeMap("VerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Period", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("GrossAccumulation", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxBaseGrossAccumulation", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AccumulationTax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AccumulationOfRecentTax", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TaxToBePaid", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ParamedicFeeTaxCalculationHd";
				meta.Destination = "ParamedicFeeTaxCalculationHd";
				
				meta.spInsert = "proc_ParamedicFeeTaxCalculationHdInsert";				
				meta.spUpdate = "proc_ParamedicFeeTaxCalculationHdUpdate";		
				meta.spDelete = "proc_ParamedicFeeTaxCalculationHdDelete";
				meta.spLoadAll = "proc_ParamedicFeeTaxCalculationHdLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicFeeTaxCalculationHdLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicFeeTaxCalculationHdMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
