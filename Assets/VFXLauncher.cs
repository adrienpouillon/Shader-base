
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class VFXLauncher : MonoBehaviour
{
    [Serializable] class VFX
    {
        public string _name;
        public ParticleSystem _particle;
        public VFXIDSO _id;
    }

    [SerializeField] private List<VFX> _vFXlist;

    public void Launch(VFXIDSO id)
    {
        VFX vFX = _vFXlist.FirstOrDefault(i => i._id == id);
        if (vFX == null)
        {
            return;
        }
        vFX._particle.Play();
    }

    /*public void Launch(string name)
    {
        VFX vFX = _vFXlist.FirstOrDefault(i => i._name == name);
        if (vFX == null)
        {
            return;
        }
        vFX._particle.Play();
        /*foreach (VFX effect in _vFXlist)
        {
            if(effect._name == name)
            {
                effect._particle.Play();
            }
        }
    }*/
}
