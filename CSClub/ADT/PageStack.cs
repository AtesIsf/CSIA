using System;
using CSClub.Data;

namespace CSClub.ADT;

/*
 * This ADT aims to store the previous pages a user visited to
 * create a "Go Back" functionality
 */

public class PageStack
{
    // Instance Vars
    private int length;
    private PageNode? head;
    private const int MAX_LEN = 20;

    // Props
    public int Len { get => length; }
    public string this[int index]
    {
        get
        {
            if (index >= length)
                throw new IndexOutOfRangeException();

            if (head == null)
                return string.Empty;
            
            var currNode = head;
            for (var i = 0; i < index; i++)
                currNode = currNode.Next;
            return currNode.Val;
        }
    }

    // ctors
    public PageStack()
    {
        length = 0;
    }

    // Methods
    public void Push(string page)
    {
        if (length == 0)
        {
            head = new PageNode(page);
            length++;
        }

        var ptr = head;
        while (ptr.Next != null)
            ptr = ptr.Next;
        ptr.Next = new PageNode(page);
        length++;

        if (length > MAX_LEN)
        {
            head = head.Next;
            length--;
        }
    }

    public string Pop()
    {
        if (length == 0) return string.Empty;

        string str;
        if (length == 1)
        {
            str = head.Val;
            head = null;
            length--;
            return str;
        }
        else
        {
            var ptr = head;
            for (int i = 0; i < length - 2; i++)
            {
                ptr = ptr.Next;
            }
            str = ptr.Next.Val;
            ptr.Next = null;
            length--;
            return str;
        }
    }

    public string Peek()
    {
        if (length == 0) return string.Empty;
        return this[length - 1];
    }
}

public class PageNode
{
    // Props
    public string Val { get; set; }
    public PageNode? Next { get; set; }

    // Ctors
    public PageNode(string val)
    {
        Val = val;
    }

    public PageNode(string val, PageNode next)
    {
        Val = val;
        Next = next;
    }
}