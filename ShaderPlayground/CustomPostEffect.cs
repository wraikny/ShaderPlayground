using System;
using System.Linq;

namespace ShaderPlayground
{
    public class CustomPostEffect : asd.PostEffect
    {
        private string filename;

        private asd.Shader2D shader;
        private asd.Material2D material2d;

        private float time;

        public CustomPostEffect(string filename)
        {
            this.filename = filename;
            SetMaterial();
        }

        private void SetMaterial()
        {
            if(asd.Engine.File.Exists(filename))
            {
                {
                    var buf = asd.Engine.File.CreateStaticFile(filename).Buffer;
                    var s = System.Text.Encoding.UTF8.GetString(buf);
                    
                    // シェーダーをHLSL/GLSLから生成する。
                    shader = asd.Engine.Graphics.CreateShader2D(s);
                    
                    // シェーダーからマテリアルを生成する。
                    material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
                }
            }
            else
            {
                Console.WriteLine($"File '{filename}' does not exist");
            }
        }

        protected override void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src)
        {
            if(asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Release)
            {
                SetMaterial();
            }

            time = time + 0.05f;
            material2d.SetFloat("time", time);
            material2d.SetVector2DF("windowSize", asd.Engine.WindowSize.To2DF());

            // マテリアルを経由してシェーダー内のg_texture変数に画面の画像(src)を入力する。
            material2d.SetTexture2D("g_texture", src);
            // 出力画像(dst)に指定したマテリアルで描画する。
            DrawOnTexture2DWithMaterial(dst, material2d);
        }
    }
}
