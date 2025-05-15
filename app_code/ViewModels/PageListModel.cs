using System.Collections.Generic;

public class PageListModel
{
    public IDictionary<string, string> Phrases { get; set; } = new Dictionary<string, string>();

    public KeyValuePair<string, string>[] Breadcrumbs { get; set; }

    public string Button { get; set; }

    public string Link_Button { get; set; }

    public string Title { get; set; }

    public string Title_Skill_Level { get; set; }

    public string Content { get; set; }

    public string Title_Type { get; set; }

    public string Title_Categories { get; set; }

    public string CategoryPath { get; set; }

    public string Banner { get; set; }

    public string Banner_Circle { get; set; }

    public string Banner_Dots { get; set; }

    public string MetaDescription { get; set; }

    public string Image_Top_Course { get; set; }

    public string MetaKeywords { get; set; }

    public string AboutUs { get; set; }

    public string Title_AboutUs { get; set; }

    public string Description_AboutUs { get; set; }

    public string Button_AboutUs { get; set; }

    public string Link_Button_AboutUs { get; set; }

    public string Banner_Before { get; set; }

    public string Banner_After { get; set; }

    public string Image { get; set; }

    public string Image_About { get; set; }

    public string Background_Image { get; set; }

    public string Top_Course { get; set; }

    public string Title_Top_Course { get; set; }

    public string Banner_Top_Course { get; set; }

    public string Link_View_All_Course { get; set; }

    public string Icon_Shape { get; set; }

    public string Banner_Video { get; set; }

    public string Video { get; set; }

    public string Icon_Dot_Shape { get; set; }

    public string Title_Works { get; set; }

    public string Works { get; set; }

    public string Description_Works { get; set; }

    public string Button_Works { get; set; }

    public string Title_Facilities { get; set; }

    public string Link_Works { get; set; }

    public string Icon_Banner_02 { get; set; }

    public string Icon_Banner_04 { get; set; }

    public int PageSize { get; set; } = 12;

    public long TotalPages { get; set; }

    public long CurrentPage { get; set; }
}