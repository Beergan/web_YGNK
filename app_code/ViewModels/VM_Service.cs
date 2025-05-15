using SLK.Common;

public class VM_Service : VM_Base
{

    [Field(
       Title = "en:Banner|vi:Banner",
       Required = false,
       Control = InputControlType.Image)]
    public string Banner { get; set; }

    [Field(
       Title = "en:Content|vi:Nội dung",
       Required = false,
       Control = InputControlType.RichTextBox)]
    public string Content { get; set; }

}