using Microsoft.VisualStudio.TestTools.UnitTesting;
using Temiang.Avicenna.Bridging.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Bridging.Controllers.Tests
{
    [TestClass()]
    public class AntrolControllerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Dal.Interfaces.esProviderFactory.Factory = new Dal.Loader.esDataProviderFactory();
        }

        [TestMethod()]
        public void PostRencanaKunjunganTest()
        {
            for (int i = 0; i < 30; i++)
            {
                Antrol.AmbilAntrean.Request.Root param = new Antrol.AmbilAntrean.Request.Root();
                var ctl = new AntrolController();
                var respon = ctl.PostRencanaKunjungan(param);
                Console.WriteLine(respon.Content);
            }

        }
    }
}