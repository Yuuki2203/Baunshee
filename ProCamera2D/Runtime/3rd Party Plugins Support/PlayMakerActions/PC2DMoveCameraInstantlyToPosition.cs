#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[Tooltip("カメラを指定位置へ即座に移動します")]
public class PC2DMoveCameraInstantlyToPosition : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("カメラの最終位置")]
	public FsmVector3 CameraPos;

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null)
			ProCamera2D.Instance.MoveCameraInstantlyToPosition(CameraPos.Value);

		Finish();
	}
}
#endif