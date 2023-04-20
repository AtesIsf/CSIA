using System;

using CSClub.Data;

namespace CSClub.ADT;

public class MemberBinSearchTree
{
	// Props
	public MemberNode? Head { get; set; }

	// Ctors
	public MemberBinSearchTree(MemberNode node)
	{
		Head = node;
	}

	public MemberBinSearchTree(ClubMember member)
	{
		Head = new MemberNode(member);
	}

    public MemberBinSearchTree(ClubMember[] arr)
    {
        foreach (var member in arr)
            Add(member);
    }

	// Methods
	public void Add(ClubMember member)
	{
		var toBeAdded = new MemberNode(member);
		var currNode = Head;

        if (Head == null)
        {
            Head = toBeAdded;
            return;
        }

		// Two distinct ID's will never be equal
		while(true)
		{
			// Left Path
			if (toBeAdded.Val.Id < currNode.Val.Id)
			{
				if (currNode.Left == null)
				{
					currNode.Left = toBeAdded;
					break;
				}
				currNode = currNode.Left;
			}

			// Right Path
			else if (toBeAdded.Val.Id > currNode.Val.Id)
            {
                if (currNode.Right == null)
                {
                    currNode.Right = toBeAdded;
                    break;
                }
                currNode = currNode.Right;
            }
        }
	}

	public ClubMember? Search(int id)
	{
		return Search(id, Head);
	}

	// Private Methods
	private ClubMember? Search(int id, MemberNode? currNode)
	{
		if (currNode == null)
			return null;

		else if (id == currNode.Val.Id)
			return currNode.Val;

		else if (id < currNode.Val.Id)
			return Search(id, currNode.Left);

		else if (id > currNode.Val.Id)
			return Search(id, currNode.Right);

		// Is never reached
		return null;
	}
}

public class MemberNode
{
	// Props
	public ClubMember Val { get; set; }
	public MemberNode? Left { get; set; }
	public MemberNode? Right { get; set; }

	// Ctors
	public MemberNode(ClubMember member)
	{
		Val = member;
	}
}