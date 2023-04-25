using System;

[Serializable]
public class DetailData
{
    public long Id { get; private set; }

    public DetailData(long id)
    {
        Id = id;
    }
}
