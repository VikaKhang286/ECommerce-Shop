namespace WebApp.Models;

public class Parent<Key> where Key : struct
{
    public Key Id { get; set; }
    public IEnumerable<Child<Key>>? Children { get; set; }
}