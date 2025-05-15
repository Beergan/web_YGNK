using SLK.Common;

public class VM_About : VM_Base
{
    [Field(
        Title = "en:Nội dung|vi:Nội dung",
        Required = false,
        Control = InputControlType.RichTextBox)]
    public string Content { get; set; }    
}