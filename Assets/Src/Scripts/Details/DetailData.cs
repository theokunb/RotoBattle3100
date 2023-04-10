using System;

[Serializable]
public class DetailData1
{
    public long Id { get; private set; }

    public DetailData1(long id)
    {
        Id = id;
    }
}
