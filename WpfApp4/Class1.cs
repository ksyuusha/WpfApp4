using System;

public class BlogPost
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Keywords { get; set; }

    public BlogPost(string title, DateTime date, string keywords)
    {
        Title = title;
        Date = date;
        Keywords = keywords;
    }
}
