using System;
using CSClub.Data;

namespace CSClub.Classes;

public class MemberStack : MemberADT
{
	// Methods
	public MemberStack()
	{
		throw new InvalidOperationException("The stack must have a preset size!");
	}

	public void Push(ClubMember member)
	{
		if (IsFull()) return;

		arr[length] = member;
		length++;
	}

    public ClubMember? Pop()
    {
		if (IsEmpty()) return null;

		length--;
        return arr[length];
    }

	public ClubMember? Peek()
	{
		if (IsEmpty()) return null;
		return arr[length - 1];
	}
}

