using System.ComponentModel.DataAnnotations;

public class DrivingModel
{
    [Required]
    public string PlateNumber {get; set;}
    
    [Required]
    public string Date {get; set;}

    [Required]
    public string Time {get; set;}
}