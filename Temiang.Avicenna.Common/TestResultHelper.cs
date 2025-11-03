using System;
using System.Linq;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Common
{
    public class TestResultHelper
    {
        #region General Private

        #endregion
        #region General Public
        public static string ReplaceKeyword(string testResult, string PhysicianSenderName)
        {
            return testResult.Replace("[sender]", PhysicianSenderName);
        }
        #endregion
    }
}