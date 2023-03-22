using System;
using System.Collections.Generic;
using LittleTricks;
using Statues;
using UnityEngine;
using UnityEditor;

namespace ImageToVolume
{
    public class ImageVolumizer : MonoBehaviour
    {
        public Sprite sprite;
        public Material mat;
        public BlockStatue statue;
        [Space(10)]
        public VolumeElement mainPrefab;
        public ElementSub subDivPrefab;
        [Space(10)]
        public Vector3 localRot;
        public int blockSize;
        public float minAlpha;
        [Space(20)] 
        public bool doSubdivide;
        public int subDivideFactor = 2;
        public int normalSubDivFactor = 2;
        [Space(20)]
        public bool clampAlpha;
        public bool pivotAtBottom;
        public bool autoSetupNeighbours;
        [Space(20)]
        public List<GameObject> spawnedBlocks;

        public int GetSize()
        {
            var texture = sprite.texture;
            var color = texture.GetPixels32();
            var size = Mathf.Sqrt((float)color.Length);
            if (size - (int)size > 0)
            {
                throw new SystemException($"the size is not a perfect square");
            }
            Debug.Log($"Pixels array length: {color.Length}, root: {size}");
            return (int)size;
        }

        private void CheckStatue()
        {
            if (statue == null)
            {
                statue = FindObjectOfType<BlockStatue>();
                if (statue == null)
                {
                    throw new SystemException($"No Statue found on the scene");
                }
            }
        }

         public void CreateByTiling()
         { 
             CheckStatue();
            statue.Puzzle.Clear();
            Clear();
            statue.Puzzle = new List<VolumeElement>();
            var puzzle = statue.Puzzle;
            var texture = sprite.texture;
            var pixels = texture.GetPixels();
            var imageSize = Mathf.Sqrt((float)pixels.Length);
            if (imageSize - (int)imageSize > 0)
                throw new SystemException($"the size is not a perfect square");

            if (imageSize % blockSize != 0)
                throw new SystemException($"Size: {imageSize} is not divisible by blockSize: {blockSize}");
            
            var blocksCount = (int)imageSize / blockSize;
            Debug.Log($"Pixels: {pixels.Length}, blocksCount: {blocksCount}");
            float xPos = 0;
            float yPos = 0;
            var xSize = mainPrefab.sizeX;
            var ySize = mainPrefab.sizeY;
            var centerPos = GetCenter(blocksCount, xSize, ySize);
            var tiling = new Vector2(1f / blocksCount, 1f / blocksCount);
            for (var x = 0; x < blocksCount; x++)
            {
                for (var y = 0; y < blocksCount; y++)
                {
                    var uvOffset = new Vector2((float)x / blocksCount, (float)y / blocksCount);
                    var alphaCheck = CheckAlpha(uvOffset, blockSize, (int)imageSize, pixels);
                    if (alphaCheck == -1)
                    {
                        yPos += ySize; 
                        continue;
                    }
                    var element = SpawnElement();
                    element.X = x;
                    element.Y = y;
                    puzzle.Add(element);
                    element.colorSetter.SetMaterial(mat, tiling, uvOffset);
                    
                    element.transform.localPosition = centerPos + new Vector3(xPos, yPos, 0f);
                    element.transform.localEulerAngles = localRot;

                    SetSubDivData(element, uvOffset, (int)imageSize, pixels, blocksCount);
                    yPos += ySize;
                    if (alphaCheck == 0 && doSubdivide)
                    {
                        Subdivide(element, pixels, uvOffset,  (int)imageSize);
                    }
                    
                    var statueElement = (StatueElement)element;
                    if (statueElement != null)
                        statueElement.statue = statue;
                }
                xPos += xSize;
                yPos = 0;
            }
            statue.Puzzle = puzzle;
            if(autoSetupNeighbours)
                SetupNeighbours();
        }

         // 1 - fully colored, 0 - some alpha, -1 - all alpha
         private int CheckAlpha(Vector2 offset, int tileSize, int imageSizeX, Color[] colors)
         {
             bool hasAlpha = false;
             bool hasColor = false;
             var startX = (int)(imageSizeX * offset.x);
             var startY= (int)(imageSizeX * offset.y);
             
             for (int x = 0; x < tileSize; x++)
             {
                 for (int y = 0; y < tileSize; y++)
                 {
                     var index = (x + startX) + (y + startY) * imageSizeX;
                     var color = colors[index];
                     if (color.a < minAlpha)
                         hasAlpha = true;
                     else
                         hasColor = true;
                 }
             }

             if (hasAlpha && hasColor)
                 return 0;
             if (hasAlpha)
                 return -1;
             return 1;
         }


