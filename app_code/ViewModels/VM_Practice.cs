using SLK.Common;

public class VM_Practice : VM_Base
{
    [Field(
        Title = "en:Content|vi:Nội dung",
        Required = false,
        Control = InputControlType.RichTextBox)]
    public string Content { get; set; }
}