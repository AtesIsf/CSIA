
using System.Collections;
using CSClub.Data;

namespace CSClub.ADT;

// TODO: Make this inherit from MemberADT
public class MemberLL : ILinearMemberADT
{
    // Instance Vars
    private int length;
    private MemberNode? head;

    // Props
    public int Len { get=>length; }   
    public MemberNode? Head { get=>head; }  
    public ClubMember this[int index]
    {
        get 
        {  
            if (index >= length)
                throw new IndexOutOfRangeException();
            
            var currNode = head;
            for (var i = 0; i<index; i++)
                currNode = currNode.Next;
            return currNode.Val;
        }
    }

    // Ctors
    public MemberLL()
    {
        length = 0;
        head = null;
    }

    public MemberLL(ClubMember member)
    {
        head = new MemberNode(member);
    }

    // Methods
    public void Add(ClubMember member)
    {
        if (length == 0)
        {
            head = new MemberNode(member);
            length++;
            return;
        }

        var currNode = head;
        while (currNode.Next != null)
            currNode = currNode.Next;

        currNode.Next = new MemberNode(member);
        length++;
    }

    public bool Includes(string name)
    {
        if (length == 0)
            return false;

        var currNode = head;
        while (true)
        {
            if (currNode.Val.Name == name)
                return true;

            if (currNode.Next == null)
                return false;
            currNode = currNode.Next;
        }
    }

    public bool Includes(int id)
    {
        if (length == 0)
            return false;

        var currNode = head;
        while (true)
        {
            if (currNode.Val.Id == id)
                return true;
                
            if (currNode.Next == null)
                return false;
            currNode = currNode.Next;
        }
    }

    public ClubMember? Remove(int id)
    {
        if (length == 0)
            return null;

        MemberNode? back = null;
        var front = head;
        
        if (head.Val.Id == id)
        {
            var temp = new ClubMember(
                head.Val.Id, head.Val.Name, 
                head.Val.Grade, head.Val.MeetingsAttended, 
                head.Val.IsCoPresident
            );
            head = head.Next;
            length--;
            return temp;
        }

        while(true)
        {
            if (front.Val.Id == id)
            {
                back.Next = front.Next;
                front.Next = null;
                length--;
                return front.Val;
            }
            
            if(back.Next == null)
                return null;
            front = front.Next;
            back = back.Next;
        }
    }

    // IEnumerator
    public IEnumerator<ClubMember> GetEnumerator()
    {
        var currNode = head;
        while (currNode != null)
        {
            yield return currNode.Val;
            currNode = currNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}