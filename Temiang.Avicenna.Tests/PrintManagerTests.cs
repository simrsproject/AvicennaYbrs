using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temiang.Avicenna.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.Tests
{
    [TestClass()]
    public class PrintManagerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Dal.Interfaces.esProviderFactory.Factory = new Dal.Loader.esDataProviderFactory();
        }


        [TestMethod()]
        public void CreatePrintJobTest()
        {
            var printJobParameters = new PrintJobParameterCollection();
            printJobParameters.AddNew("p_PrescriptionNo", "123");
            printJobParameters.AddNew("p_SequenceNo", string.Empty);
            printJobParameters.AddNew("p_Label", string.Empty);
            printJobParameters.AddNew("p_UserID", "Yoi");
            printJobParameters.AddNew("p_UserID", "Yoi"); // test double
            printJobParameters.AddNew("p_Label", string.Empty);
            PrintManager.CreatePrintJob("SLP.01.0055", printJobParameters, "Yoi", "192.168.0.44");
        }

        [TestMethod()]
        public void CreatePrintJobTesz()
        { }

    }
}
