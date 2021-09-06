using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Domain
{
    public class MinimapBiker : MonoBehaviour
    {
        private Biker biker;
        
        public Biker Biker { set => biker = value; }

        public void Start()
        {
            UpdateState();
            biker.CurrentRoleChanged += HandleCurrentRoleChanged;
        }

        private void Update()
        {
            transform.position = new Vector3(biker.transform.position.x, 5, biker.transform.position.z);
            transform.rotation = biker.transform.rotation;
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (CurrentRoleChecker.IsFollowOrPlay(biker.GetCurrentRole()))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }
}
