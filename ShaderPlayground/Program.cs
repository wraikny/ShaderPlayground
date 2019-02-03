using System;

namespace ShaderPlayground
{
    class MainClass
    {
        private static void PostEffect()
        {
            // シーン、レイヤー、画像を表示するオブジェクトを生成する。
            var scene = new asd.Scene();
            var layer = new asd.Layer2D();

            // シーンを変更し、そのシーンにレイヤーを追加し、そのレイヤーにオブジェクトを追加する。
            asd.Engine.ChangeScene(scene);
            scene.AddLayer(layer);

            // レイヤーにポストエフェクトを適用する。
            layer.AddPostEffect(new CustomPostEffect("ShaderCode/01/sample.glsl"));

            while (asd.Engine.DoEvents())
            {
                asd.Engine.Update();
            }
        }

        /*
        private static void SaveShaderAsTexture()
        {
            var target = asd.Engine.Graphics.CreateRenderTexture2D(1920, 1080, asd.TextureFormat.R8G8B8A8_UNORM_SRGB);
            var manager = new SaveShaderOutput("ShaderCode/01/sample.hlsl");

            manager.Draw(target, 0.05f);
            target.Save("OutputImages/sample01.png");
        }
        */

        public static void Main(string[] args)
        {
            // Altseedを初期化する。
            asd.Engine.Initialize("ShaderPlayground", 800, 450, new asd.EngineOption());

            PostEffect();

            asd.Engine.Terminate();
        }
    }
}
