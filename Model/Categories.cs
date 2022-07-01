using Newtonsoft.Json;
using System.Collections.Generic;

namespace PublicAPIs
{
    public partial class Categories
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("categories")]
        public List<string> CategoriesCategories { get; set; }
    }

    public partial class Categories
    {
        public static Categories FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Categories>(json, PublicAPIs.Converter.Settings);
        }
    }

    public static partial class Serialize
    {
        public static string ToJson(Categories categories)
        {
            return JsonConvert.SerializeObject(categories, PublicAPIs.Converter.Settings);
        }
    }

}
