using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeUniverse.Common.Procedural.Galaxy
{
    public class ProceduralGalaxyParameters
    {
        public string referenceAssetTexturePath { get; set; }
        public Vector3 galaxySize { get; set; }
        public float sectorSize { get; set; }
        public int maxStarCountPerSector { get; set; }
        public int maxStarTypes { get; set; }
        public float minDensity { get; set; }
    }

    public class ProceduralGalaxyGenerator
    {
        private Texture2D referenceImage { get; set; }
        private ProceduralGalaxyParameters parameters { get; set; }

        public ProceduralGalaxyGenerator(ProceduralGalaxyParameters parameters)
        {
            this.parameters = parameters;
            this.referenceImage = Resources.Load<Texture2D>(parameters.referenceAssetTexturePath);
        }

        public class Result
        {
            public Vector3 position { get; set; }
            public float temperature { get; set; }
            public float size { get; set; }
            public int type { get; set; }
        }

        private float GetUniformDensityFromImage(Vector2 pos)
        {
            // transform to image coords
            Vector2 impos = new Vector2(
                (pos.x / parameters.sectorSize) + referenceImage.width / 2.0f,
                (pos.y / parameters.sectorSize) + referenceImage.height / 2.0f);

            Color c = referenceImage.GetPixel((int)(impos.x), (int)(impos.y));

            return (c.r + c.g + c.b) / 3.0f;
        }

        public List<Result> Generate()
        {
            List<Result> items = new List<Result>();

            LibNoise.FastNoise noise = new LibNoise.FastNoise(10);
            noise.Frequency = 1;

            for (int i = 0; i < referenceImage.width; i++)
            {
                for (int j = 0; j < referenceImage.height; j++)
                {
                    Color c = referenceImage.GetPixel(i, j);
                    float density = (c.r + c.g + c.b)/3.0f;

                    if (parameters.minDensity > 0.0f && density < parameters.minDensity)
                        continue;

                    Vector2 pos2d = new Vector2(((float)i - referenceImage.width / 2.0f), ((float)j - referenceImage.height / 2.0f));
                    Vector3 sectorPosition = new Vector3(pos2d.x * parameters.sectorSize, pos2d.y * parameters.sectorSize, 0.0f);
                    float radiusMod = (pos2d.magnitude > 0.0f) ? (1.0f / pos2d.sqrMagnitude) : 1.0f;
                    int starCount = (int)(density * parameters.maxStarCountPerSector * radiusMod);

                    while (starCount > 0)
                    {
                        Vector3 starPosition = sectorPosition + UnityEngine.Random.insideUnitSphere * parameters.sectorSize;

                        starPosition.Set(starPosition.x, starPosition.y , starPosition.z * parameters.galaxySize.z);

                        float nm = (float)noise.GetValue((double)starPosition.x, (double)starPosition.y, (double)starPosition.z);

                        starPosition.Set(starPosition.x, starPosition.y, starPosition.z * nm * radiusMod);

                        starCount--;

                        Result result = new Result();
                        result.type = UnityEngine.Random.Range(0, parameters.maxStarTypes);
                        result.position = starPosition;
                        items.Add(result);
                    }
                }
            }

            Debug.Log("stars created=" + items.Count);

            return items;
            
        }
    }
}
