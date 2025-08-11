#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[Tooltip("カメラが追従するターゲットを追加します。")]
public class PC2DAddCameraTarget : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("追加するカメラターゲット")]
	public FsmGameObject target;

	[HasFloatSlider(0, 1)]
[Tooltip("全ターゲットの平均位置を計算する際に、このターゲットの水平位置が与える影響度")]
	public FsmFloat targetInfluenceH = 1;

	[HasFloatSlider(0, 1)]
[Tooltip("全ターゲットの平均位置を計算する際に、このターゲットの垂直位置が与える影響度")]
	public FsmFloat targetInfluenceV = 1;

[Tooltip("このターゲットが指定の影響度に達するまでの時間")]
	public FsmFloat duration = 0;

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null && target.Value)
			ProCamera2D.Instance.AddCameraTarget(target.Value.transform, targetInfluenceH.Value, targetInfluenceV.Value, duration.Value);

		Finish();
	}
}
#endif