using System;
using System.Linq;

namespace ShaderPlayground
{
    public class PostEffectFromFile : asd.PostEffect
    {
        private string filename;

        protected asd.Shader2D shader;
        protected asd.Material2D material2d;

        public PostEffectFromFile(string filename)
        {
            this.filename = filename;
            SetMaterial();
        }

        protected void SetMaterial()
        {
            if (asd.Engine.File.Exists(filename))
            {
                {
                    var buf = asd.Engine.File.CreateStaticFile(filename).Buffer;
                    var s = System.Text.Encoding.UTF8.GetString(buf);

                    // �V�F�[�_�[��HLSL/GLSL���琶������B
                    shader = asd.Engine.Graphics.CreateShader2D(s);

                    // �V�F�[�_�[����}�e���A���𐶐�����B
                    material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
                }
            }
            else
            {
                Console.WriteLine($"File '{filename}' does not exist");
            }
        }
    }
}
