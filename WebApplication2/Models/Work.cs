using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
  public enum Genre
  {
    fanasy, horror, romantic, history
  }
  public class Work
  {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    [Range(1,100)]
    public int Rating { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    public List<Сomment>? Comments { get; set; }
   }
}
