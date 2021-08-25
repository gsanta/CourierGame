using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
