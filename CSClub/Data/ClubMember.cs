using System;

namespace CSClub.Data;

public class ClubMember
{
    // Props
    public int Id { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }
    public int MeetingsAttended { get; set; } = -1;
    public bool IsCoPresident { get; set; } = false;

    // Ctors
    public ClubMember(int id, string name, string grade, int meetingsAttended, bool isCoPresident)
    {
        Id = id;
        Name = name;
        Grade = grade;
        MeetingsAttended = meetingsAttended;
        IsCoPresident = isCoPresident;
    }

    public ClubMember(string name, string grade, bool isCoPresident)
    {
        Name = name;
        Grade = grade;
        IsCoPresident = isCoPresident;
    }

    public ClubMember(string name, string grade)
    {
        Name = name;
        Grade = grade;
    }
}