         private void Subdivide(VolumeElement element, Color[] pixels,  Vector2 elementOffset, int imageSizeX)
         {
             var subParts = new List<ElementSub>();
             element.transform.localEulerAngles = Vector3.zero;
             var startX = (int)(imageSizeX * elementOffset.x);
             var startY= (int)(imageSizeX * elementOffset.y);
             var factor = subDivideFactor;
             var subSizePhys = 1f / factor;
             var data = SubDivideArea(pixels, imageSizeX, blockSize, startX, startY, factor, subSizePhys);
             bool oldWay = false;
             foreach (var d in data)
             {
                 if (oldWay)
                 {
                     var inst = SpawnSubTemp(d, element, Vector3.zero);
                     subParts.Add(inst);
                     continue;
                 }
                 if (d.alphaResult == 0)
                 {
                     var startX2 = (int)(imageSizeX * d.offset.x);
                     var startY2 = (int)(imageSizeX * d.offset.y);
                     factor = 2;
                     subSizePhys = 1f / (subDivideFactor * 2);
                     var data2 = SubDivideArea(pixels, imageSizeX, d.tileSize, startX2, startY2, factor, subSizePhys);
                     foreach (var d2 in data2)
                     {
                         if(d2.alphaResult == 0)
                             continue;
                         var inst = SpawnSubTemp(d2, element, d.localPos);
                         subParts.Add(inst);
                     }
                 }
                 else if(d.alphaResult == 1)
                 {
                     var inst = SpawnSubTemp(d, element, Vector3.zero);
                     subParts.Add(inst);
                 }
           
             }

             if (subParts.Count == 0)
             {
                 statue.Puzzle.Remove(element);
                 spawnedBlocks.Remove(element.gameObject);
                 DestroyImmediate(element.gameObject);
                 return;
             }
             element.SetSubdivided(subParts, subDivideFactor);
         }
         
         private List<SubDivData> SubDivideArea(Color[] pixels, int imageSizeX, int areaSize, int startXPixel, int startYPixel, int factor, float physicalSize)
         {
             List<SubDivData> datalist = new List<SubDivData>();
             var step = (float)areaSize / factor;

             // var sizeFactor = (int)(1f / physicalSize);
             var centerQ = (float)(factor / 2);
             if (factor % 2f == 0)
                 centerQ -= 0.5f;
             var centerVec = new Vector3(centerQ, centerQ, 0);
             
             var tileSize = areaSize / factor;
             var tilesCount = imageSizeX / tileSize;
             var tiling = new Vector2(1f / tilesCount, 1f / tilesCount);

             var posXInd = 0;
             var posYInd = 0;
             for (var x = 0f; x < areaSize; x += step)
             {
                 for (var y = 0f; y < areaSize; y += step)
                 {
                     var xPixel = x + startXPixel;
                     var yPixel = y + startYPixel;
                     // var indexPixel = xPixel + yPixel * imageSizeX;
                     var offset = new Vector2(xPixel, yPixel) / imageSizeX;
                     var alphaRes = CheckAlpha(offset, tileSize, imageSizeX, pixels);
                     if (alphaRes == -1)
                     {
                         posYInd++;
                         continue;
                     }
                     
                     var localpos = (new Vector3(posXInd, posYInd,0) - centerVec ) * physicalSize;
                     var data = new SubDivData();
                     data.offset = offset;
                     data.tiling = tiling;
                     data.localPos = localpos;
                     data.physicalSize = physicalSize;
                     data.alphaResult = alphaRes;
                     data.tileSize = tileSize;
                     datalist.Add(data);
                     posYInd++;
                 }  
                 posXInd++;
                 posYInd = 0;
             }
             return datalist;
         }

         private ElementSub SpawnSubTemp(SubDivData data, VolumeElement element, Vector3 tempCenter)
         {
             var temp = new GameObject("temp");
             temp.transform.parent = element.transform;
             temp.transform.localPosition = tempCenter;

             var instance = SpawnSub(temp.transform, data.localPos);
             instance.SetSize(data.physicalSize);
             instance.colorSetter.SetMaterial(mat, data.tiling, data.offset);
             instance.transform.parent = element.transform;
             DestroyImmediate(temp);
             return instance;   
         }

