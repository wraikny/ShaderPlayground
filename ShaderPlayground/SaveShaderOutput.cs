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

            // �}�e���A�����o�R���ăV�F�[�_�[����g_texture�ϐ��ɉ�ʂ̉摜(src)����͂���B
            material2d.SetTexture2D("g_texture", target);

            // �o�͉摜(dst)�Ɏw�肵���}�e���A���ŕ`�悷��B
            DrawOnTexture2DWithMaterial(target, material2d);
        }
    }
}