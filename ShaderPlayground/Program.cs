using System;

namespace ShaderPlayground
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Altseedを初期化する。
            asd.Engine.Initialize("ShaderPlayground", 800, 450, new asd.EngineOption());

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

            asd.Engine.Terminate();
        }
    }
}
