using System;
using System.Collections.Generic;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.UI.Vertexes;
using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.UI.Renderers
{
    public sealed class TextureRenderer : IDisposable
    {
        private readonly Device device;

        private readonly Sprite sprite;

        private readonly Dictionary<string, Texture> textures;

        public TextureRenderer(Device device)
        {
            this.device = device;
            sprite = new Sprite(device);
            textures = new Dictionary<string, Texture>();
        }

        public void Begin()
        {
            sprite.Begin();
        }

        public void DrawBox(RectangleF rect, Color color)
        {
            ColoredVertex[] data =
            {
                new ColoredVertex(rect.Left, rect.Top, color),
                new ColoredVertex(rect.Right, rect.Top, color),
                new ColoredVertex(rect.Right, rect.Bottom, color),
                new ColoredVertex(rect.Left, rect.Bottom, color)
            };
            device.SetTexture(0, null);
            DrawColoredVertices(PrimitiveType.TriangleFan, 2, data);
        }

        public void DrawFrame(RectangleF rect, float borderWidth, Color color)
        {
            float half = borderWidth / 2f;

            // Outer rectangle
            var p1 = new ColoredVertex(rect.Left - half, rect.Top - half, color);
            var p2 = new ColoredVertex(rect.Right + half, rect.Top - half, color);
            var p3 = new ColoredVertex(rect.Right + half, rect.Bottom + half, color);
            var p4 = new ColoredVertex(rect.Left - half, rect.Bottom + half, color);

            // Inner rectangle
            var p5 = new ColoredVertex(rect.Left + half, rect.Top + half, color);
            var p6 = new ColoredVertex(rect.Right - half, rect.Top + half, color);
            var p7 = new ColoredVertex(rect.Right - half, rect.Bottom - half, color);
            var p8 = new ColoredVertex(rect.Left + half, rect.Bottom - half, color);

            ColoredVertex[] data = { p1, p5, p2, p6, p3, p7, p4, p8, p1, p5 };
            device.SetTexture(0, null);
            DrawColoredVertices(PrimitiveType.TriangleStrip, 8, data);
        }

        public void DrawImage(string fileName, RectangleF rect, Color color, float repeatX)
        {
            TexturedVertex[] data =
            {
                new TexturedVertex(rect.Left, rect.Top, 0, 0, color),
                new TexturedVertex(rect.Right, rect.Top, repeatX, 0, color),
                new TexturedVertex(rect.Right, rect.Bottom, repeatX, 1, color),
                new TexturedVertex(rect.Left, rect.Bottom, 0, 1, color)
            };
            device.SetTexture(0, GetTexture(fileName));
            device.SetSamplerState(0, SamplerState.AddressU, TextureAddress.Wrap);
            DrawTexturedVertices(PrimitiveType.TriangleFan, 2, data);
        }

        public void DrawImage(string fileName, RectangleF rect, RectangleF uvCoords, Color color)
        {
            TexturedVertex[] data =
            {
                new TexturedVertex(rect.Left, rect.Top, uvCoords.Left, uvCoords.Top, color),
                new TexturedVertex(rect.Right, rect.Top, uvCoords.Right, uvCoords.Top, color),
                new TexturedVertex(rect.Right, rect.Bottom, uvCoords.Right, uvCoords.Bottom, color),
                new TexturedVertex(rect.Left, rect.Bottom, uvCoords.Left, uvCoords.Bottom, color)
            };
            device.SetTexture(0, GetTexture(fileName));
            DrawTexturedVertices(PrimitiveType.TriangleFan, 2, data);
        }

        public void End()
        {
            sprite.End();
        }

        public void Flush()
        {
            lock (textures)
            {
                textures.ForEach((key, texture) => texture.Dispose());
            }
            lock (textures)
            {
                textures.Clear();
            }
        }

        public void Dispose()
        {
            sprite.Dispose();
            Flush();
        }

        private void DrawColoredVertices(PrimitiveType type, int count, ColoredVertex[] data)
        {
            using (var declaration = new VertexDeclaration(device, ColoredVertex.VertexElements))
            {
                device.VertexDeclaration = declaration;
                device.DrawUserPrimitives(type, count, data);
            }
        }

        private void DrawTexturedVertices(PrimitiveType type, int count, TexturedVertex[] data)
        {
            using (var declaration = new VertexDeclaration(device, TexturedVertex.VertexElements))
            {
                device.VertexDeclaration = declaration;
                device.DrawUserPrimitives(type, count, data);
            }
        }

        private Texture GetTexture(string fileName)
        {
            lock (textures)
            {
                Texture texture;
                if (!textures.TryGetValue(fileName, out texture))
                {
                    texture = Texture.FromFile(device, fileName);
                    textures.Add(fileName, Texture.FromFile(device, fileName));
                }
                return texture;
            }
        }
    }
}