

public enum CurrentRole
{
    NONE,
    FOLLOW,
    PLAY
}

public static class CurrentRoleChecker
{ 
    public static bool IsFollowOrPlay(CurrentRole role)
    {
        return role == CurrentRole.FOLLOW || role == CurrentRole.PLAY;
    }
} 
