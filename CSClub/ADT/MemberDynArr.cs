using System;
using System.Collections;
using System.Collections.Generic;
using CSClub.Data;

namespace CSClub.ADT;

public class MemberDynArr : ILinearMemberADT
{
    // Instance Vars
    private int length;
    private int size;
    private ClubMember[] arr;

    // Props
    public int Size { get => size; }
    public int Len { get => length; }
    public ClubMember this[int index] { get => arr[index]; }

    // Ctors
    public MemberDynArr()
    {
        length = 0;
        size = 4;
        arr = new ClubMember[size];
    }

    public MemberDynArr(int presetSize)
    {
        if (presetSize <= 0) throw new ArgumentOutOfRangeException("The preset size must be greater than 0!");

        length = 0;
        size = presetSize;
        arr = new ClubMember[size];
    }

    public MemberDynArr(ClubMember[] members)
    {
        arr = members;
        size = members.Length;
        length = members.Length;
    }

	// Public Methods
    public void Add(ClubMember member)
	{
		if (IsFull()) Resize();
		arr[length] = member;
        length++;
	}

	public ClubMember? Remove(string name)
	{
		if (!Includes(name)) return null;

		ClubMember temp;
		var tempArr = new ClubMember[length - 1];

		int counter = 0;
		for (int i = 0; i< length; i++)
		{
			if (arr[i].Name == name)
			{
				temp = arr[i];
				arr = tempArr;
                length--;
                return temp;
            }
			tempArr[counter] = arr[i];
			counter++;
		}
		// This never gets reached
		return null;
	}

    public ClubMember? Remove(int id)
    {
        if (!Includes(id)) return null;

        ClubMember temp;
        var tempArr = new ClubMember[length - 1];

        int counter = 0;
        for (int i = 0; i < length; i++)
        {
            if (arr[i].Id == id)
            {
                temp = arr[i];
                arr = tempArr;
                length--;
                return temp;
            }
            tempArr[counter] = arr[i];
            counter++;
        }
        // This never gets reached
        return null;
    }

    public void Compress()
    {
        if (IsFull()) return;

        var temp = new ClubMember[length];
        for (int i = 0; i < length; i++)
            temp[i] = arr[i];
        arr = temp;
        size = length;
    }

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
    // Private Methods
    // Private Methods
    private void Resize()
	{
        var temp = new ClubMember[size*2];
        for (int i = 0; i < length; i++)
            temp[i] = arr[i];
        arr = temp;
		size *= 2;
    }
    
    private int GradeToNumber(string grade)
    {
        if (grade == "Prep") return 1;
        return Convert.ToInt32(grade);
    }

    // Bubblesort to Sort by Name
    private void SortByName(string? toggle)
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
    private void SortByGrade(string? toggle)
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
    private void SortByMeetings(string? toggle)
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

    // IEnumerators
    public IEnumerator<ClubMember> GetEnumerator()
    {
        for (var i = 0; i < length; i++)
            yield return arr[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return arr.GetEnumerator();
    }
}

