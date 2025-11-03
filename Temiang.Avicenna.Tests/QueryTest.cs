using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temiang.Avicenna.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.Tests;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Common.Tests
{
    [TestClass()]
    public class QueryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Dal.Interfaces.esProviderFactory.Factory = new Dal.Loader.esDataProviderFactory();
        }


        [TestMethod()]
        public void ClassDisposeTest()
        {
            var tp = new TransPaymentCollection();
            //var dtb = tp.TransPaymentHistory();

            using (var a = new ClassDispose())
            {
                var x = "123";
            }
        }


        [TestMethod()]
        public void TransactionScopeTest()
        {
            using (var tr = new esTransactionScope())
            {
                var ap = new AppParameter();
                ap.LoadByPrimaryKey("acc_AssetDepreciationAmountLimit");
                ap.ParameterName = ap.ParameterName + "_1";
                ap.LastUpdateByUserID = ap.LastUpdateByUserID + "_1";
                ap.Save();
                tr.Complete();

                var apX = new AppParameter();
                apX.LoadByPrimaryKey("acc_AssetsStatusDepreciationJournal");
                apX.ParameterName = apX.ParameterName + "_1";
                apX.LastUpdateByUserID = apX.LastUpdateByUserID + "_1";
                apX.Save();
            }


            using (var tr2 = new esTransactionScope())
            {
                tr2.Complete();
                var ap2 = new AppParameter();
                ap2.LoadByPrimaryKey("acc_AssetDepreciationAmountLimit");
                ap2.ParameterName = ap2.ParameterName + "_1";
                ap2.Save();
            }
        }


        [TestMethod()]
        public void TransactionScope2Test()
        {
            using (var tr = new esTransactionScope())
            {
                var ap = new AppParameter();
                ap.LoadByPrimaryKey("acc_AssetDepreciationAmountLimit");
                ap.ParameterName = ap.ParameterName + "_1";
                ap.LastUpdateByUserID = ap.LastUpdateByUserID + "_1";
                ap.Save();

                var a = 1 + 1;
                //
                //
                //

                tr.Complete();
            }



        }
        [TestMethod()]
        public void Optimize_PrescriptionTest()
        {
            var pars = new esParameters();
            pars.Add("p_RegistrationNo", "");
            var table = BusinessObject.Common.Utils.LoadDataTableFromStoreProcedure("sphis_Emr_MedicationHist_PrescriptionDetail", pars, 0);

        }
    }
}
