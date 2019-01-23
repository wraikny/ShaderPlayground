using System;
namespace ShaderPlayground
{
    public class CustomPostEffect : asd.PostEffect
    {
        private readonly string filename;

        private asd.Shader2D shader;
        private asd.Material2D material2d;

        private float time;

        public CustomPostEffect(string filename)
        {
            filename = filename;
        }

        private void SetMaterial()
        {
            // シェーダーをHLSL/GLSLから生成する。
            shader = asd.Engine.Graphics.CreateShader2D(filename);

            // シェーダーからマテリアルを生成する。
            material2d = asd.Engine.Graphics.CreateMaterial2D(shader);
        }

        protected override void OnDraw(asd.RenderTexture2D dst, asd.RenderTexture2D src)
        {
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
