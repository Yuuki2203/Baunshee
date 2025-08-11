#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[Tooltip("指定した影響をカメラに適用します")]
public class PC2DApplyInfluence : FsmStateActionProCamera2DBase
{
[Tooltip("適用する影響を表すベクトル")]
	public FsmVector2 Influence;

	public override void OnUpdate()
	{
		if (ProCamera2D.Instance != null)
			ProCamera2D.Instance.ApplyInfluence(Influence.Value);
	}
}
#endif