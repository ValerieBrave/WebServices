using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public enum ResponseTypes { JSON, XML };
    public enum SortTypes { IdAsc, IdDesc, NameAsc, NameDesc };
    public class GetStudentsQueryObject
    {
        public int? Limit { get; set; }
        public int Offset { get; set; }
        public SortTypes Sort { get; set; }
        public ResponseTypes Format { get; set; }
        public int? MinId { get; set; }
        public int? MaxId { get; set; }
        public string Like { get; set; }
        public string Columns { get; set; }
        public string GlobalLike { get; set; }
    }
    public class DynamicContractResolver : DefaultContractResolver
    {
        private readonly string[] props;

        public DynamicContractResolver(params string[] prop)
        {
            this.props = prop;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> retval = base.CreateProperties(type, memberSerialization);
            retval = retval.Where(p => this.props.Contains(p.PropertyName.ToLower())).ToList();

            return retval;
        }
    }
}