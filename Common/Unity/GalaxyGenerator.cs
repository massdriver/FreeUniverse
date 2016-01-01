using FreeUniverse.Common.Procedural;
using FreeUniverse.Common.Procedural.Galaxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Unity
{
    public class StarSystemInfo : MonoBehaviour
    {
        public string systemName;
        public uint planetCount;
    }

    public class GalaxyGenerator : MonoBehaviour
    {
        public string referenceAssetTexturePath;
        public Vector3 galaxySize;
        public float sectorSize;
        public int maxStarCountPerSector;
        public float minDensity;

        public void Start()
        {
            
            ProceduralGalaxyParameters parameters = new Procedural.Galaxy.ProceduralGalaxyParameters();

            string[] prefabs =
            {
                "Sprites/prefab_star_particle",
                "Sprites/prefab_star_particle_red",
                "Sprites/prefab_star_particle_blue",
                "Sprites/prefab_star_particle_deep_red"
            };

            parameters.maxStarTypes = prefabs.Length;

            parameters.referenceAssetTexturePath = this.referenceAssetTexturePath;
            parameters.galaxySize = this.galaxySize;
            parameters.sectorSize = this.sectorSize;
            parameters.maxStarCountPerSector = this.maxStarCountPerSector;
            parameters.minDensity = this.minDensity;

            ProceduralGalaxyGenerator generator = new Procedural.Galaxy.ProceduralGalaxyGenerator(parameters);
            List<ProceduralGalaxyGenerator.Result> result = generator.Generate();

            StarSystemNameGenerator starSystemNameGenerator = new StarSystemNameGenerator(File.ReadAllLines("C:\\Parts\\star_parts.txt"));
            
            foreach (ProceduralGalaxyGenerator.Result rst in result)
            {
                GameObject obj = UnityEngine.Object.Instantiate(Resources.Load(prefabs[rst.type]) as GameObject) as GameObject;
                obj.transform.position = rst.position;

                SphereCollider collider = obj.AddComponent<SphereCollider>();
                collider.radius = 1.0f;

                StarSystemInfo info = obj.AddComponent<StarSystemInfo>();

                uint hid = ((uint)Math.Floor(obj.transform.position.x) * 73856093) ^ ((uint)Math.Floor(obj.transform.position.y) * 19349663) ^ ((uint)Math.Floor(obj.transform.position.z) * 83492791);

                FastRandom.SetSeed(hid);
                info.planetCount = FastRandom.Next(8);
                info.systemName = starSystemNameGenerator.Generate(FastRandom.Next());
            }
            
        }

        public GameObject cameraObject;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mp = Input.mousePosition;

                Camera camera = cameraObject.GetComponent<Camera>();

                Ray ray = camera.ScreenPointToRay(mp);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5000.0f))
                {
                    StarSystemInfo info = hit.collider.gameObject.GetComponent<StarSystemInfo>();

                    Debug.Log("system=" + info.systemName + ", planets=" + info.planetCount);
                }
            }
        }
    }
}
