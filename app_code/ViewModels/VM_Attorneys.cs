using SLK.Common;

public class VM_Attorneys :VM_Base
{
    [Field(
        Title = "en:Text|vi:Tiêu đề banner",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleBanner { get; set; }

    [Field(
       Title = "en:Background banner|vi:Hình nền banner",
       Required = false,
       Control = InputControlType.Image)]
    public string BackgroundBanner { get; set; }

    [Field(
   Title = "en:TitlePracticeAreas|vi:Tiêu đề thực hành",
   Required = false,
   Control = InputControlType.TextBox)]
    public string TitlePracticeAreas { get; set; }

    [Field(Title = "en:Practice areas|vi:Các thực hành")]
    public PracticeAreas[] PracticeAreasItems { get; set; }

    [Field(
         Title = "en:Title button appintment|vi:Tiêu đề nút nhấn lịch hẹn",
         Required = false,
         Control = InputControlType.TextBox)]
    public string TitleButtonAppintment { get; set; }

    [Field(
         Title = "en:Link button appintment|vi:Liên kết nút nhấn lịch hẹn",
         Required = false,
         Control = InputControlType.Link)]
    public string ButtonAppintment { get; set; }

    [Field(
           Title = "en:Title content|vi:Tiêu đề nội dung",
           Required = false,
           Control = InputControlType.TextBox)]
    public string TitleContent { get; set; }

    [Field(
       Title = "en:Sub title content|vi:Tiêu đề phụ nội dung",
       Required = false,
       Control = InputControlType.TextBox)]
    public string SubTitleContent { get; set; }

    [Field(
           Title = "en:Content|vi:Nội dung",
           Required = false,
           Control = InputControlType.RichTextBox)]
    public string Content { get; set; }


    [Field(
           Title = "en:Title skill|vi:Tiêu đề kĩ năng",
           Required = false,
           Control = InputControlType.TextBox)]
    public string TitleSkill { get; set; }

    [Field(
       Title = "en:Sub title skill|vi:Tiêu đề phụ kĩ năng",
       Required = false,
       Control = InputControlType.TextBox)]
    public string SubTitleSkill { get; set; }

    [Field(Title = "en:Skills|vi:Các kĩ năng")]
    public Skill[] SkillItems { get; set; }

    [Field(
           Title = "en:Title education|vi:Tiêu đề giáo dục",
           Required = false,
           Control = InputControlType.TextBox)]
    public string TitleEducation { get; set; }

    [Field(
       Title = "en:Sub title education|vi:Tiêu đề phụ giáo dục",
       Required = false,
       Control = InputControlType.TextBox)]
    public string SubTitleEducation { get; set; }

    [Field(Title = "en:Educations|vi:Các giáo dục")]
    public Education[] EducationItems { get; set; }

    [Field(
        Title = "en:Title comment|vi:Tiêu đề bình luận",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleComment { get; set; }

    [Field(
       Title = "en:Sub title comment|vi:Tiêu đề phụ bình luận",
       Required = false,
       Control = InputControlType.TextBox)]
    public string SubTitleComment { get; set; }

    [Field(
        Title = "en:Title|vi:Tiêu đề luật sư",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleAttorneys { get; set; }

    [Field(
        Title = "en:Sub title|vi:Tiêu đề phụ luật sư",
        Required = false,
        Control = InputControlType.TextBox)]
    public string SubTitleAttorneys { get; set; }

    [Field(
        Title = "en:Position|vi:Chức vụ",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Position { get; set; }

    [Field(
       Title = "Link facebook",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkFacebook { get; set; }

    [Field(
      Title = "Link twitter",
      Required = false,
      Control = InputControlType.TextBox)]
    public string LinkTwitter { get; set; }

    [Field(
       Title = "Link linkedin",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkLinkedin { get; set; }

    [Field(
       Title = "Link google",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkGoogle { get; set; }

    [Field(
           Title = "en:Phone|vi:Số điện thoại",
           Required = false,
           Control = InputControlType.TextBox)]
    public string Phone { get; set; }

    [Field(
       Title = "Email",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Email { get; set; }

    public class Skill
    {
        [Field(
            Title = "en:Title|vi:Tiêu đề",
            Required = false,
            Control = InputControlType.TextBox)]
        public string Title { get; set; }

        [Field(
            Title = "en:Percent|vi:Phần trăm",
            Required = false,
            Control = InputControlType.Number)]
        public int Percent { get; set; }
    }



    public class PracticeAreas
    {
        [Field(
         Title = "en:Title|vi:Tiêu đề",
         Required = false,
         Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }



    public class Education
    {
        [Field(
            Title = "en:Title|vi:Tiêu đề",
            Required = false,
            Control = InputControlType.TextBox)]
        public string Title { get; set; }

        [Field(
            Title = "en:Description|vi:Mô tả",
            Required = false,
            Control = InputControlType.TextArea)]
        public string Description { get; set; }

        [Field(
            Title = "en:Year|vi:Năm",
            Required = false,
            Control = InputControlType.Number)]
        public int Year { get; set; }
    }

    

    
}




