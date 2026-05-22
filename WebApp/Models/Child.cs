namespace WebApp.Models;

public class Child<Key> where Key : struct
{
    public Key ParentId { get; set; }
}