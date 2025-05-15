using SLK.Common;

public class VM_Info
{
    [Field(
        Title = "en:Title|vi:Tiêu đề",
        Required = false,
        Control = InputControlType.TextBox)]
    public string Title { get; set; }

    [Field(
       Title = "en:Number|vi:Số lượng",
       Required = false,
       Control = InputControlType.Number)]
    public int Number { get; set; }
}