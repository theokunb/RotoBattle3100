using System;

[Serializable]
public class DetailData
{
    public string Title { get; private set; }

    public DetailData(string title)
    {
        Title = title;
    }
}
