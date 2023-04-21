using System;
using System.Collections;
using CSClub.Data;

namespace CSClub.ADT;

public interface ILinearMemberADT : IEnumerable<ClubMember>
{
    // Props
    public int Len { get; }
    public ClubMember this[int index] { get; }
    
    // Methods
    abstract bool Includes(string name);
    abstract bool Includes(int id);
    abstract void Add(ClubMember member);
    abstract ClubMember? Remove(int id);

    // IEnumerable
    new abstract IEnumerator<ClubMember> GetEnumerator();
    abstract IEnumerator IEnumerable.GetEnumerator();
}

