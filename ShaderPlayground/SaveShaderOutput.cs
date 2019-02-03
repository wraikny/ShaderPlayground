using System;
using System.Linq;

namespace ShaderPlayground
{
	public class SaveShaderOutput : PostEffectFromFile
    {
        public SaveShaderOutput(string filename)
            : base(filename)
        {

        }

        public void Draw(asd.RenderTexture2D target, float time)
        {
            material2d.SetFloat("time", time);
            material2d.SetVector2DF("windowSize", target.Size.To2DF());

            // マテリアルを経由してシェーダー内のg_texture変数に画面の画像(src)を入力する。
            material2d.SetTexture2D("g_texture", target);

            // 出力画像(dst)に指定したマテリアルで描画する。
            DrawOnTexture2DWithMaterial(target, material2d);
        }
    }
}