using System.Collections.Generic;

public class MyTranslator
{
    private IDictionary<string, string> _phrases = null;

    public string this[string en, string vi]
    {
        get
        {
            string langId = Root.GetRouteData<string>("LangId") ?? "vi";

            if (langId == "en")
                return en;

            if (langId == "vi")
                return vi;

            return _phrases?.ContainsKey(en) ?? false ? _phrases[en] : en;
        }
    }

    public MyTranslator(IDictionary<string, string> ph)
    {
        _phrases = ph;
    }
}

public class AdminTranslator
{
    private string _langId;

    public string this[string en, string vi] => _langId == "en" ? en : vi;

    public string this[string str] => _langId == "en" ? str.GetBefore("|").GetAfter(":") : str.GetAfter("|").GetAfter(":");

    public AdminTranslator(string langId)
    {
        _langId = langId;
    }
}