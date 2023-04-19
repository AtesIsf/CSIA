using System;
using System.Collections;
using CSClub.Data;

namespace CSClub.Classes;

public class MemberADT : IEnumerable<ClubMember>
{
    // Instance vars
    protected int length;
    protected int size;
    protected ClubMember[] arr;

    // Props
    public int Len { get => length; }
    public int Size { get => size; }
    public ClubMember this[int i] { get => arr[i]; }

    // Ctors
    public MemberADT()
    {
        length = 0;
        size = 4;
        arr = new ClubMember[size];
    }

    public MemberADT(int presetSize)
    {
        if (presetSize <= 0) throw new ArgumentOutOfRangeException("The preset size must be greater than 0!");

        length = 0;
        size = presetSize;
        arr = new ClubMember[size];
    }

    // Methods
    public bool Includes(string name)
    {
        for (int i = 0; i < length; i++)
            if (arr[i].Name == name)
                return true;
        return false;
    }

    public bool Includes(int id)
    {
        for (int i = 0; i < length; i++)
            if (arr[i].Id == id)
                return true;
        return false;
    }

    public bool IsFull()
    {
        return size == length;
    }

    public bool IsEmpty()
    {
        return length == 0;
    }

    public void Reverse()
    {
        var temp = new ClubMember[size];
        for (int i = 0; i < length; i++)
            temp[i] = arr[length - 1 - i];
        arr = temp;
    }

    public void Sync(ApplicationDbContext context)
    {
        arr = context.Members.ToArray();
        length = arr.Length;
        size = arr.Length;
    }

    public void Sort(string sortBy, string toggle)
    {
        if (sortBy == "name")
            SortByName(toggle);
        else if (sortBy == "grade")
            SortByGrade(toggle);
        else if (sortBy == "meetingsAttended")
            SortByMeetings(toggle);
    }

    // Protected Methods
    protected int GradeToNumber(string grade)
    {
        if (grade == "Prep") return 1;
        return Convert.ToInt32(grade);
    }

    // Bubblesort to Sort by Name
    protected void SortByName(string? toggle)
    {
        for (int i = 0; i < length - 1; i++)
            for (int j = 0; j < length - i - 1; j++)
                if (arr[j].Name.ToLower()[0] > arr[j + 1].Name.ToLower()[0])
                {
                    var temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }

        if (!String.IsNullOrEmpty(toggle))
            if (toggle == "ascending")
                Reverse();
    }

    // Bubblesort to Sort by Grade
    protected void SortByGrade(string? toggle)
    {
        for (int i = 0; i < length - 1; i++)
            for (int j = 0; j < length - i - 1; j++)
                if (GradeToNumber(arr[j].Grade) < GradeToNumber(arr[j + 1].Grade))
                {
                    var temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }

        if (!String.IsNullOrEmpty(toggle))
            if (toggle == "ascending")
                Reverse();
    }

    // Selection Sort to Sort by Meetings
    protected void SortByMeetings(string? toggle)
    {
        int min;
        for (int i = 0; i < length - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < length; j++)
                if (arr[j].MeetingsAttended > arr[min].MeetingsAttended)
                    min = j;

            if (min == i) continue;
            var temp = arr[min];
            arr[min] = arr[i];
            arr[i] = temp;
        }

        if (!String.IsNullOrEmpty(toggle))
            if (toggle == "ascending")
                Reverse();
    }

    // Enumerator
    IEnumerator<ClubMember> IEnumerable<ClubMember>.GetEnumerator()
    {
        for (int i = 0; i < length; i++)
            yield return arr[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return arr.GetEnumerator();
    }
}

