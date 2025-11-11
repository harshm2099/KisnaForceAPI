using System.Collections.Generic;

namespace NewAvatarWebApis.Core.Application.DTOs
{
    public class SoliterSubCategoryNewParams
    {
        public int category_id { get; set; }
        public int master_common_id { get; set; }
        public string btn_cd { get; set; }
    }

    public class SoliterSubCategoryNewData
    {
        public SoliterSubCategoryFilter sub_category { get; set; } = new SoliterSubCategoryFilter();
        public SoliterDsgColorFilter dsg_color { get; set; } = new SoliterDsgColorFilter();
        public SoliterProductTagFilter productTags { get; set; } = new SoliterProductTagFilter();
        public SoliterBrandFilter brand { get; set; } = new SoliterBrandFilter();
        public SoliterGenderFilter gender { get; set; } = new SoliterGenderFilter();
        public SoliterItemSubCategoryFilter item_sub_category { get; set; } = new SoliterItemSubCategoryFilter();
        public SoliterItemSubSubCategoryFilter item_sub_sub_category { get; set; } = new SoliterItemSubSubCategoryFilter();
    }

    public class SoliterSubCategoryFilter
    {
        public string name { get; set; } = "Collection";
        public List<SoliterFilterSubCategory> data { get; set; } = new List<SoliterFilterSubCategory>();
    }

    public class SoliterDsgColorFilter
    {
        public string name { get; set; } = "Color";
        public List<SoliterFilterDsgColor> data { get; set; } = new List<SoliterFilterDsgColor>();
    }

    public class SoliterProductTagFilter
    {
        public string name { get; set; } = "Product Tag";
        public List<SoliterFilterProductTag> data { get; set; } = new List<SoliterFilterProductTag>();
    }

    public class SoliterBrandFilter
    {
        public string name { get; set; } = "Brand";
        public List<SoliterFilterBrand> data { get; set; } = new List<SoliterFilterBrand>();
    }

    public class SoliterGenderFilter
    {
        public string name { get; set; } = "Gender";
        public List<SoliterFilterGender> data { get; set; } = new List<SoliterFilterGender>();
    }

    public class SoliterItemSubCategoryFilter
    {
        public string name { get; set; } = "Complexity";
        public List<SoliterFilterItemSubCategory> data { get; set; } = new List<SoliterFilterItemSubCategory>();
    }

    public class SoliterItemSubSubCategoryFilter
    {
        public string name { get; set; } = "Sub Complexity";
        public List<SoliterFilterItemSubSubCategory> data { get; set; } = new List<SoliterFilterItemSubSubCategory>();
    }

    public class SoliterFilterSubCategory
    {
        public string category_id { get; set; }
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string category_count { get; set; }
    }

    public class SoliterFilterDsgColor
    {
        public string color { get; set; }
    }

    public class SoliterFilterProductTag
    {
        public string tag_name { get; set; }
        public string tag_count { get; set; }
    }

    public class SoliterFilterBrand
    {
        public string brand_id { get; set; }
        public string brand_name { get; set; }
        public string brand_count { get; set; }
    }

    public class SoliterFilterGender
    {
        public string gender_id { get; set; }
        public string gender_name { get; set; }
        public string gender_count { get; set; }
    }

    public class SoliterFilterItemSubCategory
    {
        public string sub_category_id { get; set; }
        public string sub_category_name { get; set; }
        public string sub_category_count { get; set; }
    }

    public class SoliterFilterItemSubSubCategory
    {
        public string sub_sub_category_id { get; set; }
        public string sub_sub_category_name { get; set; }
        public string sub_sub_category_count { get; set; }
    }

}
