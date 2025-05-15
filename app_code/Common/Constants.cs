using System.Collections.Generic;

public static class Constants
{
    public static string Admin_Url = "/admin";
    public static string Admin_Login_Url = $"{Admin_Url}/login";

    public static readonly Dictionary<string, string> GroupTypeOptionsVI = new Dictionary<string, string>()
    {
        {"post", "Nhóm tin tức"},
        {"page", "Nhóm bài viết"},
        {"product", "Nhóm sản phẩm"},
        {"project", "Nhóm dự án"},
    };

    public static readonly Dictionary<string, string> GroupTypeOptionsEN = new Dictionary<string, string>()
    {
        {"post", "Group of news"},
        {"page", "Group of pages"},
        {"product", "Group of products"},
        {"project", "Group of projects"},
    };

    public static readonly Dictionary<string, string> DataTypeOptions = new Dictionary<string, string>()
    {
        {"text", "Text"},
        {"textarea", "Long text"},
        {"combobox", "Combobox"},
        {"checkbox", "Checkbox"},
        {"color", "Color"},
        {"date", "Date"},
        {"email", "Email"},
        {"month", "Month"},
        {"number", "Number"},
        {"radio", "Radio"},
        {"range", "Range"},
        {"tel", "Telephone"},
        {"time", "Time"},
        {"url", "Url"},
        {"week", "Week"},
        {"image", "Image"},
        {"file", "File"}
    };

    public static readonly Dictionary<string, string> ATargetOptions = new Dictionary<string, string>()
    {
        {"_self", "_self"},
        {"_blank", "_blank"},
        {"_top", "_top"},
    };

    public static readonly Dictionary<string, string> YesNoOptions = new Dictionary<string, string>()
    {
        {"true", "Có"},
        {"false", "Không"},
    };
}

public static class InputControlType
{
    public const string TextBox = "textbox";
    public const string TextArea = "textarea";
    public const string RichTextBox = "richtextbox";
    public const string CheckBox = "checkbox";
    public const string Image = "image";
    public const string Number = "number";
    public const string Date = "date";
    public const string File = "file";
    public const string Link = "link";
    public const string Combobox = "combobox";
    public const string TagsInput = "tagsinput";
}

public static class ComptType
{
    public const string Page_Template = "page_template";
    public const string Page_Element = "page_element";
    //public const string Page_Of_Group = "page_of_group";
    //public const string Page_Of_Node = "page_of_node";
}

public static class PageType
{
    public const string Single = "single";
    public const string List = "list";
    public const string Item = "item";
}