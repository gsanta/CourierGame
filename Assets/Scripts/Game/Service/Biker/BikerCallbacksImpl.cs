using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Service
{
    public class BikerCallbacksImpl : BikerCallbacks
    {
        private readonly RoleService roleService;

        public BikerCallbacksImpl(RoleService roleService)
        {
            this.roleService = roleService;
        }

        public void OnCurrentRoleChanged(Biker biker)
        {
            roleService.EmitCurrentRoleChanged();
        }
    }
}
