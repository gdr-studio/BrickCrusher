using UnityEngine;

namespace ImageToVolume
{
    public class ColorSetter : MonoBehaviour
    {
        public Color currentColor;
        public Renderer renderer;
        public string colorKey;
        public Vector2 offset;
        public Vector2 tiling;
        private static readonly int TilingOffsetVector = Shader.PropertyToID("_MainTex_ST");

        public void SetColor(Color color)
        {
            currentColor = color;
            UpdateColor();
        }
        
        public void SetMaterial(Material mat, Vector2 tiling, Vector2 offset)
        {
            renderer.material = mat;
            this.offset = offset;
            this.tiling = tiling;
            UpdateTilingAndOffset();
        }

        public void UpdateTilingAndOffset()
        {
            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            block.SetVector(TilingOffsetVector, new Vector4(tiling.x, tiling.y, offset.x, offset.y));
            renderer.SetPropertyBlock(block);
        }


        public void UpdateColor()
        {
            var block = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(block);
            block.SetColor(colorKey, currentColor);
            renderer.SetPropertyBlock(block);
        }
    }
}