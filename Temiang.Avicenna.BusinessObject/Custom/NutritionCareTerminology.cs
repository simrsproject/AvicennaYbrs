using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class NutritionCareTerminology
    {
        public string TerminologyNameForDisplay
        {
            get
            {
                var sb = new StringBuilder();
                int level = this.TerminologyLevel.HasValue ? this.TerminologyLevel.Value : 0;

                for (int i = 0; i < level; i++)
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");

                sb.Append(this.TerminologyName);
                return sb.ToString();
            }
        }
        public string TerminologyParentName
        {
            get
            {
                string retval = string.Empty;
                if (!string.IsNullOrEmpty(this.TerminologyParentID))
                {
                    var t = new NutritionCareTerminology();
                    if (t.LoadByPrimaryKey(this.TerminologyParentID))
                        retval = t.TerminologyName;
                }

                return retval;
            }
        }
        public string Domain
        {
            get
            {
                string retval = string.Empty;
                var t = new NutritionCareTerminology();
                if (t.LoadByPrimaryKey(this.DomainID))
                    retval = t.TerminologyName;

                return retval;
            }
        }
        public static NutritionCareTerminology Get(string id)
        {
            var entity = new NutritionCareTerminology();
            entity.Query.Where(entity.Query.TerminologyID == id);
            if (entity.Query.Load())
                return entity;
            else
                return null;
        }
        public static NutritionCareTerminologyCollection Get()
        {
            var coll = new NutritionCareTerminologyCollection();
            coll.Query.Where();
            coll.Query.Load();
            return coll;
        }

        public static int TotalCount(string id, string name, string parentId, string level)
        {
            int retVal = 0;
            var entity = new NutritionCareTerminology();
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(id))
            {
                string searchTextContain = string.Format("{0}%", id);
                prms.Add(entity.Query.TerminologyID.Like(searchTextContain));
                //prms.Add(entity.Query.TerminologyID.Like(id + "%"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                string searchTextContain = string.Format("%{0}%", name);
                prms.Add(entity.Query.TerminologyName.Like(searchTextContain));
                //prms.Add(entity.Query.TerminologyName.Like("%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(parentId))
            {
                string searchTextContain = string.Format("%{0}%", parentId);
                prms.Add(entity.Query.TerminologyParentID.Like(searchTextContain));
                //prms.Add(entity.Query.TerminologyParentID.Like("%" + parentId + "%"));
            }
            prms.Add(entity.Query.SRNutritionCareTerminologyLevel ==  level);

            entity.Query.es.CountAll = true;
            entity.Query.es.CountAllAlias = "Count";
            entity.Query.es.WithNoLock = true;
            if (prms.Count > 0)
                entity.Query.Where(prms.ToArray());

            if (entity.Query.Load())
                retVal = (int)entity.GetColumn("Count");

            return retVal;
        }

        private static esOrderByItem[] safeOrderByItems(string sortString)
        {
            List<esOrderByItem> list = new List<esOrderByItem>();
            string[] fieldsName = sortString.ToLowerInvariant().Split(char.Parse(","));
            foreach (string field in fieldsName)
            {
                var q = new NutritionCareTerminologyQuery();
                string[] tmp = field.Split(char.Parse("^"));
                bool isDesc = false;
                if (tmp.Length > 1)
                    isDesc = tmp[1].Equals("descending");

                if (tmp[0].Equals("sequenceno"))
                    list.Add(isDesc ? q.SequenceNo.Descending : q.SequenceNo.Ascending);
                if (tmp[0].Equals("terminologyid"))
                    list.Add(isDesc ? q.TerminologyID.Descending : q.TerminologyID.Ascending);
                if (tmp[0].Equals("terminologyname"))
                    list.Add(isDesc ? q.TerminologyName.Descending : q.TerminologyName.Ascending);
            }
            return list.ToArray();
        }

        public static NutritionCareTerminologyCollection GetAllWithPaging(int pageNumber, int pageSize, string id, string name, string parentId, string level, string sortString)
        {
            var coll = new NutritionCareTerminologyCollection();
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(id))
            {
                string searchTextContain = string.Format("{0}%", id);
                prms.Add(coll.Query.TerminologyID.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyID.Like(id + "%"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                string searchTextContain = string.Format("%{0}%", name);
                prms.Add(coll.Query.TerminologyName.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyName.Like("%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(parentId))
            {
                string searchTextContain = string.Format("%{0}%", parentId);
                prms.Add(coll.Query.TerminologyParentID.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyParentID.Like("%" + parentId + "%"));
            }
            prms.Add(coll.Query.SRNutritionCareTerminologyLevel == level);

            coll.Query.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                coll.Query.Where(prms.ToArray());

            coll.Query.es.PageSize = pageSize;
            coll.Query.es.PageNumber = pageNumber + 1;
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static NutritionCareTerminologyCollection GetAll(string id, string name, string parentId, string level, string sortString)
        {
            var coll = new NutritionCareTerminologyCollection();
            List<esComparison> prms = new List<esComparison>();

            if (!string.IsNullOrEmpty(id))
            {
                string searchTextContain = string.Format("{0}%", id);
                prms.Add(coll.Query.TerminologyID.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyID.Like(id + "%"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                string searchTextContain = string.Format("%{0}%", name);
                prms.Add(coll.Query.TerminologyName.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyName.Like("%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(parentId))
            {
                string searchTextContain = string.Format("%{0}%", parentId);
                prms.Add(coll.Query.TerminologyParentID.Like(searchTextContain));
                //prms.Add(coll.Query.TerminologyParentID.Like("%" + parentId + "%"));
            }
            prms.Add(coll.Query.SRNutritionCareTerminologyLevel == level);

            coll.Query.OrderBy(safeOrderByItems(sortString));
            if (prms.Count > 0)
                coll.Query.Where(prms.ToArray());

            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;
        }

        public static NutritionCareTerminologyCollection GetLike(string id, bool detailOnly, bool activeOnly, string level)
        {
            string searchTextContain1 = string.Format("{0}%", id);
            string searchTextContain2 = string.Format("%{0}%", id);
            var coll = new NutritionCareTerminologyCollection();
            coll.Query.Where(coll.Query.TerminologyID.Like(searchTextContain1) || coll.Query.TerminologyName.Like(searchTextContain2));
            if (detailOnly)
                coll.Query.Where(coll.Query.IsDetail == true);
            coll.Query.Where(coll.Query.SRNutritionCareTerminologyLevel == level);

            coll.Query.OrderBy(coll.Query.SequenceNo.Ascending);
            coll.Query.es.WithNoLock = true;
            coll.Query.Load();
            return coll;

        }

        public static NutritionCareTerminologyCollection GetLike(string id, bool detailOnly, string level)
        {
            return GetLike(id, detailOnly, true, level);
        }

    }
}
