#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[Tooltip("カメラのターゲットを削除します")]
public class PC2DRemoveCameraTarget : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ターゲットの Transform")]
	public FsmGameObject target;

[Tooltip("このターゲットの影響度が 0 に到達するまでの時間。滑らかな遷移に利用します。")]
	public FsmFloat duration = 0;

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null && target.Value)
			ProCamera2D.Instance.RemoveCameraTarget(target.Value.transform, duration.Value);

		Finish();
	}
}
#endif