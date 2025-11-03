/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 09/01/2024 11:31:42
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esBloodBankTransactionItemCollection : esEntityCollectionWAuditLog
	{
		public esBloodBankTransactionItemCollection()
		{

		}

		protected override string GetConnectionName()
		{
			return "sci";
		}

		protected override string GetCollectionName()
		{
			return "BloodBankTransactionItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esBloodBankTransactionItemQuery query)
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
			this.InitQuery(query as esBloodBankTransactionItemQuery);
		}
		#endregion

		virtual public BloodBankTransactionItem DetachEntity(BloodBankTransactionItem entity)
		{
			return base.DetachEntity(entity) as BloodBankTransactionItem;
		}

		virtual public BloodBankTransactionItem AttachEntity(BloodBankTransactionItem entity)
		{
			return base.AttachEntity(entity) as BloodBankTransactionItem;
		}

		virtual public void Combine(BloodBankTransactionItemCollection collection)
		{
			base.Combine(collection);
		}

		new public BloodBankTransactionItem this[int index]
		{
			get
			{
				return base[index] as BloodBankTransactionItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(BloodBankTransactionItem);
		}
	}

	[Serializable]
	abstract public class esBloodBankTransactionItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esBloodBankTransactionItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esBloodBankTransactionItem()
		{
		}

		public esBloodBankTransactionItem(DataRow row)
			: base(row)
		{
		}

		protected override string GetConnectionName()
		{
			return "sci";
		}

		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, String bagNo)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, bagNo);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, String bagNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, bagNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, bagNo);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, String bagNo)
		{
			esBloodBankTransactionItemQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.BagNo == bagNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, String bagNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
			parms.Add("BagNo", bagNo);
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
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "BagNo": this.str.BagNo = (string)value; break;
						case "ReceivedDate": this.str.ReceivedDate = (string)value; break;
						case "ReceivedTime": this.str.ReceivedTime = (string)value; break;
						case "SRBloodGroupReceived": this.str.SRBloodGroupReceived = (string)value; break;
						case "SRBloodBagStatus": this.str.SRBloodBagStatus = (string)value; break;
						case "IsScreeningLabelPassedPmi": this.str.IsScreeningLabelPassedPmi = (string)value; break;
						case "IsExpiredDate": this.str.IsExpiredDate = (string)value; break;
						case "IsLeak": this.str.IsLeak = (string)value; break;
						case "IsHemolysis": this.str.IsHemolysis = (string)value; break;
						case "IsCrossMatchingSuitability": this.str.IsCrossMatchingSuitability = (string)value; break;
						case "IsClotting": this.str.IsClotting = (string)value; break;
						case "IsBloodTypeCompatibility": this.str.IsBloodTypeCompatibility = (string)value; break;
						case "IsLabelDonorsMatchesWithPatientsForm": this.str.IsLabelDonorsMatchesWithPatientsForm = (string)value; break;
						case "IsScreeningLabelPassedBd": this.str.IsScreeningLabelPassedBd = (string)value; break;
						case "ExaminerByUserID": this.str.ExaminerByUserID = (string)value; break;
						case "UnitOfficer": this.str.UnitOfficer = (string)value; break;
						case "TransfusionStartDateTime": this.str.TransfusionStartDateTime = (string)value; break;
						case "TransfusionEndDateTime": this.str.TransfusionEndDateTime = (string)value; break;
						case "TransfusedOfficerStartBy": this.str.TransfusedOfficerStartBy = (string)value; break;
						case "TransfusedOfficerEndBy": this.str.TransfusedOfficerEndBy = (string)value; break;
						case "QtyTransfusion": this.str.QtyTransfusion = (string)value; break;
						case "SRBloodSource": this.str.SRBloodSource = (string)value; break;
						case "SRBloodSourceFrom": this.str.SRBloodSourceFrom = (string)value; break;
						case "CrossmatchCompatibleMajor": this.str.CrossmatchCompatibleMajor = (string)value; break;
						case "CrossmatchCompatibleMinor": this.str.CrossmatchCompatibleMinor = (string)value; break;
						case "CrossmatchCompatibleMinorLevel": this.str.CrossmatchCompatibleMinorLevel = (string)value; break;
						case "CrossmatchCompatibleAutoControl": this.str.CrossmatchCompatibleAutoControl = (string)value; break;
						case "CrossmatchCompatibleAutoControlLevel": this.str.CrossmatchCompatibleAutoControlLevel = (string)value; break;
						case "CrossmatchCompatibleDctControl": this.str.CrossmatchCompatibleDctControl = (string)value; break;
						case "CrossmatchCompatibleDctControlLevel": this.str.CrossmatchCompatibleDctControlLevel = (string)value; break;
						case "CrossmatchStartDateTime": this.str.CrossmatchStartDateTime = (string)value; break;
						case "CrossmatchEndDateTime": this.str.CrossmatchEndDateTime = (string)value; break;
						case "IsCrossmatchingPassed": this.str.IsCrossmatchingPassed = (string)value; break;
						case "CrossMatchingByUserID": this.str.CrossMatchingByUserID = (string)value; break;
						case "BloodBagTemperature": this.str.BloodBagTemperature = (string)value; break;
						case "BloodBagNotes": this.str.BloodBagNotes = (string)value; break;
						case "IsProceedToTransfusion": this.str.IsProceedToTransfusion = (string)value; break;
						case "BpPre": this.str.BpPre = (string)value; break;
						case "Bp10": this.str.Bp10 = (string)value; break;
						case "Bp30": this.str.Bp30 = (string)value; break;
						case "Bp60": this.str.Bp60 = (string)value; break;
						case "Bp120": this.str.Bp120 = (string)value; break;
						case "Bp180": this.str.Bp180 = (string)value; break;
						case "Bp240": this.str.Bp240 = (string)value; break;
						case "BpPost": this.str.BpPost = (string)value; break;
						case "BpPost2": this.str.BpPost2 = (string)value; break;
						case "BpPost3": this.str.BpPost3 = (string)value; break;
						case "PulsePre": this.str.PulsePre = (string)value; break;
						case "Pulse10": this.str.Pulse10 = (string)value; break;
						case "Pulse30": this.str.Pulse30 = (string)value; break;
						case "Pulse60": this.str.Pulse60 = (string)value; break;
						case "Pulse120": this.str.Pulse120 = (string)value; break;
						case "Pulse180": this.str.Pulse180 = (string)value; break;
						case "Pulse240": this.str.Pulse240 = (string)value; break;
						case "PulsePost": this.str.PulsePost = (string)value; break;
						case "PulsePost2": this.str.PulsePost2 = (string)value; break;
						case "PulsePost3": this.str.PulsePost3 = (string)value; break;
						case "TemperaturePre": this.str.TemperaturePre = (string)value; break;
						case "Temperature10": this.str.Temperature10 = (string)value; break;
						case "Temperature30": this.str.Temperature30 = (string)value; break;
						case "Temperature60": this.str.Temperature60 = (string)value; break;
						case "Temperature120": this.str.Temperature120 = (string)value; break;
						case "Temperature180": this.str.Temperature180 = (string)value; break;
						case "Temperature240": this.str.Temperature240 = (string)value; break;
						case "TemperaturePost": this.str.TemperaturePost = (string)value; break;
						case "TemperaturePost2": this.str.TemperaturePost2 = (string)value; break;
						case "TemperaturePost3": this.str.TemperaturePost3 = (string)value; break;
						case "RespiratoryPre": this.str.RespiratoryPre = (string)value; break;
						case "Respiratory10": this.str.Respiratory10 = (string)value; break;
						case "Respiratory30": this.str.Respiratory30 = (string)value; break;
						case "Respiratory60": this.str.Respiratory60 = (string)value; break;
						case "Respiratory120": this.str.Respiratory120 = (string)value; break;
						case "Respiratory180": this.str.Respiratory180 = (string)value; break;
						case "Respiratory240": this.str.Respiratory240 = (string)value; break;
						case "RespiratoryPost": this.str.RespiratoryPost = (string)value; break;
						case "RespiratoryPost2": this.str.RespiratoryPost2 = (string)value; break;
						case "RespiratoryPost3": this.str.RespiratoryPost3 = (string)value; break;
						case "IsFeverPre": this.str.IsFeverPre = (string)value; break;
						case "IsFever10": this.str.IsFever10 = (string)value; break;
						case "IsFever30": this.str.IsFever30 = (string)value; break;
						case "IsFever60": this.str.IsFever60 = (string)value; break;
						case "IsFever120": this.str.IsFever120 = (string)value; break;
						case "IsFever180": this.str.IsFever180 = (string)value; break;
						case "IsFever240": this.str.IsFever240 = (string)value; break;
						case "IsFeverPost": this.str.IsFeverPost = (string)value; break;
						case "IsFeverPost2": this.str.IsFeverPost2 = (string)value; break;
						case "IsFeverPost3": this.str.IsFeverPost3 = (string)value; break;
						case "IsShiveringPre": this.str.IsShiveringPre = (string)value; break;
						case "IsShivering10": this.str.IsShivering10 = (string)value; break;
						case "IsShivering30": this.str.IsShivering30 = (string)value; break;
						case "IsShivering60": this.str.IsShivering60 = (string)value; break;
						case "IsShivering120": this.str.IsShivering120 = (string)value; break;
						case "IsShivering180": this.str.IsShivering180 = (string)value; break;
						case "IsShivering240": this.str.IsShivering240 = (string)value; break;
						case "IsShiveringPost": this.str.IsShiveringPost = (string)value; break;
						case "IsShiveringPost2": this.str.IsShiveringPost2 = (string)value; break;
						case "IsShiveringPost3": this.str.IsShiveringPost3 = (string)value; break;
						case "IsSobPre": this.str.IsSobPre = (string)value; break;
						case "IsSob10": this.str.IsSob10 = (string)value; break;
						case "IsSob30": this.str.IsSob30 = (string)value; break;
						case "IsSob60": this.str.IsSob60 = (string)value; break;
						case "IsSob120": this.str.IsSob120 = (string)value; break;
						case "IsSob180": this.str.IsSob180 = (string)value; break;
						case "IsSob240": this.str.IsSob240 = (string)value; break;
						case "IsSobPost": this.str.IsSobPost = (string)value; break;
						case "IsSobPost2": this.str.IsSobPost2 = (string)value; break;
						case "IsSobPost3": this.str.IsSobPost3 = (string)value; break;
						case "IsPainfulPre": this.str.IsPainfulPre = (string)value; break;
						case "IsPainful10": this.str.IsPainful10 = (string)value; break;
						case "IsPainful30": this.str.IsPainful30 = (string)value; break;
						case "IsPainful60": this.str.IsPainful60 = (string)value; break;
						case "IsPainful120": this.str.IsPainful120 = (string)value; break;
						case "IsPainful180": this.str.IsPainful180 = (string)value; break;
						case "IsPainful240": this.str.IsPainful240 = (string)value; break;
						case "IsPainfulPost": this.str.IsPainfulPost = (string)value; break;
						case "IsPainfulPost2": this.str.IsPainfulPost2 = (string)value; break;
						case "IsPainfulPost3": this.str.IsPainfulPost3 = (string)value; break;
						case "IsNauseaPre": this.str.IsNauseaPre = (string)value; break;
						case "IsNausea10": this.str.IsNausea10 = (string)value; break;
						case "IsNausea30": this.str.IsNausea30 = (string)value; break;
						case "IsNausea60": this.str.IsNausea60 = (string)value; break;
						case "IsNausea120": this.str.IsNausea120 = (string)value; break;
						case "IsNausea180": this.str.IsNausea180 = (string)value; break;
						case "IsNausea240": this.str.IsNausea240 = (string)value; break;
						case "IsNauseaPost": this.str.IsNauseaPost = (string)value; break;
						case "IsNauseaPost2": this.str.IsNauseaPost2 = (string)value; break;
						case "IsNauseaPost3": this.str.IsNauseaPost3 = (string)value; break;
						case "IsBleedingPre": this.str.IsBleedingPre = (string)value; break;
						case "IsBleeding10": this.str.IsBleeding10 = (string)value; break;
						case "IsBleeding30": this.str.IsBleeding30 = (string)value; break;
						case "IsBleeding60": this.str.IsBleeding60 = (string)value; break;
						case "IsBleeding120": this.str.IsBleeding120 = (string)value; break;
						case "IsBleeding180": this.str.IsBleeding180 = (string)value; break;
						case "IsBleeding240": this.str.IsBleeding240 = (string)value; break;
						case "IsBleedingPost": this.str.IsBleedingPost = (string)value; break;
						case "IsBleedingPost2": this.str.IsBleedingPost2 = (string)value; break;
						case "IsBleedingPost3": this.str.IsBleedingPost3 = (string)value; break;
						case "IsHypotensionPre": this.str.IsHypotensionPre = (string)value; break;
						case "IsHypotension10": this.str.IsHypotension10 = (string)value; break;
						case "IsHypotension30": this.str.IsHypotension30 = (string)value; break;
						case "IsHypotension60": this.str.IsHypotension60 = (string)value; break;
						case "IsHypotension120": this.str.IsHypotension120 = (string)value; break;
						case "IsHypotension180": this.str.IsHypotension180 = (string)value; break;
						case "IsHypotension240": this.str.IsHypotension240 = (string)value; break;
						case "IsHypotensionPost": this.str.IsHypotensionPost = (string)value; break;
						case "IsHypotensionPost2": this.str.IsHypotensionPost2 = (string)value; break;
						case "IsHypotensionPost3": this.str.IsHypotensionPost3 = (string)value; break;
						case "IsShockPre": this.str.IsShockPre = (string)value; break;
						case "IsShock10": this.str.IsShock10 = (string)value; break;
						case "IsShock30": this.str.IsShock30 = (string)value; break;
						case "IsShock60": this.str.IsShock60 = (string)value; break;
						case "IsShock120": this.str.IsShock120 = (string)value; break;
						case "IsShock180": this.str.IsShock180 = (string)value; break;
						case "IsShock240": this.str.IsShock240 = (string)value; break;
						case "IsShockPost": this.str.IsShockPost = (string)value; break;
						case "IsShockPost2": this.str.IsShockPost2 = (string)value; break;
						case "IsShockPost3": this.str.IsShockPost3 = (string)value; break;
						case "IsUrticariaPre": this.str.IsUrticariaPre = (string)value; break;
						case "IsUrticaria10": this.str.IsUrticaria10 = (string)value; break;
						case "IsUrticaria30": this.str.IsUrticaria30 = (string)value; break;
						case "IsUrticaria60": this.str.IsUrticaria60 = (string)value; break;
						case "IsUrticaria120": this.str.IsUrticaria120 = (string)value; break;
						case "IsUrticaria180": this.str.IsUrticaria180 = (string)value; break;
						case "IsUrticaria240": this.str.IsUrticaria240 = (string)value; break;
						case "IsUrticariaPost": this.str.IsUrticariaPost = (string)value; break;
						case "IsUrticariaPost2": this.str.IsUrticariaPost2 = (string)value; break;
						case "IsUrticariaPost3": this.str.IsUrticariaPost3 = (string)value; break;
						case "IsRashPre": this.str.IsRashPre = (string)value; break;
						case "IsRash10": this.str.IsRash10 = (string)value; break;
						case "IsRash30": this.str.IsRash30 = (string)value; break;
						case "IsRash60": this.str.IsRash60 = (string)value; break;
						case "IsRash120": this.str.IsRash120 = (string)value; break;
						case "IsRash180": this.str.IsRash180 = (string)value; break;
						case "IsRash240": this.str.IsRash240 = (string)value; break;
						case "IsRashPost": this.str.IsRashPost = (string)value; break;
						case "IsRashPost2": this.str.IsRashPost2 = (string)value; break;
						case "IsRashPost3": this.str.IsRashPost3 = (string)value; break;
						case "IsPruritusPre": this.str.IsPruritusPre = (string)value; break;
						case "IsPruritus10": this.str.IsPruritus10 = (string)value; break;
						case "IsPruritus30": this.str.IsPruritus30 = (string)value; break;
						case "IsPruritus60": this.str.IsPruritus60 = (string)value; break;
						case "IsPruritus120": this.str.IsPruritus120 = (string)value; break;
						case "IsPruritus180": this.str.IsPruritus180 = (string)value; break;
						case "IsPruritus240": this.str.IsPruritus240 = (string)value; break;
						case "IsPruritusPost": this.str.IsPruritusPost = (string)value; break;
						case "IsPruritusPost2": this.str.IsPruritusPost2 = (string)value; break;
						case "IsPruritusPost3": this.str.IsPruritusPost3 = (string)value; break;
						case "IsAnxiousPre": this.str.IsAnxiousPre = (string)value; break;
						case "IsAnxious10": this.str.IsAnxious10 = (string)value; break;
						case "IsAnxious30": this.str.IsAnxious30 = (string)value; break;
						case "IsAnxious60": this.str.IsAnxious60 = (string)value; break;
						case "IsAnxious120": this.str.IsAnxious120 = (string)value; break;
						case "IsAnxious180": this.str.IsAnxious180 = (string)value; break;
						case "IsAnxious240": this.str.IsAnxious240 = (string)value; break;
						case "IsAnxiousPost": this.str.IsAnxiousPost = (string)value; break;
						case "IsAnxiousPost2": this.str.IsAnxiousPost2 = (string)value; break;
						case "IsAnxiousPost3": this.str.IsAnxiousPost3 = (string)value; break;
						case "IsWeakPre": this.str.IsWeakPre = (string)value; break;
						case "IsWeak10": this.str.IsWeak10 = (string)value; break;
						case "IsWeak30": this.str.IsWeak30 = (string)value; break;
						case "IsWeak60": this.str.IsWeak60 = (string)value; break;
						case "IsWeak120": this.str.IsWeak120 = (string)value; break;
						case "IsWeak180": this.str.IsWeak180 = (string)value; break;
						case "IsWeak240": this.str.IsWeak240 = (string)value; break;
						case "IsWeakPost": this.str.IsWeakPost = (string)value; break;
						case "IsWeakPost2": this.str.IsWeakPost2 = (string)value; break;
						case "IsWeakPost3": this.str.IsWeakPost3 = (string)value; break;
						case "IsPalpitationsPre": this.str.IsPalpitationsPre = (string)value; break;
						case "IsPalpitations10": this.str.IsPalpitations10 = (string)value; break;
						case "IsPalpitations30": this.str.IsPalpitations30 = (string)value; break;
						case "IsPalpitations60": this.str.IsPalpitations60 = (string)value; break;
						case "IsPalpitations120": this.str.IsPalpitations120 = (string)value; break;
						case "IsPalpitations180": this.str.IsPalpitations180 = (string)value; break;
						case "IsPalpitations240": this.str.IsPalpitations240 = (string)value; break;
						case "IsPalpitationsPost": this.str.IsPalpitationsPost = (string)value; break;
						case "IsPalpitationsPost2": this.str.IsPalpitationsPost2 = (string)value; break;
						case "IsPalpitationsPost3": this.str.IsPalpitationsPost3 = (string)value; break;
						case "IsMildDyspneaPre": this.str.IsMildDyspneaPre = (string)value; break;
						case "IsMildDyspnea10": this.str.IsMildDyspnea10 = (string)value; break;
						case "IsMildDyspnea30": this.str.IsMildDyspnea30 = (string)value; break;
						case "IsMildDyspnea60": this.str.IsMildDyspnea60 = (string)value; break;
						case "IsMildDyspnea120": this.str.IsMildDyspnea120 = (string)value; break;
						case "IsMildDyspnea180": this.str.IsMildDyspnea180 = (string)value; break;
						case "IsMildDyspnea240": this.str.IsMildDyspnea240 = (string)value; break;
						case "IsMildDyspneaPost": this.str.IsMildDyspneaPost = (string)value; break;
						case "IsMildDyspneaPost2": this.str.IsMildDyspneaPost2 = (string)value; break;
						case "IsMildDyspneaPost3": this.str.IsMildDyspneaPost3 = (string)value; break;
						case "IsHeadachePre": this.str.IsHeadachePre = (string)value; break;
						case "IsHeadache10": this.str.IsHeadache10 = (string)value; break;
						case "IsHeadache30": this.str.IsHeadache30 = (string)value; break;
						case "IsHeadache60": this.str.IsHeadache60 = (string)value; break;
						case "IsHeadache120": this.str.IsHeadache120 = (string)value; break;
						case "IsHeadache180": this.str.IsHeadache180 = (string)value; break;
						case "IsHeadache240": this.str.IsHeadache240 = (string)value; break;
						case "IsHeadachePost": this.str.IsHeadachePost = (string)value; break;
						case "IsHeadachePost2": this.str.IsHeadachePost2 = (string)value; break;
						case "IsHeadachePost3": this.str.IsHeadachePost3 = (string)value; break;
						case "IsRednessOnTheSkinPre": this.str.IsRednessOnTheSkinPre = (string)value; break;
						case "IsRednessOnTheSkin10": this.str.IsRednessOnTheSkin10 = (string)value; break;
						case "IsRednessOnTheSkin30": this.str.IsRednessOnTheSkin30 = (string)value; break;
						case "IsRednessOnTheSkin60": this.str.IsRednessOnTheSkin60 = (string)value; break;
						case "IsRednessOnTheSkin120": this.str.IsRednessOnTheSkin120 = (string)value; break;
						case "IsRednessOnTheSkin180": this.str.IsRednessOnTheSkin180 = (string)value; break;
						case "IsRednessOnTheSkin240": this.str.IsRednessOnTheSkin240 = (string)value; break;
						case "IsRednessOnTheSkinPost": this.str.IsRednessOnTheSkinPost = (string)value; break;
						case "IsRednessOnTheSkinPost2": this.str.IsRednessOnTheSkinPost2 = (string)value; break;
						case "IsRednessOnTheSkinPost3": this.str.IsRednessOnTheSkinPost3 = (string)value; break;
						case "IsTachycardiaPre": this.str.IsTachycardiaPre = (string)value; break;
						case "IsTachycardia10": this.str.IsTachycardia10 = (string)value; break;
						case "IsTachycardia30": this.str.IsTachycardia30 = (string)value; break;
						case "IsTachycardia60": this.str.IsTachycardia60 = (string)value; break;
						case "IsTachycardia120": this.str.IsTachycardia120 = (string)value; break;
						case "IsTachycardia180": this.str.IsTachycardia180 = (string)value; break;
						case "IsTachycardia240": this.str.IsTachycardia240 = (string)value; break;
						case "IsTachycardiaPost": this.str.IsTachycardiaPost = (string)value; break;
						case "IsTachycardiaPost2": this.str.IsTachycardiaPost2 = (string)value; break;
						case "IsTachycardiaPost3": this.str.IsTachycardiaPost3 = (string)value; break;
						case "IsMuscleStiffnessPre": this.str.IsMuscleStiffnessPre = (string)value; break;
						case "IsMuscleStiffness10": this.str.IsMuscleStiffness10 = (string)value; break;
						case "IsMuscleStiffness30": this.str.IsMuscleStiffness30 = (string)value; break;
						case "IsMuscleStiffness60": this.str.IsMuscleStiffness60 = (string)value; break;
						case "IsMuscleStiffness120": this.str.IsMuscleStiffness120 = (string)value; break;
						case "IsMuscleStiffness180": this.str.IsMuscleStiffness180 = (string)value; break;
						case "IsMuscleStiffness240": this.str.IsMuscleStiffness240 = (string)value; break;
						case "IsMuscleStiffnessPost": this.str.IsMuscleStiffnessPost = (string)value; break;
						case "IsMuscleStiffnessPost2": this.str.IsMuscleStiffnessPost2 = (string)value; break;
						case "IsMuscleStiffnessPost3": this.str.IsMuscleStiffnessPost3 = (string)value; break;
						case "OtherReactionPre": this.str.OtherReactionPre = (string)value; break;
						case "OtherReaction10": this.str.OtherReaction10 = (string)value; break;
						case "OtherReaction30": this.str.OtherReaction30 = (string)value; break;
						case "OtherReaction60": this.str.OtherReaction60 = (string)value; break;
						case "OtherReaction120": this.str.OtherReaction120 = (string)value; break;
						case "OtherReaction180": this.str.OtherReaction180 = (string)value; break;
						case "OtherReaction240": this.str.OtherReaction240 = (string)value; break;
						case "OtherReactionPost": this.str.OtherReactionPost = (string)value; break;
						case "OtherReactionPost2": this.str.OtherReactionPost2 = (string)value; break;
						case "OtherReactionPost3": this.str.OtherReactionPost3 = (string)value; break;
						case "HemoglobinPre": this.str.HemoglobinPre = (string)value; break;
						case "Hemoglobin10": this.str.Hemoglobin10 = (string)value; break;
						case "Hemoglobin30": this.str.Hemoglobin30 = (string)value; break;
						case "Hemoglobin60": this.str.Hemoglobin60 = (string)value; break;
						case "Hemoglobin120": this.str.Hemoglobin120 = (string)value; break;
						case "Hemoglobin180": this.str.Hemoglobin180 = (string)value; break;
						case "Hemoglobin240": this.str.Hemoglobin240 = (string)value; break;
						case "HemoglobinPost": this.str.HemoglobinPost = (string)value; break;
						case "HemoglobinPost2": this.str.HemoglobinPost2 = (string)value; break;
						case "HemoglobinPost3": this.str.HemoglobinPost3 = (string)value; break;
						case "HematocritPre": this.str.HematocritPre = (string)value; break;
						case "Hematocrit10": this.str.Hematocrit10 = (string)value; break;
						case "Hematocrit30": this.str.Hematocrit30 = (string)value; break;
						case "Hematocrit60": this.str.Hematocrit60 = (string)value; break;
						case "Hematocrit120": this.str.Hematocrit120 = (string)value; break;
						case "Hematocrit180": this.str.Hematocrit180 = (string)value; break;
						case "Hematocrit240": this.str.Hematocrit240 = (string)value; break;
						case "HematocritPost": this.str.HematocritPost = (string)value; break;
						case "HematocritPost2": this.str.HematocritPost2 = (string)value; break;
						case "HematocritPost3": this.str.HematocritPost3 = (string)value; break;
						case "PlateletPre": this.str.PlateletPre = (string)value; break;
						case "Platelet10": this.str.Platelet10 = (string)value; break;
						case "Platelet30": this.str.Platelet30 = (string)value; break;
						case "Platelet60": this.str.Platelet60 = (string)value; break;
						case "Platelet120": this.str.Platelet120 = (string)value; break;
						case "Platelet180": this.str.Platelet180 = (string)value; break;
						case "Platelet240": this.str.Platelet240 = (string)value; break;
						case "PlateletPost": this.str.PlateletPost = (string)value; break;
						case "PlateletPost2": this.str.PlateletPost2 = (string)value; break;
						case "PlateletPost3": this.str.PlateletPost3 = (string)value; break;
						case "ActionPre": this.str.ActionPre = (string)value; break;
						case "Action10": this.str.Action10 = (string)value; break;
						case "Action30": this.str.Action30 = (string)value; break;
						case "Action60": this.str.Action60 = (string)value; break;
						case "Action120": this.str.Action120 = (string)value; break;
						case "Action180": this.str.Action180 = (string)value; break;
						case "Action240": this.str.Action240 = (string)value; break;
						case "ActionPost": this.str.ActionPost = (string)value; break;
						case "ActionPost2": this.str.ActionPost2 = (string)value; break;
						case "ActionPost3": this.str.ActionPost3 = (string)value; break;
						case "IsHiv": this.str.IsHiv = (string)value; break;
						case "IsVbrl": this.str.IsVbrl = (string)value; break;
						case "IsHbsAg": this.str.IsHbsAg = (string)value; break;
						case "IsHcv": this.str.IsHcv = (string)value; break;
						case "IsReCheck": this.str.IsReCheck = (string)value; break;
						case "ReCheckDateTime": this.str.ReCheckDateTime = (string)value; break;
						case "IsReCheckExpiredDate": this.str.IsReCheckExpiredDate = (string)value; break;
						case "IsReCheckLeak": this.str.IsReCheckLeak = (string)value; break;
						case "IsReCheckHemolysis": this.str.IsReCheckHemolysis = (string)value; break;
						case "IsReCheckCrossMatchingSuitability": this.str.IsReCheckCrossMatchingSuitability = (string)value; break;
						case "IsReCheckClotting": this.str.IsReCheckClotting = (string)value; break;
						case "IsReCheckBloodTypeCompatibility": this.str.IsReCheckBloodTypeCompatibility = (string)value; break;
						case "ReCheckOfficer": this.str.ReCheckOfficer = (string)value; break;
						case "ReCheckOfficer2": this.str.ReCheckOfficer2 = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsCrossmatchBillProceed": this.str.IsCrossmatchBillProceed = (string)value; break;
						case "IsTransfusionBillProceed": this.str.IsTransfusionBillProceed = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsScreening1": this.str.IsScreening1 = (string)value; break;
						case "IsScreening2": this.str.IsScreening2 = (string)value; break;
						case "IsScreening3": this.str.IsScreening3 = (string)value; break;
						case "UnitOfficerByUserID": this.str.UnitOfficerByUserID = (string)value; break;
						case "Bp31": this.str.Bp31 = (string)value; break;
						case "Bp32": this.str.Bp32 = (string)value; break;
						case "Bp33": this.str.Bp33 = (string)value; break;
						case "Bp34": this.str.Bp34 = (string)value; break;
						case "BpPost4": this.str.BpPost4 = (string)value; break;
						case "Pulse31": this.str.Pulse31 = (string)value; break;
						case "Pulse32": this.str.Pulse32 = (string)value; break;
						case "Pulse33": this.str.Pulse33 = (string)value; break;
						case "Pulse34": this.str.Pulse34 = (string)value; break;
						case "PulsePost4": this.str.PulsePost4 = (string)value; break;
						case "Temperature31": this.str.Temperature31 = (string)value; break;
						case "Temperature32": this.str.Temperature32 = (string)value; break;
						case "Temperature33": this.str.Temperature33 = (string)value; break;
						case "Temperature34": this.str.Temperature34 = (string)value; break;
						case "TemperaturePost4": this.str.TemperaturePost4 = (string)value; break;
						case "Respiratory31": this.str.Respiratory31 = (string)value; break;
						case "Respiratory32": this.str.Respiratory32 = (string)value; break;
						case "Respiratory33": this.str.Respiratory33 = (string)value; break;
						case "Respiratory34": this.str.Respiratory34 = (string)value; break;
						case "RespiratoryPost4": this.str.RespiratoryPost4 = (string)value; break;
						case "IsFeverPost4": this.str.IsFeverPost4 = (string)value; break;
						case "IsFeverPost5": this.str.IsFeverPost5 = (string)value; break;
						case "IsFeverPost6": this.str.IsFeverPost6 = (string)value; break;
						case "IsFeverPost7": this.str.IsFeverPost7 = (string)value; break;
						case "IsFeverPost8": this.str.IsFeverPost8 = (string)value; break;
						case "IsShiveringPost4": this.str.IsShiveringPost4 = (string)value; break;
						case "IsShiveringPost5": this.str.IsShiveringPost5 = (string)value; break;
						case "IsShiveringPost6": this.str.IsShiveringPost6 = (string)value; break;
						case "IsShiveringPost7": this.str.IsShiveringPost7 = (string)value; break;
						case "IsShiveringPost8": this.str.IsShiveringPost8 = (string)value; break;
						case "IsSobPost4": this.str.IsSobPost4 = (string)value; break;
						case "IsSobPost5": this.str.IsSobPost5 = (string)value; break;
						case "IsSobPost6": this.str.IsSobPost6 = (string)value; break;
						case "IsSobPost7": this.str.IsSobPost7 = (string)value; break;
						case "IsSobPost8": this.str.IsSobPost8 = (string)value; break;
						case "IsPainfulPost4": this.str.IsPainfulPost4 = (string)value; break;
						case "IsPainfulPost5": this.str.IsPainfulPost5 = (string)value; break;
						case "IsPainfulPost6": this.str.IsPainfulPost6 = (string)value; break;
						case "IsPainfulPost7": this.str.IsPainfulPost7 = (string)value; break;
						case "IsPainfulPost8": this.str.IsPainfulPost8 = (string)value; break;
						case "IsNauseaPost4": this.str.IsNauseaPost4 = (string)value; break;
						case "IsNauseaPost5": this.str.IsNauseaPost5 = (string)value; break;
						case "IsNauseaPost6": this.str.IsNauseaPost6 = (string)value; break;
						case "IsNauseaPost7": this.str.IsNauseaPost7 = (string)value; break;
						case "IsNauseaPost8": this.str.IsNauseaPost8 = (string)value; break;
						case "IsBleedingPost4": this.str.IsBleedingPost4 = (string)value; break;
						case "IsBleedingPost5": this.str.IsBleedingPost5 = (string)value; break;
						case "IsBleedingPost6": this.str.IsBleedingPost6 = (string)value; break;
						case "IsBleedingPost7": this.str.IsBleedingPost7 = (string)value; break;
						case "IsBleedingPost8": this.str.IsBleedingPost8 = (string)value; break;
						case "IsHypotensionPost4": this.str.IsHypotensionPost4 = (string)value; break;
						case "IsHypotensionPost5": this.str.IsHypotensionPost5 = (string)value; break;
						case "IsHypotensionPost6": this.str.IsHypotensionPost6 = (string)value; break;
						case "IsHypotensionPost7": this.str.IsHypotensionPost7 = (string)value; break;
						case "IsHypotensionPost8": this.str.IsHypotensionPost8 = (string)value; break;
						case "IsShockPost4": this.str.IsShockPost4 = (string)value; break;
						case "IsShockPost5": this.str.IsShockPost5 = (string)value; break;
						case "IsShockPost6": this.str.IsShockPost6 = (string)value; break;
						case "IsShockPost7": this.str.IsShockPost7 = (string)value; break;
						case "IsShockPost8": this.str.IsShockPost8 = (string)value; break;
						case "IsUrticariaPost4": this.str.IsUrticariaPost4 = (string)value; break;
						case "IsUrticariaPost5": this.str.IsUrticariaPost5 = (string)value; break;
						case "IsUrticariaPost6": this.str.IsUrticariaPost6 = (string)value; break;
						case "IsUrticariaPost7": this.str.IsUrticariaPost7 = (string)value; break;
						case "IsUrticariaPost8": this.str.IsUrticariaPost8 = (string)value; break;
						case "IsRashPost4": this.str.IsRashPost4 = (string)value; break;
						case "IsRashPost5": this.str.IsRashPost5 = (string)value; break;
						case "IsRashPost6": this.str.IsRashPost6 = (string)value; break;
						case "IsRashPost7": this.str.IsRashPost7 = (string)value; break;
						case "IsRashPost8": this.str.IsRashPost8 = (string)value; break;
						case "IsPruritusPost4": this.str.IsPruritusPost4 = (string)value; break;
						case "IsPruritusPost5": this.str.IsPruritusPost5 = (string)value; break;
						case "IsPruritusPost6": this.str.IsPruritusPost6 = (string)value; break;
						case "IsPruritusPost7": this.str.IsPruritusPost7 = (string)value; break;
						case "IsPruritusPost8": this.str.IsPruritusPost8 = (string)value; break;
						case "IsAnxiousPost4": this.str.IsAnxiousPost4 = (string)value; break;
						case "IsAnxiousPost5": this.str.IsAnxiousPost5 = (string)value; break;
						case "IsAnxiousPost6": this.str.IsAnxiousPost6 = (string)value; break;
						case "IsAnxiousPost7": this.str.IsAnxiousPost7 = (string)value; break;
						case "IsAnxiousPost8": this.str.IsAnxiousPost8 = (string)value; break;
						case "IsWeakPost4": this.str.IsWeakPost4 = (string)value; break;
						case "IsWeakPost5": this.str.IsWeakPost5 = (string)value; break;
						case "IsWeakPost6": this.str.IsWeakPost6 = (string)value; break;
						case "IsWeakPost7": this.str.IsWeakPost7 = (string)value; break;
						case "IsWeakPost8": this.str.IsWeakPost8 = (string)value; break;
						case "IsPalpitationsPost4": this.str.IsPalpitationsPost4 = (string)value; break;
						case "IsPalpitationsPost5": this.str.IsPalpitationsPost5 = (string)value; break;
						case "IsPalpitationsPost6": this.str.IsPalpitationsPost6 = (string)value; break;
						case "IsPalpitationsPost7": this.str.IsPalpitationsPost7 = (string)value; break;
						case "IsPalpitationsPost8": this.str.IsPalpitationsPost8 = (string)value; break;
						case "IsMildDyspneaPost4": this.str.IsMildDyspneaPost4 = (string)value; break;
						case "IsMildDyspneaPost5": this.str.IsMildDyspneaPost5 = (string)value; break;
						case "IsMildDyspneaPost6": this.str.IsMildDyspneaPost6 = (string)value; break;
						case "IsMildDyspneaPost7": this.str.IsMildDyspneaPost7 = (string)value; break;
						case "IsMildDyspneaPost8": this.str.IsMildDyspneaPost8 = (string)value; break;
						case "IsHeadachePost4": this.str.IsHeadachePost4 = (string)value; break;
						case "IsHeadachePost5": this.str.IsHeadachePost5 = (string)value; break;
						case "IsHeadachePost6": this.str.IsHeadachePost6 = (string)value; break;
						case "IsHeadachePost7": this.str.IsHeadachePost7 = (string)value; break;
						case "IsHeadachePost8": this.str.IsHeadachePost8 = (string)value; break;
						case "IsRednessOnTheSkinPost4": this.str.IsRednessOnTheSkinPost4 = (string)value; break;
						case "IsRednessOnTheSkinPost5": this.str.IsRednessOnTheSkinPost5 = (string)value; break;
						case "IsRednessOnTheSkinPost6": this.str.IsRednessOnTheSkinPost6 = (string)value; break;
						case "IsRednessOnTheSkinPost7": this.str.IsRednessOnTheSkinPost7 = (string)value; break;
						case "IsRednessOnTheSkinPost8": this.str.IsRednessOnTheSkinPost8 = (string)value; break;
						case "IsTachycardiaPost4": this.str.IsTachycardiaPost4 = (string)value; break;
						case "IsTachycardiaPost5": this.str.IsTachycardiaPost5 = (string)value; break;
						case "IsTachycardiaPost6": this.str.IsTachycardiaPost6 = (string)value; break;
						case "IsTachycardiaPost7": this.str.IsTachycardiaPost7 = (string)value; break;
						case "IsTachycardiaPost8": this.str.IsTachycardiaPost8 = (string)value; break;
						case "IsMuscleStiffnessPost4": this.str.IsMuscleStiffnessPost4 = (string)value; break;
						case "IsMuscleStiffnessPost5": this.str.IsMuscleStiffnessPost5 = (string)value; break;
						case "IsMuscleStiffnessPost6": this.str.IsMuscleStiffnessPost6 = (string)value; break;
						case "IsMuscleStiffnessPost7": this.str.IsMuscleStiffnessPost7 = (string)value; break;
						case "IsMuscleStiffnessPost8": this.str.IsMuscleStiffnessPost8 = (string)value; break;
						case "OtherReactionPost4": this.str.OtherReactionPost4 = (string)value; break;
						case "OtherReactionPost5": this.str.OtherReactionPost5 = (string)value; break;
						case "OtherReactionPost6": this.str.OtherReactionPost6 = (string)value; break;
						case "OtherReactionPost7": this.str.OtherReactionPost7 = (string)value; break;
						case "OtherReactionPost8": this.str.OtherReactionPost8 = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ReceivedDate":

							if (value == null || value is System.DateTime)
								this.ReceivedDate = (System.DateTime?)value;
							break;
						case "IsScreeningLabelPassedPmi":

							if (value == null || value is System.Boolean)
								this.IsScreeningLabelPassedPmi = (System.Boolean?)value;
							break;
						case "IsExpiredDate":

							if (value == null || value is System.Boolean)
								this.IsExpiredDate = (System.Boolean?)value;
							break;
						case "IsLeak":

							if (value == null || value is System.Boolean)
								this.IsLeak = (System.Boolean?)value;
							break;
						case "IsHemolysis":

							if (value == null || value is System.Boolean)
								this.IsHemolysis = (System.Boolean?)value;
							break;
						case "IsCrossMatchingSuitability":

							if (value == null || value is System.Boolean)
								this.IsCrossMatchingSuitability = (System.Boolean?)value;
							break;
						case "IsClotting":

							if (value == null || value is System.Boolean)
								this.IsClotting = (System.Boolean?)value;
							break;
						case "IsBloodTypeCompatibility":

							if (value == null || value is System.Boolean)
								this.IsBloodTypeCompatibility = (System.Boolean?)value;
							break;
						case "IsLabelDonorsMatchesWithPatientsForm":

							if (value == null || value is System.Boolean)
								this.IsLabelDonorsMatchesWithPatientsForm = (System.Boolean?)value;
							break;
						case "IsScreeningLabelPassedBd":

							if (value == null || value is System.Boolean)
								this.IsScreeningLabelPassedBd = (System.Boolean?)value;
							break;
						case "TransfusionStartDateTime":

							if (value == null || value is System.DateTime)
								this.TransfusionStartDateTime = (System.DateTime?)value;
							break;
						case "TransfusionEndDateTime":

							if (value == null || value is System.DateTime)
								this.TransfusionEndDateTime = (System.DateTime?)value;
							break;
						case "QtyTransfusion":

							if (value == null || value is System.Int16)
								this.QtyTransfusion = (System.Int16?)value;
							break;
						case "CrossmatchCompatibleMinorLevel":

							if (value == null || value is System.Int16)
								this.CrossmatchCompatibleMinorLevel = (System.Int16?)value;
							break;
						case "CrossmatchCompatibleAutoControlLevel":

							if (value == null || value is System.Int16)
								this.CrossmatchCompatibleAutoControlLevel = (System.Int16?)value;
							break;
						case "CrossmatchCompatibleDctControlLevel":

							if (value == null || value is System.Int16)
								this.CrossmatchCompatibleDctControlLevel = (System.Int16?)value;
							break;
						case "CrossmatchStartDateTime":

							if (value == null || value is System.DateTime)
								this.CrossmatchStartDateTime = (System.DateTime?)value;
							break;
						case "CrossmatchEndDateTime":

							if (value == null || value is System.DateTime)
								this.CrossmatchEndDateTime = (System.DateTime?)value;
							break;
						case "IsCrossmatchingPassed":

							if (value == null || value is System.Boolean)
								this.IsCrossmatchingPassed = (System.Boolean?)value;
							break;
						case "BloodBagTemperature":

							if (value == null || value is System.Decimal)
								this.BloodBagTemperature = (System.Decimal?)value;
							break;
						case "IsProceedToTransfusion":

							if (value == null || value is System.Boolean)
								this.IsProceedToTransfusion = (System.Boolean?)value;
							break;
						case "PulsePre":

							if (value == null || value is System.Decimal)
								this.PulsePre = (System.Decimal?)value;
							break;
						case "Pulse10":

							if (value == null || value is System.Decimal)
								this.Pulse10 = (System.Decimal?)value;
							break;
						case "Pulse30":

							if (value == null || value is System.Decimal)
								this.Pulse30 = (System.Decimal?)value;
							break;
						case "Pulse60":

							if (value == null || value is System.Decimal)
								this.Pulse60 = (System.Decimal?)value;
							break;
						case "Pulse120":

							if (value == null || value is System.Decimal)
								this.Pulse120 = (System.Decimal?)value;
							break;
						case "Pulse180":

							if (value == null || value is System.Decimal)
								this.Pulse180 = (System.Decimal?)value;
							break;
						case "Pulse240":

							if (value == null || value is System.Decimal)
								this.Pulse240 = (System.Decimal?)value;
							break;
						case "PulsePost":

							if (value == null || value is System.Decimal)
								this.PulsePost = (System.Decimal?)value;
							break;
						case "PulsePost2":

							if (value == null || value is System.Decimal)
								this.PulsePost2 = (System.Decimal?)value;
							break;
						case "PulsePost3":

							if (value == null || value is System.Decimal)
								this.PulsePost3 = (System.Decimal?)value;
							break;
						case "TemperaturePre":

							if (value == null || value is System.Decimal)
								this.TemperaturePre = (System.Decimal?)value;
							break;
						case "Temperature10":

							if (value == null || value is System.Decimal)
								this.Temperature10 = (System.Decimal?)value;
							break;
						case "Temperature30":

							if (value == null || value is System.Decimal)
								this.Temperature30 = (System.Decimal?)value;
							break;
						case "Temperature60":

							if (value == null || value is System.Decimal)
								this.Temperature60 = (System.Decimal?)value;
							break;
						case "Temperature120":

							if (value == null || value is System.Decimal)
								this.Temperature120 = (System.Decimal?)value;
							break;
						case "Temperature180":

							if (value == null || value is System.Decimal)
								this.Temperature180 = (System.Decimal?)value;
							break;
						case "Temperature240":

							if (value == null || value is System.Decimal)
								this.Temperature240 = (System.Decimal?)value;
							break;
						case "TemperaturePost":

							if (value == null || value is System.Decimal)
								this.TemperaturePost = (System.Decimal?)value;
							break;
						case "TemperaturePost2":

							if (value == null || value is System.Decimal)
								this.TemperaturePost2 = (System.Decimal?)value;
							break;
						case "TemperaturePost3":

							if (value == null || value is System.Decimal)
								this.TemperaturePost3 = (System.Decimal?)value;
							break;
						case "RespiratoryPre":

							if (value == null || value is System.Decimal)
								this.RespiratoryPre = (System.Decimal?)value;
							break;
						case "Respiratory10":

							if (value == null || value is System.Decimal)
								this.Respiratory10 = (System.Decimal?)value;
							break;
						case "Respiratory30":

							if (value == null || value is System.Decimal)
								this.Respiratory30 = (System.Decimal?)value;
							break;
						case "Respiratory60":

							if (value == null || value is System.Decimal)
								this.Respiratory60 = (System.Decimal?)value;
							break;
						case "Respiratory120":

							if (value == null || value is System.Decimal)
								this.Respiratory120 = (System.Decimal?)value;
							break;
						case "Respiratory180":

							if (value == null || value is System.Decimal)
								this.Respiratory180 = (System.Decimal?)value;
							break;
						case "Respiratory240":

							if (value == null || value is System.Decimal)
								this.Respiratory240 = (System.Decimal?)value;
							break;
						case "RespiratoryPost":

							if (value == null || value is System.Decimal)
								this.RespiratoryPost = (System.Decimal?)value;
							break;
						case "RespiratoryPost2":

							if (value == null || value is System.Decimal)
								this.RespiratoryPost2 = (System.Decimal?)value;
							break;
						case "RespiratoryPost3":

							if (value == null || value is System.Decimal)
								this.RespiratoryPost3 = (System.Decimal?)value;
							break;
						case "IsFeverPre":

							if (value == null || value is System.Boolean)
								this.IsFeverPre = (System.Boolean?)value;
							break;
						case "IsFever10":

							if (value == null || value is System.Boolean)
								this.IsFever10 = (System.Boolean?)value;
							break;
						case "IsFever30":

							if (value == null || value is System.Boolean)
								this.IsFever30 = (System.Boolean?)value;
							break;
						case "IsFever60":

							if (value == null || value is System.Boolean)
								this.IsFever60 = (System.Boolean?)value;
							break;
						case "IsFever120":

							if (value == null || value is System.Boolean)
								this.IsFever120 = (System.Boolean?)value;
							break;
						case "IsFever180":

							if (value == null || value is System.Boolean)
								this.IsFever180 = (System.Boolean?)value;
							break;
						case "IsFever240":

							if (value == null || value is System.Boolean)
								this.IsFever240 = (System.Boolean?)value;
							break;
						case "IsFeverPost":

							if (value == null || value is System.Boolean)
								this.IsFeverPost = (System.Boolean?)value;
							break;
						case "IsFeverPost2":

							if (value == null || value is System.Boolean)
								this.IsFeverPost2 = (System.Boolean?)value;
							break;
						case "IsFeverPost3":

							if (value == null || value is System.Boolean)
								this.IsFeverPost3 = (System.Boolean?)value;
							break;
						case "IsShiveringPre":

							if (value == null || value is System.Boolean)
								this.IsShiveringPre = (System.Boolean?)value;
							break;
						case "IsShivering10":

							if (value == null || value is System.Boolean)
								this.IsShivering10 = (System.Boolean?)value;
							break;
						case "IsShivering30":

							if (value == null || value is System.Boolean)
								this.IsShivering30 = (System.Boolean?)value;
							break;
						case "IsShivering60":

							if (value == null || value is System.Boolean)
								this.IsShivering60 = (System.Boolean?)value;
							break;
						case "IsShivering120":

							if (value == null || value is System.Boolean)
								this.IsShivering120 = (System.Boolean?)value;
							break;
						case "IsShivering180":

							if (value == null || value is System.Boolean)
								this.IsShivering180 = (System.Boolean?)value;
							break;
						case "IsShivering240":

							if (value == null || value is System.Boolean)
								this.IsShivering240 = (System.Boolean?)value;
							break;
						case "IsShiveringPost":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost = (System.Boolean?)value;
							break;
						case "IsShiveringPost2":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost2 = (System.Boolean?)value;
							break;
						case "IsShiveringPost3":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost3 = (System.Boolean?)value;
							break;
						case "IsSobPre":

							if (value == null || value is System.Boolean)
								this.IsSobPre = (System.Boolean?)value;
							break;
						case "IsSob10":

							if (value == null || value is System.Boolean)
								this.IsSob10 = (System.Boolean?)value;
							break;
						case "IsSob30":

							if (value == null || value is System.Boolean)
								this.IsSob30 = (System.Boolean?)value;
							break;
						case "IsSob60":

							if (value == null || value is System.Boolean)
								this.IsSob60 = (System.Boolean?)value;
							break;
						case "IsSob120":

							if (value == null || value is System.Boolean)
								this.IsSob120 = (System.Boolean?)value;
							break;
						case "IsSob180":

							if (value == null || value is System.Boolean)
								this.IsSob180 = (System.Boolean?)value;
							break;
						case "IsSob240":

							if (value == null || value is System.Boolean)
								this.IsSob240 = (System.Boolean?)value;
							break;
						case "IsSobPost":

							if (value == null || value is System.Boolean)
								this.IsSobPost = (System.Boolean?)value;
							break;
						case "IsSobPost2":

							if (value == null || value is System.Boolean)
								this.IsSobPost2 = (System.Boolean?)value;
							break;
						case "IsSobPost3":

							if (value == null || value is System.Boolean)
								this.IsSobPost3 = (System.Boolean?)value;
							break;
						case "IsPainfulPre":

							if (value == null || value is System.Boolean)
								this.IsPainfulPre = (System.Boolean?)value;
							break;
						case "IsPainful10":

							if (value == null || value is System.Boolean)
								this.IsPainful10 = (System.Boolean?)value;
							break;
						case "IsPainful30":

							if (value == null || value is System.Boolean)
								this.IsPainful30 = (System.Boolean?)value;
							break;
						case "IsPainful60":

							if (value == null || value is System.Boolean)
								this.IsPainful60 = (System.Boolean?)value;
							break;
						case "IsPainful120":

							if (value == null || value is System.Boolean)
								this.IsPainful120 = (System.Boolean?)value;
							break;
						case "IsPainful180":

							if (value == null || value is System.Boolean)
								this.IsPainful180 = (System.Boolean?)value;
							break;
						case "IsPainful240":

							if (value == null || value is System.Boolean)
								this.IsPainful240 = (System.Boolean?)value;
							break;
						case "IsPainfulPost":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost = (System.Boolean?)value;
							break;
						case "IsPainfulPost2":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost2 = (System.Boolean?)value;
							break;
						case "IsPainfulPost3":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost3 = (System.Boolean?)value;
							break;
						case "IsNauseaPre":

							if (value == null || value is System.Boolean)
								this.IsNauseaPre = (System.Boolean?)value;
							break;
						case "IsNausea10":

							if (value == null || value is System.Boolean)
								this.IsNausea10 = (System.Boolean?)value;
							break;
						case "IsNausea30":

							if (value == null || value is System.Boolean)
								this.IsNausea30 = (System.Boolean?)value;
							break;
						case "IsNausea60":

							if (value == null || value is System.Boolean)
								this.IsNausea60 = (System.Boolean?)value;
							break;
						case "IsNausea120":

							if (value == null || value is System.Boolean)
								this.IsNausea120 = (System.Boolean?)value;
							break;
						case "IsNausea180":

							if (value == null || value is System.Boolean)
								this.IsNausea180 = (System.Boolean?)value;
							break;
						case "IsNausea240":

							if (value == null || value is System.Boolean)
								this.IsNausea240 = (System.Boolean?)value;
							break;
						case "IsNauseaPost":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost = (System.Boolean?)value;
							break;
						case "IsNauseaPost2":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost2 = (System.Boolean?)value;
							break;
						case "IsNauseaPost3":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost3 = (System.Boolean?)value;
							break;
						case "IsBleedingPre":

							if (value == null || value is System.Boolean)
								this.IsBleedingPre = (System.Boolean?)value;
							break;
						case "IsBleeding10":

							if (value == null || value is System.Boolean)
								this.IsBleeding10 = (System.Boolean?)value;
							break;
						case "IsBleeding30":

							if (value == null || value is System.Boolean)
								this.IsBleeding30 = (System.Boolean?)value;
							break;
						case "IsBleeding60":

							if (value == null || value is System.Boolean)
								this.IsBleeding60 = (System.Boolean?)value;
							break;
						case "IsBleeding120":

							if (value == null || value is System.Boolean)
								this.IsBleeding120 = (System.Boolean?)value;
							break;
						case "IsBleeding180":

							if (value == null || value is System.Boolean)
								this.IsBleeding180 = (System.Boolean?)value;
							break;
						case "IsBleeding240":

							if (value == null || value is System.Boolean)
								this.IsBleeding240 = (System.Boolean?)value;
							break;
						case "IsBleedingPost":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost = (System.Boolean?)value;
							break;
						case "IsBleedingPost2":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost2 = (System.Boolean?)value;
							break;
						case "IsBleedingPost3":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost3 = (System.Boolean?)value;
							break;
						case "IsHypotensionPre":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPre = (System.Boolean?)value;
							break;
						case "IsHypotension10":

							if (value == null || value is System.Boolean)
								this.IsHypotension10 = (System.Boolean?)value;
							break;
						case "IsHypotension30":

							if (value == null || value is System.Boolean)
								this.IsHypotension30 = (System.Boolean?)value;
							break;
						case "IsHypotension60":

							if (value == null || value is System.Boolean)
								this.IsHypotension60 = (System.Boolean?)value;
							break;
						case "IsHypotension120":

							if (value == null || value is System.Boolean)
								this.IsHypotension120 = (System.Boolean?)value;
							break;
						case "IsHypotension180":

							if (value == null || value is System.Boolean)
								this.IsHypotension180 = (System.Boolean?)value;
							break;
						case "IsHypotension240":

							if (value == null || value is System.Boolean)
								this.IsHypotension240 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost = (System.Boolean?)value;
							break;
						case "IsHypotensionPost2":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost2 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost3":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost3 = (System.Boolean?)value;
							break;
						case "IsShockPre":

							if (value == null || value is System.Boolean)
								this.IsShockPre = (System.Boolean?)value;
							break;
						case "IsShock10":

							if (value == null || value is System.Boolean)
								this.IsShock10 = (System.Boolean?)value;
							break;
						case "IsShock30":

							if (value == null || value is System.Boolean)
								this.IsShock30 = (System.Boolean?)value;
							break;
						case "IsShock60":

							if (value == null || value is System.Boolean)
								this.IsShock60 = (System.Boolean?)value;
							break;
						case "IsShock120":

							if (value == null || value is System.Boolean)
								this.IsShock120 = (System.Boolean?)value;
							break;
						case "IsShock180":

							if (value == null || value is System.Boolean)
								this.IsShock180 = (System.Boolean?)value;
							break;
						case "IsShock240":

							if (value == null || value is System.Boolean)
								this.IsShock240 = (System.Boolean?)value;
							break;
						case "IsShockPost":

							if (value == null || value is System.Boolean)
								this.IsShockPost = (System.Boolean?)value;
							break;
						case "IsShockPost2":

							if (value == null || value is System.Boolean)
								this.IsShockPost2 = (System.Boolean?)value;
							break;
						case "IsShockPost3":

							if (value == null || value is System.Boolean)
								this.IsShockPost3 = (System.Boolean?)value;
							break;
						case "IsUrticariaPre":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPre = (System.Boolean?)value;
							break;
						case "IsUrticaria10":

							if (value == null || value is System.Boolean)
								this.IsUrticaria10 = (System.Boolean?)value;
							break;
						case "IsUrticaria30":

							if (value == null || value is System.Boolean)
								this.IsUrticaria30 = (System.Boolean?)value;
							break;
						case "IsUrticaria60":

							if (value == null || value is System.Boolean)
								this.IsUrticaria60 = (System.Boolean?)value;
							break;
						case "IsUrticaria120":

							if (value == null || value is System.Boolean)
								this.IsUrticaria120 = (System.Boolean?)value;
							break;
						case "IsUrticaria180":

							if (value == null || value is System.Boolean)
								this.IsUrticaria180 = (System.Boolean?)value;
							break;
						case "IsUrticaria240":

							if (value == null || value is System.Boolean)
								this.IsUrticaria240 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost = (System.Boolean?)value;
							break;
						case "IsUrticariaPost2":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost2 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost3":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost3 = (System.Boolean?)value;
							break;
						case "IsRashPre":

							if (value == null || value is System.Boolean)
								this.IsRashPre = (System.Boolean?)value;
							break;
						case "IsRash10":

							if (value == null || value is System.Boolean)
								this.IsRash10 = (System.Boolean?)value;
							break;
						case "IsRash30":

							if (value == null || value is System.Boolean)
								this.IsRash30 = (System.Boolean?)value;
							break;
						case "IsRash60":

							if (value == null || value is System.Boolean)
								this.IsRash60 = (System.Boolean?)value;
							break;
						case "IsRash120":

							if (value == null || value is System.Boolean)
								this.IsRash120 = (System.Boolean?)value;
							break;
						case "IsRash180":

							if (value == null || value is System.Boolean)
								this.IsRash180 = (System.Boolean?)value;
							break;
						case "IsRash240":

							if (value == null || value is System.Boolean)
								this.IsRash240 = (System.Boolean?)value;
							break;
						case "IsRashPost":

							if (value == null || value is System.Boolean)
								this.IsRashPost = (System.Boolean?)value;
							break;
						case "IsRashPost2":

							if (value == null || value is System.Boolean)
								this.IsRashPost2 = (System.Boolean?)value;
							break;
						case "IsRashPost3":

							if (value == null || value is System.Boolean)
								this.IsRashPost3 = (System.Boolean?)value;
							break;
						case "IsPruritusPre":

							if (value == null || value is System.Boolean)
								this.IsPruritusPre = (System.Boolean?)value;
							break;
						case "IsPruritus10":

							if (value == null || value is System.Boolean)
								this.IsPruritus10 = (System.Boolean?)value;
							break;
						case "IsPruritus30":

							if (value == null || value is System.Boolean)
								this.IsPruritus30 = (System.Boolean?)value;
							break;
						case "IsPruritus60":

							if (value == null || value is System.Boolean)
								this.IsPruritus60 = (System.Boolean?)value;
							break;
						case "IsPruritus120":

							if (value == null || value is System.Boolean)
								this.IsPruritus120 = (System.Boolean?)value;
							break;
						case "IsPruritus180":

							if (value == null || value is System.Boolean)
								this.IsPruritus180 = (System.Boolean?)value;
							break;
						case "IsPruritus240":

							if (value == null || value is System.Boolean)
								this.IsPruritus240 = (System.Boolean?)value;
							break;
						case "IsPruritusPost":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost = (System.Boolean?)value;
							break;
						case "IsPruritusPost2":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost2 = (System.Boolean?)value;
							break;
						case "IsPruritusPost3":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost3 = (System.Boolean?)value;
							break;
						case "IsAnxiousPre":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPre = (System.Boolean?)value;
							break;
						case "IsAnxious10":

							if (value == null || value is System.Boolean)
								this.IsAnxious10 = (System.Boolean?)value;
							break;
						case "IsAnxious30":

							if (value == null || value is System.Boolean)
								this.IsAnxious30 = (System.Boolean?)value;
							break;
						case "IsAnxious60":

							if (value == null || value is System.Boolean)
								this.IsAnxious60 = (System.Boolean?)value;
							break;
						case "IsAnxious120":

							if (value == null || value is System.Boolean)
								this.IsAnxious120 = (System.Boolean?)value;
							break;
						case "IsAnxious180":

							if (value == null || value is System.Boolean)
								this.IsAnxious180 = (System.Boolean?)value;
							break;
						case "IsAnxious240":

							if (value == null || value is System.Boolean)
								this.IsAnxious240 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost = (System.Boolean?)value;
							break;
						case "IsAnxiousPost2":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost2 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost3":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost3 = (System.Boolean?)value;
							break;
						case "IsWeakPre":

							if (value == null || value is System.Boolean)
								this.IsWeakPre = (System.Boolean?)value;
							break;
						case "IsWeak10":

							if (value == null || value is System.Boolean)
								this.IsWeak10 = (System.Boolean?)value;
							break;
						case "IsWeak30":

							if (value == null || value is System.Boolean)
								this.IsWeak30 = (System.Boolean?)value;
							break;
						case "IsWeak60":

							if (value == null || value is System.Boolean)
								this.IsWeak60 = (System.Boolean?)value;
							break;
						case "IsWeak120":

							if (value == null || value is System.Boolean)
								this.IsWeak120 = (System.Boolean?)value;
							break;
						case "IsWeak180":

							if (value == null || value is System.Boolean)
								this.IsWeak180 = (System.Boolean?)value;
							break;
						case "IsWeak240":

							if (value == null || value is System.Boolean)
								this.IsWeak240 = (System.Boolean?)value;
							break;
						case "IsWeakPost":

							if (value == null || value is System.Boolean)
								this.IsWeakPost = (System.Boolean?)value;
							break;
						case "IsWeakPost2":

							if (value == null || value is System.Boolean)
								this.IsWeakPost2 = (System.Boolean?)value;
							break;
						case "IsWeakPost3":

							if (value == null || value is System.Boolean)
								this.IsWeakPost3 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPre":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPre = (System.Boolean?)value;
							break;
						case "IsPalpitations10":

							if (value == null || value is System.Boolean)
								this.IsPalpitations10 = (System.Boolean?)value;
							break;
						case "IsPalpitations30":

							if (value == null || value is System.Boolean)
								this.IsPalpitations30 = (System.Boolean?)value;
							break;
						case "IsPalpitations60":

							if (value == null || value is System.Boolean)
								this.IsPalpitations60 = (System.Boolean?)value;
							break;
						case "IsPalpitations120":

							if (value == null || value is System.Boolean)
								this.IsPalpitations120 = (System.Boolean?)value;
							break;
						case "IsPalpitations180":

							if (value == null || value is System.Boolean)
								this.IsPalpitations180 = (System.Boolean?)value;
							break;
						case "IsPalpitations240":

							if (value == null || value is System.Boolean)
								this.IsPalpitations240 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost2":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost2 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost3":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost3 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPre":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPre = (System.Boolean?)value;
							break;
						case "IsMildDyspnea10":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea10 = (System.Boolean?)value;
							break;
						case "IsMildDyspnea30":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea30 = (System.Boolean?)value;
							break;
						case "IsMildDyspnea60":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea60 = (System.Boolean?)value;
							break;
						case "IsMildDyspnea120":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea120 = (System.Boolean?)value;
							break;
						case "IsMildDyspnea180":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea180 = (System.Boolean?)value;
							break;
						case "IsMildDyspnea240":

							if (value == null || value is System.Boolean)
								this.IsMildDyspnea240 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost2":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost2 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost3":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost3 = (System.Boolean?)value;
							break;
						case "IsHeadachePre":

							if (value == null || value is System.Boolean)
								this.IsHeadachePre = (System.Boolean?)value;
							break;
						case "IsHeadache10":

							if (value == null || value is System.Boolean)
								this.IsHeadache10 = (System.Boolean?)value;
							break;
						case "IsHeadache30":

							if (value == null || value is System.Boolean)
								this.IsHeadache30 = (System.Boolean?)value;
							break;
						case "IsHeadache60":

							if (value == null || value is System.Boolean)
								this.IsHeadache60 = (System.Boolean?)value;
							break;
						case "IsHeadache120":

							if (value == null || value is System.Boolean)
								this.IsHeadache120 = (System.Boolean?)value;
							break;
						case "IsHeadache180":

							if (value == null || value is System.Boolean)
								this.IsHeadache180 = (System.Boolean?)value;
							break;
						case "IsHeadache240":

							if (value == null || value is System.Boolean)
								this.IsHeadache240 = (System.Boolean?)value;
							break;
						case "IsHeadachePost":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost = (System.Boolean?)value;
							break;
						case "IsHeadachePost2":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost2 = (System.Boolean?)value;
							break;
						case "IsHeadachePost3":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost3 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPre":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPre = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin10":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin10 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin30":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin30 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin60":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin60 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin120":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin120 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin180":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin180 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkin240":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkin240 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost2":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost2 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost3":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost3 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPre":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPre = (System.Boolean?)value;
							break;
						case "IsTachycardia10":

							if (value == null || value is System.Boolean)
								this.IsTachycardia10 = (System.Boolean?)value;
							break;
						case "IsTachycardia30":

							if (value == null || value is System.Boolean)
								this.IsTachycardia30 = (System.Boolean?)value;
							break;
						case "IsTachycardia60":

							if (value == null || value is System.Boolean)
								this.IsTachycardia60 = (System.Boolean?)value;
							break;
						case "IsTachycardia120":

							if (value == null || value is System.Boolean)
								this.IsTachycardia120 = (System.Boolean?)value;
							break;
						case "IsTachycardia180":

							if (value == null || value is System.Boolean)
								this.IsTachycardia180 = (System.Boolean?)value;
							break;
						case "IsTachycardia240":

							if (value == null || value is System.Boolean)
								this.IsTachycardia240 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost2":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost2 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost3":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost3 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPre":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPre = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness10":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness10 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness30":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness30 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness60":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness60 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness120":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness120 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness180":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness180 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffness240":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffness240 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost2":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost2 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost3":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost3 = (System.Boolean?)value;
							break;
						case "HemoglobinPre":

							if (value == null || value is System.Decimal)
								this.HemoglobinPre = (System.Decimal?)value;
							break;
						case "Hemoglobin10":

							if (value == null || value is System.Decimal)
								this.Hemoglobin10 = (System.Decimal?)value;
							break;
						case "Hemoglobin30":

							if (value == null || value is System.Decimal)
								this.Hemoglobin30 = (System.Decimal?)value;
							break;
						case "Hemoglobin60":

							if (value == null || value is System.Decimal)
								this.Hemoglobin60 = (System.Decimal?)value;
							break;
						case "Hemoglobin120":

							if (value == null || value is System.Decimal)
								this.Hemoglobin120 = (System.Decimal?)value;
							break;
						case "Hemoglobin180":

							if (value == null || value is System.Decimal)
								this.Hemoglobin180 = (System.Decimal?)value;
							break;
						case "Hemoglobin240":

							if (value == null || value is System.Decimal)
								this.Hemoglobin240 = (System.Decimal?)value;
							break;
						case "HemoglobinPost":

							if (value == null || value is System.Decimal)
								this.HemoglobinPost = (System.Decimal?)value;
							break;
						case "HemoglobinPost2":

							if (value == null || value is System.Decimal)
								this.HemoglobinPost2 = (System.Decimal?)value;
							break;
						case "HemoglobinPost3":

							if (value == null || value is System.Decimal)
								this.HemoglobinPost3 = (System.Decimal?)value;
							break;
						case "HematocritPre":

							if (value == null || value is System.Decimal)
								this.HematocritPre = (System.Decimal?)value;
							break;
						case "Hematocrit10":

							if (value == null || value is System.Decimal)
								this.Hematocrit10 = (System.Decimal?)value;
							break;
						case "Hematocrit30":

							if (value == null || value is System.Decimal)
								this.Hematocrit30 = (System.Decimal?)value;
							break;
						case "Hematocrit60":

							if (value == null || value is System.Decimal)
								this.Hematocrit60 = (System.Decimal?)value;
							break;
						case "Hematocrit120":

							if (value == null || value is System.Decimal)
								this.Hematocrit120 = (System.Decimal?)value;
							break;
						case "Hematocrit180":

							if (value == null || value is System.Decimal)
								this.Hematocrit180 = (System.Decimal?)value;
							break;
						case "Hematocrit240":

							if (value == null || value is System.Decimal)
								this.Hematocrit240 = (System.Decimal?)value;
							break;
						case "HematocritPost":

							if (value == null || value is System.Decimal)
								this.HematocritPost = (System.Decimal?)value;
							break;
						case "HematocritPost2":

							if (value == null || value is System.Decimal)
								this.HematocritPost2 = (System.Decimal?)value;
							break;
						case "HematocritPost3":

							if (value == null || value is System.Decimal)
								this.HematocritPost3 = (System.Decimal?)value;
							break;
						case "PlateletPre":

							if (value == null || value is System.Decimal)
								this.PlateletPre = (System.Decimal?)value;
							break;
						case "Platelet10":

							if (value == null || value is System.Decimal)
								this.Platelet10 = (System.Decimal?)value;
							break;
						case "Platelet30":

							if (value == null || value is System.Decimal)
								this.Platelet30 = (System.Decimal?)value;
							break;
						case "Platelet60":

							if (value == null || value is System.Decimal)
								this.Platelet60 = (System.Decimal?)value;
							break;
						case "Platelet120":

							if (value == null || value is System.Decimal)
								this.Platelet120 = (System.Decimal?)value;
							break;
						case "Platelet180":

							if (value == null || value is System.Decimal)
								this.Platelet180 = (System.Decimal?)value;
							break;
						case "Platelet240":

							if (value == null || value is System.Decimal)
								this.Platelet240 = (System.Decimal?)value;
							break;
						case "PlateletPost":

							if (value == null || value is System.Decimal)
								this.PlateletPost = (System.Decimal?)value;
							break;
						case "PlateletPost2":

							if (value == null || value is System.Decimal)
								this.PlateletPost2 = (System.Decimal?)value;
							break;
						case "PlateletPost3":

							if (value == null || value is System.Decimal)
								this.PlateletPost3 = (System.Decimal?)value;
							break;
						case "IsHiv":

							if (value == null || value is System.Boolean)
								this.IsHiv = (System.Boolean?)value;
							break;
						case "IsVbrl":

							if (value == null || value is System.Boolean)
								this.IsVbrl = (System.Boolean?)value;
							break;
						case "IsHbsAg":

							if (value == null || value is System.Boolean)
								this.IsHbsAg = (System.Boolean?)value;
							break;
						case "IsHcv":

							if (value == null || value is System.Boolean)
								this.IsHcv = (System.Boolean?)value;
							break;
						case "IsReCheck":

							if (value == null || value is System.Boolean)
								this.IsReCheck = (System.Boolean?)value;
							break;
						case "ReCheckDateTime":

							if (value == null || value is System.DateTime)
								this.ReCheckDateTime = (System.DateTime?)value;
							break;
						case "IsReCheckExpiredDate":

							if (value == null || value is System.Boolean)
								this.IsReCheckExpiredDate = (System.Boolean?)value;
							break;
						case "IsReCheckLeak":

							if (value == null || value is System.Boolean)
								this.IsReCheckLeak = (System.Boolean?)value;
							break;
						case "IsReCheckHemolysis":

							if (value == null || value is System.Boolean)
								this.IsReCheckHemolysis = (System.Boolean?)value;
							break;
						case "IsReCheckCrossMatchingSuitability":

							if (value == null || value is System.Boolean)
								this.IsReCheckCrossMatchingSuitability = (System.Boolean?)value;
							break;
						case "IsReCheckClotting":

							if (value == null || value is System.Boolean)
								this.IsReCheckClotting = (System.Boolean?)value;
							break;
						case "IsReCheckBloodTypeCompatibility":

							if (value == null || value is System.Boolean)
								this.IsReCheckBloodTypeCompatibility = (System.Boolean?)value;
							break;
						case "IsCrossmatchBillProceed":

							if (value == null || value is System.Boolean)
								this.IsCrossmatchBillProceed = (System.Boolean?)value;
							break;
						case "IsTransfusionBillProceed":

							if (value == null || value is System.Boolean)
								this.IsTransfusionBillProceed = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsScreening1":

							if (value == null || value is System.Boolean)
								this.IsScreening1 = (System.Boolean?)value;
							break;
						case "IsScreening2":

							if (value == null || value is System.Boolean)
								this.IsScreening2 = (System.Boolean?)value;
							break;
						case "IsScreening3":

							if (value == null || value is System.Boolean)
								this.IsScreening3 = (System.Boolean?)value;
							break;
						case "Pulse31":

							if (value == null || value is System.Decimal)
								this.Pulse31 = (System.Decimal?)value;
							break;
						case "Pulse32":

							if (value == null || value is System.Decimal)
								this.Pulse32 = (System.Decimal?)value;
							break;
						case "Pulse33":

							if (value == null || value is System.Decimal)
								this.Pulse33 = (System.Decimal?)value;
							break;
						case "Pulse34":

							if (value == null || value is System.Decimal)
								this.Pulse34 = (System.Decimal?)value;
							break;
						case "PulsePost4":

							if (value == null || value is System.Decimal)
								this.PulsePost4 = (System.Decimal?)value;
							break;
						case "Temperature31":

							if (value == null || value is System.Decimal)
								this.Temperature31 = (System.Decimal?)value;
							break;
						case "Temperature32":

							if (value == null || value is System.Decimal)
								this.Temperature32 = (System.Decimal?)value;
							break;
						case "Temperature33":

							if (value == null || value is System.Decimal)
								this.Temperature33 = (System.Decimal?)value;
							break;
						case "Temperature34":

							if (value == null || value is System.Decimal)
								this.Temperature34 = (System.Decimal?)value;
							break;
						case "TemperaturePost4":

							if (value == null || value is System.Decimal)
								this.TemperaturePost4 = (System.Decimal?)value;
							break;
						case "Respiratory31":

							if (value == null || value is System.Decimal)
								this.Respiratory31 = (System.Decimal?)value;
							break;
						case "Respiratory32":

							if (value == null || value is System.Decimal)
								this.Respiratory32 = (System.Decimal?)value;
							break;
						case "Respiratory33":

							if (value == null || value is System.Decimal)
								this.Respiratory33 = (System.Decimal?)value;
							break;
						case "Respiratory34":

							if (value == null || value is System.Decimal)
								this.Respiratory34 = (System.Decimal?)value;
							break;
						case "RespiratoryPost4":

							if (value == null || value is System.Decimal)
								this.RespiratoryPost4 = (System.Decimal?)value;
							break;
						case "IsFeverPost4":

							if (value == null || value is System.Boolean)
								this.IsFeverPost4 = (System.Boolean?)value;
							break;
						case "IsFeverPost5":

							if (value == null || value is System.Boolean)
								this.IsFeverPost5 = (System.Boolean?)value;
							break;
						case "IsFeverPost6":

							if (value == null || value is System.Boolean)
								this.IsFeverPost6 = (System.Boolean?)value;
							break;
						case "IsFeverPost7":

							if (value == null || value is System.Boolean)
								this.IsFeverPost7 = (System.Boolean?)value;
							break;
						case "IsFeverPost8":

							if (value == null || value is System.Boolean)
								this.IsFeverPost8 = (System.Boolean?)value;
							break;
						case "IsShiveringPost4":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost4 = (System.Boolean?)value;
							break;
						case "IsShiveringPost5":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost5 = (System.Boolean?)value;
							break;
						case "IsShiveringPost6":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost6 = (System.Boolean?)value;
							break;
						case "IsShiveringPost7":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost7 = (System.Boolean?)value;
							break;
						case "IsShiveringPost8":

							if (value == null || value is System.Boolean)
								this.IsShiveringPost8 = (System.Boolean?)value;
							break;
						case "IsSobPost4":

							if (value == null || value is System.Boolean)
								this.IsSobPost4 = (System.Boolean?)value;
							break;
						case "IsSobPost5":

							if (value == null || value is System.Boolean)
								this.IsSobPost5 = (System.Boolean?)value;
							break;
						case "IsSobPost6":

							if (value == null || value is System.Boolean)
								this.IsSobPost6 = (System.Boolean?)value;
							break;
						case "IsSobPost7":

							if (value == null || value is System.Boolean)
								this.IsSobPost7 = (System.Boolean?)value;
							break;
						case "IsSobPost8":

							if (value == null || value is System.Boolean)
								this.IsSobPost8 = (System.Boolean?)value;
							break;
						case "IsPainfulPost4":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost4 = (System.Boolean?)value;
							break;
						case "IsPainfulPost5":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost5 = (System.Boolean?)value;
							break;
						case "IsPainfulPost6":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost6 = (System.Boolean?)value;
							break;
						case "IsPainfulPost7":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost7 = (System.Boolean?)value;
							break;
						case "IsPainfulPost8":

							if (value == null || value is System.Boolean)
								this.IsPainfulPost8 = (System.Boolean?)value;
							break;
						case "IsNauseaPost4":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost4 = (System.Boolean?)value;
							break;
						case "IsNauseaPost5":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost5 = (System.Boolean?)value;
							break;
						case "IsNauseaPost6":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost6 = (System.Boolean?)value;
							break;
						case "IsNauseaPost7":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost7 = (System.Boolean?)value;
							break;
						case "IsNauseaPost8":

							if (value == null || value is System.Boolean)
								this.IsNauseaPost8 = (System.Boolean?)value;
							break;
						case "IsBleedingPost4":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost4 = (System.Boolean?)value;
							break;
						case "IsBleedingPost5":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost5 = (System.Boolean?)value;
							break;
						case "IsBleedingPost6":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost6 = (System.Boolean?)value;
							break;
						case "IsBleedingPost7":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost7 = (System.Boolean?)value;
							break;
						case "IsBleedingPost8":

							if (value == null || value is System.Boolean)
								this.IsBleedingPost8 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost4":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost4 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost5":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost5 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost6":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost6 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost7":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost7 = (System.Boolean?)value;
							break;
						case "IsHypotensionPost8":

							if (value == null || value is System.Boolean)
								this.IsHypotensionPost8 = (System.Boolean?)value;
							break;
						case "IsShockPost4":

							if (value == null || value is System.Boolean)
								this.IsShockPost4 = (System.Boolean?)value;
							break;
						case "IsShockPost5":

							if (value == null || value is System.Boolean)
								this.IsShockPost5 = (System.Boolean?)value;
							break;
						case "IsShockPost6":

							if (value == null || value is System.Boolean)
								this.IsShockPost6 = (System.Boolean?)value;
							break;
						case "IsShockPost7":

							if (value == null || value is System.Boolean)
								this.IsShockPost7 = (System.Boolean?)value;
							break;
						case "IsShockPost8":

							if (value == null || value is System.Boolean)
								this.IsShockPost8 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost4":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost4 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost5":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost5 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost6":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost6 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost7":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost7 = (System.Boolean?)value;
							break;
						case "IsUrticariaPost8":

							if (value == null || value is System.Boolean)
								this.IsUrticariaPost8 = (System.Boolean?)value;
							break;
						case "IsRashPost4":

							if (value == null || value is System.Boolean)
								this.IsRashPost4 = (System.Boolean?)value;
							break;
						case "IsRashPost5":

							if (value == null || value is System.Boolean)
								this.IsRashPost5 = (System.Boolean?)value;
							break;
						case "IsRashPost6":

							if (value == null || value is System.Boolean)
								this.IsRashPost6 = (System.Boolean?)value;
							break;
						case "IsRashPost7":

							if (value == null || value is System.Boolean)
								this.IsRashPost7 = (System.Boolean?)value;
							break;
						case "IsRashPost8":

							if (value == null || value is System.Boolean)
								this.IsRashPost8 = (System.Boolean?)value;
							break;
						case "IsPruritusPost4":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost4 = (System.Boolean?)value;
							break;
						case "IsPruritusPost5":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost5 = (System.Boolean?)value;
							break;
						case "IsPruritusPost6":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost6 = (System.Boolean?)value;
							break;
						case "IsPruritusPost7":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost7 = (System.Boolean?)value;
							break;
						case "IsPruritusPost8":

							if (value == null || value is System.Boolean)
								this.IsPruritusPost8 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost4":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost4 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost5":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost5 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost6":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost6 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost7":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost7 = (System.Boolean?)value;
							break;
						case "IsAnxiousPost8":

							if (value == null || value is System.Boolean)
								this.IsAnxiousPost8 = (System.Boolean?)value;
							break;
						case "IsWeakPost4":

							if (value == null || value is System.Boolean)
								this.IsWeakPost4 = (System.Boolean?)value;
							break;
						case "IsWeakPost5":

							if (value == null || value is System.Boolean)
								this.IsWeakPost5 = (System.Boolean?)value;
							break;
						case "IsWeakPost6":

							if (value == null || value is System.Boolean)
								this.IsWeakPost6 = (System.Boolean?)value;
							break;
						case "IsWeakPost7":

							if (value == null || value is System.Boolean)
								this.IsWeakPost7 = (System.Boolean?)value;
							break;
						case "IsWeakPost8":

							if (value == null || value is System.Boolean)
								this.IsWeakPost8 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost4":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost4 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost5":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost5 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost6":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost6 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost7":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost7 = (System.Boolean?)value;
							break;
						case "IsPalpitationsPost8":

							if (value == null || value is System.Boolean)
								this.IsPalpitationsPost8 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost4":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost4 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost5":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost5 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost6":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost6 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost7":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost7 = (System.Boolean?)value;
							break;
						case "IsMildDyspneaPost8":

							if (value == null || value is System.Boolean)
								this.IsMildDyspneaPost8 = (System.Boolean?)value;
							break;
						case "IsHeadachePost4":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost4 = (System.Boolean?)value;
							break;
						case "IsHeadachePost5":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost5 = (System.Boolean?)value;
							break;
						case "IsHeadachePost6":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost6 = (System.Boolean?)value;
							break;
						case "IsHeadachePost7":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost7 = (System.Boolean?)value;
							break;
						case "IsHeadachePost8":

							if (value == null || value is System.Boolean)
								this.IsHeadachePost8 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost4":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost4 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost5":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost5 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost6":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost6 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost7":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost7 = (System.Boolean?)value;
							break;
						case "IsRednessOnTheSkinPost8":

							if (value == null || value is System.Boolean)
								this.IsRednessOnTheSkinPost8 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost4":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost4 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost5":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost5 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost6":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost6 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost7":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost7 = (System.Boolean?)value;
							break;
						case "IsTachycardiaPost8":

							if (value == null || value is System.Boolean)
								this.IsTachycardiaPost8 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost4":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost4 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost5":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost5 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost6":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost6 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost7":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost7 = (System.Boolean?)value;
							break;
						case "IsMuscleStiffnessPost8":

							if (value == null || value is System.Boolean)
								this.IsMuscleStiffnessPost8 = (System.Boolean?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to BloodBankTransactionItem.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BagNo
		/// </summary>
		virtual public System.String BagNo
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BagNo);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BagNo, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ReceivedDate
		/// </summary>
		virtual public System.DateTime? ReceivedDate
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.ReceivedDate);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.ReceivedDate, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ReceivedTime
		/// </summary>
		virtual public System.String ReceivedTime
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.SRBloodGroupReceived
		/// </summary>
		virtual public System.String SRBloodGroupReceived
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodGroupReceived);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodGroupReceived, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.SRBloodBagStatus
		/// </summary>
		virtual public System.String SRBloodBagStatus
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodBagStatus);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodBagStatus, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsScreeningLabelPassedPmi
		/// </summary>
		virtual public System.Boolean? IsScreeningLabelPassedPmi
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedPmi);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedPmi, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsExpiredDate
		/// </summary>
		virtual public System.Boolean? IsExpiredDate
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsExpiredDate);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsLeak
		/// </summary>
		virtual public System.Boolean? IsLeak
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsLeak);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsLeak, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHemolysis
		/// </summary>
		virtual public System.Boolean? IsHemolysis
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHemolysis);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHemolysis, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsCrossMatchingSuitability
		/// </summary>
		virtual public System.Boolean? IsCrossMatchingSuitability
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsClotting
		/// </summary>
		virtual public System.Boolean? IsClotting
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsClotting);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsClotting, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBloodTypeCompatibility
		/// </summary>
		virtual public System.Boolean? IsBloodTypeCompatibility
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBloodTypeCompatibility);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBloodTypeCompatibility, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsLabelDonorsMatchesWithPatientsForm
		/// </summary>
		virtual public System.Boolean? IsLabelDonorsMatchesWithPatientsForm
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsLabelDonorsMatchesWithPatientsForm);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsLabelDonorsMatchesWithPatientsForm, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsScreeningLabelPassedBd
		/// </summary>
		virtual public System.Boolean? IsScreeningLabelPassedBd
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedBd);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedBd, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ExaminerByUserID
		/// </summary>
		virtual public System.String ExaminerByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ExaminerByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ExaminerByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.UnitOfficer
		/// </summary>
		virtual public System.String UnitOfficer
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficer);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficer, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TransfusionStartDateTime
		/// </summary>
		virtual public System.DateTime? TransfusionStartDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.TransfusionStartDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.TransfusionStartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TransfusionEndDateTime
		/// </summary>
		virtual public System.DateTime? TransfusionEndDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.TransfusionEndDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.TransfusionEndDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TransfusedOfficerStartBy
		/// </summary>
		virtual public System.String TransfusedOfficerStartBy
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerStartBy);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerStartBy, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TransfusedOfficerEndBy
		/// </summary>
		virtual public System.String TransfusedOfficerEndBy
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerEndBy);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerEndBy, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.QtyTransfusion
		/// </summary>
		virtual public System.Int16? QtyTransfusion
		{
			get
			{
				return base.GetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.QtyTransfusion);
			}

			set
			{
				base.SetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.QtyTransfusion, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.SRBloodSource
		/// </summary>
		virtual public System.String SRBloodSource
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSource);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSource, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.SRBloodSourceFrom
		/// </summary>
		virtual public System.String SRBloodSourceFrom
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSourceFrom);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSourceFrom, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleMajor
		/// </summary>
		virtual public System.String CrossmatchCompatibleMajor
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMajor);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMajor, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleMinor
		/// </summary>
		virtual public System.String CrossmatchCompatibleMinor
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinor);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinor, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleMinorLevel
		/// </summary>
		virtual public System.Int16? CrossmatchCompatibleMinorLevel
		{
			get
			{
				return base.GetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinorLevel);
			}

			set
			{
				base.SetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinorLevel, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleAutoControl
		/// </summary>
		virtual public System.String CrossmatchCompatibleAutoControl
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControl);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControl, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleAutoControlLevel
		/// </summary>
		virtual public System.Int16? CrossmatchCompatibleAutoControlLevel
		{
			get
			{
				return base.GetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControlLevel);
			}

			set
			{
				base.SetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControlLevel, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleDctControl
		/// </summary>
		virtual public System.String CrossmatchCompatibleDctControl
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControl);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControl, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchCompatibleDctControlLevel
		/// </summary>
		virtual public System.Int16? CrossmatchCompatibleDctControlLevel
		{
			get
			{
				return base.GetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControlLevel);
			}

			set
			{
				base.SetSystemInt16(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControlLevel, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchStartDateTime
		/// </summary>
		virtual public System.DateTime? CrossmatchStartDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossmatchEndDateTime
		/// </summary>
		virtual public System.DateTime? CrossmatchEndDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsCrossmatchingPassed
		/// </summary>
		virtual public System.Boolean? IsCrossmatchingPassed
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchingPassed);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchingPassed, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.CrossMatchingByUserID
		/// </summary>
		virtual public System.String CrossMatchingByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossMatchingByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.CrossMatchingByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BloodBagTemperature
		/// </summary>
		virtual public System.Decimal? BloodBagTemperature
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.BloodBagTemperature);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.BloodBagTemperature, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BloodBagNotes
		/// </summary>
		virtual public System.String BloodBagNotes
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BloodBagNotes);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BloodBagNotes, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsProceedToTransfusion
		/// </summary>
		virtual public System.Boolean? IsProceedToTransfusion
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsProceedToTransfusion);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsProceedToTransfusion, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BpPre
		/// </summary>
		virtual public System.String BpPre
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPre);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp10
		/// </summary>
		virtual public System.String Bp10
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp10);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp30
		/// </summary>
		virtual public System.String Bp30
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp30);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp60
		/// </summary>
		virtual public System.String Bp60
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp60);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp120
		/// </summary>
		virtual public System.String Bp120
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp120);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp180
		/// </summary>
		virtual public System.String Bp180
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp180);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp240
		/// </summary>
		virtual public System.String Bp240
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp240);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BpPost
		/// </summary>
		virtual public System.String BpPost
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BpPost2
		/// </summary>
		virtual public System.String BpPost2
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost2);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BpPost3
		/// </summary>
		virtual public System.String BpPost3
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost3);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PulsePre
		/// </summary>
		virtual public System.Decimal? PulsePre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse10
		/// </summary>
		virtual public System.Decimal? Pulse10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse30
		/// </summary>
		virtual public System.Decimal? Pulse30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse60
		/// </summary>
		virtual public System.Decimal? Pulse60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse120
		/// </summary>
		virtual public System.Decimal? Pulse120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse180
		/// </summary>
		virtual public System.Decimal? Pulse180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse240
		/// </summary>
		virtual public System.Decimal? Pulse240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PulsePost
		/// </summary>
		virtual public System.Decimal? PulsePost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PulsePost2
		/// </summary>
		virtual public System.Decimal? PulsePost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PulsePost3
		/// </summary>
		virtual public System.Decimal? PulsePost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TemperaturePre
		/// </summary>
		virtual public System.Decimal? TemperaturePre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature10
		/// </summary>
		virtual public System.Decimal? Temperature10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature30
		/// </summary>
		virtual public System.Decimal? Temperature30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature60
		/// </summary>
		virtual public System.Decimal? Temperature60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature120
		/// </summary>
		virtual public System.Decimal? Temperature120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature180
		/// </summary>
		virtual public System.Decimal? Temperature180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature240
		/// </summary>
		virtual public System.Decimal? Temperature240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TemperaturePost
		/// </summary>
		virtual public System.Decimal? TemperaturePost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TemperaturePost2
		/// </summary>
		virtual public System.Decimal? TemperaturePost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TemperaturePost3
		/// </summary>
		virtual public System.Decimal? TemperaturePost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.RespiratoryPre
		/// </summary>
		virtual public System.Decimal? RespiratoryPre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory10
		/// </summary>
		virtual public System.Decimal? Respiratory10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory30
		/// </summary>
		virtual public System.Decimal? Respiratory30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory60
		/// </summary>
		virtual public System.Decimal? Respiratory60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory120
		/// </summary>
		virtual public System.Decimal? Respiratory120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory180
		/// </summary>
		virtual public System.Decimal? Respiratory180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory240
		/// </summary>
		virtual public System.Decimal? Respiratory240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.RespiratoryPost
		/// </summary>
		virtual public System.Decimal? RespiratoryPost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.RespiratoryPost2
		/// </summary>
		virtual public System.Decimal? RespiratoryPost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.RespiratoryPost3
		/// </summary>
		virtual public System.Decimal? RespiratoryPost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPre
		/// </summary>
		virtual public System.Boolean? IsFeverPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever10
		/// </summary>
		virtual public System.Boolean? IsFever10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever30
		/// </summary>
		virtual public System.Boolean? IsFever30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever60
		/// </summary>
		virtual public System.Boolean? IsFever60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever120
		/// </summary>
		virtual public System.Boolean? IsFever120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever180
		/// </summary>
		virtual public System.Boolean? IsFever180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFever240
		/// </summary>
		virtual public System.Boolean? IsFever240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFever240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost
		/// </summary>
		virtual public System.Boolean? IsFeverPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost2
		/// </summary>
		virtual public System.Boolean? IsFeverPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost3
		/// </summary>
		virtual public System.Boolean? IsFeverPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPre
		/// </summary>
		virtual public System.Boolean? IsShiveringPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering10
		/// </summary>
		virtual public System.Boolean? IsShivering10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering30
		/// </summary>
		virtual public System.Boolean? IsShivering30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering60
		/// </summary>
		virtual public System.Boolean? IsShivering60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering120
		/// </summary>
		virtual public System.Boolean? IsShivering120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering180
		/// </summary>
		virtual public System.Boolean? IsShivering180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShivering240
		/// </summary>
		virtual public System.Boolean? IsShivering240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShivering240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost
		/// </summary>
		virtual public System.Boolean? IsShiveringPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost2
		/// </summary>
		virtual public System.Boolean? IsShiveringPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost3
		/// </summary>
		virtual public System.Boolean? IsShiveringPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPre
		/// </summary>
		virtual public System.Boolean? IsSobPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob10
		/// </summary>
		virtual public System.Boolean? IsSob10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob30
		/// </summary>
		virtual public System.Boolean? IsSob30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob60
		/// </summary>
		virtual public System.Boolean? IsSob60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob120
		/// </summary>
		virtual public System.Boolean? IsSob120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob180
		/// </summary>
		virtual public System.Boolean? IsSob180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSob240
		/// </summary>
		virtual public System.Boolean? IsSob240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSob240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost
		/// </summary>
		virtual public System.Boolean? IsSobPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost2
		/// </summary>
		virtual public System.Boolean? IsSobPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost3
		/// </summary>
		virtual public System.Boolean? IsSobPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPre
		/// </summary>
		virtual public System.Boolean? IsPainfulPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful10
		/// </summary>
		virtual public System.Boolean? IsPainful10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful30
		/// </summary>
		virtual public System.Boolean? IsPainful30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful60
		/// </summary>
		virtual public System.Boolean? IsPainful60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful120
		/// </summary>
		virtual public System.Boolean? IsPainful120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful180
		/// </summary>
		virtual public System.Boolean? IsPainful180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainful240
		/// </summary>
		virtual public System.Boolean? IsPainful240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainful240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost
		/// </summary>
		virtual public System.Boolean? IsPainfulPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost2
		/// </summary>
		virtual public System.Boolean? IsPainfulPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost3
		/// </summary>
		virtual public System.Boolean? IsPainfulPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPre
		/// </summary>
		virtual public System.Boolean? IsNauseaPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea10
		/// </summary>
		virtual public System.Boolean? IsNausea10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea30
		/// </summary>
		virtual public System.Boolean? IsNausea30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea60
		/// </summary>
		virtual public System.Boolean? IsNausea60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea120
		/// </summary>
		virtual public System.Boolean? IsNausea120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea180
		/// </summary>
		virtual public System.Boolean? IsNausea180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNausea240
		/// </summary>
		virtual public System.Boolean? IsNausea240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNausea240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost
		/// </summary>
		virtual public System.Boolean? IsNauseaPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost2
		/// </summary>
		virtual public System.Boolean? IsNauseaPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost3
		/// </summary>
		virtual public System.Boolean? IsNauseaPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPre
		/// </summary>
		virtual public System.Boolean? IsBleedingPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding10
		/// </summary>
		virtual public System.Boolean? IsBleeding10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding30
		/// </summary>
		virtual public System.Boolean? IsBleeding30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding60
		/// </summary>
		virtual public System.Boolean? IsBleeding60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding120
		/// </summary>
		virtual public System.Boolean? IsBleeding120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding180
		/// </summary>
		virtual public System.Boolean? IsBleeding180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleeding240
		/// </summary>
		virtual public System.Boolean? IsBleeding240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost
		/// </summary>
		virtual public System.Boolean? IsBleedingPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost2
		/// </summary>
		virtual public System.Boolean? IsBleedingPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost3
		/// </summary>
		virtual public System.Boolean? IsBleedingPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPre
		/// </summary>
		virtual public System.Boolean? IsHypotensionPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension10
		/// </summary>
		virtual public System.Boolean? IsHypotension10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension30
		/// </summary>
		virtual public System.Boolean? IsHypotension30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension60
		/// </summary>
		virtual public System.Boolean? IsHypotension60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension120
		/// </summary>
		virtual public System.Boolean? IsHypotension120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension180
		/// </summary>
		virtual public System.Boolean? IsHypotension180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotension240
		/// </summary>
		virtual public System.Boolean? IsHypotension240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost2
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost3
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPre
		/// </summary>
		virtual public System.Boolean? IsShockPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock10
		/// </summary>
		virtual public System.Boolean? IsShock10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock30
		/// </summary>
		virtual public System.Boolean? IsShock30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock60
		/// </summary>
		virtual public System.Boolean? IsShock60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock120
		/// </summary>
		virtual public System.Boolean? IsShock120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock180
		/// </summary>
		virtual public System.Boolean? IsShock180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShock240
		/// </summary>
		virtual public System.Boolean? IsShock240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShock240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost
		/// </summary>
		virtual public System.Boolean? IsShockPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost2
		/// </summary>
		virtual public System.Boolean? IsShockPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost3
		/// </summary>
		virtual public System.Boolean? IsShockPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPre
		/// </summary>
		virtual public System.Boolean? IsUrticariaPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria10
		/// </summary>
		virtual public System.Boolean? IsUrticaria10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria30
		/// </summary>
		virtual public System.Boolean? IsUrticaria30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria60
		/// </summary>
		virtual public System.Boolean? IsUrticaria60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria120
		/// </summary>
		virtual public System.Boolean? IsUrticaria120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria180
		/// </summary>
		virtual public System.Boolean? IsUrticaria180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticaria240
		/// </summary>
		virtual public System.Boolean? IsUrticaria240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost2
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost3
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPre
		/// </summary>
		virtual public System.Boolean? IsRashPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash10
		/// </summary>
		virtual public System.Boolean? IsRash10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash30
		/// </summary>
		virtual public System.Boolean? IsRash30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash60
		/// </summary>
		virtual public System.Boolean? IsRash60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash120
		/// </summary>
		virtual public System.Boolean? IsRash120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash180
		/// </summary>
		virtual public System.Boolean? IsRash180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRash240
		/// </summary>
		virtual public System.Boolean? IsRash240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRash240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost
		/// </summary>
		virtual public System.Boolean? IsRashPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost2
		/// </summary>
		virtual public System.Boolean? IsRashPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost3
		/// </summary>
		virtual public System.Boolean? IsRashPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPre
		/// </summary>
		virtual public System.Boolean? IsPruritusPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus10
		/// </summary>
		virtual public System.Boolean? IsPruritus10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus30
		/// </summary>
		virtual public System.Boolean? IsPruritus30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus60
		/// </summary>
		virtual public System.Boolean? IsPruritus60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus120
		/// </summary>
		virtual public System.Boolean? IsPruritus120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus180
		/// </summary>
		virtual public System.Boolean? IsPruritus180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritus240
		/// </summary>
		virtual public System.Boolean? IsPruritus240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost
		/// </summary>
		virtual public System.Boolean? IsPruritusPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost2
		/// </summary>
		virtual public System.Boolean? IsPruritusPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost3
		/// </summary>
		virtual public System.Boolean? IsPruritusPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPre
		/// </summary>
		virtual public System.Boolean? IsAnxiousPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious10
		/// </summary>
		virtual public System.Boolean? IsAnxious10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious30
		/// </summary>
		virtual public System.Boolean? IsAnxious30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious60
		/// </summary>
		virtual public System.Boolean? IsAnxious60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious120
		/// </summary>
		virtual public System.Boolean? IsAnxious120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious180
		/// </summary>
		virtual public System.Boolean? IsAnxious180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxious240
		/// </summary>
		virtual public System.Boolean? IsAnxious240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost2
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost3
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPre
		/// </summary>
		virtual public System.Boolean? IsWeakPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak10
		/// </summary>
		virtual public System.Boolean? IsWeak10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak30
		/// </summary>
		virtual public System.Boolean? IsWeak30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak60
		/// </summary>
		virtual public System.Boolean? IsWeak60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak120
		/// </summary>
		virtual public System.Boolean? IsWeak120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak180
		/// </summary>
		virtual public System.Boolean? IsWeak180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeak240
		/// </summary>
		virtual public System.Boolean? IsWeak240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeak240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost
		/// </summary>
		virtual public System.Boolean? IsWeakPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost2
		/// </summary>
		virtual public System.Boolean? IsWeakPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost3
		/// </summary>
		virtual public System.Boolean? IsWeakPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPre
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations10
		/// </summary>
		virtual public System.Boolean? IsPalpitations10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations30
		/// </summary>
		virtual public System.Boolean? IsPalpitations30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations60
		/// </summary>
		virtual public System.Boolean? IsPalpitations60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations120
		/// </summary>
		virtual public System.Boolean? IsPalpitations120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations180
		/// </summary>
		virtual public System.Boolean? IsPalpitations180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitations240
		/// </summary>
		virtual public System.Boolean? IsPalpitations240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost2
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost3
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPre
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea10
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea30
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea60
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea120
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea180
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspnea240
		/// </summary>
		virtual public System.Boolean? IsMildDyspnea240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost2
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost3
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePre
		/// </summary>
		virtual public System.Boolean? IsHeadachePre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache10
		/// </summary>
		virtual public System.Boolean? IsHeadache10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache30
		/// </summary>
		virtual public System.Boolean? IsHeadache30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache60
		/// </summary>
		virtual public System.Boolean? IsHeadache60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache120
		/// </summary>
		virtual public System.Boolean? IsHeadache120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache180
		/// </summary>
		virtual public System.Boolean? IsHeadache180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadache240
		/// </summary>
		virtual public System.Boolean? IsHeadache240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost
		/// </summary>
		virtual public System.Boolean? IsHeadachePost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost2
		/// </summary>
		virtual public System.Boolean? IsHeadachePost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost3
		/// </summary>
		virtual public System.Boolean? IsHeadachePost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPre
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin10
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin30
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin60
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin120
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin180
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkin240
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkin240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost2
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost3
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPre
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia10
		/// </summary>
		virtual public System.Boolean? IsTachycardia10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia30
		/// </summary>
		virtual public System.Boolean? IsTachycardia30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia60
		/// </summary>
		virtual public System.Boolean? IsTachycardia60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia120
		/// </summary>
		virtual public System.Boolean? IsTachycardia120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia180
		/// </summary>
		virtual public System.Boolean? IsTachycardia180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardia240
		/// </summary>
		virtual public System.Boolean? IsTachycardia240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost2
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost3
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPre
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPre
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPre);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness10
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness10
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness10);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness30
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness30
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness30);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness60
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness60
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness60);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness120
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness120
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness120);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness180
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness180
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness180);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffness240
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffness240
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness240);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost2
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost3
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPre
		/// </summary>
		virtual public System.String OtherReactionPre
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPre);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction10
		/// </summary>
		virtual public System.String OtherReaction10
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction10);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction30
		/// </summary>
		virtual public System.String OtherReaction30
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction30);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction60
		/// </summary>
		virtual public System.String OtherReaction60
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction60);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction120
		/// </summary>
		virtual public System.String OtherReaction120
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction120);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction180
		/// </summary>
		virtual public System.String OtherReaction180
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction180);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReaction240
		/// </summary>
		virtual public System.String OtherReaction240
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction240);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost
		/// </summary>
		virtual public System.String OtherReactionPost
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost2
		/// </summary>
		virtual public System.String OtherReactionPost2
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost2);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost3
		/// </summary>
		virtual public System.String OtherReactionPost3
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost3);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HemoglobinPre
		/// </summary>
		virtual public System.Decimal? HemoglobinPre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin10
		/// </summary>
		virtual public System.Decimal? Hemoglobin10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin30
		/// </summary>
		virtual public System.Decimal? Hemoglobin30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin60
		/// </summary>
		virtual public System.Decimal? Hemoglobin60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin120
		/// </summary>
		virtual public System.Decimal? Hemoglobin120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin180
		/// </summary>
		virtual public System.Decimal? Hemoglobin180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hemoglobin240
		/// </summary>
		virtual public System.Decimal? Hemoglobin240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HemoglobinPost
		/// </summary>
		virtual public System.Decimal? HemoglobinPost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HemoglobinPost2
		/// </summary>
		virtual public System.Decimal? HemoglobinPost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HemoglobinPost3
		/// </summary>
		virtual public System.Decimal? HemoglobinPost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HematocritPre
		/// </summary>
		virtual public System.Decimal? HematocritPre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit10
		/// </summary>
		virtual public System.Decimal? Hematocrit10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit30
		/// </summary>
		virtual public System.Decimal? Hematocrit30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit60
		/// </summary>
		virtual public System.Decimal? Hematocrit60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit120
		/// </summary>
		virtual public System.Decimal? Hematocrit120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit180
		/// </summary>
		virtual public System.Decimal? Hematocrit180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Hematocrit240
		/// </summary>
		virtual public System.Decimal? Hematocrit240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HematocritPost
		/// </summary>
		virtual public System.Decimal? HematocritPost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HematocritPost2
		/// </summary>
		virtual public System.Decimal? HematocritPost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.HematocritPost3
		/// </summary>
		virtual public System.Decimal? HematocritPost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PlateletPre
		/// </summary>
		virtual public System.Decimal? PlateletPre
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPre);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet10
		/// </summary>
		virtual public System.Decimal? Platelet10
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet10);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet30
		/// </summary>
		virtual public System.Decimal? Platelet30
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet30);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet60
		/// </summary>
		virtual public System.Decimal? Platelet60
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet60);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet120
		/// </summary>
		virtual public System.Decimal? Platelet120
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet120);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet180
		/// </summary>
		virtual public System.Decimal? Platelet180
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet180);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Platelet240
		/// </summary>
		virtual public System.Decimal? Platelet240
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet240);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Platelet240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PlateletPost
		/// </summary>
		virtual public System.Decimal? PlateletPost
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PlateletPost2
		/// </summary>
		virtual public System.Decimal? PlateletPost2
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost2);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PlateletPost3
		/// </summary>
		virtual public System.Decimal? PlateletPost3
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost3);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ActionPre
		/// </summary>
		virtual public System.String ActionPre
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPre);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPre, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action10
		/// </summary>
		virtual public System.String Action10
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action10);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action10, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action30
		/// </summary>
		virtual public System.String Action30
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action30);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action30, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action60
		/// </summary>
		virtual public System.String Action60
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action60);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action60, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action120
		/// </summary>
		virtual public System.String Action120
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action120);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action120, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action180
		/// </summary>
		virtual public System.String Action180
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action180);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action180, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Action240
		/// </summary>
		virtual public System.String Action240
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action240);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Action240, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ActionPost
		/// </summary>
		virtual public System.String ActionPost
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ActionPost2
		/// </summary>
		virtual public System.String ActionPost2
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost2);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ActionPost3
		/// </summary>
		virtual public System.String ActionPost3
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost3);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ActionPost3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHiv
		/// </summary>
		virtual public System.Boolean? IsHiv
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHiv);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHiv, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsVbrl
		/// </summary>
		virtual public System.Boolean? IsVbrl
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsVbrl);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsVbrl, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHbsAg
		/// </summary>
		virtual public System.Boolean? IsHbsAg
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHbsAg);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHbsAg, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHcv
		/// </summary>
		virtual public System.Boolean? IsHcv
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHcv);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHcv, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheck
		/// </summary>
		virtual public System.Boolean? IsReCheck
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheck);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheck, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ReCheckDateTime
		/// </summary>
		virtual public System.DateTime? ReCheckDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.ReCheckDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.ReCheckDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckExpiredDate
		/// </summary>
		virtual public System.Boolean? IsReCheckExpiredDate
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckExpiredDate);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckExpiredDate, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckLeak
		/// </summary>
		virtual public System.Boolean? IsReCheckLeak
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckLeak);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckLeak, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckHemolysis
		/// </summary>
		virtual public System.Boolean? IsReCheckHemolysis
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckHemolysis);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckHemolysis, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckCrossMatchingSuitability
		/// </summary>
		virtual public System.Boolean? IsReCheckCrossMatchingSuitability
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckCrossMatchingSuitability);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckCrossMatchingSuitability, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckClotting
		/// </summary>
		virtual public System.Boolean? IsReCheckClotting
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckClotting);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckClotting, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsReCheckBloodTypeCompatibility
		/// </summary>
		virtual public System.Boolean? IsReCheckBloodTypeCompatibility
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckBloodTypeCompatibility);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckBloodTypeCompatibility, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ReCheckOfficer
		/// </summary>
		virtual public System.String ReCheckOfficer
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.ReCheckOfficer2
		/// </summary>
		virtual public System.String ReCheckOfficer2
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer2);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsCrossmatchBillProceed
		/// </summary>
		virtual public System.Boolean? IsCrossmatchBillProceed
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchBillProceed);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchBillProceed, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTransfusionBillProceed
		/// </summary>
		virtual public System.Boolean? IsTransfusionBillProceed
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTransfusionBillProceed);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTransfusionBillProceed, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsScreening1
		/// </summary>
		virtual public System.Boolean? IsScreening1
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening1);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening1, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsScreening2
		/// </summary>
		virtual public System.Boolean? IsScreening2
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening2);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening2, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsScreening3
		/// </summary>
		virtual public System.Boolean? IsScreening3
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening3);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsScreening3, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.UnitOfficerByUserID
		/// </summary>
		virtual public System.String UnitOfficerByUserID
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficerByUserID);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficerByUserID, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp31
		/// </summary>
		virtual public System.String Bp31
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp31);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp31, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp32
		/// </summary>
		virtual public System.String Bp32
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp32);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp32, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp33
		/// </summary>
		virtual public System.String Bp33
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp33);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp33, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Bp34
		/// </summary>
		virtual public System.String Bp34
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp34);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.Bp34, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.BpPost4
		/// </summary>
		virtual public System.String BpPost4
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost4);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.BpPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse31
		/// </summary>
		virtual public System.Decimal? Pulse31
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse31);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse31, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse32
		/// </summary>
		virtual public System.Decimal? Pulse32
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse32);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse32, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse33
		/// </summary>
		virtual public System.Decimal? Pulse33
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse33);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse33, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Pulse34
		/// </summary>
		virtual public System.Decimal? Pulse34
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse34);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Pulse34, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.PulsePost4
		/// </summary>
		virtual public System.Decimal? PulsePost4
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost4);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.PulsePost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature31
		/// </summary>
		virtual public System.Decimal? Temperature31
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature31);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature31, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature32
		/// </summary>
		virtual public System.Decimal? Temperature32
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature32);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature32, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature33
		/// </summary>
		virtual public System.Decimal? Temperature33
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature33);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature33, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Temperature34
		/// </summary>
		virtual public System.Decimal? Temperature34
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature34);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Temperature34, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.TemperaturePost4
		/// </summary>
		virtual public System.Decimal? TemperaturePost4
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost4);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory31
		/// </summary>
		virtual public System.Decimal? Respiratory31
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory31);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory31, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory32
		/// </summary>
		virtual public System.Decimal? Respiratory32
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory32);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory32, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory33
		/// </summary>
		virtual public System.Decimal? Respiratory33
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory33);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory33, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.Respiratory34
		/// </summary>
		virtual public System.Decimal? Respiratory34
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory34);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.Respiratory34, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.RespiratoryPost4
		/// </summary>
		virtual public System.Decimal? RespiratoryPost4
		{
			get
			{
				return base.GetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost4);
			}

			set
			{
				base.SetSystemDecimal(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost4
		/// </summary>
		virtual public System.Boolean? IsFeverPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost5
		/// </summary>
		virtual public System.Boolean? IsFeverPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost6
		/// </summary>
		virtual public System.Boolean? IsFeverPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost7
		/// </summary>
		virtual public System.Boolean? IsFeverPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsFeverPost8
		/// </summary>
		virtual public System.Boolean? IsFeverPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost4
		/// </summary>
		virtual public System.Boolean? IsShiveringPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost5
		/// </summary>
		virtual public System.Boolean? IsShiveringPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost6
		/// </summary>
		virtual public System.Boolean? IsShiveringPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost7
		/// </summary>
		virtual public System.Boolean? IsShiveringPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShiveringPost8
		/// </summary>
		virtual public System.Boolean? IsShiveringPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost4
		/// </summary>
		virtual public System.Boolean? IsSobPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost5
		/// </summary>
		virtual public System.Boolean? IsSobPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost6
		/// </summary>
		virtual public System.Boolean? IsSobPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost7
		/// </summary>
		virtual public System.Boolean? IsSobPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsSobPost8
		/// </summary>
		virtual public System.Boolean? IsSobPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost4
		/// </summary>
		virtual public System.Boolean? IsPainfulPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost5
		/// </summary>
		virtual public System.Boolean? IsPainfulPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost6
		/// </summary>
		virtual public System.Boolean? IsPainfulPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost7
		/// </summary>
		virtual public System.Boolean? IsPainfulPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPainfulPost8
		/// </summary>
		virtual public System.Boolean? IsPainfulPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost4
		/// </summary>
		virtual public System.Boolean? IsNauseaPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost5
		/// </summary>
		virtual public System.Boolean? IsNauseaPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost6
		/// </summary>
		virtual public System.Boolean? IsNauseaPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost7
		/// </summary>
		virtual public System.Boolean? IsNauseaPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsNauseaPost8
		/// </summary>
		virtual public System.Boolean? IsNauseaPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost4
		/// </summary>
		virtual public System.Boolean? IsBleedingPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost5
		/// </summary>
		virtual public System.Boolean? IsBleedingPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost6
		/// </summary>
		virtual public System.Boolean? IsBleedingPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost7
		/// </summary>
		virtual public System.Boolean? IsBleedingPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsBleedingPost8
		/// </summary>
		virtual public System.Boolean? IsBleedingPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost4
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost5
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost6
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost7
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHypotensionPost8
		/// </summary>
		virtual public System.Boolean? IsHypotensionPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost4
		/// </summary>
		virtual public System.Boolean? IsShockPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost5
		/// </summary>
		virtual public System.Boolean? IsShockPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost6
		/// </summary>
		virtual public System.Boolean? IsShockPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost7
		/// </summary>
		virtual public System.Boolean? IsShockPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsShockPost8
		/// </summary>
		virtual public System.Boolean? IsShockPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost4
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost5
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost6
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost7
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsUrticariaPost8
		/// </summary>
		virtual public System.Boolean? IsUrticariaPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost4
		/// </summary>
		virtual public System.Boolean? IsRashPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost5
		/// </summary>
		virtual public System.Boolean? IsRashPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost6
		/// </summary>
		virtual public System.Boolean? IsRashPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost7
		/// </summary>
		virtual public System.Boolean? IsRashPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRashPost8
		/// </summary>
		virtual public System.Boolean? IsRashPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost4
		/// </summary>
		virtual public System.Boolean? IsPruritusPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost5
		/// </summary>
		virtual public System.Boolean? IsPruritusPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost6
		/// </summary>
		virtual public System.Boolean? IsPruritusPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost7
		/// </summary>
		virtual public System.Boolean? IsPruritusPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPruritusPost8
		/// </summary>
		virtual public System.Boolean? IsPruritusPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost4
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost5
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost6
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost7
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsAnxiousPost8
		/// </summary>
		virtual public System.Boolean? IsAnxiousPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost4
		/// </summary>
		virtual public System.Boolean? IsWeakPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost5
		/// </summary>
		virtual public System.Boolean? IsWeakPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost6
		/// </summary>
		virtual public System.Boolean? IsWeakPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost7
		/// </summary>
		virtual public System.Boolean? IsWeakPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsWeakPost8
		/// </summary>
		virtual public System.Boolean? IsWeakPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost4
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost5
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost6
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost7
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsPalpitationsPost8
		/// </summary>
		virtual public System.Boolean? IsPalpitationsPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost4
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost5
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost6
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost7
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMildDyspneaPost8
		/// </summary>
		virtual public System.Boolean? IsMildDyspneaPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost4
		/// </summary>
		virtual public System.Boolean? IsHeadachePost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost5
		/// </summary>
		virtual public System.Boolean? IsHeadachePost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost6
		/// </summary>
		virtual public System.Boolean? IsHeadachePost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost7
		/// </summary>
		virtual public System.Boolean? IsHeadachePost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsHeadachePost8
		/// </summary>
		virtual public System.Boolean? IsHeadachePost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost4
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost5
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost6
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost7
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsRednessOnTheSkinPost8
		/// </summary>
		virtual public System.Boolean? IsRednessOnTheSkinPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost4
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost5
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost6
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost7
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsTachycardiaPost8
		/// </summary>
		virtual public System.Boolean? IsTachycardiaPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost4
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost4
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost4);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost5
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost5
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost5);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost6
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost6
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost6);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost7
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost7
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost7);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.IsMuscleStiffnessPost8
		/// </summary>
		virtual public System.Boolean? IsMuscleStiffnessPost8
		{
			get
			{
				return base.GetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost8);
			}

			set
			{
				base.SetSystemBoolean(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost8, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost4
		/// </summary>
		virtual public System.String OtherReactionPost4
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost4);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost4, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost5
		/// </summary>
		virtual public System.String OtherReactionPost5
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost5);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost5, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost6
		/// </summary>
		virtual public System.String OtherReactionPost6
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost6);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost6, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost7
		/// </summary>
		virtual public System.String OtherReactionPost7
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost7);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost7, value);
			}
		}
		/// <summary>
		/// Maps to BloodBankTransactionItem.OtherReactionPost8
		/// </summary>
		virtual public System.String OtherReactionPost8
		{
			get
			{
				return base.GetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost8);
			}

			set
			{
				base.SetSystemString(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost8, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
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
			public esStrings(esBloodBankTransactionItem entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
			public System.String BagNo
			{
				get
				{
					System.String data = entity.BagNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BagNo = null;
					else entity.BagNo = Convert.ToString(value);
				}
			}
			public System.String ReceivedDate
			{
				get
				{
					System.DateTime? data = entity.ReceivedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedDate = null;
					else entity.ReceivedDate = Convert.ToDateTime(value);
				}
			}
			public System.String ReceivedTime
			{
				get
				{
					System.String data = entity.ReceivedTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReceivedTime = null;
					else entity.ReceivedTime = Convert.ToString(value);
				}
			}
			public System.String SRBloodGroupReceived
			{
				get
				{
					System.String data = entity.SRBloodGroupReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodGroupReceived = null;
					else entity.SRBloodGroupReceived = Convert.ToString(value);
				}
			}
			public System.String SRBloodBagStatus
			{
				get
				{
					System.String data = entity.SRBloodBagStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodBagStatus = null;
					else entity.SRBloodBagStatus = Convert.ToString(value);
				}
			}
			public System.String IsScreeningLabelPassedPmi
			{
				get
				{
					System.Boolean? data = entity.IsScreeningLabelPassedPmi;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScreeningLabelPassedPmi = null;
					else entity.IsScreeningLabelPassedPmi = Convert.ToBoolean(value);
				}
			}
			public System.String IsExpiredDate
			{
				get
				{
					System.Boolean? data = entity.IsExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsExpiredDate = null;
					else entity.IsExpiredDate = Convert.ToBoolean(value);
				}
			}
			public System.String IsLeak
			{
				get
				{
					System.Boolean? data = entity.IsLeak;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLeak = null;
					else entity.IsLeak = Convert.ToBoolean(value);
				}
			}
			public System.String IsHemolysis
			{
				get
				{
					System.Boolean? data = entity.IsHemolysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHemolysis = null;
					else entity.IsHemolysis = Convert.ToBoolean(value);
				}
			}
			public System.String IsCrossMatchingSuitability
			{
				get
				{
					System.Boolean? data = entity.IsCrossMatchingSuitability;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossMatchingSuitability = null;
					else entity.IsCrossMatchingSuitability = Convert.ToBoolean(value);
				}
			}
			public System.String IsClotting
			{
				get
				{
					System.Boolean? data = entity.IsClotting;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClotting = null;
					else entity.IsClotting = Convert.ToBoolean(value);
				}
			}
			public System.String IsBloodTypeCompatibility
			{
				get
				{
					System.Boolean? data = entity.IsBloodTypeCompatibility;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBloodTypeCompatibility = null;
					else entity.IsBloodTypeCompatibility = Convert.ToBoolean(value);
				}
			}
			public System.String IsLabelDonorsMatchesWithPatientsForm
			{
				get
				{
					System.Boolean? data = entity.IsLabelDonorsMatchesWithPatientsForm;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsLabelDonorsMatchesWithPatientsForm = null;
					else entity.IsLabelDonorsMatchesWithPatientsForm = Convert.ToBoolean(value);
				}
			}
			public System.String IsScreeningLabelPassedBd
			{
				get
				{
					System.Boolean? data = entity.IsScreeningLabelPassedBd;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScreeningLabelPassedBd = null;
					else entity.IsScreeningLabelPassedBd = Convert.ToBoolean(value);
				}
			}
			public System.String ExaminerByUserID
			{
				get
				{
					System.String data = entity.ExaminerByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExaminerByUserID = null;
					else entity.ExaminerByUserID = Convert.ToString(value);
				}
			}
			public System.String UnitOfficer
			{
				get
				{
					System.String data = entity.UnitOfficer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnitOfficer = null;
					else entity.UnitOfficer = Convert.ToString(value);
				}
			}
			public System.String TransfusionStartDateTime
			{
				get
				{
					System.DateTime? data = entity.TransfusionStartDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransfusionStartDateTime = null;
					else entity.TransfusionStartDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String TransfusionEndDateTime
			{
				get
				{
					System.DateTime? data = entity.TransfusionEndDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransfusionEndDateTime = null;
					else entity.TransfusionEndDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String TransfusedOfficerStartBy
			{
				get
				{
					System.String data = entity.TransfusedOfficerStartBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransfusedOfficerStartBy = null;
					else entity.TransfusedOfficerStartBy = Convert.ToString(value);
				}
			}
			public System.String TransfusedOfficerEndBy
			{
				get
				{
					System.String data = entity.TransfusedOfficerEndBy;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransfusedOfficerEndBy = null;
					else entity.TransfusedOfficerEndBy = Convert.ToString(value);
				}
			}
			public System.String QtyTransfusion
			{
				get
				{
					System.Int16? data = entity.QtyTransfusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QtyTransfusion = null;
					else entity.QtyTransfusion = Convert.ToInt16(value);
				}
			}
			public System.String SRBloodSource
			{
				get
				{
					System.String data = entity.SRBloodSource;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodSource = null;
					else entity.SRBloodSource = Convert.ToString(value);
				}
			}
			public System.String SRBloodSourceFrom
			{
				get
				{
					System.String data = entity.SRBloodSourceFrom;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRBloodSourceFrom = null;
					else entity.SRBloodSourceFrom = Convert.ToString(value);
				}
			}
			public System.String CrossmatchCompatibleMajor
			{
				get
				{
					System.String data = entity.CrossmatchCompatibleMajor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleMajor = null;
					else entity.CrossmatchCompatibleMajor = Convert.ToString(value);
				}
			}
			public System.String CrossmatchCompatibleMinor
			{
				get
				{
					System.String data = entity.CrossmatchCompatibleMinor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleMinor = null;
					else entity.CrossmatchCompatibleMinor = Convert.ToString(value);
				}
			}
			public System.String CrossmatchCompatibleMinorLevel
			{
				get
				{
					System.Int16? data = entity.CrossmatchCompatibleMinorLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleMinorLevel = null;
					else entity.CrossmatchCompatibleMinorLevel = Convert.ToInt16(value);
				}
			}
			public System.String CrossmatchCompatibleAutoControl
			{
				get
				{
					System.String data = entity.CrossmatchCompatibleAutoControl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleAutoControl = null;
					else entity.CrossmatchCompatibleAutoControl = Convert.ToString(value);
				}
			}
			public System.String CrossmatchCompatibleAutoControlLevel
			{
				get
				{
					System.Int16? data = entity.CrossmatchCompatibleAutoControlLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleAutoControlLevel = null;
					else entity.CrossmatchCompatibleAutoControlLevel = Convert.ToInt16(value);
				}
			}
			public System.String CrossmatchCompatibleDctControl
			{
				get
				{
					System.String data = entity.CrossmatchCompatibleDctControl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleDctControl = null;
					else entity.CrossmatchCompatibleDctControl = Convert.ToString(value);
				}
			}
			public System.String CrossmatchCompatibleDctControlLevel
			{
				get
				{
					System.Int16? data = entity.CrossmatchCompatibleDctControlLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchCompatibleDctControlLevel = null;
					else entity.CrossmatchCompatibleDctControlLevel = Convert.ToInt16(value);
				}
			}
			public System.String CrossmatchStartDateTime
			{
				get
				{
					System.DateTime? data = entity.CrossmatchStartDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchStartDateTime = null;
					else entity.CrossmatchStartDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CrossmatchEndDateTime
			{
				get
				{
					System.DateTime? data = entity.CrossmatchEndDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossmatchEndDateTime = null;
					else entity.CrossmatchEndDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsCrossmatchingPassed
			{
				get
				{
					System.Boolean? data = entity.IsCrossmatchingPassed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossmatchingPassed = null;
					else entity.IsCrossmatchingPassed = Convert.ToBoolean(value);
				}
			}
			public System.String CrossMatchingByUserID
			{
				get
				{
					System.String data = entity.CrossMatchingByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CrossMatchingByUserID = null;
					else entity.CrossMatchingByUserID = Convert.ToString(value);
				}
			}
			public System.String BloodBagTemperature
			{
				get
				{
					System.Decimal? data = entity.BloodBagTemperature;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodBagTemperature = null;
					else entity.BloodBagTemperature = Convert.ToDecimal(value);
				}
			}
			public System.String BloodBagNotes
			{
				get
				{
					System.String data = entity.BloodBagNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BloodBagNotes = null;
					else entity.BloodBagNotes = Convert.ToString(value);
				}
			}
			public System.String IsProceedToTransfusion
			{
				get
				{
					System.Boolean? data = entity.IsProceedToTransfusion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProceedToTransfusion = null;
					else entity.IsProceedToTransfusion = Convert.ToBoolean(value);
				}
			}
			public System.String BpPre
			{
				get
				{
					System.String data = entity.BpPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpPre = null;
					else entity.BpPre = Convert.ToString(value);
				}
			}
			public System.String Bp10
			{
				get
				{
					System.String data = entity.Bp10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp10 = null;
					else entity.Bp10 = Convert.ToString(value);
				}
			}
			public System.String Bp30
			{
				get
				{
					System.String data = entity.Bp30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp30 = null;
					else entity.Bp30 = Convert.ToString(value);
				}
			}
			public System.String Bp60
			{
				get
				{
					System.String data = entity.Bp60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp60 = null;
					else entity.Bp60 = Convert.ToString(value);
				}
			}
			public System.String Bp120
			{
				get
				{
					System.String data = entity.Bp120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp120 = null;
					else entity.Bp120 = Convert.ToString(value);
				}
			}
			public System.String Bp180
			{
				get
				{
					System.String data = entity.Bp180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp180 = null;
					else entity.Bp180 = Convert.ToString(value);
				}
			}
			public System.String Bp240
			{
				get
				{
					System.String data = entity.Bp240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp240 = null;
					else entity.Bp240 = Convert.ToString(value);
				}
			}
			public System.String BpPost
			{
				get
				{
					System.String data = entity.BpPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpPost = null;
					else entity.BpPost = Convert.ToString(value);
				}
			}
			public System.String BpPost2
			{
				get
				{
					System.String data = entity.BpPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpPost2 = null;
					else entity.BpPost2 = Convert.ToString(value);
				}
			}
			public System.String BpPost3
			{
				get
				{
					System.String data = entity.BpPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpPost3 = null;
					else entity.BpPost3 = Convert.ToString(value);
				}
			}
			public System.String PulsePre
			{
				get
				{
					System.Decimal? data = entity.PulsePre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PulsePre = null;
					else entity.PulsePre = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse10
			{
				get
				{
					System.Decimal? data = entity.Pulse10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse10 = null;
					else entity.Pulse10 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse30
			{
				get
				{
					System.Decimal? data = entity.Pulse30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse30 = null;
					else entity.Pulse30 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse60
			{
				get
				{
					System.Decimal? data = entity.Pulse60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse60 = null;
					else entity.Pulse60 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse120
			{
				get
				{
					System.Decimal? data = entity.Pulse120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse120 = null;
					else entity.Pulse120 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse180
			{
				get
				{
					System.Decimal? data = entity.Pulse180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse180 = null;
					else entity.Pulse180 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse240
			{
				get
				{
					System.Decimal? data = entity.Pulse240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse240 = null;
					else entity.Pulse240 = Convert.ToDecimal(value);
				}
			}
			public System.String PulsePost
			{
				get
				{
					System.Decimal? data = entity.PulsePost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PulsePost = null;
					else entity.PulsePost = Convert.ToDecimal(value);
				}
			}
			public System.String PulsePost2
			{
				get
				{
					System.Decimal? data = entity.PulsePost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PulsePost2 = null;
					else entity.PulsePost2 = Convert.ToDecimal(value);
				}
			}
			public System.String PulsePost3
			{
				get
				{
					System.Decimal? data = entity.PulsePost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PulsePost3 = null;
					else entity.PulsePost3 = Convert.ToDecimal(value);
				}
			}
			public System.String TemperaturePre
			{
				get
				{
					System.Decimal? data = entity.TemperaturePre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemperaturePre = null;
					else entity.TemperaturePre = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature10
			{
				get
				{
					System.Decimal? data = entity.Temperature10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature10 = null;
					else entity.Temperature10 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature30
			{
				get
				{
					System.Decimal? data = entity.Temperature30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature30 = null;
					else entity.Temperature30 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature60
			{
				get
				{
					System.Decimal? data = entity.Temperature60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature60 = null;
					else entity.Temperature60 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature120
			{
				get
				{
					System.Decimal? data = entity.Temperature120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature120 = null;
					else entity.Temperature120 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature180
			{
				get
				{
					System.Decimal? data = entity.Temperature180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature180 = null;
					else entity.Temperature180 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature240
			{
				get
				{
					System.Decimal? data = entity.Temperature240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature240 = null;
					else entity.Temperature240 = Convert.ToDecimal(value);
				}
			}
			public System.String TemperaturePost
			{
				get
				{
					System.Decimal? data = entity.TemperaturePost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemperaturePost = null;
					else entity.TemperaturePost = Convert.ToDecimal(value);
				}
			}
			public System.String TemperaturePost2
			{
				get
				{
					System.Decimal? data = entity.TemperaturePost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemperaturePost2 = null;
					else entity.TemperaturePost2 = Convert.ToDecimal(value);
				}
			}
			public System.String TemperaturePost3
			{
				get
				{
					System.Decimal? data = entity.TemperaturePost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemperaturePost3 = null;
					else entity.TemperaturePost3 = Convert.ToDecimal(value);
				}
			}
			public System.String RespiratoryPre
			{
				get
				{
					System.Decimal? data = entity.RespiratoryPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespiratoryPre = null;
					else entity.RespiratoryPre = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory10
			{
				get
				{
					System.Decimal? data = entity.Respiratory10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory10 = null;
					else entity.Respiratory10 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory30
			{
				get
				{
					System.Decimal? data = entity.Respiratory30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory30 = null;
					else entity.Respiratory30 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory60
			{
				get
				{
					System.Decimal? data = entity.Respiratory60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory60 = null;
					else entity.Respiratory60 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory120
			{
				get
				{
					System.Decimal? data = entity.Respiratory120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory120 = null;
					else entity.Respiratory120 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory180
			{
				get
				{
					System.Decimal? data = entity.Respiratory180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory180 = null;
					else entity.Respiratory180 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory240
			{
				get
				{
					System.Decimal? data = entity.Respiratory240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory240 = null;
					else entity.Respiratory240 = Convert.ToDecimal(value);
				}
			}
			public System.String RespiratoryPost
			{
				get
				{
					System.Decimal? data = entity.RespiratoryPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespiratoryPost = null;
					else entity.RespiratoryPost = Convert.ToDecimal(value);
				}
			}
			public System.String RespiratoryPost2
			{
				get
				{
					System.Decimal? data = entity.RespiratoryPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespiratoryPost2 = null;
					else entity.RespiratoryPost2 = Convert.ToDecimal(value);
				}
			}
			public System.String RespiratoryPost3
			{
				get
				{
					System.Decimal? data = entity.RespiratoryPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespiratoryPost3 = null;
					else entity.RespiratoryPost3 = Convert.ToDecimal(value);
				}
			}
			public System.String IsFeverPre
			{
				get
				{
					System.Boolean? data = entity.IsFeverPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPre = null;
					else entity.IsFeverPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever10
			{
				get
				{
					System.Boolean? data = entity.IsFever10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever10 = null;
					else entity.IsFever10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever30
			{
				get
				{
					System.Boolean? data = entity.IsFever30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever30 = null;
					else entity.IsFever30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever60
			{
				get
				{
					System.Boolean? data = entity.IsFever60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever60 = null;
					else entity.IsFever60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever120
			{
				get
				{
					System.Boolean? data = entity.IsFever120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever120 = null;
					else entity.IsFever120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever180
			{
				get
				{
					System.Boolean? data = entity.IsFever180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever180 = null;
					else entity.IsFever180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFever240
			{
				get
				{
					System.Boolean? data = entity.IsFever240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFever240 = null;
					else entity.IsFever240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost = null;
					else entity.IsFeverPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost2
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost2 = null;
					else entity.IsFeverPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost3
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost3 = null;
					else entity.IsFeverPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPre
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPre = null;
					else entity.IsShiveringPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering10
			{
				get
				{
					System.Boolean? data = entity.IsShivering10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering10 = null;
					else entity.IsShivering10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering30
			{
				get
				{
					System.Boolean? data = entity.IsShivering30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering30 = null;
					else entity.IsShivering30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering60
			{
				get
				{
					System.Boolean? data = entity.IsShivering60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering60 = null;
					else entity.IsShivering60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering120
			{
				get
				{
					System.Boolean? data = entity.IsShivering120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering120 = null;
					else entity.IsShivering120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering180
			{
				get
				{
					System.Boolean? data = entity.IsShivering180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering180 = null;
					else entity.IsShivering180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShivering240
			{
				get
				{
					System.Boolean? data = entity.IsShivering240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShivering240 = null;
					else entity.IsShivering240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost = null;
					else entity.IsShiveringPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost2
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost2 = null;
					else entity.IsShiveringPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost3
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost3 = null;
					else entity.IsShiveringPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPre
			{
				get
				{
					System.Boolean? data = entity.IsSobPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPre = null;
					else entity.IsSobPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob10
			{
				get
				{
					System.Boolean? data = entity.IsSob10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob10 = null;
					else entity.IsSob10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob30
			{
				get
				{
					System.Boolean? data = entity.IsSob30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob30 = null;
					else entity.IsSob30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob60
			{
				get
				{
					System.Boolean? data = entity.IsSob60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob60 = null;
					else entity.IsSob60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob120
			{
				get
				{
					System.Boolean? data = entity.IsSob120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob120 = null;
					else entity.IsSob120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob180
			{
				get
				{
					System.Boolean? data = entity.IsSob180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob180 = null;
					else entity.IsSob180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSob240
			{
				get
				{
					System.Boolean? data = entity.IsSob240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSob240 = null;
					else entity.IsSob240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost
			{
				get
				{
					System.Boolean? data = entity.IsSobPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost = null;
					else entity.IsSobPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost2
			{
				get
				{
					System.Boolean? data = entity.IsSobPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost2 = null;
					else entity.IsSobPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost3
			{
				get
				{
					System.Boolean? data = entity.IsSobPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost3 = null;
					else entity.IsSobPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPre
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPre = null;
					else entity.IsPainfulPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful10
			{
				get
				{
					System.Boolean? data = entity.IsPainful10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful10 = null;
					else entity.IsPainful10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful30
			{
				get
				{
					System.Boolean? data = entity.IsPainful30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful30 = null;
					else entity.IsPainful30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful60
			{
				get
				{
					System.Boolean? data = entity.IsPainful60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful60 = null;
					else entity.IsPainful60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful120
			{
				get
				{
					System.Boolean? data = entity.IsPainful120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful120 = null;
					else entity.IsPainful120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful180
			{
				get
				{
					System.Boolean? data = entity.IsPainful180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful180 = null;
					else entity.IsPainful180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainful240
			{
				get
				{
					System.Boolean? data = entity.IsPainful240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainful240 = null;
					else entity.IsPainful240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost = null;
					else entity.IsPainfulPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost2
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost2 = null;
					else entity.IsPainfulPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost3
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost3 = null;
					else entity.IsPainfulPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPre
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPre = null;
					else entity.IsNauseaPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea10
			{
				get
				{
					System.Boolean? data = entity.IsNausea10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea10 = null;
					else entity.IsNausea10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea30
			{
				get
				{
					System.Boolean? data = entity.IsNausea30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea30 = null;
					else entity.IsNausea30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea60
			{
				get
				{
					System.Boolean? data = entity.IsNausea60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea60 = null;
					else entity.IsNausea60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea120
			{
				get
				{
					System.Boolean? data = entity.IsNausea120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea120 = null;
					else entity.IsNausea120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea180
			{
				get
				{
					System.Boolean? data = entity.IsNausea180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea180 = null;
					else entity.IsNausea180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNausea240
			{
				get
				{
					System.Boolean? data = entity.IsNausea240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNausea240 = null;
					else entity.IsNausea240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost = null;
					else entity.IsNauseaPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost2
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost2 = null;
					else entity.IsNauseaPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost3
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost3 = null;
					else entity.IsNauseaPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPre
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPre = null;
					else entity.IsBleedingPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding10
			{
				get
				{
					System.Boolean? data = entity.IsBleeding10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding10 = null;
					else entity.IsBleeding10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding30
			{
				get
				{
					System.Boolean? data = entity.IsBleeding30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding30 = null;
					else entity.IsBleeding30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding60
			{
				get
				{
					System.Boolean? data = entity.IsBleeding60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding60 = null;
					else entity.IsBleeding60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding120
			{
				get
				{
					System.Boolean? data = entity.IsBleeding120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding120 = null;
					else entity.IsBleeding120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding180
			{
				get
				{
					System.Boolean? data = entity.IsBleeding180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding180 = null;
					else entity.IsBleeding180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleeding240
			{
				get
				{
					System.Boolean? data = entity.IsBleeding240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleeding240 = null;
					else entity.IsBleeding240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost = null;
					else entity.IsBleedingPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost2
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost2 = null;
					else entity.IsBleedingPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost3
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost3 = null;
					else entity.IsBleedingPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPre
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPre = null;
					else entity.IsHypotensionPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension10
			{
				get
				{
					System.Boolean? data = entity.IsHypotension10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension10 = null;
					else entity.IsHypotension10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension30
			{
				get
				{
					System.Boolean? data = entity.IsHypotension30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension30 = null;
					else entity.IsHypotension30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension60
			{
				get
				{
					System.Boolean? data = entity.IsHypotension60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension60 = null;
					else entity.IsHypotension60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension120
			{
				get
				{
					System.Boolean? data = entity.IsHypotension120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension120 = null;
					else entity.IsHypotension120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension180
			{
				get
				{
					System.Boolean? data = entity.IsHypotension180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension180 = null;
					else entity.IsHypotension180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotension240
			{
				get
				{
					System.Boolean? data = entity.IsHypotension240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotension240 = null;
					else entity.IsHypotension240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost = null;
					else entity.IsHypotensionPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost2
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost2 = null;
					else entity.IsHypotensionPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost3
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost3 = null;
					else entity.IsHypotensionPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPre
			{
				get
				{
					System.Boolean? data = entity.IsShockPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPre = null;
					else entity.IsShockPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock10
			{
				get
				{
					System.Boolean? data = entity.IsShock10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock10 = null;
					else entity.IsShock10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock30
			{
				get
				{
					System.Boolean? data = entity.IsShock30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock30 = null;
					else entity.IsShock30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock60
			{
				get
				{
					System.Boolean? data = entity.IsShock60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock60 = null;
					else entity.IsShock60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock120
			{
				get
				{
					System.Boolean? data = entity.IsShock120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock120 = null;
					else entity.IsShock120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock180
			{
				get
				{
					System.Boolean? data = entity.IsShock180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock180 = null;
					else entity.IsShock180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShock240
			{
				get
				{
					System.Boolean? data = entity.IsShock240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShock240 = null;
					else entity.IsShock240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost
			{
				get
				{
					System.Boolean? data = entity.IsShockPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost = null;
					else entity.IsShockPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost2
			{
				get
				{
					System.Boolean? data = entity.IsShockPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost2 = null;
					else entity.IsShockPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost3
			{
				get
				{
					System.Boolean? data = entity.IsShockPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost3 = null;
					else entity.IsShockPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPre
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPre = null;
					else entity.IsUrticariaPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria10
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria10 = null;
					else entity.IsUrticaria10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria30
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria30 = null;
					else entity.IsUrticaria30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria60
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria60 = null;
					else entity.IsUrticaria60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria120
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria120 = null;
					else entity.IsUrticaria120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria180
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria180 = null;
					else entity.IsUrticaria180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticaria240
			{
				get
				{
					System.Boolean? data = entity.IsUrticaria240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticaria240 = null;
					else entity.IsUrticaria240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost = null;
					else entity.IsUrticariaPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost2
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost2 = null;
					else entity.IsUrticariaPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost3
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost3 = null;
					else entity.IsUrticariaPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPre
			{
				get
				{
					System.Boolean? data = entity.IsRashPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPre = null;
					else entity.IsRashPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash10
			{
				get
				{
					System.Boolean? data = entity.IsRash10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash10 = null;
					else entity.IsRash10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash30
			{
				get
				{
					System.Boolean? data = entity.IsRash30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash30 = null;
					else entity.IsRash30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash60
			{
				get
				{
					System.Boolean? data = entity.IsRash60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash60 = null;
					else entity.IsRash60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash120
			{
				get
				{
					System.Boolean? data = entity.IsRash120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash120 = null;
					else entity.IsRash120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash180
			{
				get
				{
					System.Boolean? data = entity.IsRash180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash180 = null;
					else entity.IsRash180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRash240
			{
				get
				{
					System.Boolean? data = entity.IsRash240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRash240 = null;
					else entity.IsRash240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost
			{
				get
				{
					System.Boolean? data = entity.IsRashPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost = null;
					else entity.IsRashPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost2
			{
				get
				{
					System.Boolean? data = entity.IsRashPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost2 = null;
					else entity.IsRashPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost3
			{
				get
				{
					System.Boolean? data = entity.IsRashPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost3 = null;
					else entity.IsRashPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPre
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPre = null;
					else entity.IsPruritusPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus10
			{
				get
				{
					System.Boolean? data = entity.IsPruritus10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus10 = null;
					else entity.IsPruritus10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus30
			{
				get
				{
					System.Boolean? data = entity.IsPruritus30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus30 = null;
					else entity.IsPruritus30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus60
			{
				get
				{
					System.Boolean? data = entity.IsPruritus60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus60 = null;
					else entity.IsPruritus60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus120
			{
				get
				{
					System.Boolean? data = entity.IsPruritus120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus120 = null;
					else entity.IsPruritus120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus180
			{
				get
				{
					System.Boolean? data = entity.IsPruritus180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus180 = null;
					else entity.IsPruritus180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritus240
			{
				get
				{
					System.Boolean? data = entity.IsPruritus240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritus240 = null;
					else entity.IsPruritus240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost = null;
					else entity.IsPruritusPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost2
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost2 = null;
					else entity.IsPruritusPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost3
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost3 = null;
					else entity.IsPruritusPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPre
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPre = null;
					else entity.IsAnxiousPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious10
			{
				get
				{
					System.Boolean? data = entity.IsAnxious10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious10 = null;
					else entity.IsAnxious10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious30
			{
				get
				{
					System.Boolean? data = entity.IsAnxious30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious30 = null;
					else entity.IsAnxious30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious60
			{
				get
				{
					System.Boolean? data = entity.IsAnxious60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious60 = null;
					else entity.IsAnxious60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious120
			{
				get
				{
					System.Boolean? data = entity.IsAnxious120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious120 = null;
					else entity.IsAnxious120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious180
			{
				get
				{
					System.Boolean? data = entity.IsAnxious180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious180 = null;
					else entity.IsAnxious180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxious240
			{
				get
				{
					System.Boolean? data = entity.IsAnxious240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxious240 = null;
					else entity.IsAnxious240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost = null;
					else entity.IsAnxiousPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost2
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost2 = null;
					else entity.IsAnxiousPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost3
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost3 = null;
					else entity.IsAnxiousPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPre
			{
				get
				{
					System.Boolean? data = entity.IsWeakPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPre = null;
					else entity.IsWeakPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak10
			{
				get
				{
					System.Boolean? data = entity.IsWeak10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak10 = null;
					else entity.IsWeak10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak30
			{
				get
				{
					System.Boolean? data = entity.IsWeak30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak30 = null;
					else entity.IsWeak30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak60
			{
				get
				{
					System.Boolean? data = entity.IsWeak60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak60 = null;
					else entity.IsWeak60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak120
			{
				get
				{
					System.Boolean? data = entity.IsWeak120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak120 = null;
					else entity.IsWeak120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak180
			{
				get
				{
					System.Boolean? data = entity.IsWeak180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak180 = null;
					else entity.IsWeak180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeak240
			{
				get
				{
					System.Boolean? data = entity.IsWeak240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeak240 = null;
					else entity.IsWeak240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost = null;
					else entity.IsWeakPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost2
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost2 = null;
					else entity.IsWeakPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost3
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost3 = null;
					else entity.IsWeakPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPre
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPre = null;
					else entity.IsPalpitationsPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations10
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations10 = null;
					else entity.IsPalpitations10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations30
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations30 = null;
					else entity.IsPalpitations30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations60
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations60 = null;
					else entity.IsPalpitations60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations120
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations120 = null;
					else entity.IsPalpitations120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations180
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations180 = null;
					else entity.IsPalpitations180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitations240
			{
				get
				{
					System.Boolean? data = entity.IsPalpitations240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitations240 = null;
					else entity.IsPalpitations240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost = null;
					else entity.IsPalpitationsPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost2
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost2 = null;
					else entity.IsPalpitationsPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost3
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost3 = null;
					else entity.IsPalpitationsPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPre
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPre = null;
					else entity.IsMildDyspneaPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea10
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea10 = null;
					else entity.IsMildDyspnea10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea30
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea30 = null;
					else entity.IsMildDyspnea30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea60
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea60 = null;
					else entity.IsMildDyspnea60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea120
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea120 = null;
					else entity.IsMildDyspnea120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea180
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea180 = null;
					else entity.IsMildDyspnea180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspnea240
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspnea240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspnea240 = null;
					else entity.IsMildDyspnea240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost = null;
					else entity.IsMildDyspneaPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost2
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost2 = null;
					else entity.IsMildDyspneaPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost3
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost3 = null;
					else entity.IsMildDyspneaPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePre
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePre = null;
					else entity.IsHeadachePre = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache10
			{
				get
				{
					System.Boolean? data = entity.IsHeadache10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache10 = null;
					else entity.IsHeadache10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache30
			{
				get
				{
					System.Boolean? data = entity.IsHeadache30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache30 = null;
					else entity.IsHeadache30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache60
			{
				get
				{
					System.Boolean? data = entity.IsHeadache60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache60 = null;
					else entity.IsHeadache60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache120
			{
				get
				{
					System.Boolean? data = entity.IsHeadache120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache120 = null;
					else entity.IsHeadache120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache180
			{
				get
				{
					System.Boolean? data = entity.IsHeadache180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache180 = null;
					else entity.IsHeadache180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadache240
			{
				get
				{
					System.Boolean? data = entity.IsHeadache240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadache240 = null;
					else entity.IsHeadache240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost = null;
					else entity.IsHeadachePost = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost2
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost2 = null;
					else entity.IsHeadachePost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost3
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost3 = null;
					else entity.IsHeadachePost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPre
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPre = null;
					else entity.IsRednessOnTheSkinPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin10
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin10 = null;
					else entity.IsRednessOnTheSkin10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin30
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin30 = null;
					else entity.IsRednessOnTheSkin30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin60
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin60 = null;
					else entity.IsRednessOnTheSkin60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin120
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin120 = null;
					else entity.IsRednessOnTheSkin120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin180
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin180 = null;
					else entity.IsRednessOnTheSkin180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkin240
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkin240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkin240 = null;
					else entity.IsRednessOnTheSkin240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost = null;
					else entity.IsRednessOnTheSkinPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost2
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost2 = null;
					else entity.IsRednessOnTheSkinPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost3
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost3 = null;
					else entity.IsRednessOnTheSkinPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPre
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPre = null;
					else entity.IsTachycardiaPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia10
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia10 = null;
					else entity.IsTachycardia10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia30
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia30 = null;
					else entity.IsTachycardia30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia60
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia60 = null;
					else entity.IsTachycardia60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia120
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia120 = null;
					else entity.IsTachycardia120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia180
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia180 = null;
					else entity.IsTachycardia180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardia240
			{
				get
				{
					System.Boolean? data = entity.IsTachycardia240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardia240 = null;
					else entity.IsTachycardia240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost = null;
					else entity.IsTachycardiaPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost2
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost2 = null;
					else entity.IsTachycardiaPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost3
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost3 = null;
					else entity.IsTachycardiaPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPre
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPre = null;
					else entity.IsMuscleStiffnessPre = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness10
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness10 = null;
					else entity.IsMuscleStiffness10 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness30
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness30 = null;
					else entity.IsMuscleStiffness30 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness60
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness60 = null;
					else entity.IsMuscleStiffness60 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness120
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness120 = null;
					else entity.IsMuscleStiffness120 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness180
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness180 = null;
					else entity.IsMuscleStiffness180 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffness240
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffness240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffness240 = null;
					else entity.IsMuscleStiffness240 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost = null;
					else entity.IsMuscleStiffnessPost = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost2
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost2 = null;
					else entity.IsMuscleStiffnessPost2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost3
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost3 = null;
					else entity.IsMuscleStiffnessPost3 = Convert.ToBoolean(value);
				}
			}
			public System.String OtherReactionPre
			{
				get
				{
					System.String data = entity.OtherReactionPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPre = null;
					else entity.OtherReactionPre = Convert.ToString(value);
				}
			}
			public System.String OtherReaction10
			{
				get
				{
					System.String data = entity.OtherReaction10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction10 = null;
					else entity.OtherReaction10 = Convert.ToString(value);
				}
			}
			public System.String OtherReaction30
			{
				get
				{
					System.String data = entity.OtherReaction30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction30 = null;
					else entity.OtherReaction30 = Convert.ToString(value);
				}
			}
			public System.String OtherReaction60
			{
				get
				{
					System.String data = entity.OtherReaction60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction60 = null;
					else entity.OtherReaction60 = Convert.ToString(value);
				}
			}
			public System.String OtherReaction120
			{
				get
				{
					System.String data = entity.OtherReaction120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction120 = null;
					else entity.OtherReaction120 = Convert.ToString(value);
				}
			}
			public System.String OtherReaction180
			{
				get
				{
					System.String data = entity.OtherReaction180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction180 = null;
					else entity.OtherReaction180 = Convert.ToString(value);
				}
			}
			public System.String OtherReaction240
			{
				get
				{
					System.String data = entity.OtherReaction240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReaction240 = null;
					else entity.OtherReaction240 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost
			{
				get
				{
					System.String data = entity.OtherReactionPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost = null;
					else entity.OtherReactionPost = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost2
			{
				get
				{
					System.String data = entity.OtherReactionPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost2 = null;
					else entity.OtherReactionPost2 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost3
			{
				get
				{
					System.String data = entity.OtherReactionPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost3 = null;
					else entity.OtherReactionPost3 = Convert.ToString(value);
				}
			}
			public System.String HemoglobinPre
			{
				get
				{
					System.Decimal? data = entity.HemoglobinPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HemoglobinPre = null;
					else entity.HemoglobinPre = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin10
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin10 = null;
					else entity.Hemoglobin10 = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin30
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin30 = null;
					else entity.Hemoglobin30 = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin60
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin60 = null;
					else entity.Hemoglobin60 = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin120
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin120 = null;
					else entity.Hemoglobin120 = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin180
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin180 = null;
					else entity.Hemoglobin180 = Convert.ToDecimal(value);
				}
			}
			public System.String Hemoglobin240
			{
				get
				{
					System.Decimal? data = entity.Hemoglobin240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hemoglobin240 = null;
					else entity.Hemoglobin240 = Convert.ToDecimal(value);
				}
			}
			public System.String HemoglobinPost
			{
				get
				{
					System.Decimal? data = entity.HemoglobinPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HemoglobinPost = null;
					else entity.HemoglobinPost = Convert.ToDecimal(value);
				}
			}
			public System.String HemoglobinPost2
			{
				get
				{
					System.Decimal? data = entity.HemoglobinPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HemoglobinPost2 = null;
					else entity.HemoglobinPost2 = Convert.ToDecimal(value);
				}
			}
			public System.String HemoglobinPost3
			{
				get
				{
					System.Decimal? data = entity.HemoglobinPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HemoglobinPost3 = null;
					else entity.HemoglobinPost3 = Convert.ToDecimal(value);
				}
			}
			public System.String HematocritPre
			{
				get
				{
					System.Decimal? data = entity.HematocritPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HematocritPre = null;
					else entity.HematocritPre = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit10
			{
				get
				{
					System.Decimal? data = entity.Hematocrit10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit10 = null;
					else entity.Hematocrit10 = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit30
			{
				get
				{
					System.Decimal? data = entity.Hematocrit30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit30 = null;
					else entity.Hematocrit30 = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit60
			{
				get
				{
					System.Decimal? data = entity.Hematocrit60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit60 = null;
					else entity.Hematocrit60 = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit120
			{
				get
				{
					System.Decimal? data = entity.Hematocrit120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit120 = null;
					else entity.Hematocrit120 = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit180
			{
				get
				{
					System.Decimal? data = entity.Hematocrit180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit180 = null;
					else entity.Hematocrit180 = Convert.ToDecimal(value);
				}
			}
			public System.String Hematocrit240
			{
				get
				{
					System.Decimal? data = entity.Hematocrit240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Hematocrit240 = null;
					else entity.Hematocrit240 = Convert.ToDecimal(value);
				}
			}
			public System.String HematocritPost
			{
				get
				{
					System.Decimal? data = entity.HematocritPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HematocritPost = null;
					else entity.HematocritPost = Convert.ToDecimal(value);
				}
			}
			public System.String HematocritPost2
			{
				get
				{
					System.Decimal? data = entity.HematocritPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HematocritPost2 = null;
					else entity.HematocritPost2 = Convert.ToDecimal(value);
				}
			}
			public System.String HematocritPost3
			{
				get
				{
					System.Decimal? data = entity.HematocritPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HematocritPost3 = null;
					else entity.HematocritPost3 = Convert.ToDecimal(value);
				}
			}
			public System.String PlateletPre
			{
				get
				{
					System.Decimal? data = entity.PlateletPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlateletPre = null;
					else entity.PlateletPre = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet10
			{
				get
				{
					System.Decimal? data = entity.Platelet10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet10 = null;
					else entity.Platelet10 = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet30
			{
				get
				{
					System.Decimal? data = entity.Platelet30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet30 = null;
					else entity.Platelet30 = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet60
			{
				get
				{
					System.Decimal? data = entity.Platelet60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet60 = null;
					else entity.Platelet60 = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet120
			{
				get
				{
					System.Decimal? data = entity.Platelet120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet120 = null;
					else entity.Platelet120 = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet180
			{
				get
				{
					System.Decimal? data = entity.Platelet180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet180 = null;
					else entity.Platelet180 = Convert.ToDecimal(value);
				}
			}
			public System.String Platelet240
			{
				get
				{
					System.Decimal? data = entity.Platelet240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Platelet240 = null;
					else entity.Platelet240 = Convert.ToDecimal(value);
				}
			}
			public System.String PlateletPost
			{
				get
				{
					System.Decimal? data = entity.PlateletPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlateletPost = null;
					else entity.PlateletPost = Convert.ToDecimal(value);
				}
			}
			public System.String PlateletPost2
			{
				get
				{
					System.Decimal? data = entity.PlateletPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlateletPost2 = null;
					else entity.PlateletPost2 = Convert.ToDecimal(value);
				}
			}
			public System.String PlateletPost3
			{
				get
				{
					System.Decimal? data = entity.PlateletPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PlateletPost3 = null;
					else entity.PlateletPost3 = Convert.ToDecimal(value);
				}
			}
			public System.String ActionPre
			{
				get
				{
					System.String data = entity.ActionPre;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionPre = null;
					else entity.ActionPre = Convert.ToString(value);
				}
			}
			public System.String Action10
			{
				get
				{
					System.String data = entity.Action10;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action10 = null;
					else entity.Action10 = Convert.ToString(value);
				}
			}
			public System.String Action30
			{
				get
				{
					System.String data = entity.Action30;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action30 = null;
					else entity.Action30 = Convert.ToString(value);
				}
			}
			public System.String Action60
			{
				get
				{
					System.String data = entity.Action60;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action60 = null;
					else entity.Action60 = Convert.ToString(value);
				}
			}
			public System.String Action120
			{
				get
				{
					System.String data = entity.Action120;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action120 = null;
					else entity.Action120 = Convert.ToString(value);
				}
			}
			public System.String Action180
			{
				get
				{
					System.String data = entity.Action180;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action180 = null;
					else entity.Action180 = Convert.ToString(value);
				}
			}
			public System.String Action240
			{
				get
				{
					System.String data = entity.Action240;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Action240 = null;
					else entity.Action240 = Convert.ToString(value);
				}
			}
			public System.String ActionPost
			{
				get
				{
					System.String data = entity.ActionPost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionPost = null;
					else entity.ActionPost = Convert.ToString(value);
				}
			}
			public System.String ActionPost2
			{
				get
				{
					System.String data = entity.ActionPost2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionPost2 = null;
					else entity.ActionPost2 = Convert.ToString(value);
				}
			}
			public System.String ActionPost3
			{
				get
				{
					System.String data = entity.ActionPost3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ActionPost3 = null;
					else entity.ActionPost3 = Convert.ToString(value);
				}
			}
			public System.String IsHiv
			{
				get
				{
					System.Boolean? data = entity.IsHiv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHiv = null;
					else entity.IsHiv = Convert.ToBoolean(value);
				}
			}
			public System.String IsVbrl
			{
				get
				{
					System.Boolean? data = entity.IsVbrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVbrl = null;
					else entity.IsVbrl = Convert.ToBoolean(value);
				}
			}
			public System.String IsHbsAg
			{
				get
				{
					System.Boolean? data = entity.IsHbsAg;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHbsAg = null;
					else entity.IsHbsAg = Convert.ToBoolean(value);
				}
			}
			public System.String IsHcv
			{
				get
				{
					System.Boolean? data = entity.IsHcv;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHcv = null;
					else entity.IsHcv = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheck
			{
				get
				{
					System.Boolean? data = entity.IsReCheck;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheck = null;
					else entity.IsReCheck = Convert.ToBoolean(value);
				}
			}
			public System.String ReCheckDateTime
			{
				get
				{
					System.DateTime? data = entity.ReCheckDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReCheckDateTime = null;
					else entity.ReCheckDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String IsReCheckExpiredDate
			{
				get
				{
					System.Boolean? data = entity.IsReCheckExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckExpiredDate = null;
					else entity.IsReCheckExpiredDate = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheckLeak
			{
				get
				{
					System.Boolean? data = entity.IsReCheckLeak;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckLeak = null;
					else entity.IsReCheckLeak = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheckHemolysis
			{
				get
				{
					System.Boolean? data = entity.IsReCheckHemolysis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckHemolysis = null;
					else entity.IsReCheckHemolysis = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheckCrossMatchingSuitability
			{
				get
				{
					System.Boolean? data = entity.IsReCheckCrossMatchingSuitability;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckCrossMatchingSuitability = null;
					else entity.IsReCheckCrossMatchingSuitability = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheckClotting
			{
				get
				{
					System.Boolean? data = entity.IsReCheckClotting;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckClotting = null;
					else entity.IsReCheckClotting = Convert.ToBoolean(value);
				}
			}
			public System.String IsReCheckBloodTypeCompatibility
			{
				get
				{
					System.Boolean? data = entity.IsReCheckBloodTypeCompatibility;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsReCheckBloodTypeCompatibility = null;
					else entity.IsReCheckBloodTypeCompatibility = Convert.ToBoolean(value);
				}
			}
			public System.String ReCheckOfficer
			{
				get
				{
					System.String data = entity.ReCheckOfficer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReCheckOfficer = null;
					else entity.ReCheckOfficer = Convert.ToString(value);
				}
			}
			public System.String ReCheckOfficer2
			{
				get
				{
					System.String data = entity.ReCheckOfficer2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReCheckOfficer2 = null;
					else entity.ReCheckOfficer2 = Convert.ToString(value);
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
			public System.String IsCrossmatchBillProceed
			{
				get
				{
					System.Boolean? data = entity.IsCrossmatchBillProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCrossmatchBillProceed = null;
					else entity.IsCrossmatchBillProceed = Convert.ToBoolean(value);
				}
			}
			public System.String IsTransfusionBillProceed
			{
				get
				{
					System.Boolean? data = entity.IsTransfusionBillProceed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTransfusionBillProceed = null;
					else entity.IsTransfusionBillProceed = Convert.ToBoolean(value);
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
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			public System.String IsScreening1
			{
				get
				{
					System.Boolean? data = entity.IsScreening1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScreening1 = null;
					else entity.IsScreening1 = Convert.ToBoolean(value);
				}
			}
			public System.String IsScreening2
			{
				get
				{
					System.Boolean? data = entity.IsScreening2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScreening2 = null;
					else entity.IsScreening2 = Convert.ToBoolean(value);
				}
			}
			public System.String IsScreening3
			{
				get
				{
					System.Boolean? data = entity.IsScreening3;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsScreening3 = null;
					else entity.IsScreening3 = Convert.ToBoolean(value);
				}
			}
			public System.String UnitOfficerByUserID
			{
				get
				{
					System.String data = entity.UnitOfficerByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UnitOfficerByUserID = null;
					else entity.UnitOfficerByUserID = Convert.ToString(value);
				}
			}
			public System.String Bp31
			{
				get
				{
					System.String data = entity.Bp31;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp31 = null;
					else entity.Bp31 = Convert.ToString(value);
				}
			}
			public System.String Bp32
			{
				get
				{
					System.String data = entity.Bp32;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp32 = null;
					else entity.Bp32 = Convert.ToString(value);
				}
			}
			public System.String Bp33
			{
				get
				{
					System.String data = entity.Bp33;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp33 = null;
					else entity.Bp33 = Convert.ToString(value);
				}
			}
			public System.String Bp34
			{
				get
				{
					System.String data = entity.Bp34;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Bp34 = null;
					else entity.Bp34 = Convert.ToString(value);
				}
			}
			public System.String BpPost4
			{
				get
				{
					System.String data = entity.BpPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BpPost4 = null;
					else entity.BpPost4 = Convert.ToString(value);
				}
			}
			public System.String Pulse31
			{
				get
				{
					System.Decimal? data = entity.Pulse31;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse31 = null;
					else entity.Pulse31 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse32
			{
				get
				{
					System.Decimal? data = entity.Pulse32;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse32 = null;
					else entity.Pulse32 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse33
			{
				get
				{
					System.Decimal? data = entity.Pulse33;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse33 = null;
					else entity.Pulse33 = Convert.ToDecimal(value);
				}
			}
			public System.String Pulse34
			{
				get
				{
					System.Decimal? data = entity.Pulse34;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Pulse34 = null;
					else entity.Pulse34 = Convert.ToDecimal(value);
				}
			}
			public System.String PulsePost4
			{
				get
				{
					System.Decimal? data = entity.PulsePost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PulsePost4 = null;
					else entity.PulsePost4 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature31
			{
				get
				{
					System.Decimal? data = entity.Temperature31;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature31 = null;
					else entity.Temperature31 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature32
			{
				get
				{
					System.Decimal? data = entity.Temperature32;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature32 = null;
					else entity.Temperature32 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature33
			{
				get
				{
					System.Decimal? data = entity.Temperature33;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature33 = null;
					else entity.Temperature33 = Convert.ToDecimal(value);
				}
			}
			public System.String Temperature34
			{
				get
				{
					System.Decimal? data = entity.Temperature34;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Temperature34 = null;
					else entity.Temperature34 = Convert.ToDecimal(value);
				}
			}
			public System.String TemperaturePost4
			{
				get
				{
					System.Decimal? data = entity.TemperaturePost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TemperaturePost4 = null;
					else entity.TemperaturePost4 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory31
			{
				get
				{
					System.Decimal? data = entity.Respiratory31;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory31 = null;
					else entity.Respiratory31 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory32
			{
				get
				{
					System.Decimal? data = entity.Respiratory32;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory32 = null;
					else entity.Respiratory32 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory33
			{
				get
				{
					System.Decimal? data = entity.Respiratory33;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory33 = null;
					else entity.Respiratory33 = Convert.ToDecimal(value);
				}
			}
			public System.String Respiratory34
			{
				get
				{
					System.Decimal? data = entity.Respiratory34;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Respiratory34 = null;
					else entity.Respiratory34 = Convert.ToDecimal(value);
				}
			}
			public System.String RespiratoryPost4
			{
				get
				{
					System.Decimal? data = entity.RespiratoryPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RespiratoryPost4 = null;
					else entity.RespiratoryPost4 = Convert.ToDecimal(value);
				}
			}
			public System.String IsFeverPost4
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost4 = null;
					else entity.IsFeverPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost5
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost5 = null;
					else entity.IsFeverPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost6
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost6 = null;
					else entity.IsFeverPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost7
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost7 = null;
					else entity.IsFeverPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsFeverPost8
			{
				get
				{
					System.Boolean? data = entity.IsFeverPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsFeverPost8 = null;
					else entity.IsFeverPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost4
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost4 = null;
					else entity.IsShiveringPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost5
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost5 = null;
					else entity.IsShiveringPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost6
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost6 = null;
					else entity.IsShiveringPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost7
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost7 = null;
					else entity.IsShiveringPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShiveringPost8
			{
				get
				{
					System.Boolean? data = entity.IsShiveringPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShiveringPost8 = null;
					else entity.IsShiveringPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost4
			{
				get
				{
					System.Boolean? data = entity.IsSobPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost4 = null;
					else entity.IsSobPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost5
			{
				get
				{
					System.Boolean? data = entity.IsSobPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost5 = null;
					else entity.IsSobPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost6
			{
				get
				{
					System.Boolean? data = entity.IsSobPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost6 = null;
					else entity.IsSobPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost7
			{
				get
				{
					System.Boolean? data = entity.IsSobPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost7 = null;
					else entity.IsSobPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsSobPost8
			{
				get
				{
					System.Boolean? data = entity.IsSobPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSobPost8 = null;
					else entity.IsSobPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost4
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost4 = null;
					else entity.IsPainfulPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost5
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost5 = null;
					else entity.IsPainfulPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost6
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost6 = null;
					else entity.IsPainfulPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost7
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost7 = null;
					else entity.IsPainfulPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPainfulPost8
			{
				get
				{
					System.Boolean? data = entity.IsPainfulPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPainfulPost8 = null;
					else entity.IsPainfulPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost4
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost4 = null;
					else entity.IsNauseaPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost5
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost5 = null;
					else entity.IsNauseaPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost6
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost6 = null;
					else entity.IsNauseaPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost7
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost7 = null;
					else entity.IsNauseaPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsNauseaPost8
			{
				get
				{
					System.Boolean? data = entity.IsNauseaPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsNauseaPost8 = null;
					else entity.IsNauseaPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost4
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost4 = null;
					else entity.IsBleedingPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost5
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost5 = null;
					else entity.IsBleedingPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost6
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost6 = null;
					else entity.IsBleedingPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost7
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost7 = null;
					else entity.IsBleedingPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsBleedingPost8
			{
				get
				{
					System.Boolean? data = entity.IsBleedingPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBleedingPost8 = null;
					else entity.IsBleedingPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost4
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost4 = null;
					else entity.IsHypotensionPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost5
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost5 = null;
					else entity.IsHypotensionPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost6
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost6 = null;
					else entity.IsHypotensionPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost7
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost7 = null;
					else entity.IsHypotensionPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHypotensionPost8
			{
				get
				{
					System.Boolean? data = entity.IsHypotensionPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHypotensionPost8 = null;
					else entity.IsHypotensionPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost4
			{
				get
				{
					System.Boolean? data = entity.IsShockPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost4 = null;
					else entity.IsShockPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost5
			{
				get
				{
					System.Boolean? data = entity.IsShockPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost5 = null;
					else entity.IsShockPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost6
			{
				get
				{
					System.Boolean? data = entity.IsShockPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost6 = null;
					else entity.IsShockPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost7
			{
				get
				{
					System.Boolean? data = entity.IsShockPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost7 = null;
					else entity.IsShockPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsShockPost8
			{
				get
				{
					System.Boolean? data = entity.IsShockPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsShockPost8 = null;
					else entity.IsShockPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost4
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost4 = null;
					else entity.IsUrticariaPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost5
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost5 = null;
					else entity.IsUrticariaPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost6
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost6 = null;
					else entity.IsUrticariaPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost7
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost7 = null;
					else entity.IsUrticariaPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsUrticariaPost8
			{
				get
				{
					System.Boolean? data = entity.IsUrticariaPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUrticariaPost8 = null;
					else entity.IsUrticariaPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost4
			{
				get
				{
					System.Boolean? data = entity.IsRashPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost4 = null;
					else entity.IsRashPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost5
			{
				get
				{
					System.Boolean? data = entity.IsRashPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost5 = null;
					else entity.IsRashPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost6
			{
				get
				{
					System.Boolean? data = entity.IsRashPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost6 = null;
					else entity.IsRashPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost7
			{
				get
				{
					System.Boolean? data = entity.IsRashPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost7 = null;
					else entity.IsRashPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRashPost8
			{
				get
				{
					System.Boolean? data = entity.IsRashPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRashPost8 = null;
					else entity.IsRashPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost4
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost4 = null;
					else entity.IsPruritusPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost5
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost5 = null;
					else entity.IsPruritusPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost6
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost6 = null;
					else entity.IsPruritusPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost7
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost7 = null;
					else entity.IsPruritusPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPruritusPost8
			{
				get
				{
					System.Boolean? data = entity.IsPruritusPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPruritusPost8 = null;
					else entity.IsPruritusPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost4
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost4 = null;
					else entity.IsAnxiousPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost5
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost5 = null;
					else entity.IsAnxiousPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost6
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost6 = null;
					else entity.IsAnxiousPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost7
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost7 = null;
					else entity.IsAnxiousPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsAnxiousPost8
			{
				get
				{
					System.Boolean? data = entity.IsAnxiousPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAnxiousPost8 = null;
					else entity.IsAnxiousPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost4
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost4 = null;
					else entity.IsWeakPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost5
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost5 = null;
					else entity.IsWeakPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost6
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost6 = null;
					else entity.IsWeakPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost7
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost7 = null;
					else entity.IsWeakPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsWeakPost8
			{
				get
				{
					System.Boolean? data = entity.IsWeakPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsWeakPost8 = null;
					else entity.IsWeakPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost4
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost4 = null;
					else entity.IsPalpitationsPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost5
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost5 = null;
					else entity.IsPalpitationsPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost6
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost6 = null;
					else entity.IsPalpitationsPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost7
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost7 = null;
					else entity.IsPalpitationsPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsPalpitationsPost8
			{
				get
				{
					System.Boolean? data = entity.IsPalpitationsPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPalpitationsPost8 = null;
					else entity.IsPalpitationsPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost4
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost4 = null;
					else entity.IsMildDyspneaPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost5
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost5 = null;
					else entity.IsMildDyspneaPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost6
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost6 = null;
					else entity.IsMildDyspneaPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost7
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost7 = null;
					else entity.IsMildDyspneaPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMildDyspneaPost8
			{
				get
				{
					System.Boolean? data = entity.IsMildDyspneaPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMildDyspneaPost8 = null;
					else entity.IsMildDyspneaPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost4
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost4 = null;
					else entity.IsHeadachePost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost5
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost5 = null;
					else entity.IsHeadachePost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost6
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost6 = null;
					else entity.IsHeadachePost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost7
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost7 = null;
					else entity.IsHeadachePost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsHeadachePost8
			{
				get
				{
					System.Boolean? data = entity.IsHeadachePost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsHeadachePost8 = null;
					else entity.IsHeadachePost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost4
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost4 = null;
					else entity.IsRednessOnTheSkinPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost5
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost5 = null;
					else entity.IsRednessOnTheSkinPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost6
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost6 = null;
					else entity.IsRednessOnTheSkinPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost7
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost7 = null;
					else entity.IsRednessOnTheSkinPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsRednessOnTheSkinPost8
			{
				get
				{
					System.Boolean? data = entity.IsRednessOnTheSkinPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRednessOnTheSkinPost8 = null;
					else entity.IsRednessOnTheSkinPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost4
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost4 = null;
					else entity.IsTachycardiaPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost5
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost5 = null;
					else entity.IsTachycardiaPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost6
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost6 = null;
					else entity.IsTachycardiaPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost7
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost7 = null;
					else entity.IsTachycardiaPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsTachycardiaPost8
			{
				get
				{
					System.Boolean? data = entity.IsTachycardiaPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTachycardiaPost8 = null;
					else entity.IsTachycardiaPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost4
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost4 = null;
					else entity.IsMuscleStiffnessPost4 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost5
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost5 = null;
					else entity.IsMuscleStiffnessPost5 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost6
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost6 = null;
					else entity.IsMuscleStiffnessPost6 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost7
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost7 = null;
					else entity.IsMuscleStiffnessPost7 = Convert.ToBoolean(value);
				}
			}
			public System.String IsMuscleStiffnessPost8
			{
				get
				{
					System.Boolean? data = entity.IsMuscleStiffnessPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsMuscleStiffnessPost8 = null;
					else entity.IsMuscleStiffnessPost8 = Convert.ToBoolean(value);
				}
			}
			public System.String OtherReactionPost4
			{
				get
				{
					System.String data = entity.OtherReactionPost4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost4 = null;
					else entity.OtherReactionPost4 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost5
			{
				get
				{
					System.String data = entity.OtherReactionPost5;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost5 = null;
					else entity.OtherReactionPost5 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost6
			{
				get
				{
					System.String data = entity.OtherReactionPost6;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost6 = null;
					else entity.OtherReactionPost6 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost7
			{
				get
				{
					System.String data = entity.OtherReactionPost7;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost7 = null;
					else entity.OtherReactionPost7 = Convert.ToString(value);
				}
			}
			public System.String OtherReactionPost8
			{
				get
				{
					System.String data = entity.OtherReactionPost8;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OtherReactionPost8 = null;
					else entity.OtherReactionPost8 = Convert.ToString(value);
				}
			}
			private esBloodBankTransactionItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esBloodBankTransactionItemQuery query)
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
				throw new Exception("esBloodBankTransactionItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class BloodBankTransactionItem : esBloodBankTransactionItem
	{
	}

	[Serializable]
	abstract public class esBloodBankTransactionItemQuery : esDynamicQuery
	{
		protected override string GetConnectionName()
		{
			return "sci";
		}

		override protected IMetadata Meta
		{
			get
			{
				return BloodBankTransactionItemMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem BagNo
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BagNo, esSystemType.String);
			}
		}

		public esQueryItem ReceivedDate
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ReceivedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem ReceivedTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime, esSystemType.String);
			}
		}

		public esQueryItem SRBloodGroupReceived
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.SRBloodGroupReceived, esSystemType.String);
			}
		}

		public esQueryItem SRBloodBagStatus
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.SRBloodBagStatus, esSystemType.String);
			}
		}

		public esQueryItem IsScreeningLabelPassedPmi
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedPmi, esSystemType.Boolean);
			}
		}

		public esQueryItem IsExpiredDate
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsExpiredDate, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLeak
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsLeak, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHemolysis
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHemolysis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsCrossMatchingSuitability
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability, esSystemType.Boolean);
			}
		}

		public esQueryItem IsClotting
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsClotting, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBloodTypeCompatibility
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBloodTypeCompatibility, esSystemType.Boolean);
			}
		}

		public esQueryItem IsLabelDonorsMatchesWithPatientsForm
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsLabelDonorsMatchesWithPatientsForm, esSystemType.Boolean);
			}
		}

		public esQueryItem IsScreeningLabelPassedBd
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedBd, esSystemType.Boolean);
			}
		}

		public esQueryItem ExaminerByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ExaminerByUserID, esSystemType.String);
			}
		}

		public esQueryItem UnitOfficer
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.UnitOfficer, esSystemType.String);
			}
		}

		public esQueryItem TransfusionStartDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TransfusionStartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem TransfusionEndDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TransfusionEndDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem TransfusedOfficerStartBy
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerStartBy, esSystemType.String);
			}
		}

		public esQueryItem TransfusedOfficerEndBy
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerEndBy, esSystemType.String);
			}
		}

		public esQueryItem QtyTransfusion
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.QtyTransfusion, esSystemType.Int16);
			}
		}

		public esQueryItem SRBloodSource
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.SRBloodSource, esSystemType.String);
			}
		}

		public esQueryItem SRBloodSourceFrom
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.SRBloodSourceFrom, esSystemType.String);
			}
		}

		public esQueryItem CrossmatchCompatibleMajor
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMajor, esSystemType.String);
			}
		}

		public esQueryItem CrossmatchCompatibleMinor
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinor, esSystemType.String);
			}
		}

		public esQueryItem CrossmatchCompatibleMinorLevel
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinorLevel, esSystemType.Int16);
			}
		}

		public esQueryItem CrossmatchCompatibleAutoControl
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControl, esSystemType.String);
			}
		}

		public esQueryItem CrossmatchCompatibleAutoControlLevel
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControlLevel, esSystemType.Int16);
			}
		}

		public esQueryItem CrossmatchCompatibleDctControl
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControl, esSystemType.String);
			}
		}

		public esQueryItem CrossmatchCompatibleDctControlLevel
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControlLevel, esSystemType.Int16);
			}
		}

		public esQueryItem CrossmatchStartDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CrossmatchEndDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsCrossmatchingPassed
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchingPassed, esSystemType.Boolean);
			}
		}

		public esQueryItem CrossMatchingByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.CrossMatchingByUserID, esSystemType.String);
			}
		}

		public esQueryItem BloodBagTemperature
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BloodBagTemperature, esSystemType.Decimal);
			}
		}

		public esQueryItem BloodBagNotes
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BloodBagNotes, esSystemType.String);
			}
		}

		public esQueryItem IsProceedToTransfusion
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsProceedToTransfusion, esSystemType.Boolean);
			}
		}

		public esQueryItem BpPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BpPre, esSystemType.String);
			}
		}

		public esQueryItem Bp10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp10, esSystemType.String);
			}
		}

		public esQueryItem Bp30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp30, esSystemType.String);
			}
		}

		public esQueryItem Bp60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp60, esSystemType.String);
			}
		}

		public esQueryItem Bp120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp120, esSystemType.String);
			}
		}

		public esQueryItem Bp180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp180, esSystemType.String);
			}
		}

		public esQueryItem Bp240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp240, esSystemType.String);
			}
		}

		public esQueryItem BpPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BpPost, esSystemType.String);
			}
		}

		public esQueryItem BpPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BpPost2, esSystemType.String);
			}
		}

		public esQueryItem BpPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BpPost3, esSystemType.String);
			}
		}

		public esQueryItem PulsePre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PulsePre, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse10, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse30, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse60, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse120, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse180, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse240, esSystemType.Decimal);
			}
		}

		public esQueryItem PulsePost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PulsePost, esSystemType.Decimal);
			}
		}

		public esQueryItem PulsePost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PulsePost2, esSystemType.Decimal);
			}
		}

		public esQueryItem PulsePost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PulsePost3, esSystemType.Decimal);
			}
		}

		public esQueryItem TemperaturePre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TemperaturePre, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature10, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature30, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature60, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature120, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature180, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature240, esSystemType.Decimal);
			}
		}

		public esQueryItem TemperaturePost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost, esSystemType.Decimal);
			}
		}

		public esQueryItem TemperaturePost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost2, esSystemType.Decimal);
			}
		}

		public esQueryItem TemperaturePost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost3, esSystemType.Decimal);
			}
		}

		public esQueryItem RespiratoryPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPre, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory10, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory30, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory60, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory120, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory180, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory240, esSystemType.Decimal);
			}
		}

		public esQueryItem RespiratoryPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost, esSystemType.Decimal);
			}
		}

		public esQueryItem RespiratoryPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost2, esSystemType.Decimal);
			}
		}

		public esQueryItem RespiratoryPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost3, esSystemType.Decimal);
			}
		}

		public esQueryItem IsFeverPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFever240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFever240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShivering240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShivering240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSob240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSob240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainful240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainful240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNausea240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNausea240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleeding240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleeding240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotension240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotension240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShock240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShock240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticaria240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRash240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRash240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritus240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritus240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxious240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxious240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeak240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeak240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitations240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspnea240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadache240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadache240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkin240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardia240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPre, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness10, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness30, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness60, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness120, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness180, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffness240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness240, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost3, esSystemType.Boolean);
			}
		}

		public esQueryItem OtherReactionPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPre, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction10, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction30, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction60, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction120, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction180, esSystemType.String);
			}
		}

		public esQueryItem OtherReaction240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReaction240, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost2, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost3, esSystemType.String);
			}
		}

		public esQueryItem HemoglobinPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPre, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin10, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin30, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin60, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin120, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin180, esSystemType.Decimal);
			}
		}

		public esQueryItem Hemoglobin240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin240, esSystemType.Decimal);
			}
		}

		public esQueryItem HemoglobinPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost, esSystemType.Decimal);
			}
		}

		public esQueryItem HemoglobinPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost2, esSystemType.Decimal);
			}
		}

		public esQueryItem HemoglobinPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost3, esSystemType.Decimal);
			}
		}

		public esQueryItem HematocritPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HematocritPre, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit10, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit30, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit60, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit120, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit180, esSystemType.Decimal);
			}
		}

		public esQueryItem Hematocrit240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Hematocrit240, esSystemType.Decimal);
			}
		}

		public esQueryItem HematocritPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HematocritPost, esSystemType.Decimal);
			}
		}

		public esQueryItem HematocritPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HematocritPost2, esSystemType.Decimal);
			}
		}

		public esQueryItem HematocritPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.HematocritPost3, esSystemType.Decimal);
			}
		}

		public esQueryItem PlateletPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PlateletPre, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet10, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet30, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet60, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet120, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet180, esSystemType.Decimal);
			}
		}

		public esQueryItem Platelet240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Platelet240, esSystemType.Decimal);
			}
		}

		public esQueryItem PlateletPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PlateletPost, esSystemType.Decimal);
			}
		}

		public esQueryItem PlateletPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PlateletPost2, esSystemType.Decimal);
			}
		}

		public esQueryItem PlateletPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PlateletPost3, esSystemType.Decimal);
			}
		}

		public esQueryItem ActionPre
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ActionPre, esSystemType.String);
			}
		}

		public esQueryItem Action10
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action10, esSystemType.String);
			}
		}

		public esQueryItem Action30
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action30, esSystemType.String);
			}
		}

		public esQueryItem Action60
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action60, esSystemType.String);
			}
		}

		public esQueryItem Action120
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action120, esSystemType.String);
			}
		}

		public esQueryItem Action180
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action180, esSystemType.String);
			}
		}

		public esQueryItem Action240
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Action240, esSystemType.String);
			}
		}

		public esQueryItem ActionPost
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ActionPost, esSystemType.String);
			}
		}

		public esQueryItem ActionPost2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ActionPost2, esSystemType.String);
			}
		}

		public esQueryItem ActionPost3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ActionPost3, esSystemType.String);
			}
		}

		public esQueryItem IsHiv
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHiv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVbrl
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsVbrl, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHbsAg
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHbsAg, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHcv
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHcv, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheck
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheck, esSystemType.Boolean);
			}
		}

		public esQueryItem ReCheckDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ReCheckDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem IsReCheckExpiredDate
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckExpiredDate, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheckLeak
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckLeak, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheckHemolysis
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckHemolysis, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheckCrossMatchingSuitability
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckCrossMatchingSuitability, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheckClotting
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckClotting, esSystemType.Boolean);
			}
		}

		public esQueryItem IsReCheckBloodTypeCompatibility
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsReCheckBloodTypeCompatibility, esSystemType.Boolean);
			}
		}

		public esQueryItem ReCheckOfficer
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer, esSystemType.String);
			}
		}

		public esQueryItem ReCheckOfficer2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer2, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsCrossmatchBillProceed
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchBillProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTransfusionBillProceed
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTransfusionBillProceed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsScreening1
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsScreening1, esSystemType.Boolean);
			}
		}

		public esQueryItem IsScreening2
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsScreening2, esSystemType.Boolean);
			}
		}

		public esQueryItem IsScreening3
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsScreening3, esSystemType.Boolean);
			}
		}

		public esQueryItem UnitOfficerByUserID
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.UnitOfficerByUserID, esSystemType.String);
			}
		}

		public esQueryItem Bp31
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp31, esSystemType.String);
			}
		}

		public esQueryItem Bp32
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp32, esSystemType.String);
			}
		}

		public esQueryItem Bp33
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp33, esSystemType.String);
			}
		}

		public esQueryItem Bp34
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Bp34, esSystemType.String);
			}
		}

		public esQueryItem BpPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.BpPost4, esSystemType.String);
			}
		}

		public esQueryItem Pulse31
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse31, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse32
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse32, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse33
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse33, esSystemType.Decimal);
			}
		}

		public esQueryItem Pulse34
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Pulse34, esSystemType.Decimal);
			}
		}

		public esQueryItem PulsePost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.PulsePost4, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature31
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature31, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature32
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature32, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature33
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature33, esSystemType.Decimal);
			}
		}

		public esQueryItem Temperature34
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Temperature34, esSystemType.Decimal);
			}
		}

		public esQueryItem TemperaturePost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost4, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory31
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory31, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory32
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory32, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory33
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory33, esSystemType.Decimal);
			}
		}

		public esQueryItem Respiratory34
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.Respiratory34, esSystemType.Decimal);
			}
		}

		public esQueryItem RespiratoryPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost4, esSystemType.Decimal);
			}
		}

		public esQueryItem IsFeverPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsFeverPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShiveringPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsSobPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsSobPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPainfulPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsNauseaPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsBleedingPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHypotensionPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsShockPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsShockPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsUrticariaPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRashPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRashPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPruritusPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAnxiousPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsWeakPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsPalpitationsPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMildDyspneaPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsHeadachePost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsRednessOnTheSkinPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsTachycardiaPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost4, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost5, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost6, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost7, esSystemType.Boolean);
			}
		}

		public esQueryItem IsMuscleStiffnessPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost8, esSystemType.Boolean);
			}
		}

		public esQueryItem OtherReactionPost4
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost4, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost5
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost5, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost6
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost6, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost7
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost7, esSystemType.String);
			}
		}

		public esQueryItem OtherReactionPost8
		{
			get
			{
				return new esQueryItem(this, BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost8, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("BloodBankTransactionItemCollection")]
	public partial class BloodBankTransactionItemCollection : esBloodBankTransactionItemCollection, IEnumerable<BloodBankTransactionItem>
	{
		public BloodBankTransactionItemCollection()
		{

		}

		public static implicit operator List<BloodBankTransactionItem>(BloodBankTransactionItemCollection coll)
		{
			List<BloodBankTransactionItem> list = new List<BloodBankTransactionItem>();

			foreach (BloodBankTransactionItem emp in coll)
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
				return BloodBankTransactionItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBankTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new BloodBankTransactionItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new BloodBankTransactionItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public BloodBankTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBankTransactionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(BloodBankTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public BloodBankTransactionItem AddNew()
		{
			BloodBankTransactionItem entity = base.AddNewEntity() as BloodBankTransactionItem;

			return entity;
		}
		public BloodBankTransactionItem FindByPrimaryKey(String transactionNo, String bagNo)
		{
			return base.FindByPrimaryKey(transactionNo, bagNo) as BloodBankTransactionItem;
		}

		#region IEnumerable< BloodBankTransactionItem> Members

		IEnumerator<BloodBankTransactionItem> IEnumerable<BloodBankTransactionItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as BloodBankTransactionItem;
			}
		}

		#endregion

		private BloodBankTransactionItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'BloodBankTransactionItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("BloodBankTransactionItem ({TransactionNo, BagNo})")]
	[Serializable]
	public partial class BloodBankTransactionItem : esBloodBankTransactionItem
	{
		public BloodBankTransactionItem()
		{
		}

		public BloodBankTransactionItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return BloodBankTransactionItemMetadata.Meta();
			}
		}

		override protected esBloodBankTransactionItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new BloodBankTransactionItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public BloodBankTransactionItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new BloodBankTransactionItemQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(BloodBankTransactionItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private BloodBankTransactionItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class BloodBankTransactionItemQuery : esBloodBankTransactionItemQuery
	{
		public BloodBankTransactionItemQuery()
		{

		}

		public BloodBankTransactionItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "BloodBankTransactionItemQuery";
		}
	}

	[Serializable]
	public partial class BloodBankTransactionItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected BloodBankTransactionItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BagNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BagNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ReceivedDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ReceivedDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ReceivedTime, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ReceivedTime;
			c.CharacterMaxLength = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.SRBloodGroupReceived, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.SRBloodGroupReceived;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.SRBloodBagStatus, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.SRBloodBagStatus;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedPmi, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsScreeningLabelPassedPmi;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsExpiredDate, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsLeak, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsLeak;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHemolysis, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHemolysis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsCrossMatchingSuitability, 10, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsCrossMatchingSuitability;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsClotting, 11, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsClotting;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBloodTypeCompatibility, 12, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBloodTypeCompatibility;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsLabelDonorsMatchesWithPatientsForm, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsLabelDonorsMatchesWithPatientsForm;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsScreeningLabelPassedBd, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsScreeningLabelPassedBd;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ExaminerByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ExaminerByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficer, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.UnitOfficer;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TransfusionStartDateTime, 17, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TransfusionStartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TransfusionEndDateTime, 18, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TransfusionEndDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerStartBy, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TransfusedOfficerStartBy;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TransfusedOfficerEndBy, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TransfusedOfficerEndBy;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.QtyTransfusion, 21, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.QtyTransfusion;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSource, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.SRBloodSource;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.SRBloodSourceFrom, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.SRBloodSourceFrom;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMajor, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleMajor;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinor, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleMinor;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleMinorLevel, 26, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleMinorLevel;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControl, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleAutoControl;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleAutoControlLevel, 28, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleAutoControlLevel;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControl, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleDctControl;
			c.CharacterMaxLength = 1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchCompatibleDctControlLevel, 30, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchCompatibleDctControlLevel;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchStartDateTime, 31, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchStartDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossmatchEndDateTime, 32, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossmatchEndDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchingPassed, 33, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsCrossmatchingPassed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.CrossMatchingByUserID, 34, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.CrossMatchingByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BloodBagTemperature, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BloodBagTemperature;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BloodBagNotes, 36, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BloodBagNotes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsProceedToTransfusion, 37, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsProceedToTransfusion;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BpPre, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BpPre;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp10, 39, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp10;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp30, 40, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp30;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp60, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp60;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp120, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp120;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp180, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp180;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp240, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp240;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BpPost, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BpPost;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BpPost2, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BpPost2;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BpPost3, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BpPost3;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PulsePre, 48, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PulsePre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse10, 49, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse30, 50, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse60, 51, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse120, 52, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse180, 53, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse240, 54, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PulsePost, 55, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PulsePost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PulsePost2, 56, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PulsePost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PulsePost3, 57, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PulsePost3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePre, 58, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TemperaturePre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature10, 59, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature30, 60, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature60, 61, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature120, 62, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature180, 63, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature240, 64, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost, 65, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TemperaturePost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost2, 66, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TemperaturePost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost3, 67, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TemperaturePost3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPre, 68, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.RespiratoryPre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory10, 69, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory30, 70, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory60, 71, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory120, 72, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory180, 73, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory240, 74, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost, 75, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.RespiratoryPost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost2, 76, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.RespiratoryPost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost3, 77, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.RespiratoryPost3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPre, 78, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever10, 79, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever30, 80, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever60, 81, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever120, 82, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever180, 83, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFever240, 84, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFever240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost, 85, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost2, 86, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost3, 87, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPre, 88, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering10, 89, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering30, 90, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering60, 91, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering120, 92, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering180, 93, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShivering240, 94, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShivering240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost, 95, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost2, 96, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost3, 97, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPre, 98, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob10, 99, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob30, 100, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob60, 101, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob120, 102, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob180, 103, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSob240, 104, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSob240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost, 105, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost2, 106, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost3, 107, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPre, 108, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful10, 109, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful30, 110, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful60, 111, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful120, 112, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful180, 113, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainful240, 114, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainful240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost, 115, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost2, 116, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost3, 117, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPre, 118, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea10, 119, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea30, 120, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea60, 121, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea120, 122, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea180, 123, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNausea240, 124, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNausea240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost, 125, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost2, 126, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost3, 127, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPre, 128, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding10, 129, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding30, 130, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding60, 131, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding120, 132, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding180, 133, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleeding240, 134, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleeding240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost, 135, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost2, 136, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost3, 137, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPre, 138, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension10, 139, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension30, 140, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension60, 141, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension120, 142, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension180, 143, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotension240, 144, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotension240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost, 145, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost2, 146, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost3, 147, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPre, 148, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock10, 149, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock30, 150, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock60, 151, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock120, 152, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock180, 153, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShock240, 154, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShock240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost, 155, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost2, 156, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost3, 157, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPre, 158, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria10, 159, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria30, 160, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria60, 161, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria120, 162, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria180, 163, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticaria240, 164, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticaria240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost, 165, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost2, 166, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost3, 167, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPre, 168, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash10, 169, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash30, 170, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash60, 171, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash120, 172, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash180, 173, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRash240, 174, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRash240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost, 175, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost2, 176, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost3, 177, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPre, 178, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus10, 179, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus30, 180, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus60, 181, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus120, 182, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus180, 183, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritus240, 184, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritus240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost, 185, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost2, 186, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost3, 187, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPre, 188, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious10, 189, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious30, 190, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious60, 191, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious120, 192, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious180, 193, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxious240, 194, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxious240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost, 195, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost2, 196, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost3, 197, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPre, 198, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak10, 199, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak30, 200, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak60, 201, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak120, 202, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak180, 203, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeak240, 204, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeak240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost, 205, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost2, 206, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost3, 207, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPre, 208, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations10, 209, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations30, 210, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations60, 211, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations120, 212, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations180, 213, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitations240, 214, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitations240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost, 215, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost2, 216, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost3, 217, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPre, 218, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea10, 219, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea30, 220, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea60, 221, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea120, 222, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea180, 223, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspnea240, 224, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspnea240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost, 225, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost2, 226, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost3, 227, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePre, 228, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache10, 229, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache30, 230, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache60, 231, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache120, 232, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache180, 233, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadache240, 234, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadache240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost, 235, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost2, 236, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost3, 237, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPre, 238, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin10, 239, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin30, 240, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin60, 241, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin120, 242, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin180, 243, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkin240, 244, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkin240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost, 245, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost2, 246, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost3, 247, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPre, 248, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia10, 249, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia30, 250, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia60, 251, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia120, 252, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia180, 253, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardia240, 254, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardia240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost, 255, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost2, 256, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost3, 257, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPre, 258, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPre;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness10, 259, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness30, 260, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness30;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness60, 261, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness60;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness120, 262, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness120;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness180, 263, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness180;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffness240, 264, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffness240;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost, 265, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost2, 266, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost3, 267, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPre, 268, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPre;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction10, 269, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction10;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction30, 270, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction30;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction60, 271, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction60;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction120, 272, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction120;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction180, 273, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction180;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReaction240, 274, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReaction240;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost, 275, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost2, 276, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost2;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost3, 277, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost3;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPre, 278, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HemoglobinPre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin10, 279, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin30, 280, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin60, 281, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin120, 282, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin180, 283, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hemoglobin240, 284, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hemoglobin240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost, 285, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HemoglobinPost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost2, 286, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HemoglobinPost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HemoglobinPost3, 287, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HemoglobinPost3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HematocritPre, 288, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HematocritPre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit10, 289, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit30, 290, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit60, 291, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit120, 292, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit180, 293, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Hematocrit240, 294, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Hematocrit240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost, 295, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HematocritPost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost2, 296, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HematocritPost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.HematocritPost3, 297, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.HematocritPost3;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PlateletPre, 298, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PlateletPre;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet10, 299, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet10;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet30, 300, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet30;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet60, 301, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet60;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet120, 302, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet120;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet180, 303, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet180;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Platelet240, 304, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Platelet240;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost, 305, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PlateletPost;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost2, 306, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PlateletPost2;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PlateletPost3, 307, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PlateletPost3;
			c.NumericPrecision = 10;
			c.NumericScale = 3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ActionPre, 308, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ActionPre;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action10, 309, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action10;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action30, 310, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action30;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action60, 311, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action60;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action120, 312, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action120;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action180, 313, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action180;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Action240, 314, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Action240;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ActionPost, 315, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ActionPost;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ActionPost2, 316, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ActionPost2;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ActionPost3, 317, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ActionPost3;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHiv, 318, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHiv;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsVbrl, 319, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsVbrl;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHbsAg, 320, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHbsAg;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHcv, 321, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHcv;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheck, 322, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheck;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ReCheckDateTime, 323, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ReCheckDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckExpiredDate, 324, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckLeak, 325, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckLeak;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckHemolysis, 326, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckHemolysis;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckCrossMatchingSuitability, 327, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckCrossMatchingSuitability;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckClotting, 328, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckClotting;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsReCheckBloodTypeCompatibility, 329, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsReCheckBloodTypeCompatibility;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer, 330, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ReCheckOfficer;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.ReCheckOfficer2, 331, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.ReCheckOfficer2;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Notes, 332, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsCrossmatchBillProceed, 333, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsCrossmatchBillProceed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTransfusionBillProceed, 334, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTransfusionBillProceed;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsVoid, 335, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsVoid;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.VoidDateTime, 336, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.VoidByUserID, 337, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateDateTime, 338, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.LastUpdateByUserID, 339, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsScreening1, 340, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsScreening1;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsScreening2, 341, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsScreening2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsScreening3, 342, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsScreening3;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.UnitOfficerByUserID, 343, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.UnitOfficerByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp31, 344, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp31;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp32, 345, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp32;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp33, 346, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp33;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Bp34, 347, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Bp34;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.BpPost4, 348, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.BpPost4;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse31, 349, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse31;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse32, 350, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse32;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse33, 351, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse33;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Pulse34, 352, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Pulse34;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.PulsePost4, 353, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.PulsePost4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature31, 354, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature31;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature32, 355, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature32;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature33, 356, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature33;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Temperature34, 357, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Temperature34;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.TemperaturePost4, 358, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.TemperaturePost4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory31, 359, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory31;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory32, 360, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory32;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory33, 361, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory33;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.Respiratory34, 362, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.Respiratory34;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.RespiratoryPost4, 363, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.RespiratoryPost4;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost4, 364, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost5, 365, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost6, 366, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost7, 367, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsFeverPost8, 368, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsFeverPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost4, 369, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost5, 370, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost6, 371, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost7, 372, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShiveringPost8, 373, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShiveringPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost4, 374, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost5, 375, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost6, 376, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost7, 377, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsSobPost8, 378, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsSobPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost4, 379, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost5, 380, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost6, 381, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost7, 382, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPainfulPost8, 383, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPainfulPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost4, 384, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost5, 385, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost6, 386, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost7, 387, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsNauseaPost8, 388, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsNauseaPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost4, 389, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost5, 390, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost6, 391, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost7, 392, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsBleedingPost8, 393, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsBleedingPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost4, 394, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost5, 395, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost6, 396, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost7, 397, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHypotensionPost8, 398, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHypotensionPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost4, 399, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost5, 400, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost6, 401, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost7, 402, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsShockPost8, 403, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsShockPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost4, 404, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost5, 405, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost6, 406, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost7, 407, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsUrticariaPost8, 408, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsUrticariaPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost4, 409, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost5, 410, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost6, 411, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost7, 412, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRashPost8, 413, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRashPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost4, 414, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost5, 415, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost6, 416, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost7, 417, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPruritusPost8, 418, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPruritusPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost4, 419, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost5, 420, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost6, 421, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost7, 422, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsAnxiousPost8, 423, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsAnxiousPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost4, 424, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost5, 425, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost6, 426, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost7, 427, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsWeakPost8, 428, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsWeakPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost4, 429, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost5, 430, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost6, 431, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost7, 432, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsPalpitationsPost8, 433, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsPalpitationsPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost4, 434, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost5, 435, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost6, 436, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost7, 437, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMildDyspneaPost8, 438, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMildDyspneaPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost4, 439, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost5, 440, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost6, 441, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost7, 442, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsHeadachePost8, 443, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsHeadachePost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost4, 444, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost5, 445, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost6, 446, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost7, 447, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsRednessOnTheSkinPost8, 448, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsRednessOnTheSkinPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost4, 449, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost5, 450, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost6, 451, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost7, 452, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsTachycardiaPost8, 453, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsTachycardiaPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost4, 454, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost5, 455, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost5;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost6, 456, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost6;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost7, 457, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost7;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.IsMuscleStiffnessPost8, 458, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.IsMuscleStiffnessPost8;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost4, 459, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost4;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost5, 460, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost5;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost6, 461, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost6;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost7, 462, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost7;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(BloodBankTransactionItemMetadata.ColumnNames.OtherReactionPost8, 463, typeof(System.String), esSystemType.String);
			c.PropertyName = BloodBankTransactionItemMetadata.PropertyNames.OtherReactionPost8;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public BloodBankTransactionItemMetadata Meta()
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
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string BagNo = "BagNo";
			public const string ReceivedDate = "ReceivedDate";
			public const string ReceivedTime = "ReceivedTime";
			public const string SRBloodGroupReceived = "SRBloodGroupReceived";
			public const string SRBloodBagStatus = "SRBloodBagStatus";
			public const string IsScreeningLabelPassedPmi = "IsScreeningLabelPassedPmi";
			public const string IsExpiredDate = "IsExpiredDate";
			public const string IsLeak = "IsLeak";
			public const string IsHemolysis = "IsHemolysis";
			public const string IsCrossMatchingSuitability = "IsCrossMatchingSuitability";
			public const string IsClotting = "IsClotting";
			public const string IsBloodTypeCompatibility = "IsBloodTypeCompatibility";
			public const string IsLabelDonorsMatchesWithPatientsForm = "IsLabelDonorsMatchesWithPatientsForm";
			public const string IsScreeningLabelPassedBd = "IsScreeningLabelPassedBd";
			public const string ExaminerByUserID = "ExaminerByUserID";
			public const string UnitOfficer = "UnitOfficer";
			public const string TransfusionStartDateTime = "TransfusionStartDateTime";
			public const string TransfusionEndDateTime = "TransfusionEndDateTime";
			public const string TransfusedOfficerStartBy = "TransfusedOfficerStartBy";
			public const string TransfusedOfficerEndBy = "TransfusedOfficerEndBy";
			public const string QtyTransfusion = "QtyTransfusion";
			public const string SRBloodSource = "SRBloodSource";
			public const string SRBloodSourceFrom = "SRBloodSourceFrom";
			public const string CrossmatchCompatibleMajor = "CrossmatchCompatibleMajor";
			public const string CrossmatchCompatibleMinor = "CrossmatchCompatibleMinor";
			public const string CrossmatchCompatibleMinorLevel = "CrossmatchCompatibleMinorLevel";
			public const string CrossmatchCompatibleAutoControl = "CrossmatchCompatibleAutoControl";
			public const string CrossmatchCompatibleAutoControlLevel = "CrossmatchCompatibleAutoControlLevel";
			public const string CrossmatchCompatibleDctControl = "CrossmatchCompatibleDctControl";
			public const string CrossmatchCompatibleDctControlLevel = "CrossmatchCompatibleDctControlLevel";
			public const string CrossmatchStartDateTime = "CrossmatchStartDateTime";
			public const string CrossmatchEndDateTime = "CrossmatchEndDateTime";
			public const string IsCrossmatchingPassed = "IsCrossmatchingPassed";
			public const string CrossMatchingByUserID = "CrossMatchingByUserID";
			public const string BloodBagTemperature = "BloodBagTemperature";
			public const string BloodBagNotes = "BloodBagNotes";
			public const string IsProceedToTransfusion = "IsProceedToTransfusion";
			public const string BpPre = "BpPre";
			public const string Bp10 = "Bp10";
			public const string Bp30 = "Bp30";
			public const string Bp60 = "Bp60";
			public const string Bp120 = "Bp120";
			public const string Bp180 = "Bp180";
			public const string Bp240 = "Bp240";
			public const string BpPost = "BpPost";
			public const string BpPost2 = "BpPost2";
			public const string BpPost3 = "BpPost3";
			public const string PulsePre = "PulsePre";
			public const string Pulse10 = "Pulse10";
			public const string Pulse30 = "Pulse30";
			public const string Pulse60 = "Pulse60";
			public const string Pulse120 = "Pulse120";
			public const string Pulse180 = "Pulse180";
			public const string Pulse240 = "Pulse240";
			public const string PulsePost = "PulsePost";
			public const string PulsePost2 = "PulsePost2";
			public const string PulsePost3 = "PulsePost3";
			public const string TemperaturePre = "TemperaturePre";
			public const string Temperature10 = "Temperature10";
			public const string Temperature30 = "Temperature30";
			public const string Temperature60 = "Temperature60";
			public const string Temperature120 = "Temperature120";
			public const string Temperature180 = "Temperature180";
			public const string Temperature240 = "Temperature240";
			public const string TemperaturePost = "TemperaturePost";
			public const string TemperaturePost2 = "TemperaturePost2";
			public const string TemperaturePost3 = "TemperaturePost3";
			public const string RespiratoryPre = "RespiratoryPre";
			public const string Respiratory10 = "Respiratory10";
			public const string Respiratory30 = "Respiratory30";
			public const string Respiratory60 = "Respiratory60";
			public const string Respiratory120 = "Respiratory120";
			public const string Respiratory180 = "Respiratory180";
			public const string Respiratory240 = "Respiratory240";
			public const string RespiratoryPost = "RespiratoryPost";
			public const string RespiratoryPost2 = "RespiratoryPost2";
			public const string RespiratoryPost3 = "RespiratoryPost3";
			public const string IsFeverPre = "IsFeverPre";
			public const string IsFever10 = "IsFever10";
			public const string IsFever30 = "IsFever30";
			public const string IsFever60 = "IsFever60";
			public const string IsFever120 = "IsFever120";
			public const string IsFever180 = "IsFever180";
			public const string IsFever240 = "IsFever240";
			public const string IsFeverPost = "IsFeverPost";
			public const string IsFeverPost2 = "IsFeverPost2";
			public const string IsFeverPost3 = "IsFeverPost3";
			public const string IsShiveringPre = "IsShiveringPre";
			public const string IsShivering10 = "IsShivering10";
			public const string IsShivering30 = "IsShivering30";
			public const string IsShivering60 = "IsShivering60";
			public const string IsShivering120 = "IsShivering120";
			public const string IsShivering180 = "IsShivering180";
			public const string IsShivering240 = "IsShivering240";
			public const string IsShiveringPost = "IsShiveringPost";
			public const string IsShiveringPost2 = "IsShiveringPost2";
			public const string IsShiveringPost3 = "IsShiveringPost3";
			public const string IsSobPre = "IsSobPre";
			public const string IsSob10 = "IsSob10";
			public const string IsSob30 = "IsSob30";
			public const string IsSob60 = "IsSob60";
			public const string IsSob120 = "IsSob120";
			public const string IsSob180 = "IsSob180";
			public const string IsSob240 = "IsSob240";
			public const string IsSobPost = "IsSobPost";
			public const string IsSobPost2 = "IsSobPost2";
			public const string IsSobPost3 = "IsSobPost3";
			public const string IsPainfulPre = "IsPainfulPre";
			public const string IsPainful10 = "IsPainful10";
			public const string IsPainful30 = "IsPainful30";
			public const string IsPainful60 = "IsPainful60";
			public const string IsPainful120 = "IsPainful120";
			public const string IsPainful180 = "IsPainful180";
			public const string IsPainful240 = "IsPainful240";
			public const string IsPainfulPost = "IsPainfulPost";
			public const string IsPainfulPost2 = "IsPainfulPost2";
			public const string IsPainfulPost3 = "IsPainfulPost3";
			public const string IsNauseaPre = "IsNauseaPre";
			public const string IsNausea10 = "IsNausea10";
			public const string IsNausea30 = "IsNausea30";
			public const string IsNausea60 = "IsNausea60";
			public const string IsNausea120 = "IsNausea120";
			public const string IsNausea180 = "IsNausea180";
			public const string IsNausea240 = "IsNausea240";
			public const string IsNauseaPost = "IsNauseaPost";
			public const string IsNauseaPost2 = "IsNauseaPost2";
			public const string IsNauseaPost3 = "IsNauseaPost3";
			public const string IsBleedingPre = "IsBleedingPre";
			public const string IsBleeding10 = "IsBleeding10";
			public const string IsBleeding30 = "IsBleeding30";
			public const string IsBleeding60 = "IsBleeding60";
			public const string IsBleeding120 = "IsBleeding120";
			public const string IsBleeding180 = "IsBleeding180";
			public const string IsBleeding240 = "IsBleeding240";
			public const string IsBleedingPost = "IsBleedingPost";
			public const string IsBleedingPost2 = "IsBleedingPost2";
			public const string IsBleedingPost3 = "IsBleedingPost3";
			public const string IsHypotensionPre = "IsHypotensionPre";
			public const string IsHypotension10 = "IsHypotension10";
			public const string IsHypotension30 = "IsHypotension30";
			public const string IsHypotension60 = "IsHypotension60";
			public const string IsHypotension120 = "IsHypotension120";
			public const string IsHypotension180 = "IsHypotension180";
			public const string IsHypotension240 = "IsHypotension240";
			public const string IsHypotensionPost = "IsHypotensionPost";
			public const string IsHypotensionPost2 = "IsHypotensionPost2";
			public const string IsHypotensionPost3 = "IsHypotensionPost3";
			public const string IsShockPre = "IsShockPre";
			public const string IsShock10 = "IsShock10";
			public const string IsShock30 = "IsShock30";
			public const string IsShock60 = "IsShock60";
			public const string IsShock120 = "IsShock120";
			public const string IsShock180 = "IsShock180";
			public const string IsShock240 = "IsShock240";
			public const string IsShockPost = "IsShockPost";
			public const string IsShockPost2 = "IsShockPost2";
			public const string IsShockPost3 = "IsShockPost3";
			public const string IsUrticariaPre = "IsUrticariaPre";
			public const string IsUrticaria10 = "IsUrticaria10";
			public const string IsUrticaria30 = "IsUrticaria30";
			public const string IsUrticaria60 = "IsUrticaria60";
			public const string IsUrticaria120 = "IsUrticaria120";
			public const string IsUrticaria180 = "IsUrticaria180";
			public const string IsUrticaria240 = "IsUrticaria240";
			public const string IsUrticariaPost = "IsUrticariaPost";
			public const string IsUrticariaPost2 = "IsUrticariaPost2";
			public const string IsUrticariaPost3 = "IsUrticariaPost3";
			public const string IsRashPre = "IsRashPre";
			public const string IsRash10 = "IsRash10";
			public const string IsRash30 = "IsRash30";
			public const string IsRash60 = "IsRash60";
			public const string IsRash120 = "IsRash120";
			public const string IsRash180 = "IsRash180";
			public const string IsRash240 = "IsRash240";
			public const string IsRashPost = "IsRashPost";
			public const string IsRashPost2 = "IsRashPost2";
			public const string IsRashPost3 = "IsRashPost3";
			public const string IsPruritusPre = "IsPruritusPre";
			public const string IsPruritus10 = "IsPruritus10";
			public const string IsPruritus30 = "IsPruritus30";
			public const string IsPruritus60 = "IsPruritus60";
			public const string IsPruritus120 = "IsPruritus120";
			public const string IsPruritus180 = "IsPruritus180";
			public const string IsPruritus240 = "IsPruritus240";
			public const string IsPruritusPost = "IsPruritusPost";
			public const string IsPruritusPost2 = "IsPruritusPost2";
			public const string IsPruritusPost3 = "IsPruritusPost3";
			public const string IsAnxiousPre = "IsAnxiousPre";
			public const string IsAnxious10 = "IsAnxious10";
			public const string IsAnxious30 = "IsAnxious30";
			public const string IsAnxious60 = "IsAnxious60";
			public const string IsAnxious120 = "IsAnxious120";
			public const string IsAnxious180 = "IsAnxious180";
			public const string IsAnxious240 = "IsAnxious240";
			public const string IsAnxiousPost = "IsAnxiousPost";
			public const string IsAnxiousPost2 = "IsAnxiousPost2";
			public const string IsAnxiousPost3 = "IsAnxiousPost3";
			public const string IsWeakPre = "IsWeakPre";
			public const string IsWeak10 = "IsWeak10";
			public const string IsWeak30 = "IsWeak30";
			public const string IsWeak60 = "IsWeak60";
			public const string IsWeak120 = "IsWeak120";
			public const string IsWeak180 = "IsWeak180";
			public const string IsWeak240 = "IsWeak240";
			public const string IsWeakPost = "IsWeakPost";
			public const string IsWeakPost2 = "IsWeakPost2";
			public const string IsWeakPost3 = "IsWeakPost3";
			public const string IsPalpitationsPre = "IsPalpitationsPre";
			public const string IsPalpitations10 = "IsPalpitations10";
			public const string IsPalpitations30 = "IsPalpitations30";
			public const string IsPalpitations60 = "IsPalpitations60";
			public const string IsPalpitations120 = "IsPalpitations120";
			public const string IsPalpitations180 = "IsPalpitations180";
			public const string IsPalpitations240 = "IsPalpitations240";
			public const string IsPalpitationsPost = "IsPalpitationsPost";
			public const string IsPalpitationsPost2 = "IsPalpitationsPost2";
			public const string IsPalpitationsPost3 = "IsPalpitationsPost3";
			public const string IsMildDyspneaPre = "IsMildDyspneaPre";
			public const string IsMildDyspnea10 = "IsMildDyspnea10";
			public const string IsMildDyspnea30 = "IsMildDyspnea30";
			public const string IsMildDyspnea60 = "IsMildDyspnea60";
			public const string IsMildDyspnea120 = "IsMildDyspnea120";
			public const string IsMildDyspnea180 = "IsMildDyspnea180";
			public const string IsMildDyspnea240 = "IsMildDyspnea240";
			public const string IsMildDyspneaPost = "IsMildDyspneaPost";
			public const string IsMildDyspneaPost2 = "IsMildDyspneaPost2";
			public const string IsMildDyspneaPost3 = "IsMildDyspneaPost3";
			public const string IsHeadachePre = "IsHeadachePre";
			public const string IsHeadache10 = "IsHeadache10";
			public const string IsHeadache30 = "IsHeadache30";
			public const string IsHeadache60 = "IsHeadache60";
			public const string IsHeadache120 = "IsHeadache120";
			public const string IsHeadache180 = "IsHeadache180";
			public const string IsHeadache240 = "IsHeadache240";
			public const string IsHeadachePost = "IsHeadachePost";
			public const string IsHeadachePost2 = "IsHeadachePost2";
			public const string IsHeadachePost3 = "IsHeadachePost3";
			public const string IsRednessOnTheSkinPre = "IsRednessOnTheSkinPre";
			public const string IsRednessOnTheSkin10 = "IsRednessOnTheSkin10";
			public const string IsRednessOnTheSkin30 = "IsRednessOnTheSkin30";
			public const string IsRednessOnTheSkin60 = "IsRednessOnTheSkin60";
			public const string IsRednessOnTheSkin120 = "IsRednessOnTheSkin120";
			public const string IsRednessOnTheSkin180 = "IsRednessOnTheSkin180";
			public const string IsRednessOnTheSkin240 = "IsRednessOnTheSkin240";
			public const string IsRednessOnTheSkinPost = "IsRednessOnTheSkinPost";
			public const string IsRednessOnTheSkinPost2 = "IsRednessOnTheSkinPost2";
			public const string IsRednessOnTheSkinPost3 = "IsRednessOnTheSkinPost3";
			public const string IsTachycardiaPre = "IsTachycardiaPre";
			public const string IsTachycardia10 = "IsTachycardia10";
			public const string IsTachycardia30 = "IsTachycardia30";
			public const string IsTachycardia60 = "IsTachycardia60";
			public const string IsTachycardia120 = "IsTachycardia120";
			public const string IsTachycardia180 = "IsTachycardia180";
			public const string IsTachycardia240 = "IsTachycardia240";
			public const string IsTachycardiaPost = "IsTachycardiaPost";
			public const string IsTachycardiaPost2 = "IsTachycardiaPost2";
			public const string IsTachycardiaPost3 = "IsTachycardiaPost3";
			public const string IsMuscleStiffnessPre = "IsMuscleStiffnessPre";
			public const string IsMuscleStiffness10 = "IsMuscleStiffness10";
			public const string IsMuscleStiffness30 = "IsMuscleStiffness30";
			public const string IsMuscleStiffness60 = "IsMuscleStiffness60";
			public const string IsMuscleStiffness120 = "IsMuscleStiffness120";
			public const string IsMuscleStiffness180 = "IsMuscleStiffness180";
			public const string IsMuscleStiffness240 = "IsMuscleStiffness240";
			public const string IsMuscleStiffnessPost = "IsMuscleStiffnessPost";
			public const string IsMuscleStiffnessPost2 = "IsMuscleStiffnessPost2";
			public const string IsMuscleStiffnessPost3 = "IsMuscleStiffnessPost3";
			public const string OtherReactionPre = "OtherReactionPre";
			public const string OtherReaction10 = "OtherReaction10";
			public const string OtherReaction30 = "OtherReaction30";
			public const string OtherReaction60 = "OtherReaction60";
			public const string OtherReaction120 = "OtherReaction120";
			public const string OtherReaction180 = "OtherReaction180";
			public const string OtherReaction240 = "OtherReaction240";
			public const string OtherReactionPost = "OtherReactionPost";
			public const string OtherReactionPost2 = "OtherReactionPost2";
			public const string OtherReactionPost3 = "OtherReactionPost3";
			public const string HemoglobinPre = "HemoglobinPre";
			public const string Hemoglobin10 = "Hemoglobin10";
			public const string Hemoglobin30 = "Hemoglobin30";
			public const string Hemoglobin60 = "Hemoglobin60";
			public const string Hemoglobin120 = "Hemoglobin120";
			public const string Hemoglobin180 = "Hemoglobin180";
			public const string Hemoglobin240 = "Hemoglobin240";
			public const string HemoglobinPost = "HemoglobinPost";
			public const string HemoglobinPost2 = "HemoglobinPost2";
			public const string HemoglobinPost3 = "HemoglobinPost3";
			public const string HematocritPre = "HematocritPre";
			public const string Hematocrit10 = "Hematocrit10";
			public const string Hematocrit30 = "Hematocrit30";
			public const string Hematocrit60 = "Hematocrit60";
			public const string Hematocrit120 = "Hematocrit120";
			public const string Hematocrit180 = "Hematocrit180";
			public const string Hematocrit240 = "Hematocrit240";
			public const string HematocritPost = "HematocritPost";
			public const string HematocritPost2 = "HematocritPost2";
			public const string HematocritPost3 = "HematocritPost3";
			public const string PlateletPre = "PlateletPre";
			public const string Platelet10 = "Platelet10";
			public const string Platelet30 = "Platelet30";
			public const string Platelet60 = "Platelet60";
			public const string Platelet120 = "Platelet120";
			public const string Platelet180 = "Platelet180";
			public const string Platelet240 = "Platelet240";
			public const string PlateletPost = "PlateletPost";
			public const string PlateletPost2 = "PlateletPost2";
			public const string PlateletPost3 = "PlateletPost3";
			public const string ActionPre = "ActionPre";
			public const string Action10 = "Action10";
			public const string Action30 = "Action30";
			public const string Action60 = "Action60";
			public const string Action120 = "Action120";
			public const string Action180 = "Action180";
			public const string Action240 = "Action240";
			public const string ActionPost = "ActionPost";
			public const string ActionPost2 = "ActionPost2";
			public const string ActionPost3 = "ActionPost3";
			public const string IsHiv = "IsHiv";
			public const string IsVbrl = "IsVbrl";
			public const string IsHbsAg = "IsHbsAg";
			public const string IsHcv = "IsHcv";
			public const string IsReCheck = "IsReCheck";
			public const string ReCheckDateTime = "ReCheckDateTime";
			public const string IsReCheckExpiredDate = "IsReCheckExpiredDate";
			public const string IsReCheckLeak = "IsReCheckLeak";
			public const string IsReCheckHemolysis = "IsReCheckHemolysis";
			public const string IsReCheckCrossMatchingSuitability = "IsReCheckCrossMatchingSuitability";
			public const string IsReCheckClotting = "IsReCheckClotting";
			public const string IsReCheckBloodTypeCompatibility = "IsReCheckBloodTypeCompatibility";
			public const string ReCheckOfficer = "ReCheckOfficer";
			public const string ReCheckOfficer2 = "ReCheckOfficer2";
			public const string Notes = "Notes";
			public const string IsCrossmatchBillProceed = "IsCrossmatchBillProceed";
			public const string IsTransfusionBillProceed = "IsTransfusionBillProceed";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScreening1 = "IsScreening1";
			public const string IsScreening2 = "IsScreening2";
			public const string IsScreening3 = "IsScreening3";
			public const string UnitOfficerByUserID = "UnitOfficerByUserID";
			public const string Bp31 = "Bp31";
			public const string Bp32 = "Bp32";
			public const string Bp33 = "Bp33";
			public const string Bp34 = "Bp34";
			public const string BpPost4 = "BpPost4";
			public const string Pulse31 = "Pulse31";
			public const string Pulse32 = "Pulse32";
			public const string Pulse33 = "Pulse33";
			public const string Pulse34 = "Pulse34";
			public const string PulsePost4 = "PulsePost4";
			public const string Temperature31 = "Temperature31";
			public const string Temperature32 = "Temperature32";
			public const string Temperature33 = "Temperature33";
			public const string Temperature34 = "Temperature34";
			public const string TemperaturePost4 = "TemperaturePost4";
			public const string Respiratory31 = "Respiratory31";
			public const string Respiratory32 = "Respiratory32";
			public const string Respiratory33 = "Respiratory33";
			public const string Respiratory34 = "Respiratory34";
			public const string RespiratoryPost4 = "RespiratoryPost4";
			public const string IsFeverPost4 = "IsFeverPost4";
			public const string IsFeverPost5 = "IsFeverPost5";
			public const string IsFeverPost6 = "IsFeverPost6";
			public const string IsFeverPost7 = "IsFeverPost7";
			public const string IsFeverPost8 = "IsFeverPost8";
			public const string IsShiveringPost4 = "IsShiveringPost4";
			public const string IsShiveringPost5 = "IsShiveringPost5";
			public const string IsShiveringPost6 = "IsShiveringPost6";
			public const string IsShiveringPost7 = "IsShiveringPost7";
			public const string IsShiveringPost8 = "IsShiveringPost8";
			public const string IsSobPost4 = "IsSobPost4";
			public const string IsSobPost5 = "IsSobPost5";
			public const string IsSobPost6 = "IsSobPost6";
			public const string IsSobPost7 = "IsSobPost7";
			public const string IsSobPost8 = "IsSobPost8";
			public const string IsPainfulPost4 = "IsPainfulPost4";
			public const string IsPainfulPost5 = "IsPainfulPost5";
			public const string IsPainfulPost6 = "IsPainfulPost6";
			public const string IsPainfulPost7 = "IsPainfulPost7";
			public const string IsPainfulPost8 = "IsPainfulPost8";
			public const string IsNauseaPost4 = "IsNauseaPost4";
			public const string IsNauseaPost5 = "IsNauseaPost5";
			public const string IsNauseaPost6 = "IsNauseaPost6";
			public const string IsNauseaPost7 = "IsNauseaPost7";
			public const string IsNauseaPost8 = "IsNauseaPost8";
			public const string IsBleedingPost4 = "IsBleedingPost4";
			public const string IsBleedingPost5 = "IsBleedingPost5";
			public const string IsBleedingPost6 = "IsBleedingPost6";
			public const string IsBleedingPost7 = "IsBleedingPost7";
			public const string IsBleedingPost8 = "IsBleedingPost8";
			public const string IsHypotensionPost4 = "IsHypotensionPost4";
			public const string IsHypotensionPost5 = "IsHypotensionPost5";
			public const string IsHypotensionPost6 = "IsHypotensionPost6";
			public const string IsHypotensionPost7 = "IsHypotensionPost7";
			public const string IsHypotensionPost8 = "IsHypotensionPost8";
			public const string IsShockPost4 = "IsShockPost4";
			public const string IsShockPost5 = "IsShockPost5";
			public const string IsShockPost6 = "IsShockPost6";
			public const string IsShockPost7 = "IsShockPost7";
			public const string IsShockPost8 = "IsShockPost8";
			public const string IsUrticariaPost4 = "IsUrticariaPost4";
			public const string IsUrticariaPost5 = "IsUrticariaPost5";
			public const string IsUrticariaPost6 = "IsUrticariaPost6";
			public const string IsUrticariaPost7 = "IsUrticariaPost7";
			public const string IsUrticariaPost8 = "IsUrticariaPost8";
			public const string IsRashPost4 = "IsRashPost4";
			public const string IsRashPost5 = "IsRashPost5";
			public const string IsRashPost6 = "IsRashPost6";
			public const string IsRashPost7 = "IsRashPost7";
			public const string IsRashPost8 = "IsRashPost8";
			public const string IsPruritusPost4 = "IsPruritusPost4";
			public const string IsPruritusPost5 = "IsPruritusPost5";
			public const string IsPruritusPost6 = "IsPruritusPost6";
			public const string IsPruritusPost7 = "IsPruritusPost7";
			public const string IsPruritusPost8 = "IsPruritusPost8";
			public const string IsAnxiousPost4 = "IsAnxiousPost4";
			public const string IsAnxiousPost5 = "IsAnxiousPost5";
			public const string IsAnxiousPost6 = "IsAnxiousPost6";
			public const string IsAnxiousPost7 = "IsAnxiousPost7";
			public const string IsAnxiousPost8 = "IsAnxiousPost8";
			public const string IsWeakPost4 = "IsWeakPost4";
			public const string IsWeakPost5 = "IsWeakPost5";
			public const string IsWeakPost6 = "IsWeakPost6";
			public const string IsWeakPost7 = "IsWeakPost7";
			public const string IsWeakPost8 = "IsWeakPost8";
			public const string IsPalpitationsPost4 = "IsPalpitationsPost4";
			public const string IsPalpitationsPost5 = "IsPalpitationsPost5";
			public const string IsPalpitationsPost6 = "IsPalpitationsPost6";
			public const string IsPalpitationsPost7 = "IsPalpitationsPost7";
			public const string IsPalpitationsPost8 = "IsPalpitationsPost8";
			public const string IsMildDyspneaPost4 = "IsMildDyspneaPost4";
			public const string IsMildDyspneaPost5 = "IsMildDyspneaPost5";
			public const string IsMildDyspneaPost6 = "IsMildDyspneaPost6";
			public const string IsMildDyspneaPost7 = "IsMildDyspneaPost7";
			public const string IsMildDyspneaPost8 = "IsMildDyspneaPost8";
			public const string IsHeadachePost4 = "IsHeadachePost4";
			public const string IsHeadachePost5 = "IsHeadachePost5";
			public const string IsHeadachePost6 = "IsHeadachePost6";
			public const string IsHeadachePost7 = "IsHeadachePost7";
			public const string IsHeadachePost8 = "IsHeadachePost8";
			public const string IsRednessOnTheSkinPost4 = "IsRednessOnTheSkinPost4";
			public const string IsRednessOnTheSkinPost5 = "IsRednessOnTheSkinPost5";
			public const string IsRednessOnTheSkinPost6 = "IsRednessOnTheSkinPost6";
			public const string IsRednessOnTheSkinPost7 = "IsRednessOnTheSkinPost7";
			public const string IsRednessOnTheSkinPost8 = "IsRednessOnTheSkinPost8";
			public const string IsTachycardiaPost4 = "IsTachycardiaPost4";
			public const string IsTachycardiaPost5 = "IsTachycardiaPost5";
			public const string IsTachycardiaPost6 = "IsTachycardiaPost6";
			public const string IsTachycardiaPost7 = "IsTachycardiaPost7";
			public const string IsTachycardiaPost8 = "IsTachycardiaPost8";
			public const string IsMuscleStiffnessPost4 = "IsMuscleStiffnessPost4";
			public const string IsMuscleStiffnessPost5 = "IsMuscleStiffnessPost5";
			public const string IsMuscleStiffnessPost6 = "IsMuscleStiffnessPost6";
			public const string IsMuscleStiffnessPost7 = "IsMuscleStiffnessPost7";
			public const string IsMuscleStiffnessPost8 = "IsMuscleStiffnessPost8";
			public const string OtherReactionPost4 = "OtherReactionPost4";
			public const string OtherReactionPost5 = "OtherReactionPost5";
			public const string OtherReactionPost6 = "OtherReactionPost6";
			public const string OtherReactionPost7 = "OtherReactionPost7";
			public const string OtherReactionPost8 = "OtherReactionPost8";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string BagNo = "BagNo";
			public const string ReceivedDate = "ReceivedDate";
			public const string ReceivedTime = "ReceivedTime";
			public const string SRBloodGroupReceived = "SRBloodGroupReceived";
			public const string SRBloodBagStatus = "SRBloodBagStatus";
			public const string IsScreeningLabelPassedPmi = "IsScreeningLabelPassedPmi";
			public const string IsExpiredDate = "IsExpiredDate";
			public const string IsLeak = "IsLeak";
			public const string IsHemolysis = "IsHemolysis";
			public const string IsCrossMatchingSuitability = "IsCrossMatchingSuitability";
			public const string IsClotting = "IsClotting";
			public const string IsBloodTypeCompatibility = "IsBloodTypeCompatibility";
			public const string IsLabelDonorsMatchesWithPatientsForm = "IsLabelDonorsMatchesWithPatientsForm";
			public const string IsScreeningLabelPassedBd = "IsScreeningLabelPassedBd";
			public const string ExaminerByUserID = "ExaminerByUserID";
			public const string UnitOfficer = "UnitOfficer";
			public const string TransfusionStartDateTime = "TransfusionStartDateTime";
			public const string TransfusionEndDateTime = "TransfusionEndDateTime";
			public const string TransfusedOfficerStartBy = "TransfusedOfficerStartBy";
			public const string TransfusedOfficerEndBy = "TransfusedOfficerEndBy";
			public const string QtyTransfusion = "QtyTransfusion";
			public const string SRBloodSource = "SRBloodSource";
			public const string SRBloodSourceFrom = "SRBloodSourceFrom";
			public const string CrossmatchCompatibleMajor = "CrossmatchCompatibleMajor";
			public const string CrossmatchCompatibleMinor = "CrossmatchCompatibleMinor";
			public const string CrossmatchCompatibleMinorLevel = "CrossmatchCompatibleMinorLevel";
			public const string CrossmatchCompatibleAutoControl = "CrossmatchCompatibleAutoControl";
			public const string CrossmatchCompatibleAutoControlLevel = "CrossmatchCompatibleAutoControlLevel";
			public const string CrossmatchCompatibleDctControl = "CrossmatchCompatibleDctControl";
			public const string CrossmatchCompatibleDctControlLevel = "CrossmatchCompatibleDctControlLevel";
			public const string CrossmatchStartDateTime = "CrossmatchStartDateTime";
			public const string CrossmatchEndDateTime = "CrossmatchEndDateTime";
			public const string IsCrossmatchingPassed = "IsCrossmatchingPassed";
			public const string CrossMatchingByUserID = "CrossMatchingByUserID";
			public const string BloodBagTemperature = "BloodBagTemperature";
			public const string BloodBagNotes = "BloodBagNotes";
			public const string IsProceedToTransfusion = "IsProceedToTransfusion";
			public const string BpPre = "BpPre";
			public const string Bp10 = "Bp10";
			public const string Bp30 = "Bp30";
			public const string Bp60 = "Bp60";
			public const string Bp120 = "Bp120";
			public const string Bp180 = "Bp180";
			public const string Bp240 = "Bp240";
			public const string BpPost = "BpPost";
			public const string BpPost2 = "BpPost2";
			public const string BpPost3 = "BpPost3";
			public const string PulsePre = "PulsePre";
			public const string Pulse10 = "Pulse10";
			public const string Pulse30 = "Pulse30";
			public const string Pulse60 = "Pulse60";
			public const string Pulse120 = "Pulse120";
			public const string Pulse180 = "Pulse180";
			public const string Pulse240 = "Pulse240";
			public const string PulsePost = "PulsePost";
			public const string PulsePost2 = "PulsePost2";
			public const string PulsePost3 = "PulsePost3";
			public const string TemperaturePre = "TemperaturePre";
			public const string Temperature10 = "Temperature10";
			public const string Temperature30 = "Temperature30";
			public const string Temperature60 = "Temperature60";
			public const string Temperature120 = "Temperature120";
			public const string Temperature180 = "Temperature180";
			public const string Temperature240 = "Temperature240";
			public const string TemperaturePost = "TemperaturePost";
			public const string TemperaturePost2 = "TemperaturePost2";
			public const string TemperaturePost3 = "TemperaturePost3";
			public const string RespiratoryPre = "RespiratoryPre";
			public const string Respiratory10 = "Respiratory10";
			public const string Respiratory30 = "Respiratory30";
			public const string Respiratory60 = "Respiratory60";
			public const string Respiratory120 = "Respiratory120";
			public const string Respiratory180 = "Respiratory180";
			public const string Respiratory240 = "Respiratory240";
			public const string RespiratoryPost = "RespiratoryPost";
			public const string RespiratoryPost2 = "RespiratoryPost2";
			public const string RespiratoryPost3 = "RespiratoryPost3";
			public const string IsFeverPre = "IsFeverPre";
			public const string IsFever10 = "IsFever10";
			public const string IsFever30 = "IsFever30";
			public const string IsFever60 = "IsFever60";
			public const string IsFever120 = "IsFever120";
			public const string IsFever180 = "IsFever180";
			public const string IsFever240 = "IsFever240";
			public const string IsFeverPost = "IsFeverPost";
			public const string IsFeverPost2 = "IsFeverPost2";
			public const string IsFeverPost3 = "IsFeverPost3";
			public const string IsShiveringPre = "IsShiveringPre";
			public const string IsShivering10 = "IsShivering10";
			public const string IsShivering30 = "IsShivering30";
			public const string IsShivering60 = "IsShivering60";
			public const string IsShivering120 = "IsShivering120";
			public const string IsShivering180 = "IsShivering180";
			public const string IsShivering240 = "IsShivering240";
			public const string IsShiveringPost = "IsShiveringPost";
			public const string IsShiveringPost2 = "IsShiveringPost2";
			public const string IsShiveringPost3 = "IsShiveringPost3";
			public const string IsSobPre = "IsSobPre";
			public const string IsSob10 = "IsSob10";
			public const string IsSob30 = "IsSob30";
			public const string IsSob60 = "IsSob60";
			public const string IsSob120 = "IsSob120";
			public const string IsSob180 = "IsSob180";
			public const string IsSob240 = "IsSob240";
			public const string IsSobPost = "IsSobPost";
			public const string IsSobPost2 = "IsSobPost2";
			public const string IsSobPost3 = "IsSobPost3";
			public const string IsPainfulPre = "IsPainfulPre";
			public const string IsPainful10 = "IsPainful10";
			public const string IsPainful30 = "IsPainful30";
			public const string IsPainful60 = "IsPainful60";
			public const string IsPainful120 = "IsPainful120";
			public const string IsPainful180 = "IsPainful180";
			public const string IsPainful240 = "IsPainful240";
			public const string IsPainfulPost = "IsPainfulPost";
			public const string IsPainfulPost2 = "IsPainfulPost2";
			public const string IsPainfulPost3 = "IsPainfulPost3";
			public const string IsNauseaPre = "IsNauseaPre";
			public const string IsNausea10 = "IsNausea10";
			public const string IsNausea30 = "IsNausea30";
			public const string IsNausea60 = "IsNausea60";
			public const string IsNausea120 = "IsNausea120";
			public const string IsNausea180 = "IsNausea180";
			public const string IsNausea240 = "IsNausea240";
			public const string IsNauseaPost = "IsNauseaPost";
			public const string IsNauseaPost2 = "IsNauseaPost2";
			public const string IsNauseaPost3 = "IsNauseaPost3";
			public const string IsBleedingPre = "IsBleedingPre";
			public const string IsBleeding10 = "IsBleeding10";
			public const string IsBleeding30 = "IsBleeding30";
			public const string IsBleeding60 = "IsBleeding60";
			public const string IsBleeding120 = "IsBleeding120";
			public const string IsBleeding180 = "IsBleeding180";
			public const string IsBleeding240 = "IsBleeding240";
			public const string IsBleedingPost = "IsBleedingPost";
			public const string IsBleedingPost2 = "IsBleedingPost2";
			public const string IsBleedingPost3 = "IsBleedingPost3";
			public const string IsHypotensionPre = "IsHypotensionPre";
			public const string IsHypotension10 = "IsHypotension10";
			public const string IsHypotension30 = "IsHypotension30";
			public const string IsHypotension60 = "IsHypotension60";
			public const string IsHypotension120 = "IsHypotension120";
			public const string IsHypotension180 = "IsHypotension180";
			public const string IsHypotension240 = "IsHypotension240";
			public const string IsHypotensionPost = "IsHypotensionPost";
			public const string IsHypotensionPost2 = "IsHypotensionPost2";
			public const string IsHypotensionPost3 = "IsHypotensionPost3";
			public const string IsShockPre = "IsShockPre";
			public const string IsShock10 = "IsShock10";
			public const string IsShock30 = "IsShock30";
			public const string IsShock60 = "IsShock60";
			public const string IsShock120 = "IsShock120";
			public const string IsShock180 = "IsShock180";
			public const string IsShock240 = "IsShock240";
			public const string IsShockPost = "IsShockPost";
			public const string IsShockPost2 = "IsShockPost2";
			public const string IsShockPost3 = "IsShockPost3";
			public const string IsUrticariaPre = "IsUrticariaPre";
			public const string IsUrticaria10 = "IsUrticaria10";
			public const string IsUrticaria30 = "IsUrticaria30";
			public const string IsUrticaria60 = "IsUrticaria60";
			public const string IsUrticaria120 = "IsUrticaria120";
			public const string IsUrticaria180 = "IsUrticaria180";
			public const string IsUrticaria240 = "IsUrticaria240";
			public const string IsUrticariaPost = "IsUrticariaPost";
			public const string IsUrticariaPost2 = "IsUrticariaPost2";
			public const string IsUrticariaPost3 = "IsUrticariaPost3";
			public const string IsRashPre = "IsRashPre";
			public const string IsRash10 = "IsRash10";
			public const string IsRash30 = "IsRash30";
			public const string IsRash60 = "IsRash60";
			public const string IsRash120 = "IsRash120";
			public const string IsRash180 = "IsRash180";
			public const string IsRash240 = "IsRash240";
			public const string IsRashPost = "IsRashPost";
			public const string IsRashPost2 = "IsRashPost2";
			public const string IsRashPost3 = "IsRashPost3";
			public const string IsPruritusPre = "IsPruritusPre";
			public const string IsPruritus10 = "IsPruritus10";
			public const string IsPruritus30 = "IsPruritus30";
			public const string IsPruritus60 = "IsPruritus60";
			public const string IsPruritus120 = "IsPruritus120";
			public const string IsPruritus180 = "IsPruritus180";
			public const string IsPruritus240 = "IsPruritus240";
			public const string IsPruritusPost = "IsPruritusPost";
			public const string IsPruritusPost2 = "IsPruritusPost2";
			public const string IsPruritusPost3 = "IsPruritusPost3";
			public const string IsAnxiousPre = "IsAnxiousPre";
			public const string IsAnxious10 = "IsAnxious10";
			public const string IsAnxious30 = "IsAnxious30";
			public const string IsAnxious60 = "IsAnxious60";
			public const string IsAnxious120 = "IsAnxious120";
			public const string IsAnxious180 = "IsAnxious180";
			public const string IsAnxious240 = "IsAnxious240";
			public const string IsAnxiousPost = "IsAnxiousPost";
			public const string IsAnxiousPost2 = "IsAnxiousPost2";
			public const string IsAnxiousPost3 = "IsAnxiousPost3";
			public const string IsWeakPre = "IsWeakPre";
			public const string IsWeak10 = "IsWeak10";
			public const string IsWeak30 = "IsWeak30";
			public const string IsWeak60 = "IsWeak60";
			public const string IsWeak120 = "IsWeak120";
			public const string IsWeak180 = "IsWeak180";
			public const string IsWeak240 = "IsWeak240";
			public const string IsWeakPost = "IsWeakPost";
			public const string IsWeakPost2 = "IsWeakPost2";
			public const string IsWeakPost3 = "IsWeakPost3";
			public const string IsPalpitationsPre = "IsPalpitationsPre";
			public const string IsPalpitations10 = "IsPalpitations10";
			public const string IsPalpitations30 = "IsPalpitations30";
			public const string IsPalpitations60 = "IsPalpitations60";
			public const string IsPalpitations120 = "IsPalpitations120";
			public const string IsPalpitations180 = "IsPalpitations180";
			public const string IsPalpitations240 = "IsPalpitations240";
			public const string IsPalpitationsPost = "IsPalpitationsPost";
			public const string IsPalpitationsPost2 = "IsPalpitationsPost2";
			public const string IsPalpitationsPost3 = "IsPalpitationsPost3";
			public const string IsMildDyspneaPre = "IsMildDyspneaPre";
			public const string IsMildDyspnea10 = "IsMildDyspnea10";
			public const string IsMildDyspnea30 = "IsMildDyspnea30";
			public const string IsMildDyspnea60 = "IsMildDyspnea60";
			public const string IsMildDyspnea120 = "IsMildDyspnea120";
			public const string IsMildDyspnea180 = "IsMildDyspnea180";
			public const string IsMildDyspnea240 = "IsMildDyspnea240";
			public const string IsMildDyspneaPost = "IsMildDyspneaPost";
			public const string IsMildDyspneaPost2 = "IsMildDyspneaPost2";
			public const string IsMildDyspneaPost3 = "IsMildDyspneaPost3";
			public const string IsHeadachePre = "IsHeadachePre";
			public const string IsHeadache10 = "IsHeadache10";
			public const string IsHeadache30 = "IsHeadache30";
			public const string IsHeadache60 = "IsHeadache60";
			public const string IsHeadache120 = "IsHeadache120";
			public const string IsHeadache180 = "IsHeadache180";
			public const string IsHeadache240 = "IsHeadache240";
			public const string IsHeadachePost = "IsHeadachePost";
			public const string IsHeadachePost2 = "IsHeadachePost2";
			public const string IsHeadachePost3 = "IsHeadachePost3";
			public const string IsRednessOnTheSkinPre = "IsRednessOnTheSkinPre";
			public const string IsRednessOnTheSkin10 = "IsRednessOnTheSkin10";
			public const string IsRednessOnTheSkin30 = "IsRednessOnTheSkin30";
			public const string IsRednessOnTheSkin60 = "IsRednessOnTheSkin60";
			public const string IsRednessOnTheSkin120 = "IsRednessOnTheSkin120";
			public const string IsRednessOnTheSkin180 = "IsRednessOnTheSkin180";
			public const string IsRednessOnTheSkin240 = "IsRednessOnTheSkin240";
			public const string IsRednessOnTheSkinPost = "IsRednessOnTheSkinPost";
			public const string IsRednessOnTheSkinPost2 = "IsRednessOnTheSkinPost2";
			public const string IsRednessOnTheSkinPost3 = "IsRednessOnTheSkinPost3";
			public const string IsTachycardiaPre = "IsTachycardiaPre";
			public const string IsTachycardia10 = "IsTachycardia10";
			public const string IsTachycardia30 = "IsTachycardia30";
			public const string IsTachycardia60 = "IsTachycardia60";
			public const string IsTachycardia120 = "IsTachycardia120";
			public const string IsTachycardia180 = "IsTachycardia180";
			public const string IsTachycardia240 = "IsTachycardia240";
			public const string IsTachycardiaPost = "IsTachycardiaPost";
			public const string IsTachycardiaPost2 = "IsTachycardiaPost2";
			public const string IsTachycardiaPost3 = "IsTachycardiaPost3";
			public const string IsMuscleStiffnessPre = "IsMuscleStiffnessPre";
			public const string IsMuscleStiffness10 = "IsMuscleStiffness10";
			public const string IsMuscleStiffness30 = "IsMuscleStiffness30";
			public const string IsMuscleStiffness60 = "IsMuscleStiffness60";
			public const string IsMuscleStiffness120 = "IsMuscleStiffness120";
			public const string IsMuscleStiffness180 = "IsMuscleStiffness180";
			public const string IsMuscleStiffness240 = "IsMuscleStiffness240";
			public const string IsMuscleStiffnessPost = "IsMuscleStiffnessPost";
			public const string IsMuscleStiffnessPost2 = "IsMuscleStiffnessPost2";
			public const string IsMuscleStiffnessPost3 = "IsMuscleStiffnessPost3";
			public const string OtherReactionPre = "OtherReactionPre";
			public const string OtherReaction10 = "OtherReaction10";
			public const string OtherReaction30 = "OtherReaction30";
			public const string OtherReaction60 = "OtherReaction60";
			public const string OtherReaction120 = "OtherReaction120";
			public const string OtherReaction180 = "OtherReaction180";
			public const string OtherReaction240 = "OtherReaction240";
			public const string OtherReactionPost = "OtherReactionPost";
			public const string OtherReactionPost2 = "OtherReactionPost2";
			public const string OtherReactionPost3 = "OtherReactionPost3";
			public const string HemoglobinPre = "HemoglobinPre";
			public const string Hemoglobin10 = "Hemoglobin10";
			public const string Hemoglobin30 = "Hemoglobin30";
			public const string Hemoglobin60 = "Hemoglobin60";
			public const string Hemoglobin120 = "Hemoglobin120";
			public const string Hemoglobin180 = "Hemoglobin180";
			public const string Hemoglobin240 = "Hemoglobin240";
			public const string HemoglobinPost = "HemoglobinPost";
			public const string HemoglobinPost2 = "HemoglobinPost2";
			public const string HemoglobinPost3 = "HemoglobinPost3";
			public const string HematocritPre = "HematocritPre";
			public const string Hematocrit10 = "Hematocrit10";
			public const string Hematocrit30 = "Hematocrit30";
			public const string Hematocrit60 = "Hematocrit60";
			public const string Hematocrit120 = "Hematocrit120";
			public const string Hematocrit180 = "Hematocrit180";
			public const string Hematocrit240 = "Hematocrit240";
			public const string HematocritPost = "HematocritPost";
			public const string HematocritPost2 = "HematocritPost2";
			public const string HematocritPost3 = "HematocritPost3";
			public const string PlateletPre = "PlateletPre";
			public const string Platelet10 = "Platelet10";
			public const string Platelet30 = "Platelet30";
			public const string Platelet60 = "Platelet60";
			public const string Platelet120 = "Platelet120";
			public const string Platelet180 = "Platelet180";
			public const string Platelet240 = "Platelet240";
			public const string PlateletPost = "PlateletPost";
			public const string PlateletPost2 = "PlateletPost2";
			public const string PlateletPost3 = "PlateletPost3";
			public const string ActionPre = "ActionPre";
			public const string Action10 = "Action10";
			public const string Action30 = "Action30";
			public const string Action60 = "Action60";
			public const string Action120 = "Action120";
			public const string Action180 = "Action180";
			public const string Action240 = "Action240";
			public const string ActionPost = "ActionPost";
			public const string ActionPost2 = "ActionPost2";
			public const string ActionPost3 = "ActionPost3";
			public const string IsHiv = "IsHiv";
			public const string IsVbrl = "IsVbrl";
			public const string IsHbsAg = "IsHbsAg";
			public const string IsHcv = "IsHcv";
			public const string IsReCheck = "IsReCheck";
			public const string ReCheckDateTime = "ReCheckDateTime";
			public const string IsReCheckExpiredDate = "IsReCheckExpiredDate";
			public const string IsReCheckLeak = "IsReCheckLeak";
			public const string IsReCheckHemolysis = "IsReCheckHemolysis";
			public const string IsReCheckCrossMatchingSuitability = "IsReCheckCrossMatchingSuitability";
			public const string IsReCheckClotting = "IsReCheckClotting";
			public const string IsReCheckBloodTypeCompatibility = "IsReCheckBloodTypeCompatibility";
			public const string ReCheckOfficer = "ReCheckOfficer";
			public const string ReCheckOfficer2 = "ReCheckOfficer2";
			public const string Notes = "Notes";
			public const string IsCrossmatchBillProceed = "IsCrossmatchBillProceed";
			public const string IsTransfusionBillProceed = "IsTransfusionBillProceed";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsScreening1 = "IsScreening1";
			public const string IsScreening2 = "IsScreening2";
			public const string IsScreening3 = "IsScreening3";
			public const string UnitOfficerByUserID = "UnitOfficerByUserID";
			public const string Bp31 = "Bp31";
			public const string Bp32 = "Bp32";
			public const string Bp33 = "Bp33";
			public const string Bp34 = "Bp34";
			public const string BpPost4 = "BpPost4";
			public const string Pulse31 = "Pulse31";
			public const string Pulse32 = "Pulse32";
			public const string Pulse33 = "Pulse33";
			public const string Pulse34 = "Pulse34";
			public const string PulsePost4 = "PulsePost4";
			public const string Temperature31 = "Temperature31";
			public const string Temperature32 = "Temperature32";
			public const string Temperature33 = "Temperature33";
			public const string Temperature34 = "Temperature34";
			public const string TemperaturePost4 = "TemperaturePost4";
			public const string Respiratory31 = "Respiratory31";
			public const string Respiratory32 = "Respiratory32";
			public const string Respiratory33 = "Respiratory33";
			public const string Respiratory34 = "Respiratory34";
			public const string RespiratoryPost4 = "RespiratoryPost4";
			public const string IsFeverPost4 = "IsFeverPost4";
			public const string IsFeverPost5 = "IsFeverPost5";
			public const string IsFeverPost6 = "IsFeverPost6";
			public const string IsFeverPost7 = "IsFeverPost7";
			public const string IsFeverPost8 = "IsFeverPost8";
			public const string IsShiveringPost4 = "IsShiveringPost4";
			public const string IsShiveringPost5 = "IsShiveringPost5";
			public const string IsShiveringPost6 = "IsShiveringPost6";
			public const string IsShiveringPost7 = "IsShiveringPost7";
			public const string IsShiveringPost8 = "IsShiveringPost8";
			public const string IsSobPost4 = "IsSobPost4";
			public const string IsSobPost5 = "IsSobPost5";
			public const string IsSobPost6 = "IsSobPost6";
			public const string IsSobPost7 = "IsSobPost7";
			public const string IsSobPost8 = "IsSobPost8";
			public const string IsPainfulPost4 = "IsPainfulPost4";
			public const string IsPainfulPost5 = "IsPainfulPost5";
			public const string IsPainfulPost6 = "IsPainfulPost6";
			public const string IsPainfulPost7 = "IsPainfulPost7";
			public const string IsPainfulPost8 = "IsPainfulPost8";
			public const string IsNauseaPost4 = "IsNauseaPost4";
			public const string IsNauseaPost5 = "IsNauseaPost5";
			public const string IsNauseaPost6 = "IsNauseaPost6";
			public const string IsNauseaPost7 = "IsNauseaPost7";
			public const string IsNauseaPost8 = "IsNauseaPost8";
			public const string IsBleedingPost4 = "IsBleedingPost4";
			public const string IsBleedingPost5 = "IsBleedingPost5";
			public const string IsBleedingPost6 = "IsBleedingPost6";
			public const string IsBleedingPost7 = "IsBleedingPost7";
			public const string IsBleedingPost8 = "IsBleedingPost8";
			public const string IsHypotensionPost4 = "IsHypotensionPost4";
			public const string IsHypotensionPost5 = "IsHypotensionPost5";
			public const string IsHypotensionPost6 = "IsHypotensionPost6";
			public const string IsHypotensionPost7 = "IsHypotensionPost7";
			public const string IsHypotensionPost8 = "IsHypotensionPost8";
			public const string IsShockPost4 = "IsShockPost4";
			public const string IsShockPost5 = "IsShockPost5";
			public const string IsShockPost6 = "IsShockPost6";
			public const string IsShockPost7 = "IsShockPost7";
			public const string IsShockPost8 = "IsShockPost8";
			public const string IsUrticariaPost4 = "IsUrticariaPost4";
			public const string IsUrticariaPost5 = "IsUrticariaPost5";
			public const string IsUrticariaPost6 = "IsUrticariaPost6";
			public const string IsUrticariaPost7 = "IsUrticariaPost7";
			public const string IsUrticariaPost8 = "IsUrticariaPost8";
			public const string IsRashPost4 = "IsRashPost4";
			public const string IsRashPost5 = "IsRashPost5";
			public const string IsRashPost6 = "IsRashPost6";
			public const string IsRashPost7 = "IsRashPost7";
			public const string IsRashPost8 = "IsRashPost8";
			public const string IsPruritusPost4 = "IsPruritusPost4";
			public const string IsPruritusPost5 = "IsPruritusPost5";
			public const string IsPruritusPost6 = "IsPruritusPost6";
			public const string IsPruritusPost7 = "IsPruritusPost7";
			public const string IsPruritusPost8 = "IsPruritusPost8";
			public const string IsAnxiousPost4 = "IsAnxiousPost4";
			public const string IsAnxiousPost5 = "IsAnxiousPost5";
			public const string IsAnxiousPost6 = "IsAnxiousPost6";
			public const string IsAnxiousPost7 = "IsAnxiousPost7";
			public const string IsAnxiousPost8 = "IsAnxiousPost8";
			public const string IsWeakPost4 = "IsWeakPost4";
			public const string IsWeakPost5 = "IsWeakPost5";
			public const string IsWeakPost6 = "IsWeakPost6";
			public const string IsWeakPost7 = "IsWeakPost7";
			public const string IsWeakPost8 = "IsWeakPost8";
			public const string IsPalpitationsPost4 = "IsPalpitationsPost4";
			public const string IsPalpitationsPost5 = "IsPalpitationsPost5";
			public const string IsPalpitationsPost6 = "IsPalpitationsPost6";
			public const string IsPalpitationsPost7 = "IsPalpitationsPost7";
			public const string IsPalpitationsPost8 = "IsPalpitationsPost8";
			public const string IsMildDyspneaPost4 = "IsMildDyspneaPost4";
			public const string IsMildDyspneaPost5 = "IsMildDyspneaPost5";
			public const string IsMildDyspneaPost6 = "IsMildDyspneaPost6";
			public const string IsMildDyspneaPost7 = "IsMildDyspneaPost7";
			public const string IsMildDyspneaPost8 = "IsMildDyspneaPost8";
			public const string IsHeadachePost4 = "IsHeadachePost4";
			public const string IsHeadachePost5 = "IsHeadachePost5";
			public const string IsHeadachePost6 = "IsHeadachePost6";
			public const string IsHeadachePost7 = "IsHeadachePost7";
			public const string IsHeadachePost8 = "IsHeadachePost8";
			public const string IsRednessOnTheSkinPost4 = "IsRednessOnTheSkinPost4";
			public const string IsRednessOnTheSkinPost5 = "IsRednessOnTheSkinPost5";
			public const string IsRednessOnTheSkinPost6 = "IsRednessOnTheSkinPost6";
			public const string IsRednessOnTheSkinPost7 = "IsRednessOnTheSkinPost7";
			public const string IsRednessOnTheSkinPost8 = "IsRednessOnTheSkinPost8";
			public const string IsTachycardiaPost4 = "IsTachycardiaPost4";
			public const string IsTachycardiaPost5 = "IsTachycardiaPost5";
			public const string IsTachycardiaPost6 = "IsTachycardiaPost6";
			public const string IsTachycardiaPost7 = "IsTachycardiaPost7";
			public const string IsTachycardiaPost8 = "IsTachycardiaPost8";
			public const string IsMuscleStiffnessPost4 = "IsMuscleStiffnessPost4";
			public const string IsMuscleStiffnessPost5 = "IsMuscleStiffnessPost5";
			public const string IsMuscleStiffnessPost6 = "IsMuscleStiffnessPost6";
			public const string IsMuscleStiffnessPost7 = "IsMuscleStiffnessPost7";
			public const string IsMuscleStiffnessPost8 = "IsMuscleStiffnessPost8";
			public const string OtherReactionPost4 = "OtherReactionPost4";
			public const string OtherReactionPost5 = "OtherReactionPost5";
			public const string OtherReactionPost6 = "OtherReactionPost6";
			public const string OtherReactionPost7 = "OtherReactionPost7";
			public const string OtherReactionPost8 = "OtherReactionPost8";
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
			lock (typeof(BloodBankTransactionItemMetadata))
			{
				if (BloodBankTransactionItemMetadata.mapDelegates == null)
				{
					BloodBankTransactionItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (BloodBankTransactionItemMetadata.meta == null)
				{
					BloodBankTransactionItemMetadata.meta = new BloodBankTransactionItemMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BagNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReceivedDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ReceivedTime", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodGroupReceived", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodBagStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsScreeningLabelPassedPmi", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsExpiredDate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLeak", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHemolysis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsCrossMatchingSuitability", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClotting", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBloodTypeCompatibility", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsLabelDonorsMatchesWithPatientsForm", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsScreeningLabelPassedBd", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ExaminerByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("UnitOfficer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransfusionStartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransfusionEndDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("TransfusedOfficerStartBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransfusedOfficerEndBy", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QtyTransfusion", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SRBloodSource", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRBloodSourceFrom", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CrossmatchCompatibleMajor", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CrossmatchCompatibleMinor", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CrossmatchCompatibleMinorLevel", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CrossmatchCompatibleAutoControl", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CrossmatchCompatibleAutoControlLevel", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CrossmatchCompatibleDctControl", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("CrossmatchCompatibleDctControlLevel", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CrossmatchStartDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CrossmatchEndDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsCrossmatchingPassed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CrossMatchingByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BloodBagTemperature", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BloodBagNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsProceedToTransfusion", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("BpPre", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp10", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp30", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp60", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp120", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp180", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp240", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpPost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpPost2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpPost3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PulsePre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PulsePost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PulsePost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PulsePost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TemperaturePre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TemperaturePost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TemperaturePost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TemperaturePost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RespiratoryPre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RespiratoryPost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RespiratoryPost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RespiratoryPost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsFeverPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFever240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShivering240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSob240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainful240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNausea240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleeding240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotension240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShock240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticaria240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRash240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritus240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxious240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeak240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitations240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspnea240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadache240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkin240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardia240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPre", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness10", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness30", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness60", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness120", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness180", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffness240", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OtherReactionPre", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction10", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction30", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction60", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction120", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction180", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReaction240", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HemoglobinPre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hemoglobin240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HemoglobinPost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HemoglobinPost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HemoglobinPost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HematocritPre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Hematocrit240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HematocritPost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HematocritPost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HematocritPost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PlateletPre", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet10", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet30", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet60", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet120", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet180", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Platelet240", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PlateletPost", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PlateletPost2", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PlateletPost3", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("ActionPre", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action10", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action30", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action60", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action120", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action180", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Action240", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionPost", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionPost2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ActionPost3", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsHiv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVbrl", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHbsAg", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHcv", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheck", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReCheckDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsReCheckExpiredDate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheckLeak", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheckHemolysis", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheckCrossMatchingSuitability", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheckClotting", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsReCheckBloodTypeCompatibility", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ReCheckOfficer", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReCheckOfficer2", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsCrossmatchBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTransfusionBillProceed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsScreening1", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsScreening2", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsScreening3", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("UnitOfficerByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp31", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp32", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp33", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Bp34", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BpPost4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Pulse31", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse32", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse33", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Pulse34", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PulsePost4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature31", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature32", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature33", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Temperature34", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("TemperaturePost4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory31", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory32", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory33", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Respiratory34", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("RespiratoryPost4", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsFeverPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsFeverPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShiveringPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsSobPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPainfulPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsNauseaPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBleedingPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHypotensionPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsShockPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsUrticariaPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRashPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPruritusPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAnxiousPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsWeakPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsPalpitationsPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMildDyspneaPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsHeadachePost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsRednessOnTheSkinPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsTachycardiaPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost4", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost5", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost6", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost7", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsMuscleStiffnessPost8", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("OtherReactionPost4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost5", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost6", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost7", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OtherReactionPost8", new esTypeMap("varchar", "System.String"));


				meta.Source = "BloodBankTransactionItem";
				meta.Destination = "BloodBankTransactionItem";
				meta.spInsert = "proc_BloodBankTransactionItemInsert";
				meta.spUpdate = "proc_BloodBankTransactionItemUpdate";
				meta.spDelete = "proc_BloodBankTransactionItemDelete";
				meta.spLoadAll = "proc_BloodBankTransactionItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_BloodBankTransactionItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private BloodBankTransactionItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
