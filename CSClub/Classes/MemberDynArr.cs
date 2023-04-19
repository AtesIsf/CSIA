using System;
using System.Collections;
using System.Collections.Generic;
using CSClub.Data;

namespace CSClub.Classes;

public class MemberDynArr : MemberADT
{
    // Private Methods

    private void Resize()
	{
        var temp = new ClubMember[size+4];
        for (int i = 0; i < length; i++)
            temp[i] = arr[i];
        arr = temp;
		size += 4;
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
}

