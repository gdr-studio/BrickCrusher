using System.Collections;
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
        private static readonly int TilingOffsetVector = Shader.PropertyToID("_BaseMap_ST");
        private Coroutine _colorChanging;
        
        public void SetColor(Color endColor)
        {
            currentColor = endColor;
            UpdateColor();
        }
        
        public void SetColor(Color startColor, Color endColor, float duration)
        {
            UpdateColor();
            if(_colorChanging != null)
                StopCoroutine(_colorChanging);
            _colorChanging = StartCoroutine(ChangingColor(startColor, endColor, duration));
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

        private IEnumerator ChangingColor(Color startColor, Color endColor, float time)
        {
            var elapsed = 0f;
            // var start = currentColor;
            while (elapsed < time)
            {
                var c = Color.Lerp(startColor, endColor, elapsed / time);
                elapsed += Time.deltaTime;
                currentColor = c;
                UpdateColor();
                yield return null;
            }
        }
    }
}