         private class SubDivData
         {
             public float physicalSize;
             public int tileSize;
             public Vector3 localPos;
             public Vector2 tiling;
             public Vector3 offset;
             public int alphaResult;
         }
         
         
         // DO NOT TOUCH
         private void SetSubDivData(VolumeElement element, Vector2 offset, int imageSizeX, Color[] colors, int blocksCount)
         {
             var factor = normalSubDivFactor;
             var startX = (int)(imageSizeX * offset.x);
             var startY= (int)(imageSizeX * offset.y);
             var step = (float)blockSize / (float)factor;
             var subDivBlockSize = 1f / factor;
             var posXInd = 0;
             var posYInd = 0;
             var centerQ = (float)(factor / 2);
             if (factor % 2f == 0)
                 centerQ -= 0.5f;
             var centerVec = new Vector3(centerQ, centerQ, 0);
             var allData = new List<ElementSubDropData>();
             var tiling = new Vector2(1f,1f) * (1f / blocksCount);
             for (var x = 0f; x < blockSize; x += step)
             {
                 for (var y = 0f; y < blockSize; y += step)
                 {
                     var xPixel = x + startX;
                     var yPixel = y + startY;
                     var index = xPixel + yPixel * imageSizeX;
                     var color = colors[Mathf.RoundToInt(index)];
                     if (color.a < minAlpha)
                     {
                         posYInd++;
                         continue;
                     }
                     var localpos = (new Vector3(posXInd, posYInd,0) - centerVec ) * subDivBlockSize;
                     var uvOffset = new Vector2(xPixel/imageSizeX, yPixel/ imageSizeX);

                     var data = new ElementSubDropData();
                     data.localPos = localpos;
                     data.Tiling = tiling;
                     data.Offset = uvOffset;
                     
                     allData.Add(data);
                     posYInd++;
                 }  
                 posXInd++;
                 posYInd = 0;
             }
             element.subdivider.SetData(allData);
         }
         
         
        public void CreateByPixel()
        {
            statue.Puzzle.Clear();
            Clear();
            statue.Puzzle = new List<VolumeElement>();
            var puzzle = statue.Puzzle;
            var texture = sprite.texture;
            var colorArray = texture.GetPixels();
            var size = Mathf.Sqrt((float)colorArray.Length);
            if (size - (int)size > 0)
            {
                throw new SystemException($"the size is not a perfect square");
            }
            if (size > 128)
            {
                throw new SystemException($"{size} is too big!");
            }
            Debug.Log($"Pixels array length: {colorArray.Length}, root: {size}");
            float xPos = 0;
            float yPos = 0;
            var xSize = mainPrefab.sizeX;
            var ySize = mainPrefab.sizeY;
            var loopsCount = (int)size;
            var centerPos = GetCenter(size, xSize, ySize);
            for (int x = 0; x < loopsCount; x++)
            {
                for (int y = 0; y < loopsCount; y++)
                {
                    var element = SpawnElement();
                    var color = colorArray[x + y * loopsCount];
        
                    if (color.a < minAlpha)
                    {
                        spawnedBlocks.Remove(element.gameObject);
                        DestroyImmediate(element.gameObject);
                    }
                    else
                    {
                        if (clampAlpha)
                            color.a = 1f;
                        element.X = x;
                        element.Y = y;
                        puzzle.Add(element);
                        element.colorSetter.SetColor(color);
                        element.transform.localPosition = centerPos + new Vector3(xPos, yPos, 0f);
                        var statueElement = (StatueElement)element;
                        if (statueElement != null)
                            statueElement.statue = statue;
                    }
                    yPos += ySize;
                }
                xPos += xSize;
                yPos = 0;
            }
            statue.Puzzle = puzzle;
            if(autoSetupNeighbours)
                SetupNeighbours();
        }
        
        
        

        public void SetupNeighbours()
        {
            var puzzle = statue.Puzzle;
            for (var i = 0; i < puzzle.Count; i++)
            {
                var p = puzzle[i];
                p.neighbourIndices = new List<int>();
                
                var neighbours = CastNeighbours(p, statue.transform.localScale.x);
                foreach (var n in neighbours)
                {
                    p.neighbourIndices.Add(puzzle.IndexOf(n));
                }
            }
        }
        
        private Vector3 GetCenter(float size, float xSize, float ySize)
        {
            var centerPos = new Vector3(-size * xSize / 2, -size * ySize / 2,0);
            if (pivotAtBottom)
                centerPos.y = 0;
            return centerPos;
        }

        private List<VolumeElement> CastNeighbours(VolumeElement element, float scale)
        {
            var elements = new List<VolumeElement>();
            var results = Physics.OverlapSphere(element.transform.position, (element.sizeX) * scale);
            foreach (var result in results)
            {
                var el = result.gameObject.GetComponent<VolumeElement>();
                if(el != null && el != element)
                    elements.Add(el);
            }
            return elements;
        }
        
        private ElementSub SpawnSub(Transform parent, Vector3 localPos)
        {
#if UNITY_EDITOR
            var prefabInst = PrefabUtility.InstantiatePrefab(subDivPrefab, parent) as ElementSub;
            prefabInst.transform.localPosition = localPos;
            return prefabInst;
#endif
            var instance = Instantiate(subDivPrefab, parent);
            instance.transform.localPosition = localPos;
            return instance;
        }
        
        private VolumeElement SpawnElement()
        {
            #if UNITY_EDITOR
            var prefabInst = PrefabUtility.InstantiatePrefab(mainPrefab, statue.transform) as VolumeElement;
            spawnedBlocks.Add(prefabInst.gameObject);
            return prefabInst;
            #endif
            
            var instance = Instantiate(mainPrefab, statue.transform);
            spawnedBlocks.Add(instance.gameObject);
            return instance;
        }

        public void Clear()
        {
            Destroyer.ClearGOList(spawnedBlocks);
        }
        
        
        

    }
}
