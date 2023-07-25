using System;
using System.ComponentModel.DataAnnotations;

namespace CSClub.Data;

public class LogModel
{
    public int Id { get; set; }
    public string Entry { get; set; } = "";
    [DataType(DataType.Date)]
    public string Date { get; set; } = "";
    public string Attendance { get; set; } = "";
}

