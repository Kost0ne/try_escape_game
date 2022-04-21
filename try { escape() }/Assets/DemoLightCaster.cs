using UnityEngine;

public class DemoLightCaster : MonoBehaviour
{
    public GameObject[] sceneObjects;
    public GameObject[] ignoreCollision;

    public float offset = 0.01f;

    public GameObject lightRays;

    private Mesh mesh;

    public GameObject player;

    public struct angledVerts
    {
        public Vector3 vert;
        public float angle;
        public Vector2 uv;
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = lightRays.GetComponent<MeshFilter>().mesh;
        transform.position = player.transform.position + new Vector3(-0.75f, 0.55f, 0);
    }

    public static int[] AddItemsToArray(int[] original, int itemToAdd1, int itemToAdd2, int itemToAdd3)
    {
        int[] finalArray = new int[original.Length + 3];
        for (int i = 0; i < original.Length; i++)
        {
            finalArray[i] = original[i];
        }

        finalArray[original.Length] = itemToAdd1;
        finalArray[original.Length + 1] = itemToAdd2;
        finalArray[original.Length + 2] = itemToAdd3;
        return finalArray;
    }

    public static Vector2[] ConcatArrays(Vector2[] first, Vector2[] second)
    {
        var concatted = new Vector2[first.Length + second.Length];

        System.Array.Copy(first, concatted, first.Length);
        System.Array.Copy(second, 0, concatted, first.Length, second.Length);

        return concatted;
    }

    // Update is called once per frame
    void Update()
    {
        mesh.Clear();


        var lanternDirection = Movment.IsFacingRight ? 1 : -1;
        transform.position = player.transform.position + new Vector3(1.5f * lanternDirection, -0.05f, 0);

        var objverts = sceneObjects[0].GetComponent<SpriteRenderer>().sprite.vertices;
        for (var i = 1; i < sceneObjects.Length; i++)
        {
            objverts = ConcatArrays(objverts, sceneObjects[i].GetComponent<SpriteRenderer>().sprite.vertices);
        }

        var angledverts = new angledVerts[(objverts.Length * 2)];
        var verts = new Vector3[(objverts.Length * 2) + 1];
        var uvs = new Vector2[(objverts.Length * 2) + 1];

        verts[0] = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(this.transform.position);
        uvs[0] = new Vector2(verts[0].x, verts[0].y);

        var h = 0;

        var myLoc = transform.position;
        foreach (var obj in sceneObjects)
        {
            var sprite = obj.GetComponent<SpriteRenderer>().sprite.vertices;
            foreach (var vertice in sprite)
            {
                var vertLoc = obj.transform.localToWorldMatrix.MultiplyPoint3x4(vertice);

                var angle1 = Mathf.Atan2((vertLoc.y - myLoc.y - offset), (vertLoc.x - myLoc.x - offset));
                var angle2 = Mathf.Atan2((vertLoc.y - myLoc.y + offset), (vertLoc.x - myLoc.x + offset));

                //Physics.Raycast(myLoc, new Vector2(vertLoc.x-myLoc.x-offset,vertLoc.y-myLoc.y-offset), out hit, 100);
                var hit = Physics2D.Raycast(myLoc, new Vector2(vertLoc.x - myLoc.x - offset,
                    vertLoc.y - myLoc.y - offset), 200, 3);
                var hit2 = Physics2D.Raycast(myLoc, new Vector2(vertLoc.x - myLoc.x + offset,
                    vertLoc.y - myLoc.y + offset), 200, 3);


                Debug.DrawLine(myLoc, hit.point, Color.red);
                Debug.DrawLine(myLoc, hit2.point, Color.green);

                angledverts[(h * 2)].vert = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
                angledverts[(h * 2)].angle = angle1;
                angledverts[(h * 2)].uv = new Vector2(angledverts[(h * 2)].vert.x, angledverts[(h * 2)].vert.y);

                angledverts[(h * 2) + 1].vert = lightRays.transform.worldToLocalMatrix.MultiplyPoint3x4(hit2.point);
                angledverts[(h * 2) + 1].angle = angle2;
                angledverts[(h * 2) + 1].uv =
                    new Vector2(angledverts[(h * 2) + 1].vert.x, angledverts[(h * 2) + 1].vert.y);

                h++;
            }
        }

        System.Array.Sort(angledverts,
            delegate(angledVerts one, angledVerts two) { return one.angle.CompareTo(two.angle); });

        for (var i = 0; i < angledverts.Length; i++)
        {
            verts[i + 1] = angledverts[i].vert;
            uvs[i + 1] = angledverts[i].uv;
        }

        mesh.vertices = verts;

        for (var i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(uvs[i].x + .5f, uvs[i].y + .5f);
        }

        mesh.uv = uvs;

        int[] triangles = {0, 1, verts.Length - 1};

        for (int i = verts.Length - 1; i > 0; i--)
        {
            triangles = AddItemsToArray(triangles, 0, i, i - 1);
        }

        mesh.triangles = triangles;
    }
}