using System;
using System.Linq;

namespace ShaderPlayground
{
    public class CustomPostEffect : PostEffectFromFile
    {

        private float time;

        public CustomPostEffect(string filename)
            : base(filename)
        {
            time = 0.0f;
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
