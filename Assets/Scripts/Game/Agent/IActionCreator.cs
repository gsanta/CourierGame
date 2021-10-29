using AI;
using System;
using System.Collections.Generic;

namespace Agents
{
    public interface IActionCreator<T> where T : IGameObject
    {
        List<GoapAction<T>> GetActions();
    }
}
