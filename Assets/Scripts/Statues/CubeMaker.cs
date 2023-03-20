using UnityEditor;
using UnityEngine;

namespace Statues
{
    public class CubeMaker : MonoBehaviour
    {
        public MeshFilter filer;
        public Mesh mesh;
        public float size = 1f;

        public void Debug()
        {
            
        }

        public void BuildCube()
        {
            Mesh tempMesh = new Mesh();
            var vertices = new Vector3[4 * 6];
            // top
            vertices[0] = new Vector3(-1, 1, 1) * size / 2;
            vertices[1] = new Vector3(1, 1, 1) * size / 2;
            vertices[2] = new Vector3(1, -1, 1) * size / 2;
            vertices[3] = new Vector3(-1, -1, 1) * size / 2;
            // bot
            vertices[4] = new Vector3(-1, 1, -1) * size / 2;
            vertices[5] = new Vector3(1, 1, -1) * size / 2;
            vertices[6] = new Vector3(1, -1, -1) * size / 2;
            vertices[7] = new Vector3(-1, -1, -1) * size / 2;
            // front
            vertices[8] = new Vector3(-1, -1, 1) * size / 2;
            vertices[9] = new Vector3(1, -1, 1) * size / 2;
            vertices[10] = new Vector3(1, -1, -1) * size / 2;
            vertices[11] = new Vector3(-1, -1, -1) * size / 2;          
            // back
            vertices[12] = new Vector3(-1, 1, 1) * size / 2;
            vertices[13] = new Vector3(1, 1, 1) * size / 2;
            vertices[14] = new Vector3(1, 1, -1) * size / 2;
            vertices[15] = new Vector3(-1, 1, -1) * size / 2;         
            
            // right
            vertices[16] = new Vector3(1, -1, 1) * size / 2;
            vertices[17] = new Vector3(1, 1, 1) * size / 2;
            vertices[18] = new Vector3(1, 1, -1) * size / 2;
            vertices[19] = new Vector3(1, -1, -1) * size / 2;      
            
            // left
            vertices[20] = new Vector3(-1, -1, 1) * size / 2;
            vertices[21] = new Vector3(-1, 1, 1) * size / 2;
            vertices[22] = new Vector3(-1, 1, -1) * size / 2;
            vertices[23] = new Vector3(-1, -1, -1) * size / 2;    
            
            var triangles = new int[12 * 3]
            {
                // top 2
                0,1,3,
                1,2,3,
                // bot 2
                7,6,4,
                6,5,4,
                // front
                8,9,11,
                9,10,11,
                // back
                15,14,12,
                14,13,12,
                // right
                16, 17, 19,
                17, 18, 19,
                // left 
                23, 22, 20,
                22, 21, 20
            };
            var uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),

                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0),
                
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            };

            var normals = new Vector3[4 * 6]
            {
                new Vector3(0, 0, 1),
                new Vector3(0, 0, 1),
                new Vector3(0, 0, 1),
                new Vector3(0, 0, 1),

                new Vector3(0, 0, -1),
                new Vector3(0, 0, -1),
                new Vector3(0, 0, -1),
                new Vector3(0, 0, -1),

                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, -1, 0),

                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 0),

                new Vector3(1, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(1, 0, 1),

                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(-1, 0, 0),
            };
            
            tempMesh.vertices = vertices;
            tempMesh.triangles = triangles;
            tempMesh.uv = uv;
            this.mesh = tempMesh;
            filer.mesh = mesh;

        }
    }








    #if UNITY_EDITOR
    [CustomEditor(typeof(CubeMaker))]
    public class UVFlipperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var me = target as CubeMaker;
            base.OnInspectorGUI();
            if (GUILayout.Button("Debug"))
            {
                
            }

            if (GUILayout.Button("Create"))
            {
                me.BuildCube();
            }
        }
    }
    #endif
}