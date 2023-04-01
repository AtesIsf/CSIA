using System;
using System.Collections;
using System.Collections.Generic;
using CSClub.Data;

namespace CSClub.Classes;

public class MemberDynArr : IEnumerable<ClubMember>
{
	// Instance Vars
	private int len;
	private int size;

	// Props
	public int Len { get => len; }
	public int Size { get => size; }
	public ClubMember[] arr { get; set; }

	// Ctors
	public MemberDynArr()
	{
		len = 0;
		size = 4;
		arr = new ClubMember[size];
	}

	public MemberDynArr(int presetSize)
	{
		if (presetSize <= 0) throw new ArgumentOutOfRangeException();

		len = 0;
		size = presetSize;
		arr = new ClubMember[size];
	}

	// Private Methods
	private bool isFull()
	{
		return size == len;
	}

	private void Resize()
	{
        var temp = new ClubMember[size+4];
        for (int i = 0; i < len; i++)
            temp[i] = arr[i];
        arr = temp;
		size += 4;
    }

	private int GradeToNumber(string grade)
	{
		if (grade == "Prep") return 1;
		return Convert.ToInt32(grade);
    }

	// Public Methods
    public void Add(ClubMember mem)
	{
		if (isFull()) Resize();
		arr[len] = mem;
		len++;
	}

	public ClubMember? Remove(string name)
	{
		if (!Includes(name)) return null;

		ClubMember temp;
		var tempArr = new ClubMember[len - 1];

		int counter = 0;
		for (int i = 0; i<len; i++)
		{
			if (arr[i].Name == name)
			{
				temp = arr[i];
				arr = tempArr;
				len--;
                return temp;
            }
			tempArr[counter] = arr[i];
			counter++;
		}
		// This never gets reached
		return null;
	}

    public bool Includes(string name)
    {
        for (int i = 0; i < len; i++)
            if (arr[i].Name == name)
                return true;
        return false;
    }

    public void Compress()
    {
        if (isFull()) return;

        var temp = new ClubMember[len];
        for (int i = 0; i < len; i++)
            temp[i] = arr[i];
        arr = temp;
        size = len;
    }

	// TODO: BOGOSORT

	// Bubblesort to Sort by Name
	public void SortByName(string? toggle)
	{
        for (int i = 0; i<len-1;i++)
			for (int j = 0; j<len-i-1; j++)
				if (arr[j].Name.ToLower()[0] > arr[j + 1].Name.ToLower()[0])
				{
					var temp = arr[j];
					arr[j] = arr[j + 1];
					arr[j + 1] = temp;
				}

        if (!String.IsNullOrEmpty(toggle))
            if (toggle == "a")
                Reverse();
    }

	// Bubblesort to Sort by Grade
    public void SortByGrade(string? toggle)
    {
        for (int i = 0; i < len - 1; i++)
            for (int j = 0; j < len - i - 1; j++)
                if (GradeToNumber(arr[j].Grade) < GradeToNumber(arr[j + 1].Grade))
                {
                    var temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }

		if (!String.IsNullOrEmpty(toggle))
			if(toggle == "a")
				Reverse();
    }

	// Selection Sort to Sort by Meetings
    public void SortByMeetings(string? toggle)
    {
		int min;
        for (int i = 0; i<len-1;i++)
		{
			min = i;
			for (int j = i + 1; j < len; j++)
				if (arr[j].MeetingsAttended > arr[min].MeetingsAttended)
					min = j;

			if (min == i) continue;
			var temp = arr[min];
			arr[min] = arr[i];
			arr[i] = temp;
		}

        if (!String.IsNullOrEmpty(toggle))
            if (toggle == "a")
                Reverse();
    }

	public void Reverse()
	{
		var temp = new ClubMember[size];
		for (int i = 0; i < len; i++)
            temp[i] = arr[len-1-i];
		arr = temp;
	}

    // Enumerator
    IEnumerator<ClubMember> IEnumerable<ClubMember>.GetEnumerator()
    {
		for (int i = 0; i < len; i++)
			yield return arr[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return arr.GetEnumerator();
    }
}

