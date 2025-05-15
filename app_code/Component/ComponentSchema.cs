using System.Collections.Generic;

public class ObjectSchema
{
    public string PropName { get; set; }

    public string Title { get; set; }

    public string ChildTitle { get; set; }

    public List<ComponentField> SingleFieldTypes { get; set; }

    public List<ComponentField> ArrayFieldTypes { get; set; }

    public List<ObjectSchema> SingleObjectTypes { get; set; }

    public List<ObjectSchema> ArrayObjectTypes { get; set; }

    public ObjectSchema()
    {
        SingleFieldTypes = new List<ComponentField>();
        ArrayFieldTypes = new List<ComponentField>();
        SingleObjectTypes = new List<ObjectSchema>();
        ArrayObjectTypes = new List<ObjectSchema>();
    }
}