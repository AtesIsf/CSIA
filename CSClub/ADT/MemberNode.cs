using CSClub.Data;

public class MemberNode
{
    // Props
    public ClubMember Val { get; set; }    
    public MemberNode? Next { get; set; }

    // Ctors
    public MemberNode(ClubMember member)
    {
        Val = member;
    }
}