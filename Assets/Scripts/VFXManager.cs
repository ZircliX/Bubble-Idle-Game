using System.Linq;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.VFX;

namespace BubbleIdle
{
    public class VFXManager : MonoSingleton<VFXManager>
    {
        [SerializeField] private VFXAsset[] vfxAssets;

        public void PlayVFX(string vfxName, Vector3 position)
        {
            VisualEffect effect = vfxAssets
                .Select(ctx => ctx)
                .FirstOrDefault(ctx => ctx.vfxName == vfxName).vfx;

            VisualEffect instantiatedVFX = Instantiate(effect, position, Quaternion.identity);
            instantiatedVFX.Play();
            
            Destroy(instantiatedVFX.gameObject, 2f);
        }
    }
    
    [System.Serializable]
    public struct VFXAsset
    {
        public string vfxName;
        public VisualEffect vfx;
    }
}