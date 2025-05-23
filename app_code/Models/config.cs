using SLK.Common;
using System.Reflection;
using static FormSection;

public class Config
{
    public Config()
    {
        ComplainItems = new ComplainItem[]
        {
            new ComplainItem {
                Name ="Mr.Sang",
                Phone ="0352889129"
            },
            new ComplainItem {
                Name ="Mr.Pờm",
                Phone ="0372461306"
            },
        };
        this.WebTitle = "VIET GROUP";        
        this.Favicon = "/assets/images/pre-logo.png";
        this.GoogleGtag = "1234";
        this.LinkFacebook = "https://facebook.com";
        this.LinkInstagram = "https://www.instagram.com";
        this.Company = new CompanyInfo
        {
                          
        };
       
    }

    public string email { get; set; }

    public string pass { get; set; }



    [Field(
        Title = "Tiêu đề website",
        Required = false,
        Control = InputControlType.TextBox)]
    public string WebTitle { get; set; }

    [Field(
        Title = "Tên website",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Description { get; set; }

    [Field(
        Title = "Favicon",
        Required = false,
        Control = InputControlType.Image)]
    public string Favicon { get; set; }

	[Field(
	Title = "Logo",
	Required = false,
	Control = InputControlType.Image)]
	public string Logo { get; set; }
	[Field(
        Title = "Google Gtag",
        Required = false,
        Control = InputControlType.TextBox)]
    public string GoogleGtag { get; set; }

    [Field(
       Title = "Link facebook",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkFacebook { get; set; }
    [Field(
       Title = "Link Instagram",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkInstagram { get; set; }
    
    [Field(
       Title = "Link twitter",
       Required = false,
       Control = InputControlType.TextBox)]
    public string LinkTwitter { get; set; }
    [Field(
   Title = "Link KakaoTalk",
   Required = false,
   Control = InputControlType.TextBox)]
    public string LinkKakaoTalk { get; set; }


    [Field(
     Title = "Link Zalo",
     Required = false,
     Control = InputControlType.TextBox)]
    public string Zalo { get; set; }


	[Field(
    Title = "Link Map",
    Required = false,
    Control = InputControlType.TextBox)]
    public string LinkMap { get; set; }

	[Field(
	   Title = "Baner",
	   Required = false,
	   Control = InputControlType.Image)]
	public string BanerPost { get; set; }
	[Field(
	   Title = "Ảnh chia sẽ",
	   Required = false,
	   Control = InputControlType.Image)]
	public string BanerNew { get; set; }

	[Field(
	   Title = "Giá lọc",
	   Required = false,
	   Control = InputControlType.Number)]
	public string Price { get; set; }

	[Field(Title = "Danh sách menu", ChildTitle = "Menu chính")]
    public MenuItem[] MainMenus { get; set; }

    [Field(Title = "Thông tin công ty")]
    public CompanyInfo Company { get; set; }

    public ComplainItem[] ComplainItems { get; set; }
	[Field(Title = "Các liên kết")]
	public Link Link { get; set; }
}
public class Link
{
	[Field(
		Title = "Trang đặt hàng",
		Required = false,
		Control = InputControlType.TextBox)]
	public string PageMakeOrder { get; set; }

	[Field(
	  Title = "Trang giỏ hàng",
	  Required = false,
	  Control = InputControlType.TextBox)]
	public string PageCart { get; set; }

	[Field(
	 Title = "Trang thông tin đơn hàng",
	 Required = false,
	 Control = InputControlType.TextBox)]
	public string PageInfoOrder { get; set; }

	[Field(
		Title = "Trang các nhãn hiệu",
		Required = false,
		Control = InputControlType.TextBox)]
	public string PageAllBrands { get; set; }

	[Field(
	   Title = "Trang liên hệ",
	   Required = false,
	   Control = InputControlType.TextBox)]
	public string PageContact { get; set; }

	[Field(
   Title = "Trang blog",
   Required = false,
   Control = InputControlType.Link)]
	public string PageBlog { get; set; }

	[Field(
   Title = "Trang Shop",
   Required = false,
   Control = InputControlType.Link)]
	public string PageShop { get; set; }


	[Field(
   Title = "Tìm kiếm",
   Required = false,
   Control = InputControlType.Link)]
	public string Seach { get; set; }
}
public class MenuItem
{
    [Field(
       Title = "en:Title|vi:Tiêu đề",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
       Title = "en:Link|vi:Đường dẫn",
       Required = false,
       Control = InputControlType.Link)]
    public string Href { get; set; }

	[Field(Title = "Các menu con", ChildTitle = "Menu con")]
	public SubMenu[] SubMenus { get; set; }
}

public class SubMenu
{
    [Field(
       Title = "en:Title|vi:Tiêu đề",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
       Title = "en:Link|vi:Đường dẫn",
       Required = false,
       Control = InputControlType.Link)]
    public string Href { get; set; }
	[Field(Title = "Các menu con", ChildTitle = "Menu con")]
	public SubMenu2[] SubMenus { get; set; }
}

public class SubMenu2
{
	[Field(
	   Title = "en:Title|vi:Tiêu đề",
	   Required = false,
	   Control = InputControlType.TextBox)]
	public string Title { get; set; }

	[Field(
	   Title = "en:Link|vi:Đường dẫn",
	   Required = false,
	   Control = InputControlType.Link)]
	public string Href { get; set; }

}
public class ComplainItem
{
    [Field(
       Title = "en:Name|vi:Tên",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Name { get; set; }
    [Field(
       Title = "en:Phone|vi:Số điện thoại",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Phone { get; set; }
}

public class FormSection
{
    [Field(
         Title = "en:Title form |vi:Tiêu đề form",
         Required = false,
         Control = InputControlType.TextBox)]
    public string TitleForm { get; set; }

    [Field(
       Title = "en:Describe form |vi:Mô tả form",
       Required = false,
       Control = InputControlType.TextBox)]
    public string DescribeForm { get; set; }

    [Field(
          Title = "en:Title section Nation |vi:Tiêu đề mục chọn quốc gia",
          Required = false,
          Control = InputControlType.TextBox)]
    public string TitleNation { get; set; }

    [Field(
         Title = "en:Title section time |vi:Tiêu đề mục chọn Thời gian",
         Required = false,
         Control = InputControlType.TextBox)]
    public string TitleTime { get; set; }

    [Field(
         Title = "en:Title section Office Address |vi:Tiêu đề mục chọn văn phòng gần nhất",
         Required = false,
         Control = InputControlType.TextBox)]
    public string TitleOfficeAddress { get; set; }

    [Field(
        Title = "en:Title section Consultation Form |vi:Tiêu đề mục chọn hình thức tư vấn",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleConsultationForm { get; set; }

    [Field(
        Title = "en:Title section Level Of Interest |vi:Tiêu đề mục chọn bậc học",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleLevelOfInterest { get; set; }

    [Field(
        Title = "en:Title button|vi:Tiêu đề nút",
        Required = false,
        Control = InputControlType.TextBox)]
    public string TitleButton { get; set; }
    public class Nation
    {
        [Field(
           Title = "en:Title |vi:Tiêu đề",
           Required = false,
           Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }
    [Field(Title = "en:Nation|vi:Quốc gia")]
    public Nation[] NationItems { get; set; }

    public class StartTime
    {
        [Field(
           Title = "en:Title |vi:Tiêu đề",
           Required = false,
           Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }
    [Field(Title = "en:Start time|vi:Thời gian bắt đầu")]
    public StartTime[] StartTimeItems { get; set; }

    public class OfficeAddress
    {
        [Field(
           Title = "en:Title |vi:Tiêu đề",
           Required = false,
           Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }
    [Field(Title = "en:Office address|vi:Địa chỉ văn phòng")]
    public OfficeAddress[] OfficeAddressItems { get; set; }

    public class ConsultationForm
    {
        [Field(
           Title = "en:Title |vi:Tiêu đề",
           Required = false,
           Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }
    [Field(Title = "en:Consultation form|vi:Hình thức tư vấn")]
    public ConsultationForm[] ConsultationFormItems { get; set; }

    public class LevelOfInterest
    {
        [Field(
           Title = "en:Title |vi:Tiêu đề",
           Required = false,
           Control = InputControlType.TextBox)]
        public string Title { get; set; }
    }
    [Field(Title = "en:Level of interest|vi:Bậc học quan tâm")]
    public LevelOfInterest[] LevelOfInterestItems { get; set; }
}

public class CompanyInfo
{    
    [Field(
        Title = "en:Slogan|vi:Slogan",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Slogan { get; set; }

    [Field(
        Title = "en:Company name|vi:Tên công ty",
        Required = false,
        Control = InputControlType.TextBox)]
    public string CompanyName { get; set; }   

    [Field(
        Title = "en:Số Fax|vi:Số Fax",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Fax { get; set; }
    [Field(
        Title = "en:Bank number|vi:Số tài khoản",
        Required = false,
        Control = InputControlType.TextBox)]
    public string BankNumber { get; set; }
    [Field(
       Title = "en:Bank name|vi:Tên ngân hàng",
       Required = false,
       Control = InputControlType.TextBox)]
    public string BankName { get; set; }

    [Field(
        Title = "Hotline",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Hotline { get; set; }

    [Field(
        Title = "Số điện thoại",
        Required = false,
        Control = InputControlType.TextBox)]
    public string ContactPhone { get; set; }

    [Field(
      Title = "Website",
      Required = false,
      Control = InputControlType.TextBox)]
    public string Website { get; set; }

    [Field(
      Title = "Email",
      Required = false,
      Control = InputControlType.TextBox)]
    public string Email { get; set; }

    [Field(
        Title = "Copyright Text",
        Required = false,
        Control = InputControlType.TextBox)]
    public string CopyrightText { get; set; }

    [Field(
        Title = "Giờ làm việc",
        Required = false,
        Control = InputControlType.TextBox)]
    public string WorkingHours { get; set; }

    [Field(
        Title = "en:Địa chỉ|vi:Địa chỉ",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Address { get; set; }


	[Field(
        Title = "en:Tên công ty bảo vệ bản quyền|vi:Tên công ty bảo vệ bản quyền",
        Required = false,
        Control = InputControlType.TextBox)]
    public string CompanyNameGuardCopyRight { get; set; }

    [Field(
        Title = "en:Link công ty bảo vệ bản quyền|vi:Link công ty bảo vệ bản quyền",
        Required = false,
        Control = InputControlType.Link)]
    public string LinkCompanyGuardCopyRight { get; set; }

    [Field(
        Title = "en:Bản đồ|vi:Bản đồ",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Map { get; set; }         
}

public class Links
{
    [Field(
       Title = "en:Tiêu đề|vi:Tiêu đề",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
       Title = "en:Link|vi:Link",
       Required = false,
       Control = InputControlType.TextBox)]
    public string Link { get; set; }
}

