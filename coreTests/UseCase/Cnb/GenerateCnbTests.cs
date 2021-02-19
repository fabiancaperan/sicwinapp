using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace core.UseCase.Cnb.Tests
{
    [TestClass()]
    public class GenerateCnbTests
    {
        [TestMethod()]
        public void GenerateCnbTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BuildTest()
        {
            var sap = new Entities.ConvertData.SapModel()
            {
                Cod_RTL = "3007045519",
                Fiid_Emisor = "0007"
            };
            var entidad = new Entities.MasterData.EntidadesModel()
            {
                fiid = "0007",
                nit = "1919",
                nombre = "test"
            };
            var ret = new GenerateCnb().Build(new List<Entities.ConvertData.SapModel>() { sap },
                new List<Entities.MasterData.EntidadesModel>() { entidad }, new List<Entities.MasterData.CnbsModel>(), new StringBuilder().Append("20200101"));
            Assert.Fail();
        }
    }
}