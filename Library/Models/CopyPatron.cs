using System;

namespace Library.Models
{
  public class CopyPatron
  {
    public int CopyPatronId { get; set; }
    public int CopyId {get; set;} 
    public int PatronId {get; set;}

    public DateTime CopyCheckoutDate {get; set;}

    public Copy Copy {get; set;}
    public Patron Patron {get; set;}

    public string Date(DateTime inputDateTime) 
    { 
      string outputDateTime = inputDateTime.ToShortDateString();
      return outputDateTime;
    }
  }
}