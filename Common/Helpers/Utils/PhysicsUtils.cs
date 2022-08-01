using System.Collections.Generic;
using Genbox.VelcroPhysics.Shared;
using Genbox.VelcroPhysics.Tools.Triangulation.TriangulationBase;
using Genbox.VelcroPhysics.Utilities;
using MonoGame.Extended.Sprites;

namespace ECSGame.Common.Helpers.Utils
{
    public static class PhysicsUtils
    {
        public static Vertices SpriteToVertices(Sprite sprite)
        {
            Texture2D polygonTexture = sprite.TextureRegion.Texture;

            //Create an array to hold the data from the texture
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];

            //Transfer the texture data to the array
            polygonTexture.GetData(data);

            //Find the vertices that makes up the outline of the shape in the texture
            Vertices verts = PolygonUtils.CreatePolygon(data, polygonTexture.Width);

            Vector2 centroid = -verts.GetCentroid();
            verts.Translate(ref centroid);

            Vector2 scale = new Vector2(0.07f, -0.07f);
            verts.Scale(ref scale);

            return verts;
        }

        public static List<Vertices> SpriteToVerticesList(Sprite sprite)
        {
            Texture2D polygonTexture = sprite.TextureRegion.Texture;

            //Create an array to hold the data from the texture
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];

            //Transfer the texture data to the array
            polygonTexture.GetData(data);

            //Find the vertices that makes up the outline of the shape in the texture
            Vertices verts = PolygonUtils.CreatePolygon(data, polygonTexture.Width);

            Vector2 centroid = -verts.GetCentroid();
            verts.Translate(ref centroid);

            Vector2 scale = new Vector2(0.07f, -0.07f);
            verts.Scale(ref scale);

            var vertsList = Triangulate.ConvexPartition(verts, TriangulationAlgorithm.Bayazit);

            return vertsList;
        }
    }
}